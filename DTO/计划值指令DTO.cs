using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanExecut.Web.Areas.ZhiLingManage.DTO {

	public class 计划值指令DTO<T> {

		public string text { get; set; }

		public List<T> datalist { get; set; }
	}
}