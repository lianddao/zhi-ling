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
	/// 主要控制指标指令又称为"计划值指令"
	/// </summary>
	public partial class ZhiLingController {


		public ViewResult 主要控制指标_嵌入页() {
			return View("主要控制指标_嵌入页");
		}

		public ViewResult 主要控制指标_新建指令弹出页() {
			return View("主要控制指标_新建指令弹出页");
		}


		public ViewResult 主要控制指标_指令状态弹出页() {
			return View("主要控制指标_指令状态弹出页");
		}


		public ViewResult 主要控制指标_修改补充指令弹出页() {
			return View("主要控制指标_修改补充指令弹出页");
		}





		[ActionAllowOrigin]
		public string 主要控制指标_新增指令的快速保存(List<YuZhiLingTemp> selectedList, string root_mesid) {
			string v = service.主要控制指标_新增指令的快速保存(selectedList, root_mesid);
			return v;
		}



		[ActionAllowOrigin]
		public string 主要控制指标_追加指令的快速保存(List<YuZhiLingTemp> selectedList, string root_mesid) {
			string v = service.主要控制指标_追加指令的快速保存(selectedList, root_mesid);
			return v;
		}



		[ActionAllowOrigin]
		public string 主要控制指标_追加指令时_快速删除什么也不填的某一行(YuZhiLingTemp one) {
			string v = service.主要控制指标_追加指令时_快速删除什么也不填的某一行(one);
			return v;
		}



		[ActionAllowOrigin]
		public int 主要控制指标_等候指令的刷新通知() {
			return service.主要控制指标_等候指令的刷新通知();
		}



		[ActionAllowOrigin]
		public string 主要控制指标_不选择一个指令并返回最新的指令字符串(YuZhiLingTemp one) {
			string v = service.主要控制指标_不选择一个指令并返回最新的指令字符串(one);
			return v;
		}



		[ActionAllowOrigin]
		public int 主要控制指标_点击取消新增按钮后_将临时表对应的行删除() {
			return service.主要控制指标_点击取消新增按钮后_将临时表对应的行删除();
		}

	}
}