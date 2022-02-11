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
using PlanExecut.Web.Areas.PlanOrderManage.VO;
using PlanExecut.Web.Areas.ZhiLingManage.DTO;

namespace PlanExecut.Web.Areas.ZhiLingManage.Services {

	public partial class ZhiLingService {


		public IEnumerable<用户自定义的指令DTO> Service1_指令维护页面使用的_用户自定义指令() {
			var list =
				from a in myDataSource._用户自定义的指令.IQueryable().ToList()
				orderby a.DeviceCode
				select a.Convert<用户自定义的指令DTO>();

			return list;
		}

		public 用户自定义的指令DTO Service1_指令维护页面使用的_用户自定义指令(string deviceCode) {
			var list =
				from a in myDataSource._用户自定义的指令.IQueryable().ToList()
				where a.DeviceCode == deviceCode
				orderby a.DeviceCode
				select a.Convert<用户自定义的指令DTO>();

			return list.FirstOrDefault();
		}









		public bool 新增自定义指令(用户自定义的指令DTO one) {
			int count = myDataSource._UserZhiLing.IQueryable().Count(i => i.Name == one.ZhiLingName);
			if (count > 0) {
				return false;
			}
			UserZhiLing obj = one.Convert<UserZhiLing>();
			return myDataSource._UserZhiLing.Insert(obj) == 1;
		}

		public bool 修改自定义指令的名称(用户自定义的指令DTO one) {
			UserZhiLing obj = one.Convert<UserZhiLing>();
			return myDataSource._UserZhiLing.Update(obj) == 1;
		}

		public 侧线与其点位号DTO 修改点位号等信息(侧线与其点位号DTO one) {
			M_materialCodeInTableEntity obj = one.Convert<M_materialCodeInTableEntity>();
			int row = myDataSource._侧线与其点位号.Update(obj);
			if (row == 1) {
				return one;
			}
			return null;
		}

		public bool 删除一个指令(用户自定义的指令DTO one) {
			UserZhiLing obj = one.Convert<UserZhiLing>();
			return myDataSource._UserZhiLing.Delete(obj) == 1;
		}




		public IEnumerable<M_data_UnitEntity> DanWei_Service1_所有单位数据() {
			var list = myDataSource._单位.IQueryable().ToList();

			return list;
		}






	}
}