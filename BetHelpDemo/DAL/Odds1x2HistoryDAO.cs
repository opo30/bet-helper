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
    public class Odds1x2HistoryDAO
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SeoWebSite.Model.Odds1x2History model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into odds1x2history(");
            strSql.Append("scheduleid,companyid,home,draw,away,homep,drawp,awayp,returnrate,time)");
            strSql.Append(" values (");
            strSql.Append("@scheduleid,@companyid,@home,@draw,@away,@homep,@drawp,@awayp,@returnrate,@time)");
            SqlParameter[] parameters = {
					new SqlParameter("@scheduleid", SqlDbType.Int),
					new SqlParameter("@companyid", SqlDbType.Int),
					new SqlParameter("@home", SqlDbType.Float),
					new SqlParameter("@draw", SqlDbType.Float),
					new SqlParameter("@away", SqlDbType.Float),
					new SqlParameter("@homep", SqlDbType.Float),
					new SqlParameter("@drawp", SqlDbType.Float),
                    new SqlParameter("@awayp", SqlDbType.Float),
                    new SqlParameter("@returnrate", SqlDbType.Float),
                    new SqlParameter("@time", SqlDbType.DateTime)};
            parameters[0].Value = model.scheduleid;
            parameters[1].Value = model.companyid;
            parameters[2].Value = model.home;
            parameters[3].Value = model.draw;
            parameters[4].Value = model.away;
            parameters[5].Value = model.homep;
            parameters[6].Value = model.drawp;
            parameters[7].Value = model.awayp;
            parameters[8].Value = model.returnrate;
            parameters[9].Value = model.time;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM odds1x2history ");
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
        public List<Odds1x2History> GetLastList(int scheduleid, DateTime time)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  a.id, a.scheduleid, a.companyid, a.home, a.draw, a.away, a.homep, a.drawp, a.awayp, a.returnrate, a.time FROM odds1x2history AS a INNER JOIN (SELECT   companyid, MAX(time) AS time FROM odds1x2history");
            strSql.Append(" WHERE (time <= @time) AND (scheduleid = @scheduleid) GROUP BY companyid) AS b ON a.companyid = b.companyid AND a.time = b.time AND a.scheduleid = @scheduleid");
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
        public List<Odds1x2History> GetLastListByCompanys(int scheduleid, string companyids, DateTime time)
        {
            StringBuilder strSql = new StringBuilder();
            //select AVG(home) avghome, AVG(draw) avgdraw, AVG(away) avgaway,AVG(homep) avghomep, AVG(drawp) avgdrawp, AVG(awayp) avgawayp from odds1x2history a where ID=(select min(ID) from odds1x2history where companyid=a.companyid and (time <= '2010/9/12 14:58:00') AND (scheduleid = '376366') and companyid IN (474,499,517,18,545,81,659,676,660,594,601,582,531,456,466,436,124,64,658,32,33))

            strSql.Append("SELECT  a.id, a.scheduleid, a.companyid, a.home, a.draw, a.away, a.homep, a.drawp, a.awayp, a.returnrate, a.time");
            strSql.Append(" FROM odds1x2history a where ID=(select min(ID) from odds1x2history");
            strSql.Append(" WHERE (time <= @time) AND (scheduleid = @scheduleid) AND companyid IN (" + companyids + ") AND companyid=a.companyid)");
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AVG(home) avghome, AVG(draw) avgdraw, AVG(away) avgaway,AVG(homep) avghomep, AVG(drawp) avgdrawp, AVG(awayp) avgawayp from (SELECT  a.id, a.scheduleid, a.companyid, a.home, a.draw, a.away, a.homep, a.drawp, a.awayp, a.returnrate, a.time FROM odds1x2history AS a INNER JOIN (SELECT   companyid, MAX(time) AS time FROM odds1x2history");
            strSql.Append(" WHERE (time <= @time) AND (scheduleid = @scheduleid) GROUP BY companyid) AS b ON a.companyid = b.companyid AND a.time = b.time AND a.scheduleid = @scheduleid) as c");
            SqlParameter[] parameters = {
					new SqlParameter("@time", SqlDbType.DateTime),
                    new SqlParameter("@scheduleid", SqlDbType.Int)};
            parameters[0].Value = time;
            parameters[1].Value = scheduleid;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
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
            strSql.Append(" WHERE (scheduleid = @scheduleid) AND (companyid in (" + string.Join(",", companyids) + ")) ORDER BY time");
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

        public DataSet GetAveNumByCompanys(string scheduleid, string companyids, DateTime time)
        {
            //select AVG(home) avghome, AVG(draw) avgdraw, AVG(away) avgaway,AVG(homep) avghomep, AVG(drawp) avgdrawp, AVG(awayp) avgawayp from odds1x2history a where ID=(select min(ID) from odds1x2history where companyid=a.companyid and (time <= '2010/9/12 14:58:00') AND (scheduleid = '376366') and companyid IN (474,499,517,18,545,81,659,676,660,594,601,582,531,456,466,436,124,64,658,32,33))

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AVG(home) avghome, AVG(draw) avgdraw, AVG(away) avgaway,AVG(homep) avghomep, AVG(drawp) avgdrawp, AVG(awayp) avgawayp");
            strSql.Append(" FROM odds1x2history a where ID=(select min(ID) from odds1x2history");
            strSql.Append(" WHERE (time <= @time) AND (scheduleid = @scheduleid) AND companyid IN (" + companyids + ") AND companyid=a.companyid)");
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

        public DataSet GetChangeTimesByCompanys(string scheduleid, string companyids)
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

        public bool Exists(string matchid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from odds1x2history");
            strSql.Append(" where scheduleid=@scheduleid ");
            SqlParameter[] parameters = {
					new SqlParameter("@scheduleid", SqlDbType.VarChar,50)};
            parameters[0].Value = matchid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 查询最后几小时有更新的公司
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public string[] getLastCompanyIDs(string scheduleID, int hourNum)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct companyid from odds1x2history where DATEDIFF(HOUR,time,(select max(time) from odds1x2history where scheduleid=@scheduleID))<=@hourNum and scheduleid=@scheduleID");
            SqlParameter[] parameters = {
                    new SqlParameter("@scheduleID", SqlDbType.VarChar),
                    new SqlParameter("@hourNum", SqlDbType.Int)};
            parameters[0].Value = scheduleID;
            parameters[1].Value = hourNum;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            List<string> companyIdList = new List<string>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                companyIdList.Add(item[0].ToString());
            }
            return companyIdList.ToArray();
        }

        /// <summary>
        /// 获得比赛编码的所有赔率总数
        /// </summary>
        /// <param name="scheduleid"></param>
        /// <returns></returns>
        public int GetCount(int scheduleid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select COUNT(id) from odds1x2history where scheduleid=@scheduleid");
            SqlParameter[] parameters = {
                    new SqlParameter("@scheduleid", SqlDbType.Int)};
            parameters[0].Value = scheduleid;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return (int)ds.Tables[0].Rows[0][0];
        }

        public void CreateTable(string tableName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("IF NOT EXISTS(SELECT 1 FROM SYSOBJECTS WHERE NAME = '" + tableName + "' AND XTYPE ='U')");
            strSql.Append("CREATE TABLE [dbo].[" + tableName + "](");
            strSql.Append("[id] [int] IDENTITY(1,1) NOT NULL,");
            strSql.Append("[scheduleid] [int] NULL,");
            strSql.Append("[companyid] [int] NULL,");
            strSql.Append("[home] [float] NULL,");
            strSql.Append("[draw] [float] NULL,");
            strSql.Append("[away] [float] NULL,");
            strSql.Append("[homep] [float] NULL,");
            strSql.Append("[drawp] [float] NULL,");
            strSql.Append("[awayp] [float] NULL,");
            strSql.Append("[returnrate] [float] NULL,");
            strSql.Append("[time] [datetime] NULL,");
            strSql.Append("CONSTRAINT [PK_" + tableName + "] PRIMARY KEY CLUSTERED ");
            strSql.Append("(");
            strSql.Append("[id] ASC");
            strSql.Append(")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]");
            strSql.Append(") ON [PRIMARY]");
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }
    }
}
