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
                        case "stat1":
                            statOddsHistory1();
                            break;
                        case "statmail":
                            statOddsHistoryMail();
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

        private void statOddsHistoryMail()
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

                    DataSet sds = scheduleBLL.statOddsHistory2(null, null, swhereStr);
                    DataSet eds = scheduleBLL.statOddsHistory2(null, null, ewhereStr);
                    int[] c1 = new int[6] { getNum(sds.Tables[0].Rows[0][1]), getNum(sds.Tables[0].Rows[0][2]), getNum(sds.Tables[0].Rows[0][3]), getNum(eds.Tables[0].Rows[0][1]), getNum(eds.Tables[0].Rows[0][2]), getNum(eds.Tables[0].Rows[0][3]) };
                    sds = scheduleBLL.statOddsHistory2(sclassArr[9], null, swhereStr);
                    eds = scheduleBLL.statOddsHistory2(sclassArr[9], null, ewhereStr);
                    int[] c2 = new int[6] { getNum(sds.Tables[0].Rows[0][1]), getNum(sds.Tables[0].Rows[0][2]), getNum(sds.Tables[0].Rows[0][3]), getNum(eds.Tables[0].Rows[0][1]), getNum(eds.Tables[0].Rows[0][2]), getNum(eds.Tables[0].Rows[0][3]) };
                    sds = scheduleBLL.statOddsHistory2(null, sclassArr[0], swhereStr);
                    eds = scheduleBLL.statOddsHistory2(null, sclassArr[0], ewhereStr);
                    int[] c3 = new int[6] { getNum(sds.Tables[0].Rows[0][1]), getNum(sds.Tables[0].Rows[0][2]), getNum(sds.Tables[0].Rows[0][3]), getNum(eds.Tables[0].Rows[0][1]), getNum(eds.Tables[0].Rows[0][2]), getNum(eds.Tables[0].Rows[0][3]) };
                    int[] c4 = new int[6]{ c1[0] + c2[0] + c3[0],c1[1] + c2[1] + c3[1],c1[2] + c2[2] + c3[2],c1[3] + c2[3] + c3[3],c1[4] + c2[4] + c3[4],c1[5] + c2[5] + c3[5] };
                    if (c4.Sum() >= 3)
                    {
                        if (Math.Min(c4[0] + c4[3], c4[2] + c4[5]) == 0 || (c4[0] / c4[2] >= 10 || c4[0] / c4[2] <= 1 / 10))
                        {
                            string title = scheduleArr[4] + "-" + scheduleArr[7];
                            StringBuilder sb = new StringBuilder();
                            sb.Append("<table border=1 width=90%>");
                            sb.Append("<tr><td>赛事</td><td>时间</td><td>主队</td><td>客队</td><td>让球</td></tr>");
                            sb.Append(String.Format("<tr><td bgcolor=" + sclassArr[4] + ">" + sclassArr[1] + "</td><td>{10}</td><td>{4}</td><td>{7}</td><td>{25}</td></tr>", scheduleArr));
                            sb.Append("</table>");
                            sb.Append("<table border=1 width=90%>");
                            sb.Append("<tr><td>比赛</td><td>初盘</td><td>终盘</td></tr>");
                            sb.Append(String.Format("<tr><td>所有</td><td>{0} {1} {2}</td><td>{3} {4} {5}</td></tr>", new object[] { c1[0], c1[1], c1[2], c1[3], c1[4], c1[5] }));
                            sb.Append(String.Format("<tr><td>国家</td><td>{0} {1} {2}</td><td>{3} {4} {5}</td></tr>", new object[] { c2[0], c2[1], c2[2], c2[3], c2[4], c2[5] }));
                            sb.Append(String.Format("<tr><td>赛事</td><td>{0} {1} {2}</td><td>{3} {4} {5}</td></tr>", new object[] { c3[0], c3[1], c3[2], c3[3], c3[4], c3[5] }));
                            sb.Append(String.Format("<tr><td>合计</td><td>{0} {1} {2}</td><td>{3} {4} {5}</td></tr>", new object[] { c4[0], c4[1], c4[2], c4[3], c4[4], c4[5] }));
                            sb.Append("</table>");
                            MailSender mail = new MailSender();
                            mail.Send(title, sb.ToString());
                        }
                    }
                }
                catch (Exception e)
                {

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

                    DataSet sds = scheduleBLL.statOddsHistory2(null, null, swhereStr);
                    DataSet eds = scheduleBLL.statOddsHistory2(null, null, ewhereStr);

                    DataTable dt = new DataTable();
                    dt.Columns.Add("name", typeof(string));
                    dt.Columns.Add("swin", typeof(int));
                    dt.Columns.Add("sdraw", typeof(int));
                    dt.Columns.Add("slost", typeof(int));
                    dt.Columns.Add("ewin", typeof(int));
                    dt.Columns.Add("edraw", typeof(int));
                    dt.Columns.Add("elost", typeof(int));
                    dt.Columns.Add("avgscore", typeof(float));
                    dt.Columns.Add("totalCount", typeof(int));

                    if (Convert.ToInt32(eds.Tables[0].Rows[0]["totalCount"]) > 0 &&
                        Convert.ToInt32(sds.Tables[0].Rows[0]["totalCount"]) > 0)
                    {
                        Common.DataCache.SetCache("swhere", swhereStr);
                        Common.DataCache.SetCache("ewhere", ewhereStr);
                        Common.DataCache.SetCache("cclassid", sclassArr[9]);
                        Common.DataCache.SetCache("sclassid", sclassArr[0]);

                        DataRow dr = dt.NewRow();
                        dr["name"] = "全局";
                        dr["swin"] = getNum(sds.Tables[0].Rows[0][1]);
                        dr["sdraw"] = getNum(sds.Tables[0].Rows[0][2]);
                        dr["slost"] = getNum(sds.Tables[0].Rows[0][3]);
                        dr["ewin"] = getNum(eds.Tables[0].Rows[0][1]);
                        dr["edraw"] = getNum(eds.Tables[0].Rows[0][2]);
                        dr["elost"] = getNum(eds.Tables[0].Rows[0][3]);
                        dr["totalCount"] = getNum(eds.Tables[0].Rows[0][0]);
                        dt.Rows.Add(dr);
                        sds = scheduleBLL.statOddsHistory2(sclassArr[9], null, swhereStr);
                        eds = scheduleBLL.statOddsHistory2(sclassArr[9], null, ewhereStr);
                        dr = dt.NewRow();
                        dr["name"] = "国家";
                        dr["swin"] = getNum(sds.Tables[0].Rows[0][1]);
                        dr["sdraw"] = getNum(sds.Tables[0].Rows[0][2]);
                        dr["slost"] = getNum(sds.Tables[0].Rows[0][3]);
                        dr["ewin"] = getNum(eds.Tables[0].Rows[0][1]);
                        dr["edraw"] = getNum(eds.Tables[0].Rows[0][2]);
                        dr["elost"] = getNum(eds.Tables[0].Rows[0][3]);
                        dr["totalCount"] = getNum(eds.Tables[0].Rows[0][0]);
                        dt.Rows.Add(dr);
                        sds = scheduleBLL.statOddsHistory2(null, sclassArr[0], swhereStr);
                        eds = scheduleBLL.statOddsHistory2(null, sclassArr[0], ewhereStr);
                        dr = dt.NewRow();
                        dr["name"] = "赛事";
                        dr["swin"] = getNum(sds.Tables[0].Rows[0][1]);
                        dr["sdraw"] = getNum(sds.Tables[0].Rows[0][2]);
                        dr["slost"] = getNum(sds.Tables[0].Rows[0][3]);
                        dr["ewin"] = getNum(eds.Tables[0].Rows[0][1]);
                        dr["edraw"] = getNum(eds.Tables[0].Rows[0][2]);
                        dr["elost"] = getNum(eds.Tables[0].Rows[0][3]);
                        dr["totalCount"] = getNum(eds.Tables[0].Rows[0][0]);
                        dt.Rows.Add(dr);
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
    }
}
