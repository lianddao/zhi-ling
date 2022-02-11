using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using NPOI.POIFS.NIO;
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
using PlanExecut.Web.Areas.ZhiLingManage.DTO;
using PlanExecut.Web.Areas.ZhiLingManage.Services;


namespace PlanExecut.Web.Areas.ZhiLingManage.Controllers {

	public partial class ZhiLingController {

		public ViewResult 指令在某日期的运行状态() {
			return View("指令在某日期的运行状态");
		}

		public ViewResult 指令月状态() {
			return View("指令月状态");
		}


		public ViewResult 指令运行状态() {
			return View("指令运行状态");
		}



		[HttpGet]
		[ActionName("GetChartData")]
		public JsonResult 获取图表格式的实际数据(int mesOrderId) {
			var chartData = service.获取图表格式的实际数据(mesOrderId);
			return Json(chartData, JsonRequestBehavior.AllowGet);
		}




		[ActionAllowOrigin]
		[HttpPost]
		[ActionName("GetRealData")]
		public JsonResult 异步获取实际数据(ZhiLingInfo arg) {
			string 点位号类型 = arg.DianWeiType.ToLower();
			if (点位号类型 == "lims") {
				var vo = service.异步获取Lims实际数据(arg);
			}
			else {
				service.异步获取Phd实际数据(arg);
			}
			return Json(arg);
		}



		[ActionAllowOrigin]
		[HttpPost]
		[ActionName("GetMesOrderState")]
		public JsonResult 异步获取MES指令状态(string mesid) {
			// 1. 从远程获取状态


			MES系统响应的DTO one = null;


			try {
				one = service.异步获取MES指令状态(mesid);
			} catch (Exception exception) {
				var response = new {
					state = "error",
					message = exception.Message
				};
				return Json(response);
			}


			if (one == null) {
				return null;
			}

			// 2. 更新到 [ZhiLing]
			service.设置MES指令的状态(mesid, one);

			return Json(one);
		}

	}
}