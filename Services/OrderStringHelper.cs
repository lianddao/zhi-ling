using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PlanExecut.Domain.Entity.ZhiLingManage;

namespace PlanExecut.Web.Areas.ZhiLingManage.Services {

	/// <summary>
	/// 指令格式化为字符串常用
	/// </summary>
	public static class OrderStringHelper {

		public static string 装置调整_转换到字符串表示_单行(ZhiLingInfo one) {
			var title = string.Format("{0}单元 ", one.DeviceUnitCode);

			if (string.IsNullOrWhiteSpace(one.PrevPlanValue)) {
				title += string.Format("{0}调整为 {1}{2}", one.OrderTitle, one.PlanValue, one.Unit);
			}
			else {
				title += string.Format("{0}由 {1}{2} 调整为 {3}{4}", one.OrderTitle, one.PrevPlanValue, one.Unit, one.PlanValue, one.Unit);
			}

			return title;
		}




		public static string 主要控制指标_转换到字符串表示(IEnumerable<ZhiLingInfo> zhiLingInfoList) {
			var groups = zhiLingInfoList.GroupBy(i => i.DeviceUnitCode).ToList();
			List<string> rows = new List<string>();

			foreach (var group in groups) {
				var list = group.ToList();

				var row = string.Format("（{0}）{1}单元 ", groups.IndexOf(group) + 1, group.Key);
				foreach (var one in list) {
					var PlanValue = one.PlanValue;
					var ShangXiaXian = one.ShangXiaXian;
					var UpValue = one.UpValue;
					var LowValue = one.LowValue;
					var Unit = one.Unit;

					//
					string name = one.OrderTitle;
					string value = string.Empty;
					if (string.IsNullOrWhiteSpace(PlanValue) == false) {
						if (string.IsNullOrWhiteSpace(ShangXiaXian) == false) {
							value = PlanValue + "±" + ShangXiaXian + Unit;
						}
						else {
							value = PlanValue + Unit;
						}
						row += " " + name + " " + value + "，";
						continue;
					}

					//
					bool 具有上限值和下限值 = string.IsNullOrWhiteSpace(UpValue) == false && string.IsNullOrWhiteSpace(LowValue) == false;
					if (具有上限值和下限值) {
						value = LowValue + " ~ " + UpValue + Unit;
						row += " " + name + " " + value + "，";
						continue;
					}

					//
					if (string.IsNullOrWhiteSpace(UpValue) == false) {
						value = "≤" + UpValue + Unit;
						row += " " + name + " " + value + "，";
						continue;
					}

					//
					if (string.IsNullOrWhiteSpace(LowValue) == false) {
						value = "≥" + LowValue + Unit;
						row += " " + name + " " + value + "，";
						continue;
					}

				}
				rows.Add(row.TrimEnd('，') + "。");
			}

			string result = string.Join("\n", rows);
			return result;
		}


		public static string 主要控制指标_转换到字符串表示_单行(ZhiLingInfo one) {
			var row = string.Format("{0}单元 ", one.DeviceUnitCode);

			var PlanValue = one.PlanValue;
			var ShangXiaXian = one.ShangXiaXian;
			var UpValue = one.UpValue;
			var LowValue = one.LowValue;
			var Unit = one.Unit;


			string name = one.OrderTitle;
			string value = string.Empty;
			if (string.IsNullOrWhiteSpace(PlanValue) == false) {
				if (string.IsNullOrWhiteSpace(ShangXiaXian) == false) {
					value = PlanValue + "±" + ShangXiaXian + Unit;
				}
				else {
					value = PlanValue + Unit;
				}
				row += name + " " + value + "，";
				return row;
			}

			//
			bool 具有上限值和下限值 = string.IsNullOrWhiteSpace(UpValue) == false && string.IsNullOrWhiteSpace(LowValue) == false;
			if (具有上限值和下限值) {
				value = LowValue + " ~ " + UpValue + Unit;
				row += name + " " + value + "，";
				return row;
			}

			//
			if (string.IsNullOrWhiteSpace(UpValue) == false) {
				value = "≤" + UpValue + Unit;
				row += name + " " + value + "，";
				return row;
			}

			//
			if (string.IsNullOrWhiteSpace(LowValue) == false) {
				value = "≥" + LowValue + Unit;
				row += name + " " + value + "，";
				return row;
			}

			return null;
		}




	}
}