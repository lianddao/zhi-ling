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


	/// <summary>
	/// 文本描述指令状态
	/// </summary>
	public partial class ZhiLingController : Controller {


		#region 页面定义
		[HttpGet]
		[ActionName("TextOrderState_Page")]
		public ViewResult 文本描述指令状态() {
			return View("文本描述指令状态");
		}

		[HttpGet]
		[ActionName("SubmitTextPage")]
		public ViewResult 下文本描述指令For帆软报表() {
			return View("下文本描述指令For帆软报表");
		}
		#endregion



		


	}
}