using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.BLL;
using Winista.Text.HtmlParser.Lex;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Util;
using Winista.Text.HtmlParser.Filters;
using System.Net;
using Winista.Text.HtmlParser.Tags;

public partial class Default4 : System.Web.UI.Page
{
    ScollGoalBLL bll = new ScollGoalBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //GridView1.DataSource = bll.GetScrollMatchList();
            //GridView1.DataBind();
            WebClient web = WebClientBLL.getWebClient();
            string s = web.DownloadString("http://data.nowscore.com/detail/403052.html");
            Lexer lexer = new Lexer(s);
            Parser parser = new Parser(lexer);
            INode tableNode = parser.Parse(new TagNameFilter("HTML"))[0].Children.ExtractAllNodesThatMatch(new TagNameFilter("BODY"))[0].Children[0];
            TableTag tt = tableNode as TableTag;

            Response.Write(tt.GetRow(0).Columns[0].Children[0].Children[0].ToPlainTextString());
            Response.Write(tt.GetRow(0).Columns[1].Children[0].Children[0].ToPlainTextString());
            Response.Write(tt.GetRow(0).Columns[2].Children[0].Children[0].ToPlainTextString());

            //ITag divNode = bodyNodes.ExtractAllNodesThatMatch(new TagNameFilter("FORM"))[0].Children.ExtractAllNodesThatMatch(new TagNameFilter("DIV"))[0] as ITag;
            //if (divNode.Attributes["ID"].Equals("PageBody"))
            //{
            //    NodeList dataDivList = divNode.Children.SearchFor(typeof(Winista.Text.HtmlParser.Tags.Div));

            //}
        }
    }
}