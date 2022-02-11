using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Services.Description;
using PlanExecut.Code;
using PlanExecut.Data;
using PlanExecut.Data.Extensions;
using PlanExecut.Domain.Entity.DataManageEntity;
using PlanExecut.Domain.Entity.PlanOrderManageEntity;
using PlanExecut.Domain.Entity.PlanOrderManageEntity.DTO;
using PlanExecut.Domain.Entity.ZhiLingManage;
using PlanExecut.Repository.PlanOrderManage;
using PlanExecut.Repository.ZhiLingManage;
using PlanExecut.Web.Areas.PlanOrderManage.VO;
using PlanExecut.Web.Areas.ZhiLingManage.DTO;
using Newtonsoft.Json;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Collections;

namespace PlanExecut.Web.Areas.ZhiLingManage.Services {

	public partial class ZhiLingService {




		private string 提交指令到远程服务2(string 指令的标题, string 指令字符串, string 开始日期, string 结束日期, int 人员配置方案id) {

			//
			string 执行人, 阅知人, 创建人;
			var fangAn = myDataSource._人员配置方案.IQueryable().Where(i => i.ID == 人员配置方案id).FirstOrDefault();
			执行人 = fangAn.ExecutorValue;
			阅知人 = fangAn.KnowerValue;
			创建人 = _获取创建人信息().UserName;


			string postData = "templateName=调度指令&taskName={0}&taskContent={1}&beginDate={2}&endDate={3}&executor={4}&knower={5}&createBy={6}&IsSubmit={7}";
			postData = string.Format(
				postData,
				HttpUtility.UrlEncode(指令的标题),         // 指令标题
				HttpUtility.UrlEncode(指令字符串),       // 指令内容
				开始日期 + " 00:00:00",     // 指令开始时间
				结束日期 + " 00:00:00",     //指令结束时间
				执行人,  // 执行人
				阅知人,  // 阅知人
				创建人,  // 创建人
				0    //指令状态 0:创建指令为草稿状态， 1:为提交状态
			);



			string 响应结果 = string.Empty;




			bool 测试 = true;
			if (测试 == false) {
				var url = "http://10.152.70.4:8040/orders/Task/CreateOperatorTask";
				//var url = "http://localhost:7502/Task/CreateOperatorTask";
				var request = WebRequest.Create(url);
				request.Method = "POST";

				byte [] byteArray = Encoding.UTF8.GetBytes(postData);

				request.ContentType = "application/x-www-form-urlencoded";
				request.ContentLength = byteArray.Length;

				Stream dataStream = request.GetRequestStream();
				dataStream.Write(byteArray, 0, byteArray.Length);
				dataStream.Close();


				WebResponse response = request.GetResponse();


				using (dataStream = response.GetResponseStream()) {
					StreamReader reader = new StreamReader(dataStream);
					string responseFromServer = reader.ReadToEnd();
					Debug.WriteLine(responseFromServer);
					响应结果 = responseFromServer;
				}
				response.Close();


				const string 示例返回结果 = "[{Data=F63DAEC1-7038-4704-9EF2-910F92FABD0C}]";
				var len = 示例返回结果.Length;
				if (响应结果.Length == len) {
					响应结果 = 响应结果.Substring(7, 响应结果.Length - 7);
					响应结果 = 响应结果.Substring(0, 响应结果.Length - 2);
				}
			}
			else {
				响应结果 = "MESID-" + DateTime.Now.Ticks;
				Debug.WriteLine("返回测试的MESID = " + 响应结果);
			}


			string MESID = 响应结果;


			return MESID;
		}



	}
}