using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PlanExecut.Domain.Entity.PlanOrderManageEntity;
using PlanExecut.Domain.Entity.ZhiLingManage;
using PlanExecut.Code;
using NPOI.SS.Formula.Functions;
using NPOI.XSSF.Streaming.Values;
using System.Web.UI.WebControls;
using System.Diagnostics;
using PlanExecut.Web.Areas.ZhiLingManage.Services;

namespace PlanExecut.Web.Areas.ZhiLingManage.VO {


	public class 主要控制指标_指令状态VO {


		public string RunningDay { get; set; }

		private List<ZhiLingInfo> _已下达的指令数据 = new List<ZhiLingInfo>();
		public List<ZhiLingInfo> 已下达的指令数据 {
			get { return _已下达的指令数据; }
			set {
				_已下达的指令数据 = value;
				foreach (var one in _已下达的指令数据) {
					one.主要控制指标指令的标题 = OrderStringHelper.主要控制指标_转换到字符串表示_单行(one);
				}
			}
		}

		public string 指令字符串 { get; set; }


	}
}