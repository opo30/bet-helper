using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.BLL;
using System.Data;
using SeoWebSite.DBUtility;

public partial class Default6 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ScheduleBLL bll = new ScheduleBLL();
            DataSet ds = bll.GetList("h_teamid is null or h_teamid=''");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string[] dataArr = dr["data"].ToString().Split(',');

                DbHelperSQL.ExecuteSql("update schedule set h_teamid=" + dataArr[2] + ",g_teamid=" + dataArr[3] + " where id=" + dr["id"]);
            }
        }
    }
}