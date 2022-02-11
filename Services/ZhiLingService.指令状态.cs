using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using PlanExecut.Code;
using PlanExecut.Data.Extensions;
using PlanExecut.Domain.Entity.ZhiLingManage;
using PlanExecut.Web.Areas.PlanOrderManage.Controllers;
using PlanExecut.Web.Areas.PlanOrderManage.VO;
using PlanExecut.Web.Areas.ZhiLingManage.DTO;
using PlanExecut.Web.Areas.ZhiLingManage.VO;

namespace PlanExecut.Web.Areas.ZhiLingManage.Services {
	public partial class ZhiLingService {


		public List<指令运行状态VO> 指令运行状态(string running_day) {
			var 数据1 = 装置调整_获取特定的实际数据(running_day);
			var 数据2 = 主要控制指标_获取特定的实际数据(running_day);
			var 数据3 = 生产运行优化_获取特定的实际数据(running_day);

			List<ZhiLingInfo> all = new List<ZhiLingInfo>();
			all.AddRange(数据1.装置调整.已下达的指令数据);
			all.AddRange(数据2.主要控制指标.已下达的指令数据);
			all.AddRange(数据3.生产运行优化.已下达的指令数据);


			List<指令运行状态VO> result = new List<指令运行状态VO>();
			var groups = all.GroupBy(i => i.ROOT_MES_ID).ToList();
			foreach (var group in groups) {
				var list = group.ToList();
				指令运行状态VO one = new 指令运行状态VO(running_day, list);
				one.ZhiLingTitle = list.Count == 0 ? string.Empty : list [0].ROOT_MES_TITLE;
				one.RootMesId = list.Count == 0 ? string.Empty : list [0].ROOT_MES_ID;
				one.StartDay = list.Count == 0 ? null : list [0].StartDay;
				one.EndDay = list.Count == 0 ? null : list [0].EndDay;
				result.Add(one);
			}


			return result;
		}








		public ZhiLingInfo 异步获取Lims实际数据(ZhiLingInfo one) {

			// 1. 获取新数据(新增或修改)
			获取Lims实际数据(one);


			// 2. 所需的值
			var query2 = myDataSource._ZhiLingRealInfo.IQueryable().Where(i => i.ZhiLingId == one.ID).ToList().Select(j => {
				return new {
					aaa = j.RealTime.ConvertToJavascriptUTC(),
					bbb = j.RealValue
				};
			});


			// 3. 设置迷你值
			var realtime = query2.Select(i => i.aaa).ToList();
			var realvalue = query2.Select(i => i.bbb).ToList();
			one.MiniTime = realtime;
			one.MiniValue = realvalue;


			return one;
		}

		public ZhiLingInfo 异步获取Phd实际数据(ZhiLingInfo one) {

			// 1. 获取新数据(新增或修改)
			获取Phd或opc实际数据(one);


			// 2. 所需的值
			var query2 = myDataSource._ZhiLingRealInfo.IQueryable().Where(i => i.ZhiLingId == one.ID).ToList().Select(j => {
				return new {
					aaa = j.RealTime.ConvertToJavascriptUTC(),
					bbb = j.RealValue
				};
			});


			// 3. 设置迷你值
			var realtime = query2.Select(i => i.aaa).ToList();
			var realvalue = query2.Select(i => i.bbb).ToList();
			one.MiniTime = realtime;
			one.MiniValue = realvalue;


			return one;
		}




		public MES系统响应的DTO 异步获取MES指令状态(string mesid) {
			MES系统响应的DTO dto;


			string connectionString = ConfigurationManager.ConnectionStrings ["MES_ORDER_STATE"].ConnectionString;
			string sql = string.Format("SELECT TaskId, TaskName, CloseContent, TaskStatus FROM WF_Task where TaskId='{0}'", mesid);

			DataTable dt1 = new DataTable();

			try {
				DbHelper.gExecuteDT(sql, connectionString, dt1);
			} catch (Exception) {
				return null;
			}

			var list = dt1.ToList<MES系统响应的DTO>();
			if (list.Count == 1) {
				dto = list [0];
				return dto;
			}


			//var url = "http://10.152.70.4:8040/kpi/getTaskById?taskId=" + mesid;
			//var request = WebRequest.Create(url);
			//request.Method = "POST";

			//byte [] byteArray = Encoding.UTF8.GetBytes(string.Empty);

			//Stream dataStream = request.GetRequestStream();
			//dataStream.Write(byteArray, 0, byteArray.Length);
			//dataStream.Close();


			//WebResponse response = request.GetResponse();


			//string 响应结果 = string.Empty;

			//using (dataStream = response.GetResponseStream()) {
			//	StreamReader reader = new StreamReader(dataStream);
			//	string responseFromServer = reader.ReadToEnd();
			//	响应结果 = responseFromServer;
			//}
			//response.Close();


			//var list = 响应结果.ToList<MES系统响应的DTO>();
			//if (list.Count == 1) {
			//	dto = list [0];

			//	return dto;
			//}

			return null;
		}


		public int 设置MES指令的状态(string mesid, MES系统响应的DTO dto) {
			var query1 =
				from a in myDataSource._ZhiLingMes.IQueryable()
				where a.ROOT_MES_ID == mesid
				select a;
			var list1 = query1.ToList();
			foreach (var one in list1) {
				one.OrderState = dto.State.ToString();
				one.MesCloseContent = dto.CloseContent;
			}


			return 1;
		}





		#region 从远程获取实际数据
		private void 获取Lims实际数据(ZhiLingInfo zhiLing) {
			OrderHistoryController orderHistoryController = new OrderHistoryController();


			//string start = "2021-05-01 00:00:00";
			//string end = "2021-07-30 23:23:59";
			//List<MesOrderRealValueInfo> realDataList = orderHistoryController.LIMS__从WebService获取Lims数据(
			//	DateTime.Parse(start),
			//	DateTime.Parse(end),
			//	"十六烷值",
			//	"106-SN-21002");

			// 1. 从远程获取实际值
			string start = zhiLing.OrderStartTime.ToDateString() + " 00:00:00";
			string end = zhiLing.OrderEndTime.ToDateString() + " 00:00:00";
			List<MesOrderRealValueInfo> realDataList = orderHistoryController.LIMS__从WebService获取Lims数据(
				DateTime.Parse(start),
				DateTime.Parse(end),
				zhiLing.DianWeiName,
				zhiLing.DianWeiHao);


			if (realDataList == null || realDataList.Count == 0) {
				return;
			}


			// 2. 保存"最后的值"到MES表
			var last = realDataList.OrderByDescending(i => i.RealTime).First();
			zhiLing.RealValue = last.RealValue;
			myDataSource._ZhiLingMes.Update(zhiLing);



			// 3. 保存"最后的值"到预指令表
			var otherOne = myDataSource._YuZhiLing.IQueryable().Where(i => i.MesTableId == zhiLing.ID).FirstOrDefault();
			if (otherOne != null) {
				myDataSource._YuZhiLing.Update(otherOne);
			}
			if (otherOne == null) {
				return;
			}



			// 4. 过滤,取当天最后时间点的那个对象保存到 [ZhiLingRealInfo]
			var filterRealDataList = realDataList.OrderByDescending(i => i.RealTime).GroupBy(i => i.RealDay).Select(i => i.First()).OrderBy(i => i.RealDay).ToList();



			// 5. 将 filterRealDataList 保存实际值到 [ZhiLingRealInfo]
			List<ZhiLingRealInfo> 实际数据 = new List<ZhiLingRealInfo>();
			foreach (var realOne in filterRealDataList) {
				ZhiLingRealInfo info = new ZhiLingRealInfo();
				info.YuZhiLingId = otherOne.ID;
				info.ZhiLingId = zhiLing.ID;
				info.RealTime = realOne.RealTime;
				info.RealValue = realOne.RealValue;
				实际数据.Add(info);
			}
			// 对应的计划值，上下限制等
			var query = from a in 实际数据
						join b in myDataSource._YuZhiLing.IQueryable()
						on new { orderid = a.YuZhiLingId, mesorderid = a.ZhiLingId } equals new { orderid = b.ID, mesorderid = b.MesTableId.Value } into ab
						from abResult in ab.DefaultIfEmpty()

						join c in myDataSource._ZhiLingRealInfo.IQueryable()
						on new { a.ZhiLingId, a.RealTime } equals new { c.ZhiLingId, c.RealTime } into ac
						from acResult in ac.DefaultIfEmpty()

						select new {
							aaa = a,
							bbb = abResult,
							ccc = acResult
						};
			var list = query.Select(item => {
				item.aaa.PrevPlanValue = item.bbb.PrevPlanValue;
				item.aaa.PlanValue = item.bbb.PlanValue;
				item.aaa.ShangXiaXian = item.bbb.ShangXiaXian;
				item.aaa.UpValue = item.bbb.UpValue;
				item.aaa.LowValue = item.bbb.LowValue;
				item.aaa.UnitId = item.bbb.UnitId;
				item.aaa.DianWeiType = item.bbb.DianWeiType;

				if (item.ccc != null) {
					item.aaa.ID = item.ccc.ID;
				}

				return item.aaa;
			}).ToList();


			foreach (var one in 实际数据) {
				myDataSource._ZhiLingRealInfo.InsertOrUpdate(one);
			}
		}


		/// <summary>
		/// 从远程获取装置调整的数据
		/// </summary>
		/// <param name="zhiLing"></param>
		private void 获取Phd或opc实际数据(ZhiLingInfo zhiLing) {
			OrderHistoryController orderHistoryController = new OrderHistoryController();

			//DateTime st = DateTime.Parse("2021-07-29 08:00:00");
			//DateTime et = DateTime.Parse("2021-07-30 08:00:00");
			//List<MesOrderRealValueInfo> realDataList = orderHistoryController.PHD__从WebService获取PHD数据(st, et, "DCS.101F_3_FT01602B.PV", "900");


			// 1. 从远程获取实际值
			DateTime st = DateTime.Parse(zhiLing.OrderStartTime.ToDateString() + " 08:00:00");
			DateTime et = DateTime.Parse(zhiLing.OrderEndTime.ToDateString() + " 08:00:00");
			if (zhiLing.OrderEndTime > DateTime.Now) {
				et = DateTime.Now;
			}
			// ★★★ 900 ÷ 60 = 15,复盘小结里默认是900,即每15分钟获取一次的数据
			// 60 × 60 = 3600 每一小时的数据
			// 3600 × 24 = 86400 每一天的数据
			List<MesOrderRealValueInfo> realDataList = orderHistoryController.PHD__从WebService获取PHD数据(st, et, zhiLing.DianWeiHao, "86400"); // 按天取的数据,返回的实际值是 [当天最低的那个值,时间固定为08:00点]


			if (realDataList == null) {
				return;
			}


			// 2. 保存"最后的值"到MES表
			var last = realDataList.OrderByDescending(i => i.RealTime).First();
			zhiLing.RealValue = last.RealValue;
			myDataSource._ZhiLingMes.Update(zhiLing);


			// 3. 保存"最后的值"到预指令表
			var otherOne = myDataSource._YuZhiLing.IQueryable().Where(i => i.MesTableId == zhiLing.ID).FirstOrDefault();
			if (otherOne != null) {
				myDataSource._YuZhiLing.Update(otherOne);
			}
			if (otherOne == null) {
				return;
			}



			// 4. 保存实际值到 [ZhiLingRealInfo]
			List<ZhiLingRealInfo> 实际数据 = new List<ZhiLingRealInfo>();
			foreach (var realOne in realDataList) {
				ZhiLingRealInfo info = new ZhiLingRealInfo();
				info.YuZhiLingId = otherOne.ID;
				info.ZhiLingId = zhiLing.ID;
				info.RealTime = realOne.RealTime;
				info.RealValue = realOne.RealValue;
				实际数据.Add(info);
			}
			// 对应的计划值，上下限制等
			var query = from a in 实际数据
						join b in myDataSource._YuZhiLing.IQueryable()
						on new { orderid = a.YuZhiLingId, mesorderid = a.ZhiLingId } equals new { orderid = b.ID, mesorderid = b.MesTableId.Value } into ab
						from abResult in ab.DefaultIfEmpty()

						join c in myDataSource._ZhiLingRealInfo.IQueryable()
						on new { a.ZhiLingId, a.RealTime } equals new { c.ZhiLingId, c.RealTime } into ac
						from acResult in ac.DefaultIfEmpty()

						select new {
							aaa = a,
							bbb = abResult,
							ccc = acResult
						};
			var list = query.Select(item => {
				item.aaa.PrevPlanValue = item.bbb.PrevPlanValue;
				item.aaa.PlanValue = item.bbb.PlanValue;
				item.aaa.ShangXiaXian = item.bbb.ShangXiaXian;
				item.aaa.UpValue = item.bbb.UpValue;
				item.aaa.LowValue = item.bbb.LowValue;
				item.aaa.UnitId = item.bbb.UnitId;
				item.aaa.DianWeiType = item.bbb.DianWeiType;

				if (item.ccc != null) {
					item.aaa.ID = item.ccc.ID;
				}

				return item.aaa;
			}).ToList();


			foreach (var one in 实际数据) {
				myDataSource._ZhiLingRealInfo.InsertOrUpdate(one);
			}
		}



		/// <summary>
		/// 获取指定指令在某一天的每一小时一次的数据
		/// </summary>
		public 图表数据点DTO 获取某指令在指定日期的实际数据(int zhiLingId, string day) {
			var zhiLing = myDataSource._ZhiLingMes.IQueryable().First(i => i.ID == zhiLingId);

			// 1. 从远程获取实际值
			OrderHistoryController orderHistoryController = new OrderHistoryController();
			DateTime st = DateTime.Parse(day + " 00:00:00");
			DateTime et = st.AddDays(1);
			List<MesOrderRealValueInfo> realDataList = null;
			if (zhiLing.DianWeiType.ToLower() == "lims") {
				// lims
				st = st.AddSeconds(1);
				et = et.AddSeconds(-1);
				realDataList = orderHistoryController.LIMS__从WebService获取Lims数据(st, et, zhiLing.DianWeiName, zhiLing.DianWeiHao);
			}
			else {
				// opc或phd
				// ★★★ 900 ÷ 60 = 15,复盘小结里默认是900,即每15分钟获取一次的数据
				// 60 × 60 = 3600 每一小时的数据
				// 3600 × 24 = 86400 每一天的数据
				realDataList = orderHistoryController.PHD__从WebService获取PHD数据(st, et, zhiLing.DianWeiHao, "3600");  // 每小时一次的数据
			}

			//
			if (realDataList == null || realDataList.Count == 0) {
				return null;
			}

			// 提取图表用数据
			图表数据点DTO dto = new 图表数据点DTO();
			dto.指令名称 = zhiLing.OrderTitle;
			dto.实际数据 = from a in realDataList
					   select new object [] { a.RealTime.ConvertToJavascriptUTC(), a.RealValue.ToDouble() };


			return dto;
		}



		#endregion








		public 图表DTO 获取图表格式的实际数据(int mesOrderId) {
			var query = from a in myDataSource._ZhiLingRealInfo.IQueryable().ToList()
						join b in myDataSource._单位.IQueryable() on a.UnitId equals b.F_Id.ToString() into ab
						from abResult in ab.DefaultIfEmpty()

						join c in myDataSource._ZhiLingMes.IQueryable() on a.ZhiLingId equals c.ID into ac
						from acResult in ac.DefaultIfEmpty()

						where a.ZhiLingId == mesOrderId

						select new {
							aaa = a,
							bbb = abResult,
							ccc = acResult
						};
			var list = query.Select(item => {
				item.aaa.Unit = item.bbb == null ? null : item.bbb.Unit;
				item.aaa.OrderTitle = item.ccc == null ? null : item.ccc.OrderTitle;
				return item.aaa;
			});


			if (list.Count() == 0) {
				return null;
			}


			// ★如果没有设定号的上下限值,则按照实际数据,计算一个接近的范围
			IEnumerable<object> ranges = from a in list
										 select new object [] {
											 a.RealTime.ConvertToJavascriptUTC(),
											 a.图表用下限值 == null ? null: a.图表用下限值,
											 a.图表用上限值 == null ? null : a.图表用上限值 };
			IEnumerable<object> realValues = from a in list
											 select new object [] { a.RealTime.ConvertToJavascriptUTC(), a.RealValue.ToDouble() };
			IEnumerable<object> planValues = from a in list
											 select new object [] { a.RealTime.ConvertToJavascriptUTC(), a.图表用计划值 };




			var otherInfo = list.FirstOrDefault();
			var dto = new 图表DTO {
				范围 = ranges,
				实际值 = realValues,
				计划值 = planValues,
				title = otherInfo.OrderTitle,
				unit = otherInfo.Unit
			};

			return dto;
		}





	}
}