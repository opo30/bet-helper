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
		public int  Add(SeoWebSite.Model.odds_dx model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Update(SeoWebSite.Model.odds_dx model)
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
		public SeoWebSite.Model.odds_dx GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ�����
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
		public List<SeoWebSite.Model.odds_dx> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
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

