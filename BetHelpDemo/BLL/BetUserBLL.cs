using System;
using System.Data;
using System.Collections.Generic;
using SeoWebSite.Common;
using SeoWebSite.Model;
namespace SeoWebSite.BLL
{
	/// <summary>
	/// 业务逻辑类betuser 的摘要说明。
	/// </summary>
	public class BetUserBLL
	{
		private readonly SeoWebSite.DAL.BetUserDAO dal=new SeoWebSite.DAL.BetUserDAO();
		public BetUserBLL()
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
		public void Add(SeoWebSite.Model.BetUser model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(SeoWebSite.Model.BetUser model)
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
		public SeoWebSite.Model.BetUser GetModel(string id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public SeoWebSite.Model.BetUser GetModelByCache(string id)
		{
			
			string CacheKey = "betuserModel-" + id;
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
			return (SeoWebSite.Model.BetUser)objModel;
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
		public List<SeoWebSite.Model.BetUser> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SeoWebSite.Model.BetUser> DataTableToList(DataTable dt)
		{
			List<SeoWebSite.Model.BetUser> modelList = new List<SeoWebSite.Model.BetUser>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SeoWebSite.Model.BetUser model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SeoWebSite.Model.BetUser();
					model.id=dt.Rows[n]["id"].ToString();
					model.username=dt.Rows[n]["username"].ToString();
					model.password=dt.Rows[n]["password"].ToString();
					model.truename=dt.Rows[n]["truename"].ToString();
					if(dt.Rows[n]["age"].ToString()!="")
					{
						model.age=int.Parse(dt.Rows[n]["age"].ToString());
					}
					model.sex=dt.Rows[n]["sex"].ToString();
					model.qq=dt.Rows[n]["qq"].ToString();
					model.mobile=dt.Rows[n]["mobile"].ToString();
					model.email=dt.Rows[n]["email"].ToString();
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

