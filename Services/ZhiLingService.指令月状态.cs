using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Policy;
using PlanExecut.Code;
using PlanExecut.Data.Extensions;
using PlanExecut.Domain.Entity.ZhiLingManage;
using PlanExecut.Web.Areas.PlanOrderManage.Controllers;
using PlanExecut.Web.Areas.PlanOrderManage.VO;
using PlanExecut.Web.Areas.ZhiLingManage.DTO;
using PlanExecut.Web.Areas.ZhiLingManage.VO;

namespace PlanExecut.Web.Areas.ZhiLingManage.Services {
	public partial class ZhiLingService {


		public 月图表DTO 指令月状态(string month) {

			// 1. 从远程获取数据
			var query1 = from a in myDataSource._ZhiLingMes.IQueryable().AsEnumerable()
						 where a.Month == month && string.IsNullOrWhiteSpace(a.DianWeiType) == false
						 select a;
			var list1 = query1.ToList();
			foreach (var one in list1) {
				if (one.DianWeiType.ToLower() == "lims") {
					获取Lims实际数据(one);
				}
				else {
					获取Phd或opc实际数据(one);
				}
			}


			// 2. 从 [ZhiLingRealInfo] 获取数据
			var query2 = from a in myDataSource._ZhiLingRealInfo.IQueryable().AsEnumerable()
						 join b in myDataSource._YuZhiLing.IQueryable()
						 on a.YuZhiLingId equals b.ID into ab
						 from abResult in ab
						 where a.Month == month
						 select new {
							 aaa = a,
							 bbb = abResult
						 };
			var list2 = query2.Select(item => {
				item.aaa.OrderTitle = item.bbb.OrderTitle;

				return item.aaa;
			}).ToList();


			// 3. 组合成为图表对象
			var groups = list2.GroupBy(i => i.OrderTitle).ToList();
			List<图表数据点DTO> dtos = new List<图表数据点DTO>();
			foreach (var group in groups) {
				string title = group.Key;
				List<ZhiLingRealInfo> list = group.ToList();

				// 受到重叠指令的影响,则只提取重叠指令最后那个的值
				var filterList = list.OrderByDescending(i => i.ZhiLingId).GroupBy(i => i.RealTime).Select(i => i.First()).ToList();


				图表数据点DTO dto = new 图表数据点DTO();
				dto.指令名称 = title;
				dto.实际数据 = from a in filterList
						   select new object [] { a.RealTime.ConvertToJavascriptUTC(), a.RealValue.ToDouble(), a.Day, a.ZhiLingId };

				dtos.Add(dto);
			}


			月图表DTO aaa = new 月图表DTO();
			aaa.Month = month;
			aaa.指令列表 = dtos;

			return aaa;
		}


	}
}