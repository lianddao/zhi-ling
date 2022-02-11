using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PlanExecut.Domain.Entity.PlanOrderManageEntity;
using PlanExecut.Domain.Entity.ZhiLingManage;

namespace PlanExecut.Web.Areas.ZhiLingManage.DTO {
	public class 下指令For帆软报表DTO {

		public List<YuZhiLingTemp> 已定义的预指令 { get; set; }

		public string 指令字符串 { get; set; }


		public string 开始日期 { get; set; }

		public string 结束日期 { get; set; }


		public string 指令标题 { get; set; }
	}
}