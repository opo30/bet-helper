using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using SeoWebSite.DBUtility;
using SeoWebSite.Model;

namespace SeoWebSite.DAL
{
    public class OddsLiveDAO
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void AddMatch(SeoWebSite.Model.OddsLiveMatch model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into oddslive_match(");
            strSql.Append("id,name,urlparams,time)");
            strSql.Append(" values (");
            strSql.Append("@id,@name,@urlparams,@time)");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar),
					new SqlParameter("@name", SqlDbType.NVarChar),
					new SqlParameter("@urlparams", SqlDbType.VarChar),
                    new SqlParameter("@time", SqlDbType.DateTime)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.name;
            parameters[2].Value = model.urlparams;
            parameters[3].Value = model.time;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void AddHistory(SeoWebSite.Model.OddsLiveHistory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into oddslive_history(");
            strSql.Append("matchid,home,draw,away,time)");
            strSql.Append(" values (");
            strSql.Append("@matchid,@home,@draw,@away,@time)");
            SqlParameter[] parameters = {
					new SqlParameter("@matchid", SqlDbType.VarChar),
					new SqlParameter("@home", SqlDbType.Float),
					new SqlParameter("@draw", SqlDbType.Float),
					new SqlParameter("@away", SqlDbType.Float),
                    new SqlParameter("@time", SqlDbType.DateTime)};
            parameters[0].Value = model.matchid;
            parameters[1].Value = model.home;
            parameters[2].Value = model.draw;
            parameters[3].Value = model.away;
            parameters[4].Value = model.time;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM oddslive_match ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(string scheduleid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from odds1x2history");
            strSql.Append(" where scheduleid=@scheduleid ");
            SqlParameter[] parameters = {
					new SqlParameter("@scheduleid", SqlDbType.Int)};
            parameters[0].Value = scheduleid;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回某场比赛某公司的赔率变化列表
        /// </summary>
        public List<Odds1x2History> GetList(string scheduleid, string companyid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT id, scheduleid, companyid, home, draw, away, homep, drawp, awayp, returnrate, time FROM      odds1x2history");
            strSql.Append(" WHERE (companyid = @companyid) AND (scheduleid = @scheduleid) ORDER BY time DESC");
            SqlParameter[] parameters = {
					new SqlParameter("@scheduleid", SqlDbType.Int),
                    new SqlParameter("@companyid", SqlDbType.Int)};
            parameters[0].Value = scheduleid;
            parameters[1].Value = companyid;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            List<Odds1x2History> list = new List<Odds1x2History>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Odds1x2History model = new Odds1x2History();
                    model.id = (int)dr["id"];
                    model.scheduleid = (int)dr["scheduleid"];
                    model.companyid = (int)dr["companyid"];
                    model.home = float.Parse(dr["home"].ToString());
                    model.draw = float.Parse(dr["draw"].ToString());
                    model.away = float.Parse(dr["away"].ToString());
                    model.homep = float.Parse(dr["homep"].ToString());
                    model.drawp = float.Parse(dr["drawp"].ToString());
                    model.awayp = float.Parse(dr["awayp"].ToString());
                    model.returnrate = float.Parse(dr["returnrate"].ToString());
                    model.time = (DateTime)dr["time"];
                    list.Add(model);
                }
                return list;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 返回某场比赛某公司的赔率变化列表
        /// </summary>
        public List<Odds1x2History> GetList(string scheduleid, string[] companyids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT id, scheduleid, companyid, home, draw, away, homep, drawp, awayp, returnrate, time FROM      odds1x2history");
            strSql.Append(" WHERE (companyid in (" + companyids + ")) AND (scheduleid = @scheduleid) ORDER BY time DESC");
            SqlParameter[] parameters = {
					new SqlParameter("@scheduleid", SqlDbType.Int)};
            parameters[0].Value = scheduleid;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            List<Odds1x2History> list = new List<Odds1x2History>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Odds1x2History model = new Odds1x2History();
                    model.id = (int)dr["id"];
                    model.scheduleid = (int)dr["scheduleid"];
                    model.companyid = (int)dr["companyid"];
                    model.home = float.Parse(dr["home"].ToString());
                    model.draw = float.Parse(dr["draw"].ToString());
                    model.away = float.Parse(dr["away"].ToString());
                    model.homep = float.Parse(dr["homep"].ToString());
                    model.drawp = float.Parse(dr["drawp"].ToString());
                    model.awayp = float.Parse(dr["awayp"].ToString());
                    model.returnrate = float.Parse(dr["returnrate"].ToString());
                    model.time = (DateTime)dr["time"];
                    list.Add(model);
                }
                return list;
            }
            else
            {
                return null;
            }
        }
        

        /// <summary>
        /// 返回某场比赛某时间前所有公司的赔率
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public  List<Odds1x2History> GetLastList(int scheduleid,DateTime time)
        {
            StringBuilder strSql=new StringBuilder();
            strSql.Append("SELECT  a.id, a.scheduleid, a.companyid, a.home, a.draw, a.away, a.homep, a.drawp, a.awayp, a.returnrate, a.time FROM odds1x2history AS a INNER JOIN (SELECT   companyid, MAX(time) AS time FROM odds1x2history");
            strSql.Append(" WHERE (time <= @time) AND (scheduleid = @scheduleid) GROUP BY companyid) AS b ON a.companyid = b.companyid AND a.time = b.time AND a.scheduleid = @scheduleid");
			SqlParameter[] parameters = {
					new SqlParameter("@time", SqlDbType.DateTime),
                    new SqlParameter("@scheduleid", SqlDbType.Int)};
            parameters[0].Value = time;
            parameters[1].Value = scheduleid;

            List<Odds1x2History> list = new List<Odds1x2History>();
            
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Odds1x2History model = new Odds1x2History();
                    model.id = (int)dr["id"];
                    model.scheduleid = (int)dr["scheduleid"];
                    model.companyid = (int)dr["companyid"];
                    model.home = float.Parse(dr["home"].ToString());
                    model.draw = float.Parse(dr["draw"].ToString());
                    model.away = float.Parse(dr["away"].ToString());
                    model.homep = float.Parse(dr["homep"].ToString());
                    model.drawp = float.Parse(dr["drawp"].ToString());
                    model.awayp = float.Parse(dr["awayp"].ToString());
                    model.returnrate = float.Parse(dr["returnrate"].ToString());
                    model.time = (DateTime)dr["time"];
                    list.Add(model);
                }
			}
            return list;
        }

        /// <summary>
        /// 返回某场比赛某时间前所选公司的赔率
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public List<Odds1x2History> GetLastListByCompanys(int scheduleid, string companyids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  a.id, a.scheduleid, a.companyid, a.home, a.draw, a.away, a.homep, a.drawp, a.awayp, a.returnrate, a.time FROM odds1x2history AS a INNER JOIN (SELECT   companyid, MAX(time) AS time FROM odds1x2history");
            strSql.Append(" WHERE scheduleid = @scheduleid GROUP BY companyid) AS b ON a.companyid = b.companyid AND a.companyid IN (" + companyids + ") AND a.time = b.time AND a.scheduleid = @scheduleid");
            SqlParameter[] parameters = {
                    new SqlParameter("@scheduleid", SqlDbType.Int)};
            parameters[0].Value = scheduleid;

            List<Odds1x2History> list = new List<Odds1x2History>();

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Odds1x2History model = new Odds1x2History();
                    model.id = (int)dr["id"];
                    model.scheduleid = (int)dr["scheduleid"];
                    model.companyid = (int)dr["companyid"];
                    model.home = float.Parse(dr["home"].ToString());
                    model.draw = float.Parse(dr["draw"].ToString());
                    model.away = float.Parse(dr["away"].ToString());
                    model.homep = float.Parse(dr["homep"].ToString());
                    model.drawp = float.Parse(dr["drawp"].ToString());
                    model.awayp = float.Parse(dr["awayp"].ToString());
                    model.returnrate = float.Parse(dr["returnrate"].ToString());
                    model.time = (DateTime)dr["time"];
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 返回某场比赛某时间前所选公司的赔率
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public List<Odds1x2History> GetLastListByCompanys(int scheduleid,string companyids, DateTime time)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  a.id, a.scheduleid, a.companyid, a.home, a.draw, a.away, a.homep, a.drawp, a.awayp, a.returnrate, a.time FROM odds1x2history AS a INNER JOIN (SELECT   companyid, MAX(time) AS time FROM odds1x2history");
            strSql.Append(" WHERE (time <= @time) AND (scheduleid = @scheduleid) GROUP BY companyid) AS b ON a.companyid = b.companyid AND a.companyid IN (" + companyids + ") AND a.time = b.time AND a.scheduleid = @scheduleid");
            SqlParameter[] parameters = {
					new SqlParameter("@time", SqlDbType.DateTime),
                    new SqlParameter("@scheduleid", SqlDbType.Int)};
            parameters[0].Value = time;
            parameters[1].Value = scheduleid;

            List<Odds1x2History> list = new List<Odds1x2History>();

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Odds1x2History model = new Odds1x2History();
                    model.id = (int)dr["id"];
                    model.scheduleid = (int)dr["scheduleid"];
                    model.companyid = (int)dr["companyid"];
                    model.home = float.Parse(dr["home"].ToString());
                    model.draw = float.Parse(dr["draw"].ToString());
                    model.away = float.Parse(dr["away"].ToString());
                    model.homep = float.Parse(dr["homep"].ToString());
                    model.drawp = float.Parse(dr["drawp"].ToString());
                    model.awayp = float.Parse(dr["awayp"].ToString());
                    model.returnrate = float.Parse(dr["returnrate"].ToString());
                    model.time = (DateTime)dr["time"];
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得某场比赛单位时间以前全部公司的平均资料 包括平均赔率 平均比例
        /// </summary>
        /// <param name="scheduleid"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public DataSet GetAvgNumber(int scheduleid, DateTime time)
        { 
            StringBuilder strSql=new StringBuilder();
            strSql.Append("select AVG(home) avghome, AVG(draw) avgdraw, AVG(away) avgaway,AVG(homep) avghomep, AVG(drawp) avgdrawp, AVG(awayp) avgawayp from (SELECT  a.id, a.scheduleid, a.companyid, a.home, a.draw, a.away, a.homep, a.drawp, a.awayp, a.returnrate, a.time FROM odds1x2history AS a INNER JOIN (SELECT   companyid, MAX(time) AS time FROM odds1x2history");
            strSql.Append(" WHERE (time <= @time) AND (scheduleid = @scheduleid) GROUP BY companyid) AS b ON a.companyid = b.companyid AND a.time = b.time AND a.scheduleid = @scheduleid) as c");
			SqlParameter[] parameters = {
					new SqlParameter("@time", SqlDbType.DateTime),
                    new SqlParameter("@scheduleid", SqlDbType.Int)};
            parameters[0].Value = time;
            parameters[1].Value = scheduleid;

			return DbHelperSQL.Query(strSql.ToString(),parameters);
        }

        /// <summary>
        /// 获得某场比赛单位时间以前全部公司的平均资料 包括平均赔率 平均比例
        /// </summary>
        /// <param name="scheduleid"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public DataSet GetLastOdds(int scheduleid, DateTime time)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT   TOP (1) id, scheduleid, companyid, home, draw, away, homep, drawp, awayp, returnrate, time FROM odds1x2history");
            strSql.Append(" WHERE (scheduleid = @scheduleid) AND (time <= @time) ORDER BY time DESC");
            SqlParameter[] parameters = {
                    new SqlParameter("@scheduleid", SqlDbType.Int),
                    new SqlParameter("@time", SqlDbType.DateTime)
                                        };
            parameters[0].Value = scheduleid;
            parameters[1].Value = time;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得某场比赛单位时间以前全部公司的平均资料 包括平均赔率 平均比例
        /// </summary>
        /// <param name="scheduleid"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public DataSet GetCompanyLastOdds(int scheduleid, int companyid, DateTime time)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT   TOP (1) id, scheduleid, companyid, home, draw, away, homep, drawp, awayp, returnrate, time FROM odds1x2history");
            strSql.Append(" WHERE (scheduleid = @scheduleid) AND (companyid = @companyid) AND (time <= @time) ORDER BY time DESC");
            SqlParameter[] parameters = {
                    new SqlParameter("@scheduleid", SqlDbType.Int),
                    new SqlParameter("@companyid", SqlDbType.Int),
                    new SqlParameter("@time", SqlDbType.DateTime)
                                        };
            parameters[0].Value = scheduleid;
            parameters[1].Value = companyid;
            parameters[2].Value = time;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        public DataSet GetFirstTimePoint(string scheduleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT   TOP (1) time FROM odds1x2history");
            strSql.Append(" WHERE (scheduleid = @scheduleid) ORDER BY time");
            SqlParameter[] parameters = {
                    new SqlParameter("@scheduleid", SqlDbType.Int)
                                        };
            parameters[0].Value = scheduleID;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 查询博彩公司开出初盘的时间
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <param name="companyids"></param>
        /// <returns></returns>
        public DataSet GetCompanyStartPoint(string scheduleID, string[] companyids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT   TOP (1) time FROM odds1x2history");
            strSql.Append(" WHERE (scheduleid = @scheduleid) AND (companyid in ("+string.Join(",",companyids)+")) ORDER BY time");
            SqlParameter[] parameters = {
                    new SqlParameter("@scheduleid", SqlDbType.Int)
                                        };
            parameters[0].Value = scheduleID;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 查询博彩公司开出终盘的时间
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <param name="companyids"></param>
        /// <returns></returns>
        public DataSet GetCompanyEndPoint(string scheduleID, string[] companyids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT   TOP (1) time FROM odds1x2history");
            strSql.Append(" WHERE (scheduleid = @scheduleid) AND (companyid in (" + string.Join(",", companyids) + ")) ORDER BY time DESC");
            SqlParameter[] parameters = {
                    new SqlParameter("@scheduleid", SqlDbType.Int)
                                        };
            parameters[0].Value = scheduleID;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        public DataSet GetAveNumByCompanys(string scheduleid,string companyids,DateTime time)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AVG(home) avghome, AVG(draw) avgdraw, AVG(away) avgaway,AVG(homep) avghomep, AVG(drawp) avgdrawp, AVG(awayp) avgawayp from (SELECT  a.id, a.scheduleid, a.companyid, a.home, a.draw, a.away, a.homep, a.drawp, a.awayp, a.returnrate, a.time FROM odds1x2history AS a INNER JOIN (SELECT   companyid, MAX(time) AS time FROM odds1x2history");
            strSql.Append(" WHERE (time <= @time) AND (scheduleid = @scheduleid) GROUP BY companyid) AS b ON a.companyid = b.companyid AND a.companyid IN ("+companyids+") AND a.time = b.time AND a.scheduleid = @scheduleid) as c");
            SqlParameter[] parameters = {
					new SqlParameter("@time", SqlDbType.DateTime),
                    new SqlParameter("@scheduleid", SqlDbType.Int)};
            parameters[0].Value = time;
            parameters[1].Value = scheduleid;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        ///  查询所有公司初盘列表
        /// </summary>
        /// <param name="scheduleid"></param>
        /// <param name="companyids"></param>
        /// <returns></returns>
        public List<Odds1x2History> GetFirstListByCompanys(int scheduleid, string companyids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  a.id, a.scheduleid, a.companyid, a.home, a.draw, a.away, a.homep, a.drawp, a.awayp, a.returnrate, a.time FROM odds1x2history AS a INNER JOIN (SELECT   companyid, MIN(time) AS time FROM odds1x2history");
            strSql.Append(" WHERE scheduleid = @scheduleid GROUP BY companyid) AS b ON a.companyid = b.companyid AND a.companyid IN (" + companyids + ") AND a.time = b.time AND a.scheduleid = @scheduleid");
            SqlParameter[] parameters = {
                    new SqlParameter("@scheduleid", SqlDbType.Int)};
            parameters[0].Value = scheduleid;

            List<Odds1x2History> list = new List<Odds1x2History>();

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Odds1x2History model = new Odds1x2History();
                    model.id = (int)dr["id"];
                    model.scheduleid = (int)dr["scheduleid"];
                    model.companyid = (int)dr["companyid"];
                    model.home = float.Parse(dr["home"].ToString());
                    model.draw = float.Parse(dr["draw"].ToString());
                    model.away = float.Parse(dr["away"].ToString());
                    model.homep = float.Parse(dr["homep"].ToString());
                    model.drawp = float.Parse(dr["drawp"].ToString());
                    model.awayp = float.Parse(dr["awayp"].ToString());
                    model.returnrate = float.Parse(dr["returnrate"].ToString());
                    model.time = (DateTime)dr["time"];
                    list.Add(model);
                }
            }
            return list;
        }

        public DataSet GetChangeTimesByCompanys(string scheduleid,string companyids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT distinct time FROM odds1x2history");
            strSql.Append(" WHERE scheduleid = @scheduleid and companyid in (" + companyids + ") order by time");
            SqlParameter[] parameters = {
                    new SqlParameter("@scheduleid", SqlDbType.Int)};
            parameters[0].Value = scheduleid;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        public DataSet GetOddsDataCount(string scheduleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT count(*) FROM odds1x2history WHERE scheduleid = @scheduleid");
            SqlParameter[] parameters = {
                    new SqlParameter("@scheduleid", SqlDbType.Int)};
            parameters[0].Value = scheduleID;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        public DataSet GetCompanyGroupOddsCount(string scheduleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT   COUNT(*) AS oddscount, companyid FROM odds1x2history WHERE (scheduleid = @scheduleid) GROUP BY companyid");
            SqlParameter[] parameters = {
                    new SqlParameter("@scheduleid", SqlDbType.VarChar)};
            parameters[0].Value = scheduleID;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
    }
}
