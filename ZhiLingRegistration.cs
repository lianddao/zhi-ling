using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlanExecut.Web.Areas.ZhiLing {
	public class ZhiLingRegistration : AreaRegistration {
		public override string AreaName {
			get {
				return "ZhiLingManage";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context) {
			context.MapRoute(
			  this.AreaName + "_Default",
			  this.AreaName + "/{controller}/{action}/{id}",
			  new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
			  new string [] { "PlanExecut.Web.Areas." + this.AreaName + ".Controllers" }
			);
		}
	}
}