using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PlanExecut.Domain.Entity.PlanOrderManageEntity;
using PlanExecut.Domain.Entity.ZhiLingManage;

namespace PlanExecut.Web.Areas.ZhiLingManage.DTO {

	public class 指令状态页面分页DTO {

		public string RunningDay { get; set; }

		public List<ZhiLingInfo> ZhiLingMesList { get; set; }

		public List<DateTime> Groups { get; set; }
	}
}