using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SeoWebSite.DBUtility;//请先添加引用
namespace SeoWebSite.DAL
{
	/// <summary>
	/// 数据访问类bettingline。
	/// </summary>
	public class BettingLineDAO
	{
		public BettingLineDAO()
		{}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from bettingline");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar,50)};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SeoWebSite.Model.BettingLine model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into bettingline(");
			strSql.Append("id,betmoney,returnmoney,profit,state,formulaid,userid)");
			strSql.Append(" values (");
			strSql.Append("@id,@betmoney,@returnmoney,@profit,@state,@formulaid,@userid)");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar,20),
					new SqlParameter("@betmoney", SqlDbType.Decimal,9),
					new SqlParameter("@returnmoney", SqlDbType.Decimal,9),
					new SqlParameter("@profit", SqlDbType.Decimal,9),
					new SqlParameter("@state", SqlDbType.NVarChar,50),
					new SqlParameter("@formulaid", SqlDbType.VarChar,20),
					new SqlParameter("@userid", SqlDbType.VarChar,20)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.betmoney;
			parameters[2].Value = model.returnmoney;
			parameters[3].Value = model.profit;
			parameters[4].Value = model.state;
			parameters[5].Value = model.formulaid;
			parameters[6].Value = model.userid;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(SeoWebSite.Model.BettingLine model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update bettingline set ");
			strSql.Append("betmoney=@betmoney,");
			strSql.Append("returnmoney=@returnmoney,");
			strSql.Append("profit=@profit,");
			strSql.Append("state=@state,");
			strSql.Append("formulaid=@formulaid,");
			strSql.Append("userid=@userid");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar,20),
					new SqlParameter("@betmoney", SqlDbType.Decimal,9),
					new SqlParameter("@returnmoney", SqlDbType.Decimal,9),
					new SqlParameter("@profit", SqlDbType.Decimal,9),
					new SqlParameter("@state", SqlDbType.NVarChar,50),
					new SqlParameter("@formulaid", SqlDbType.VarChar,20),
					new SqlParameter("@userid", SqlDbType.VarChar,20)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.betmoney;
			parameters[2].Value = model.returnmoney;
			parameters[3].Value = model.profit;
			parameters[4].Value = model.state;
			parameters[5].Value = model.formulaid;
			parameters[6].Value = model.userid;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from bettingline ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar,50)};
			parameters[0].Value = id;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SeoWebSite.Model.BettingLine GetModel(string id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,betmoney,returnmoney,profit,state,formulaid,userid from bettingline ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar,50)};
			parameters[0].Value = id;

			SeoWebSite.Model.BettingLine model=new SeoWebSite.Model.BettingLine();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				model.id=ds.Tables[0].Rows[0]["id"].ToString();
				if(ds.Tables[0].Rows[0]["betmoney"].ToString()!="")
				{
					model.betmoney=decimal.Parse(ds.Tables[0].Rows[0]["betmoney"].ToString());
				}
				if(ds.Tables[0].Rows[0]["returnmoney"].ToString()!="")
				{
					model.returnmoney=decimal.Parse(ds.Tables[0].Rows[0]["returnmoney"].ToString());
				}
				if(ds.Tables[0].Rows[0]["profit"].ToString()!="")
				{
					model.profit=decimal.Parse(ds.Tables[0].Rows[0]["profit"].ToString());
				}
				model.state=ds.Tables[0].Rows[0]["state"].ToString();
				model.formulaid=ds.Tables[0].Rows[0]["formulaid"].ToString();
				model.userid=ds.Tables[0].Rows[0]["userid"].ToString();
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
			strSql.Append("select id,name,betmoney,returnmoney,profit,state,formulaid,userid ");
			strSql.Append(" FROM bettingline ");
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
			strSql.Append(" id,betmoney,returnmoney,profit,state,formulaid,userid ");
			strSql.Append(" FROM bettingline ");
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
			parameters[0].Value = "bettingline";
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

