using System;
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
using SeoWebSite.Common;
using System.Text;
using System.Collections.Specialized;

namespace SeoWebSite.Web.Data.NowGoal
{
    public partial class GetOdds1x2History : System.Web.UI.Page
    {
        protected string StringJSON = "";
        ScheduleBLL scheduleBLL = new ScheduleBLL();
        ScheduleAnalysisBLL scheduleAnalysisBLL = new ScheduleAnalysisBLL();
        FetionProxy fetion = new FetionProxy("13871459996", "thinkpadt60");

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
                        case "stat1":
                            statOddsHistory1();
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
                        case "today":
                            statTodayHistory();
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

        private string createEndWhere(string e, double cha)
        {
            //if (decimal.Parse(e) > decimal.Parse(s))
            //{
            //    return ">=" + e;
            //}
            //else if (decimal.Parse(e) < decimal.Parse(s))
            //{
            //    return "<=" + e;
            //}
            //else if (decimal.Parse(e) == decimal.Parse(s))
            //{
            //    return "=" + e;
            //}

            return "between " + (double.Parse(e) - cha) + " and " + (double.Parse(e) + cha);
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
                            ewhereList.Add("(companyid=" + odds[0] + " and (e_win=" + odds[10] +
                                ") and (e_draw=" + odds[11] + ") and (e_lost=" + odds[12] + "))");
                            ewlist.Add(Convert.ToDecimal(odds[13]));
                            edlist.Add(Convert.ToDecimal(odds[14]));
                            ellist.Add(Convert.ToDecimal(odds[15]));
                        }
                    }
                    string swhereStr = "(" + String.Join(" or ", swhereList.ToArray()) + ")";
                    string ewhereStr = "(" + String.Join(" or ", ewhereList.ToArray()) + ")";
                    //string ewhereStr = "(" + String.Join(" or ", ewhereList.ToArray()) + ")";

                    DataSet sds = scheduleBLL.statOddsHistory1(null, null, swhereStr);
                    DataSet eds = scheduleBLL.statOddsHistory1(null, null, ewhereStr);

                    DataTable dt = new DataTable();
                    dt.Columns.Add("name", typeof(string));
                    dt.Columns.Add("perwin", typeof(float));
                    dt.Columns.Add("perdraw", typeof(float));
                    dt.Columns.Add("perlost", typeof(float));
                    dt.Columns.Add("oddswin", typeof(float));
                    dt.Columns.Add("oddsdraw", typeof(float));
                    dt.Columns.Add("oddslost", typeof(float));
                    dt.Columns.Add("avgscore", typeof(float));
                    dt.Columns.Add("totalCount", typeof(int));

                    if (Convert.ToInt32(eds.Tables[0].Rows[0]["totalCount"]) > 0 &&
                        Convert.ToInt32(sds.Tables[0].Rows[0]["totalCount"]) > 0)
                    {
                        Common.DataCache.SetCache("swhere", swhereStr);
                        Common.DataCache.SetCache("ewhere", ewhereStr);
                        Common.DataCache.SetCache("cclassid", sclassArr[9]);
                        Common.DataCache.SetCache("sclassid", sclassArr[0]);

                        dt.ImportRow(sds.Tables[0].Rows[0]);
                        dt.Rows[0]["name"] = "全局初";
                        dt.Rows[0]["oddswin"] = swlist.Average();
                        dt.Rows[0]["oddsdraw"] = sdlist.Average();
                        dt.Rows[0]["oddslost"] = sllist.Average();
                        dt.ImportRow(eds.Tables[0].Rows[0]);
                        dt.Rows[1]["name"] = "全局终";
                        dt.Rows[1]["oddswin"] = ewlist.Average();
                        dt.Rows[1]["oddsdraw"] = edlist.Average();
                        dt.Rows[1]["oddslost"] = ellist.Average();
                        sds = scheduleBLL.statOddsHistory1(sclassArr[9], null, swhereStr);
                        eds = scheduleBLL.statOddsHistory1(sclassArr[9], null, ewhereStr);
                        dt.ImportRow(sds.Tables[0].Rows[0]);
                        dt.Rows[2]["name"] = "国家初";
                        dt.Rows[2]["oddswin"] = Convert.ToDecimal(dt.Rows[0][1]) - swlist.Average();
                        dt.Rows[2]["oddsdraw"] = Convert.ToDecimal(dt.Rows[0][2]) - sdlist.Average();
                        dt.Rows[2]["oddslost"] = Convert.ToDecimal(dt.Rows[0][3]) - sllist.Average();
                        dt.ImportRow(eds.Tables[0].Rows[0]);
                        dt.Rows[3]["name"] = "国家终";
                        dt.Rows[3]["oddswin"] = Convert.ToDecimal(dt.Rows[1][1]) - ewlist.Average();
                        dt.Rows[3]["oddsdraw"] = Convert.ToDecimal(dt.Rows[1][2]) - edlist.Average();
                        dt.Rows[3]["oddslost"] = Convert.ToDecimal(dt.Rows[1][3]) - ellist.Average();
                        sds = scheduleBLL.statOddsHistory1(null, sclassArr[0], swhereStr);
                        eds = scheduleBLL.statOddsHistory1(null, sclassArr[0], ewhereStr);
                        dt.ImportRow(sds.Tables[0].Rows[0]);
                        dt.Rows[4]["name"] = "赛事初";
                        dt.ImportRow(eds.Tables[0].Rows[0]);
                        dt.Rows[5]["name"] = "赛事终";
                    }

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

        private void statOddsHistory1()
        {
            if (Request["stypeid"] != null && Request["oddsarr"] != null)
            {
                string[] sclassArr = Request.Form["stypeid"].Split('^');
                string[] oddsArr = Request.Form["oddsarr"].Split('^');
                string[] scheduleArr = Request.Form["schedulearr"].Split('^');
                string[] oddsInfo = Request.Form["odds"].Split(',');
                List<string> swhereList = new List<string>();
                List<string> ewhereList = new List<string>();
                foreach (string oddsStr in oddsArr)
                {
                    string[] odds = oddsStr.Split('|');
                    swhereList.Add("(companyid=" + odds[0] + " and s_win=" + odds[3] +
                            " and s_draw=" + odds[4] + " and s_lost=" + odds[5] + ")");
                    if (!String.IsNullOrEmpty(odds[10]) && !String.IsNullOrEmpty(odds[11]) && !String.IsNullOrEmpty(odds[12]))
                    {
                        ewhereList.Add("(companyid=" + odds[0] + " and (e_win=" + odds[10] +
                            ") and (e_draw=" + odds[11] + ") and (e_lost=" + odds[12] + "))");
                    }
                }
                string swhereStr = "(" + String.Join(" or ", swhereList.ToArray()) + ")";
                string ewhereStr = "(" + String.Join(" or ", ewhereList.ToArray()) + ")";

                switch (Request.Form["query"])
                {
                    case "2":
                        swhereStr += " and c.cclassid=" + sclassArr[9];
                        ewhereStr += " and c.cclassid=" + sclassArr[9];
                        break;
                    case "3":
                        swhereStr += " and a.sclassid=" + sclassArr[0];
                        ewhereStr += " and a.sclassid=" + sclassArr[0];
                        break;
                    default:
                        break;
                }
                //DataTable dt = scheduleBLL.queryCompanyHistory(1, swhereStr, 300).Tables[0];
                //if (ewhereList.Count > 0)
                //{
                //    DataTable dt1 = scheduleBLL.queryCompanyHistory(2, ewhereStr, 200).Tables[0];
                //    dt.Merge(dt1);
                //}
                DataTable dt = scheduleBLL.queryCompanyHistory(2, ewhereStr, 100).Tables[0];
                dt.Columns.Add("time", typeof(DateTime));
                dt.Columns.Add("w", typeof(decimal));
                dt.Columns.Add("d", typeof(decimal));
                dt.Columns.Add("l", typeof(decimal));
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (string oddsStr in oddsArr)
                    {
                        string[] odds = oddsStr.Split('|');
                        if (dr["companyid"].ToString() == odds[0])
                        {
                            string[] timeArr = odds[20].Split(',');
                            dr.SetField("time", new DateTime(int.Parse(timeArr[0]), int.Parse(timeArr[1].Remove(2)), int.Parse(timeArr[2]), int.Parse(timeArr[3]), int.Parse(timeArr[4]), int.Parse(timeArr[5])).AddHours(-8));
                            if (dr["type"].ToString() == "1")
                            {
                                dr.SetField("swin", Convert.ToDecimal(dr["swin"]) - Convert.ToDecimal(odds[6]));
                                dr.SetField("sdraw", Convert.ToDecimal(dr["sdraw"]) - Convert.ToDecimal(odds[7]));
                                dr.SetField("slost", Convert.ToDecimal(dr["slost"]) - Convert.ToDecimal(odds[8]));
                            }
                            else
                            {
                                decimal[] decimalArr = new decimal[] { 
                                    Convert.ToDecimal(dr["swin"]) - Convert.ToDecimal(odds[13]),
                                    Convert.ToDecimal(dr["sdraw"]) - Convert.ToDecimal(odds[14]),
                                    Convert.ToDecimal(dr["slost"]) - Convert.ToDecimal(odds[15])
                                };
                                dr.SetField("swin", decimalArr[0]);
                                dr.SetField("sdraw", decimalArr[1]);
                                dr.SetField("slost", decimalArr[2]);
                            }
                        }
                    }
                }
                JsonSerializer serializer = new JsonSerializer();
                serializer.Converters.Add(new JavaScriptDateTimeConverter());
                serializer.NullValueHandling = NullValueHandling.Ignore;
                Response.Write(JArray.FromObject(dt, serializer));
            }

        }

        private void statTodayHistory()
        {
            if (Request["stypeid"] != null && Request["oddsarr"] != null)
            {
                string[] sclassArr = Request.Form["stypeid"].Split('^');
                string[] oddsArr = Request.Form["oddsarr"].Split('^');
                string[] scheduleArr = Request.Form["schedulearr"].Split('^');
                string[] oddsInfo = Request.Form["odds"].Split(',');
                List<string> swhereList = new List<string>();
                List<string> ewhereList = new List<string>();
                foreach (string oddsStr in oddsArr)
                {
                    string[] odds = oddsStr.Split('|');
                    swhereList.Add("(companyid=" + odds[0] + " and s_win=" + odds[3] +
                            " and s_draw=" + odds[4] + " and s_lost=" + odds[5] + ")");
                    if (!String.IsNullOrEmpty(odds[10]) && !String.IsNullOrEmpty(odds[11]) && !String.IsNullOrEmpty(odds[12]))
                    {
                        ewhereList.Add("(companyid=" + odds[0] + " and (e_win=" + odds[10] +
                            ") and (e_draw=" + odds[11] + ") and (e_lost=" + odds[12] + "))");
                    }
                }
                string swhereStr = "(" + String.Join(" or ", swhereList.ToArray()) + ")";
                string ewhereStr = "(" + String.Join(" or ", ewhereList.ToArray()) + ")";

                JObject result = new JObject();
                try
                {
                    DataTable dt = scheduleBLL.queryCompanyHistory(2, ewhereStr, 200).Tables[0];
                    
                    foreach (DataRow dr in dt.Rows)
                    {
                        foreach (string oddsStr in oddsArr)
                        {
                            string[] odds = oddsStr.Split('|');
                            if (dr["companyid"].ToString() == odds[0])
                            {
                                if (dr["type"].ToString() == "2")
                                {
                                    dr.SetField("swin", Convert.ToDecimal(dr["swin"]) - Convert.ToDecimal(odds[13]));
                                    dr.SetField("sdraw", Convert.ToDecimal(dr["sdraw"]) - Convert.ToDecimal(odds[14]));
                                    dr.SetField("slost", Convert.ToDecimal(dr["slost"]) - Convert.ToDecimal(odds[15]));
                                }
                            }
                        }
                    }

                    result.Add("wmax", Convert.ToDouble(dt.Compute("max(swin)", "1=1")));
                    result.Add("dmax", Convert.ToDouble(dt.Compute("max(sdraw)", "1=1")));
                    result.Add("lmax", Convert.ToDouble(dt.Compute("max(slost)", "1=1")));
                }
                catch (Exception)
                {
                    result.Add("wmax", 0);
                    result.Add("dmax", 0);
                    result.Add("lmax", 0);
                }
                Response.Write(result.ToString());
            }

        }

        private int getNum(object r)
        {
            return r.ToString() == "" ? 0 : Convert.ToInt32(r);
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

        private int toInt(object o)
        {
            if (o == System.DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(o);
            }
        }

        private double toDouble(object o)
        {
            if (o == System.DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(o);
            }
        }
    }
}
