using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PlanExecut.Domain.Entity.DataManageEntity;

namespace PlanExecut.Web.Areas.ZhiLingManage.DTO {
	public class 侧线与其点位号DTO {

		/// <summary>
		/// [M_hzsh_materialCodeInTable].[id]
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// 装置单元. 如101
		/// </summary>
		public string DeviceUnitCode { get; set; }

		/// <summary>
		/// 侧线编码
		/// </summary>
		public string MaterialCode { get; set; }

		/// <summary>
		/// 侧线的名称. 比如"XX原油"
		/// </summary>
		public string CodeName { get; set; }

		/// <summary>
		/// 侧线对应的指标名称. 比如"加工量"
		/// </summary>
		public string CategoryName { get; set; }

		/// <summary>
		/// 点位名
		/// </summary>
		private string _dianweiname;
		public string DianWeiName {
			get { return _dianweiname; }
			set { _dianweiname = value == null ? string.Empty : value; }
		}

		/// <summary>
		/// 点位号
		/// </summary>
		private string _dianweihao;
		public string DianWeiHao {
			get { return _dianweihao; }
			set { _dianweihao = value == null ? string.Empty : value; }
		}

		public int? UnitId { get; set; }

		/// <summary>
		/// 单位
		/// </summary>
		public string Unit { get; set; }


		public bool IsUsed { get; set; }

		/// <summary>
		/// 计划值
		/// </summary>
		private string _planvalue;
		public string PlanValue {
			get { return _planvalue; }
			set { _planvalue = value == null ? string.Empty : value; }
		}

		/// <summary>
		/// 上限值
		/// </summary>
		private string _topvalue;
		public string TopValue {
			get { return _topvalue; }
			set { _topvalue = value == null ? string.Empty : value; }
		}

		/// <summary>
		/// 下限值
		/// </summary>
		private string _lowvalue;
		public string LowValue {
			get { return _lowvalue; }
			set { _lowvalue = value == null ? string.Empty : value; }
		}


		/// <summary>
		/// 数据来源
		/// </summary>
		public string Remark { get; set; }

		public string 数据来源 {
			get {
				switch (Remark) {
					case "fpxj":
						return "复盘小结";
					case "excl":
						return "excl导入";
					default:
						return Remark;
				}
			}
		}



		public string 名称 {
			get {
				return CodeName + " 的 " + CategoryName;
			}
		}
	}
}