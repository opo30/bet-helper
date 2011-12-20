using System;
using System.Data;
using System.Collections.Generic;
using SeoWebSite.Common;
using SeoWebSite.Model;
namespace SeoWebSite.BLL
{
	/// <summary>
	/// ҵ���߼���Schedule ��ժҪ˵����
	/// </summary>
	public class ScheduleBLL
	{
        private readonly SeoWebSite.DAL.ScheduleDAO dal = new SeoWebSite.DAL.ScheduleDAO();
		public ScheduleBLL()
		{}
		#region  ��Ա����

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
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool ExistsSchedule(int scheduleid)
        {
            return dal.ExistsSchedule(scheduleid);
        }

		/// <summary>
		/// ����һ������
		/// </summary>
		public void  Add(SeoWebSite.Model.Schedule model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(SeoWebSite.Model.Schedule model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int id)
		{
			
			dal.Delete(id);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public SeoWebSite.Model.Schedule GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public SeoWebSite.Model.Schedule GetModelByCache(int id)
		{
			
			string CacheKey = "ScheduleModel-" + id;
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
			return (SeoWebSite.Model.Schedule)objModel;
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
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(string strWhere, int start, int end, out int totalCount)
        {
            totalCount = dal.GetTotalCount(strWhere);
            return dal.GetList(strWhere, start, end);
        }
        /// <summary>
        /// ��ҳ
        /// </summary>
        public DataSet GetList(int PageSize, int PageIndex, string strWhere)
        {
            return dal.GetList(PageSize, PageIndex, strWhere);
        }
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<SeoWebSite.Model.Schedule> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<SeoWebSite.Model.Schedule> DataTableToList(DataTable dt)
		{
			List<SeoWebSite.Model.Schedule> modelList = new List<SeoWebSite.Model.Schedule>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SeoWebSite.Model.Schedule model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SeoWebSite.Model.Schedule();
					if(dt.Rows[n]["id"].ToString()!="")
					{
						model.id=int.Parse(dt.Rows[n]["id"].ToString());
					}
                    if (dt.Rows[n]["sclassid"].ToString() != "")
					{
                        model.sclassid = int.Parse(dt.Rows[n]["sclassid"].ToString());
					}
					model.data=dt.Rows[n]["Data"].ToString();
					if(dt.Rows[n]["Date"].ToString()!="")
					{
						model.date=DateTime.Parse(dt.Rows[n]["Date"].ToString());
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
		/// ��������б�
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  ��Ա����

        public SeoWebSite.Model.Schedule GetTopOne()
        {
            return dal.GetTopOne();
        }

        public SeoWebSite.Model.Schedule GetTopOne_NoExp()
        {
            return dal.GetTopOne_NoExp();
        }

        public void UpdateState(int p, bool p_2)
        {
            dal.UpdateState(p, p_2);
        }

        public DataSet statOddsHistory(string whereStr)
        {
            return dal.statOddsHistory(whereStr);
        }

        public DataSet queryOddsHistory(string whereStr)
        {
            return dal.queryOddsHistory(whereStr);
        }

        public DataSet statOddsHistoryGroupByDate(string whereStr)
        {
            return dal.statOddsHistoryGroupByDate(whereStr);
        }
    }
}

