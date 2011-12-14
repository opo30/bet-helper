using System;
using System.Data;
using System.Collections.Generic;
using SeoWebSite.Common;
using SeoWebSite.Model;
namespace SeoWebSite.BLL
{
	/// <summary>
	/// ҵ���߼���betuser ��ժҪ˵����
	/// </summary>
	public class BetUserBLL
	{
		private readonly SeoWebSite.DAL.BetUserDAO dal=new SeoWebSite.DAL.BetUserDAO();
		public BetUserBLL()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(string id)
		{
			return dal.Exists(id);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(SeoWebSite.Model.BetUser model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(SeoWebSite.Model.BetUser model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string id)
		{
			
			dal.Delete(id);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public SeoWebSite.Model.BetUser GetModel(string id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public SeoWebSite.Model.BetUser GetModelByCache(string id)
		{
			
			string CacheKey = "betuserModel-" + id;
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
			return (SeoWebSite.Model.BetUser)objModel;
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
		public List<SeoWebSite.Model.BetUser> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<SeoWebSite.Model.BetUser> DataTableToList(DataTable dt)
		{
			List<SeoWebSite.Model.BetUser> modelList = new List<SeoWebSite.Model.BetUser>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SeoWebSite.Model.BetUser model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SeoWebSite.Model.BetUser();
					model.id=dt.Rows[n]["id"].ToString();
					model.username=dt.Rows[n]["username"].ToString();
					model.password=dt.Rows[n]["password"].ToString();
					model.truename=dt.Rows[n]["truename"].ToString();
					if(dt.Rows[n]["age"].ToString()!="")
					{
						model.age=int.Parse(dt.Rows[n]["age"].ToString());
					}
					model.sex=dt.Rows[n]["sex"].ToString();
					model.qq=dt.Rows[n]["qq"].ToString();
					model.mobile=dt.Rows[n]["mobile"].ToString();
					model.email=dt.Rows[n]["email"].ToString();
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
	}
}

