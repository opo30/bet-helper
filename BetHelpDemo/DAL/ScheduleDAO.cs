using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SeoWebSite.DBUtility;//Please add references
namespace SeoWebSite.DAL
{
	/// <summary>
	/// ���ݷ�����:Schedule
	/// </summary>
	public class ScheduleDAO
	{
		public ScheduleDAO()
		{}
		#region  Method

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "Schedule"); 
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
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
		/// ����һ������
		/// </summary>
		public void Add(SeoWebSite.Model.Schedule model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Schedule(");
            strSql.Append("id,data,updated,date,home,away,halfhome,halfaway,h_teamid,g_teamid,sclassid)");
			strSql.Append(" values (");
            strSql.Append("@id,@data,@updated,@date,@home,@away,@halfhome,@halfaway,@h_teamid,@g_teamid,@sclassid)");
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
                    new SqlParameter("@sclassid", SqlDbType.Int,4)};
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
            parameters[10].Value = model.sclassid;
			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// ����һ������
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
        /// �����Ƿ����
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
		/// ɾ��һ������
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
		/// ɾ��һ������
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
		/// �õ�һ������ʵ��
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
		/// ��������б�
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
		/// ���ǰ��������
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
        /// ��ҳ��ȡ�����б�
        /// </summary>
         * */
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
            parameters[1].Value = "*";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}

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
            strSql.Append("WITH TT AS (SELECT Schedule.*,scheduleclass.data scheduletype,ROW_NUMBER() OVER (order by date)as RowNumber FROM Schedule join scheduleclass on Schedule.sclassid=scheduleclass.id");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(") SELECT * FROM TT WHERE RowNumber between " + start + " and " + end);
            return DbHelperSQL.Query(strSql.ToString(),999);
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

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public SeoWebSite.Model.Schedule GetTopOne()
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top(1) s.* from Schedule s where not exists (select o.scheduleid from odds1x2history o where o.scheduleid=s.ScheduleID)");
            SeoWebSite.Model.Schedule model = new SeoWebSite.Model.Schedule();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sclassid"].ToString() != "")
                {
                    model.sclassid= int.Parse(ds.Tables[0].Rows[0]["sclassid"].ToString());
                }
                model.data = ds.Tables[0].Rows[0]["Data"].ToString();
                if (ds.Tables[0].Rows[0]["Date"].ToString() != "")
                {
                    model.date = DateTime.Parse(ds.Tables[0].Rows[0]["Date"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// �õ�һ��������δ��������ʵ��
        /// </summary>
        public SeoWebSite.Model.Schedule GetTopOne_NoExp()
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top(1) s.* from Schedule s where s.ScheduleID in (select o.scheduleid from odds1x2history o where o.scheduleid=s.ScheduleID) and not exists (select b.id from BetExp b where b.id=s.ScheduleID)");
            SeoWebSite.Model.Schedule model = new SeoWebSite.Model.Schedule();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sclassid"].ToString() != "")
                {
                    model.sclassid = int.Parse(ds.Tables[0].Rows[0]["sclassid"].ToString());
                }
                model.data = ds.Tables[0].Rows[0]["Data"].ToString();
                if (ds.Tables[0].Rows[0]["Date"].ToString() != "")
                {
                    model.date = DateTime.Parse(ds.Tables[0].Rows[0]["Date"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

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

        public DataSet statOddsHistory(string cclassid,string sclassid,string whereStr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select perwin=100.0*sum(case when a.home>a.away then 1 else 0 end)/count(a.id),");
            strSql.Append("perdraw=100.0*sum(case when a.home=a.away then 1 else 0 end)/count(a.id),");
            strSql.Append("perlost=100.0*sum(case when a.home<a.away then 1 else 0 end)/count(a.id),");
            strSql.Append("avgscore=avg(1.0*(a.home+a.away)),");
            strSql.Append("count(a.id) totalCount from");
            strSql.Append(" Schedule a join (select scheduleid,count(*) scount from Odds");
            strSql.Append(" where " + whereStr);
            strSql.Append(" group by scheduleid) b on a.id=b.scheduleid join scheduleclass c on a.sclassid=c.id");
            strSql.Append(" where a.updated=1 and b.scount>0");
            if (!String.IsNullOrEmpty(cclassid))
            {
                strSql.Append(" and c.cclassid=" + cclassid);
            }
            else if (!String.IsNullOrEmpty(sclassid))
            {
                strSql.Append(" and a.sclassid=" + sclassid);
            }
            
            return DbHelperSQL.Query(strSql.ToString(), 999);
        }

        public DataSet statOddsHistory1(string cclassid, string sclassid, string whereStr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select avg(win) perwin,avg(draw) perdraw,avg(lost) perlost,SUM(scount) totalCount from ");
            strSql.Append("(select companyid, count(*) scount,");
            strSql.Append("win=100.0*sum(case when a.home>a.away then 1 else 0 end)/count(a.id),");
            strSql.Append("draw=100.0*sum(case when a.home=a.away then 1 else 0 end)/count(a.id),");
            strSql.Append("lost=100.0*sum(case when a.home<a.away then 1 else 0 end)/count(a.id) ");
            strSql.Append("from Odds b join schedule a on b.scheduleid=a.id join scheduleclass c on a.sclassid=c.id");
            strSql.Append(" where " + whereStr);
            strSql.Append(" and a.updated=1");
            if (!String.IsNullOrEmpty(cclassid))
            {
                strSql.Append(" and c.cclassid=" + cclassid);
            }
            else if (!String.IsNullOrEmpty(sclassid))
            {
                strSql.Append(" and a.sclassid=" + sclassid);
            }
            strSql.Append(" group by companyid) d ");
            return DbHelperSQL.Query(strSql.ToString(), 999);
        }

        public DataSet statOddsHistory2(string cclassid, string sclassid, string whereStr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SUM(scount) totalCount,perwin=sum(case when win=100 then 1 else 0 end),perdraw=sum(case when draw=100 then 1 else 0 end),perlost=sum(case when lost=100 then 1 else 0 end) from ");
            strSql.Append("(select companyid, count(*) scount,");
            strSql.Append("win=100.0*sum(case when a.home>a.away then 1 else 0 end)/count(a.id),");
            strSql.Append("draw=100.0*sum(case when a.home=a.away then 1 else 0 end)/count(a.id),");
            strSql.Append("lost=100.0*sum(case when a.home<a.away then 1 else 0 end)/count(a.id) ");
            strSql.Append("from Odds b join schedule a on b.scheduleid=a.id join scheduleclass c on a.sclassid=c.id");
            strSql.Append(" where " + whereStr);
            strSql.Append(" and a.updated=1");
            if (!String.IsNullOrEmpty(cclassid))
            {
                strSql.Append(" and c.cclassid=" + cclassid);
            }
            else if (!String.IsNullOrEmpty(sclassid))
            {
                strSql.Append(" and a.sclassid=" + sclassid);
            }
            strSql.Append(" group by companyid) d ");
            return DbHelperSQL.Query(strSql.ToString(), 999);
        }

        public DataSet queryOddsHistory2(string cclassid, string sclassid, string whereStr, string selectStr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select e.id,e.fullname,e.name,e.isprimary,e.isexchange,result=case when d.win>90 then 3 when d.draw>90 then 1 when d.lost>90 then 0 end,d.scount," + selectStr + " from ");
            strSql.Append("(select companyid, count(*) scount,");
            strSql.Append("win=100.0*sum(case when a.home>a.away then 1 else 0 end)/count(a.id),");
            strSql.Append("draw=100.0*sum(case when a.home=a.away then 1 else 0 end)/count(a.id),");
            strSql.Append("lost=100.0*sum(case when a.home<a.away then 1 else 0 end)/count(a.id) ");
            strSql.Append("from Odds b join schedule a on b.scheduleid=a.id join scheduleclass c on a.sclassid=c.id");
            strSql.Append(" where " + whereStr);
            strSql.Append(" and a.updated=1");
            if (!String.IsNullOrEmpty(cclassid))
            {
                strSql.Append(" and c.cclassid=" + cclassid);
            }
            else if (!String.IsNullOrEmpty(sclassid))
            {
                strSql.Append(" and a.sclassid=" + sclassid);
            }
            strSql.Append(" group by companyid) d join company e on e.id=d.companyid and d.scount>1 and (d.win>90 or d.draw>90 or d.lost>90)");
            return DbHelperSQL.Query(strSql.ToString(), 999);
        }

        public DataSet statOddsHistory(string whereStr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select perwin=100.0*sum(case when a.home>a.away then 1 else 0 end)/count(a.id),");
            strSql.Append("perdraw=100.0*sum(case when a.home=a.away then 1 else 0 end)/count(a.id),");
            strSql.Append("perlost=100.0*sum(case when a.home<a.away then 1 else 0 end)/count(a.id),count(a.id) totalcount");
            strSql.Append(" from Schedule a join (select scheduleid,count(*) scount from Odds");
            strSql.Append(" where " + whereStr);
            strSql.Append(" group by scheduleid) b on a.id=b.scheduleid");
            strSql.Append(" where a.updated=1");
            return DbHelperSQL.Query(strSql.ToString(), 999);
        }

        public DataSet queryOddsHistory(string cclassid,string sclassid, string whereStr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.data sdata,scount,c.data sclass from Schedule a join (select scheduleid,count(*) scount from Odds");
            strSql.Append(" where " + whereStr);
            strSql.Append(" group by scheduleid) b on a.id=b.scheduleid");
            strSql.Append(" join scheduleclass c on a.sclassid=c.id");
            strSql.Append(" where a.updated=1");
            //if (String.IsNullOrEmpty(cclassid) && String.IsNullOrEmpty(sclassid))
            //{
            //    strSql.Append(" and b.scount>2");
            //}
            if (!String.IsNullOrEmpty(cclassid))
            {
                strSql.Append(" and c.cclassid=" + cclassid);
            }
            else if (!String.IsNullOrEmpty(sclassid))
            {
                strSql.Append(" and a.sclassid=" + sclassid);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet statOddsHistoryGroupByDate(string rangqiu,string whereStr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select c.date sdate,");
            strSql.Append("sumwin=sum(case when c.home-c.away>" + rangqiu + " then 1 else 0 end),");
            strSql.Append("sumdraw=sum(case when c.home-c.away=" + rangqiu + " then 1 else 0 end),");
            strSql.Append("sumlost=sum(case when c.home-c.away<" + rangqiu + " then 1 else 0 end),");
            strSql.Append("avgscore=avg(1.0*(c.home+c.away)),");
            strSql.Append("count(c.id) totalCount from ");
            strSql.Append("(select distinct a.id,a.home,a.away,a.date from");
            strSql.Append(" Schedule a join Odds b on a.id=b.scheduleid and a.updated=1");
            strSql.Append(" where " + whereStr + ") c group by c.date");
            return DbHelperSQL.Query(strSql.ToString(), 999);
        }



        public void UpdateState(int p, bool p_2)
        {
            throw new NotImplementedException();
        }

    }
}

