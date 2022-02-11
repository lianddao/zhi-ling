using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
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
using PlanExecut.Web.Areas.ZhiLingManage.VO;

namespace PlanExecut.Web.Areas.ZhiLingManage.Controllers {


	/// <summary>
	/// 生产运行优化指令又称为"文本描述指令"
	/// </summary>
	public partial class ZhiLingController {


		public ViewResult 生产运行优化_嵌入页() {
			return View("生产运行优化_嵌入页");
		}

		public ViewResult 生产运行优化_指令状态弹出页() {
			return View("生产运行优化_指令状态弹出页");
		}


		[ActionAllowOrigin]
		public int 生产运行优化_设置文本描述指令的执行结果(ZhiLingInfo one) {
			return service.生产运行优化_设置文本描述指令的执行结果(one);
		}




		[ActionAllowOrigin]
		public int 生产运行优化_作为新指令发送到MES(string root_mesid, string newZhiLingText, string order_title) {
			return service.生产运行优化_作为新指令发送到MES(root_mesid, newZhiLingText, order_title);
		}





		[ActionAllowOrigin]
		[HttpPost]
		[ActionName("QuickSaveTextOrderTo_YuZhiLingTemp")]
		public int 生产运行优化_快速保存文本描述指令到临时表(string zhiLingText, string start_day, string end_day, int userConfigId) {
			return service.生产运行优化_快速保存文本描述指令到临时表(zhiLingText, start_day, end_day, userConfigId);
		}


		[ActionAllowOrigin]
		public int 生产运行优化_等候指令的刷新通知() {
			return service.生产运行优化_等候指令的刷新通知();
		}


		[ActionAllowOrigin]
		public int 生产运行优化_点击取消新增按钮后_将临时表对应的行删除() {
			return service.生产运行优化_点击取消新增按钮后_将临时表对应的行删除();
		}

	}
}