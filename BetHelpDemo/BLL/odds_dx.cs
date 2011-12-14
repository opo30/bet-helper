using System;
using System.Data;
using System.Collections.Generic;
using SeoWebSite.Common;
using SeoWebSite.Model;
using SeoWebSite.DAL;
namespace SeoWebSite.BLL
{
	/// <summary>
	/// odds_dx
	/// </summary>
	public class odds_dx
	{
        private readonly SeoWebSite.DAL.odds_dx dal = new SeoWebSite.DAL.odds_dx();
		public odds_dx()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(SeoWebSite.Model.odds_dx model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SeoWebSite.Model.odds_dx model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			return dal.Delete(id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			return dal.DeleteList(idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SeoWebSite.Model.odds_dx GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public SeoWebSite.Model.odds_dx GetModelByCache(int id)
		{
			
			string CacheKey = "odds_dxModel-" + id;
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
			return (SeoWebSite.Model.odds_dx)objModel;
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
		public List<SeoWebSite.Model.odds_dx> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SeoWebSite.Model.odds_dx> DataTableToList(DataTable dt)
		{
			List<SeoWebSite.Model.odds_dx> modelList = new List<SeoWebSite.Model.odds_dx>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SeoWebSite.Model.odds_dx model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SeoWebSite.Model.odds_dx();
					if(dt.Rows[n]["id"].ToString()!="")
					{
						model.id=int.Parse(dt.Rows[n]["id"].ToString());
					}
					if(dt.Rows[n]["companyID"].ToString()!="")
					{
						model.companyID=int.Parse(dt.Rows[n]["companyID"].ToString());
					}
					if(dt.Rows[n]["scheduleID"].ToString()!="")
					{
						model.scheduleID=int.Parse(dt.Rows[n]["scheduleID"].ToString());
					}
					if(dt.Rows[n]["pankou"].ToString()!="")
					{
						model.pankou=decimal.Parse(dt.Rows[n]["pankou"].ToString());
					}
					if(dt.Rows[n]["big"].ToString()!="")
					{
						model.big=decimal.Parse(dt.Rows[n]["big"].ToString());
					}
					if(dt.Rows[n]["small"].ToString()!="")
					{
						model.small=decimal.Parse(dt.Rows[n]["small"].ToString());
					}
					if(dt.Rows[n]["time"].ToString()!="")
					{
						model.time=DateTime.Parse(dt.Rows[n]["time"].ToString());
					}
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
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method

        /// <summary>
        /// 删除某公司某比赛的赔率
        /// </summary>
        public void Delete(string companyid, string scheduleid)
        {
            dal.Delete(companyid, scheduleid);
        }

        public DataSet queryOddsPer(string p, string p_2, string[] oddsArray)
        {
            throw new NotImplementedException();
        }

        public DataSet queryOddsCount(List<string> oddsList, float blur, int scheduleTypeID)
        {
            try
            {
                dal.CreateTempTable();
                return dal.queryOddsCount(oddsList, blur, scheduleTypeID);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

