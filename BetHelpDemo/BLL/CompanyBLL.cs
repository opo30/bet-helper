using System;
using System.Data;
using System.Collections.Generic;
using SeoWebSite.Common;
using SeoWebSite.Model;
using SeoWebSite.DAL;
namespace SeoWebSite.BLL
{
	/// <summary>
	/// company
	/// </summary>
	public partial class CompanyBLL
	{
		private readonly CompanyDAO dal=new CompanyDAO();
		public CompanyBLL()
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
		public void Add(SeoWebSite.Model.Company model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SeoWebSite.Model.Company model)
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
		public SeoWebSite.Model.Company GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public SeoWebSite.Model.Company GetModelByCache(int id)
		{
			
			string CacheKey = "companyModel-" + id;
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
			return (SeoWebSite.Model.Company)objModel;
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
		public List<SeoWebSite.Model.Company> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SeoWebSite.Model.Company> DataTableToList(DataTable dt)
		{
			List<SeoWebSite.Model.Company> modelList = new List<SeoWebSite.Model.Company>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SeoWebSite.Model.Company model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SeoWebSite.Model.Company();
					if(dt.Rows[n]["id"]!=null && dt.Rows[n]["id"].ToString()!="")
					{
						model.id=int.Parse(dt.Rows[n]["id"].ToString());
					}
					if(dt.Rows[n]["fullname"]!=null && dt.Rows[n]["fullname"].ToString()!="")
					{
					model.fullname=dt.Rows[n]["fullname"].ToString();
					}
					if(dt.Rows[n]["name"]!=null && dt.Rows[n]["name"].ToString()!="")
					{
					model.name=dt.Rows[n]["name"].ToString();
					}
					if(dt.Rows[n]["isprimary"]!=null && dt.Rows[n]["isprimary"].ToString()!="")
					{
						if((dt.Rows[n]["isprimary"].ToString()=="1")||(dt.Rows[n]["isprimary"].ToString().ToLower()=="true"))
						{
						model.isprimary=true;
						}
						else
						{
							model.isprimary=false;
						}
					}
					if(dt.Rows[n]["isexchange"]!=null && dt.Rows[n]["isexchange"].ToString()!="")
					{
						if((dt.Rows[n]["isexchange"].ToString()=="1")||(dt.Rows[n]["isexchange"].ToString().ToLower()=="true"))
						{
						model.isexchange=true;
						}
						else
						{
							model.isexchange=false;
						}
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

