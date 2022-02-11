using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PlanExecut.Domain.Entity.PlanOrderManageEntity;
using PlanExecut.Domain.Entity.ZhiLingManage;
using PlanExecut.Web.Areas.ZhiLingManage.Services;

namespace PlanExecut.Web.Areas.ZhiLingManage.VO {


	public class 指令运行状态VO {
		public 指令运行状态VO() {
			装置调整 = new 装置调整_指令状态VO();
			主要控制指标 = new 主要控制指标_指令状态VO();
			生产运行优化 = new 生产运行优化_指令状态VO();
		}

		public 指令运行状态VO(string running_day, List<ZhiLingInfo> list) {
			var 参考对象 = list.Count == 0 ? null : list [0];

			// 
			装置调整_指令状态VO aa = new 装置调整_指令状态VO();
			aa.RunningDay = running_day;
			aa.已下达的指令数据 = list.Where(i => i.Type == 2).ToList();
			this.装置调整 = aa;

			//
			主要控制指标_指令状态VO bb = new 主要控制指标_指令状态VO();
			bb.RunningDay = running_day;
			bb.已下达的指令数据 = list.Where(i => i.Type == 0).ToList();
			this.主要控制指标 = bb;

			//
			生产运行优化_指令状态VO cc = new 生产运行优化_指令状态VO();
			cc.RunningDay = running_day;
			cc.已下达的指令数据 = list.Where(i => i.Type == 1).ToList();
			this.生产运行优化 = cc;
		}


		public 装置调整_指令状态VO 装置调整 { get; set; }

		public 主要控制指标_指令状态VO 主要控制指标 { get; set; }

		public 生产运行优化_指令状态VO 生产运行优化 { get; set; }


		public string ZhiLingTitle { get; set; }

		public string RootMesId { get; set; }


		public string StartDay { get; set; }


		public string EndDay { get; set; }


		/// <summary>
		/// 指令状态
		/// </summary>
		public int State { get; set; }

		public string StateText {
			get {
				string v;
				switch (State) {
					case 1:
						v = "进行中";
						break;
					case 2:
						v = "已完成";
						break;
					case 3:
						v = "已完结";
						break;
					default:
						v = "-";
						break;
				}
				return v;
			}
		}

	}
}