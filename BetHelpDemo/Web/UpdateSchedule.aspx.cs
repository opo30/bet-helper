using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net;
using System.Text;

public partial class UpdateSchedule : System.Web.UI.Page
{
    protected string HistoryDataURL = ConfigurationManager.AppSettings["HistoryDataURL"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        WebClient web = new WebClient();
        web.Encoding = Encoding.GetEncoding("utf-8");
        string s = web.DownloadString(string.Format(HistoryDataURL, ""));
        
    }
}