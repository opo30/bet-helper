using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Winista.Text.HtmlParser.Lex;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Util;
using SeoWebSite.Common;
using System.Net;
using Winista.Text.HtmlParser.Filters;
using SeoWebSite.BLL;

public partial class Default3 : System.Web.UI.Page
{
    ScollGoalBLL bll = new ScollGoalBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridView1.DataSource = bll.GetScrollMatchList();
            GridView1.DataBind();

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        bll.GetMatchScrollOdds("111","https://mobile.bet365.com/default.aspx?ID=204:1&pid=200:0&o=0/0&t=");
    }
}