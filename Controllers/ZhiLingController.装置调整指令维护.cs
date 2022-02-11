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


		public ViewResult 装置调整指令维护() {
			return View("装置调整指令维护");
		}


		public ViewResult 装置调整指令_指令详情(string zhiLingId) {
			return View("装置调整指令维护.指令详情");
		}


		public ViewResult 装置调整指令_指令详情_保存(UserZhiLing one) {
			one.PointNumName = string.IsNullOrWhiteSpace(one.PointNumName) ? string.Empty : one.PointNumName;
			one.PointNumOrValue = string.IsNullOrWhiteSpace(one.PointNumOrValue) ? string.Empty : one.PointNumOrValue;
			one.PlanValue = string.IsNullOrWhiteSpace(one.PlanValue) ? string.Empty : one.PlanValue;
			one.ShangXiaXian = string.IsNullOrWhiteSpace(one.ShangXiaXian) ? string.Empty : one.ShangXiaXian;
			one.LowValue = string.IsNullOrWhiteSpace(one.LowValue) ? string.Empty : one.LowValue;
			one.UpValue = string.IsNullOrWhiteSpace(one.UpValue) ? string.Empty : one.UpValue;
			one.Sort = one.Sort == null ? 0 : one.Sort;

			service.装置调整指令_指令详情_保存(one);
			return View("装置调整指令维护.指令详情");
		}
	}
}