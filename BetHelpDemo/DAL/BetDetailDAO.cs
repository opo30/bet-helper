using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SeoWebSite.DBUtility;//请先添加引用
namespace SeoWebSite.DAL
{
	/// <summary>
	/// 数据访问类betdetail。
	/// </summary>
	public class BetDetailDAO
	{
		public BetDetailDAO()
		{}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from betdetail");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar,50)};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SeoWebSite.Model.BetDetail model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into betdetail(");
			strSql.Append("id,itemid,name,formulaid,description)");
			strSql.Append(" values (");
			strSql.Append("@id,@itemid,@name,@formulaid,@description)");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar,20),
					new SqlParameter("@itemid", SqlDbType.VarChar,20),
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@formulaid", SqlDbType.VarChar,20),
					new SqlParameter("@description", SqlDbType.NVarChar,1000)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.itemid;
			parameters[2].Value = model.name;
			parameters[3].Value = model.formulaid;
			parameters[4].Value = model.description;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(SeoWebSite.Model.BetDetail model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update betdetail set ");
			strSql.Append("itemid=@itemid,");
			strSql.Append("name=@name,");
			strSql.Append("formulaid=@formulaid,");
			strSql.Append("description=@description");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar,20),
					new SqlParameter("@itemid", SqlDbType.VarChar,20),
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@formulaid", SqlDbType.VarChar,20),
					new SqlParameter("@description", SqlDbType.NVarChar,1000)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.itemid;
			parameters[2].Value = model.name;
			parameters[3].Value = model.formulaid;
			parameters[4].Value = model.description;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from betdetail ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar,50)};
			parameters[0].Value = id;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SeoWebSite.Model.BetDetail GetModel(string id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,itemid,name,formulaid,description from betdetail ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar,50)};
			parameters[0].Value = id;

			SeoWebSite.Model.BetDetail model=new SeoWebSite.Model.BetDetail();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				model.id=ds.Tables[0].Rows[0]["id"].ToString();
				model.itemid=ds.Tables[0].Rows[0]["itemid"].ToString();
				model.name=ds.Tables[0].Rows[0]["name"].ToString();
				model.formulaid=ds.Tables[0].Rows[0]["formulaid"].ToString();
				model.description=ds.Tables[0].Rows[0]["description"].ToString();
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
			strSql.Append("select id,itemid,name,formulaid,description ");
			strSql.Append(" FROM betdetail ");
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
			strSql.Append(" id,itemid,name,formulaid,description ");
			strSql.Append(" FROM betdetail ");
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
			parameters[0].Value = "betdetail";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  成员方法
	}
}

