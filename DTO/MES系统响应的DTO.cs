using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PlanExecut.Web.Areas.ZhiLingManage.DTO {

	public class MES系统响应的DTO {

		[JsonProperty("TaskId")]
		public string ID { get; set; }

		public string TaskName { get; set; }

		public string CloseContent { get; set; }

		[JsonProperty("TaskStatus")]
		public int State { get; set; }

		public string StateText {
			get {
				string v;
				switch (State) {
					case 1:
						v = "进行中";
						break;
					case 2:
						v = "已完成";
						break;
					case 3:
						v = "已完结";
						break;
					default:
						v = string.Empty;
						break;
				}
				return v;
			}
		}
	}
}