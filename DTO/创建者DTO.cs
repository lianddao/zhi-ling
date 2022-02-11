using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanExecut.Web.Areas.ZhiLingManage.DTO {

	/// <summary>
	/// 对应 [V_WF_Users]
	/// </summary>
	public class 创建者DTO {

		/// <summary>
		/// 用户名 ( 如 global\duanjx ) 
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// 直观名称 ( 如 段俊雄（生产指挥中心）)
		/// </summary>
		public string DisplayName { get; set; }

	}
}