using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using SeoWebSite.Common;

public partial class Data_bet365 : System.Web.UI.Page
{
    protected string json = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            switch (Request["a"])
            {
                case "list":
                    this.ProcessList();
                    break;
                case "compare":
                    this.ProcessCompare();
                    break;
                case "compare1":
                    this.ProcessCompare1();
                    break;
                default:
                    break;
            }
        }
    }

    private void ProcessCompare()
    {
        JObject jo = new JObject();
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("t", typeof(int));
            dt.Columns.Add("o1", typeof(double));
            dt.Columns.Add("o2", typeof(double));
            dt.Columns.Add("o3", typeof(double));

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("http://live1.nowscore.com/odds/match.aspx?id=" + Request["id"]);
            HtmlNodeCollection zdnodes = doc.DocumentNode.SelectNodes("//span[@id='odds']//span[text()='走地']/../..");
            foreach (HtmlNode zdnode in zdnodes)
            {
                for (int i = 1; i <= 3; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["t"] = i;
                    if (string.IsNullOrEmpty(zdnode.SelectSingleNode("td[" + (i * 6 - 1) + "]").InnerText.Trim()))
                    {
                        continue;
                    }
                    dr["o1"] = zdnode.SelectSingleNode("td[" + (i * 6 - 1) + "]").InnerText;
                    dr["o3"] = zdnode.SelectSingleNode("td[" + (i * 6 + 1) + "]").InnerText;
                    string o2text = zdnode.SelectSingleNode("td[" + (i * 6) + "]").InnerText;
                    if (i == 1)
                    {
                        dr["o2"] = CommonHelper.GoalCnToGoal(o2text);
                    }
                    else if (i == 2)
                    {
                        dr["o2"] = o2text;
                    }
                    else if (i == 3)
                    {
                        dr["o2"] = CommonHelper.BallSizeToBall(o2text);
                    }
                    dt.Rows.Add(dr);
                }
            }

            doc = web.Load("https://mobile.bet365.com/premium/V6/sport/coupon/coupon.aspx?zone=0&isocode=HK&tzi=1&key=" + Request["key"] + "&ip=1&gn=0&lng=2&ct=198&cts=43&clt=9996&ot=2");

            DataTable bdt = dt.Clone();
            zdnodes = doc.DocumentNode.SelectNodes("//section[position()<3] | //h1[starts-with(text(),'全場賽果')]/../../../..");
            foreach (HtmlNode zdnode in zdnodes)
            {
                try
                {
                    DataRow dr = bdt.NewRow();
                    HtmlNodeCollection oddsNodes = zdnode.SelectNodes("div//td[@class='cols2'] | div//td[@class='cols3']");
                    
                    if (oddsNodes.Count == 2)
	                {
                        dr["t"] = zdnodes.IndexOf(zdnode) == 0 ? 1 : 3;
		                string pankou = oddsNodes[0].SelectSingleNode("div/text()").InnerText;
                        List<double> pkList = new List<double>();
                        foreach (string item in pankou.Split(','))
                        {
                            pkList.Add(Convert.ToDouble(item.Replace("超過","")));
                        }
                        dr["o2"] = Convert.ToInt32(dr["t"]) == 1 ? (0 - pkList.Average()) : pkList.Average();
                        dr["o1"] = Convert.ToDouble(oddsNodes[0].SelectSingleNode("div/span").InnerText) - 1;
                        dr["o3"] = Convert.ToDouble(oddsNodes[oddsNodes.Count - 1].SelectSingleNode("div/span").InnerText) - 1;
                    }
                    else
                    {
                        dr["t"] = 2;
                        dr["o2"] = oddsNodes[1].SelectSingleNode("div/span").InnerText;
                        dr["o1"] = Convert.ToDouble(oddsNodes[0].SelectSingleNode("div/span").InnerText);
                        dr["o3"] = Convert.ToDouble(oddsNodes[oddsNodes.Count - 1].SelectSingleNode("div/span").InnerText);
                    }
                    bdt.Rows.Add(dr);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            string s = "";
            if (Convert.ToInt32(bdt.Compute("count(t)","t=1")) > 0)
	        {
                DataRow dr = bdt.Select("t=1")[0];
                int count = Convert.ToInt32(dt.Compute("count(t)", string.Format("t=1 and o2={0}", new object[] { dr["o2"] })));
                if (count > 0)
                {
                    if (Convert.ToInt32(dt.Compute("count(t)", string.Format("t=1 and o1<={0} and o2={1}", new object[] { dr["o1"], dr["o2"] }))) == 0)
                    {
                        s += "亚盘" + dr["o1"] + " " + dr["o2"] + " " + dr["o3"] + "看好赢盘信心" + count + "<br>";
                    }
                    if (Convert.ToInt32(dt.Compute("count(t)", string.Format("t=1 and o1>={0} and o2={1}", new object[] { dr["o1"], dr["o2"] }))) == 0)
                    {
                        s += "亚盘" + dr["o1"] + " " + dr["o2"] + " " + dr["o3"] + "看衰赢盘信心" + count + "<br>";
                    }
                    if (Convert.ToInt32(dt.Compute("count(t)", string.Format("t=1 and o3<={0} and o2={1}", new object[] { dr["o3"], dr["o2"] }))) == 0)
                    {
                        s += "亚盘" + dr["o1"] + " " + dr["o2"] + " " + dr["o3"] + "看好输盘信心" + count + "<br>";
                    }
                    if (Convert.ToInt32(dt.Compute("count(t)", string.Format("t=1 and o3>={0} and o2={1}", new object[] { dr["o3"], dr["o2"] }))) == 0)
                    {
                        s += "亚盘" + dr["o1"] + " " + dr["o2"] + " " + dr["o3"] + "看衰输盘信心" + count + "<br>";
                    }
                }
	        }
            if (Convert.ToInt32(bdt.Compute("count(t)", "t=2")) > 0)
            {
                DataRow dr = bdt.Select("t=2")[0];
                if (Convert.ToInt32(dt.Compute("count(t)", string.Format("t=2 and o1<={0}", new object[] { dr["o1"] }))) == 0)
                {
                    s += "欧盘" + dr["o1"] + " " + dr["o2"] + " " + dr["o3"] + "看好胜<br>";
                }
                if (Convert.ToInt32(dt.Compute("count(t)", string.Format("t=2 and o1>={0}", new object[] { dr["o1"] }))) == 0)
                {
                    s += "欧盘" + dr["o1"] + " " + dr["o2"] + " " + dr["o3"] + "看衰胜<br>";
                }
                if (Convert.ToInt32(dt.Compute("count(t)", string.Format("t=2 and o2<={0}", new object[] { dr["o2"] }))) == 0)
                {
                    s += "欧盘" + dr["o1"] + " " + dr["o2"] + " " + dr["o3"] + "看好平<br>";
                }
                if (Convert.ToInt32(dt.Compute("count(t)", string.Format("t=2 and o2>={0}", new object[] { dr["o2"] }))) == 0)
                {
                    s += "欧盘" + dr["o1"] + " " + dr["o2"] + " " + dr["o3"] + "看衰平<br>";
                }
                if (Convert.ToInt32(dt.Compute("count(t)", string.Format("t=2 and o3<={0}", new object[] { dr["o3"] }))) == 0)
                {
                    s += "欧盘" + dr["o1"] + " " + dr["o2"] + " " + dr["o3"] + "看好负<br>";
                }
                if (Convert.ToInt32(dt.Compute("count(t)", string.Format("t=2 and o3>={0}", new object[] { dr["o3"] }))) == 0)
                {
                    s += "欧盘" + dr["o1"] + " " + dr["o2"] + " " + dr["o3"] + "看衰负<br>";
                }
            }
            if (Convert.ToInt32(bdt.Compute("count(t)", "t=3")) > 0)
            {
                DataRow dr = bdt.Select("t=3")[0];
                int count = Convert.ToInt32(dt.Compute("count(t)", string.Format("t=3 and o2={0}", new object[] { dr["o2"] })));
                if (count > 0)
                {
                    if (Convert.ToInt32(dt.Compute("count(t)", string.Format("t=3 and o1<={0} and o2={1}", new object[] { dr["o1"], dr["o2"] }))) == 0)
                    {
                        s += "大小盘" + dr["o1"] + " " + dr["o2"] + " " + dr["o3"] + "看好大球信心" + count + "<br>";
                    }
                    if (Convert.ToInt32(dt.Compute("count(t)", string.Format("t=3 and o1>={0} and o2={1}", new object[] { dr["o1"], dr["o2"] }))) == 0)
                    {
                        s += "大小盘" + dr["o1"] + " " + dr["o2"] + " " + dr["o3"] + "看衰大球信心" + count + "<br>";
                    }
                    if (Convert.ToInt32(dt.Compute("count(t)", string.Format("t=3 and o3<={0} and o2={1}", new object[] { dr["o3"], dr["o2"] }))) == 0)
                    {
                        s += "大小盘" + dr["o1"] + " " + dr["o2"] + " " + dr["o3"] + "看好小球信心" + count + "<br>";
                    }
                    if (Convert.ToInt32(dt.Compute("count(t)", string.Format("t=3 and o3>={0} and o2={1}", new object[] { dr["o3"], dr["o2"] }))) == 0)
                    {
                        s += "大小盘" + dr["o1"] + " " + dr["o2"] + " " + dr["o3"] + "看衰小球信心" + count + "<br>";
                    }
                }
            }
            if (Convert.ToInt32(bdt.Compute("count(t)", "t=1")) > 0)
            {
                DataRow dr = bdt.Select("t=1")[0];
                int count = Convert.ToInt32(dt.Compute("count(t)", string.Format("t=1 and o2={0}", new object[] { dr["o2"] })));
                if (count > 0)
                {
                    s += "--------逆向亚盘---------<br>";
                    if (Convert.ToInt32(dt.Compute("count(t)", string.Format("t=1 and o1<={0} and o2={1}", new object[] { dr["o3"], dr["o2"] }))) == 0)
                    {
                        s += "亚盘" + dr["o1"] + " " + dr["o2"] + " " + dr["o3"] + "看好赢盘信心" + count + "<br>";
                    }
                    if (Convert.ToInt32(dt.Compute("count(t)", string.Format("t=1 and o1>={0} and o2={1}", new object[] { dr["o3"], dr["o2"] }))) == 0)
                    {
                        s += "亚盘" + dr["o1"] + " " + dr["o2"] + " " + dr["o3"] + "看衰赢盘信心" + count + "<br>";
                    }
                    if (Convert.ToInt32(dt.Compute("count(t)", string.Format("t=1 and o3<={0} and o2={1}", new object[] { dr["o1"], dr["o2"] }))) == 0)
                    {
                        s += "亚盘" + dr["o1"] + " " + dr["o2"] + " " + dr["o3"] + "看好输盘信心" + count + "<br>";
                    }
                    if (Convert.ToInt32(dt.Compute("count(t)", string.Format("t=1 and o3>={0} and o2={1}", new object[] { dr["o1"], dr["o2"] }))) == 0)
                    {
                        s += "亚盘" + dr["o1"] + " " + dr["o2"] + " " + dr["o3"] + "看衰输盘信心" + count + "<br>";
                    }
                }
            }
            jo.Add("success", true);
            jo.Add("message", s);
        }
        catch (Exception e)
        {
            jo.Add("success", false);
            jo.Add("message", e.Message);
        }
        finally {
            json = jo.ToString();
        }
    }

    private void ProcessCompare1()
    {
        JObject jo = new JObject();
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("o1", typeof(double));
            dt.Columns.Add("o2", typeof(double));
            dt.Columns.Add("o3", typeof(double));
            dt.Columns.Add("s1", typeof(int));
            dt.Columns.Add("s2", typeof(int));

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("http://live1.nowscore.com/odds/match.aspx?id=" + Request["id"]);
            HtmlNodeCollection zdnodes = doc.DocumentNode.SelectNodes("//span[@id='odds']//span[text()='走地']/../..");
            foreach (HtmlNode zdnode in zdnodes)
            {
                DataRow dr = dt.NewRow();
                if (string.IsNullOrEmpty(zdnode.SelectSingleNode("td[5]").InnerText.Trim()))
                {
                    continue;
                }
                dr["o1"] = zdnode.SelectSingleNode("td[5]").InnerText;
                dr["o3"] = zdnode.SelectSingleNode("td[7]").InnerText;
                string o2text = zdnode.SelectSingleNode("td[6]").InnerText;
                dr["o2"] = CommonHelper.GoalCnToGoal(o2text);
                dt.Rows.Add(dr);
            }

            doc = web.Load("https://mobile.bet365.com/premium/V6/sport/coupon/coupon.aspx?zone=0&isocode=HK&tzi=1&key=" + Request["key"] + "&ip=1&gn=0&lng=2&ct=198&cts=43&clt=9996&ot=2");

            DataTable bdt = dt.Clone();
            DataRow bdr = bdt.NewRow();
            HtmlNode node = doc.DocumentNode.SelectSingleNode("//section[1]");
            if (node != null)
            {
                HtmlNodeCollection oddsNodes = node.SelectNodes("div//td[@class='cols2']");
                string pankou = oddsNodes[0].SelectSingleNode("div/text()").InnerText;
                List<double> pkList = new List<double>();
                foreach (string item in pankou.Split(','))
                {
                    pkList.Add(Convert.ToDouble(item.Replace("超過", "")));
                }
                bdr["o2"] = 0 - pkList.Average();
                bdr["o1"] = Convert.ToDouble(oddsNodes[0].SelectSingleNode("div/span").InnerText) - 1;
                bdr["o3"] = Convert.ToDouble(oddsNodes[oddsNodes.Count - 1].SelectSingleNode("div/span").InnerText) - 1;
            }
            bool isReverse = Request["reverse"] == "1";
            if (bdr["o1"] != DBNull.Value && bdr["o2"] != DBNull.Value && bdr["o3"] != DBNull.Value)
            {
                bdr["s1"] = 0;
                bdr["s2"] = 0;
                int count = Convert.ToInt32(dt.Compute("count(o1)", string.Format("o2={0}", new object[] { bdr["o2"] })));
                if (count > 0)
                {
                    if (Convert.ToInt32(dt.Compute("count(o1)", string.Format("o1<={0} and o2={1}", new object[] { bdr[isReverse ? "o3" : "o1"], bdr["o2"] }))) == 0)
                    {
                        bdr["s1"] = 1;
                    }
                    if (Convert.ToInt32(dt.Compute("count(o1)", string.Format("o1>={0} and o2={1}", new object[] { bdr[isReverse ? "o3" : "o1"], bdr["o2"] }))) == 0)
                    {
                        bdr["s1"] = -1;
                    }
                    if (Convert.ToInt32(dt.Compute("count(o1)", string.Format("o3<={0} and o2={1}", new object[] { bdr[isReverse ? "o1" : "o3"], bdr["o2"] }))) == 0)
                    {
                        bdr["s2"] = 1;
                    }
                    if (Convert.ToInt32(dt.Compute("count(o1)", string.Format("o3>={0} and o2={1}", new object[] { bdr[isReverse ? "o1" : "o3"], bdr["o2"] }))) == 0)
                    {
                        bdr["s2"] = -1;
                    }
                }
                bdt.Rows.Add(bdr);
            }
            jo.Add("success", true);
            jo.Add("message", JArray.FromObject(bdt)[0]);
        }
        catch (Exception e)
        {
            jo.Add("success", false);
            jo.Add("message", e.Message);
        }
        finally
        {
            json = jo.ToString();
        }
    }

    private void ProcessList()
    {
        JArray ja = new JArray();
        try
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://mobile.bet365.com/premium/V6/sport/InPlay.aspx?zone=0&isocode=HK&tzi=1&ip=1&gn=0&lng=2&ct=198&cts=43&clt=9996");
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@id='InPlay']/ul[1]/li");
            foreach (HtmlNode node in nodes)
            {
                JObject jo = new JObject();
                jo.Add("key", node.Attributes["data-nav"].Value.Split(',')[2]);
                jo.Add("clockup", node.SelectSingleNode("div/div[@class='matchDetails']/span[1]").InnerText);
                jo.Add("teams", node.SelectSingleNode("div/div[@class='teams']").InnerText.Trim());
                jo.Add("scores", node.SelectSingleNode("div/div[@class='scores']").InnerText);
                jo.Add("reverse", 0);
                ja.Add(jo);
            }
            foreach (HtmlNode node in nodes)
            {
                JObject jo = new JObject();
                jo.Add("key", node.Attributes["data-nav"].Value.Split(',')[2]);
                jo.Add("clockup", node.SelectSingleNode("div/div[@class='matchDetails']/span[1]").InnerText);
                jo.Add("teams", "反-" + node.SelectSingleNode("div/div[@class='teams']").InnerText.Trim());
                jo.Add("scores", node.SelectSingleNode("div/div[@class='scores']").InnerText);
                jo.Add("reverse", 1);
                ja.Add(jo);
            }
        }
        catch (Exception)
        {

            throw;
        }
        finally {
            json = ja.ToString();
        }
    }
}