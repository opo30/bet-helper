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
using System.Text;

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
                    case "chart":
                        scheduleAnalysisChart();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void scheduleAnalysisChart()
    {
        if (Request["scheduleid"] != null)
        {
            DataSet ds = bll.GetList("1=1 order by time desc");
            List<string> label = new List<string>();
            List<string> oddsw = new List<string>();
            List<string> oddsd = new List<string>();
            List<string> oddsl = new List<string>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    label.Add(dr["time"].ToString());
                    oddsw.Add(dr["oddswin"].ToString());
                    oddsd.Add(dr["oddsdraw"].ToString());
                    oddsl.Add(dr["oddslost"].ToString());
                }
            }
            Response.ContentType = "text/xml";
            Response.Buffer = true; //完成整个响应后再发送
            Response.ContentEncoding = Encoding.UTF8;
            responseText = "<chart compactDataMode=\"1\" dataSeparator=\"|\" paletteThemeColor=\"5D57A5\" divLineColor=\"5D57A5\" divLineAlpha=\"40\" vDivLineAlpha=\"40\" dynamicAxis=\"1\">";
            responseText += "<categories>" + string.Join("|", label.ToArray()) + "</categories>\n";
            responseText += "<dataset seriesName=\"Series 1\">" + string.Join("|", oddsw.ToArray()) + "</dataset>";
            responseText += "<dataset seriesName=\"Series 2\">" + string.Join("|", oddsd.ToArray()) + "</dataset></chart>";
        }
    }

    private void scheduleAnalysisList()
    {
        if (Request["scheduleid"] != null)
        {
            DataSet ds = bll.GetList("scheduleid=" + Request.Form["scheduleid"] + " and DATEDIFF(HOUR,time,(select max(time) from scheduleanalysis where scheduleid=" + Request.Form["scheduleid"] + "))<=3 order by time desc");
            string javascriptJson = JsonConvert.SerializeObject(ds.Tables[0], new JavaScriptDateTimeConverter());
            responseText = "{success:true,totalCount:" + ds.Tables[0].Rows.Count + ",data:" + javascriptJson + "}";
        }
    }
}