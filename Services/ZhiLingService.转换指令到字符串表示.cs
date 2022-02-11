using PlanExecut.Repository.DataManage;
using PlanExecut.Repository.PlanOrderManage;
using PlanExecut.Repository.ZhiLingManage;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using PlanExecut.Domain.Entity.ZhiLingManage;
using PlanExecut.Code;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using PlanExecut.Data.Extensions;
using System.Configuration;
using System.CodeDom.Compiler;
using PlanExecut.Web.Areas.ZhiLingManage.DTO;
using System.Security.Policy;
using PlanExecut.Domain.Entity.PlanOrderManageEntity.DTO;
using System.Data.Entity.Infrastructure;
using NPOI.SS.Formula.Functions;
using PlanExecut.Domain.Entity.DataManageEntity;
using PlanExecut.Web.Areas.ZhiLingManage.VO;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace PlanExecut.Web.Areas.ZhiLingManage.Services {

	public partial class ZhiLingService {


		public string Function_主要控制指标_转换到字符串表示(List<ZhiLingInfo> zhiLingInfoList) {
			if (zhiLingInfoList == null) {
				return string.Empty;
			}

			// 1. 关联单位数据
			var query1 = from a in zhiLingInfoList
						 join b in myDataSource._单位.IQueryable()
						 on a.UnitId == null ? string.Empty : a.UnitId equals b.F_Id.ToString() into ab
						 from abResult in ab.DefaultIfEmpty()
						 select new {
							 aaa = a,
							 bbb = abResult
						 };
			var list1 = query1.Select(item => {
				if (item.bbb != null) {
					item.aaa.Unit = item.bbb.Unit;
				}
				return item.aaa;
			});

			// 2.
			string str = OrderStringHelper.主要控制指标_转换到字符串表示(list1);
			return str;
		}


	}
}