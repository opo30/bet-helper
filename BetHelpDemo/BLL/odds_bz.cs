using System;
using System.Data;
using System.Collections.Generic;
using SeoWebSite.Common;
using SeoWebSite.Model;
using SeoWebSite.DAL;
namespace SeoWebSite.BLL
{
	/// <summary>
	/// odds_bz
	/// </summary>
	public class odds_bz
	{
        private readonly SeoWebSite.DAL.odds_bz dal = new SeoWebSite.DAL.odds_bz();
		public odds_bz()
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
		public int  Add(SeoWebSite.Model.odds_bz model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SeoWebSite.Model.odds_bz model)
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
		public SeoWebSite.Model.odds_bz GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public SeoWebSite.Model.odds_bz GetModelByCache(int id)
		{
			
			string CacheKey = "odds_bzModel-" + id;
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
			return (SeoWebSite.Model.odds_bz)objModel;
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
		public List<SeoWebSite.Model.odds_bz> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SeoWebSite.Model.odds_bz> DataTableToList(DataTable dt)
		{
			List<SeoWebSite.Model.odds_bz> modelList = new List<SeoWebSite.Model.odds_bz>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SeoWebSite.Model.odds_bz model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SeoWebSite.Model.odds_bz();
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
					if(dt.Rows[n]["home"].ToString()!="")
					{
						model.home=decimal.Parse(dt.Rows[n]["home"].ToString());
					}
					if(dt.Rows[n]["draw"].ToString()!="")
					{
						model.draw=decimal.Parse(dt.Rows[n]["draw"].ToString());
					}
					if(dt.Rows[n]["away"].ToString()!="")
					{
						model.away=decimal.Parse(dt.Rows[n]["away"].ToString());
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

        public DataSet GetList(string companyid, string tt, string p1, string p2, string p3)
        {
            return dal.GetList(companyid, tt, p1, p2, p3);
        }

        public DataSet GetList(string companyid, string[] oddsArray)
        {
            return dal.GetList(companyid, oddsArray);
        }

        public DataSet queryOddsPer(string p, string p_2, string[] oddsArray)
        {
            throw new NotImplementedException();
        }

        public DataSet queryOddsCount(List<string> oddsList, float blur,int scheduleTypeID)
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

        public DataTable queryCompanyOddsPer(List<string> oddsList, int scheduleTypeID, int scheduleID)
        {
            try
            {
                dal.CreateTempTable();
                return dal.queryCompanyOddsPer(oddsList, scheduleTypeID, scheduleID);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

