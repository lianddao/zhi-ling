using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanExecut.Web.Areas.ZhiLingManage.DTO {
	public class 图表DTO {
		public string title;

		public string unit;

		public IEnumerable<object> 范围;

		public IEnumerable<object> 实际值;

		public IEnumerable<object> 计划值;
	}
}