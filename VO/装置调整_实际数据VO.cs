using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PlanExecut.Domain.Entity.PlanOrderManageEntity;
using PlanExecut.Domain.Entity.ZhiLingManage;

namespace PlanExecut.Web.Areas.ZhiLingManage.VO {


	public class 装置调整_实际数据VO {

		public List<ZhiLingInfo> 已定义的预指令 { get; set; }

		public string 指令字符串 { get; set; }

		public string 指令字符串2 {
			get {
				var groups = 已定义的预指令.GroupBy(i => i.DeviceUnitCode).ToList();
				List<string> rows = new List<string>();

				foreach (var group in groups) {
					var list = group.ToList();

					var row = string.Format("（{0}）{1}单元 ", groups.IndexOf(group) + 1, group.Key);
					foreach (var one in list) {

						string item = string.Empty;
						if (string.IsNullOrWhiteSpace(one.PrevPlanValue)) {
							item += string.Format("{0}调整为 {2}{3}", one.OrderTitle, one.PrevPlanValue, one.PlanValue, one.Unit);
						}
						else {
							item += string.Format("{0}由 {1} 调整为 {2}{3}", one.OrderTitle, one.PrevPlanValue, one.PlanValue, one.Unit);
						}
						row += item + "，";
					}
					rows.Add(row.TrimEnd('，') + "。");
				}

				string result = string.Join("\n", rows);
				return result;
			}
			private set { }
		}

		public string MESID { get; set; }

		public string RunningDay { get; set; }

	}
}