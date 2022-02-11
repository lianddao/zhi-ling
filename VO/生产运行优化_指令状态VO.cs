using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PlanExecut.Domain.Entity.PlanOrderManageEntity;
using PlanExecut.Domain.Entity.ZhiLingManage;

namespace PlanExecut.Web.Areas.ZhiLingManage.VO {


	public class 生产运行优化_指令状态VO {

		public string RunningDay { get; set; }

		private List<ZhiLingInfo> _已下达的指令数据 = new List<ZhiLingInfo>();
		public List<ZhiLingInfo> 已下达的指令数据 {
			get { return _已下达的指令数据; }
			set {
				_已下达的指令数据 = value;
				foreach (var one in _已下达的指令数据) {
					one.装置调整指令的标题 = one.OrderContent;
				}
			}
		}

		public string 指令字符串 { get; set; }

	}
}