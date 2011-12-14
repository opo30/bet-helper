using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using SeoWebSite.Model;
using SeoWebSite.Common.JSON;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using SeoWebSite.Common;
using Winista.Text.HtmlParser.Lex;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser.Util;
using Winista.Text.HtmlParser.Tags;
using SeoWebSite.BLL;

namespace SeoWebSite.BLL
{
    public class HistoryOddsBLL
    {
        private readonly SeoWebSite.DAL.Odds1x2HistoryDAO dal = new SeoWebSite.DAL.Odds1x2HistoryDAO();
        private DataSet ds;

        public void Delete(int scheduleID)
        {
            dal.Delete(scheduleID.ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public string Add(string scheduleID, string companyids, string historyids,DateTime time)
        {
            WebClientBLL bll = new WebClientBLL();
            string[] companyidArr = companyids.Split(',');
            string[] historyidArr = historyids.Split(',');
            int count = 0;
            if (companyidArr.Length == historyidArr.Length)
            {
                dal.Delete(scheduleID);
                for (int i = 0; i < companyidArr.Length; i++)
                {
                    string s = bll.GetOddsHistoryContent(historyidArr[i]);

                    Lexer lexer = new Lexer(s);
                    Parser parser = new Parser(lexer);
                    NodeList bodyNodes = parser.Parse(new TagNameFilter("HTML"))[0].Children.ExtractAllNodesThatMatch(new TagNameFilter("BODY"))[0].Children;
                    ITag table = bodyNodes.SearchFor(typeof(Winista.Text.HtmlParser.Tags.TableTag))[0] as ITag;

                    NodeList tableRows = table.Children.SearchFor(typeof(Winista.Text.HtmlParser.Tags.TableRow));

                    for (int f = 0; f < tableRows.Count; f++)
                    {
                        ITag row = tableRows[f] as ITag;
                        if (row.Attributes["ALIGN"].Equals("center") && row.Attributes["BGCOLOR"].Equals("#FFFFFF")){
                            Odds1x2History model = new Odds1x2History();
                            model.companyid = int.Parse(companyidArr[i]);
                            model.scheduleid = int.Parse(scheduleID);
                            model.home = float.Parse(row.Children[0].ToPlainTextString());
                            model.draw = float.Parse(row.Children[1].ToPlainTextString());
                            model.away = float.Parse(row.Children[2].ToPlainTextString());
                            this.FillOdds1x2History(model);
                            string[] t2 = row.Children[3].ToPlainTextString().Replace("showtime(", "").Replace(")", "").Split(',');
                            int yy = int.Parse(t2[0]);
                            int mm = int.Parse(t2[1].Remove(2));
                            int dd = int.Parse(t2[2]);
                            int hh = int.Parse(t2[3]);
                            int mi = int.Parse(t2[4]);
                            int ss = int.Parse(t2[5]);
                            model.time = new DateTime(yy, mm, dd, hh, mi, ss, DateTimeKind.Utc).AddHours(8d);
                            if (model.time > time)
                            {
                                continue;
                            }
                            dal.Add(model);
                            count++;
                        }
                    }
                }
            }
            JSONHelper json = new JSONHelper();
            json.success = true;
            json.totlalCount = count;
            return json.ToString();
        }

        /// <summary>
        /// 获得历史改变的赔率
        /// </summary>
        /// <param name="scheduleid"></param>
        /// <param name="companyids"></param>
        /// <param name="companynames"></param>
        /// <returns></returns>
        public string GetOdds1x2ChangeList(string scheduleid, string[] companyids, string[] companynames, string tcompanyid)
        {
            JSONHelper json = new JSONHelper();
            for (int i = 0; i < companyids.Length; i++)
            {
                List<Odds1x2History> list = dal.GetList(scheduleid, companyids[i]);
                json.success = true;
                foreach (Odds1x2History model in list)
                {
                    this.FillOdds1x2Kelly(model, tcompanyid);
                    //投注记录表数据
                    json.AddItem("id", model.id.ToString());
                    json.AddItem("scheduleid", model.scheduleid.ToString());
                    json.AddItem("companyid", model.companyid.ToString());
                    json.AddItem("companyname", companynames[i]);
                    json.AddItem("home", model.home.ToString());
                    json.AddItem("draw", model.draw.ToString());
                    json.AddItem("away", model.away.ToString());
                    json.AddItem("homep", model.homep.ToString());
                    json.AddItem("drawp", model.drawp.ToString());
                    json.AddItem("awayp", model.awayp.ToString());
                    json.AddItem("homek", model.homek.ToString());
                    json.AddItem("drawk", model.drawk.ToString());
                    json.AddItem("awayk", model.awayk.ToString());
                    json.AddItem("returnrate", model.returnrate.ToString());
                    json.AddItem("time", model.time.ToString("yyyy-MM-dd HH:mm"));

                    json.ItemOk();
                }
                json.totlalCount += list.Count;
            }

            return json.ToString();
        }

        private void FillOdds1x2Kelly(Odds1x2History model, string tcompanyid)
        {
            if (string.IsNullOrEmpty(tcompanyid))
            {
                ds = dal.GetAvgNumber(model.scheduleid, model.time);
                model.homek = model.home * (float.Parse(ds.Tables[0].Rows[0]["avghomep"].ToString()));
                model.drawk = model.draw * (float.Parse(ds.Tables[0].Rows[0]["avgdrawp"].ToString()));
                model.awayk = model.away * (float.Parse(ds.Tables[0].Rows[0]["avgawayp"].ToString()));
            }
            else
            {
                ds = dal.GetCompanyLastOdds(model.scheduleid, int.Parse(tcompanyid), model.time);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    model.homek = model.home * float.Parse(dr["homep"].ToString());
                    model.drawk = model.draw * float.Parse(dr["drawp"].ToString());
                    model.awayk = model.away * float.Parse(dr["awayp"].ToString());
                }
                else
                {
                    ds = dal.GetAvgNumber(model.scheduleid, model.time);
                    model.homek = model.home * (float.Parse(ds.Tables[0].Rows[0]["avghomep"].ToString()));
                    model.drawk = model.draw * (float.Parse(ds.Tables[0].Rows[0]["avgdrawp"].ToString()));
                    model.awayk = model.away * (float.Parse(ds.Tables[0].Rows[0]["avgawayp"].ToString()));
                }
            }
        }

        /// <summary>
        /// 填充概率和返回率
        /// </summary>
        /// <param name="model"></param>
        private void FillOdds1x2History(Odds1x2History model)
        {
            model.returnrate = 1 / ((1 / model.home) + (1 / model.draw) + (1 / model.away));
            model.homep = 100 / (model.home / model.returnrate);
            model.drawp = 100 / (model.draw / model.returnrate);
            model.awayp = 100 / (model.away / model.returnrate);
        }

        

        public string GetOdds1x2ChangeListByTime(string scheduleID, string[] companynames, long timespace)
        {
            ds = dal.GetFirstTimePoint(scheduleID);
            DateTime time = DateTime.Parse(ds.Tables[0].Rows[0][0].ToString());
            DateTime nowtime = DateTime.Now;
            JSONHelper json = new JSONHelper();
            while (time > nowtime)
            {
                //ds = dal.GetAvgNumber(int.Parse(scheduleID), time);
                //json.AddItem("home", );
                //json.AddItem("draw", model.draw.ToString());
                //json.AddItem("away", model.away.ToString());

                //time.AddSeconds(3600);
            }
            json.success = true;

            throw new NotImplementedException();
        }

        public string GetCompanyOdds1x2ChangeListByTime(string scheduleID, string[] companyids, string[] companynames, long timespace)
        {
            DataSet dstime;
            if (companyids.Length == 0)
            {
                dstime = dal.GetFirstTimePoint(scheduleID);
            }
            else
            {
                dstime = dal.GetCompanyStartPoint(scheduleID, companyids);
            }
            DateTime firsttime = DateTime.Parse(dstime.Tables[0].Rows[0][0].ToString());
            DateTime nowtime = DateTime.Now;

            while (firsttime < nowtime)
            {
                //ds = dal.
            }

            throw new NotImplementedException();
        }

        public string GetOdds1x2ChartsData(string scheduleID, string[] companyids, double time)
        {
            DataSet dsStart = dal.GetCompanyStartPoint(scheduleID, companyids);
            DataSet dsEnd = dal.GetCompanyEndPoint(scheduleID, companyids);
            DateTime firsttime;
            DateTime lasttime = DateTime.Parse(dsEnd.Tables[0].Rows[0][0].ToString());
            if (time == 0)
            {
                firsttime = DateTime.Parse(dsStart.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                firsttime = lasttime.AddSeconds(0 - time);
            }

            if (firsttime < DateTime.Parse(dsStart.Tables[0].Rows[0][0].ToString()))
            {
                firsttime = DateTime.Parse(dsStart.Tables[0].Rows[0][0].ToString());
            }

            double timespace = (lasttime - firsttime).TotalSeconds / 30;
            int index = 0;
            Odds1x2History firthmodel = new Odds1x2History();
            JArray data = new JArray();
            while (firsttime < lasttime)
            {
                ds = dal.GetAveNumByCompanys(scheduleID, string.Join(",", companyids), firsttime);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    JObject item = new JObject();
                    DataRow dr = ds.Tables[0].Rows[0];
                    Odds1x2History model = new Odds1x2History();
                    model.home = float.Parse(dr["avghome"].ToString());
                    model.draw = float.Parse(dr["avgdraw"].ToString());
                    model.away = float.Parse(dr["avgaway"].ToString());
                    if (index == 0)
                    {
                        firthmodel = model;
                    }
                    item.Add("home", (model.home - firthmodel.home) * 100);
                    item.Add("draw", (model.draw - firthmodel.draw) * 100);
                    item.Add("away", (model.away - firthmodel.away) * 100);
                    item.Add("time", firsttime.ToString());
                    data.Add(item);
                    firsttime = firsttime.AddSeconds(timespace);
                    index++;
                }
            }
            JObject result = new JObject();
            result.Add(new JProperty("totlalCount", data.Count));
            result.Add(new JProperty("data", data));
            return result.ToString();
        }

        /// <summary>
        /// 获得平均凯利线性图形
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <param name="companyids"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public string GetKellyAverageLineChart(string scheduleID, string[] companyids, double time,bool isRefresh)
        {
            string jsonStr = "";
            BetExpBLL bll = new BetExpBLL();
            if (bll.Exists(int.Parse(scheduleID)) && !isRefresh)
            {
                BetExp betExp = bll.GetModel(int.Parse(scheduleID));
                jsonStr = betExp.data;
            }
            else
            {
                if (companyids == null || companyids.Length == 0)
                {
                    companyids = dal.getLastCompanyIDs(scheduleID, 1);
                }

                DataSet dsStart = dal.GetCompanyStartPoint(scheduleID, companyids);
                DataSet dsEnd = dal.GetCompanyEndPoint(scheduleID, companyids);

                DateTime firsttime = DateTime.Parse(dsStart.Tables[0].Rows[0][0].ToString());
                DateTime lasttime = DateTime.Parse(dsEnd.Tables[0].Rows[0][0].ToString());

                DateTime firsttime1 = lasttime.AddSeconds(-time);

                if (firsttime1 > firsttime)
                {
                    firsttime = firsttime1;
                }
                float inithome = 0;
                float initdraw = 0;
                float initaway = 0;
                float initrate = 0;

                JsonSerializer serializer = GetJsonSerializer();
                double timespace = lasttime.Subtract(firsttime).TotalSeconds / 30;
                Odds1x2History firthmodel = new Odds1x2History();
                JArray data = new JArray();
                while (firsttime <= lasttime)
                {
                    ds = dal.GetAveNumByCompanys(scheduleID, string.Join(",", companyids), firsttime);
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        List<float> homeKelly = new List<float>();
                        List<float> drawKelly = new List<float>();
                        List<float> awayKelly = new List<float>();
                        List<float> returnrateKelly = new List<float>();
                        List<Odds1x2History> oddsList = dal.GetLastListByCompanys(int.Parse(scheduleID), string.Join(",", companyids), firsttime);
                        foreach (Odds1x2History item in oddsList)
                        {
                            homeKelly.Add(item.home * float.Parse(dr["avghomep"].ToString()));
                            drawKelly.Add(item.draw * float.Parse(dr["avgdrawp"].ToString()));
                            awayKelly.Add(item.away * float.Parse(dr["avgawayp"].ToString()));
                            returnrateKelly.Add(item.returnrate * 100);
                        }

                        JObject result = new JObject();
                        if (data.Count <= 0)
                        {
                            inithome = homeKelly.Average();
                            initdraw = drawKelly.Average();
                            initaway = awayKelly.Average();
                            initrate = returnrateKelly.Average();
                        }

                        result.Add("avehome", (homeKelly.Average() - inithome) * 10);
                        result.Add("avedraw", (drawKelly.Average() - initdraw) * 10);
                        result.Add("aveaway", (awayKelly.Average() - initaway) * 10);
                        result.Add("returnrate", (returnrateKelly.Average() - initrate) * 10);
                        result.Add("varhome", this.CalculationVariance(homeKelly));
                        result.Add("vardraw", this.CalculationVariance(drawKelly));
                        result.Add("varaway", this.CalculationVariance(awayKelly));
                        result.Add("time", JProperty.FromObject(firsttime, serializer));
                        data.Add(result);
                        firsttime = firsttime.AddSeconds(timespace);
                    }
                }
                jsonStr =  data.ToString();
                bll.Delete(int.Parse(scheduleID));
                string[] valueArr = new string[data.Count];
                for (int i = 0; i < data.Count; i++)
                {
                    valueArr[i] = bll.GetTrendsValue(JObject.Parse(data[data.Count - (data.Count - i)].ToString()),"");
                }
                bll.Add(new BetExp(int.Parse(scheduleID), jsonStr, string.Join(",", valueArr),"*" + bll.GetChangesValue(data,""), false));
            }
            return jsonStr;
        }

        /// <summary>
        /// 查询博彩公司的凯利指数图形
        /// </summary>
        /// <param name="scheduleID">比赛编码</param>
        /// <param name="companyids">博彩公司编码</param>
        /// <param name="time">时间范围 默认24小时</param>
        /// <returns></returns>
        public string GetOddsKellyChartsData(string scheduleID, string[] companyids)
        {
            string cacheKey = scheduleID + "_" + string.Join(",", companyids);
            string resData = "";
            if (DataCache.GetCache(cacheKey) == null) {
                List<Odds1x2History> oddsList = dal.GetList(scheduleID, string.Join(",", companyids));
                List<Odds1x2History> resultList = new List<Odds1x2History>();
                foreach (Odds1x2History item in oddsList)
                {
                    ds = dal.GetAvgNumber(int.Parse(scheduleID), item.time);
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        item.homek = item.home * float.Parse(dr["avghomep"].ToString());
                        item.drawk = item.draw * float.Parse(dr["avgdrawp"].ToString());
                        item.awayk = item.away * float.Parse(dr["avgawayp"].ToString());
                        item.returnrate *= 100;
                    }
                    resultList.Add(item);
                }
                JsonSerializer serializer = new JsonSerializer();
                serializer.Converters.Add(new JavaScriptDateTimeConverter());
                serializer.NullValueHandling = NullValueHandling.Ignore;
                JObject result = new JObject();
                result.Add("success", true);
                result.Add("total", oddsList.Count);
                result.Add("data", JArray.FromObject(resultList, serializer));
                resData = result.ToString();
                DataCache.SetCache(scheduleID + "_" + string.Join(",", companyids), resData);
            }
            return DataCache.GetCache(cacheKey).ToString();
        }
        


        public string GetAveKellyChartsData(string scheduleID, string[] companyids)
        {
            JSONHelper json = new JSONHelper();
            json.success = true;
            List<Odds1x2History> firstOddsList = dal.GetFirstListByCompanys(int.Parse(scheduleID), string.Join(",", companyids));
            List<float> firstHomekList = new List<float>();
            List<float> firstDrawkList = new List<float>();
            List<float> firstAwaykList = new List<float>();
            int firstHomekCount = 0;
            int firstDrawkCount = 0;
            int firstAwaykCount = 0;
            foreach (Odds1x2History model in firstOddsList)
            {
                this.FillOdds1x2Kelly(model, "");
                firstHomekList.Add(model.homek);
                firstDrawkList.Add(model.drawk);
                firstAwaykList.Add(model.awayk);
                if (model.homek < model.awayk && model.homek < model.drawk)
                {
                    firstHomekCount++;
                }
                else if (model.homek >= 100)
                {
                    firstHomekCount--;
                }
                if (model.drawk < model.awayk && model.drawk < model.homek)
                {
                    firstDrawkCount++;
                }
                else if (model.drawk >= 100)
                {
                    firstDrawkCount--;
                }
                if (model.awayk < model.homek && model.awayk < model.drawk)
                {
                    firstAwaykCount++;
                }
                else if (model.awayk >= 100)
                {
                    firstAwaykCount--;
                }
            }

            List<Odds1x2History> lastOddsList = dal.GetLastListByCompanys(int.Parse(scheduleID), string.Join(",", companyids));
            List<float> lastHomekList = new List<float>();
            List<float> lastDrawkList = new List<float>();
            List<float> lastAwaykList = new List<float>();
            int lastHomekCount = 0;
            int lastDrawkCount = 0;
            int lastAwaykCount = 0;
            foreach (Odds1x2History model in lastOddsList)
            {
                this.FillOdds1x2Kelly(model, "");
                lastHomekList.Add(model.homek);
                lastDrawkList.Add(model.drawk);
                lastAwaykList.Add(model.awayk);
                if (model.homek < model.awayk && model.homek < model.drawk)
                {
                    lastHomekCount++;
                }
                else if (model.homek >= 100)
                {
                    lastHomekCount--;
                }
                if (model.drawk < model.awayk && model.drawk < model.homek)
                {
                    lastDrawkCount++;
                }
                else if (model.drawk >= 100)
                {
                    lastDrawkCount--;
                }
                if (model.awayk < model.homek && model.awayk < model.drawk)
                {
                    lastAwaykCount++;
                }
                else if (model.awayk >= 100)
                {
                    lastAwaykCount--;
                }
            }

            json.AddItem("name", "主胜");
            json.AddItem("firstkelly", firstHomekList.Average().ToString());
            json.AddItem("lastkelly", lastHomekList.Average().ToString());
            json.AddItem("firstsupport", firstHomekCount.ToString());
            json.AddItem("lastsupport", lastHomekCount.ToString());
            json.ItemOk();
            json.AddItem("name", "平局");
            json.AddItem("firstkelly", firstDrawkList.Average().ToString());
            json.AddItem("lastkelly", lastDrawkList.Average().ToString());
            json.AddItem("firstsupport", firstDrawkCount.ToString());
            json.AddItem("lastsupport", lastDrawkCount.ToString());
            json.ItemOk();
            json.AddItem("name", "客胜");
            json.AddItem("firstkelly", firstAwaykList.Average().ToString());
            json.AddItem("lastkelly", lastAwaykList.Average().ToString());
            json.AddItem("firstsupport", firstAwaykCount.ToString());
            json.AddItem("lastsupport", lastAwaykCount.ToString());
            json.ItemOk();

            return json.ToString();
        }


        /// <summary>
        /// 计算方差
        /// </summary>
        /// <param name="numberList"></param>
        /// <returns></returns>
        private float CalculationVariance(List<float> numberList)
        {
            double result = 0;
            double ave = numberList.Average();
            foreach (float f in numberList)
            {
                result += Math.Pow((ave - f), 2);
            }
            result = result / numberList.Count;
            return (float)result;
        }

        public string GetOddsCompanyChartsData(string scheduleID, string[] companyids, double time)
        {
            JSONHelper json = new JSONHelper();
            json.success = true;
            DataSet dsStart = dal.GetCompanyStartPoint(scheduleID, companyids);
            DataSet dsEnd = dal.GetCompanyEndPoint(scheduleID, companyids);
            DateTime firsttime;
            DateTime lasttime = DateTime.Parse(dsEnd.Tables[0].Rows[0][0].ToString());
            if (time == 0)
            {
                firsttime = DateTime.Parse(dsStart.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                firsttime = lasttime.AddSeconds(0 - time);
            }

            double timespace = (lasttime - firsttime).TotalSeconds / 60;

            while (firsttime <= lasttime)
            {
                Odds1x2History model = new Odds1x2History();
                ds = dal.GetAvgNumber(int.Parse(scheduleID), firsttime);
                DataRow dr = ds.Tables[0].Rows[0];

                List<Odds1x2History> oddsList = dal.GetLastListByCompanys(int.Parse(scheduleID), string.Join(",", companyids), firsttime);
                int home = 0;
                int draw = 0;
                int away = 0;
                int nhome = 0;
                int ndraw = 0;
                int naway = 0;
                int count = 0;
                foreach (Odds1x2History item in oddsList)
                {
                    item.homek = item.home * float.Parse(dr["avghomep"].ToString());
                    item.drawk = item.draw * float.Parse(dr["avgdrawp"].ToString());
                    item.awayk = item.away * float.Parse(dr["avgawayp"].ToString());
                    if (item.homek < item.returnrate * 100 - 2)
                    {
                        home++;
                    }
                    else if (item.homek >= item.returnrate * 100 + 2)
                    {
                        nhome--;
                    }
                    if (item.drawk < item.returnrate * 100 - 2)
                    {
                        draw++;
                    }
                    else if (item.drawk >= item.returnrate * 100 + 2)
                    {
                        ndraw--;
                    }
                    if (item.awayk < item.returnrate * 100 - 2)
                    {
                        away++;
                    }
                    else if (item.awayk >= item.returnrate * 100 + 2)
                    {
                        naway--;
                    }
                    count++;
                }
                json.AddItem("home", home.ToString());
                json.AddItem("draw", draw.ToString());
                json.AddItem("away", away.ToString());
                json.AddItem("nhome", nhome.ToString());
                json.AddItem("ndraw", ndraw.ToString());
                json.AddItem("naway", naway.ToString());
                json.AddItem("count", count.ToString());
                json.AddItem("time", firsttime.ToString());
                json.ItemOk();
                firsttime = firsttime.AddSeconds(timespace);

            }
            json.totlalCount = ds.Tables[0].Rows.Count;
            return json.ToString();
        }

        public string GetOddsPointChartsData(string scheduleID, string[] companyids)
        {
            JSONHelper json = new JSONHelper();
            json.success = true;
            DataSet dsAllTime = dal.GetChangeTimesByCompanys(scheduleID, string.Join(",", companyids));

            List<float> avehomeklist = new List<float>();
            List<float> avedrawklist = new List<float>();
            List<float> aveawayklist = new List<float>();
            List<float> avehomevlist = new List<float>();
            List<float> avedrawvlist = new List<float>();
            List<float> aveawayvlist = new List<float>();
            List<float> avereratelist = new List<float>();

            Odds1x2History lastmodel = new Odds1x2History();
            foreach (DataRow dr in dsAllTime.Tables[0].Rows)
            {
                ds = dal.GetAvgNumber(int.Parse(scheduleID), DateTime.Parse(dr[0].ToString()));
                List<Odds1x2History> oddsList = dal.GetLastListByCompanys(int.Parse(scheduleID), string.Join(",", companyids), DateTime.Parse(dr["time"].ToString()));
                List<float> homeklist = new List<float>();
                List<float> drawklist = new List<float>();
                List<float> awayklist = new List<float>();
                List<float> reratelist = new List<float>();
                foreach (Odds1x2History item in oddsList)
                {
                    homeklist.Add(item.home * float.Parse(ds.Tables[0].Rows[0]["avghomep"].ToString()));
                    drawklist.Add(item.draw * float.Parse(ds.Tables[0].Rows[0]["avgdrawp"].ToString()));
                    awayklist.Add(item.away * float.Parse(ds.Tables[0].Rows[0]["avgawayp"].ToString()));
                    reratelist.Add(item.returnrate * 100);
                }
                avehomeklist.Add(homeklist.Average());
                avedrawklist.Add(drawklist.Average());
                aveawayklist.Add(awayklist.Average());
                avehomevlist.Add(this.CalculationVariance(homeklist));
                avedrawvlist.Add(this.CalculationVariance(drawklist));
                aveawayvlist.Add(this.CalculationVariance(awayklist));
                avereratelist.Add(reratelist.Average());
            }
            int h = 0, d = 0, a = 0;
            int hh = 0, dd = 0, aa = 0;
            for (int i = 1; i < avehomeklist.Count; i++)
            {
                if (avehomeklist[i] < avehomeklist[i - 1])
                    h++;
                else if (avehomeklist[i] > avehomeklist[i - 1])
                    h--;
                if (avedrawklist[i] < avedrawklist[i - 1])
                    d++;
                else if (avedrawklist[i] > avedrawklist[i - 1])
                    d--;
                if (aveawayklist[i] < aveawayklist[i - 1])
                    a++;
                else if (aveawayklist[i] > aveawayklist[i - 1])
                    a--;
                if (avehomevlist[i] <= avehomevlist[i - 1])
                {
                    if (avehomeklist[i] < avehomeklist[i - 1])
                        hh++;
                    else if (avehomeklist[i] > avehomeklist[i - 1])
                        hh--;
                }
                if (avedrawvlist[i] <= avedrawvlist[i - 1])
                {
                    if (avedrawklist[i] < avedrawklist[i - 1])
                        dd++;
                    else if (avedrawklist[i] > avedrawklist[i - 1])
                        dd--;
                }
                if (aveawayvlist[i] <= aveawayvlist[i - 1])
                {
                    if (aveawayklist[i] < aveawayklist[i - 1])
                        aa++;
                    else if (aveawayklist[i] > aveawayklist[i - 1])
                        aa--;
                }
            }
            json.AddItem("name", "主胜");
            json.AddItem("point", h.ToString());
            json.AddItem("xpoint", hh.ToString());
            json.ItemOk();
            json.AddItem("name", "平局");
            json.AddItem("point", d.ToString());
            json.AddItem("xpoint", dd.ToString());
            json.ItemOk();
            json.AddItem("name", "客胜");
            json.AddItem("point", a.ToString());
            json.AddItem("xpoint", aa.ToString());
            json.ItemOk();


            json.totlalCount = 3;
            return json.ToString();
        }


        public string GetCompanysSupportChartsData(string scheduleID, string[] companyids)
        {
            List<Odds1x2History> oddsFirstList = dal.GetFirstListByCompanys(int.Parse(scheduleID), string.Join(",", companyids));
            List<Odds1x2History> oddsLastList = dal.GetLastListByCompanys(int.Parse(scheduleID), string.Join(",", companyids));

            int h = 0, d = 0, a = 0;
            int nh = 0, nd = 0, na = 0;
            int hh = 0, dd = 0, aa = 0;
            int nhh = 0, ndd = 0, naa = 0;

            foreach (Odds1x2History item in oddsFirstList)
            {
                ds = dal.GetAvgNumber(int.Parse(scheduleID), item.time);
                item.homek = item.home * float.Parse(ds.Tables[0].Rows[0]["avghomep"].ToString());
                item.drawk = item.draw * float.Parse(ds.Tables[0].Rows[0]["avgdrawp"].ToString());
                item.awayk = item.away * float.Parse(ds.Tables[0].Rows[0]["avgawayp"].ToString());
                if (item.homek < item.returnrate * 100 - 2)
                {
                    h++;
                }
                else if (item.homek >= item.returnrate * 100 + 2)
                {
                    nh++;
                }
                if (item.drawk < item.returnrate * 100 - 2)
                {
                    d++;
                }
                else if (item.drawk >= item.returnrate * 100 + 2)
                {
                    nd++;
                }
                if (item.awayk < item.returnrate * 100 - 2)
                {
                    a++;
                }
                else if (item.awayk >= item.returnrate * 100 + 2)
                {
                    na++;
                }
            }

            foreach (Odds1x2History item in oddsLastList)
            {
                ds = dal.GetAvgNumber(int.Parse(scheduleID), item.time);
                item.homek = item.home * float.Parse(ds.Tables[0].Rows[0]["avghomep"].ToString());
                item.drawk = item.draw * float.Parse(ds.Tables[0].Rows[0]["avgdrawp"].ToString());
                item.awayk = item.away * float.Parse(ds.Tables[0].Rows[0]["avgawayp"].ToString());
                if (item.homek < item.returnrate * 100 - 2)
                {
                    hh++;
                }
                else if (item.homek >= item.returnrate * 100 + 2)
                {
                    nhh++;
                }
                if (item.drawk < item.returnrate * 100 - 2)
                {
                    dd++;
                }
                else if (item.drawk >= item.returnrate * 100 + 2)
                {
                    ndd++;
                }
                if (item.awayk < item.returnrate * 100 - 2)
                {
                    aa++;
                }
                else if (item.awayk >= item.returnrate * 100 + 2)
                {
                    naa++;
                }
            }

            JArray data = new JArray();
            JObject obj = new JObject();
            obj.Add("name", "初盘主胜");
            obj.Add("point", h);
            obj.Add("xpoint", nh);
            data.Add(obj);
            obj = new JObject();
            obj.Add("name", "初盘平局");
            obj.Add("point", d.ToString());
            obj.Add("xpoint", nd.ToString());
            data.Add(obj);
            obj = new JObject();
            obj.Add("name", "初盘客胜");
            obj.Add("point", a.ToString());
            obj.Add("xpoint", na.ToString());
            data.Add(obj);
            obj = new JObject();
            obj.Add("name", "临场主胜");
            obj.Add("point", hh.ToString());
            obj.Add("xpoint", nhh.ToString());
            data.Add(obj);
            obj = new JObject();
            obj.Add("name", "临场平局");
            obj.Add("point", dd.ToString());
            obj.Add("xpoint", ndd.ToString());
            data.Add(obj);
            obj = new JObject();
            obj.Add("name", "临场客胜");
            obj.Add("point", aa.ToString());
            obj.Add("xpoint", naa.ToString());
            data.Add(obj);
            obj = new JObject();
            obj.Add("name", "主胜差值");
            obj.Add("point", Math.Abs(h - hh));
            obj.Add("xpoint", Math.Abs(nh - nhh));
            data.Add(obj);
            obj = new JObject();
            obj.Add("name", "平局差值");
            obj.Add("point", Math.Abs(d - dd));
            obj.Add("xpoint", Math.Abs(nd - ndd));
            data.Add(obj);
            obj = new JObject();
            obj.Add("name", "客胜差值");
            obj.Add("point", Math.Abs(a - aa));
            obj.Add("xpoint", Math.Abs(na - naa));
            data.Add(obj);
            JObject result = new JObject();
            result.Add(new JProperty("totlalCount", data.Count));
            result.Add(new JProperty("data", data));
            return result.ToString();
        }


        public string GetOddsDataCount(string scheduleID)
        {
            ds = dal.GetOddsDataCount(scheduleID);
            int recordcount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            JObject result = new JObject();
            result.Add(new JProperty("recordcount", recordcount));
            if (recordcount > 0)
            {
                ds = dal.GetLastOdds(int.Parse(scheduleID), DateTime.Now);
                result.Add(new JProperty("lasttime", DateTime.Parse(ds.Tables[0].Rows[0]["time"].ToString()).ToString("MM月dd HH时mm分")));
            }
            return result.ToString();
        }

        private JsonSerializer GetJsonSerializer()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            return serializer;
        }

        public bool Exists(string matchid){
            return dal.Exists(matchid);
        }

        public int GetCount(int scheduleid)
        {
            return dal.GetCount(scheduleid);
        }
    }
}
