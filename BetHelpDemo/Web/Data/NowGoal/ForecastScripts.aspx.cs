using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.BLL;
using System.Data;
using Newtonsoft.Json.Linq;

public partial class Data_NowGoal_ForecastScripts : System.Web.UI.Page
{
    protected string JsonStr = "";
    ForecastScriptsBLL bll = new ForecastScriptsBLL();
    NowGoalBLL ngbll = new NowGoalBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["a"] != null)
            {
                switch (Request.QueryString["a"])
                {
                    case "list":
                        ProccessListAction();
                        break;
                    case "statistics":
                        ProccessStatisticsAction();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void ProccessStatisticsAction()
    {
        JObject result = new JObject();
        try
        {
            int matchid = int.Parse(Request.Form["matchid"]);
            string[] scriptids = Request.Form["scriptids"].Split(',');
            string[] wins = Request.Form["wins"].Split(',');
            string[] victorys = Request.Form["victorys"].Split(',');
            BetExpBLL betExpBLL = new BetExpBLL();
            if (!betExpBLL.ExistsStatistics(matchid))
            {
                for (int i = 0; i < scriptids.Length; i++)
                {
                    string win = (wins[i]=="true" ? "win":"lost");
                    string victory = (victorys[i]=="true" ? "resultwin":"resultlost");
                    bll.Increase(int.Parse(scriptids[i]), win, victory);
                }
                betExpBLL.Update(matchid, true);
            }
            result.Add("success",true);
        }
        catch (Exception e)
        {
            result.Add("success", false);
            result.Add("error", e.ToString());
        }
        JsonStr = result.ToString();
    }

    private void ProccessListAction()
    {
        JObject result = new JObject();
        try
        {
            DataSet ds = bll.GetAllList();
            JArray data = JArray.FromObject(ds.Tables[0]);
            result.Add("success", true);
            result.Add("data", data);
        }
        catch (Exception e)
        {
            result.Add("success", false);
            result.Add("error", e.ToString());
        }
        JsonStr = result.ToString();
    }
}