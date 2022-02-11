using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PlanExecut.Domain.Entity.ZhiLingManage;

namespace PlanExecut.Web.Areas.ZhiLingManage.DTO {

	public class 重复指令检测结果DTO {

		public bool 存在重复项 {
			get {
				return YuZhiLingTemp_List.Count > 0 || YuZhiLing_List.Count > 0;
			}
		}

		public List<YuZhiLingTemp> YuZhiLingTemp_List { get; set; }

		public List<YuZhiLing> YuZhiLing_List { get; set; }
	}
}