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

namespace PlanExecut.Web.Areas.ZhiLingManage.Services {
	public partial class ZhiLingService {

		public bool 指令维护_新增自定义指令(UserZhiLing one) {
			if (string.IsNullOrWhiteSpace(one.Name)) {
				return false;
			}

			int count = myDataSource._UserZhiLing.IQueryable().Count(i => i.Name == one.Name);
			if (count > 0) {
				return false;
			}
			return myDataSource._UserZhiLing.Insert(one) == 1;
		}


		public 侧线与其点位号DTO 修改点位号等信息(侧线与其点位号DTO one) {
			M_materialCodeInTableEntity obj = one.Convert<M_materialCodeInTableEntity>();
			int row = myDataSource._侧线与其点位号.Update(obj);
			if (row == 1) {
				return one;
			}
			return null;
		}



		public bool 指令维护_删除一个指令(UserZhiLing one) {
			var dbOne = myDataSource._UserZhiLing.IQueryable().FirstOrDefault(i => i.ID == one.ID);
			dbOne.Hidden = 1;
			return myDataSource._UserZhiLing.Update(dbOne) == 1;
		}

	}
}