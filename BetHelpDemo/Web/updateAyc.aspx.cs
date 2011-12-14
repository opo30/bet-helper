using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.BLL;
using System.Data;
using Newtonsoft.Json.Linq;

public partial class updateAyc : System.Web.UI.Page
{
    BetExpBLL bll = new BetExpBLL();
    ForecastScriptsBLL bll1 = new ForecastScriptsBLL();
    protected string allData = "";
    protected string foreData = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet ds = bll.GetAllList();
            allData = JArray.FromObject(ds.Tables[0]).ToString();
            ds = bll1.GetAllList();
            foreData = JArray.FromObject(ds.Tables[0]).ToString();
        }
    }
}