using System;
using System.Data;
using System.Collections.Generic;
using SeoWebSite.Common;
using SeoWebSite.Model;
using System.Text.RegularExpressions;
namespace SeoWebSite.BLL
{
	/// <summary>
	/// 业务逻辑类betexp 的摘要说明。
	/// </summary>
	public class BetExpBLL
	{
		private readonly SeoWebSite.DAL.BetExpDAO dal=new SeoWebSite.DAL.BetExpDAO();
        public BetExpBLL()
		{}
		#region  成员方法

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
        /// 是否存在该记录
        /// </summary>
        public bool ExistsStatistics(int id)
        {
            return dal.ExistsStatistics(id);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SeoWebSite.Model.BetExp model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(SeoWebSite.Model.BetExp model)
		{
			dal.Update(model);
		}

        /// <summary>
        /// 更新一条数据的经验
        /// </summary>
        public void Update(int matchid,string exp)
        {
            dal.Update(matchid, exp);
        }

        /// <summary>
        /// 更新一条数据的分析
        /// </summary>
        public void UpdateAnalysis(int matchid, string a)
        {
            dal.UpdateAnalysis(matchid, a);
        }

        /// <summary>
        /// 更新一条数据的统计
        /// </summary>
        public void Update(int matchid, bool hasstatistics)
        {
            dal.Update(matchid, hasstatistics);
        }

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int id)
		{
			
			dal.Delete(id);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SeoWebSite.Model.BetExp GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public SeoWebSite.Model.BetExp GetModelByCache(int id)
		{
			
			string CacheKey = "betexpModel-" + id;
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
			return (SeoWebSite.Model.BetExp)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

        /// <summary>
        /// 获得数据数量
        /// </summary>
        public long GetListCount(string strWhere)
        {
            return dal.GetListCount(strWhere);
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
		public List<SeoWebSite.Model.BetExp> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SeoWebSite.Model.BetExp> DataTableToList(DataTable dt)
		{
			List<SeoWebSite.Model.BetExp> modelList = new List<SeoWebSite.Model.BetExp>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SeoWebSite.Model.BetExp model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SeoWebSite.Model.BetExp();
					if(dt.Rows[n]["id"].ToString()!="")
					{
						model.id=int.Parse(dt.Rows[n]["id"].ToString());
					}
					model.data=dt.Rows[n]["data"].ToString();
					if(dt.Rows[n]["isexp"].ToString()!="")
					{
						if((dt.Rows[n]["isexp"].ToString()=="1")||(dt.Rows[n]["isexp"].ToString().ToLower()=="true"))
						{
						model.isexp=true;
						}
						else
						{
							model.isexp=false;
						}
					}
					model.exp=dt.Rows[n]["exp"].ToString();
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
		/// 获得数据列表
		/// </summary>
        public DataSet GetList(int start, int end, string strWhere)
        {
            return dal.GetList(start, end, strWhere);
        }

		#endregion  成员方法

        public string GetIsExpByID(string matchid)
        {
            return dal.GetIsExpByID(matchid);
        }

        public DataSet GetListByAnalysis(string data)
        {
            return dal.GetListByAnalysis(data);
        }

        public int GetCountByAnalysis(string data)
        {
            return dal.GetCountByAnalysis(data);
        }

        public DataTable GetSimilarListByAnalysis(string data, bool isSpot)
        {
            //DataTable dt = new DataTable();
            //DataSet ds = dal.GetListByAnalysis(spot);
            //dt = ds.Tables[0].Clone();
            //string[] strArr = Regex.Split(data, ",Algorithm.", RegexOptions.IgnoreCase);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow item in ds.Tables[0].Rows)
            //    {
            //        string analysis = item["analysis"].ToString();
            //        int count = 0;
            //        foreach (string s in strArr)
            //        {
            //            if (analysis.Contains(s))
            //            {
            //                count++;
            //            }
            //        }
            //        if (count >= strArr.Length - 1)
            //        {
            //            dt.ImportRow(item);
            //        }
            //    }
            //}
            //return dt;
            DataTable dt = new DataTable();
            string whereStr = "";
            string[] strArr = Regex.Split(data, ",Algorithm.", RegexOptions.IgnoreCase);
            int count = (isSpot ? strArr.Length - 2 : strArr.Length);
            for (int i = 0; i < count; i++)
			{
                whereStr = "analysis like '" + data.Replace(strArr[i], "%") + "'";
                DataSet ds = dal.GetList(whereStr);
                if (dt.Columns.Count == 0)
                {
                    dt = ds.Tables[0].Clone();
                }
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    dt.ImportRow(item);
                }
			}
            return dt;
        }

        public DataSet GetNoAnalysisList(int recordCount)
        {
            return dal.GetNoAnalysisList(recordCount);
        }


        public DataSet GetListByTrends(string value,string changesValue)
        {
            return dal.GetListByTrends(value, changesValue);
        }

        public string GetTrendsValue(Newtonsoft.Json.Linq.JObject jObject, string mode)
        {
            float avehome = float.Parse(jObject["avehome"].ToString());
            float avedraw = float.Parse(jObject["avedraw"].ToString());
            float aveaway = float.Parse(jObject["aveaway"].ToString());
            float returnrate = float.Parse(jObject["returnrate"].ToString());
            List<float> avelist = new List<float>();
            avelist.Add(avehome); avelist.Add(avedraw); avelist.Add(aveaway); avelist.Add(returnrate);
            CommonHelper.bubbleUp(avelist);
            jObject["avehome"] = avelist.IndexOf(avehome);
            jObject["avedraw"] = avelist.IndexOf(avedraw);
            jObject["aveaway"] = avelist.IndexOf(aveaway);
            jObject["returnrate"] = avelist.IndexOf(returnrate);
            if (mode == "avekelly")
            {
                return avelist.IndexOf(avehome).ToString() + avelist.IndexOf(avedraw).ToString() + avelist.IndexOf(aveaway).ToString() + avelist.IndexOf(returnrate).ToString() + "|___";
            }
            else
            {
                List<float> varlist = new List<float>();
                float varhome = float.Parse(jObject["varhome"].ToString());
                float vardraw = float.Parse(jObject["vardraw"].ToString());
                float varaway = float.Parse(jObject["varaway"].ToString());
                varlist.Add(varhome); varlist.Add(vardraw); varlist.Add(varaway);
                CommonHelper.bubbleUp(varlist);
                return avelist.IndexOf(avehome).ToString() + avelist.IndexOf(avedraw).ToString() + avelist.IndexOf(aveaway).ToString() + avelist.IndexOf(returnrate).ToString() + "|" + varlist.IndexOf(varhome).ToString() + varlist.IndexOf(vardraw).ToString() + varlist.IndexOf(varaway).ToString();
            }
        }

        public string GetChangesValue(Newtonsoft.Json.Linq.JArray data, string mode)
        {
            string[] valueArr = new string[data.Count - 1];
            for (int i = 1; i < data.Count; i++)
            {
                valueArr[i - 1] = (float.Parse(data[i]["avehome"].ToString()) >= float.Parse(data[i - 1]["avehome"].ToString())) ? "1" : "0";
                valueArr[i - 1] += (float.Parse(data[i]["avedraw"].ToString()) >= float.Parse(data[i - 1]["avedraw"].ToString())) ? "1" : "0";
                valueArr[i - 1] += (float.Parse(data[i]["aveaway"].ToString()) >= float.Parse(data[i - 1]["aveaway"].ToString())) ? "1" : "0";
                valueArr[i - 1] += (float.Parse(data[i]["returnrate"].ToString()) >= float.Parse(data[i - 1]["returnrate"].ToString())) ? "1" : "0";
                valueArr[i - 1] += "|";
                if (mode == "avekelly")
                {
                    valueArr[i - 1] += "___";
                }
                else
                {
                    valueArr[i - 1] += (float.Parse(data[i]["varhome"].ToString()) >= float.Parse(data[i - 1]["varhome"].ToString())) ? "1" : "0";
                    valueArr[i - 1] += (float.Parse(data[i]["vardraw"].ToString()) >= float.Parse(data[i - 1]["vardraw"].ToString())) ? "1" : "0";
                    valueArr[i - 1] += (float.Parse(data[i]["varaway"].ToString()) >= float.Parse(data[i - 1]["varaway"].ToString())) ? "1" : "0";
                }
            }
            return string.Join(",", valueArr);
        }
    }
}

