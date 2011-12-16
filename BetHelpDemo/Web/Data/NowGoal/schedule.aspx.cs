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
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Util;
using SeoWebSite.Common;

public partial class Data_NowGoal_schedule : System.Web.UI.Page
{
    protected string responseText = "";
    ScheduleBLL bll = new ScheduleBLL();
    SeoWebSite.BLL.ScheduleBLL schedulebll = new SeoWebSite.BLL.ScheduleBLL();
    SeoWebSite.BLL.WebClientBLL webClientBLL = new WebClientBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            switch (Request.QueryString["a"])
            {
                case "list":
                    this.GetList();
                    break;
                case "queryOddsRQ":
                    this.queryOddsRQ();
                    break;
                case "queryOddsBZ":
                    this.queryOddsBZ();
                    break;
                case "queryOddsDX":
                    this.queryOddsDX();
                    break;
                case "queryOddsCountRQ":
                    this.queryOddsCountRQ();
                    break;
                case "queryCompanyCountRQ":
                    this.queryCompanyCountRQ();
                    break;
                case "queryOddsPerRQ":
                    this.queryOddsPerRQ();
                    break;
                case "queryOddsPerRQ1":
                    this.queryOddsPerRQ1();
                    break;
                case "queryOddsCountBZ":
                    this.queryOddsCountBZ();
                    break;
                case "queryOddsCountDX":
                    this.queryOddsCountDX();
                    break;
                default:
                    break;
            }
        }
    }

    private void queryOddsPerRQ1()
    {
        if (Request["scheduletypeid"] != null && Request["scheduleid"] != null && Request["companyids"] != null)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("rqy", typeof(double));
            dt.Columns.Add("rqz", typeof(double));
            dt.Columns.Add("rqs", typeof(double));
            dt.Columns.Add("totalCount", typeof(int));
            SeoWebSite.BLL.odds_rq odds_rqbll = new SeoWebSite.BLL.odds_rq();
            string[] companyidarray = Request.Form["companyids"].Split(',');
            string scheduleid = Request.Form["scheduleid"];
            List<int> companyidList = new List<int>();
            foreach (string companyid in companyidarray)
            {
                companyidList.Add(Convert.ToInt32(companyid));
                List<string> whereStrList = new List<string>();
                string[] paramArr = { companyid, scheduleid };
                string s = webClientBLL.GetRemoteHtml("odds/detail.aspx?companyID={0}&scheduleid={1}", paramArr);
                if (string.IsNullOrEmpty(s))
                {
                    continue;
                }
                Parser parser = Parser.CreateParser(s, "utf-8");
                AndFilter andFilter = new AndFilter(new TagNameFilter("table"), new HasAttributeFilter("bgColor", "#bbbbbb"));
                NodeList tableList = parser.ExtractAllNodesThatMatch(andFilter);
                parser = Parser.CreateParser(s, "utf-8");
                NodeList h3tag = parser.ExtractAllNodesThatMatch(new TagNameFilter("h3"));
                string year = h3tag[0].ToPlainTextString().Remove(5);
                if (tableList.Count == 3)
                {
                    AndFilter andFilter1 = new AndFilter(new TagNameFilter("tr"), new HasAttributeFilter("class", "ts1"));
                    
                    NodeList list = tableList[0].Children.ExtractAllNodesThatMatch(andFilter1);
                    for (int i = 0; i < list.Count; i++)
                    {
                        NodeList tdList = list[i].Children.SearchFor(typeof(Winista.Text.HtmlParser.Tags.TableColumn));
                        string pankou = CommonHelper.GoalCnToGoal(tdList[1].ToPlainTextString());
                        if (!string.IsNullOrEmpty(pankou))
                        {
                            whereStrList.Add("e.home=" + tdList[0].ToPlainTextString() + " and e.pankou=" + pankou + " and e.away=" + tdList[2].ToPlainTextString());
                        }
                    }
                }

                if (whereStrList.Count > 0)
                {
                    DataTable dt1 = odds_rqbll.queryCompanyOddsPer1(Convert.ToInt32(Request.Form["scheduletypeid"]), Convert.ToInt32(Request.Form["scheduleid"]), Convert.ToInt32(companyid),whereStrList);
                    if (dt1 != null && dt1.Rows.Count > 0 && Convert.ToInt32(dt1.Rows[0]["totalCount"]) > 0)
                    {
                        dt.ImportRow(dt1.Rows[0]);
                    }
                }
            }
            
            DataTable data = new DataTable();
            data.Columns.Add("id", typeof(int));
            data.Columns.Add("name", typeof(string));
            data.Columns.Add("rqyper", typeof(float));
            data.Columns.Add("rqzper", typeof(float));
            data.Columns.Add("rqsper", typeof(float));
            data.Columns.Add("diffper", typeof(float));
            data.Columns.Add("suggest", typeof(int));

            DataRow dr = data.NewRow();
            dr["id"] = 1;
            dr["name"] = "赔率支持平均";
            dr["rqyper"] = dt.Compute("avg(rqy)", "1=1");
            dr["rqzper"] = dt.Compute("avg(rqz)", "1=1");
            dr["rqsper"] = dt.Compute("avg(rqs)", "1=1");
            dr["diffper"] = Convert.ToInt32(dt.Compute("avg(rqy)", "1=1")) - Convert.ToInt32(dt.Compute("avg(rqs)", "1=1"));
            dr["suggest"] = 25;
            data.Rows.Add(dr);

            dr = data.NewRow();
            dr["id"] = 2;
            dr["name"] = "赔率支持最大";
            dr["rqyper"] = dt.Compute("max(rqy)", "1=1");
            dr["rqzper"] = dt.Compute("avg(rqz)", "1=1");
            dr["rqsper"] = dt.Compute("max(rqs)", "1=1");
            dr["diffper"] = Convert.ToInt32(dt.Compute("max(rqy)", "1=1")) - Convert.ToInt32(dt.Compute("max(rqs)", "1=1"));
            dr["suggest"] = 40;
            data.Rows.Add(dr);

            double ycount = Convert.ToDouble(dt.Compute("count(rqy)", "rqy > rqs"));
            double scount = Convert.ToDouble(dt.Compute("count(rqy)", "rqy < rqs"));
            dr = data.NewRow();
            dr["id"] = 3;
            dr["name"] = "公司支持数量";
            dr["rqyper"] = ycount / (ycount + scount) * 100;
            dr["rqzper"] = 0;
            dr["rqsper"] = scount / (ycount + scount) * 100;
            dr["diffper"] = ycount / (ycount + scount) * 100 - scount / (ycount + scount) * 100;
            dr["suggest"] = 80;
            data.Rows.Add(dr);
            responseText = "{success:true,data:" + JArray.FromObject(data) + "}";
        }
    }

    private void queryOddsPerRQ()
    {
        if (Request["rowData"] != null)
        {
            SeoWebSite.BLL.odds_rq odds_rqbll = new SeoWebSite.BLL.odds_rq();
            SeoWebSite.BLL.odds_bz odds_bzbll = new SeoWebSite.BLL.odds_bz();
            JArray rowData = JArray.Parse(Request.Form["rowData"]);
            List<string> oddsList = new List<string>();
            List<string> oddsList1 = new List<string>();
            foreach (JObject item in rowData)
            {
                if (!string.IsNullOrEmpty((string)item["aaa"]) && !string.IsNullOrEmpty((string)item["aab"]) && !string.IsNullOrEmpty((string)item["aac"]) && !string.IsNullOrEmpty((string)item["aba"]) && !string.IsNullOrEmpty((string)item["abb"]) && !string.IsNullOrEmpty((string)item["abc"]))
                {
                    string[] sa = { (string)item["aaa"], (string)item["aab"], (string)item["aac"], (string)item["aba"], (string)item["abb"], (string)item["abc"], item["companyid"].ToString() };
                    oddsList.Add(string.Join(",", sa));
                }
                if (!string.IsNullOrEmpty((string)item["baa"]) && !string.IsNullOrEmpty((string)item["bab"]) && !string.IsNullOrEmpty((string)item["bac"]) && !string.IsNullOrEmpty((string)item["bba"]) && !string.IsNullOrEmpty((string)item["bbb"]) && !string.IsNullOrEmpty((string)item["bbc"]))
                {
                    string[] sa = { (string)item["baa"], (string)item["bab"], (string)item["bac"], (string)item["bba"], (string)item["bbb"], (string)item["bbc"], item["companyid"].ToString() };
                    oddsList1.Add(string.Join(",", sa));
                }
            }
            DataTable dt = odds_rqbll.queryCompanyOddsPer(oddsList, 0,0);
            DataTable dt1 = odds_bzbll.queryCompanyOddsPer(oddsList1, 0, 0);
            DataTable data = new DataTable();
            data.Columns.Add("id", typeof(int));
            data.Columns.Add("name", typeof(string));
            data.Columns.Add("rqyper", typeof(float));
            data.Columns.Add("rqzper", typeof(float));
            data.Columns.Add("rqsper", typeof(float));
            data.Columns.Add("diffper", typeof(float));
            data.Columns.Add("suggest", typeof(int));

            DataRow dr = data.NewRow();
            dr["id"] = 1;
            dr["name"] = "亚赔支持平均";
            dr["rqyper"] = dt.Compute("avg(rqy)", "1=1");
            dr["rqzper"] = dt.Compute("avg(rqz)", "1=1");
            dr["rqsper"] = dt.Compute("avg(rqs)", "1=1");
            dr["diffper"] = Convert.ToInt32(dt.Compute("avg(rqy)", "1=1")) - Convert.ToInt32(dt.Compute("avg(rqs)", "1=1"));
            dr["suggest"] = 25;
            data.Rows.Add(dr);

            dr = data.NewRow();
            dr["id"] = 2;
            dr["name"] = "亚赔支持最大";
            dr["rqyper"] = dt.Compute("max(rqy)", "1=1");
            dr["rqzper"] = dt.Compute("max(rqz)", "1=1");
            dr["rqsper"] = dt.Compute("max(rqs)", "1=1");
            dr["diffper"] = Convert.ToInt32(dt.Compute("max(rqy)", "1=1")) - Convert.ToInt32(dt.Compute("max(rqs)", "1=1"));
            dr["suggest"] = 40;
            data.Rows.Add(dr);

            double ycount = Convert.ToDouble(dt.Compute("count(rqy)", "rqy > rqs"));
            double scount = Convert.ToDouble(dt.Compute("count(rqy)", "rqy < rqs"));
            dr = data.NewRow();
            dr["id"] = 3;
            dr["name"] = "亚赔支持数量";
            dr["rqyper"] = ycount / (ycount + scount) * 100;
            dr["rqzper"] = 0;
            dr["rqsper"] = scount / (ycount + scount) * 100;
            dr["diffper"] = ycount / (ycount + scount) * 100 - scount / (ycount + scount) * 100;
            dr["suggest"] = 80;
            data.Rows.Add(dr);

            dr = data.NewRow();
            dr["id"] = 4;
            dr["name"] = "欧赔支持平均";
            dr["rqyper"] = dt1.Compute("avg(rqy)", "1=1");
            dr["rqzper"] = dt1.Compute("avg(rqz)", "1=1");
            dr["rqsper"] = dt1.Compute("avg(rqs)", "1=1");
            dr["diffper"] = Convert.ToInt32(dt1.Compute("avg(rqy)", "1=1")) - Convert.ToInt32(dt1.Compute("avg(rqs)", "1=1"));
            dr["suggest"] = 40;
            data.Rows.Add(dr);

            dr = data.NewRow();
            dr["id"] = 5;
            dr["name"] = "欧赔支持最大";
            dr["rqyper"] = dt1.Compute("max(rqy)", "1=1");
            dr["rqzper"] = dt1.Compute("max(rqz)", "1=1");
            dr["rqsper"] = dt1.Compute("max(rqs)", "1=1");
            dr["diffper"] = Convert.ToInt32(dt1.Compute("max(rqy)", "1=1")) - Convert.ToInt32(dt1.Compute("max(rqs)", "1=1"));
            dr["suggest"] = 40;
            data.Rows.Add(dr);

            double ycount1 = Convert.ToDouble(dt1.Compute("count(rqy)", "rqy > rqs"));
            double scount1 = Convert.ToDouble(dt1.Compute("count(rqy)", "rqy < rqs"));
            dr = data.NewRow();
            dr["id"] = 6;
            dr["name"] = "欧赔支持数量";
            dr["rqyper"] = ycount1 / (ycount1 + scount1) * 100;
            dr["rqzper"] = 0;
            dr["rqsper"] = scount1 / (ycount1 + scount1) * 100;
            dr["diffper"] = ycount1 / (ycount1 + scount1) * 100 - scount1 / (ycount1 + scount1) * 100;
            dr["suggest"] = 80;
            data.Rows.Add(dr);
            responseText = "{success:true,data:" + JArray.FromObject(data) + "}";
        }
    }

    private void queryCompanyCountRQ()
    {
        if (Request["rowData"] != null && Request["blur"] != null)
        {
            SeoWebSite.BLL.odds_rq odds_rqbll = new SeoWebSite.BLL.odds_rq();
            JArray rowData = JArray.Parse(Request.Form["rowData"]);
            List<string> oddsList = new List<string>();
            foreach (JObject item in rowData)
            {
                if (!string.IsNullOrEmpty((string)item["aaa"]) && !string.IsNullOrEmpty((string)item["aab"]) && !string.IsNullOrEmpty((string)item["aac"]) && !string.IsNullOrEmpty((string)item["aba"]) && !string.IsNullOrEmpty((string)item["abb"]) && !string.IsNullOrEmpty((string)item["abc"]))
                {
                    string[] sa = { (string)item["aaa"], (string)item["aab"], (string)item["aac"], (string)item["aba"], (string)item["abb"], (string)item["abc"], item["companyid"].ToString() };
                    oddsList.Add(string.Join(",", sa));
                }
            }
            JArray data1 = new JArray();
            JArray data2 = new JArray();

            JObject obj = JObject.Parse("{blur:" + Request.Form["blur"] + "}");
            DataSet ds = odds_rqbll.queryOddsCount(oddsList, float.Parse(Request.Form["blur"]), Convert.ToInt32(Request.Form["scheduleType"]));
            int totalCount = int.Parse(ds.Tables[0].Rows[0]["totalCount"].ToString());
            if (totalCount > 0)
            {
                obj.Add("rqycount1", ds.Tables[0].Rows[0][0].ToString());
                obj.Add("rqzcount1", ds.Tables[0].Rows[0][1].ToString());
                obj.Add("rqscount1", ds.Tables[0].Rows[0][2].ToString());
                obj.Add("totalCount1", totalCount);
                double diffPer = (Convert.ToDouble(ds.Tables[0].Rows[0][0]) / totalCount) - (Convert.ToDouble(ds.Tables[0].Rows[0][2]) / totalCount);
                if (totalCount >= 8)
                {
                    double rmb = 0;
                    if (Math.Abs(diffPer) < 0.33)
                    {
                        rmb = Math.Floor(Math.Abs(diffPer) * 100 * 5 / 3);
                    }
                    else if (Math.Abs(diffPer) >= 0.33 && Math.Abs(diffPer) <= 0.6)
                    {
                        rmb = Math.Floor(Math.Abs(diffPer) * 100 * 5 / 2);
                    }
                    else
                    {
                        rmb = Math.Floor(Math.Abs(diffPer) * 100 * 5);
                        if (totalCount > 12 && float.Parse(Request.Form["blur"]) < 0.1f)
                        {
                            rmb = rmb * 2;
                        }
                    }

                    if (diffPer > 0)
                    {
                        obj.Add("suggest", "<font color=red>" + rmb + "元</font>");
                    }
                    else if (diffPer < 0)
                    {
                        obj.Add("suggest", "<font color=blue>" + rmb + "元</font>");
                    }
                    else
                    {
                        obj.Add("suggest", "<font color=green>0元</font>");
                    }

                }


            }
            //ds = odds_rqbll.queryOddsCount(oddsList, 2, float.Parse(Request.Form["blur"]), Convert.ToInt32(Request.Form["scheduleType"]));
            //totalCount = int.Parse(ds.Tables[0].Rows[0]["totalCount"].ToString());
            //if (totalCount > 0)
            //{
            //    obj.Add("rqycount2", ds.Tables[0].Rows[0][0].ToString());
            //    obj.Add("rqzcount2", ds.Tables[0].Rows[0][1].ToString());
            //    obj.Add("rqscount2", ds.Tables[0].Rows[0][2].ToString());
            //    obj.Add("totalCount2", totalCount);
            //}
            JArray data = new JArray();
            data.Add(obj);
            responseText = "{success:true,data:" + data.ToString() + "}";
        }
    }

    private void queryOddsDX()
    {
        throw new NotImplementedException();
    }

    private void queryOddsCountBZ()
    {
        if (Request["rowData"] != null && Request["blur"] != null)
        {
            SeoWebSite.BLL.odds_bz odds_bzbll = new SeoWebSite.BLL.odds_bz();
            JArray rowData = JArray.Parse(Request.Form["rowData"]);
            List<string> oddsList = new List<string>();
            foreach (JObject item in rowData)
            {
                if (!string.IsNullOrEmpty((string)item["baa"]) && !string.IsNullOrEmpty((string)item["bab"]) && !string.IsNullOrEmpty((string)item["bac"]) && !string.IsNullOrEmpty((string)item["bba"]) && !string.IsNullOrEmpty((string)item["bbb"]) && !string.IsNullOrEmpty((string)item["bbc"]))
                {
                    string[] sa = { (string)item["baa"], (string)item["bab"], (string)item["bac"], (string)item["bba"], (string)item["bbb"], (string)item["bbc"], item["companyid"].ToString() };
                    oddsList.Add(string.Join(",", sa));
                }
            }
            JArray data1 = new JArray();
            JArray data2 = new JArray();

            JObject obj = JObject.Parse("{blur:" + Request.Form["blur"] + "}");
            DataSet ds = odds_bzbll.queryOddsCount(oddsList, float.Parse(Request.Form["blur"]), Convert.ToInt32(Request.Form["scheduleType"]));
            int totalCount = int.Parse(ds.Tables[0].Rows[0]["totalCount"].ToString());
            if (totalCount > 0)
            {
                obj.Add("bzscount1", ds.Tables[0].Rows[0][0].ToString());
                obj.Add("bzpcount1", ds.Tables[0].Rows[0][1].ToString());
                obj.Add("bzfcount1", ds.Tables[0].Rows[0][2].ToString());
                obj.Add("totalCount1", totalCount);
            }
            JArray data = new JArray();
            data.Add(obj);
            responseText = "{success:true,data:" + data.ToString() + "}";
        }
    }

    private void queryOddsCountRQ()
    {
        if (Request["rowData"] != null && Request["blur"] != null )
        {
            SeoWebSite.BLL.odds_rq odds_rqbll = new SeoWebSite.BLL.odds_rq();
            JArray rowData = JArray.Parse(Request.Form["rowData"]);
            List<string> oddsList = new List<string>();
            foreach (JObject item in rowData)
	        {
                if (!string.IsNullOrEmpty((string)item["aaa"]) && !string.IsNullOrEmpty((string)item["aab"]) && !string.IsNullOrEmpty((string)item["aac"])&&!string.IsNullOrEmpty((string)item["aba"]) && !string.IsNullOrEmpty((string)item["abb"]) && !string.IsNullOrEmpty((string)item["abc"]))
                 {
                     string[] sa = { (string)item["aaa"], (string)item["aab"], (string)item["aac"], (string)item["aba"], (string)item["abb"], (string)item["abc"], item["companyid"].ToString() };
                    oddsList.Add(string.Join(",", sa));
                 }
	        }
            JArray data1 = new JArray();
            JArray data2 = new JArray();
            
            JObject obj = JObject.Parse("{blur:"+Request.Form["blur"]+"}");
            DataSet ds = odds_rqbll.queryOddsCount(oddsList, float.Parse(Request.Form["blur"]), Convert.ToInt32(Request.Form["scheduleType"]));
            int totalCount = int.Parse(ds.Tables[0].Rows[0]["totalCount"].ToString());
            if (totalCount > 0)
            {
                obj.Add("rqycount1", ds.Tables[0].Rows[0][0].ToString());
                obj.Add("rqzcount1", ds.Tables[0].Rows[0][1].ToString());
                obj.Add("rqscount1", ds.Tables[0].Rows[0][2].ToString());
                obj.Add("totalCount1", totalCount);
                double diffPer = (Convert.ToDouble(ds.Tables[0].Rows[0][0]) / totalCount) - (Convert.ToDouble(ds.Tables[0].Rows[0][2]) / totalCount);
                if (totalCount >= 5)
                {
                    double rmb = 0;
                    rmb = Math.Floor(Math.Abs(diffPer) * 100);
                    if (Math.Abs(diffPer) == 1)
                    {
                        rmb *= 2;
                    }
                    if (diffPer > 0)
                    {
                        obj.Add("suggest", "<font color=red>" + rmb + "元</font>");
                    }
                    else if (diffPer < 0)
                    {
                        obj.Add("suggest", "<font color=blue>" + rmb + "元</font>");
                    }
                    else
                    {
                        obj.Add("suggest", "<font color=green>0元</font>");
                    }

                }
                
                
            }
            //ds = odds_rqbll.queryOddsCount(oddsList, 2, float.Parse(Request.Form["blur"]), Convert.ToInt32(Request.Form["scheduleType"]));
            //totalCount = int.Parse(ds.Tables[0].Rows[0]["totalCount"].ToString());
            //if (totalCount > 0)
            //{
            //    obj.Add("rqycount2", ds.Tables[0].Rows[0][0].ToString());
            //    obj.Add("rqzcount2", ds.Tables[0].Rows[0][1].ToString());
            //    obj.Add("rqscount2", ds.Tables[0].Rows[0][2].ToString());
            //    obj.Add("totalCount2", totalCount);
            //}
            JArray data = new JArray();
            data.Add(obj);
            responseText = "{success:true,data:" + data.ToString() + "}";
        }
    }

    private void queryOddsCountDX()
    {
        if (Request["rowData"] != null && Request["blur"] != null)
        {
            SeoWebSite.BLL.odds_dx odds_dxbll = new SeoWebSite.BLL.odds_dx();
            JArray rowData = JArray.Parse(Request.Form["rowData"]);
            List<string> oddsList = new List<string>();
            foreach (JObject item in rowData)
            {
                if (!string.IsNullOrEmpty((string)item["caa"]) && !string.IsNullOrEmpty((string)item["cab"]) && !string.IsNullOrEmpty((string)item["cac"]) && !string.IsNullOrEmpty((string)item["cba"]) && !string.IsNullOrEmpty((string)item["cbb"]) && !string.IsNullOrEmpty((string)item["cbc"]))
                {
                    string[] sa = { (string)item["caa"], (string)item["cab"], (string)item["cac"], (string)item["cba"], (string)item["cbb"], (string)item["cbc"], item["companyid"].ToString() };
                    oddsList.Add(string.Join(",", sa));
                }
            }
            JArray data1 = new JArray();
            JArray data2 = new JArray();

            JObject obj = JObject.Parse("{blur:" + Request.Form["blur"] + "}");
            DataSet ds = odds_dxbll.queryOddsCount(oddsList, float.Parse(Request.Form["blur"]), Convert.ToInt32(Request.Form["scheduleType"]));
            int totalCount = int.Parse(ds.Tables[0].Rows[0]["totalCount"].ToString());
            if (totalCount > 0)
            {
                obj.Add("dxycount1", ds.Tables[0].Rows[0][0].ToString());
                obj.Add("dxzcount1", ds.Tables[0].Rows[0][1].ToString());
                obj.Add("dxscount1", ds.Tables[0].Rows[0][2].ToString());
                obj.Add("totalCount1", totalCount);
            }
            //ds = odds_rqbll.queryOddsCount(oddsList, 2, float.Parse(Request.Form["blur"]), Convert.ToInt32(Request.Form["scheduleType"]));
            //totalCount = int.Parse(ds.Tables[0].Rows[0]["totalCount"].ToString());
            //if (totalCount > 0)
            //{
            //    obj.Add("rqycount2", ds.Tables[0].Rows[0][0].ToString());
            //    obj.Add("rqzcount2", ds.Tables[0].Rows[0][1].ToString());
            //    obj.Add("rqscount2", ds.Tables[0].Rows[0][2].ToString());
            //    obj.Add("totalCount2", totalCount);
            //}
            JArray data = new JArray();
            data.Add(obj);
            responseText = "{success:true,data:" + data.ToString() + "}";
        }
    }

    private void queryOddsBZ()
    {
        if (Request["odds"] != null)
        {
            JObject result = new JObject();
            JArray oddsArray = JArray.Parse(Request.Form["odds"]);
            int totalCount = 0;
            DataSet ds = null;
            DataTable dt = null;
            SeoWebSite.BLL.odds_bz odds_bzbll = new SeoWebSite.BLL.odds_bz();
            foreach (JObject item in oddsArray)
            {
                ds = odds_bzbll.GetList(item["companyid"].ToString().Replace("\"", ""), new string[] { (string)item["home1"], (string)item["draw1"], (string)item["away1"], (string)item["home2"], (string)item["draw2"], (string)item["away2"] });
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (dt == null)
                    {
                        dt = ds.Tables[0];
                    }
                    else
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                            dt.ImportRow(dr);
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("victory");
                foreach (DataRow dr in dt.Rows)
                {
                    int score = (int)dr["home"] - (int)dr["away"];
                    if (score > 0)
                    {
                        dr["victory"] = 3;
                    }
                    else if (score == 0)
                    {
                        dr["victory"] = 1;
                    }
                    else
                    {
                        dr["victory"] = 0;
                    }
                }
            }
            result.Add("success", true);
            result.Add("totalCount", totalCount);
            string javascriptJson = JsonConvert.SerializeObject(dt, new JavaScriptDateTimeConverter());
            responseText = "{success:true,totalCount:" + totalCount + ",data:" + javascriptJson + "}";
        }
    }

    private void queryOddsRQ()
    {
        if (Request["odds"] != null && Request["isTrue"] != null)
        {
            JObject result = new JObject();
            JArray oddsArray = JArray.Parse(Request.Form["odds"]);
            int totalCount = 0;
            DataSet ds = null;
            DataTable dt = null;
            SeoWebSite.BLL.odds_rq odds_rqbll = new SeoWebSite.BLL.odds_rq();

            foreach (JObject item in oddsArray)
            {
                ds = odds_rqbll.GetList(item["companyid"].ToString().Replace("\"", ""), new string[] { (string)item["home1"], (string)item["pankou1"], (string)item["away1"], (string)item["home2"], (string)item["pankou2"], (string)item["away2"] });
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (dt == null)
                    {
                        dt = ds.Tables[0];
                    }
                    else
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                            dt.ImportRow(dr);
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("victory");
                dt.Columns.Add("win1");
                dt.Columns.Add("win2");
                foreach (DataRow dr in dt.Rows)
                {
                    int score = (int)dr["home"] - (int)dr["away"];
                    if (score > 0)
                    {
                        dr["victory"] = 3;
                    }
                    else if (score == 0)
                    {
                        dr["victory"] = 1;
                    }
                    else
                    {
                        dr["victory"] = 0;
                    }
                    if (score > float.Parse(dr["pankou1"].ToString()))
                    {
                        dr["win1"] = 3;
                    }
                    else if (score == float.Parse(dr["pankou1"].ToString()))
                    {
                        dr["win1"] = 1;
                    }
                    else
                    {
                        dr["win1"] = 0;
                    }
                    if (score > float.Parse(dr["pankou2"].ToString()))
                    {
                        dr["win2"] = 3;
                    }
                    else if (score == float.Parse(dr["pankou2"].ToString()))
                    {
                        dr["win2"] = 1;
                    }
                    else
                    {
                        dr["win2"] = 0;
                    }
                }
            }
            result.Add("success", true);
            result.Add("totalCount", totalCount);
            string javascriptJson = JsonConvert.SerializeObject(dt, new JavaScriptDateTimeConverter());
            responseText = "{success:true,totalCount:" + totalCount + ",data:" + javascriptJson + "}";
        }
    }

    private void GetList()
    {
        JObject result = new JObject();
        int start = int.Parse(Request.Form["start"]);
        int limit = int.Parse(Request.Form["limit"]);
        int totalCount = 0;
        string where = Request.Form["where"];
        if (!string.IsNullOrEmpty(where))
        {
            where = "Schedule.data like '%" + where + "%'";
        }
        DataSet ds = schedulebll.GetList(where, start + 1, start + limit, out totalCount);
        result.Add("success", true);
        result.Add("totalCount", totalCount);
        string javascriptJson = JsonConvert.SerializeObject(ds.Tables[0], new JavaScriptDateTimeConverter());
        responseText = "{success:true,totalCount:" + totalCount + ",data:" + javascriptJson + "}";
    }
}