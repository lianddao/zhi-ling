using PlanExecut.Repository.DataManage;
using PlanExecut.Repository.PlanOrderManage;
using PlanExecut.Repository.ZhiLingManage;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using PlanExecut.Domain.Entity.ZhiLingManage;
using PlanExecut.Code;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using PlanExecut.Data.Extensions;
using System.Configuration;
using System.CodeDom.Compiler;
using PlanExecut.Web.Areas.ZhiLingManage.DTO;
using System.Security.Policy;
using PlanExecut.Domain.Entity.PlanOrderManageEntity.DTO;
using System.Data.Entity.Infrastructure;
using NPOI.SS.Formula.Functions;
using PlanExecut.Domain.Entity.DataManageEntity;

namespace PlanExecut.Web.Areas.ZhiLingManage.Services {
	public partial class ZhiLingService {


		// 一个装置 包括 多个指令定义 『M_OrderType』
		// 一个指令定义 包括 多个侧线定义 『M_ProductOrderType』

		private class DataSource {

			///// <summary>
			///// 『M_OrderType』用户自定义的指令
			///// </summary>
			//[Obsolete]
			//public M_OrderTypeRepository _用户自定义的指令 { get; private set; }

			///// <summary>
			///// 『M_ProductOrderType』
			///// </summary>
			//[Obsolete]
			//public M_ProductOrderTypeRepository _自定义指令的详情 { get; private set; }

			/// <summary>
			/// 侧线与其点位号
			/// </summary>
			public M_materialCodeInTableRepository _侧线与其点位号 { get; private set; }

			/// <summary>
			/// 预指令
			/// </summary>
			public YuZhiLingRepository _YuZhiLing { get; private set; }

			public PlanExecutorRepository _人员配置方案 { get; private set; }

			public ZhiLingMesRepository _ZhiLingMes { get; private set; }

			public ZhiLingRealInfoRepository _ZhiLingRealInfo { get; private set; }

			/// <summary>
			/// 预指令历史方案
			/// </summary>
			public YuZhiLingTempRepository _YuZhiLingTemp { get; private set; }


			public M_data_UnitRepository _单位 { get; private set; }

			//[Obsolete]
			//public ZhuangZhiTiaoZhengRepository _装置调整 { get; private set; }

			//[Obsolete]
			//public KongZhiZhiBiaoRepository _主要控制指标 { get; private set; }

			public UserZhiLingRepository _UserZhiLing { get; private set; }

			//public ZhiLingHistoryRepository _ZhiLingHistory { get; set; }


			public DataSource() {
				//this._用户自定义的指令 = new M_OrderTypeRepository();
				//this._自定义指令的详情 = new M_ProductOrderTypeRepository();
				this._侧线与其点位号 = new M_materialCodeInTableRepository();
				this._YuZhiLingTemp = new YuZhiLingTempRepository();
				this._YuZhiLing = new YuZhiLingRepository();
				this._ZhiLingMes = new ZhiLingMesRepository();
				this._ZhiLingRealInfo = new ZhiLingRealInfoRepository();
				this._人员配置方案 = new PlanExecutorRepository();
				this._单位 = new M_data_UnitRepository();

				//this._装置调整 = new ZhuangZhiTiaoZhengRepository();
				//this._主要控制指标 = new KongZhiZhiBiaoRepository();
				this._UserZhiLing = new UserZhiLingRepository();
				//this._ZhiLingHistory = new ZhiLingHistoryRepository();
			}
		}

		private DataSource myDataSource = new DataSource();







		public 创建者DTO _获取创建人信息() {

			/*
			 * 
			 * 保留这段旧代码作参考用
			string createBy = "";
			string createByName = "";

			string connectionString = ConfigurationManager.ConnectionStrings ["abc123456"].ConnectionString;

			string sql2 = "select ShiftID from V_BanBie";
			DataTable dt2 = new DataTable();
			DbHelper.gExecuteDT(sql2, connectionString, dt2);

			if (dt2.Rows.Count > 0) {
				string sql3 = "select UserName,DisplayName from V_MesUser where UserName like '%zhzx%' and DisplayName like '%" + dt2.Rows [0] ["ShiftID"].ToString() + "%' ";
				DataTable dt3 = new DataTable();
				DbHelper.gExecuteDT(sql3, connectionString, dt3);

				if (dt3.Rows.Count > 0) {
					createBy = dt3.Rows [0] ["UserName"].ToString();
					createByName = dt3.Rows [0] ["DisplayName"].ToString();
				}
			}
			*
			*/

			// ★★★ 固定值: 段俊雄 ★★★

			string connectionString = ConfigurationManager.ConnectionStrings ["abc123456"].ConnectionString;
			string sql1 = "SELECT * FROM [dbo].[V_MesUser] WHERE DepartName LIKE '%指挥中心%' AND DisplayName LIKE '%段俊雄%'";

			DataTable dt1 = new DataTable();
			DbHelper.gExecuteDT(sql1, connectionString, dt1);

			创建者DTO dto = new 创建者DTO();
			dto.UserName = dt1.Rows [0] ["UserName"].ToString();
			dto.DisplayName = dt1.Rows [0] ["DisplayName"].ToString();

			return dto;
		}







		public IEnumerable<M_data_UnitEntity> DanWei_Service1_所有单位数据() {
			var list = myDataSource._单位.IQueryable().ToList();

			return list;
		}
	}
}