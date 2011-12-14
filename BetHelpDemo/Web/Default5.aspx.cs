using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SeoWebSite.BLL;
using System.Text;
using SeoWebSite.DBUtility;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;

public partial class Default5 : System.Web.UI.Page
{
    BetExpBLL bll = new BetExpBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,data from BetExp");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //JArray data = JArray.Parse(dr["data"].ToString());
                //string[] valueArr = new string[data.Count];
                //for (int i = 0; i < data.Count; i++)
                //{
                //    valueArr[i] = bll.GetTrendsValue(JObject.Parse(data[data.Count - (data.Count - i)].ToString()));
                //}
                //strSql = new StringBuilder();
                //strSql.Append("update BetExp set trends ='" + (string.Join(",", valueArr)) + "' where id=" + dr["id"]);
                //DbHelperSQL.ExecuteSql(strSql.ToString());
                //Response.Write(dr["id"].ToString() + "\r\n");
                JArray data = JArray.Parse(dr["data"].ToString());
                
                strSql = new StringBuilder();
                strSql.Append("update BetExp set changes ='" + bll.GetChangesValue(data,"") + "' where id=" + dr["id"]);
                DbHelperSQL.ExecuteSql(strSql.ToString());
                Response.Write(dr["id"].ToString() + "\r\n");
            }
        }
    }

    protected List<float> bubbleUp(List<float> Array)
    {
        for (int i = 0; i < Array.Count; i++)
        {
            for (int j = i + 1; j < Array.Count; j++)
            {
                if (Array[i] > Array[j])
                {
                    float temp = Array[i];
                    Array[i] = Array[j];
                    Array[j] = temp;
                }
            }
        }
        return Array;
    }
}