using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanExecut.Web.Areas.ZhiLingManage.DTO {
	public class 执行人_阅知人_创建人DTO {

		public string 执行人 { get; set; }

		public string 阅知人 { get; set; }


		/// <summary>
		/// 创建人
		/// </summary>
		public string CreateBy { get; set; }

		public string CreateByName { get; set; }
	}
}