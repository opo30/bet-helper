using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.BLL;
using System.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

public partial class Data_NowGoal_odds : System.Web.UI.Page
{
    protected string responseText = "";
    ScheduleBLL bll = new ScheduleBLL();
    SeoWebSite.BLL.Schedule schedulebll = new SeoWebSite.BLL.Schedule();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            switch (Request.QueryString["a"])
            {
                case "rqlist":
                    this.GetRQList();
                    break;
                case "bzlist":
                    this.GetBZList();
                    break;
                case "dxlist":
                    this.GetDXList();
                    break;
                default:
                    break;
            }
        }
    }

    private void GetDXList()
    {
        throw new NotImplementedException();
    }

    private void GetBZList()
    {
        throw new NotImplementedException();
    }

    private void GetRQList()
    {
        try
        {
            SeoWebSite.BLL.odds_rq oddsrqbll = new SeoWebSite.BLL.odds_rq();
            JObject result = new JObject();
            int start = int.Parse(Request.Form["start"]);
            int limit = int.Parse(Request.Form["limit"]);
            int totalCount = 0;
            string where = Request.Form["where"];
            DataSet ds = oddsrqbll.GetList(where, start + 1, start + limit, out totalCount);
            result.Add("success", true);
            result.Add("totalCount", totalCount);
            string javascriptJson = JsonConvert.SerializeObject(ds.Tables[0], new JavaScriptDateTimeConverter());
            responseText = "{success:true,totalCount:" + totalCount + ",data:" + javascriptJson + "}";
        }
        catch (Exception)
        {
            
            throw;
        }
    }
}