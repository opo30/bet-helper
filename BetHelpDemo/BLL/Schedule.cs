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
		public void Add(SeoWebSite.Model.Schedule model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SeoWebSite.Model.Schedule model)
		{
			return dal.Update(model);
		}

        /// <summary>
        /// 设置是否更新
        /// </summary>
        public void SetUpdated(string scheduleid,bool isUpdated)
        {
            dal.SetUpdated(scheduleid,isUpdated);
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
		public SeoWebSite.Model.Schedule GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
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
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetScheduleIDList(string strWhere)
        {
            return dal.GetScheduleIDList(strWhere);
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
		public List<SeoWebSite.Model.Schedule> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
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
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(string strWhere, int start, int end, out int totalCount)
        {
            totalCount = dal.GetTotalCount(strWhere);
            return dal.GetList(strWhere, start, end);
        }
	}
}

