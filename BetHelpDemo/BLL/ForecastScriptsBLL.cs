using System;
using System.Data;
using System.Collections.Generic;
using SeoWebSite.Common;
using SeoWebSite.Model;
using SeoWebSite.DAL;
namespace SeoWebSite.BLL
{
    /// <summary>
    /// 业务逻辑类ForecastScriptsBLL 的摘要说明。
    /// </summary>
    public class ForecastScriptsBLL
    {
        private readonly SeoWebSite.DAL.ForecastScriptsDAO dal = new SeoWebSite.DAL.ForecastScriptsDAO();
        public ForecastScriptsBLL()
        { }
        #region  成员方法
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
        public int Add(SeoWebSite.Model.ForecastScripts model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SeoWebSite.Model.ForecastScripts model)
        {
            dal.Update(model);
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
        public SeoWebSite.Model.ForecastScripts GetModel(int id)
        {

            return dal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public SeoWebSite.Model.ForecastScripts GetModelByCache(int id)
        {

            string CacheKey = "ForecastScriptsBLLModel-" + id;
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
                catch { }
            }
            return (SeoWebSite.Model.ForecastScripts)objModel;
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SeoWebSite.Model.ForecastScripts> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SeoWebSite.Model.ForecastScripts> DataTableToList(DataTable dt)
        {
            List<SeoWebSite.Model.ForecastScripts> modelList = new List<SeoWebSite.Model.ForecastScripts>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SeoWebSite.Model.ForecastScripts model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SeoWebSite.Model.ForecastScripts();
                    if (dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    model.name = dt.Rows[n]["name"].ToString();
                    model.content = dt.Rows[n]["content"].ToString();
                    model.remark = dt.Rows[n]["remark"].ToString();
                    if (dt.Rows[n]["win"].ToString() != "")
                    {
                        model.win = int.Parse(dt.Rows[n]["win"].ToString());
                    }
                    if (dt.Rows[n]["lost"].ToString() != "")
                    {
                        model.lost = int.Parse(dt.Rows[n]["lost"].ToString());
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
        /// 获得数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Increase(int id, string win,string victory)
        {
            dal.Increase(id, win,victory);
        }
        #endregion  成员方法
    }
}

