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
using PlanExecut.Repository.DataManage;
using PlanExecut.Repository.PlanOrderManage;
using PlanExecut.Web.Areas.PlanOrderManage.VO;
using PlanExecut.Web.Areas.ZhiLingManage.Services;

namespace PlanExecut.Web.Areas.ZhiLingManage.Controllers {

	public partial class ZhiLingController {


		public ViewResult 质量指标_新建指令嵌入页() {
			return View("质量指标_新建指令嵌入页");
		}

	}
}