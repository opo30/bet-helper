using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeoWebSite.DBUtility;
using System.Data.SqlClient;
using System.Data;//Please add references

namespace SeoWebSite.DAL
{
    /// <summary>
    /// 数据访问类:ScheduleAnalysis
    /// </summary>
    public partial class ScheduleAnalysisDAO
    {
        public ScheduleAnalysisDAO()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "ScheduleAnalysis");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ScheduleAnalysis");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SeoWebSite.Model.ScheduleAnalysis model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ScheduleAnalysis(");
            strSql.Append("scheduleid,oddswin,oddsdraw,oddslost,perwin,perdraw,perlost,time)");
            strSql.Append(" values (");
            strSql.Append("@scheduleid,@oddswin,@oddsdraw,@oddslost,@perwin,@perdraw,@perlost,@time)");
            SqlParameter[] parameters = {
					new SqlParameter("@scheduleid", SqlDbType.Int,4),
					new SqlParameter("@oddswin", SqlDbType.Float,8),
					new SqlParameter("@oddsdraw", SqlDbType.Float,8),
					new SqlParameter("@oddslost", SqlDbType.Float,8),
					new SqlParameter("@perwin", SqlDbType.Float,8),
					new SqlParameter("@perdraw", SqlDbType.Float,8),
					new SqlParameter("@perlost", SqlDbType.Float,8),
					new SqlParameter("@time", SqlDbType.DateTime)};
            parameters[0].Value = model.scheduleid;
            parameters[1].Value = model.oddswin;
            parameters[2].Value = model.oddsdraw;
            parameters[3].Value = model.oddslost;
            parameters[4].Value = model.perwin;
            parameters[5].Value = model.perdraw;
            parameters[6].Value = model.perlost;
            parameters[7].Value = model.time;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SeoWebSite.Model.ScheduleAnalysis model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ScheduleAnalysis set ");
            strSql.Append("scheduleid=@scheduleid,");
            strSql.Append("oddswin=@oddswin,");
            strSql.Append("oddsdraw=@oddsdraw,");
            strSql.Append("oddslost=@oddslost,");
            strSql.Append("perwin=@perwin,");
            strSql.Append("perdraw=@perdraw,");
            strSql.Append("perlost=@perlost,");
            strSql.Append("totalCount=@totalCount,");
            strSql.Append("time=@time");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@scheduleid", SqlDbType.Int,4),
					new SqlParameter("@oddswin", SqlDbType.Float,8),
					new SqlParameter("@oddsdraw", SqlDbType.Float,8),
					new SqlParameter("@oddslost", SqlDbType.Float,8),
					new SqlParameter("@perwin", SqlDbType.Float,8),
					new SqlParameter("@perdraw", SqlDbType.Float,8),
					new SqlParameter("@perlost", SqlDbType.Float,8),
					new SqlParameter("@totalCount", SqlDbType.Int,4),
					new SqlParameter("@time", SqlDbType.DateTime),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.scheduleid;
            parameters[1].Value = model.oddswin;
            parameters[2].Value = model.oddsdraw;
            parameters[3].Value = model.oddslost;
            parameters[4].Value = model.perwin;
            parameters[5].Value = model.perdraw;
            parameters[6].Value = model.perlost;
            parameters[7].Value = model.totalCount;
            parameters[8].Value = model.time;
            parameters[9].Value = model.id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ScheduleAnalysis ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ScheduleAnalysis ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SeoWebSite.Model.ScheduleAnalysis GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,scheduleid,oddswin,oddsdraw,oddslost,perwin,perdraw,perlost,totalCount,time from ScheduleAnalysis ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            SeoWebSite.Model.ScheduleAnalysis model = new SeoWebSite.Model.ScheduleAnalysis();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["scheduleid"] != null && ds.Tables[0].Rows[0]["scheduleid"].ToString() != "")
                {
                    model.scheduleid = int.Parse(ds.Tables[0].Rows[0]["scheduleid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["oddswin"] != null && ds.Tables[0].Rows[0]["oddswin"].ToString() != "")
                {
                    model.oddswin = decimal.Parse(ds.Tables[0].Rows[0]["oddswin"].ToString());
                }
                if (ds.Tables[0].Rows[0]["oddsdraw"] != null && ds.Tables[0].Rows[0]["oddsdraw"].ToString() != "")
                {
                    model.oddsdraw = decimal.Parse(ds.Tables[0].Rows[0]["oddsdraw"].ToString());
                }
                if (ds.Tables[0].Rows[0]["oddslost"] != null && ds.Tables[0].Rows[0]["oddslost"].ToString() != "")
                {
                    model.oddslost = decimal.Parse(ds.Tables[0].Rows[0]["oddslost"].ToString());
                }
                if (ds.Tables[0].Rows[0]["perwin"] != null && ds.Tables[0].Rows[0]["perwin"].ToString() != "")
                {
                    model.perwin = decimal.Parse(ds.Tables[0].Rows[0]["perwin"].ToString());
                }
                if (ds.Tables[0].Rows[0]["perdraw"] != null && ds.Tables[0].Rows[0]["perdraw"].ToString() != "")
                {
                    model.perdraw = decimal.Parse(ds.Tables[0].Rows[0]["perdraw"].ToString());
                }
                if (ds.Tables[0].Rows[0]["perlost"] != null && ds.Tables[0].Rows[0]["perlost"].ToString() != "")
                {
                    model.perlost = decimal.Parse(ds.Tables[0].Rows[0]["perlost"].ToString());
                }
                if (ds.Tables[0].Rows[0]["totalCount"] != null && ds.Tables[0].Rows[0]["totalCount"].ToString() != "")
                {
                    model.totalCount = int.Parse(ds.Tables[0].Rows[0]["totalCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["time"] != null && ds.Tables[0].Rows[0]["time"].ToString() != "")
                {
                    model.time = DateTime.Parse(ds.Tables[0].Rows[0]["time"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,scheduleid,oddswin,oddsdraw,oddslost,perwin,perdraw,perlost,time");
            strSql.Append(" FROM ScheduleAnalysis ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), 999);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,scheduleid,oddswin,oddsdraw,oddslost,perwin,perdraw,perlost,totalCount,time ");
            strSql.Append(" FROM ScheduleAnalysis ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "ScheduleAnalysis";
            parameters[1].Value = "id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method

        public bool Exists(Model.ScheduleAnalysis model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ScheduleAnalysis");
            strSql.Append(" where scheduleid=@scheduleid");
            strSql.Append(" and perwin=@perwin and perdraw=@perdraw and perlost=@perlost");
            SqlParameter[] parameters = {
					new SqlParameter("@scheduleid", SqlDbType.Int),
                    new SqlParameter("@perwin", SqlDbType.Decimal),
                    new SqlParameter("@perdraw", SqlDbType.Decimal),
                    new SqlParameter("@perlost", SqlDbType.Decimal)};
            parameters[0].Value = model.scheduleid;
            parameters[1].Value = model.perwin;
            parameters[2].Value = model.perdraw;
            parameters[3].Value = model.perlost;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
    }
}
