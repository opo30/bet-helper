using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.BLL;
using System.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public partial class Data_NowGoal_ScheduleAnalysis : System.Web.UI.Page
{
    protected string responseText = "";
    ScheduleAnalysisBLL bll = new ScheduleAnalysisBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["a"] != null)
            {
                switch (Request.Params["a"])
                {
                    case "list":
                        scheduleAnalysisList();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void scheduleAnalysisList()
    {
        if (Request["scheduleid"] != null)
        {
            
            DataSet ds = bll.GetList("scheduleid=" + Request.Form["scheduleid"] + " order by time desc");
            string javascriptJson = JsonConvert.SerializeObject(ds.Tables[0], new JavaScriptDateTimeConverter());
            responseText = "{success:true,totalCount:" + ds.Tables[0].Rows.Count + ",data:" + javascriptJson + "}";
        }
    }
}