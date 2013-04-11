using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using Winista.Text.HtmlParser.Lex;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser.Util;
using System.Text.RegularExpressions;
using SeoWebSite.BLL;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public partial class Data_NowGoal_OddsHistory : System.Web.UI.Page
{
    ScheduleBLL scheduleBLL = new ScheduleBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["odds"] != null)
            {
                string[] oddsArray = Request["odds"].Split('|');
                DataTable dt = this.FillOddsHistory(oddsArray);//填充
                DateTime stime = Convert.ToDateTime(dt.Compute("max(time)", "")).AddHours(-6);// 6小时内
                dt = dt.Select("time > '" + stime.ToString() + "'").CopyToDataTable();
                JArray data = new JArray();
                foreach (DataRow dr in dt.Rows)
                {
                    DataSet ds = scheduleBLL.statOddsHistory("companyid=" + oddsArray[0] + " and e_win=" + dr["odds_w"] +
                                    " and e_draw=" + dr["odds_d"] + " and e_lost=" + dr["odds_l"]);
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["totalcount"]) > 0)
                    {
                        JObject obj = new JObject();
                        obj.Add("time", dr["time"].ToString());
                        obj.Add("win", Convert.ToDecimal(ds.Tables[0].Rows[0]["perwin"]) - Convert.ToDecimal(dr["per_w"]) * 100);
                        obj.Add("draw", Convert.ToDecimal(ds.Tables[0].Rows[0]["perdraw"]) - Convert.ToDecimal(dr["per_d"]) * 100);
                        obj.Add("lost", Convert.ToDecimal(ds.Tables[0].Rows[0]["perlost"]) - Convert.ToDecimal(dr["per_l"]) * 100);
                        obj.Add("totalcount", Convert.ToInt32(ds.Tables[0].Rows[0]["totalcount"]));
                        data.Add(obj);
                    }
                }
                Response.Write(data.ToString());
                Response.End();
            }
        }
    }

    private DataTable FillOddsHistory(string[] odds)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("odds_w", typeof(float));
        dt.Columns.Add("odds_d", typeof(float));
        dt.Columns.Add("odds_l", typeof(float));
        dt.Columns.Add("per_w", typeof(float));
        dt.Columns.Add("per_d", typeof(float));
        dt.Columns.Add("per_l", typeof(float));
        dt.Columns.Add("time", typeof(DateTime));
        WebClient web = WebClientBLL.getWebClient();
        string s = web.DownloadString("http://live.nowodds.com/1x2/OddsHistory.aspx?id=" + odds[1]);
        
        Parser parser = Parser.CreateParser(s, "utf-8");
        
        NodeList nodeList = parser.ExtractAllNodesThatMatch(new TagNameFilter("TR"));
        for (int i = 1; i < nodeList.Count; i++)
        {
            DataRow dr = dt.NewRow();
            decimal w = Convert.ToDecimal(nodeList[i].Children[0].ToPlainTextString());
            decimal d = Convert.ToDecimal(nodeList[i].Children[1].ToPlainTextString());
            decimal l = Convert.ToDecimal(nodeList[i].Children[2].ToPlainTextString());
            decimal ret = 1/(1/w + 1/d + 1/l);
            dr["odds_w"] = nodeList[i].Children[0].ToPlainTextString();
            dr["odds_d"] = nodeList[i].Children[1].ToPlainTextString();
            dr["odds_l"] = nodeList[i].Children[2].ToPlainTextString();
            dr["per_w"] = 1 / w * ret;
            dr["per_d"] = 1 / d * ret;
            dr["per_l"] = 1 / l * ret;
            string[] t = nodeList[i].Children[3].ToPlainTextString().Replace("showtime(", "").Replace(")", "").Split(',');
            dr["time"] = new DateTime(int.Parse(t[0]), int.Parse(t[1].Split('-')[0]), int.Parse(t[2]), int.Parse(t[3]), int.Parse(t[4]), int.Parse(t[5])).AddHours(8);
            dt.Rows.Add(dr);
        }
        return dt;
    }

    
}