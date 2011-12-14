using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SeoWebSite.DBUtility;
using System.Data;

namespace SeoWebSite.DAL
{
    public class ForecastScriptsDAO
    {
        public ForecastScriptsDAO()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from forecastscripts");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SeoWebSite.Model.ForecastScripts model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into forecastscripts(");
			strSql.Append("name,content,remark,win,lost)");
			strSql.Append(" values (");
			strSql.Append("@name,@content,@remark,@win,@lost)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@content", SqlDbType.Text),
					new SqlParameter("@remark", SqlDbType.Text),
					new SqlParameter("@win", SqlDbType.Int,4),
					new SqlParameter("@lost", SqlDbType.Int,4)};
			parameters[0].Value = model.name;
			parameters[1].Value = model.content;
			parameters[2].Value = model.remark;
			parameters[3].Value = model.win;
			parameters[4].Value = model.lost;

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
		public void Update(SeoWebSite.Model.ForecastScripts model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update forecastscripts set ");
			strSql.Append("name=@name,");
			strSql.Append("content=@content,");
			strSql.Append("remark=@remark,");
			strSql.Append("win=@win,");
			strSql.Append("lost=@lost");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@content", SqlDbType.Text),
					new SqlParameter("@remark", SqlDbType.Text),
					new SqlParameter("@win", SqlDbType.Int,4),
					new SqlParameter("@lost", SqlDbType.Int,4)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.name;
			parameters[2].Value = model.content;
			parameters[3].Value = model.remark;
			parameters[4].Value = model.win;
			parameters[5].Value = model.lost;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from forecastscripts ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SeoWebSite.Model.ForecastScripts GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,name,content,remark,win,lost from forecastscripts ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			SeoWebSite.Model.ForecastScripts model=new SeoWebSite.Model.ForecastScripts();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				model.name=ds.Tables[0].Rows[0]["name"].ToString();
				model.content=ds.Tables[0].Rows[0]["content"].ToString();
				model.remark=ds.Tables[0].Rows[0]["remark"].ToString();
				if(ds.Tables[0].Rows[0]["win"].ToString()!="")
				{
					model.win=int.Parse(ds.Tables[0].Rows[0]["win"].ToString());
				}
				if(ds.Tables[0].Rows[0]["lost"].ToString()!="")
				{
					model.lost=int.Parse(ds.Tables[0].Rows[0]["lost"].ToString());
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
			strSql.Append("select id,name,content,remark,win,lost,resultwin,resultlost ");
			strSql.Append(" FROM forecastscripts ");
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
			strSql.Append(" id,name,content,remark,win,lost ");
			strSql.Append(" FROM forecastscripts ");
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
			parameters[0].Value = "forecastscripts";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  成员方法

        public void Increase(int id, string win, string victory)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update forecastscripts set ");
            strSql.Append(win + "=" + win + "+1,");
            strSql.Append(victory + "="+victory+"+1");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
    }
}
