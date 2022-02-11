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
using PlanExecut.Repository.ZhiLingManage;
using PlanExecut.Web.Areas.PlanOrderManage.VO;
using PlanExecut.Web.Areas.ZhiLingManage.DTO;
using PlanExecut.Web.Areas.ZhiLingManage.Services;
using PlanExecut.Web.Areas.ZhiLingManage.VO;

namespace PlanExecut.Web.Areas.ZhiLingManage.Controllers {

	/// <summary>
	/// 装置调整指令
	/// </summary>
	public partial class ZhiLingController {

		public ViewResult 装置调整_嵌入页() {
			return View("装置调整_嵌入页");
		}

		public ViewResult 装置调整_新建指令弹出页() {
			return View("装置调整_新建指令弹出页");
		}

		public ViewResult 装置调整_指令状态弹出页() {
			return View("装置调整_指令状态弹出页");
		}

		public ViewResult 装置调整_修改补充指令弹出页() {
			return View("装置调整_修改补充指令弹出页");
		}









		public string 装置调整_追加指令的快速保存(List<YuZhiLingTemp> selectedList, string root_mesid) {
			string v = service.装置调整_追加指令的快速保存(selectedList, root_mesid);
			return v;
		}


		public string 装置调整_追加指令时_快速删除什么也不填的某一行(YuZhiLingTemp one) {
			string v = service.装置调整_追加指令时_快速删除什么也不填的某一行(one);
			return v;
		}






		[ActionAllowOrigin]
		public string 装置调整_快速保存指令到临时表并返回对应的指令字符串(List<YuZhiLingTemp> selectedList) {
			string v = service.装置调整_快速保存指令到临时表并返回对应的指令字符串(selectedList);
			return v;
		}

		[ActionAllowOrigin]
		public string 装置调整_不选择一个指令并返回最新的指令字符串(YuZhiLingTemp one) {
			string v = service.装置调整_不选择一个指令并返回最新的指令字符串(one);
			return v;
		}

		[ActionAllowOrigin]
		public int 装置调整_等候指令的刷新通知() {
			return service.装置调整_等候指令的刷新通知();
		}

		[ActionAllowOrigin]
		public int 装置调整_点击取消新增按钮后_将临时表对应的行删除() {
			return service.装置调整_点击取消新增按钮后_将临时表对应的行删除();
		}

	}
}