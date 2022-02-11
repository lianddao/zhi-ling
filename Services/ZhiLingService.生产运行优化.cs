using PlanExecut.Repository.DataManage;
using PlanExecut.Repository.PlanOrderManage;
using PlanExecut.Repository.ZhiLingManage;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using PlanExecut.Domain.Entity.ZhiLingManage;
using PlanExecut.Code;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using PlanExecut.Data.Extensions;
using System.Configuration;
using System.CodeDom.Compiler;
using PlanExecut.Web.Areas.ZhiLingManage.DTO;
using System.Security.Policy;
using PlanExecut.Domain.Entity.PlanOrderManageEntity.DTO;
using System.Data.Entity.Infrastructure;
using NPOI.SS.Formula.Functions;
using PlanExecut.Domain.Entity.DataManageEntity;
using PlanExecut.Web.Areas.ZhiLingManage.VO;
using PlanExecut.Domain.Entity.PlanOrderManageEntity;

namespace PlanExecut.Web.Areas.ZhiLingManage.Services {

	public partial class ZhiLingService {


		public int 生产运行优化_设置文本描述指令的执行结果(ZhiLingInfo one) {
			var dbOne = myDataSource._ZhiLingMes.IQueryable().FirstOrDefault(i => i.ID == one.ID);
			dbOne.OrderContentRealValue = one.OrderContentRealValue == null ? string.Empty : one.OrderContentRealValue;
			myDataSource._ZhiLingMes.Update(dbOne);

			return 1;
		}





		public int 生产运行优化_作为新指令发送到MES(string root_mesid, string newZhiLingText, string 追加指令的指令标题) {
			List<YuZhiLing> 待发送的指令 = new List<YuZhiLing>();
			List<YuZhiLing> 追加的指令 = new List<YuZhiLing>();

			// 1. 按换行符'\n'拆分为多行
			var list = newZhiLingText.Split(new char [] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();

			// 2. 取一个兄弟对象作参照
			var 参考对象 = myDataSource._ZhiLingMes.IQueryable().Where(i => i.ROOT_MES_ID == root_mesid).FirstOrDefault();

			// 3. 把'新增的指令'存入 [YuZhiLingTemp]
			foreach (var str in list) {
				string v = str.Trim();
				if (v.Length == 0) {
					continue;
				}

				YuZhiLingTemp one = new YuZhiLingTemp();
				one.Type = 1;
				one.OrderContent = v;
				one.OrderStartTime = 参考对象.OrderStartTime;
				one.OrderEndTime = 参考对象.OrderEndTime;
				one.UserConfigId = 参考对象.UserConfigId;
				one.IsSelected = true;
				myDataSource._YuZhiLingTemp.Insert(one);

				追加的指令.Add(one.Convert<YuZhiLing>());
			}


			// 5. 提取为指令字符串
			待发送的指令.AddRange(追加的指令);
			string 待发送指令的字符串表示 = __生产运行优化_格式化为指令字符串(待发送的指令.Convert<List<ZhiLingInfo>>());


			// 6. 发送给MES
			var startDay = 参考对象.OrderStartTime.ToDateString();
			var endDay = 参考对象.OrderEndTime.ToDateString();
			var userConfigId = 参考对象.UserConfigId;
			帆软报表提交时_将指令进行下达操作_新版(追加指令的指令标题, startDay, endDay, userConfigId, null, root_mesid);


			return 1;
		}




		public 指令运行状态VO 生产运行优化_获取特定的实际数据(string running_day) {
			var tiao_bao = new TianBiaoContentRepository().IQueryable().FirstOrDefault();
			var mesid = tiao_bao.MESID;

			if (string.IsNullOrWhiteSpace(mesid)) {
				return new 指令运行状态VO();
			}


			List<ZhiLingInfo> list0 = new List<ZhiLingInfo>();


			// 1. 默认走向: 获取最新MESid对应的实际数据
			if (string.IsNullOrWhiteSpace(running_day)) {
				var query1 = from a in myDataSource._ZhiLingMes.IQueryable().Where(i => i.Type == 1 && i.ROOT_MES_ID == mesid).ToList()
							 join b in myDataSource._YuZhiLing.IQueryable() on a.ID equals b.MesTableId into ab
							 from abResult in ab.DefaultIfEmpty()
							 orderby a.DeviceUnitCode
							 select new {
								 aaa = a,
								 bbb = abResult
							 };
				list0 = query1.ToList().Select(item => {
					item.aaa.OrderContent = item.bbb.OrderContent;

					return item.aaa;
				}).ToList();
			}


			//2. 用户选择了日期, 则是搜索
			else {
				var query2 = from a in myDataSource._ZhiLingMes.IQueryable().Where(i => i.Type == 1).ToList()
							 join b in myDataSource._YuZhiLing.IQueryable() on a.ID equals b.MesTableId into ab
							 from abResult in ab.DefaultIfEmpty()
							 where a.OrderStartTime >= DateTime.Parse(running_day)
							 orderby a.DeviceUnitCode
							 select new {
								 aaa = a,
								 bbb = abResult
							 };
				list0 = query2.ToList().Select(item => {
					item.aaa.OrderContent = item.bbb.OrderContent;

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
			one.生产运行优化.指令字符串 = __生产运行优化_格式化为指令字符串(one.生产运行优化.已下达的指令数据);


			return one;
		}






		public List<指令运行状态VO> 生产运行优化_获取特定的实际数据_并按MESID分组(string running_day) {
			var list0 = 生产运行优化_获取特定的实际数据(running_day).生产运行优化.已下达的指令数据;

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






		public int 生产运行优化_等候指令的刷新通知() {
			int count = myDataSource._YuZhiLingTemp.IQueryable().Count(i => i.Type == 1);
			return count == 0 ? 1 : 0;
		}


		public int 生产运行优化_点击取消新增按钮后_将临时表对应的行删除() {
			var list1 = myDataSource._YuZhiLingTemp.IQueryable().Where(i => i.Type == 1).ToList();
			foreach (var one in list1) {
				myDataSource._YuZhiLingTemp.Delete(one);
			}

			return 1;
		}




		public int 生产运行优化_快速保存文本描述指令到临时表(string zhiLingText, string start_day, string end_day, int userConfigId) {
			// 1. 按换行符'\n'拆分为多行
			var list = zhiLingText.Split(new char [] { '\n' }).ToList();

			// 2. 清空文本描述指令
			myDataSource._YuZhiLingTemp.Clear文本描述指令();

			// 3. 设置执行时间
			DateTime startDay = DateTime.Parse(start_day);
			DateTime endDay = DateTime.Parse(end_day);

			// 4. 人员配置方案
			PlanExecutor fangAn;
			if (userConfigId > 0) {
				fangAn = myDataSource._人员配置方案.IQueryable().Where(i => i.ID == userConfigId).First();
			}
			else {
				fangAn = myDataSource._人员配置方案.IQueryable().Where(i => i.IsDefault).First();
			}

			// 5. 存入[YuZhiLingTemp]
			foreach (var str in list) {
				YuZhiLingTemp one = new YuZhiLingTemp();
				one.Type = 1;
				one.OrderContent = str;
				one.OrderStartTime = startDay;
				one.OrderEndTime = endDay;
				one.IsSelected = true;
				one.UserConfigId = fangAn.ID;
				myDataSource._YuZhiLingTemp.Insert(one);
			}

			return 1;
		}



		private string __生产运行优化_格式化为指令字符串(List<ZhiLingInfo> 待下达指令) {
			if (待下达指令 == null) {
				// 临时表采用
				var query1 = from a in myDataSource._YuZhiLingTemp.IQueryable()
							 where a.Type == 1 && a.IsSelected
							 orderby a.DeviceUnitCode
							 select a;
				待下达指令 = query1.ToList().Convert<List<ZhiLingInfo>>();
			}
			else {
			}


			string result = string.Join("\n", 待下达指令.Select(i => i.OrderContent));


			return result;
		}

	}
}