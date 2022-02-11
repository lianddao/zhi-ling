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

		public ViewResult 人员配置方案维护() {
			return View("人员配置方案维护");
		}

		[HttpGet]
		[ActionName("SelectUserConfig_Div")]
		public ViewResult 选择人员配置方案弹出层() {
			return View("选择人员配置方案弹出层");
		}


		[HttpGet]
		[ActionName("UserConfigForTextOrder_Page")]
		public ViewResult 为文本描述指令选择人员配置方案() {
			return View("为文本描述指令选择人员配置方案");
		}


		public ViewResult 修改文本描述指令的日期和人员配置方案() {
			return View("修改文本描述指令的日期和人员配置方案");
		}



		[ActionName("SetDefaultUserConfig")]
		public int 设置默认的人员配置方案(int id) {
			return service.设置默认的人员配置方案(id);
		}


		public int 设置人员配置方案的启用状态(int id, int enable) {
			return service.设置人员配置方案的启用状态(id, enable);
		}


		public ViewResult 人员配置方案维护_新增或修改() {
			return View("人员配置方案维护.新增或修改");
		}

		public ViewResult 人员配置方案维护_人员数据(string arg) {
			return View("人员配置方案维护.人员数据");
		}



		public JsonResult 追加选择的用户(List<MesUser> mesUsers) {
			return Json(mesUsers);
		}


		public int 人员配置方案维护_保存(string 方案名称, string 执行人索引, string 阅知人索引) {
			var index1 = 执行人索引.Split(new char [] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
			var index2 = 阅知人索引.Split(new char [] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();

			// 1. 取出选中对应的对象
			List<MesUser> 所有人员数据 = service.获取指定部门的用户(null);
			List<MesUser> 执行人 = new List<MesUser>();
			List<MesUser> 阅知人 = new List<MesUser>();
			foreach (var one in index1) {
				var index = int.Parse(one);
				执行人.Add(所有人员数据 [index]);
			}
			foreach (var one in index2) {
				var index = int.Parse(one);
				阅知人.Add(所有人员数据 [index]);
			}

			// 2. 提取值, 并组合为一个新对象
			string executor = string.Join(",", 执行人.Select(i => i.DisplayName));
			string executorValue = string.Join(",", 执行人.Select(i => i.UserName));
			string knower = string.Join(",", 阅知人.Select(i => i.DisplayName));
			string knowerValue = string.Join(",", 阅知人.Select(i => i.UserName));

			var aaa = 执行人.GroupBy(i => i.DepartName).Select(j => j.Key).ToList();
			var bbb = 阅知人.GroupBy(i => i.DepartName).Select(j => j.Key).ToList();
			string edepartname = string.Join(",", aaa);
			string kdepartname = string.Join(",", bbb);

			PlanExecutor obj = new PlanExecutor();
			obj.RemarkName = 方案名称;
			obj.Executor = executor;
			obj.ExecutorValue = executorValue;
			obj.ExecutorDepartment = edepartname;
			obj.Knower = knower;
			obj.KnowerValue = knowerValue;
			obj.KnowerDepartment = kdepartname;
			obj.Enable = 1;
			obj.IsDefault = false;
			obj.CreatTime = DateTime.Now;
			return service.新增或保存人员配置方案(obj);
		}



		public PlanExecutor 获取指定的人员配置方案(int id) {
			return service.获取指定的人员配置方案(id);
		}

	}
}