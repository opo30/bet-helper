using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.BLL;
using System.Text;
using Winista.Text.HtmlParser.Lex;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Util;
using Winista.Text.HtmlParser.Filters;
using System.Text.RegularExpressions;
using Winista.Text.HtmlParser.Tags;

public partial class Data_NowGoal_GetRemoteHtml : System.Web.UI.Page
{
    WebClientBLL bll = new WebClientBLL();
    private string showgoallist = "/detail/{0}.html";
    private string analysis = "/analysis/{0}.html";
    private string AsianOdds = "/odds/match.aspx?id={0}";
    private string EuropeOdds = "/1x2/{0}.htm";
    private string TeamPanlu_10 = "/panlu/{0}.html";
    private string oddsDetail = "/odds/detail.aspx?scheduleID={0}&companyID={1}";
    private string historyMatch = "/data/score.aspx?date={0}";
    private string OddsHistory = "/1x2/OddsHistory.aspx?id={0}";
    private string EuropeOddsJS = "http://1x2.nowscore.com/{0}.js";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string matchid = Request.QueryString["matchid"];
            string html = "";
            Response.ContentType = "text/html";
            Response.Buffer = true; //完成整个响应后再发送
            Response.ContentEncoding = Encoding.UTF8;

            switch (Request.QueryString["a"])
            {
                case "showgoallist":
                    html = bll.GetRemoteHtml(showgoallist, new string[] { matchid });
                    html = html.Replace("img src=/images/", "img src=" + WebClientBLL.root + "/images/");
                    break;
                case "analysis":
                    html = bll.GetRemoteHtml(analysis, new string[] { matchid });
                    break;
                case "AsianOdds":
                    html = bll.GetRemoteHtml(AsianOdds, new string[] { matchid });
                    break;
                case "EuropeOdds":
                    html = bll.GetRemoteHtml(EuropeOdds, new string[] { matchid });
                    break;
                case "EuropeOddsJS":
                    html = bll.UpdateOdds1x2Content(matchid);
                    break;
                case "TeamPanlu_10":
                    html = bll.GetRemoteHtml(TeamPanlu_10, new string[] { matchid });
                    break;
                case "oddsDetail":
                    html = bll.GetRemoteHtml(oddsDetail, new string[] { matchid });
                    break;
                case "historyMatch":
                    html = bll.GetRemoteHtml(historyMatch, new string[] { Request.Form["date"] });
                    break;
                case "OddsHistory":
                    html = bll.GetRemoteHtml(OddsHistory, new string[] { Request.Form["oddsid"] });
                    break;
                default:
                    break;
            }
            Response.Write(html);
            Response.End();
        }
    }


    /*
    function showgoallist(ID)
{
	window.open("/detail/" + ID +".html", "","scrollbars=yes,resizable=yes,width=668, height=720");
}
function  analysis(ID)
{
	var theURL="/analysis/" + ID +".html";
	window.open(theURL);
}
function AsianOdds(ID)
{
	var theURL="/odds/match.aspx?id="  +ID;
	window.open(theURL);
}

function EuropeOdds(ID)
{
	var theURL="/1x2/" + ID + ".htm";
	window.open(theURL);
}

function TeamPanlu_10(ID)
{
    var	theURL="/panlu/" + ID + ".html";
	window.open(theURL,"","width=640,height=700,top=10,left=100,resizable=yes,scrollbars=yes");
}
function oddsDetail(ID,cId)
{
	window.open("/odds/detail.aspx?scheduleID=" + ID +"&companyID="+ cId, "","");
}

    */
}