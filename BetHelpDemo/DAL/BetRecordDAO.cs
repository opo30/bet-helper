using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SeoWebSite.DBUtility;//请先添加引用
namespace SeoWebSite.DAL
{
	/// <summary>
	/// 数据访问类betrecord。
	/// </summary>
	public class BetRecordDAO
	{
		public BetRecordDAO()
		{}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from betrecord");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar,50)};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SeoWebSite.Model.BetRecord model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into betrecord(");
			strSql.Append("id,lineid,teamname,traditional,starttime,endtime,bettime,itemid,detailid,betmoney,returnmoney,result)");
			strSql.Append(" values (");
			strSql.Append("@id,@lineid,@teamname,@traditional,@starttime,@endtime,@bettime,@itemid,@detailid,@betmoney,@returnmoney,@result)");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar,20),
					new SqlParameter("@lineid", SqlDbType.VarChar,20),
					new SqlParameter("@teamname", SqlDbType.NVarChar,50),
					new SqlParameter("@traditional", SqlDbType.NVarChar,50),
					new SqlParameter("@starttime", SqlDbType.DateTime),
					new SqlParameter("@endtime", SqlDbType.DateTime),
					new SqlParameter("@bettime", SqlDbType.DateTime),
					new SqlParameter("@itemid", SqlDbType.VarChar,20),
					new SqlParameter("@detailid", SqlDbType.VarChar,20),
					new SqlParameter("@betmoney", SqlDbType.Decimal,9),
					new SqlParameter("@returnmoney", SqlDbType.Decimal,9),
					new SqlParameter("@result", SqlDbType.NVarChar,2)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.lineid;
			parameters[2].Value = model.teamname;
			parameters[3].Value = model.traditional;
			parameters[4].Value = model.starttime;
			parameters[5].Value = model.endtime;
			parameters[6].Value = model.bettime;
			parameters[7].Value = model.itemid;
			parameters[8].Value = model.detailid;
			parameters[9].Value = model.betmoney;
			parameters[10].Value = model.returnmoney;
			parameters[11].Value = model.result;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(SeoWebSite.Model.BetRecord model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update betrecord set ");
			strSql.Append("lineid=@lineid,");
			strSql.Append("teamname=@teamname,");
			strSql.Append("traditional=@traditional,");
			strSql.Append("starttime=@starttime,");
			strSql.Append("endtime=@endtime,");
			strSql.Append("bettime=@bettime,");
			strSql.Append("itemid=@itemid,");
			strSql.Append("detailid=@detailid,");
			strSql.Append("betmoney=@betmoney,");
			strSql.Append("returnmoney=@returnmoney,");
			strSql.Append("result=@result");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar,20),
					new SqlParameter("@lineid", SqlDbType.VarChar,20),
					new SqlParameter("@teamname", SqlDbType.NVarChar,50),
					new SqlParameter("@traditional", SqlDbType.NVarChar,50),
					new SqlParameter("@starttime", SqlDbType.DateTime),
					new SqlParameter("@endtime", SqlDbType.DateTime),
					new SqlParameter("@bettime", SqlDbType.DateTime),
					new SqlParameter("@itemid", SqlDbType.VarChar,20),
					new SqlParameter("@detailid", SqlDbType.VarChar,20),
					new SqlParameter("@betmoney", SqlDbType.Decimal,9),
					new SqlParameter("@returnmoney", SqlDbType.Decimal,9),
					new SqlParameter("@result", SqlDbType.NVarChar,2)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.lineid;
			parameters[2].Value = model.teamname;
			parameters[3].Value = model.traditional;
			parameters[4].Value = model.starttime;
			parameters[5].Value = model.endtime;
			parameters[6].Value = model.bettime;
			parameters[7].Value = model.itemid;
			parameters[8].Value = model.detailid;
			parameters[9].Value = model.betmoney;
			parameters[10].Value = model.returnmoney;
			parameters[11].Value = model.result;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from betrecord ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar,50)};
			parameters[0].Value = id;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SeoWebSite.Model.BetRecord GetModel(string id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,lineid,teamname,traditional,starttime,endtime,bettime,itemid,detailid,betmoney,returnmoney,result from betrecord ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar,50)};
			parameters[0].Value = id;

			SeoWebSite.Model.BetRecord model=new SeoWebSite.Model.BetRecord();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				model.id=ds.Tables[0].Rows[0]["id"].ToString();
				model.lineid=ds.Tables[0].Rows[0]["lineid"].ToString();
				model.teamname=ds.Tables[0].Rows[0]["teamname"].ToString();
				model.traditional=ds.Tables[0].Rows[0]["traditional"].ToString();
				if(ds.Tables[0].Rows[0]["starttime"].ToString()!="")
				{
					model.starttime=DateTime.Parse(ds.Tables[0].Rows[0]["starttime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["endtime"].ToString()!="")
				{
					model.endtime=DateTime.Parse(ds.Tables[0].Rows[0]["endtime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["bettime"].ToString()!="")
				{
					model.bettime=DateTime.Parse(ds.Tables[0].Rows[0]["bettime"].ToString());
				}
				model.itemid=ds.Tables[0].Rows[0]["itemid"].ToString();
				model.detailid=ds.Tables[0].Rows[0]["detailid"].ToString();
				if(ds.Tables[0].Rows[0]["betmoney"].ToString()!="")
				{
					model.betmoney=decimal.Parse(ds.Tables[0].Rows[0]["betmoney"].ToString());
				}
				if(ds.Tables[0].Rows[0]["returnmoney"].ToString()!="")
				{
					model.returnmoney=decimal.Parse(ds.Tables[0].Rows[0]["returnmoney"].ToString());
				}
				model.result=ds.Tables[0].Rows[0]["result"].ToString();
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得某用户的数据列表
		/// </summary>
		public DataSet GetList(string userid)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select br.*,bl.name linename,bl.betmoney linebetmoney,bl.returnmoney linereturnmoney,bl.profit,bl.state,bl.iscomplete,bl.isbetting,bi.name itemname,bd.name detailname ");
			strSql.Append(" FROM betrecord br join bettingline bl on br.lineid=bl.id join betitems bi on br.itemid=bi.id join betdetail bd on br.detailid=bd.id join betuser bu on bl.userid=bu.id ");
            if (userid.Trim() != "")
			{
                strSql.Append(" where bu.id=" + userid);
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
			strSql.Append(" id,lineid,teamname,traditional,starttime,endtime,bettime,itemid,detailid,betmoney,returnmoney,result ");
			strSql.Append(" FROM betrecord ");
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
			parameters[0].Value = "betrecord";
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

