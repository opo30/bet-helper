using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SeoWebSite.DBUtility;
using System.Collections.Generic;//Please add references
namespace SeoWebSite.DAL
{
	/// <summary>
	/// 数据访问类:odds_dx
	/// </summary>
	public class odds_dx
	{
		public odds_dx()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "odds_dx"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from odds_dx");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SeoWebSite.Model.odds_dx model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into odds_dx(");
			strSql.Append("companyID,scheduleID,pankou,big,small,time)");
			strSql.Append(" values (");
			strSql.Append("@companyID,@scheduleID,@pankou,@big,@small,@time)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@companyID", SqlDbType.Int,4),
					new SqlParameter("@scheduleID", SqlDbType.Int,4),
					new SqlParameter("@pankou", SqlDbType.Float,8),
					new SqlParameter("@big", SqlDbType.Float,8),
					new SqlParameter("@small", SqlDbType.Float,8),
					new SqlParameter("@time", SqlDbType.DateTime)};
			parameters[0].Value = model.companyID;
			parameters[1].Value = model.scheduleID;
			parameters[2].Value = model.pankou;
			parameters[3].Value = model.big;
			parameters[4].Value = model.small;
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
		public bool Update(SeoWebSite.Model.odds_dx model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update odds_dx set ");
			strSql.Append("companyID=@companyID,");
			strSql.Append("scheduleID=@scheduleID,");
			strSql.Append("pankou=@pankou,");
			strSql.Append("big=@big,");
			strSql.Append("small=@small,");
			strSql.Append("time=@time");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@companyID", SqlDbType.Int,4),
					new SqlParameter("@scheduleID", SqlDbType.Int,4),
					new SqlParameter("@pankou", SqlDbType.Float,8),
					new SqlParameter("@big", SqlDbType.Float,8),
					new SqlParameter("@small", SqlDbType.Float,8),
					new SqlParameter("@time", SqlDbType.DateTime)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.companyID;
			parameters[2].Value = model.scheduleID;
			parameters[3].Value = model.pankou;
			parameters[4].Value = model.big;
			parameters[5].Value = model.small;
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
			strSql.Append("delete from odds_dx ");
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
        public void Delete(string companyid, string scheduleid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from odds_dx ");
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
			strSql.Append("delete from odds_dx ");
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
		public SeoWebSite.Model.odds_dx GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,companyID,scheduleID,pankou,big,small,time from odds_dx ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
			parameters[0].Value = id;

			SeoWebSite.Model.odds_dx model=new SeoWebSite.Model.odds_dx();
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
				if(ds.Tables[0].Rows[0]["pankou"].ToString()!="")
				{
					model.pankou=decimal.Parse(ds.Tables[0].Rows[0]["pankou"].ToString());
				}
				if(ds.Tables[0].Rows[0]["big"].ToString()!="")
				{
					model.big=decimal.Parse(ds.Tables[0].Rows[0]["big"].ToString());
				}
				if(ds.Tables[0].Rows[0]["small"].ToString()!="")
				{
					model.small=decimal.Parse(ds.Tables[0].Rows[0]["small"].ToString());
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
			strSql.Append("select id,companyID,scheduleID,pankou,big,small,time ");
			strSql.Append(" FROM odds_dx ");
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
			strSql.Append(" id,companyID,scheduleID,pankou,big,small,time ");
			strSql.Append(" FROM odds_dx ");
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
			parameters[0].Value = "odds_dx";
			parameters[1].Value = "";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method

        /// <summary>
        /// 查询让球盘统计
        /// </summary>
        /// <param name="oddsList">赔率信息列表</param>
        /// <param name="blur">模糊值</param>
        /// <param name="scheduleTypeID">比赛类别ID</param>
        /// <returns>数据集</returns>
        public DataSet queryOddsCount(List<string> oddsList, float blur, int scheduleTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append("rqy=sum(case when g.home + g.away > e.pankou then 1 else 0 end),");
            strSql.Append("rqz=sum(case when g.home + g.away = e.pankou then 1 else 0 end),");
            strSql.Append("rqs=sum(case when g.home + g.away < e.pankou then 1 else 0 end),");
            strSql.Append("count(*) totalCount from");
            strSql.Append(" tempdb..TempTable_Companys_Chupan_DX e join tempdb..TempTable_Companys_Zhongpan_DX f");
            strSql.Append(" on e.scheduleID= f.scheduleID and e.companyID=f.companyID AND (");
            for (int i = 0; i < oddsList.Count; i++)
            {
                string[] oddsArray = oddsList[i].Split(',');
                if (i > 0)
                {
                    strSql.Append(" or ");
                }
                //初盘匹配
                strSql.Append(" (e.big = " + oddsArray[0]);
                strSql.Append(" AND e.small = " + oddsArray[2]);
                strSql.Append(" AND e.pankou = " + oddsArray[1]);
                //临场盘匹配
                strSql.Append(" AND f.big >= " + (float.Parse(oddsArray[3]) - blur) + " and f.big <= " + (float.Parse(oddsArray[3]) + blur));
                strSql.Append(" AND f.small >= " + (float.Parse(oddsArray[5]) - blur) + " and f.small <= " + (float.Parse(oddsArray[5]) + blur));
                strSql.Append(" AND f.pankou = " + oddsArray[4]);
                //公司匹配
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
            if (!DbHelperSQL.TempTabExsits("TempTable_Companys_Chupan_DX"))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT a.companyID, a.scheduleID, a.big, a.pankou, a.small, a.time into tempdb..TempTable_Companys_Chupan_DX ");
                strSql.Append("FROM dbo.odds_dx AS a INNER JOIN ");
                strSql.Append("(SELECT MAX(id) AS Expr1, scheduleID FROM dbo.odds_dx GROUP BY scheduleID, companyID) AS b ");
                strSql.Append("ON a.id = b.Expr1 ");
                sqlList.Add(strSql.ToString());
            }
            if (!DbHelperSQL.TempTabExsits("TempTable_Companys_Zhongpan_DX"))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT c.companyID, c.scheduleID, c.big, c.pankou, c.small, c.time into tempdb..TempTable_Companys_Zhongpan_DX ");
                strSql.Append("FROM dbo.odds_dx AS c INNER JOIN ");
                strSql.Append("(SELECT MIN(id) AS Expr1, scheduleID FROM dbo.odds_dx AS odds_rq_1 GROUP BY scheduleID, companyID) AS d ");
                strSql.Append("ON c.id = d.Expr1");
                sqlList.Add(strSql.ToString());
            }
            DbHelperSQL.ExecuteSqlTran(sqlList);
        }
	}

    
}

