using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PlanExecut.Domain.Entity.ZhiLingManage;

namespace PlanExecut.Web.Areas.ZhiLingManage.DTO {
	public class 下指令For帆软报表默认DTO {

		public string 计划值指令的字符串表示 { get; set; }

		public string 文本描述指令的字符串表示 { get; set; }


		public string 开始日期 { get; set; }

		public string 结束日期 { get; set; }

		public bool 是否为临时指令 { get; set; }

		public List<YuZhiLingTemp> 计划值指令 { get; set; }

		public List<YuZhiLingTemp> 文本描述指令 { get; set; }
	}
}