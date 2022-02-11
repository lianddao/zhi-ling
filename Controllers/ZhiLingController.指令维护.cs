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
using PlanExecut.Domain.Entity.ZhiLingManage;
using PlanExecut.Repository.PlanOrderManage;
using PlanExecut.Web.Areas.PlanOrderManage.VO;
using PlanExecut.Web.Areas.ZhiLingManage.DTO;
using PlanExecut.Web.Areas.ZhiLingManage.Services;

namespace PlanExecut.Web.Areas.ZhiLingManage.Controllers {


	public partial class ZhiLingController : Controller {

		[HttpPost]
		public bool 指令维护_新增自定义_装置调整指令(UserZhiLing one) {
			one.Enable = 1;
			one.Type = 2;
			return service.指令维护_新增自定义指令(one);
		}


		[HttpPost]
		public bool 指令维护_新增自定义_主要控制指标指令(UserZhiLing one) {
			one.Enable = 1;
			one.Type = 0;
			return service.指令维护_新增自定义指令(one);
		}


		[HttpPost]
		public bool 指令维护_删除一个指令(UserZhiLing one) {
			return service.指令维护_删除一个指令(one);
		}




		[HttpPost]
		public JsonResult 修改点位号等信息(侧线与其点位号DTO one) {
			var a = service.修改点位号等信息(one);
			return Json(a);
		}








	}
}