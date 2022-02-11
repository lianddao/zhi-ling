using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PlanExecut.Web.Areas.ZhiLingManage.DTO {
	public class 月图表DTO {
		public string Month;

		[JsonProperty("series")]
		public List<图表数据点DTO> 指令列表;
	}
}