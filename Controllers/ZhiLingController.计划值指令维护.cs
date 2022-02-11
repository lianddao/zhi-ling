using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using PlanExecut.Code;
using PlanExecut.Data;
using PlanExecut.Data.Extensions;
using PlanExecut.Domain.Entity.DataManageEntity;
using PlanExecut.Domain.Entity.PlanOrderManageEntity;
using PlanExecut.Domain.Entity.PlanOrderManageEntity.DTO;
using PlanExecut.Repository.PlanOrderManage;
using PlanExecut.Web.Areas.PlanOrderManage.VO;
using PlanExecut.Web.Areas.ZhiLingManage.DTO;
using PlanExecut.Web.Areas.ZhiLingManage.Services;

namespace PlanExecut.Web.Areas.ZhiLingManage.Controllers {


	public partial class ZhiLingController : Controller {

		private ZhiLingService service = new ZhiLingService();




		[HttpGet]
		[ActionName("WeiHuDianWei")]
		public ViewResult 指令维护(string DanYuan, string OrderTypeId) {
			return View("计划值指令维护.右侧点位号内容");
		}






		[HttpPost]
		public bool 新增自定义指令(用户自定义的指令DTO one) {
			return service.新增自定义指令(one);
		}

		[HttpPost]
		public bool 修改自定义指令的名称(用户自定义的指令DTO one) {
			return service.修改自定义指令的名称(one);
		}

		[HttpPost]
		public JsonResult 修改点位号等信息(侧线与其点位号DTO one) {
			var a = service.修改点位号等信息(one);
			return Json(a);
		}

		[HttpPost]
		public bool 删除一个指令(用户自定义的指令DTO one) {
			return service.删除一个指令(one);
		}

	}
}