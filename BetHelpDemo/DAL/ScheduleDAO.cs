using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SeoWebSite.DBUtility;//请先添加引用
namespace SeoWebSite.DAL
{
	/// <summary>
	/// 数据访问类Schedule。
	/// </summary>
	public class ScheduleDAO
	{
		public ScheduleDAO()
		{}
		#region  成员方法

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
		public int Add(SeoWebSite.Model.Schedule1 model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Schedule(");
			strSql.Append("ScheduleID,Data,Date)");
			strSql.Append(" values (");
			strSql.Append("@ScheduleID,@Data,@Date)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ScheduleID", SqlDbType.Int,4),
					new SqlParameter("@Data", SqlDbType.VarChar,1000),
					new SqlParameter("@Date", SqlDbType.Date,3)};
			parameters[0].Value = model.ScheduleID;
			parameters[1].Value = model.Data;
			parameters[2].Value = model.Date;

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
		public void Update(SeoWebSite.Model.Schedule1 model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Schedule set ");
			strSql.Append("ScheduleID=@ScheduleID,");
			strSql.Append("Data=@Data,");
			strSql.Append("Date=@Date");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@ScheduleID", SqlDbType.Int,4),
					new SqlParameter("@Data", SqlDbType.VarChar,1000),
					new SqlParameter("@Date", SqlDbType.Date,3)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.ScheduleID;
			parameters[2].Value = model.Data;
			parameters[3].Value = model.Date;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Schedule ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SeoWebSite.Model.Schedule1 GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,ScheduleID,Data,Date from Schedule ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			SeoWebSite.Model.Schedule1 model=new SeoWebSite.Model.Schedule1();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ScheduleID"].ToString()!="")
				{
					model.ScheduleID=int.Parse(ds.Tables[0].Rows[0]["ScheduleID"].ToString());
				}
				model.Data=ds.Tables[0].Rows[0]["Data"].ToString();
				if(ds.Tables[0].Rows[0]["Date"].ToString()!="")
				{
					model.Date=DateTime.Parse(ds.Tables[0].Rows[0]["Date"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SeoWebSite.Model.Schedule1 GetTopOne()
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top(1) s.* from Schedule s where not exists (select o.scheduleid from odds1x2history o where o.scheduleid=s.ScheduleID)");
            SeoWebSite.Model.Schedule1 model = new SeoWebSite.Model.Schedule1();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ScheduleID"].ToString() != "")
                {
                    model.ScheduleID = int.Parse(ds.Tables[0].Rows[0]["ScheduleID"].ToString());
                }
                model.Data = ds.Tables[0].Rows[0]["Data"].ToString();
                if (ds.Tables[0].Rows[0]["Date"].ToString() != "")
                {
                    model.Date = DateTime.Parse(ds.Tables[0].Rows[0]["Date"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个有数据未分析对象实体
        /// </summary>
        public SeoWebSite.Model.Schedule1 GetTopOne_NoExp()
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top(1) s.* from Schedule s where s.ScheduleID in (select o.scheduleid from odds1x2history o where o.scheduleid=s.ScheduleID) and not exists (select b.id from BetExp b where b.id=s.ScheduleID)");
            SeoWebSite.Model.Schedule1 model = new SeoWebSite.Model.Schedule1();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ScheduleID"].ToString() != "")
                {
                    model.ScheduleID = int.Parse(ds.Tables[0].Rows[0]["ScheduleID"].ToString());
                }
                model.Data = ds.Tables[0].Rows[0]["Data"].ToString();
                if (ds.Tables[0].Rows[0]["Date"].ToString() != "")
                {
                    model.Date = DateTime.Parse(ds.Tables[0].Rows[0]["Date"].ToString());
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
			strSql.Append("select id,scheduletypeid,Data,Date,home,away ");
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
			strSql.Append(" id,ScheduleID,Data,Date ");
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
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  成员方法

        public bool ExistsSchedule(int scheduleid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Schedule");
            strSql.Append(" where Scheduleid=@Scheduleid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Scheduleid", SqlDbType.Int,10)};
            parameters[0].Value = scheduleid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public DataSet GetList(string strWhere, int start, int end)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("WITH TT AS (SELECT ROW_NUMBER() OVER (order by date)as RowNumber FROM Schedule");
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
            strSql.Append("select count(*) from Schedule");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            return int.Parse(DbHelperSQL.Query(strSql.ToString()).Tables[0].Rows[0][0].ToString());
        }

        public void UpdateState(int p, bool p_2)
        {
            throw new NotImplementedException();
        }

        public DataSet statOddsHistory(string whereStr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append("sumwin=sum(case when a.home>a.away then 1 else 0 end),");
            strSql.Append("sumdraw=sum(case when a.home=a.away then 1 else 0 end),");
            strSql.Append("sumlost=sum(case when a.home<a.away then 1 else 0 end),");
            strSql.Append("count(*) totalCount from");
            strSql.Append(" Schedule a join Odds b on a.id=b.scheduleid and a.updated=1");
            strSql.Append(" where " + whereStr);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet queryOddsHistory(string whereStr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select data,s_win,s_draw,s_lost,e_win,e_draw,e_lost");
            strSql.Append(" from");
            strSql.Append(" Schedule a join Odds b on a.id=b.scheduleid and a.updated=1");
            strSql.Append(" where " + whereStr);
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}

