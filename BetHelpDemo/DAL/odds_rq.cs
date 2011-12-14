using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SeoWebSite.DBUtility;
using System.Collections.Generic;//Please add references
namespace SeoWebSite.DAL
{
    /// <summary>
    /// 数据访问类:odds_rq
    /// </summary>
    public class odds_rq
    {
        public odds_rq()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "odds_rq");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from odds_rq");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SeoWebSite.Model.odds_rq model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into odds_rq(");
            strSql.Append("companyID,scheduleID,pankou,home,away,time)");
            strSql.Append(" values (");
            strSql.Append("@companyID,@scheduleID,@pankou,@home,@away,@time)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@companyID", SqlDbType.Int,4),
					new SqlParameter("@scheduleID", SqlDbType.Int,4),
					new SqlParameter("@pankou", SqlDbType.Float,8),
					new SqlParameter("@home", SqlDbType.Float,8),
					new SqlParameter("@away", SqlDbType.Float,8),
					new SqlParameter("@time", SqlDbType.DateTime)};
            parameters[0].Value = model.companyID;
            parameters[1].Value = model.scheduleID;
            parameters[2].Value = model.pankou;
            parameters[3].Value = model.home;
            parameters[4].Value = model.away;
            parameters[5].Value = model.time;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SeoWebSite.Model.odds_rq model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update odds_rq set ");
            strSql.Append("companyID=@companyID,");
            strSql.Append("scheduleID=@scheduleID,");
            strSql.Append("pankou=@pankou,");
            strSql.Append("home=@home,");
            strSql.Append("away=@away,");
            strSql.Append("time=@time");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@companyID", SqlDbType.Int,4),
					new SqlParameter("@scheduleID", SqlDbType.Int,4),
					new SqlParameter("@pankou", SqlDbType.Float,8),
					new SqlParameter("@home", SqlDbType.Float,8),
					new SqlParameter("@away", SqlDbType.Float,8),
					new SqlParameter("@time", SqlDbType.DateTime)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.companyID;
            parameters[2].Value = model.scheduleID;
            parameters[3].Value = model.pankou;
            parameters[4].Value = model.home;
            parameters[5].Value = model.away;
            parameters[6].Value = model.time;

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
            strSql.Append("delete from odds_rq ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
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
        /// 删除某公司某比赛的赔率
        /// </summary>
        public void Delete(string companyid, string scheduleid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from odds_rq ");
            strSql.Append(" where companyid=@companyid and scheduleid=@scheduleid");
            SqlParameter[] parameters = {
					new SqlParameter("@companyid", SqlDbType.Int),
                    new SqlParameter("@scheduleid", SqlDbType.Int)};
            parameters[0].Value = companyid;
            parameters[1].Value = scheduleid;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from odds_rq ");
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
        public SeoWebSite.Model.odds_rq GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,companyID,scheduleID,pankou,home,away,time from odds_rq ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
            parameters[0].Value = id;

            SeoWebSite.Model.odds_rq model = new SeoWebSite.Model.odds_rq();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["companyID"].ToString() != "")
                {
                    model.companyID = int.Parse(ds.Tables[0].Rows[0]["companyID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["scheduleID"].ToString() != "")
                {
                    model.scheduleID = int.Parse(ds.Tables[0].Rows[0]["scheduleID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["pankou"].ToString() != "")
                {
                    model.pankou = decimal.Parse(ds.Tables[0].Rows[0]["pankou"].ToString());
                }
                if (ds.Tables[0].Rows[0]["home"].ToString() != "")
                {
                    model.home = decimal.Parse(ds.Tables[0].Rows[0]["home"].ToString());
                }
                if (ds.Tables[0].Rows[0]["away"].ToString() != "")
                {
                    model.away = decimal.Parse(ds.Tables[0].Rows[0]["away"].ToString());
                }
                if (ds.Tables[0].Rows[0]["time"].ToString() != "")
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
            strSql.Append("select id,companyID,scheduleID,pankou,home,away,time ");
            strSql.Append(" FROM odds_rq ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
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
            strSql.Append(" id,companyID,scheduleID,pankou,home,away,time ");
            strSql.Append(" FROM odds_rq ");
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
            parameters[0].Value = "odds_rq";
            parameters[1].Value = "";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method


        public DataSet GetList(string strWhere, int start, int end)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("WITH TT AS (SELECT odds_rq.*,Schedule.data,ROW_NUMBER() OVER (order by odds_rq.id)as RowNumber FROM odds_rq join schedule on odds_rq.scheduleid=schedule.id");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(") SELECT * FROM TT WHERE RowNumber between " + start + " and " + end);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public int GetTotalCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(id) from odds_rq");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return int.Parse(DbHelperSQL.Query(strSql.ToString()).Tables[0].Rows[0][0].ToString());
        }

        public DataSet GetList(string companyid, string[] oddsArray)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select e.id, g.id scheduleid, g.data,g.date, g.updated, g.home, g.away, g.halfhome, g.halfaway,e.scheduleID,e.companyID, e.time time1,e.home home1,e.pankou pankou1,e.away away1,f.time time2,f.home home2,f.pankou pankou2,f.away away2 from ");
            strSql.Append("tempdb..TempTable_Companys_Chupan_RQ e join tempdb..TempTable_Companys_Chupan_RQ f on e.scheduleID= f.scheduleID and e.companyID=f.companyID");
            strSql.Append(" join Schedule g on e.scheduleID=g.id and g.updated=1");
            if (!string.IsNullOrEmpty(oddsArray[0]))
            {
                strSql.Append(" AND e.home = " + oddsArray[0]);
            }
            if (!string.IsNullOrEmpty(oddsArray[1]))
            {
                strSql.Append(" AND e.pankou = " + oddsArray[1]);
            }
            if (!string.IsNullOrEmpty(oddsArray[2]))
            {
                strSql.Append(" AND e.away = " + oddsArray[2]);
            }
            if (!string.IsNullOrEmpty(oddsArray[3]))
            {
                strSql.Append(" AND f.home = " + oddsArray[3]);
            }
            if (!string.IsNullOrEmpty(oddsArray[4]))
            {
                strSql.Append(" AND f.pankou = " + oddsArray[4]);
            }
            if (!string.IsNullOrEmpty(oddsArray[5]))
            {
                strSql.Append(" AND f.away = " + oddsArray[5]);
            }

            if (!string.IsNullOrEmpty(companyid))
            {
                strSql.Append(" and e.companyID= " + companyid);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetList(string companyid, string tt, string[] oddsArray)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT c.id, c.data, c.updated, c.date, c.home, c.away, c.halfhome, c.halfaway, c.scheduleTypeID, a.companyID, a.time time1,a.home home1,a.pankou pankou1,a.away away1 FROM odds_rq AS a INNER JOIN (SELECT   companyID, scheduleID, " + tt + "(id) AS Expr1 FROM odds_rq GROUP BY companyID, scheduleID) AS b ON a.scheduleID = b.scheduleID AND a.companyID = b.companyID AND a.id = b.Expr1");
            if (!string.IsNullOrEmpty(oddsArray[0]))
            {
                strSql.Append(" AND a.home = " + oddsArray[0]);
            }
            if (!string.IsNullOrEmpty(oddsArray[1]))
            {
                strSql.Append(" AND a.pankou = " + oddsArray[1]);
            }
            if (!string.IsNullOrEmpty(oddsArray[2]))
            {
                strSql.Append(" AND a.away = " + oddsArray[2]);
            }
            if (!string.IsNullOrEmpty(companyid))
            {
                strSql.Append(" and a.companyID= " + companyid);
            }
            strSql.Append(" join schedule c on c.id=a.scheduleid and c.updated=1");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查询让球盘统计
        /// </summary>
        /// <param name="oddsList">赔率信息列表</param>
        /// <param name="blur">模糊值</param>
        /// <param name="scheduleTypeID">比赛类别ID</param>
        /// <returns>数据集</returns>
        public DataSet queryOddsCount(List<string> oddsList, float blur, int scheduleTypeID, int scheduleid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append("rqy=sum(case when g.home-g.away > e.pankou then 1 else 0 end),");
            strSql.Append("rqz=sum(case when g.home-g.away = e.pankou then 1 else 0 end),");
            strSql.Append("rqs=sum(case when g.home-g.away < e.pankou then 1 else 0 end),");
            strSql.Append("count(*) totalCount from");
            strSql.Append(" tempdb..TempTable_Companys_Chupan_RQ e join tempdb..TempTable_Companys_Zhongpan_RQ f");
            strSql.Append(" on e.scheduleID= f.scheduleID and e.companyID=f.companyID AND (");
            for (int i = 0; i < oddsList.Count; i++)
            {
                string[] oddsArray = oddsList[i].Split(',');
                if (i > 0)
                {
                    strSql.Append(" or ");
                }
                //初盘匹配
                strSql.Append(" (e.home = " + oddsArray[0]);
                strSql.Append(" AND e.away = " + oddsArray[2]);
                strSql.Append(" AND e.pankou = " + oddsArray[1]);
                //临场盘匹配
                strSql.Append(" AND f.home >= " + (float.Parse(oddsArray[3]) - blur) + " and f.home <= " + (float.Parse(oddsArray[3]) + blur));
                strSql.Append(" AND f.away >= " + (float.Parse(oddsArray[5]) - blur) + " and f.away <= " + (float.Parse(oddsArray[5]) + blur));
                strSql.Append(" AND f.pankou = " + oddsArray[4]);
                //公司匹配
                strSql.Append(" AND e.companyID = " + oddsArray[6] + ")");
            }
            strSql.Append(") join Schedule g on e.scheduleID=g.id and g.updated=1 and g.id<>" + scheduleid);
            if (scheduleTypeID != 0)
            {
                strSql.Append(" and g.scheduleTypeID=" + scheduleTypeID);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public void CreateTempTable()
        {
            if (!DbHelperSQL.TempTabExsits("TempTable_Companys_Chupan_RQ"))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT a.id, a.companyID, a.scheduleID, a.home, a.pankou, a.away, a.time into tempdb..TempTable_Companys_Chupan_RQ ");
                strSql.Append("FROM dbo.odds_rq AS a INNER JOIN ");
                strSql.Append("(SELECT MAX(id) AS Expr1, scheduleID FROM dbo.odds_rq GROUP BY scheduleID, companyID) AS b ");
                strSql.Append("ON a.id = b.Expr1 ");
                DbHelperSQL.ExecuteSqlByTime(strSql.ToString(), 180);
            }
            if (!DbHelperSQL.TempTabExsits("TempTable_Companys_Zhongpan_RQ"))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT c.id, c.companyID, c.scheduleID, c.home, c.pankou, c.away, c.time into tempdb..TempTable_Companys_Zhongpan_RQ ");
                strSql.Append("FROM dbo.odds_rq AS c INNER JOIN ");
                strSql.Append("(SELECT MIN(id) AS Expr1, scheduleID FROM dbo.odds_rq AS odds_rq_1 GROUP BY scheduleID, companyID) AS d ");
                strSql.Append("ON c.id = d.Expr1");
                DbHelperSQL.ExecuteSqlByTime(strSql.ToString(), 180);
            }

        }

        public DataSet queryCompanyOddsCount(string companyid, string home, string pankou, string away, int scheduleTypeID, string ptime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append("rqy=sum(case when g.home-g.away > e.pankou then 1 else 0 end),");
            strSql.Append("rqz=sum(case when g.home-g.away = e.pankou then 1 else 0 end),");
            strSql.Append("rqs=sum(case when g.home-g.away < e.pankou then 1 else 0 end),");
            strSql.Append("count(*) totalCount from");
            strSql.Append(" tempdb..TempTable_Companys_" + ptime + "_RQ e join Schedule g on e.scheduleID=g.id and g.updated=1");
            strSql.Append(" where e.home=@home and e.pankou=@pankou and e.away=@away and companyID=@companyID");
            if (scheduleTypeID != 0)
            {
                strSql.Append(" and g.scheduleTypeID=" + scheduleTypeID);
            }
            SqlParameter[] parameters = {
                       new SqlParameter("@home", SqlDbType.Float),
                       new SqlParameter("@pankou", SqlDbType.Float),
                       new SqlParameter("@away", SqlDbType.Float),
					new SqlParameter("@companyID", SqlDbType.Int)};
            parameters[0].Value = home;
            parameters[1].Value = pankou;
            parameters[2].Value = away;
            parameters[3].Value = companyid;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        public DataTable queryCompanyOddsPer(List<string> oddsList, int scheduleTypeID, int scheduleID)
        {
            List<string> sqlList = new List<string>();
            DataTable dt = new DataTable();
            dt.Columns.Add("rqy", typeof(double));
            dt.Columns.Add("rqz", typeof(double));
            dt.Columns.Add("rqs", typeof(double));
            dt.Columns.Add("totalCount", typeof(int));

            for (int i = 0; i < oddsList.Count; i++)
            {
                string[] oddsArray = oddsList[i].Split(',');
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select ");
                strSql.Append("rqy=100.0 * sum(case when g.home-g.away > e.pankou then 1 else 0 end) / count(*),");
                strSql.Append("rqz=100.0 * sum(case when g.home-g.away = e.pankou then 1 else 0 end) / count(*),");
                strSql.Append("rqs=100.0 * sum(case when g.home-g.away < e.pankou then 1 else 0 end) / count(*),");
                strSql.Append("count(*) totalCount from");
                strSql.Append(" tempdb..TempTable_Companys_Chupan_RQ e join Schedule g on e.scheduleID=g.id and g.updated=1");
                strSql.Append(" where e.home=" + oddsArray[0] + " and e.pankou=" + oddsArray[1] + " and e.away=" + oddsArray[2] + " and companyID=" + oddsArray[6] + " and g.id<>" + scheduleID);
                if (scheduleTypeID != 0)
                {
                    strSql.Append(" and g.scheduleTypeID=" + scheduleTypeID);
                }
                sqlList.Add(strSql.ToString());
                strSql = new StringBuilder();
                strSql.Append("select ");
                strSql.Append("rqy=100.0 * sum(case when g.home-g.away > e.pankou then 1 else 0 end) / count(*),");
                strSql.Append("rqz=100.0 * sum(case when g.home-g.away = e.pankou then 1 else 0 end) / count(*),");
                strSql.Append("rqs=100.0 * sum(case when g.home-g.away < e.pankou then 1 else 0 end) / count(*),");
                strSql.Append("count(*) totalCount from");
                strSql.Append(" tempdb..TempTable_Companys_Zhongpan_RQ e join Schedule g on e.scheduleID=g.id and g.updated=1");
                strSql.Append(" where e.home=" + oddsArray[3] + " and e.pankou=" + oddsArray[4] + " and e.away=" + oddsArray[5] + " and companyID=" + oddsArray[6] + " and g.id<>" + scheduleID);
                if (scheduleTypeID != 0)
                {
                    strSql.Append(" and g.scheduleTypeID=" + scheduleTypeID);
                }
                sqlList.Add(strSql.ToString());
            }
            foreach (string sqlStr in sqlList)
            {
                DataSet ds = DbHelperSQL.Query(sqlStr);
                if (ds.Tables[0].Rows.Count > 0 && Convert.ToInt32(ds.Tables[0].Rows[0]["totalCount"]) > 0)
                {
                    dt.ImportRow(ds.Tables[0].Rows[0]);
                }
            }
            return dt;
        }

        public DataTable queryOddsPer(List<string> oddsList, float blur, int scheduleTypeID, int scheduleid)
        {
            List<string> sqlList = new List<string>();
            DataTable dt = new DataTable();
            dt.Columns.Add("rqy", typeof(double));
            dt.Columns.Add("rqz", typeof(double));
            dt.Columns.Add("rqs", typeof(double));
            dt.Columns.Add("totalCount", typeof(int));
            for (int i = 0; i < oddsList.Count; i++)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select ");
                strSql.Append("rqy=100.0 * sum(case when g.home-g.away > e.pankou then 1 else 0 end) / count(*),");
                strSql.Append("rqz=100.0 * sum(case when g.home-g.away = e.pankou then 1 else 0 end) / count(*),");
                strSql.Append("rqs=100.0 * sum(case when g.home-g.away < e.pankou then 1 else 0 end) / count(*),");
                strSql.Append("count(*) totalCount from");
                strSql.Append(" tempdb..TempTable_Companys_Chupan_RQ e join tempdb..TempTable_Companys_Zhongpan_RQ f");
                strSql.Append(" on e.scheduleID= f.scheduleID and e.companyID=f.companyID AND (");

                string[] oddsArray = oddsList[i].Split(',');
                //初盘匹配
                strSql.Append(" (e.home = " + oddsArray[0]);
                strSql.Append(" AND e.away = " + oddsArray[2]);
                strSql.Append(" AND e.pankou = " + oddsArray[1]);
                //临场盘匹配
                strSql.Append(" AND f.home >= " + (float.Parse(oddsArray[3]) - blur) + " and f.home <= " + (float.Parse(oddsArray[3]) + blur));
                strSql.Append(" AND f.away >= " + (float.Parse(oddsArray[5]) - blur) + " and f.away <= " + (float.Parse(oddsArray[5]) + blur));
                strSql.Append(" AND f.pankou = " + oddsArray[4]);
                //公司匹配
                strSql.Append(" AND e.companyID = " + oddsArray[6] + ")");
                strSql.Append(") join Schedule g on e.scheduleID=g.id and g.updated=1 and g.id<>" + scheduleid);
                if (scheduleTypeID != 0)
                {
                    strSql.Append(" and g.scheduleTypeID=" + scheduleTypeID);
                }
                sqlList.Add(strSql.ToString());
            }
            foreach (string sqlStr in sqlList)
            {
                DataSet ds = DbHelperSQL.Query(sqlStr);
                if (ds.Tables[0].Rows.Count > 0 && Convert.ToInt32(ds.Tables[0].Rows[0]["totalCount"]) > 0)
                {
                    dt.ImportRow(ds.Tables[0].Rows[0]);
                }
            }
            return dt;
        }

        public DataTable queryCompanyOddsPer1(int scheduleTypeID, int scheduleID, List<int> companyidList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("rqy", typeof(double));
            dt.Columns.Add("rqz", typeof(double));
            dt.Columns.Add("rqs", typeof(double));
            dt.Columns.Add("totalCount", typeof(int));
            foreach (int companyid in companyidList)
            {
                DataSet ds = DbHelperSQL.Query("select * from odds_rq where companyid = " + companyid + " and scheduleid=" + scheduleID);
                StringBuilder strSql = new StringBuilder();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<string> filterList = new List<string>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        filterList.Add("e.home=" + dr["home"] + " and e.pankou=" + dr["pankou"] + " and e.away=" + dr["away"]);
                    }
                    strSql.Append("select ");
                    strSql.Append("rqy=100.0 * sum(case when g.home-g.away > e.pankou then 1 else 0 end) / count(*),");
                    strSql.Append("rqz=100.0 * sum(case when g.home-g.away = e.pankou then 1 else 0 end) / count(*),");
                    strSql.Append("rqs=100.0 * sum(case when g.home-g.away < e.pankou then 1 else 0 end) / count(*),");
                    strSql.Append("count(*) totalCount from");
                    strSql.Append(" odds_rq e join Schedule g on e.scheduleID=g.id and g.updated=1");
                    strSql.Append(" where companyID=" + companyid + " and g.id<>" + scheduleID + " and ");
                    if (scheduleTypeID != 0)
                    {
                        strSql.Append(" g.scheduleTypeID=" + scheduleTypeID + " and ");
                    }
                    strSql.Append("(" + string.Join(" or ", filterList.ToArray()) + ")");

                    DataSet ds1 = DbHelperSQL.Query(strSql.ToString());
                    if (ds1.Tables[0].Rows.Count > 0 && Convert.ToInt32(ds1.Tables[0].Rows[0]["totalCount"]) > 0)
                    {
                        dt.ImportRow(ds1.Tables[0].Rows[0]);
                    }
                }
            }
            return dt;
        }

        public DataTable queryCompanyOddsPer1(int scheduleTypeID, int scheduleID, int companyid, List<string> whereStrList)
        {
            StringBuilder strSql = new StringBuilder();
            List<string> filterList = new List<string>();
            strSql.Append("select ");
            strSql.Append("rqy=100.0 * sum(case when g.home-g.away > e.pankou then 1 else 0 end) / count(*),");
            strSql.Append("rqz=100.0 * sum(case when g.home-g.away = e.pankou then 1 else 0 end) / count(*),");
            strSql.Append("rqs=100.0 * sum(case when g.home-g.away < e.pankou then 1 else 0 end) / count(*),");
            strSql.Append("count(*) totalCount from");
            strSql.Append(" odds_rq e join Schedule g on e.scheduleID=g.id and g.updated=1");
            strSql.Append(" where companyID=" + companyid + " and g.id<>" + scheduleID + " and");
            if (scheduleTypeID != 0)
            {
                strSql.Append(" g.scheduleTypeID=" + scheduleTypeID + " and ");
            }
            strSql.Append(" (" + string.Join(" or ", whereStrList.ToArray()) + ")");

            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }
    }
}

