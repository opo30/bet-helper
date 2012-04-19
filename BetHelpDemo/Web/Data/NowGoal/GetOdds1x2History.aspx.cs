﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.BLL;
using System.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SeoWebSite.DBUtility;
using SeoWebSite.Model;

namespace SeoWebSite.Web.Data.NowGoal
{
    public partial class GetOdds1x2ChangeList : System.Web.UI.Page
    {
        protected string StringJSON = "";
        ScheduleBLL scheduleBLL = new ScheduleBLL();
        ScheduleAnalysisBLL scheduleAnalysisBLL = new ScheduleAnalysisBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["a"] != null)
                {
                    switch (Request.QueryString["a"])
                    {
                        case "stat":
                            statOddsHistory();
                            break;
                        case "statdate":
                            statOddsHistoryDate();
                            break;
                        case "list":
                            queryOddsHistory();
                            break;
                        case "update":
                            updateOddsHistory();
                            break;
                        default:
                            break;
                    }
                }
            }

        }

        private void updateOddsHistory()
        {
            if (Request["odds"] != null && Request["scheduleid"] != null)
            {
                JObject result = new JObject();
                try
                {
                    string[] allOddsArray = Request.Form["odds"].Split('^');
                    List<string> swhereList = new List<string>();
                    List<string> ewhereList = new List<string>();
                    List<decimal> swlist = new List<decimal>();
                    List<decimal> sdlist = new List<decimal>();
                    List<decimal> sllist = new List<decimal>();
                    List<decimal> ewlist = new List<decimal>();
                    List<decimal> edlist = new List<decimal>();
                    List<decimal> ellist = new List<decimal>();
                    foreach (string oddsStr in allOddsArray)
                    {
                        string[] oddsArray = oddsStr.Split('|');
                        swhereList.Add("(companyid=" + oddsArray[0] + " and s_win=" + oddsArray[3] +
                                    " and s_draw=" + oddsArray[4] + " and s_lost=" + oddsArray[5] + ")");
                        swlist.Add(Convert.ToDecimal(oddsArray[6]));
                        sdlist.Add(Convert.ToDecimal(oddsArray[7]));
                        sllist.Add(Convert.ToDecimal(oddsArray[8]));
                        if (!string.IsNullOrEmpty(oddsArray[10]) && !string.IsNullOrEmpty(oddsArray[11]) && !string.IsNullOrEmpty(oddsArray[12]) && !string.IsNullOrEmpty(oddsArray[13]) && !string.IsNullOrEmpty(oddsArray[14]) && !string.IsNullOrEmpty(oddsArray[15]))
                        {
                            ewhereList.Add("(companyid=" + oddsArray[0] + " and e_win=" + oddsArray[10] +
                                    " and e_draw=" + oddsArray[11] + " and e_lost=" + oddsArray[12] + ")");
                            ewlist.Add(Convert.ToDecimal(oddsArray[13]));
                            edlist.Add(Convert.ToDecimal(oddsArray[14]));
                            ellist.Add(Convert.ToDecimal(oddsArray[15]));
                        }
                    }
                    DataSet sds = scheduleBLL.statOddsHistory("(" + String.Join(" or ", swhereList.ToArray()) + ")");
                    DataSet eds = scheduleBLL.statOddsHistory("(" + String.Join(" or ", ewhereList.ToArray()) + ")");
                    if (sds != null && eds != null)
                    {
                        ScheduleAnalysis model = new ScheduleAnalysis();
                        model.scheduleid = Convert.ToInt32(Request.Form["scheduleid"]);
                        model.oddswin = ewlist.Average() - swlist.Average();
                        model.oddsdraw = edlist.Average() - sdlist.Average();
                        model.oddslost = ellist.Average() - sllist.Average();
                        model.perwin = Convert.ToDecimal(eds.Tables[0].Rows[0][0]) - Convert.ToDecimal(sds.Tables[0].Rows[0][0]);
                        model.perdraw = Convert.ToDecimal(eds.Tables[0].Rows[0][1]) - Convert.ToDecimal(sds.Tables[0].Rows[0][1]);
                        model.perlost = Convert.ToDecimal(eds.Tables[0].Rows[0][2]) - Convert.ToDecimal(sds.Tables[0].Rows[0][2]);
                        model.time = DateTime.Now;
                        if (!scheduleAnalysisBLL.Exists(model))
                        {
                            scheduleAnalysisBLL.Add(model);
                            Response.Write("更新赔率成功");
                        }
                    }
                }
                catch (Exception e)
                {
                    Response.Write(e.Message);
                }
            }
        }

        private void statOddsHistoryDate()
        {
            if (Request["rowindex"] != null)
            {
                string strWhere = "";
                if (Request.Form["rowindex"] == "2")
                {
                    strWhere = Common.DataCache.GetCache("swhere").ToString();
                }
                else if (Request.Form["rowindex"] == "3")
                {
                    strWhere = Common.DataCache.GetCache("ewhere").ToString();
                }
                if (!String.IsNullOrEmpty(strWhere))
                {
                    DataSet ds = scheduleBLL.statOddsHistoryGroupByDate(Common.DataCache.GetCache("rangqiu").ToString(), strWhere);
                    JObject result = JObject.Parse("{success:true}");
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Converters.Add(new JavaScriptDateTimeConverter());
                    serializer.NullValueHandling = NullValueHandling.Ignore;
                    result.Add("data", JArray.FromObject(ds.Tables[0], serializer));
                    JsonConvert.SerializeObject(ds.Tables[0], new JavaScriptDateTimeConverter());
                    StringJSON = result.ToString();
                }
            }
        }

        private void queryOddsHistory()
        {
            if (Request["rowindex"] != null)
            {
                string strWhere = "";
                string cclassid = "";
                string sclassid = "";
                int rowindex = Convert.ToInt32(Request.Form["rowindex"]);
                if (rowindex % 2 == 0)
                {
                    strWhere = Common.DataCache.GetCache("swhere").ToString();
                }
                else
                {
                    strWhere = Common.DataCache.GetCache("ewhere").ToString();
                }
                if (rowindex == 2 || rowindex == 3)
                {
                    cclassid = Common.DataCache.GetCache("cclassid").ToString();
                }
                else if (rowindex == 4 || rowindex == 5)
                {
                    sclassid = Common.DataCache.GetCache("sclassid").ToString();
                }
                DataSet ds = scheduleBLL.queryOddsHistory(cclassid, sclassid, strWhere);
                JArray data = new JArray();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string[] Cookie = Request.Cookies["Cookie"].Value.Split('^');
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string[] scheduleArr = dr["sdata"].ToString().Split(',');
                        string[] scheduleClassArr = dr["sclass"].ToString().Split(',');
                        JObject row = new JObject();
                        row.Add("s_time", scheduleArr[10].Replace("<br>", " "));
                        row.Add("league", scheduleClassArr[1]);
                        row.Add("bgcolor", scheduleClassArr[4]);
                        row.Add("h_teamname", scheduleArr[4]);
                        row.Add("g_teamname", scheduleArr[7]);
                        row.Add("score", scheduleArr[13] + "-" + scheduleArr[14]);
                        row.Add("half", scheduleArr[15] + "-" + scheduleArr[16]);
                        row.Add("rangqiu", scheduleArr[25]);
                        if (!string.IsNullOrEmpty(scheduleArr[25]))
                        {
                            double numResult = Convert.ToDouble(scheduleArr[13]) - Convert.ToDouble(scheduleArr[14]) - Convert.ToDouble(scheduleArr[25]);
                            row.Add("numResult", numResult);
                        }

                        row.Add("scount", dr["scount"].ToString());
                        data.Add(row);
                    }
                }
                JObject result = JObject.Parse("{success:true}");
                result.Add("data", data);
                StringJSON = result.ToString();
            }
        }

        private string createEndWhere(string s,string e)
        {
            if (decimal.Parse(e) > decimal.Parse(s))
            {
                return ">=" + e;
            }
            else if (decimal.Parse(e) < decimal.Parse(s))
            {
                return "<=" + e;
            }
            else if (decimal.Parse(e) == decimal.Parse(s))
            {
                return "=" + e;
            }
            return "";
        }

        private void statOddsHistory()
        {
            if (Request["stypeid"] != null && Request["oddsarr"] != null)
            {
                try
                {
                    string[] sclassArr = Request.Form["stypeid"].Split('^');
                    string[] oddsArr = Request.Form["oddsarr"].Split('^');
                    string[] scheduleArr = Request.Form["schedulearr"].Split('^');
                    List<string> swhereList = new List<string>();
                    List<string> ewhereList = new List<string>();
                    List<string> oddswhereList = new List<string>();
                    List<decimal> swlist = new List<decimal>();
                    List<decimal> sdlist = new List<decimal>();
                    List<decimal> sllist = new List<decimal>();
                    List<decimal> ewlist = new List<decimal>();
                    List<decimal> edlist = new List<decimal>();
                    List<decimal> ellist = new List<decimal>();
                    foreach (string oddsStr in oddsArr)
                    {
                        string[] odds = oddsStr.Split('|');
                        swhereList.Add("(companyid=" + odds[0] + " and s_win=" + odds[3] +
                                " and s_draw=" + odds[4] + " and s_lost=" + odds[5] + ")");
                        swlist.Add(Convert.ToDecimal(odds[6]));
                        sdlist.Add(Convert.ToDecimal(odds[7]));
                        sllist.Add(Convert.ToDecimal(odds[8]));
                        if (!String.IsNullOrEmpty(odds[10]) && !String.IsNullOrEmpty(odds[11]) && !String.IsNullOrEmpty(odds[12]))
                        {
                            ewhereList.Add("(companyid=" + odds[0] + " and s_win=" + odds[3] +
                                " and s_draw=" + odds[4] + " and s_lost=" + odds[5] + " and e_win" + createEndWhere(odds[3], odds[10]) +
                                " and e_draw" + createEndWhere(odds[4], odds[11]) + " and e_lost" + createEndWhere(odds[5], odds[12]) + ")");
                            ewlist.Add(Convert.ToDecimal(odds[13]));
                            edlist.Add(Convert.ToDecimal(odds[14]));
                            ellist.Add(Convert.ToDecimal(odds[15]));
                        }
                    }
                    string swhereStr = "(" + String.Join(" or ", swhereList.ToArray()) + ")";
                    string ewhereStr = "(" + String.Join(" or ", ewhereList.ToArray()) + ")";

                    Common.DataCache.SetCache("swhere", swhereStr);
                    Common.DataCache.SetCache("ewhere", ewhereStr);
                    Common.DataCache.SetCache("cclassid", sclassArr[9]);
                    Common.DataCache.SetCache("sclassid", sclassArr[0]);

                    DataTable dt = new DataTable();
                    dt.Columns.Add("name", typeof(string));
                    dt.Columns.Add("perwin", typeof(float));
                    dt.Columns.Add("perdraw", typeof(float));
                    dt.Columns.Add("perlost", typeof(float));
                    dt.Columns.Add("rqwin", typeof(float));
                    dt.Columns.Add("rqdraw", typeof(float));
                    dt.Columns.Add("rqlost", typeof(float));
                    dt.Columns.Add("avgscore", typeof(float));
                    dt.Columns.Add("totalCount", typeof(int));

                    DataSet sds = scheduleBLL.statOddsHistory(null, null, swhereStr);
                    DataSet eds = scheduleBLL.statOddsHistory(null, null, ewhereStr);
                    dt.ImportRow(sds.Tables[0].Rows[0]);
                    dt.Rows[0]["name"] = "全局初";
                    dt.ImportRow(eds.Tables[0].Rows[0]);
                    dt.Rows[1]["name"] = "全局终";
                    sds = scheduleBLL.statOddsHistory(sclassArr[9], null, swhereStr);
                    eds = scheduleBLL.statOddsHistory(sclassArr[9], null, ewhereStr);
                    dt.ImportRow(sds.Tables[0].Rows[0]);
                    dt.Rows[2]["name"] = "国家初";
                    dt.ImportRow(eds.Tables[0].Rows[0]);
                    dt.Rows[3]["name"] = "国家终";
                    sds = scheduleBLL.statOddsHistory(null, sclassArr[0], swhereStr);
                    eds = scheduleBLL.statOddsHistory(null, sclassArr[0], ewhereStr);
                    dt.ImportRow(sds.Tables[0].Rows[0]);
                    dt.Rows[4]["name"] = "赛事初";
                    dt.ImportRow(eds.Tables[0].Rows[0]);
                    dt.Rows[5]["name"] = "赛事终";

                    List<decimal> support = new List<decimal>();
                    support.Add(ewlist.Average() - swlist.Average());
                    support.Add(edlist.Average() - sdlist.Average());
                    support.Add(ellist.Average() - sllist.Average());
                    support.Add(Convert.ToDecimal(dt.Rows[1][1]) - Convert.ToDecimal(dt.Rows[0][1]));
                    support.Add(Convert.ToDecimal(dt.Rows[1][2]) - Convert.ToDecimal(dt.Rows[0][2]));
                    support.Add(Convert.ToDecimal(dt.Rows[1][3]) - Convert.ToDecimal(dt.Rows[0][3]));
                    DataRow dr = dt.NewRow();
                    dr["name"] = "赔率";
                    dr["perwin"] = support[0];
                    dr["perdraw"] = support[1];
                    dr["perlost"] = support[2];
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    dr["name"] = "胜率";
                    dr["perwin"] = support[3];
                    dr["perdraw"] = support[4];
                    dr["perlost"] = support[5];
                    for (int i = 0; i < 3; i++)
                    {
                        if (support[i] > 0 && support[i + 3] > 1)
                        {
                            dr[4 + i] = support[i + 3] / Math.Ceiling(support[i]) * 10;
                        }
                    }
                    dt.Rows.Add(dr);

                    ScheduleAnalysis model = new ScheduleAnalysis();

                    //List<decimal> numList = new List<decimal>();
                    //numList.Add(soddsperwin.Average());
                    //numList.Add(soddsperdraw.Average());
                    //numList.Add(soddsperlost.Average());
                    //numList.Add(eoddsperwin.Average());
                    //numList.Add(eoddsperdraw.Average());
                    //numList.Add(eoddsperlost.Average());
                    //numList.Add(Convert.ToDecimal(dt.Rows[2][1]));
                    //numList.Add(Convert.ToDecimal(dt.Rows[2][2]));
                    //numList.Add(Convert.ToDecimal(dt.Rows[2][3]));
                    //numList.Add(Convert.ToDecimal(dt.Rows[3][1]));
                    //numList.Add(Convert.ToDecimal(dt.Rows[3][2]));
                    //numList.Add(Convert.ToDecimal(dt.Rows[3][3]));
                    //decimal[] numArr = numList.ToArray();
                    //numList.Sort();
                    //string res = "";
                    //foreach (decimal item in numArr)
                    //{
                    //    res += numList.IndexOf(item) + ",";
                    //}
                    //DataSet dsNum = DbHelperSQL.Query("select perwin=100.0*sum(case when a.home>a.away then 1 else 0 end)/count(a.id),perdraw=100.0*sum(case when a.home=a.away then 1 else 0 end)/count(a.id),perlost=100.0*sum(case when a.home<a.away then 1 else 0 end)/count(a.id),rqwin=100.0*sum(case when a.home-a.away>" + scheduleArr[25] + " then 1 else 0 end)/count(a.id),rqdraw=100.0*sum(case when a.home-a.away=" + scheduleArr[25] + " then 1 else 0 end)/count(a.id),rqlost=100.0*sum(case when a.home-a.away<" + scheduleArr[25] + " then 1 else 0 end)/count(a.id),avgscore=avg(1.0*(a.home+a.away)),count(a.id) totalCount  from schedule a join schedulerecord b on b.scheduleid=a.id and b.result='" + res + "'", 999);
                    //dt.ImportRow(dsNum.Tables[0].Rows[0]);


                    JObject result = JObject.Parse("{success:true}");
                    result.Add("data", JArray.FromObject(dt));
                    StringJSON = result.ToString();
                }
                catch (Exception e)
                {
                    StringJSON = "{success:false,error:message:'" + e.Message + "'}";
                }
            }
        }

        public void GetOdds1x2Change(string scheduleID, string[] companyids, string[] companynames, string tcompanyid)
        {
            try
            {
                HistoryOddsBLL bll = new HistoryOddsBLL();
                StringJSON = bll.GetOdds1x2ChangeList(scheduleID, companyids, companynames, tcompanyid);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e);
                throw;
            }

        }
    }
}
