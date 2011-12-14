using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SeoWebSite.DBUtility;
using System.Collections.Generic;//Please add references
namespace SeoWebSite.DAL
{
	/// <summary>
	/// 数据访问类:odds_bz
	/// </summary>
	public class odds_bz
	{
		public odds_bz()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "odds_bz"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from odds_bz");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SeoWebSite.Model.odds_bz model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into odds_bz(");
			strSql.Append("companyID,scheduleID,home,draw,away,time)");
			strSql.Append(" values (");
			strSql.Append("@companyID,@scheduleID,@home,@draw,@away,@time)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@companyID", SqlDbType.Int,4),
					new SqlParameter("@scheduleID", SqlDbType.Int,4),
					new SqlParameter("@home", SqlDbType.Float,8),
					new SqlParameter("@draw", SqlDbType.Float,8),
					new SqlParameter("@away", SqlDbType.Float,8),
					new SqlParameter("@time", SqlDbType.DateTime)};
			parameters[0].Value = model.companyID;
			parameters[1].Value = model.scheduleID;
			parameters[2].Value = model.home;
			parameters[3].Value = model.draw;
			parameters[4].Value = model.away;
			parameters[5].Value = model.time;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(SeoWebSite.Model.odds_bz model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update odds_bz set ");
			strSql.Append("companyID=@companyID,");
			strSql.Append("scheduleID=@scheduleID,");
			strSql.Append("home=@home,");
			strSql.Append("draw=@draw,");
			strSql.Append("away=@away,");
			strSql.Append("time=@time");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@companyID", SqlDbType.Int,4),
					new SqlParameter("@scheduleID", SqlDbType.Int,4),
					new SqlParameter("@home", SqlDbType.Float,8),
					new SqlParameter("@draw", SqlDbType.Float,8),
					new SqlParameter("@away", SqlDbType.Float,8),
					new SqlParameter("@time", SqlDbType.DateTime)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.companyID;
			parameters[2].Value = model.scheduleID;
			parameters[3].Value = model.home;
			parameters[4].Value = model.draw;
			parameters[5].Value = model.away;
			parameters[6].Value = model.time;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from odds_bz ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
			parameters[0].Value = id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
        public void Delete(string companyid,string scheduleid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from odds_bz ");
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
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from odds_bz ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public SeoWebSite.Model.odds_bz GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,companyID,scheduleID,home,draw,away,time from odds_bz ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
			parameters[0].Value = id;

			SeoWebSite.Model.odds_bz model=new SeoWebSite.Model.odds_bz();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["companyID"].ToString()!="")
				{
					model.companyID=int.Parse(ds.Tables[0].Rows[0]["companyID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["scheduleID"].ToString()!="")
				{
					model.scheduleID=int.Parse(ds.Tables[0].Rows[0]["scheduleID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["home"].ToString()!="")
				{
					model.home=decimal.Parse(ds.Tables[0].Rows[0]["home"].ToString());
				}
				if(ds.Tables[0].Rows[0]["draw"].ToString()!="")
				{
					model.draw=decimal.Parse(ds.Tables[0].Rows[0]["draw"].ToString());
				}
				if(ds.Tables[0].Rows[0]["away"].ToString()!="")
				{
					model.away=decimal.Parse(ds.Tables[0].Rows[0]["away"].ToString());
				}
				if(ds.Tables[0].Rows[0]["time"].ToString()!="")
				{
					model.time=DateTime.Parse(ds.Tables[0].Rows[0]["time"].ToString());
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,companyID,scheduleID,home,draw,away,time ");
			strSql.Append(" FROM odds_bz ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" id,companyID,scheduleID,home,draw,away,time ");
			strSql.Append(" FROM odds_bz ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			parameters[0].Value = "odds_bz";
			parameters[1].Value = "";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method

        public DataSet GetList(string companyid, string tt, string p1, string p2, string p3)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT   c.id, c.data, c.updated, c.date, c.home, c.away, c.halfhome, c.halfaway, c.scheduleTypeID, a.companyID, a.time,a.home p1,a.draw p2,a.away p3 FROM odds_bz AS a INNER JOIN (SELECT   companyID, scheduleID, " + tt + "(time) AS Expr1 FROM odds_bz GROUP BY companyID, scheduleID) AS b ON a.scheduleID = b.scheduleID AND a.companyID = b.companyID AND a.time = b.Expr1");
            if (!string.IsNullOrEmpty(p1))
            {
                strSql.Append(" AND a.home = " + p1);
            }
            if (!string.IsNullOrEmpty(p2))
            {
                strSql.Append(" AND a.draw = " + p2);
            }
            if (!string.IsNullOrEmpty(p3))
            {
                strSql.Append(" AND a.away = " + p3);
            }
            if (!string.IsNullOrEmpty(companyid))
            {
                strSql.Append(" and a.companyID= " + companyid);
            }
            strSql.Append(" join schedule c on c.id=a.scheduleid and c.updated=1");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetList(string companyid, string[] oddsArray)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select g.id, g.data, g.updated, g.date, g.home, g.away, g.halfhome, g.halfaway, g.scheduleTypeID, e.companyID, e.time1,e.home1,e.draw1,e.away1,f.time2,f.home2,f.draw2,f.away2 from ");
            strSql.Append("(select a.companyID,a.scheduleID,a.home home1,a.draw draw1,a.away away1,a.time time1 from odds_bz a join (SELECT   MAX(id) AS Expr1, companyID, scheduleID");
            strSql.Append(" FROM odds_bz GROUP BY scheduleID, companyID) b on a.id = b.Expr1) e join");
            strSql.Append("(select c.companyID,c.scheduleID,c.home home2,c.draw draw2,c.away away2,c.time time2 from odds_bz c join (SELECT   min(id) AS Expr1, companyID, scheduleID");
            strSql.Append(" FROM odds_bz  GROUP BY scheduleID, companyID) d on c.id = d.Expr1) f on e.scheduleID= f.scheduleID and e.companyID=f.companyID");
            if (!string.IsNullOrEmpty(oddsArray[0]))
            {
                strSql.Append(" AND e.home1 = " + oddsArray[0]);
            }
            if (!string.IsNullOrEmpty(oddsArray[1]))
            {
                strSql.Append(" AND e.draw1 = " + oddsArray[1]);
            }
            if (!string.IsNullOrEmpty(oddsArray[2]))
            {
                strSql.Append(" AND e.away1 = " + oddsArray[2]);
            }
            if (!string.IsNullOrEmpty(oddsArray[3]))
            {
                strSql.Append(" AND f.home2 = " + oddsArray[3]);
            }
            if (!string.IsNullOrEmpty(oddsArray[4]))
            {
                strSql.Append(" AND f.draw2 = " + oddsArray[4]);
            }
            if (!string.IsNullOrEmpty(oddsArray[5]))
            {
                strSql.Append(" AND f.away2 = " + oddsArray[5]);
            }
            if (!string.IsNullOrEmpty(companyid))
            {
                strSql.Append(" and e.companyID= " + companyid);
            }
            strSql.Append(" left join Schedule g on e.scheduleID=g.id and g.updated=1");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet queryOddsCount(System.Collections.Generic.List<string> oddsList, float blur, int scheduleTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append("rqy=sum(case when g.home > g.away then 1 else 0 end),");
            strSql.Append("rqz=sum(case when g.home = g.away then 1 else 0 end),");
            strSql.Append("rqs=sum(case when g.home < g.away then 1 else 0 end),");
            strSql.Append("count(*) totalCount from");
            strSql.Append(" tempdb..TempTable_Companys_Chupan_BZ e join tempdb..TempTable_Companys_Zhongpan_BZ f");
            strSql.Append(" on e.scheduleID= f.scheduleID and e.companyID=f.companyID AND (");
            for (int i = 0; i < oddsList.Count; i++)
            {
                string[] oddsArray = oddsList[i].Split(',');
                if (i > 0)
                {
                    strSql.Append(" or ");
                }
                strSql.Append(" (e.home = " + oddsArray[0]);
                strSql.Append(" AND e.away = " + oddsArray[2]);
                strSql.Append(" AND e.draw = " + oddsArray[1]);
                
                
                strSql.Append(" AND f.home >= " + (float.Parse(oddsArray[3]) - blur) + " and f.home <= " + (float.Parse(oddsArray[3]) + blur));
                strSql.Append(" AND f.away >= " + (float.Parse(oddsArray[5]) - blur) + " and f.away <= " + (float.Parse(oddsArray[5]) + blur));
                strSql.Append(" AND f.draw >= " + (float.Parse(oddsArray[4]) - blur) + " and f.draw <= " + (float.Parse(oddsArray[4]) + blur));
                
                strSql.Append(" AND e.companyID = " + oddsArray[6] + ")");
            }
            strSql.Append(") join Schedule g on e.scheduleID=g.id and g.updated=1");
            if (scheduleTypeID != 0)
            {
                strSql.Append(" and g.scheduleTypeID=" + scheduleTypeID);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public void CreateTempTable()
        {
            List<string> sqlList = new List<string>();
            if (!DbHelperSQL.TempTabExsits("TempTable_Companys_Chupan_BZ"))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT a.companyID, a.scheduleID, a.home, a.draw, a.away, a.time into tempdb..TempTable_Companys_Chupan_BZ ");
                strSql.Append("FROM dbo.odds_bz AS a INNER JOIN ");
                strSql.Append("(SELECT MAX(id) AS Expr1, scheduleID FROM dbo.odds_bz GROUP BY scheduleID, companyID) AS b ");
                strSql.Append("ON a.id = b.Expr1 ");
                sqlList.Add(strSql.ToString());
            }
            if (!DbHelperSQL.TempTabExsits("TempTable_Companys_Zhongpan_BZ"))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT c.companyID, c.scheduleID, c.home, c.draw, c.away, c.time into tempdb..TempTable_Companys_Zhongpan_BZ ");
                strSql.Append("FROM dbo.odds_bz AS c INNER JOIN ");
                strSql.Append("(SELECT MIN(id) AS Expr1, scheduleID FROM dbo.odds_bz GROUP BY scheduleID, companyID) AS d ");
                strSql.Append("ON c.id = d.Expr1");
                sqlList.Add(strSql.ToString());
            }
            DbHelperSQL.ExecuteSqlTran(sqlList);
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
                strSql.Append("rqy=100.0 * sum(case when g.home-g.away > 0 then 1 else 0 end) / count(*),");
                strSql.Append("rqz=100.0 * sum(case when g.home-g.away = 0 then 1 else 0 end) / count(*),");
                strSql.Append("rqs=100.0 * sum(case when g.home-g.away < 0 then 1 else 0 end) / count(*),");
                strSql.Append("count(*) totalCount from");
                strSql.Append(" tempdb..TempTable_Companys_Chupan_BZ e join Schedule g on e.scheduleID=g.id and g.updated=1");
                strSql.Append(" where e.home=" + oddsArray[0] + " and e.draw=" + oddsArray[1] + " and e.away=" + oddsArray[2] + " and companyID=" + oddsArray[6] + " and g.id<>" + scheduleID);
                if (scheduleTypeID != 0)
                {
                    strSql.Append(" and g.scheduleTypeID=" + scheduleTypeID);
                }
                sqlList.Add(strSql.ToString());
                strSql = new StringBuilder();
                strSql.Append("select ");
                strSql.Append("rqy=100.0 * sum(case when g.home-g.away > 0 then 1 else 0 end) / count(*),");
                strSql.Append("rqz=100.0 * sum(case when g.home-g.away = 0 then 1 else 0 end) / count(*),");
                strSql.Append("rqs=100.0 * sum(case when g.home-g.away < 0 then 1 else 0 end) / count(*),");
                strSql.Append("count(*) totalCount from");
                strSql.Append(" tempdb..TempTable_Companys_Chupan_BZ e join Schedule g on e.scheduleID=g.id and g.updated=1");
                strSql.Append(" where e.home=" + oddsArray[3] + " and e.draw=" + oddsArray[4] + " and e.away=" + oddsArray[5] + " and companyID=" + oddsArray[6] + " and g.id<>" + scheduleID);
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
    }
}

