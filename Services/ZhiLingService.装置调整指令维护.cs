using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Services.Description;
using PlanExecut.Code;
using PlanExecut.Data;
using PlanExecut.Data.Extensions;
using PlanExecut.Domain.Entity.DataManageEntity;
using PlanExecut.Domain.Entity.PlanOrderManageEntity;
using PlanExecut.Domain.Entity.PlanOrderManageEntity.DTO;
using PlanExecut.Domain.Entity.ZhiLingManage;
using PlanExecut.Repository.PlanOrderManage;
using PlanExecut.Repository.ZhiLingManage;
using PlanExecut.Web.Areas.PlanOrderManage.VO;
using PlanExecut.Web.Areas.ZhiLingManage.DTO;

namespace PlanExecut.Web.Areas.ZhiLingManage.Services {

	public partial class ZhiLingService {


		public List<UserZhiLing> 装置调整指令维护() {
			var query1 = from a in myDataSource._UserZhiLing.IQueryable()
						 where a.Type == 2 && ( a.Hidden == null ? true : a.Hidden != 1 )
						 select a;
			var list1 = query1.ToList();

			return list1;
		}


		public UserZhiLing 指定id的用户指令(string id) {
			var _id = int.Parse(id);
			var one = myDataSource._UserZhiLing.IQueryable().FirstOrDefault(i => i.ID == _id);
			return one;
		}


		public int 装置调整指令_指令详情_保存(UserZhiLing one) {
			return myDataSource._UserZhiLing.Update(one);
		}

	}
}