using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Services.Description;
using PlanExecut.Code;
using PlanExecut.Data;
using PlanExecut.Data.Extensions;
using PlanExecut.Domain.Entity.DataManageEntity;
using PlanExecut.Domain.Entity.PlanOrderManageEntity;
using PlanExecut.Domain.Entity.PlanOrderManageEntity.DTO;
using PlanExecut.Domain.Entity.ZhiLingManage;
using PlanExecut.Repository.PlanOrderManage;
using PlanExecut.Repository.ZhiLingManage;
using PlanExecut.Web.Areas.PlanOrderManage.VO;
using PlanExecut.Web.Areas.ZhiLingManage.DTO;
using Newtonsoft.Json;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Collections;

namespace PlanExecut.Web.Areas.ZhiLingManage.Services {

	public partial class ZhiLingService {

		public string 下指令前的指令预览文本值() {
			// 装置调整指令
			var query1 = from a in myDataSource._YuZhiLingTemp.IQueryable()
						 where a.Type == 2 && a.IsSelected
						 select a;
			string 装置调整指令的字符串表示 = __装置调整_格式化为指令字符串(null);


			// 主要控制指标指令(计划值指令)
			var query2 = from a in myDataSource._YuZhiLingTemp.IQueryable()
						 where a.Type == 0 && a.IsSelected
						 select a;
			string 主要控制指标指令的字符串表示 = __主要控制指标_格式化为指令字符串(null);


			// 生产运行优化指令(文本描述指令)
			var query3 = from a in myDataSource._YuZhiLingTemp.IQueryable()
						 where a.Type == 1
						 select a;
			string 生产运行优化指令的字符串表示 = __生产运行优化_格式化为指令字符串(null);


			string text = string.Empty;
			if (装置调整指令的字符串表示.Length == 0) {
				text += "装置进料指令:「无」\n\n\n";
			}
			else {
				text += "装置进料指令:\n" + 装置调整指令的字符串表示 + "\n\n\n";
			}

			if (主要控制指标指令的字符串表示.Length == 0) {
				text += "质量指标指令:「无」\n\n\n";
			}
			else {
				text += "质量指标指令:\n" + 主要控制指标指令的字符串表示 + "\n\n\n";
			}

			if (生产运行优化指令的字符串表示.Length == 0) {
				text += "装置调整指令:「无」";
			}
			else {
				text += "装置调整指令:\n" + 生产运行优化指令的字符串表示;
			}

			return text;
		}

		public string 下指令前的指令预览文本值(List<YuZhiLingTemp> 被舍弃的指令) {

			var ids = 被舍弃的指令.Select(i => i.ID).ToList();

			var query0 = myDataSource._YuZhiLingTemp.IQueryable().Where(i => ids.Contains(i.ID) == false);


			// 装置调整指令
			var query1 = from a in query0
						 where a.Type == 2 && a.IsSelected
						 select a;
			var list1 = query1.ToList().Convert<List<ZhiLingInfo>>();
			string 装置调整指令的字符串表示 = __装置调整_格式化为指令字符串(list1);


			// 主要控制指标指令(计划值指令)
			var query2 = from a in query0
						 where a.Type == 0 && a.IsSelected
						 select a;
			var list2 = query2.ToList().Convert<List<ZhiLingInfo>>();
			string 主要控制指标指令的字符串表示 = __主要控制指标_格式化为指令字符串(list2);


			// 生产运行优化指令(文本描述指令)
			var query3 = from a in query0
						 where a.Type == 1
						 select a;
			var list3 = query3.ToList().Convert<List<ZhiLingInfo>>();
			string 生产运行优化指令的字符串表示 = __生产运行优化_格式化为指令字符串(list3);


			string text = string.Empty;
			if (装置调整指令的字符串表示.Length == 0) {
				text += "装置进料指令:「无」\n\n\n";
			}
			else {
				text += "装置进料指令:\n" + 装置调整指令的字符串表示 + "\n\n\n";
			}

			if (主要控制指标指令的字符串表示.Length == 0) {
				text += "质量指标指令:「无」\n\n\n";
			}
			else {
				text += "质量指标指令:\n" + 主要控制指标指令的字符串表示 + "\n\n\n";
			}

			if (生产运行优化指令的字符串表示.Length == 0) {
				text += "装置调整指令:「无」";
			}
			else {
				text += "装置调整指令:\n" + 生产运行优化指令的字符串表示;
			}

			return text;
		}



		public List<YuZhiLingTemp> 下指令前判断_待发送的指令() {
			var list = myDataSource._YuZhiLingTemp.IQueryable().Where(i => i.IsSelected).ToList();
			return list;
		}





		public int 修改指令提交后操作_将临时表的指令作为新指令发送到MES(string root_mesid, string 追加指令的指令标题) {
			// 1. 获取参考对象,设置执行日期,人员配置方案
			var 参考对象 = myDataSource._ZhiLingMes.IQueryable().FirstOrDefault(i => i.ROOT_MES_ID == root_mesid);
			var startDay = 参考对象.OrderStartTime.ToDateString();
			var endDay = 参考对象.OrderEndTime.ToDateString();
			var userConfigId = 参考对象.UserConfigId;


			// 2.
			帆软报表提交时_将指令进行下达操作_新版(追加指令的指令标题, startDay, endDay, userConfigId, null, root_mesid);


			return 1;
		}





		/// <summary>
		/// 
		/// </summary>
		/// <returns>返回刚接收到的MESID</returns>
		public int 帆软报表提交时_将指令进行下达操作_新版(string 指令标题, string startDay, string endDay, int userConfigId, List<YuZhiLingTemp> 被舍弃的指令, string 基于哪个MESID追加) {
			// -1. 如果用户没有进行指令的填写,只是单纯的编辑帆软报表,则不需要进行「下指令」操作
			var 有效的临时指令 = myDataSource._YuZhiLingTemp.IQueryable().Where(i => i.IsSelected).ToList();
			if (有效的临时指令.Count == 0) {
				return -1;
			}


			// 0. 发生重叠指令时,删除用户选择的'被舍弃指令'
			if (被舍弃的指令 != null && 被舍弃的指令.Count > 0) {
				var out_ids = 被舍弃的指令.Select(i => i.ID).ToList();
				var list0 = myDataSource._YuZhiLingTemp.IQueryable().Where(i => out_ids.Contains(i.ID)).ToList();
				foreach (var one in list0) {
					myDataSource._YuZhiLingTemp.Delete(one);
				}
			}




			// 1. 更新最后一部需要重新赋值的参数: 执行日期, 用户配置id
			var query1 = from a in myDataSource._YuZhiLingTemp.IQueryable()
						 where a.IsSelected
						 select a;
			var list1 = query1.ToList();
			var rep_temp = new YuZhiLingTempRepository();
			foreach (var one in list1) {
				one.OrderStartTime = DateTime.Parse(startDay);
				one.OrderEndTime = DateTime.Parse(endDay);
				one.UserConfigId = userConfigId;
				rep_temp.Update(one);
			}





			// 2. 保存装置调整指令 到表 [YuZhiLing], [ZhiLing]
			List<ZhiLingInfo> 装置调整指令 = __帆软报表提交时_步骤1_保存装置调整指令(基于哪个MESID追加);


			// 3. 保存主要控制指标指令 到表 [YuZhiLing], [ZhiLing]
			List<ZhiLingInfo> 主要控制指标指令 = __帆软报表提交时_步骤2_保存主要控制指标指令(基于哪个MESID追加);


			// 4. 保存生产运行优化指令 到表 [ZhiLing]
			List<ZhiLingInfo> 生产运行优化指令 = __帆软报表提交时_步骤3_保存生产运行优化指令(基于哪个MESID追加);




			// 5. 发送指令到远程服务
			var 待发送的指令字符串 = 下指令前的指令预览文本值();
			string mes_id = 提交指令到远程服务2(指令标题, 待发送的指令字符串, startDay, endDay, userConfigId);




			// 6. 保存到[TianBaoContent]
			string 装置调整指令的字符串表示 = __装置调整_格式化为指令字符串(装置调整指令);
			string 主要控制指标指令的字符串表示 = __主要控制指标_格式化为指令字符串(主要控制指标指令);
			string 生产运行优化指令的字符串表示 = __生产运行优化_格式化为指令字符串(生产运行优化指令);
			var rep = new TianBiaoContentRepository();
			var first = rep.IQueryable().FirstOrDefault();
			first.LIMS指令 = 装置调整指令的字符串表示;
			first.OPC指令 = 主要控制指标指令的字符串表示;
			first.文本指令 = 生产运行优化指令的字符串表示;
			first.MESID = mes_id;
			first.指令的标题 = 指令标题;
			rep.Update(first);




			// 7. 将MESID, '指令标题'赋值到表 [ZhiLing]
			var re2 = new ZhiLingMesRepository();
			var re3 = new YuZhiLingRepository();
			List<ZhiLingInfo> all = new List<ZhiLingInfo>();
			all.AddRange(装置调整指令);
			all.AddRange(主要控制指标指令);
			all.AddRange(生产运行优化指令);
			foreach (var one in all) {
				one.ROOT_MES_ID = mes_id;
				one.ROOT_MES_TITLE = 指令标题;
				re2.Update(one);
			}




			// 8. 清空[YuZhiLingTemp]
			var tempRepository = new YuZhiLingTempRepository();
			tempRepository.ClearAll();






			return 1;
		}




		public 重复指令检测结果DTO 修改临时指令的日期并判断是否已经存在(string startDay, string endDay) {
			var tempList = myDataSource._YuZhiLingTemp.IQueryable().Where(i => i.IsSelected).ToList();
			List<YuZhiLingTemp> 临时表中的指令 = new List<YuZhiLingTemp>();
			List<YuZhiLing> 预指令表中的指令 = new List<YuZhiLing>();


			// 1. 传入新的日期时,将临时表中的对象赋新日期值
			if (string.IsNullOrWhiteSpace(startDay) && string.IsNullOrWhiteSpace(endDay)) {
			}
			else {
				var st = DateTime.Parse(startDay);
				var et = DateTime.Parse(endDay);
				foreach (var one in tempList) {
					one.OrderStartTime = st;
					one.OrderEndTime = et;
					myDataSource._YuZhiLingTemp.Update(one);
				}
			}


			// 2. 在[YuZhiLingTemp] 和 [YuZhiLing] 中判断并取出数值指令(主要控制指标指令,装置调整指令)的重叠数据行
			var query1 = from a in tempList
						 join b in myDataSource._YuZhiLing.IQueryable()
						 on
						 new { a.Type, a.DeviceUnitCode, a.MaterialCode, a.OrderStartTime, a.OrderEndTime } equals
						 new { b.Type, b.DeviceUnitCode, b.MaterialCode, b.OrderStartTime, b.OrderEndTime }
						 where a.Type != 1
						 select new {
							 aaa = a,
							 bbb = b
						 };
			var aaa = query1.Select(i => i.aaa).ToList();
			var bbb = query1.Select(item => {
				item.bbb.Unit = item.aaa.Unit;  // [YuZhiLing]中没有[Unit]的值. 这里进行提取单位,之后设置给bbb
				return item.bbb;
			}).ToList();
			// ★★★ 分组去重: 在进行多次的重叠指令追加后,需要进行去重
			var aaa_filterList = aaa
				.OrderByDescending(k => k.ID)
				.GroupBy(i => new { i.Type, i.DeviceUnitCode, i.MaterialCode, i.OrderStartTime, i.OrderEndTime })
				.Select(j => j.FirstOrDefault())
				.ToList();  // ★★★ 将aaa按id倒序,排在最前面的,即是最后叠加的那个指令
			var bbb_filterList = bbb
				.OrderByDescending(k => k.ID)
				.GroupBy(i => new { i.Type, i.DeviceUnitCode, i.MaterialCode, i.OrderStartTime, i.OrderEndTime })
				.Select(j => j.FirstOrDefault())
				.ToList();  // ★★★ 将bbb按id倒序,排在最前面的,即是最后叠加的那个指令



			// 3. 文本描述指令不需要判断重叠项,因为它们的唯一条件就是 内容+日期



			临时表中的指令.AddRange(aaa_filterList);
			预指令表中的指令.AddRange(bbb_filterList);



			// 设置直观值
			foreach (var one in 临时表中的指令) {
				if (one.Type == 2) {
					one.直观值 = ___格式化为装置调整的指令字符串(one);
				}
				if (one.Type == 0) {
					one.直观值 = ___格式化为主要控制指标的指令字符串(one);
				}
				if (one.Type == 1) {
					one.直观值 = one.OrderContent;
				}
			}

			foreach (var one in 预指令表中的指令) {
				if (one.Type == 2) {
					one.直观值 = ___格式化为装置调整的指令字符串(one.Convert<YuZhiLingTemp>());
				}
				if (one.Type == 0) {
					one.直观值 = ___格式化为主要控制指标的指令字符串(one.Convert<YuZhiLingTemp>());
				}
				if (one.Type == 1) {
					one.直观值 = one.OrderContent;
				}
			}



			重复指令检测结果DTO dto = new 重复指令检测结果DTO();
			dto.YuZhiLingTemp_List = 临时表中的指令;
			dto.YuZhiLing_List = 预指令表中的指令;

			return dto;
		}





		public string 帆软报表提交时_舍弃某个临时指令后_返回对应的指令字符串(List<YuZhiLingTemp> 被舍弃的指令) {
			var str = 下指令前的指令预览文本值(被舍弃的指令);
			return str;
		}





		private List<ZhiLingInfo> __帆软报表提交时_步骤1_保存装置调整指令(string 基于哪个MESID追加) {
			List<ZhiLingInfo> 待下达的指令 = new List<ZhiLingInfo>();

			var query1 = from a in myDataSource._YuZhiLingTemp.IQueryable()
						 where a.Type == 2 && a.IsSelected
						 select a;
			var list1 = query1.ToList().Convert<List<YuZhiLing>>();

			// ★★★ 新增 或 追加 都是执行 Insert 操作
			foreach (var one in list1) {
				// 保存到 [YuZhiLing]
				myDataSource._YuZhiLing.Insert(one);

				// 保存到 [ZhiLing]
				var otherOne = one.Convert<ZhiLingInfo>();
				myDataSource._ZhiLingMes.Insert(otherOne);

				// 关联
				one.MesTableId = otherOne.ID;
				if (false == string.IsNullOrWhiteSpace(基于哪个MESID追加)) {
					one.基于哪个MESID追加 = 基于哪个MESID追加;
				}
				myDataSource._YuZhiLing.Update(one);

				待下达的指令.Add(otherOne);
			}

			return 待下达的指令;
		}




		private List<ZhiLingInfo> __帆软报表提交时_步骤2_保存主要控制指标指令(string 基于哪个MESID追加) {
			List<ZhiLingInfo> 待下达的指令 = new List<ZhiLingInfo>();

			var query1 = from a in myDataSource._YuZhiLingTemp.IQueryable()
						 where a.Type == 0 && a.IsSelected
						 select a;
			var list1 = query1.ToList().Convert<List<YuZhiLing>>();

			// ★★★ 新增 或 追加 都是执行 Insert 操作
			foreach (var one in list1) {
				// 保存到 [YuZhiLing]
				myDataSource._YuZhiLing.Insert(one);

				// 保存到 [ZhiLing]
				var otherOne = one.Convert<ZhiLingInfo>();
				myDataSource._ZhiLingMes.Insert(otherOne);

				// 关联
				one.MesTableId = otherOne.ID;
				if (false == string.IsNullOrWhiteSpace(基于哪个MESID追加)) {
					one.基于哪个MESID追加 = 基于哪个MESID追加;
				}
				myDataSource._YuZhiLing.Update(one);

				待下达的指令.Add(otherOne);
			}


			return 待下达的指令;
		}


		private List<ZhiLingInfo> __帆软报表提交时_步骤3_保存生产运行优化指令(string 基于哪个MESID追加) {
			List<ZhiLingInfo> 待下达的指令 = new List<ZhiLingInfo>();

			var query1 = from a in myDataSource._YuZhiLingTemp.IQueryable()
						 where a.Type == 1 && a.IsSelected
						 select a;
			var list1 = query1.ToList().Convert<List<YuZhiLing>>();

			// ★★★ 新增 或 追加 都是执行 Insert 操作
			foreach (var one in list1) {
				// 保存到 [YuZhiLing]
				myDataSource._YuZhiLing.Insert(one);

				// 保存到 [ZhiLing]
				var otherOne = one.Convert<ZhiLingInfo>();
				myDataSource._ZhiLingMes.Insert(otherOne);

				// 关联
				one.MesTableId = otherOne.ID;
				if (false == string.IsNullOrWhiteSpace(基于哪个MESID追加)) {
					one.基于哪个MESID追加 = 基于哪个MESID追加;
				}
				myDataSource._YuZhiLing.Update(one);

				待下达的指令.Add(otherOne);
			}

			return 待下达的指令;
		}







		public int 快速修改文本描述指令到临时表(YuZhiLingTemp arg) {
			var one = myDataSource._YuZhiLingTemp.IQueryable().First(i => i.ID == arg.ID);
			one.OrderStartTime = arg.OrderStartTime;
			one.OrderEndTime = arg.OrderEndTime;
			one.UserConfigId = arg.UserConfigId;
			myDataSource._YuZhiLingTemp.Update(one);

			return 1;
		}



		public int 快速设置文本描述指令为已选择状态到临时表() {
			var list = myDataSource._YuZhiLingTemp.IQueryable().ToList();
			foreach (var one in list) {
				one.IsSelected = true;
				myDataSource._YuZhiLingTemp.Update(one);
			}

			return 1;
		}


		public int 等候计划值指令的刷新通知() {
			int count = myDataSource._YuZhiLingTemp.IQueryable().Count(i => i.Type == 0);

			return count == 0 ? 1 : 0;
		}



	}
}