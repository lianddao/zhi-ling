using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PlanExecut.Domain.Entity.ZhiLingManage;

namespace PlanExecut.Web.Areas.ZhiLingManage.DTO {
	public class 修改计划值指令返回DTO {
		public string ZhiLingText { get; set; }

		public List<YuZhiLingTemp> SelectedList { get; set; }
	}
}