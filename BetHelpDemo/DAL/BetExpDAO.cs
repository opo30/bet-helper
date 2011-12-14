using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SeoWebSite.DBUtility;
using MySql.Data.MySqlClient;//请先添加引用
namespace SeoWebSite.DAL
{
	/// <summary>
	/// 数据访问类betexp。
	/// </summary>
	public class BetExpDAO
	{
        public BetExpDAO()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "betexp"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from betexp");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 是否存在统计痕迹
        /// </summary>
        public bool ExistsStatistics(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from betexp");
            strSql.Append(" where id=@id and hasstatistics=1");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SeoWebSite.Model.BetExp model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into betexp(");
            strSql.Append("id,data,isexp,trends,changes)");
			strSql.Append(" values (");
            strSql.Append("@id,@data,@isexp,@trends,@changes)");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@data", SqlDbType.Text),
					new SqlParameter("@isexp", SqlDbType.Bit,1),
                    new SqlParameter("@trends", SqlDbType.Text),
                    new SqlParameter("@changes", SqlDbType.Text)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.data;
			parameters[2].Value = model.isexp;
            parameters[3].Value = model.trends;
            parameters[4].Value = model.changes;
			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(SeoWebSite.Model.BetExp model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update betexp set ");
            strSql.Append("isexp=@isexp,hometeam=@hometeam,awayteam=@awayteam,homescore=@homescore,awayscore=@awayscore,victory=@victory,win=@win,asia=@asia");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@isexp", SqlDbType.Bit,1),
                    new SqlParameter("@hometeam", SqlDbType.VarChar),
                    new SqlParameter("@awayteam", SqlDbType.VarChar),
                    new SqlParameter("@homescore", SqlDbType.SmallInt),
                    new SqlParameter("@awayscore", SqlDbType.SmallInt),
                    new SqlParameter("@victory", SqlDbType.SmallInt),
                    new SqlParameter("@win", SqlDbType.SmallInt),
                    new SqlParameter("@asia", SqlDbType.Float)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.isexp;
            parameters[2].Value = model.hometeam;
            parameters[3].Value = model.awayteam;
            parameters[4].Value = model.homescore;
            parameters[5].Value = model.awayscore;
            parameters[6].Value = model.victory;
            parameters[7].Value = model.win;
            parameters[8].Value = model.asia;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(int matchid ,string exp)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update betexp set ");
            strSql.Append("exp=@exp");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@exp", SqlDbType.Text),
                    new SqlParameter("@id", SqlDbType.Int)};
            parameters[0].Value = exp;
            parameters[1].Value = matchid;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(int matchid, bool hasstatistics)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update betexp set ");
            strSql.Append("hasstatistics=@hasstatistics");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@hasstatistics", SqlDbType.Bit),
                    new SqlParameter("@id", SqlDbType.Int)};
            parameters[0].Value = hasstatistics;
            parameters[1].Value = matchid;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from betexp ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SeoWebSite.Model.BetExp GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,data,isexp,exp from betexp ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			SeoWebSite.Model.BetExp model=new SeoWebSite.Model.BetExp();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				model.data=ds.Tables[0].Rows[0]["data"].ToString();
				if(ds.Tables[0].Rows[0]["isexp"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["isexp"].ToString()=="1")||(ds.Tables[0].Rows[0]["isexp"].ToString().ToLower()=="true"))
					{
						model.isexp=true;
					}
					else
					{
						model.isexp=false;
					}
				}
				model.exp=ds.Tables[0].Rows[0]["exp"].ToString();
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
			strSql.Append("select id,data,isexp,exp ");
			strSql.Append(" FROM betexp ");
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
			strSql.Append(" id,data,isexp,exp ");
			strSql.Append(" FROM betexp ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		
         ///<summary>
         ///分页获取数据列表
         ///</summary>
		public DataSet GetList(int start,int end,string strWhere)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("WITH TT AS (SELECT *,ROW_NUMBER() OVER (order by id) as rowid ");
            strSql.Append("FROM BetExp ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append("WHERE " + strWhere);
            }
            strSql.Append(") SELECT * FROM TT ");
            strSql.Append("WHERE rowid between @start and @end");
			SqlParameter[] parameters = {
					new SqlParameter("@start", SqlDbType.Int),
					new SqlParameter("@end", SqlDbType.Int)
					};
            parameters[0].Value = start;
            parameters[1].Value = end;
            return DbHelperSQL.Query(strSql.ToString(),parameters);
		}


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public long GetListCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) ");
            strSql.Append(" FROM betexp ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }
		#endregion  成员方法

        public string GetIsExpByID(string matchid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select isexp ");
            strSql.Append(" FROM betexp ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar)
					};
            parameters[0].Value = matchid;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(),parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return null;
            }
        }

        public void UpdateAnalysis(int matchid, string a)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update betexp set ");
            strSql.Append("analysis=@analysis");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@analysis", SqlDbType.VarChar),
                    new SqlParameter("@id", SqlDbType.Int)};
            parameters[0].Value = a;
            parameters[1].Value = matchid;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public int GetCountByAnalysis(string data)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) ");
            strSql.Append(" FROM betexp ");
            strSql.Append(" where analysis like @analysis");
            SqlParameter[] parameters = {
					new SqlParameter("@analysis", SqlDbType.Text)};
            parameters[0].Value = '%' + data + '%';
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }

        public DataSet GetListByAnalysis(string data)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,data,isexp,exp,analysis");
            strSql.Append(" FROM BetExp ");
            strSql.Append(" where analysis like @analysis");
            SqlParameter[] parameters = {
					new SqlParameter("@analysis", SqlDbType.Text)};
            parameters[0].Value = '%' + data + '%';
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 查询没有分析的合适的现有数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetNoAnalysisList(int recordCount)
        {
            //select c.scheduleid from (select distinct a.scheduleid ,(select COUNT(id) from odds1x2history where scheduleid = a.scheduleid) as recoredcount from odds1x2history a where id not in (select b.id from BetExp b)) c where c.recoredcount >=500

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select c.scheduleid from (select distinct a.scheduleid,(select COUNT(id) from odds1x2history where scheduleid = a.scheduleid) as recordcount");
            strSql.Append(" from odds1x2history a");
            strSql.Append(" where a.scheduleid not in (select b.id from BetExp b)) c where c.recordcount >=@recordCount");
            SqlParameter[] parameters = {
					new SqlParameter("@recordCount", SqlDbType.Int)};
            parameters[0].Value = recordCount;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        public DataSet GetListByTrends(string value,string changesValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select s.*,b.data chartdata FROM Schedule s join  BetExp b on s.ScheduleID=b.id");
            strSql.Append(" where b.trends like @trends and b.changes like @changes");
            SqlParameter[] parameters = {
					new SqlParameter("@trends", SqlDbType.Text),
                        new SqlParameter("@changes", SqlDbType.Text)};
            parameters[0].Value = "%" + value;
            parameters[1].Value = "%" + changesValue;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
    }
}

