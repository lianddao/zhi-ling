using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PlanExecut.Domain.Entity.ZhiLingManage;

namespace PlanExecut.Web.Areas.ZhiLingManage.DTO {
	
	/// <summary>
	/// 指令查询常用DTO
	/// </summary>
	public class LinqZhiLingDTO {
		public YuZhiLingTemp temp { get; set; }
		public YuZhiLing yuzhiling { get; set; }


	}
}