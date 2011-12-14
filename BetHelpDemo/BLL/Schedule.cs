using System;
using System.Data;
using System.Collections.Generic;
using SeoWebSite.Common;
using SeoWebSite.Model;
using SeoWebSite.DAL;
namespace SeoWebSite.BLL
{
	/// <summary>
	/// Schedule
	/// </summary>
	public class Schedule
	{
        private readonly SeoWebSite.DAL.Schedule dal = new SeoWebSite.DAL.Schedule();
		public Schedule()
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
		public void Add(SeoWebSite.Model.Schedule model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Update(SeoWebSite.Model.Schedule model)
		{
			return dal.Update(model);
		}

        /// <summary>
        /// �����Ƿ����
        /// </summary>
        public void SetUpdated(string scheduleid,bool isUpdated)
        {
            dal.SetUpdated(scheduleid,isUpdated);
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
		public SeoWebSite.Model.Schedule GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ�����
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
        /// ��������б�
        /// </summary>
        public DataSet GetScheduleIDList(string strWhere)
        {
            return dal.GetScheduleIDList(strWhere);
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
					model.data=dt.Rows[n]["data"].ToString();
					if(dt.Rows[n]["updated"].ToString()!="")
					{
						if((dt.Rows[n]["updated"].ToString()=="1")||(dt.Rows[n]["updated"].ToString().ToLower()=="true"))
						{
						model.updated=true;
						}
						else
						{
							model.updated=false;
						}
					}
					if(dt.Rows[n]["date"].ToString()!="")
					{
						model.date=DateTime.Parse(dt.Rows[n]["date"].ToString());
					}
					if(dt.Rows[n]["home"].ToString()!="")
					{
						model.home=int.Parse(dt.Rows[n]["home"].ToString());
					}
					if(dt.Rows[n]["away"].ToString()!="")
					{
						model.away=int.Parse(dt.Rows[n]["away"].ToString());
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
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(string strWhere, int start, int end, out int totalCount)
        {
            totalCount = dal.GetTotalCount(strWhere);
            return dal.GetList(strWhere, start, end);
        }
	}
}

