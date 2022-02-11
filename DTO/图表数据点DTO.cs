using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PlanExecut.Web.Areas.ZhiLingManage.DTO {
	public class 图表数据点DTO {

		[JsonProperty("name")]
		public string 指令名称;

		[JsonProperty("data")]
		public IEnumerable<object> 实际数据;
	}
}