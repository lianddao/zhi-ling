using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using PlanExecut.Code;
using PlanExecut.Domain.Entity.ZhiLingManage;
using PlanExecut.Repository.ZhiLingManage;
using PlanExecut.Web.Areas.ZhiLingManage.DTO;
using PlanExecut.Web.Areas.ZhiLingManage.VO;
using System.Diagnostics;
using System.Collections;

namespace PlanExecut.Web.Areas.ZhiLingManage.Services {

	public partial class ZhiLingService {

		private List<UserZhiLing> __主要控制指标_下指令使用的装置数据2() {
			var query1 = from a in myDataSource._UserZhiLing.IQueryable().Where(i => i.Type == 0 && i.Enable == 1).ToList()
						 join b in myDataSource._单位.IQueryable()
						 on a.UnitId == null ? 0 : a.UnitId.Value equals b.F_Id into ab
						 from abResult in ab.DefaultIfEmpty()
						 select new {
							 aaa = a,
							 bbb = abResult
						 };
			var list1 = query1.ToList().Select(item => {
				if (item.bbb != null) {
					item.aaa.Unit = item.bbb.Unit;
				}
				return item.aaa;
			}).ToList();
			return list1;
		}


		public 下指令For帆软报表DTO 主要控制指标_下指令时使用的DTO(string root_mesid) {
			下指令For帆软报表DTO dto = new 下指令For帆软报表DTO();

			// 模板所需数据
			List<UserZhiLing> list1 = __主要控制指标_下指令使用的装置数据2();

			// 指令模板
			var query2 = from a in list1
						 join b in myDataSource._YuZhiLingTemp.IQueryable().Where(i => i.Type == 0)
						 on new { device = a.DeviceUnitCode, a.MaterialCode } equals new { device = b.DeviceUnitCode, b.MaterialCode } into ab
						 from abResult in ab.DefaultIfEmpty()
						 select new {
							 aaa = a,
							 bbb = abResult
						 };
			// list2 是 模板数据
			var list2 = query2.Select(item => {
				YuZhiLingTemp one = item.aaa.Convert<YuZhiLingTemp>();

				one.ID = -1;
				one.Type = 0;
				one.DeviceUnitCode = item.aaa.DeviceUnitCode;
				one.DianWeiName = item.aaa.PointNumName;
				one.DianWeiHao = item.aaa.PointNumOrValue;
				one.DianWeiType = item.aaa.Remark;
				one.OrderTitle = item.aaa.Name;
				one.OrderStartTime = DateTime.Now.Date;
				one.OrderEndTime = DateTime.Now.AddDays(7).Date;

				if (item.bbb != null && root_mesid == null) {
					one.IsSelected = item.bbb.IsSelected;
					one.PlanValue = item.bbb.PlanValue;
					one.ShangXiaXian = item.bbb.ShangXiaXian;
					one.LowValue = item.bbb.LowValue;
					one.UpValue = item.bbb.UpValue;
					one.OrderStartTime = item.bbb.OrderStartTime;
					one.OrderEndTime = item.bbb.OrderEndTime;
				}


				return one;
			}).ToList();




			if (false == string.IsNullOrWhiteSpace(root_mesid)) {
				// 判断:如果 [YuZhiLing] 存在对应的行,则取值赋值到 list2
				var query3 = from a in myDataSource._ZhiLingMes.IQueryable().Where(i => i.ROOT_MES_ID == root_mesid && i.Type == 0).ToList()
							 join b in myDataSource._YuZhiLing.IQueryable()
							 on a.ID equals b.MesTableId into ab
							 from abResult in ab
							 select new {
								 aaa = a,
								 bbb = abResult
							 };
				var list3 = query3.ToList().Select(item => {
					item.aaa.PlanValue = item.bbb.PlanValue;
					item.aaa.ShangXiaXian = item.bbb.ShangXiaXian;
					item.aaa.LowValue = item.bbb.LowValue;
					item.aaa.UpValue = item.bbb.UpValue;
					item.aaa.OrderStartTime = item.bbb.OrderStartTime;
					item.aaa.OrderEndTime = item.bbb.OrderEndTime;

					item.bbb.ROOT_MES_TITLE = item.aaa.ROOT_MES_TITLE;

					return item.bbb;
				}).ToList();

				foreach (var one in list3) {
					var target = list2.FirstOrDefault(i => i.DeviceUnitCode == one.DeviceUnitCode && i.MaterialCode == one.MaterialCode);
					if (target != null) {
						target.IsSelected = true;
						target.PlanValue = one.PlanValue;
						target.ShangXiaXian = one.ShangXiaXian;
						target.LowValue = one.LowValue;
						target.UpValue = one.UpValue;
						target.OrderStartTime = one.OrderStartTime;
						target.OrderEndTime = one.OrderEndTime;
					}
				}

				dto.已定义的预指令 = list2;
				var 已下达的指令 = list2.Where(i => i.IsSelected).Convert<List<ZhiLingInfo>>();
				dto.指令字符串 = __主要控制指标_格式化为指令字符串(已下达的指令);

				var 参考对象 = list3.FirstOrDefault();
				if (参考对象 != null) {
					dto.开始日期 = 参考对象.OrderStartTime.ToDateString();
					dto.结束日期 = 参考对象.OrderEndTime.ToDateString();
					dto.指令标题 = 参考对象.ROOT_MES_TITLE;
				}
			}
			else {
				dto.已定义的预指令 = list2.ToList();
				dto.指令字符串 = __主要控制指标_格式化为指令字符串(null);
			}


			return dto;
		}





		public string 主要控制指标_追加指令的快速保存(List<YuZhiLingTemp> selectedList, string root_mesid) {

			// 1. root_mesid对应的数据
			var query1 = from a in myDataSource._ZhiLingMes.IQueryable().Where(i => i.ROOT_MES_ID == root_mesid).ToList()
						 join b in myDataSource._YuZhiLing.IQueryable()
						 on a.ID equals b.MesTableId into ab
						 from abResult in ab
						 where a.Type == 0
						 select abResult;

			// 2. 识别受到更改的指令
			var query2 = from a in selectedList
						 join b in query1
						 on
						 new { a.PlanValue, a.ShangXiaXian, a.LowValue, a.UpValue }
						 equals
						 new { b.PlanValue, b.ShangXiaXian, b.LowValue, b.UpValue }
						 into ab
						 from abResult in ab.DefaultIfEmpty()
						 where abResult == null
						 select a;
			var list2 = query2.ToList();



			myDataSource._YuZhiLingTemp.ClearAll();

			var 参考对象 = myDataSource._ZhiLingMes.IQueryable().FirstOrDefault(i => i.ROOT_MES_ID == root_mesid);
			foreach (var one in list2) {
				one.OrderStartTime = 参考对象.OrderStartTime;
				one.OrderEndTime = 参考对象.OrderEndTime;
				one.UserConfigId = 参考对象.UserConfigId;

				var dbOne = myDataSource._YuZhiLingTemp.IQueryable().FirstOrDefault(i =>
					i.DeviceUnitCode == one.DeviceUnitCode &&
					i.MaterialCode == one.MaterialCode &&
					i.OrderStartTime == one.OrderStartTime &&
					i.OrderEndTime == one.OrderEndTime
				);

				if (dbOne == null) {
					myDataSource._YuZhiLingTemp.Insert(one);
				}
				else {
					one.ID = dbOne.ID;
					myDataSource._YuZhiLingTemp.Update(one);
				}
			}



			// 4. 返回要追加的指令的字符串
			string str = __主要控制指标_格式化为指令字符串(null);

			return str;
		}



		public string 主要控制指标_追加指令时_快速删除什么也不填的某一行(YuZhiLingTemp one) {
			var dbOne = myDataSource._YuZhiLingTemp.IQueryable().FirstOrDefault(i =>
				i.DeviceUnitCode == one.DeviceUnitCode &&
				i.MaterialCode == one.MaterialCode &&
				i.OrderStartTime == one.OrderStartTime &&
				i.OrderEndTime == one.OrderEndTime
			);
			if (dbOne != null) {
				Debug.WriteLine(dbOne.ToJson());
				var rep = new YuZhiLingTempRepository();
				int v = rep.Delete(dbOne);
				Debug.WriteLine("delete = " + v);
			}
			else {
				Debug.WriteLine("删除失败");
			}

			string str = __主要控制指标_格式化为指令字符串(null);

			return str;
		}




		public string 主要控制指标_新增指令的快速保存(List<YuZhiLingTemp> selectedList, string root_mesid) {
			ZhiLingInfo 参考对象 = null;
			if (false == string.IsNullOrWhiteSpace(root_mesid)) {
				参考对象 = myDataSource._ZhiLingMes.IQueryable().FirstOrDefault(i => i.ROOT_MES_ID == root_mesid);
			}

			foreach (var one in selectedList) {
				if (参考对象 != null) {
					one.OrderStartTime = 参考对象.OrderStartTime;
					one.OrderEndTime = 参考对象.OrderEndTime;
				}


				var rep = myDataSource._YuZhiLingTemp;

				var query =
					from a in myDataSource._YuZhiLingTemp.IQueryable()
					where a.Type == 0 &&
					a.DeviceUnitCode == one.DeviceUnitCode &&
					a.MaterialCode == one.MaterialCode
					select a;   // 临时表中不区分指令执行时间段
				if (query.Count() == 0) {
					myDataSource._YuZhiLingTemp.Insert(one);
				}
				else {
					one.ID = query.First().ID;
					rep.Update(one);
				}
			}

			var list1 = selectedList.Convert<List<ZhiLingInfo>>();
			string v = __主要控制指标_格式化为指令字符串(list1);
			return v;
		}


		public string 主要控制指标_不选择一个指令并返回最新的指令字符串(YuZhiLingTemp one) {
			one.IsSelected = false;

			// 1.
			var dbOne = myDataSource._YuZhiLingTemp.IQueryable().Where(i => i.ID == one.ID).FirstOrDefault();

			// 2.
			if (dbOne == null) {
				dbOne = myDataSource._YuZhiLingTemp.IQueryable().Where(i =>
					 i.Type == 0 &&
					 i.DeviceUnitCode == one.DeviceUnitCode &&
					 i.MaterialCode == one.MaterialCode
				).FirstOrDefault();
			}

			// 3.
			if (dbOne != null) {
				if (string.IsNullOrWhiteSpace(one.PlanValue) &&
					string.IsNullOrWhiteSpace(one.LowValue) &&
					string.IsNullOrWhiteSpace(one.UpValue)
					) {
					myDataSource._YuZhiLingTemp.Delete(dbOne);
				}
				else {
					one.ID = dbOne.ID;
					myDataSource._YuZhiLingTemp.Update(one);
				}
			}
			string v = __主要控制指标_格式化为指令字符串(null);
			return v;
		}


		public int 主要控制指标_点击取消新增按钮后_将临时表对应的行删除() {
			var list1 = myDataSource._YuZhiLingTemp.IQueryable().Where(i => i.Type == 0).ToList();
			foreach (var one in list1) {
				myDataSource._YuZhiLingTemp.Delete(one);
			}

			return 1;
		}




		public 指令运行状态VO 主要控制指标_获取特定的实际数据(string running_day) {
			var tiao_bao = new TianBiaoContentRepository().IQueryable().FirstOrDefault();
			var mesid = tiao_bao.MESID;

			if (string.IsNullOrWhiteSpace(mesid)) {
				return new 指令运行状态VO();
			}


			List<ZhiLingInfo> list0 = new List<ZhiLingInfo>();

			// 1. 默认走向: 获取最新MESid对应的实际数据
			if (string.IsNullOrWhiteSpace(running_day)) {
				var query1 = from a in myDataSource._ZhiLingMes.IQueryable().Where(i => i.Type == 0 && i.ROOT_MES_ID == mesid).ToList()
							 join b in myDataSource._YuZhiLing.IQueryable() on a.ID equals b.MesTableId into ab
							 from abResult in ab.DefaultIfEmpty()
							 join c in myDataSource._单位.IQueryable() on abResult.UnitId equals c.F_Id.ToString() into bc
							 from acResult in bc.DefaultIfEmpty()
							 orderby a.DeviceUnitCode
							 select new {
								 aaa = a,
								 bbb = abResult,
								 ccc = acResult
							 };
				list0 = query1.ToList().Select(item => {
					item.aaa.DianWeiType = "opc";
					item.aaa.PlanValue = item.bbb.PlanValue;
					item.aaa.ShangXiaXian = item.bbb.ShangXiaXian;
					item.aaa.LowValue = item.bbb.LowValue;
					item.aaa.UpValue = item.bbb.UpValue;

					if (item.ccc != null) {
						item.aaa.Unit = item.ccc.Unit;
						item.aaa.UnitId = item.ccc.F_Id.ToString();
					}

					return item.aaa;
				}).ToList();
			}


			//2. 用户选择了日期, 则是搜索
			else {
				var query2 = from a in myDataSource._ZhiLingMes.IQueryable().Where(i => i.Type == 0).ToList()
							 join b in myDataSource._YuZhiLing.IQueryable() on a.ID equals b.MesTableId into ab
							 from abResult in ab.DefaultIfEmpty()
							 join c in myDataSource._单位.IQueryable() on abResult.UnitId equals c.F_Id.ToString() into bc
							 from acResult in bc.DefaultIfEmpty()
							 where a.OrderStartTime >= DateTime.Parse(running_day)
							 orderby a.DeviceUnitCode
							 select new {
								 aaa = a,
								 bbb = abResult,
								 ccc = acResult
							 };
				list0 = query2.ToList().Select(item => {
					item.aaa.DianWeiType = "opc";
					item.aaa.PlanValue = item.bbb.PlanValue;
					item.aaa.ShangXiaXian = item.bbb.ShangXiaXian;
					item.aaa.LowValue = item.bbb.LowValue;
					item.aaa.UpValue = item.bbb.UpValue;

					if (item.ccc != null) {
						item.aaa.Unit = item.ccc.Unit;
						item.aaa.UnitId = item.ccc.F_Id.ToString();
					}

					return item.aaa;
				}).ToList();
			}



			指令运行状态VO one = new 指令运行状态VO(running_day, list0);
			one.ZhiLingTitle = list0.Count == 0 ? string.Empty : list0 [0].ROOT_MES_TITLE;
			one.RootMesId = list0.Count == 0 ? string.Empty : list0 [0].ROOT_MES_ID;


			// 获取指令状态
			var state_dto = 异步获取MES指令状态(one.RootMesId);
			//var state_dto = 异步获取MES指令状态("9a82f314-b3bd-4b76-91ae-810382a9d464"); // 测试用
			if (state_dto != null) {
				one.State = state_dto.State;
			}


			// 指令字符串
			one.主要控制指标.指令字符串 = __主要控制指标_格式化为指令字符串(one.主要控制指标.已下达的指令数据);


			// 当没有对应指令时,也呈现当前所属mesid指令的空,呈现追加按钮
			if (list0.Count == 0) {
				one.RootMesId = mesid;
				one.ZhiLingTitle = tiao_bao.指令的标题;
			}


			return one;
		}





		public List<指令运行状态VO> 主要控制指标_获取特定的实际数据_并按MESID分组(string running_day) {
			var list0 = 主要控制指标_获取特定的实际数据(running_day).主要控制指标.已下达的指令数据;

			List<指令运行状态VO> result = new List<指令运行状态VO>();

			var groups = list0.OrderBy(i => i.ID).GroupBy(i => i.ROOT_MES_ID);
			foreach (var group in groups) {
				var list = group.ToList();
				指令运行状态VO one = new 指令运行状态VO(running_day, list);
				one.ZhiLingTitle = list.Count == 0 ? string.Empty : list [0].ROOT_MES_TITLE;
				one.RootMesId = list.Count == 0 ? string.Empty : list [0].ROOT_MES_ID;
				result.Add(one);
			}


			return result;
		}






		public int 主要控制指标_等候指令的刷新通知() {
			int count = myDataSource._YuZhiLingTemp.IQueryable().Count(i => i.Type == 0);
			return count == 0 ? 1 : 0;
		}






		private string __主要控制指标_格式化为指令字符串(List<ZhiLingInfo> 待下达指令) {
			if (待下达指令 == null) {
				var query1 = from a in myDataSource._YuZhiLingTemp.IQueryable()
							 where a.Type == 0 && a.IsSelected
							 orderby a.DeviceUnitCode
							 select a;
				待下达指令 = query1.ToList().Convert<List<ZhiLingInfo>>();
			}
			else {
			}


			return Function_主要控制指标_转换到字符串表示(待下达指令);
		}



		private string ___格式化为主要控制指标的指令字符串(YuZhiLingTemp one) {
			var str = string.Format("{0}单元 ", one.DeviceUnitCode);

			var PlanValue = one.PlanValue;
			var ShangXiaXian = one.ShangXiaXian;
			var UpValue = one.UpValue;
			var LowValue = one.LowValue;
			var Unit = one.Unit;

			//
			string name = one.OrderMesTitle;
			string value = string.Empty;
			if (string.IsNullOrWhiteSpace(PlanValue) == false) {
				if (string.IsNullOrWhiteSpace(ShangXiaXian) == false) {
					value = PlanValue + "±" + ShangXiaXian + Unit;
				}
				else {
					value = PlanValue + Unit;
				}
				str += " " + name + " " + value + "，";
				return str;
			}

			//
			bool 具有上限值和下限值 = string.IsNullOrWhiteSpace(UpValue) == false && string.IsNullOrWhiteSpace(LowValue) == false;
			if (具有上限值和下限值) {
				value = LowValue + " ~ " + UpValue + Unit;
				str += " " + name + " " + value + "，";
				return str;
			}

			//
			if (string.IsNullOrWhiteSpace(UpValue) == false) {
				value = "≤" + UpValue + Unit;
				str += " " + name + " " + value + "，";
				return str;
			}

			//
			if (string.IsNullOrWhiteSpace(LowValue) == false) {
				value = "≥" + LowValue + Unit;
				str += " " + name + " " + value + "，";
				return str;
			}


			return string.Empty;
		}


	}
}