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

public partial class Data_NowGoal_OddsHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["odds"] != null)
            {
                string[] oddsArray = Request["odds"].Split('^');

                DataTable dt = new DataTable();
                dt.Columns.Add("win", typeof(float));
                dt.Columns.Add("draw", typeof(float));
                dt.Columns.Add("lost", typeof(float));
                dt.Columns.Add("time", typeof(DateTime));
                dt.Columns.Add("companyid", typeof(int));
                foreach (string oddsStr in oddsArray)
                {
                    string[] odds = oddsStr.Split('|');
                    this.FillOddsHistory(odds, dt);
                }

                decimal avewin = Convert.ToDecimal(dt.Compute("avg(win)", ""));
            }
        }
    }

    private void FillOddsHistory(string[] odds ,DataTable dt)
    {
        WebClient web = new WebClient();
        web.Encoding = System.Text.Encoding.UTF8;
        string s = web.DownloadString("http://live.nowscore.com/1x2/OddsHistory.aspx?id=" + odds[1]);
        
        Parser parser = Parser.CreateParser(s, "utf-8");
        
        NodeList nodeList = parser.ExtractAllNodesThatMatch(new TagNameFilter("TR"));
        for (int i = 1; i < nodeList.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["companyid"] = odds[0];
            dr["win"] = nodeList[i].Children[0].ToPlainTextString();
            dr["draw"] = nodeList[i].Children[1].ToPlainTextString();
            dr["lost"] = nodeList[i].Children[2].ToPlainTextString();
            string[] t = nodeList[i].Children[3].ToPlainTextString().Replace("showtime(", "").Replace(")", "").Split(',');

            dr["time"] = new DateTime(int.Parse(t[0]), int.Parse(t[1].Split('-')[0]), int.Parse(t[2]), int.Parse(t[3]), int.Parse(t[4]), int.Parse(t[5])).AddHours(8);
            dt.Rows.Add(dr);
        }
    }

    
}