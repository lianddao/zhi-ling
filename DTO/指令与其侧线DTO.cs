using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlanExecut.Web.Areas.ZhiLingManage.DTO {

	/// <summary>
	/// (保存指令与侧线的关联)
	/// </summary>
	[Obsolete]
	public class 指令与其侧线DTO {

		/// <summary>
		/// [M_ProductOrderType].[id]
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// 侧线编码
		/// </summary>
		public string MaterialCode { get; set; }
	}
}