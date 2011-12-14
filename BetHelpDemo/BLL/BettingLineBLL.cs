using System;
using System.Data;
using System.Collections.Generic;
using SeoWebSite.Common;
using SeoWebSite.Model;
using SeoWebSite.Common.JSON;
namespace SeoWebSite.BLL
{
	/// <summary>
	/// 业务逻辑类bettingline 的摘要说明。
	/// </summary>
	public class BettingLineBLL
	{
		private readonly SeoWebSite.DAL.BettingLineDAO dal=new SeoWebSite.DAL.BettingLineDAO();
        private DataSet ds;
		public BettingLineBLL()
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
		public void Add(SeoWebSite.Model.BettingLine model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(SeoWebSite.Model.BettingLine model)
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
		public SeoWebSite.Model.BettingLine GetModel(string id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public SeoWebSite.Model.BettingLine GetModelByCache(string id)
		{
			
			string CacheKey = "bettinglineModel-" + id;
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
			return (SeoWebSite.Model.BettingLine)objModel;
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
		public List<SeoWebSite.Model.BettingLine> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SeoWebSite.Model.BettingLine> DataTableToList(DataTable dt)
		{
			List<SeoWebSite.Model.BettingLine> modelList = new List<SeoWebSite.Model.BettingLine>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SeoWebSite.Model.BettingLine model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SeoWebSite.Model.BettingLine();
					model.id=dt.Rows[n]["id"].ToString();
					if(dt.Rows[n]["betmoney"].ToString()!="")
					{
						model.betmoney=decimal.Parse(dt.Rows[n]["betmoney"].ToString());
					}
					if(dt.Rows[n]["returnmoney"].ToString()!="")
					{
						model.returnmoney=decimal.Parse(dt.Rows[n]["returnmoney"].ToString());
					}
					if(dt.Rows[n]["profit"].ToString()!="")
					{
						model.profit=decimal.Parse(dt.Rows[n]["profit"].ToString());
					}
					model.state=dt.Rows[n]["state"].ToString();
					model.formulaid=dt.Rows[n]["formulaid"].ToString();
					model.userid=dt.Rows[n]["userid"].ToString();
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
        
	}
}

