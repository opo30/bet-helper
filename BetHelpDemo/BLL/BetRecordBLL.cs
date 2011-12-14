using System;
using System.Data;
using System.Collections.Generic;
using SeoWebSite.Common;
using SeoWebSite.Model;
using SeoWebSite.Common.JSON;
namespace SeoWebSite.BLL
{
	/// <summary>
	/// ҵ���߼���betrecord ��ժҪ˵����
	/// </summary>
	public class BetRecordBLL
	{
		private readonly SeoWebSite.DAL.BetRecordDAO dal=new SeoWebSite.DAL.BetRecordDAO();
        private DataSet ds;
		public BetRecordBLL()
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
		public void Add(SeoWebSite.Model.BetRecord model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(SeoWebSite.Model.BetRecord model)
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
		public SeoWebSite.Model.BetRecord GetModel(string id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public SeoWebSite.Model.BetRecord GetModelByCache(string id)
		{
			
			string CacheKey = "betrecordModel-" + id;
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
			return (SeoWebSite.Model.BetRecord)objModel;
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
		public List<SeoWebSite.Model.BetRecord> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<SeoWebSite.Model.BetRecord> DataTableToList(DataTable dt)
		{
			List<SeoWebSite.Model.BetRecord> modelList = new List<SeoWebSite.Model.BetRecord>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SeoWebSite.Model.BetRecord model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SeoWebSite.Model.BetRecord();
					model.id=dt.Rows[n]["id"].ToString();
					model.lineid=dt.Rows[n]["lineid"].ToString();
					model.teamname=dt.Rows[n]["teamname"].ToString();
					model.traditional=dt.Rows[n]["traditional"].ToString();
					if(dt.Rows[n]["starttime"].ToString()!="")
					{
						model.starttime=DateTime.Parse(dt.Rows[n]["starttime"].ToString());
					}
					if(dt.Rows[n]["endtime"].ToString()!="")
					{
						model.endtime=DateTime.Parse(dt.Rows[n]["endtime"].ToString());
					}
					if(dt.Rows[n]["bettime"].ToString()!="")
					{
						model.bettime=DateTime.Parse(dt.Rows[n]["bettime"].ToString());
					}
					model.itemid=dt.Rows[n]["itemid"].ToString();
					model.detailid=dt.Rows[n]["detailid"].ToString();
					if(dt.Rows[n]["betmoney"].ToString()!="")
					{
						model.betmoney=decimal.Parse(dt.Rows[n]["betmoney"].ToString());
					}
					if(dt.Rows[n]["returnmoney"].ToString()!="")
					{
						model.returnmoney=decimal.Parse(dt.Rows[n]["returnmoney"].ToString());
					}
					model.result=dt.Rows[n]["result"].ToString();
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

        /// <summary>
        /// ��ѯĳ�û���Ͷע�б�
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetBetRecordList(string userid)
        {
            JSONHelper json = new JSONHelper();
            ds = dal.GetList(userid);
            json.success = true;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //Ͷע��¼������
                json.AddItem("id", dr["id"].ToString());
                json.AddItem("lineid", dr["lineid"].ToString());
                json.AddItem("teamname", dr["teamname"].ToString());
                json.AddItem("traditional", dr["traditional"].ToString());
                json.AddItem("starttime", dr["starttime"].ToString());
                json.AddItem("endtime", dr["endtime"].ToString());
                json.AddItem("bettime", dr["bettime"].ToString());
                json.AddItem("itemid", dr["itemid"].ToString());
                json.AddItem("detailid", dr["detailid"].ToString());
                json.AddItem("betmoney", dr["betmoney"].ToString());
                json.AddItem("returnmoney", dr["returnmoney"].ToString());
                json.AddItem("result", dr["result"].ToString());
                json.AddItem("sourceid", dr["sourceid"].ToString());
                //Ͷע�̱߳�����
                json.AddItem("linename", dr["linename"].ToString());
                json.AddItem("linebetmoney", dr["linebetmoney"].ToString());
                json.AddItem("profit", dr["profit"].ToString());
                json.AddItem("state", dr["state"].ToString());
                json.AddItem("iscomplete", dr["iscomplete"].ToString());
                json.AddItem("isbetting", dr["isbetting"].ToString());
                //Ͷע��Ŀ������
                json.AddItem("itemname", dr["itemname"].ToString());
                //Ͷעϸ������
                json.AddItem("detailname", dr["detailname"].ToString());

                json.ItemOk();
            }
            json.totlalCount = ds.Tables[0].Rows.Count;
            string betRecordlist = json.ToString();
            return betRecordlist;
        }
	}
}

