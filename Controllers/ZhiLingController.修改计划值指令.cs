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


namespace PlanExecut.Web.Areas.ZhiLingManage.Controllers {

	public partial class ZhiLingController {

		/// <summary>
		/// 
		/// </summary>
		/// <seealso cref="ZhiLingController.主要控制指标_嵌入页"/>
		/// <returns></returns>
		[Obsolete]
		public ViewResult 修改计划值指令() {
			return View("修改计划值指令");
		}
	}
}