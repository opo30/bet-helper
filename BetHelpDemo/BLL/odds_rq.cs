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
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(SeoWebSite.Model.odds_rq model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Update(SeoWebSite.Model.odds_rq model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool Delete(int id)
		{
			
			return dal.Delete(id);
		}
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool DeleteList(string idlist )
		{
			return dal.DeleteList(idlist );
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public SeoWebSite.Model.odds_rq GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ�����
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
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// ���ǰ��������
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<SeoWebSite.Model.odds_rq> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
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
		/// ��������б�
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// ��ҳ��ȡ�����б�
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method

        /// <summary>
        /// ɾ��ĳ��˾ĳ����������
        /// </summary>
        public void Delete(string companyid, string scheduleid)
        {
            dal.Delete(companyid, scheduleid);
        }


        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(string strWhere, int start, int end, out int totalCount)
        {
            totalCount = dal.GetTotalCount(strWhere);
            return dal.GetList(strWhere, start, end);
        }

        /// <summary>
        /// ��÷������������ʵı���
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

