using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlanExecut.Web.Areas.ZhiLingManage.DTO {
	public class 用户自定义的指令DTO {

		/// <summary>
		/// [M_OrderType].[id]
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// [M_MonitorPointMaintenance].[F_Id]
		/// </summary>
		public int ZhiLingId { get; set; }

		/// <summary>
		/// 用户自定义的指令的名称
		/// </summary>
		public string ZhiLingName { get; set; }

		public string 指令的名称 { get; set; }


		/// <summary>
		/// 装置单元
		/// </summary>
		public string DeviceUnitCode { get; set; }

		/// <summary>
		/// 0: 主要控制指标
		/// 1: 文本描述
		/// 2: 装置调整
		/// </summary>
		public int Type { get; set; }
	}
}