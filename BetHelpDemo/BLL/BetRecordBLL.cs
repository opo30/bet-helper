using System;
using System.Data;
using System.Collections.Generic;
using SeoWebSite.Common;
using SeoWebSite.Model;
using SeoWebSite.Common.JSON;
namespace SeoWebSite.BLL
{
	/// <summary>
	/// 业务逻辑类betrecord 的摘要说明。
	/// </summary>
	public class BetRecordBLL
	{
		private readonly SeoWebSite.DAL.BetRecordDAO dal=new SeoWebSite.DAL.BetRecordDAO();
        private DataSet ds;
		public BetRecordBLL()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string id)
		{
			return dal.Exists(id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SeoWebSite.Model.BetRecord model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(SeoWebSite.Model.BetRecord model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string id)
		{
			
			dal.Delete(id);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SeoWebSite.Model.BetRecord GetModel(string id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public SeoWebSite.Model.BetRecord GetModelByCache(string id)
		{
			
			string CacheKey = "betrecordModel-" + id;
			object objModel = SeoWebSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(id);
					if (objModel != null)
					{
						int ModelCache = SeoWebSite.Common.ConfigHelper.GetConfigInt("ModelCache");
						SeoWebSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (SeoWebSite.Model.BetRecord)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SeoWebSite.Model.BetRecord> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SeoWebSite.Model.BetRecord> DataTableToList(DataTable dt)
		{
			List<SeoWebSite.Model.BetRecord> modelList = new List<SeoWebSite.Model.BetRecord>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SeoWebSite.Model.BetRecord model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SeoWebSite.Model.BetRecord();
					model.id=dt.Rows[n]["id"].ToString();
					model.lineid=dt.Rows[n]["lineid"].ToString();
					model.teamname=dt.Rows[n]["teamname"].ToString();
					model.traditional=dt.Rows[n]["traditional"].ToString();
					if(dt.Rows[n]["starttime"].ToString()!="")
					{
						model.starttime=DateTime.Parse(dt.Rows[n]["starttime"].ToString());
					}
					if(dt.Rows[n]["endtime"].ToString()!="")
					{
						model.endtime=DateTime.Parse(dt.Rows[n]["endtime"].ToString());
					}
					if(dt.Rows[n]["bettime"].ToString()!="")
					{
						model.bettime=DateTime.Parse(dt.Rows[n]["bettime"].ToString());
					}
					model.itemid=dt.Rows[n]["itemid"].ToString();
					model.detailid=dt.Rows[n]["detailid"].ToString();
					if(dt.Rows[n]["betmoney"].ToString()!="")
					{
						model.betmoney=decimal.Parse(dt.Rows[n]["betmoney"].ToString());
					}
					if(dt.Rows[n]["returnmoney"].ToString()!="")
					{
						model.returnmoney=decimal.Parse(dt.Rows[n]["returnmoney"].ToString());
					}
					model.result=dt.Rows[n]["result"].ToString();
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  成员方法

        /// <summary>
        /// 查询某用户的投注列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetBetRecordList(string userid)
        {
            JSONHelper json = new JSONHelper();
            ds = dal.GetList(userid);
            json.success = true;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //投注记录表数据
                json.AddItem("id", dr["id"].ToString());
                json.AddItem("lineid", dr["lineid"].ToString());
                json.AddItem("teamname", dr["teamname"].ToString());
                json.AddItem("traditional", dr["traditional"].ToString());
                json.AddItem("starttime", dr["starttime"].ToString());
                json.AddItem("endtime", dr["endtime"].ToString());
                json.AddItem("bettime", dr["bettime"].ToString());
                json.AddItem("itemid", dr["itemid"].ToString());
                json.AddItem("detailid", dr["detailid"].ToString());
                json.AddItem("betmoney", dr["betmoney"].ToString());
                json.AddItem("returnmoney", dr["returnmoney"].ToString());
                json.AddItem("result", dr["result"].ToString());
                json.AddItem("sourceid", dr["sourceid"].ToString());
                //投注线程表数据
                json.AddItem("linename", dr["linename"].ToString());
                json.AddItem("linebetmoney", dr["linebetmoney"].ToString());
                json.AddItem("profit", dr["profit"].ToString());
                json.AddItem("state", dr["state"].ToString());
                json.AddItem("iscomplete", dr["iscomplete"].ToString());
                json.AddItem("isbetting", dr["isbetting"].ToString());
                //投注项目表数据
                json.AddItem("itemname", dr["itemname"].ToString());
                //投注细节数据
                json.AddItem("detailname", dr["detailname"].ToString());

                json.ItemOk();
            }
            json.totlalCount = ds.Tables[0].Rows.Count;
            string betRecordlist = json.ToString();
            return betRecordlist;
        }
	}
}

