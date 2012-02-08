using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeoWebSite.Common;
using System.Data;

namespace SeoWebSite.BLL
{
    /// <summary>
    /// ScheduleAnalysis
    /// </summary>
    public partial class ScheduleAnalysisBLL
    {
        private readonly SeoWebSite.DAL.ScheduleAnalysisDAO dal = new SeoWebSite.DAL.ScheduleAnalysisDAO();
        public ScheduleAnalysisBLL()
        { }
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
        public void Add(SeoWebSite.Model.ScheduleAnalysis model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SeoWebSite.Model.ScheduleAnalysis model)
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
        public bool DeleteList(string idlist)
        {
            return dal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SeoWebSite.Model.ScheduleAnalysis GetModel(int id)
        {

            return dal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public SeoWebSite.Model.ScheduleAnalysis GetModelByCache(int id)
        {

            string CacheKey = "ScheduleAnalysisModel-" + id;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(id);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (SeoWebSite.Model.ScheduleAnalysis)objModel;
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
        public List<SeoWebSite.Model.ScheduleAnalysis> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SeoWebSite.Model.ScheduleAnalysis> DataTableToList(DataTable dt)
        {
            List<SeoWebSite.Model.ScheduleAnalysis> modelList = new List<SeoWebSite.Model.ScheduleAnalysis>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SeoWebSite.Model.ScheduleAnalysis model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SeoWebSite.Model.ScheduleAnalysis();
                    if (dt.Rows[n]["id"] != null && dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    if (dt.Rows[n]["scheduleid"] != null && dt.Rows[n]["scheduleid"].ToString() != "")
                    {
                        model.scheduleid = int.Parse(dt.Rows[n]["scheduleid"].ToString());
                    }
                    if (dt.Rows[n]["oddswin"] != null && dt.Rows[n]["oddswin"].ToString() != "")
                    {
                        model.oddswin = decimal.Parse(dt.Rows[n]["oddswin"].ToString());
                    }
                    if (dt.Rows[n]["oddsdraw"] != null && dt.Rows[n]["oddsdraw"].ToString() != "")
                    {
                        model.oddsdraw = decimal.Parse(dt.Rows[n]["oddsdraw"].ToString());
                    }
                    if (dt.Rows[n]["oddslost"] != null && dt.Rows[n]["oddslost"].ToString() != "")
                    {
                        model.oddslost = decimal.Parse(dt.Rows[n]["oddslost"].ToString());
                    }
                    if (dt.Rows[n]["perwin"] != null && dt.Rows[n]["perwin"].ToString() != "")
                    {
                        model.perwin = decimal.Parse(dt.Rows[n]["perwin"].ToString());
                    }
                    if (dt.Rows[n]["perdraw"] != null && dt.Rows[n]["perdraw"].ToString() != "")
                    {
                        model.perdraw = decimal.Parse(dt.Rows[n]["perdraw"].ToString());
                    }
                    if (dt.Rows[n]["perlost"] != null && dt.Rows[n]["perlost"].ToString() != "")
                    {
                        model.perlost = decimal.Parse(dt.Rows[n]["perlost"].ToString());
                    }
                    if (dt.Rows[n]["totalCount"] != null && dt.Rows[n]["totalCount"].ToString() != "")
                    {
                        model.totalCount = int.Parse(dt.Rows[n]["totalCount"].ToString());
                    }
                    if (dt.Rows[n]["time"] != null && dt.Rows[n]["time"].ToString() != "")
                    {
                        model.time = DateTime.Parse(dt.Rows[n]["time"].ToString());
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

        public bool Exists(Model.ScheduleAnalysis model)
        {
            return dal.Exists(model);
        }
    }
}
