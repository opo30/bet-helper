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

namespace SeoWebSite.Web.Data.NowGoal
{
    public partial class GetOdds1x2ChangeList : System.Web.UI.Page
    {
        protected string StringJSON = "";
        ScheduleBLL scheduleBLL = new ScheduleBLL();

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
                        default:
                            break;
                    }
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
                    foreach (string oddsStr in oddsArr)
                    {
                        string[] odds = oddsStr.Split('|');
                        swhereList.Add("(companyid=" + odds[0] + " and s_win=" + odds[3] +
                            " and s_draw=" + odds[4] + " and s_lost=" + odds[5] + ")");
                        if (!String.IsNullOrEmpty(odds[10]) && !String.IsNullOrEmpty(odds[11]) && !String.IsNullOrEmpty(odds[12]))
                        {
                            ewhereList.Add("(companyid=" + odds[0] + " and e_win=" + odds[10] +
                                " and e_draw=" + odds[11] + " and e_lost=" + odds[12] + ")");
                            oddswhereList.Add("(companyid=" + odds[0] + " and s_win=" + odds[3] +
                            " and s_draw=" + odds[4] + " and s_lost=" + odds[5] + " and e_win=" + odds[10] +
                                " and e_draw=" + odds[11] + " and e_lost=" + odds[12] + ")");
                        }
                        //else
                        //{
                        //    ewhereList.Add("(companyid=" + odds[0] + " and s_win=" + odds[3] +
                        //    " and s_draw=" + odds[4] + " and s_lost=" + odds[5] + " and e_win is null and e_draw is null and e_lost is null)");
                        //}
                    }
                    string swhereStr = "(" + String.Join(" or ", swhereList.ToArray()) + ")";
                    string ewhereStr = "(" + String.Join(" or ", ewhereList.ToArray()) + ")";

                    Common.DataCache.SetCache("swhere", swhereStr);
                    Common.DataCache.SetCache("ewhere", ewhereStr);
                    Common.DataCache.SetCache("cclassid", sclassArr[9]);
                    Common.DataCache.SetCache("sclassid", sclassArr[0]);
                    Common.DataCache.SetCache("rangqiu", scheduleArr[25]);

                    DataTable dt = new DataTable();
                    dt.Columns.Add("name", typeof(string));
                    dt.Columns.Add("perwin", typeof(float));
                    dt.Columns.Add("perdraw", typeof(float));
                    dt.Columns.Add("perlost", typeof(float));
                    dt.Columns.Add("rqwin", typeof(float));
                    dt.Columns.Add("rqdraw", typeof(float));
                    dt.Columns.Add("rqlost", typeof(float));
                    dt.Columns.Add("avgscore", typeof(float));
                    dt.Columns.Add("totalCount", typeof(float));

                    DataSet sds = scheduleBLL.statOddsHistory(scheduleArr[25], null, null, swhereStr);
                    DataSet eds = scheduleBLL.statOddsHistory(scheduleArr[25], null, null, ewhereStr);
                    dt.ImportRow(sds.Tables[0].Rows[0]);
                    dt.Rows[0]["name"] = "全局初";
                    dt.ImportRow(eds.Tables[0].Rows[0]);
                    dt.Rows[1]["name"] = "全局终";
                    sds = scheduleBLL.statOddsHistory(scheduleArr[25], sclassArr[9], null, swhereStr);
                    eds = scheduleBLL.statOddsHistory(scheduleArr[25], sclassArr[9], null, ewhereStr);
                    dt.ImportRow(sds.Tables[0].Rows[0]);
                    dt.Rows[2]["name"] = "国家初";
                    dt.ImportRow(eds.Tables[0].Rows[0]);
                    dt.Rows[3]["name"] = "国家终";
                    sds = scheduleBLL.statOddsHistory(scheduleArr[25], null, sclassArr[0], swhereStr);
                    eds = scheduleBLL.statOddsHistory(scheduleArr[25], null, sclassArr[0], ewhereStr);
                    dt.ImportRow(sds.Tables[0].Rows[0]);
                    dt.Rows[4]["name"] = "赛事初";
                    dt.ImportRow(eds.Tables[0].Rows[0]);
                    dt.Rows[5]["name"] = "赛事终";

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
