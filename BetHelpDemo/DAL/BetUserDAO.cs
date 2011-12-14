using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SeoWebSite.DBUtility;//请先添加引用
namespace SeoWebSite.DAL
{
	/// <summary>
	/// 数据访问类betuser。
	/// </summary>
	public class BetUserDAO
	{
		public BetUserDAO()
		{}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from betuser");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar,50)};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SeoWebSite.Model.BetUser model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into betuser(");
			strSql.Append("id,username,password,truename,age,sex,qq,mobile,email)");
			strSql.Append(" values (");
			strSql.Append("@id,@username,@password,@truename,@age,@sex,@qq,@mobile,@email)");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar,20),
					new SqlParameter("@username", SqlDbType.NVarChar,16),
					new SqlParameter("@password", SqlDbType.VarChar,32),
					new SqlParameter("@truename", SqlDbType.NVarChar,32),
					new SqlParameter("@age", SqlDbType.Int,4),
					new SqlParameter("@sex", SqlDbType.NVarChar,1),
					new SqlParameter("@qq", SqlDbType.VarChar,16),
					new SqlParameter("@mobile", SqlDbType.VarChar,16),
					new SqlParameter("@email", SqlDbType.VarChar,200)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.username;
			parameters[2].Value = model.password;
			parameters[3].Value = model.truename;
			parameters[4].Value = model.age;
			parameters[5].Value = model.sex;
			parameters[6].Value = model.qq;
			parameters[7].Value = model.mobile;
			parameters[8].Value = model.email;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(SeoWebSite.Model.BetUser model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update betuser set ");
			strSql.Append("username=@username,");
			strSql.Append("password=@password,");
			strSql.Append("truename=@truename,");
			strSql.Append("age=@age,");
			strSql.Append("sex=@sex,");
			strSql.Append("qq=@qq,");
			strSql.Append("mobile=@mobile,");
			strSql.Append("email=@email");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar,20),
					new SqlParameter("@username", SqlDbType.NVarChar,16),
					new SqlParameter("@password", SqlDbType.VarChar,32),
					new SqlParameter("@truename", SqlDbType.NVarChar,32),
					new SqlParameter("@age", SqlDbType.Int,4),
					new SqlParameter("@sex", SqlDbType.NVarChar,1),
					new SqlParameter("@qq", SqlDbType.VarChar,16),
					new SqlParameter("@mobile", SqlDbType.VarChar,16),
					new SqlParameter("@email", SqlDbType.VarChar,200)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.username;
			parameters[2].Value = model.password;
			parameters[3].Value = model.truename;
			parameters[4].Value = model.age;
			parameters[5].Value = model.sex;
			parameters[6].Value = model.qq;
			parameters[7].Value = model.mobile;
			parameters[8].Value = model.email;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from betuser ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar,50)};
			parameters[0].Value = id;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SeoWebSite.Model.BetUser GetModel(string id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,username,password,truename,age,sex,qq,mobile,email from betuser ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar,50)};
			parameters[0].Value = id;

			SeoWebSite.Model.BetUser model=new SeoWebSite.Model.BetUser();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				model.id=ds.Tables[0].Rows[0]["id"].ToString();
				model.username=ds.Tables[0].Rows[0]["username"].ToString();
				model.password=ds.Tables[0].Rows[0]["password"].ToString();
				model.truename=ds.Tables[0].Rows[0]["truename"].ToString();
				if(ds.Tables[0].Rows[0]["age"].ToString()!="")
				{
					model.age=int.Parse(ds.Tables[0].Rows[0]["age"].ToString());
				}
				model.sex=ds.Tables[0].Rows[0]["sex"].ToString();
				model.qq=ds.Tables[0].Rows[0]["qq"].ToString();
				model.mobile=ds.Tables[0].Rows[0]["mobile"].ToString();
				model.email=ds.Tables[0].Rows[0]["email"].ToString();
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
			strSql.Append("select id,username,password,truename,age,sex,qq,mobile,email ");
			strSql.Append(" FROM betuser ");
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
			strSql.Append(" id,username,password,truename,age,sex,qq,mobile,email ");
			strSql.Append(" FROM betuser ");
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
			parameters[0].Value = "betuser";
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

