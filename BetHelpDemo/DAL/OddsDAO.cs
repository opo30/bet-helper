using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SeoWebSite.DAL;
using SeoWebSite.DBUtility;//Please add references
namespace SeoWebSite.DAL
{
	/// <summary>
	/// 数据访问类:odds
	/// </summary>
	public partial class OddsDAO
	{
		public OddsDAO()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "odds"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from odds");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SeoWebSite.Model.Odds model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into odds(");
			strSql.Append("id,companyid,scheduleid,s_win,s_draw,s_lost,s_winper,s_drawper,s_lostper,s_return,e_win,e_draw,e_lost,e_winper,e_drawper,e_lostper,e_return,winkelly,drawkelly,lostkelly,lastupdatetime)");
			strSql.Append(" values (");
            strSql.Append("@id,@companyid,@scheduleid,@s_win,@s_draw,@s_lost,@s_winper,@s_drawper,@s_lostper,@s_return,@e_win,@e_draw,@e_lost,@e_winper,@e_drawper,@e_lostper,@e_return,@winkelly,@drawkelly,@lostkelly,@lastupdatetime)");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@companyid", SqlDbType.Int,4),
                    new SqlParameter("@scheduleid", SqlDbType.Int,4),
					new SqlParameter("@s_win", SqlDbType.Float,8),
					new SqlParameter("@s_draw", SqlDbType.Float,8),
					new SqlParameter("@s_lost", SqlDbType.Float,8),
					new SqlParameter("@s_winper", SqlDbType.Float,8),
					new SqlParameter("@s_drawper", SqlDbType.Float,8),
					new SqlParameter("@s_lostper", SqlDbType.Float,8),
					new SqlParameter("@s_return", SqlDbType.Float,8),
					new SqlParameter("@e_win", SqlDbType.Float,8),
					new SqlParameter("@e_draw", SqlDbType.Float,8),
					new SqlParameter("@e_lost", SqlDbType.Float,8),
					new SqlParameter("@e_winper", SqlDbType.Float,8),
					new SqlParameter("@e_drawper", SqlDbType.Float,8),
					new SqlParameter("@e_lostper", SqlDbType.Float,8),
					new SqlParameter("@e_return", SqlDbType.Float,8),
					new SqlParameter("@winkelly", SqlDbType.Float,8),
					new SqlParameter("@drawkelly", SqlDbType.Float,8),
					new SqlParameter("@lostkelly", SqlDbType.Float,8),
					new SqlParameter("@lastupdatetime", SqlDbType.DateTime)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.companyid;
            parameters[2].Value = model.scheduleid;
			parameters[3].Value = model.s_win;
			parameters[4].Value = model.s_draw;
			parameters[5].Value = model.s_lost;
			parameters[6].Value = model.s_winper;
			parameters[7].Value = model.s_drawper;
			parameters[8].Value = model.s_lostper;
			parameters[9].Value = model.s_return;
			parameters[10].Value = model.e_win;
			parameters[11].Value = model.e_draw;
			parameters[12].Value = model.e_lost;
			parameters[13].Value = model.e_winper;
			parameters[14].Value = model.e_drawper;
			parameters[15].Value = model.e_lostper;
			parameters[16].Value = model.e_return;
			parameters[17].Value = model.winkelly;
			parameters[18].Value = model.drawkelly;
			parameters[19].Value = model.lostkelly;
			parameters[20].Value = model.lastupdatetime;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SeoWebSite.Model.Odds model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update odds set ");
			strSql.Append("companyid=@companyid,");
			strSql.Append("s_win=@s_win,");
			strSql.Append("s_draw=@s_draw,");
			strSql.Append("s_lost=@s_lost,");
			strSql.Append("s_winper=@s_winper,");
			strSql.Append("s_drawper=@s_drawper,");
			strSql.Append("s_lostper=@s_lostper,");
			strSql.Append("s_return=@s_return,");
			strSql.Append("e_win=@e_win,");
			strSql.Append("e_draw=@e_draw,");
			strSql.Append("e_lost=@e_lost,");
			strSql.Append("e_winper=@e_winper,");
			strSql.Append("e_drawper=@e_drawper,");
			strSql.Append("e_lostper=@e_lostper,");
			strSql.Append("e_return=@e_return,");
			strSql.Append("winkelly=@winkelly,");
			strSql.Append("drawkelly=@drawkelly,");
			strSql.Append("lostkelly=@lostkelly,");
			strSql.Append("lastupdatetime=@lastupdatetime");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@companyid", SqlDbType.Int,4),
					new SqlParameter("@s_win", SqlDbType.Float,8),
					new SqlParameter("@s_draw", SqlDbType.Float,8),
					new SqlParameter("@s_lost", SqlDbType.Float,8),
					new SqlParameter("@s_winper", SqlDbType.Float,8),
					new SqlParameter("@s_drawper", SqlDbType.Float,8),
					new SqlParameter("@s_lostper", SqlDbType.Float,8),
					new SqlParameter("@s_return", SqlDbType.Float,8),
					new SqlParameter("@e_win", SqlDbType.Float,8),
					new SqlParameter("@e_draw", SqlDbType.Float,8),
					new SqlParameter("@e_lost", SqlDbType.Float,8),
					new SqlParameter("@e_winper", SqlDbType.Float,8),
					new SqlParameter("@e_drawper", SqlDbType.Float,8),
					new SqlParameter("@e_lostper", SqlDbType.Float,8),
					new SqlParameter("@e_return", SqlDbType.Float,8),
					new SqlParameter("@winkelly", SqlDbType.Float,8),
					new SqlParameter("@drawkelly", SqlDbType.Float,8),
					new SqlParameter("@lostkelly", SqlDbType.Float,8),
					new SqlParameter("@lastupdatetime", SqlDbType.DateTime),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.companyid;
			parameters[1].Value = model.s_win;
			parameters[2].Value = model.s_draw;
			parameters[3].Value = model.s_lost;
			parameters[4].Value = model.s_winper;
			parameters[5].Value = model.s_drawper;
			parameters[6].Value = model.s_lostper;
			parameters[7].Value = model.s_return;
			parameters[8].Value = model.e_win;
			parameters[9].Value = model.e_draw;
			parameters[10].Value = model.e_lost;
			parameters[11].Value = model.e_winper;
			parameters[12].Value = model.e_drawper;
			parameters[13].Value = model.e_lostper;
			parameters[14].Value = model.e_return;
			parameters[15].Value = model.winkelly;
			parameters[16].Value = model.drawkelly;
			parameters[17].Value = model.lostkelly;
			parameters[18].Value = model.lastupdatetime;
			parameters[19].Value = model.id;

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
			strSql.Append("delete from odds ");
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
			strSql.Append("delete from odds ");
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
		public SeoWebSite.Model.Odds GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,companyid,s_win,s_draw,s_lost,s_winper,s_drawper,s_lostper,s_return,e_win,e_draw,e_lost,e_winper,e_drawper,e_lostper,e_return,winkelly,drawkelly,lostkelly,lastupdatetime from odds ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			SeoWebSite.Model.Odds model=new SeoWebSite.Model.Odds();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"]!=null && ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["companyid"]!=null && ds.Tables[0].Rows[0]["companyid"].ToString()!="")
				{
					model.companyid=int.Parse(ds.Tables[0].Rows[0]["companyid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["s_win"]!=null && ds.Tables[0].Rows[0]["s_win"].ToString()!="")
				{
					model.s_win=decimal.Parse(ds.Tables[0].Rows[0]["s_win"].ToString());
				}
				if(ds.Tables[0].Rows[0]["s_draw"]!=null && ds.Tables[0].Rows[0]["s_draw"].ToString()!="")
				{
					model.s_draw=decimal.Parse(ds.Tables[0].Rows[0]["s_draw"].ToString());
				}
				if(ds.Tables[0].Rows[0]["s_lost"]!=null && ds.Tables[0].Rows[0]["s_lost"].ToString()!="")
				{
					model.s_lost=decimal.Parse(ds.Tables[0].Rows[0]["s_lost"].ToString());
				}
				if(ds.Tables[0].Rows[0]["s_winper"]!=null && ds.Tables[0].Rows[0]["s_winper"].ToString()!="")
				{
					model.s_winper=decimal.Parse(ds.Tables[0].Rows[0]["s_winper"].ToString());
				}
				if(ds.Tables[0].Rows[0]["s_drawper"]!=null && ds.Tables[0].Rows[0]["s_drawper"].ToString()!="")
				{
					model.s_drawper=decimal.Parse(ds.Tables[0].Rows[0]["s_drawper"].ToString());
				}
				if(ds.Tables[0].Rows[0]["s_lostper"]!=null && ds.Tables[0].Rows[0]["s_lostper"].ToString()!="")
				{
					model.s_lostper=decimal.Parse(ds.Tables[0].Rows[0]["s_lostper"].ToString());
				}
				if(ds.Tables[0].Rows[0]["s_return"]!=null && ds.Tables[0].Rows[0]["s_return"].ToString()!="")
				{
					model.s_return=decimal.Parse(ds.Tables[0].Rows[0]["s_return"].ToString());
				}
				if(ds.Tables[0].Rows[0]["e_win"]!=null && ds.Tables[0].Rows[0]["e_win"].ToString()!="")
				{
					model.e_win=decimal.Parse(ds.Tables[0].Rows[0]["e_win"].ToString());
				}
				if(ds.Tables[0].Rows[0]["e_draw"]!=null && ds.Tables[0].Rows[0]["e_draw"].ToString()!="")
				{
					model.e_draw=decimal.Parse(ds.Tables[0].Rows[0]["e_draw"].ToString());
				}
				if(ds.Tables[0].Rows[0]["e_lost"]!=null && ds.Tables[0].Rows[0]["e_lost"].ToString()!="")
				{
					model.e_lost=decimal.Parse(ds.Tables[0].Rows[0]["e_lost"].ToString());
				}
				if(ds.Tables[0].Rows[0]["e_winper"]!=null && ds.Tables[0].Rows[0]["e_winper"].ToString()!="")
				{
					model.e_winper=decimal.Parse(ds.Tables[0].Rows[0]["e_winper"].ToString());
				}
				if(ds.Tables[0].Rows[0]["e_drawper"]!=null && ds.Tables[0].Rows[0]["e_drawper"].ToString()!="")
				{
					model.e_drawper=decimal.Parse(ds.Tables[0].Rows[0]["e_drawper"].ToString());
				}
				if(ds.Tables[0].Rows[0]["e_lostper"]!=null && ds.Tables[0].Rows[0]["e_lostper"].ToString()!="")
				{
					model.e_lostper=decimal.Parse(ds.Tables[0].Rows[0]["e_lostper"].ToString());
				}
				if(ds.Tables[0].Rows[0]["e_return"]!=null && ds.Tables[0].Rows[0]["e_return"].ToString()!="")
				{
					model.e_return=decimal.Parse(ds.Tables[0].Rows[0]["e_return"].ToString());
				}
				if(ds.Tables[0].Rows[0]["winkelly"]!=null && ds.Tables[0].Rows[0]["winkelly"].ToString()!="")
				{
					model.winkelly=decimal.Parse(ds.Tables[0].Rows[0]["winkelly"].ToString());
				}
				if(ds.Tables[0].Rows[0]["drawkelly"]!=null && ds.Tables[0].Rows[0]["drawkelly"].ToString()!="")
				{
					model.drawkelly=decimal.Parse(ds.Tables[0].Rows[0]["drawkelly"].ToString());
				}
				if(ds.Tables[0].Rows[0]["lostkelly"]!=null && ds.Tables[0].Rows[0]["lostkelly"].ToString()!="")
				{
					model.lostkelly=decimal.Parse(ds.Tables[0].Rows[0]["lostkelly"].ToString());
				}
				if(ds.Tables[0].Rows[0]["lastupdatetime"]!=null && ds.Tables[0].Rows[0]["lastupdatetime"].ToString()!="")
				{
					model.lastupdatetime=DateTime.Parse(ds.Tables[0].Rows[0]["lastupdatetime"].ToString());
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
			strSql.Append("select id,companyid,s_win,s_draw,s_lost,s_winper,s_drawper,s_lostper,s_return,e_win,e_draw,e_lost,e_winper,e_drawper,e_lostper,e_return,winkelly,drawkelly,lostkelly,lastupdatetime ");
			strSql.Append(" FROM odds ");
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
			strSql.Append(" id,companyid,s_win,s_draw,s_lost,s_winper,s_drawper,s_lostper,s_return,e_win,e_draw,e_lost,e_winper,e_drawper,e_lostper,e_return,winkelly,drawkelly,lostkelly,lastupdatetime ");
			strSql.Append(" FROM odds ");
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
			parameters[0].Value = "odds";
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

