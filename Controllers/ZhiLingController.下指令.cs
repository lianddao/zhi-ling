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
using PlanExecut.Web.Areas.ZhiLingManage.DTO;
using PlanExecut.Web.Areas.ZhiLingManage.Services;

namespace PlanExecut.Web.Areas.ZhiLingManage.Controllers {

	public partial class ZhiLingController {


		#region 页面定义
		[HttpGet]
		public ViewResult 选择计划值指令弹出页For新增() {
			return View("选择计划值指令弹出页For新增");
		}

		public ViewResult 选择计划值指令弹出页For修改() {
			return View("选择计划值指令弹出页For修改");
		}





		[HttpGet]
		[ActionName("XiaZhiLing_ValueOrder")]
		public ViewResult 下指令For帆软报表2() {
			return View("下指令For帆软报表2");
		}


		[Obsolete]
		public ViewResult 新建计划值指令() {
			return View("新建计划值指令");
		}





		public ViewResult 新建文本描述指令() {
			return View("新建文本描述指令");
		}


		public ViewResult 修改文本描述指令() {
			return View("修改文本描述指令");
		}
		#endregion




		#region 异步接口

		[ActionAllowOrigin]
		[HttpPost]
		[ActionName("QuickUpdateTextOrder")]
		public int 快速修改文本描述指令到临时表(YuZhiLingTemp arg) {
			return service.快速修改文本描述指令到临时表(arg);
		}



		[ActionAllowOrigin]
		[HttpPost]
		[ActionName("QuickSaveTextOrderTo_YuZhiLingTemp_IsSelected")]
		public int 快速设置文本描述指令为已选择状态到临时表() {
			return service.快速设置文本描述指令为已选择状态到临时表();
		}





		[ActionAllowOrigin]
		public int 等候计划值指令的刷新通知() {
			return service.等候计划值指令的刷新通知();
		}






		#endregion





		[ActionAllowOrigin]
		public int 修改指令提交后操作_将临时表的指令作为新指令发送到MES(string root_mesid, string new_order_title) {
			return service.修改指令提交后操作_将临时表的指令作为新指令发送到MES(root_mesid, new_order_title);
		}



		[ActionAllowOrigin]
		[ActionName("XiaZhiLing")]
		public int 帆软报表提交时_将指令进行下达操作_新版(string zhiLingTitle, string startDay, string endDay, int userConfigId, List<YuZhiLingTemp> 被舍弃的指令) {
			return service.帆软报表提交时_将指令进行下达操作_新版(zhiLingTitle, startDay, endDay, userConfigId, 被舍弃的指令, null);
		}



		[ActionAllowOrigin]
		public JsonResult 修改临时指令的日期并判断是否已经存在(string startDay, string endDay) {
			重复指令检测结果DTO dto = service.修改临时指令的日期并判断是否已经存在(startDay, endDay);
			return Json(dto);
		}




		[ActionAllowOrigin]
		public string 帆软报表提交时_舍弃某个临时指令后_返回对应的指令字符串(List<YuZhiLingTemp> 被舍弃的指令) {
			return service.帆软报表提交时_舍弃某个临时指令后_返回对应的指令字符串(被舍弃的指令);
		}


	}
}