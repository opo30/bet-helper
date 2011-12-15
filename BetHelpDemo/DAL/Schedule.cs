using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SeoWebSite.DBUtility;//Please add references
namespace SeoWebSite.DAL
{
	/// <summary>
	/// 数据访问类:Schedule
	/// </summary>
	public class Schedule
	{
		public Schedule()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "Schedule"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Schedule");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SeoWebSite.Model.Schedule model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Schedule(");
            strSql.Append("id,data,updated,date,home,away,halfhome,halfaway,h_teamid,g_teamid,scheduleTypeID)");
			strSql.Append(" values (");
            strSql.Append("@id,@data,@updated,@date,@home,@away,@halfhome,@halfaway,@h_teamid,@g_teamid,@scheduleTypeID)");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,8),
					new SqlParameter("@data", SqlDbType.NVarChar,1000),
					new SqlParameter("@updated", SqlDbType.Bit,1),
					new SqlParameter("@date", SqlDbType.Date,3),
					new SqlParameter("@home", SqlDbType.Int,4),
					new SqlParameter("@away", SqlDbType.Int,4),
                    new SqlParameter("@halfhome", SqlDbType.Int,4),
					new SqlParameter("@halfaway", SqlDbType.Int,4),
                    new SqlParameter("@h_teamid", SqlDbType.Int,4),
					new SqlParameter("@g_teamid", SqlDbType.Int,4),
                    new SqlParameter("@scheduleTypeID", SqlDbType.Int,4)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.data;
			parameters[2].Value = model.updated;
			parameters[3].Value = model.date;
			parameters[4].Value = model.home;
			parameters[5].Value = model.away;
            parameters[6].Value = model.halfhome;
            parameters[7].Value = model.halfaway;
            parameters[8].Value = model.h_teamid;
            parameters[9].Value = model.g_teamid;
            parameters[10].Value = model.scheduleTypeID;
			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SeoWebSite.Model.Schedule model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Schedule set ");
			strSql.Append("data=@data,");
			strSql.Append("updated=@updated,");
			strSql.Append("date=@date,");
			strSql.Append("home=@home,");
			strSql.Append("away=@away");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@data", SqlDbType.NVarChar,1000),
					new SqlParameter("@updated", SqlDbType.Bit,1),
					new SqlParameter("@date", SqlDbType.Date,3),
					new SqlParameter("@home", SqlDbType.Int,4),
					new SqlParameter("@away", SqlDbType.Int,4)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.data;
			parameters[2].Value = model.updated;
			parameters[3].Value = model.date;
			parameters[4].Value = model.home;
			parameters[5].Value = model.away;

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
        /// 设置是否更新
        /// </summary>
        public void SetUpdated(string scheduleid, bool isUpdated)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Schedule set ");
            strSql.Append("updated=@updated");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@updated", SqlDbType.Bit,1),
                    new SqlParameter("@id", SqlDbType.Int)};
            parameters[0].Value = isUpdated;
            parameters[1].Value = scheduleid;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Schedule ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
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
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Schedule ");
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
		public SeoWebSite.Model.Schedule GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 id,data,updated,date,home,away,halfhome,halfaway from Schedule ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			SeoWebSite.Model.Schedule model=new SeoWebSite.Model.Schedule();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				model.data=ds.Tables[0].Rows[0]["data"].ToString();
				if(ds.Tables[0].Rows[0]["updated"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["updated"].ToString()=="1")||(ds.Tables[0].Rows[0]["updated"].ToString().ToLower()=="true"))
					{
						model.updated=true;
					}
					else
					{
						model.updated=false;
					}
				}
				if(ds.Tables[0].Rows[0]["date"].ToString()!="")
				{
					model.date=DateTime.Parse(ds.Tables[0].Rows[0]["date"].ToString());
				}
				if(ds.Tables[0].Rows[0]["home"].ToString()!="")
				{
					model.home=int.Parse(ds.Tables[0].Rows[0]["home"].ToString());
				}
				if(ds.Tables[0].Rows[0]["away"].ToString()!="")
				{
					model.away=int.Parse(ds.Tables[0].Rows[0]["away"].ToString());
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
			strSql.Append("select id,data,updated,date,home,away ");
			strSql.Append(" FROM Schedule ");
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
			strSql.Append(" id,data,updated,date,home,away ");
			strSql.Append(" FROM Schedule ");
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
			parameters[0].Value = "Schedule";
			parameters[1].Value = "";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method

        public int GetTotalCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from Schedule");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            return int.Parse(DbHelperSQL.Query(strSql.ToString()).Tables[0].Rows[0][0].ToString());
        }

        public DataSet GetList(string strWhere, int start, int end)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("WITH TT AS (SELECT Schedule.*,scheduletype.data scheduletype,ROW_NUMBER() OVER (order by date)as RowNumber FROM Schedule join scheduletype on Schedule.scheduletypeid=scheduletype.id");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(") SELECT * FROM TT WHERE RowNumber between " + start + " and " + end);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetScheduleIDList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id ");
            strSql.Append(" FROM Schedule ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}

