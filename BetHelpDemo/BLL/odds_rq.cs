using System;
using System.Data;
using System.Collections.Generic;
using SeoWebSite.Common;
using SeoWebSite.Model;
using SeoWebSite.DAL;
namespace SeoWebSite.BLL
{
	/// <summary>
	/// odds_rq
	/// </summary>
	public class odds_rq
	{
        private readonly SeoWebSite.DAL.odds_rq dal = new SeoWebSite.DAL.odds_rq();
		public odds_rq()
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
		public int  Add(SeoWebSite.Model.odds_rq model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SeoWebSite.Model.odds_rq model)
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
		public SeoWebSite.Model.odds_rq GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public SeoWebSite.Model.odds_rq GetModelByCache(int id)
		{
			
			string CacheKey = "odds_rqModel-" + id;
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
			return (SeoWebSite.Model.odds_rq)objModel;
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
		public List<SeoWebSite.Model.odds_rq> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SeoWebSite.Model.odds_rq> DataTableToList(DataTable dt)
		{
			List<SeoWebSite.Model.odds_rq> modelList = new List<SeoWebSite.Model.odds_rq>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SeoWebSite.Model.odds_rq model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SeoWebSite.Model.odds_rq();
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
					if(dt.Rows[n]["home"].ToString()!="")
					{
						model.home=decimal.Parse(dt.Rows[n]["home"].ToString());
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


        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(string strWhere, int start, int end, out int totalCount)
        {
            totalCount = dal.GetTotalCount(strWhere);
            return dal.GetList(strWhere, start, end);
        }

        /// <summary>
        /// 获得符合条件的赔率的比赛
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="tt"></param>
        /// <param name="oddsArray"></param>
        /// <returns></returns>
        public DataSet GetList(string companyid , string[] oddsArray)
        {
            return dal.GetList(companyid, oddsArray);
        }


        public DataSet queryOddsCount(List<string> oddsList, float blur, int scheduleTypeID)
        {
            try
            {
                dal.CreateTempTable();
                return dal.queryOddsCount(oddsList, blur, scheduleTypeID,0);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataSet queryOddsCount(List<string> oddsList, float blur, int scheduleTypeID,int scheduleid)
        {
            try
            {
                dal.CreateTempTable();
                return dal.queryOddsCount(oddsList, blur, scheduleTypeID, scheduleid);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable queryOddsPer(List<string> oddsList, float blur, int scheduleTypeID, int scheduleid)
        {
            try
            {
                dal.CreateTempTable();
                return dal.queryOddsPer(oddsList, blur, scheduleTypeID, scheduleid);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataSet queryCompanyOddsCount(string companyid, string home, string pankou, string away, int scheduleType,string ptime)
        {
            try
            {
                dal.CreateTempTable();
                return dal.queryCompanyOddsCount(companyid, home, pankou, away, scheduleType, ptime);
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

        public DataTable queryCompanyOddsPer1(int scheduleTypeID, int scheduleID, List<int> companyidList)
        {
            try
            {
                dal.CreateTempTable();
                return dal.queryCompanyOddsPer1(scheduleTypeID, scheduleID, companyidList);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable queryCompanyOddsPer1(int scheduleTypeID, int scheduleID, int companyid, List<string> whereStrList)
        {
            try
            {
                dal.CreateTempTable();
                return dal.queryCompanyOddsPer1(scheduleTypeID, scheduleID, companyid, whereStrList);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

