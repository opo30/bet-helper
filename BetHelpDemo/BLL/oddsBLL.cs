using System;
using System.Data;
using System.Collections.Generic;
using SeoWebSite.Common;
using SeoWebSite.Model;
using SeoWebSite.DAL;

namespace SeoWebSite.BLL
{
	/// <summary>
	/// odds
	/// </summary>
	public partial class OddsBLL
	{
		private readonly OddsDAO dal=new OddsDAO();
		public OddsBLL()
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
		public void Add(SeoWebSite.Model.Odds model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SeoWebSite.Model.Odds model)
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
		public SeoWebSite.Model.Odds GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public SeoWebSite.Model.Odds GetModelByCache(int id)
		{
			
			string CacheKey = "oddsModel-" + id;
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
			return (SeoWebSite.Model.Odds)objModel;
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
		public List<SeoWebSite.Model.Odds> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SeoWebSite.Model.Odds> DataTableToList(DataTable dt)
		{
			List<SeoWebSite.Model.Odds> modelList = new List<SeoWebSite.Model.Odds>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SeoWebSite.Model.Odds model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SeoWebSite.Model.Odds();
					if(dt.Rows[n]["id"]!=null && dt.Rows[n]["id"].ToString()!="")
					{
						model.id=int.Parse(dt.Rows[n]["id"].ToString());
					}
					if(dt.Rows[n]["companyid"]!=null && dt.Rows[n]["companyid"].ToString()!="")
					{
						model.companyid=int.Parse(dt.Rows[n]["companyid"].ToString());
					}
					if(dt.Rows[n]["s_win"]!=null && dt.Rows[n]["s_win"].ToString()!="")
					{
						model.s_win=decimal.Parse(dt.Rows[n]["s_win"].ToString());
					}
					if(dt.Rows[n]["s_draw"]!=null && dt.Rows[n]["s_draw"].ToString()!="")
					{
						model.s_draw=decimal.Parse(dt.Rows[n]["s_draw"].ToString());
					}
					if(dt.Rows[n]["s_lost"]!=null && dt.Rows[n]["s_lost"].ToString()!="")
					{
						model.s_lost=decimal.Parse(dt.Rows[n]["s_lost"].ToString());
					}
					if(dt.Rows[n]["s_winper"]!=null && dt.Rows[n]["s_winper"].ToString()!="")
					{
						model.s_winper=decimal.Parse(dt.Rows[n]["s_winper"].ToString());
					}
					if(dt.Rows[n]["s_drawper"]!=null && dt.Rows[n]["s_drawper"].ToString()!="")
					{
						model.s_drawper=decimal.Parse(dt.Rows[n]["s_drawper"].ToString());
					}
					if(dt.Rows[n]["s_lostper"]!=null && dt.Rows[n]["s_lostper"].ToString()!="")
					{
						model.s_lostper=decimal.Parse(dt.Rows[n]["s_lostper"].ToString());
					}
					if(dt.Rows[n]["s_return"]!=null && dt.Rows[n]["s_return"].ToString()!="")
					{
						model.s_return=decimal.Parse(dt.Rows[n]["s_return"].ToString());
					}
					if(dt.Rows[n]["e_win"]!=null && dt.Rows[n]["e_win"].ToString()!="")
					{
						model.e_win=decimal.Parse(dt.Rows[n]["e_win"].ToString());
					}
					if(dt.Rows[n]["e_draw"]!=null && dt.Rows[n]["e_draw"].ToString()!="")
					{
						model.e_draw=decimal.Parse(dt.Rows[n]["e_draw"].ToString());
					}
					if(dt.Rows[n]["e_lost"]!=null && dt.Rows[n]["e_lost"].ToString()!="")
					{
						model.e_lost=decimal.Parse(dt.Rows[n]["e_lost"].ToString());
					}
					if(dt.Rows[n]["e_winper"]!=null && dt.Rows[n]["e_winper"].ToString()!="")
					{
						model.e_winper=decimal.Parse(dt.Rows[n]["e_winper"].ToString());
					}
					if(dt.Rows[n]["e_drawper"]!=null && dt.Rows[n]["e_drawper"].ToString()!="")
					{
						model.e_drawper=decimal.Parse(dt.Rows[n]["e_drawper"].ToString());
					}
					if(dt.Rows[n]["e_lostper"]!=null && dt.Rows[n]["e_lostper"].ToString()!="")
					{
						model.e_lostper=decimal.Parse(dt.Rows[n]["e_lostper"].ToString());
					}
					if(dt.Rows[n]["e_return"]!=null && dt.Rows[n]["e_return"].ToString()!="")
					{
						model.e_return=decimal.Parse(dt.Rows[n]["e_return"].ToString());
					}
					if(dt.Rows[n]["winkelly"]!=null && dt.Rows[n]["winkelly"].ToString()!="")
					{
						model.winkelly=decimal.Parse(dt.Rows[n]["winkelly"].ToString());
					}
					if(dt.Rows[n]["drawkelly"]!=null && dt.Rows[n]["drawkelly"].ToString()!="")
					{
						model.drawkelly=decimal.Parse(dt.Rows[n]["drawkelly"].ToString());
					}
					if(dt.Rows[n]["lostkelly"]!=null && dt.Rows[n]["lostkelly"].ToString()!="")
					{
						model.lostkelly=decimal.Parse(dt.Rows[n]["lostkelly"].ToString());
					}
					if(dt.Rows[n]["lastupdatetime"]!=null && dt.Rows[n]["lastupdatetime"].ToString()!="")
					{
						model.lastupdatetime=DateTime.Parse(dt.Rows[n]["lastupdatetime"].ToString());
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
	}
}

