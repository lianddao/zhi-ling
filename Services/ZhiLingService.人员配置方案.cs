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


		public List<string> 获取部门数据() {
			MesUserRepository repository = new MesUserRepository();
			var list1 = repository.IQueryable().ToList();

			var 部门数据 = list1.GroupBy(i => i.DepartName).Select(i => i.Key).ToList();

			return 部门数据;
		}


		public List<MesUser> 获取指定部门的用户(string 部门) {
			MesUserRepository repository = new MesUserRepository();

			// 人员数据
			List<MesUser> list1 = repository.IQueryable().OrderBy(i => i.DepartName).ToList();

			for (int i = 0; i < list1.Count; i++) {
				list1 [i].Index = i;
			}

			if (false == string.IsNullOrWhiteSpace(部门)) {
				list1 = list1.Where(i => i.DepartName == 部门).ToList();
			}


			return list1;
		}



		public List<PlanExecutor> 获取人员配置方案数据() {
			var query1 =
				from a in myDataSource._人员配置方案.IQueryable()
				orderby a.Sort
				select a;
			var list1 = query1.ToList();

			return list1;
		}


		public int 设置默认的人员配置方案(int id) {
			var rep = myDataSource._人员配置方案;

			// 旧对象设置为false
			var obj1 = rep.IQueryable().Where(i => i.IsDefault).FirstOrDefault();
			if (obj1 != null) {
				obj1.IsDefault = false;
				rep.Update(obj1);
			}

			// 新对象设置为true
			var obj2 = rep.IQueryable().Where(i => i.ID == id).FirstOrDefault();
			if (obj2 != null) {
				obj2.IsDefault = true;
				rep.Update(obj2);
			}

			return 1;
		}


		public int 设置人员配置方案的启用状态(int id, int enable) {
			var rep = myDataSource._人员配置方案;

			var obj1 = rep.IQueryable().FirstOrDefault(i => i.ID == id);
			if (obj1 != null) {
				obj1.Enable = enable;
				rep.Update(obj1);
			}

			return 1;
		}



		public int 新增或保存人员配置方案(PlanExecutor one) {
			if (one.ID == 0) {
				return myDataSource._人员配置方案.Insert(one);
			}
			else {
				return myDataSource._人员配置方案.Update(one);
			}
		}


		public PlanExecutor 获取指定的人员配置方案(int id) {
			var one = myDataSource._人员配置方案.IQueryable().FirstOrDefault(i => i.ID == id);

			List<MesUser> mesUserList = 获取指定部门的用户(null);
			return null;

		}

	}
}