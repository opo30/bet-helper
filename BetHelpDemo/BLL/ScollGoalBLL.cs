using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeoWebSite.Common;
using System.Net;
using Winista.Text.HtmlParser.Lex;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Util;
using Winista.Text.HtmlParser.Filters;
using SeoWebSite.Model;
using System.Data;

namespace SeoWebSite.BLL
{
    public class ScollGoalBLL
    {
        private readonly SeoWebSite.DAL.OddsLiveDAO dal = new SeoWebSite.DAL.OddsLiveDAO();
        private DataSet ds;
        private string domain = "mobile.bet365.com";
        private string zoudiUrl = "?ID=204:1&pid=200:0&o=0/0&t=";

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        public List<OddsLiveMatch> GetScrollMatchList()
        {
            List<OddsLiveMatch> liveMatchList = new List<OddsLiveMatch>();
            try
            {
                HttpHelper h = new HttpHelper();
                Cookie lng = new Cookie("lng", "2");
                lng.Domain = domain;
                h.CookieContainer.Add(lng);
                string zoudi = h.GetHtml("https://" +domain+ "/default.aspx"+ zoudiUrl);
                if (!string.IsNullOrEmpty(zoudi))
                {
                    #region 分析网页html节点
                    Lexer lexer = new Lexer(zoudi);
                    Parser parser = new Parser(lexer);
                    NodeList bodyNodes = parser.Parse(new TagNameFilter("HTML"))[0].Children.ExtractAllNodesThatMatch(new TagNameFilter("BODY"))[0].Children;
                    ITag divNode = bodyNodes.ExtractAllNodesThatMatch(new TagNameFilter("FORM"))[0].Children.ExtractAllNodesThatMatch(new TagNameFilter("DIV"))[0] as ITag;
                    if (divNode.Attributes["ID"].Equals("PageBody"))
                    {
                        NodeList dataDivList = divNode.Children.SearchFor(typeof(Winista.Text.HtmlParser.Tags.Div));
                        if (dataDivList[0].ToPlainTextString() == "走地盤")
                        {
                            if (dataDivList[2].ToPlainTextString() == "全場賽果")
                            {
                                return liveMatchList;
                            }
                            for (int i = 0; i < dataDivList.Count; i++)
                            {
                                ITag div = dataDivList[i] as ITag;
                                if (div.Attributes["CLASS"] != null && div.Attributes["CLASS"].Equals("menuRow"))
                                {
                                    OddsLiveMatch oddsLive = new OddsLiveMatch();
                                    oddsLive.urlparams = (div.FirstChild as ITag).Attributes["HREF"].ToString();
                                    oddsLive.id = oddsLive.urlparams.Split('&')[0].Substring(4);
                                    oddsLive.time = DateTime.Now;
                                    oddsLive.name = div.ToPlainTextString();
                                    liveMatchList.Add(oddsLive);
                                }
                            }
                        }
                    }
                    #endregion 分析网页html节点
                }
            }
            catch (Exception)
            {
                
            }
            return liveMatchList;
        }

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        public List<OddsLiveMatch> GetMatchScrollOdds(string matchid,string urlparams)
        {
            List<OddsLiveMatch> liveMatchList = new List<OddsLiveMatch>();
            try
            {
                HttpHelper h = new HttpHelper();
                Cookie lng = new Cookie("lng", "2");
                lng.Domain = domain;
                h.CookieContainer.Add(lng);
                //string zoudi = h.GetHtml("https://" + domain + "/default.aspx" + urlparams);
                string zoudi = h.GetHtml(urlparams);
                if (!string.IsNullOrEmpty(zoudi))
                {
                    #region 分析网页html节点
                    Lexer lexer = new Lexer(zoudi);
                    Parser parser = new Parser(lexer);
                    NodeList bodyNodes = parser.Parse(new TagNameFilter("HTML"))[0].Children.ExtractAllNodesThatMatch(new TagNameFilter("BODY"))[0].Children;
                    ITag divNode = bodyNodes.ExtractAllNodesThatMatch(new TagNameFilter("FORM"))[0].Children.ExtractAllNodesThatMatch(new TagNameFilter("DIV"))[0] as ITag;
                    if (divNode.Attributes["ID"].Equals("PageBody"))
                    {
                        NodeList dataDivList = divNode.Children.SearchFor(typeof(Winista.Text.HtmlParser.Tags.Div));
                        if (dataDivList[0].ToPlainTextString() == "走地盤")
                        {
                            if (dataDivList[2].ToPlainTextString() == "全場賽果")
                            {
                                OddsLiveHistory liveHistory = new OddsLiveHistory();
                                liveHistory.matchid = matchid;
                                liveHistory.home = float.Parse(dataDivList[3].ToPlainTextString().Split(' ')[0]);
                                liveHistory.draw = float.Parse(dataDivList[5].ToPlainTextString().Split(' ')[0]);
                                liveHistory.away = float.Parse(dataDivList[7].ToPlainTextString().Split(' ')[0]);
                                liveHistory.time = DateTime.Now;
                                dal.AddHistory(liveHistory);
                            }
                        }
                    }
                    #endregion 分析网页html节点
                }
            }
            catch (Exception)
            {

            }
            return liveMatchList;
        }

        public void AddLiveMatch(OddsLiveMatch model) {
            dal.AddMatch(model);
        }

    }
}
