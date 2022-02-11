using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PlanExecut.Domain.Entity.ZhiLingManage;

namespace PlanExecut.Web.Areas.ZhiLingManage.DTO {

	public class 填报数据表最新的MESID及其对应的计划值指令DTO {

		/// <summary>
		/// 用"英文逗号"分割的多个MESID, 每一个MESID由不同的人员配置ID分组后发送到MES系统而来
		/// <para>按"人员配置id分组发送给MES后",得到的多个mesid</para>
		/// </summary>
		public string MESID { get; set; }

		/// <summary>
		/// 对应的计划值指令
		/// </summary>
		public List<ZhiLingInfo> ZhiLingInfoList { get; set; }

	}
}