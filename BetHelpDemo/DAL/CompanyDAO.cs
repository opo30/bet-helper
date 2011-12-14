using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SeoWebSite.DAL;
using SeoWebSite.DBUtility;//Please add references
namespace SeoWebSite.DAL
{
	/// <summary>
	/// 数据访问类:company
	/// </summary>
	public partial class CompanyDAO
	{
		public CompanyDAO()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "company"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from company");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SeoWebSite.Model.Company model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into company(");
			strSql.Append("id,fullname,name,isprimary,isexchange)");
			strSql.Append(" values (");
			strSql.Append("@id,@fullname,@name,@isprimary,@isexchange)");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@fullname", SqlDbType.NVarChar,100),
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@isprimary", SqlDbType.Bit,1),
					new SqlParameter("@isexchange", SqlDbType.Bit,1)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.fullname;
			parameters[2].Value = model.name;
			parameters[3].Value = model.isprimary;
			parameters[4].Value = model.isexchange;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SeoWebSite.Model.Company model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update company set ");
			strSql.Append("fullname=@fullname,");
			strSql.Append("name=@name,");
			strSql.Append("isprimary=@isprimary,");
			strSql.Append("isexchange=@isexchange");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@fullname", SqlDbType.NVarChar,100),
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@isprimary", SqlDbType.Bit,1),
					new SqlParameter("@isexchange", SqlDbType.Bit,1),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.fullname;
			parameters[1].Value = model.name;
			parameters[2].Value = model.isprimary;
			parameters[3].Value = model.isexchange;
			parameters[4].Value = model.id;

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
			strSql.Append("delete from company ");
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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from company ");
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
		public SeoWebSite.Model.Company GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,fullname,name,isprimary,isexchange from company ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			SeoWebSite.Model.Company model=new SeoWebSite.Model.Company();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"]!=null && ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["fullname"]!=null && ds.Tables[0].Rows[0]["fullname"].ToString()!="")
				{
					model.fullname=ds.Tables[0].Rows[0]["fullname"].ToString();
				}
				if(ds.Tables[0].Rows[0]["name"]!=null && ds.Tables[0].Rows[0]["name"].ToString()!="")
				{
					model.name=ds.Tables[0].Rows[0]["name"].ToString();
				}
				if(ds.Tables[0].Rows[0]["isprimary"]!=null && ds.Tables[0].Rows[0]["isprimary"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["isprimary"].ToString()=="1")||(ds.Tables[0].Rows[0]["isprimary"].ToString().ToLower()=="true"))
					{
						model.isprimary=true;
					}
					else
					{
						model.isprimary=false;
					}
				}
				if(ds.Tables[0].Rows[0]["isexchange"]!=null && ds.Tables[0].Rows[0]["isexchange"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["isexchange"].ToString()=="1")||(ds.Tables[0].Rows[0]["isexchange"].ToString().ToLower()=="true"))
					{
						model.isexchange=true;
					}
					else
					{
						model.isexchange=false;
					}
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
			strSql.Append("select id,fullname,name,isprimary,isexchange ");
			strSql.Append(" FROM company ");
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
			strSql.Append(" id,fullname,name,isprimary,isexchange ");
			strSql.Append(" FROM company ");
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
			parameters[0].Value = "company";
			parameters[1].Value = "id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
	}
}

