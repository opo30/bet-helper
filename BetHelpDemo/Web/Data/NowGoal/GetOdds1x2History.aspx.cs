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
                string cacheName = Request.Form["rowindex"] == "0" ? "swhere" : "ewhere"; ;
                DataSet ds = scheduleBLL.statOddsHistoryGroupByDate(Common.DataCache.GetCache(cacheName).ToString());
                JObject result = JObject.Parse("{success:true}");
                JsonSerializer serializer = new JsonSerializer();
                serializer.Converters.Add(new JavaScriptDateTimeConverter());
                serializer.NullValueHandling = NullValueHandling.Ignore;
                result.Add("data", JArray.FromObject(ds.Tables[0], serializer));
                JsonConvert.SerializeObject(ds.Tables[0], new JavaScriptDateTimeConverter());
                StringJSON = result.ToString();
            }
        }

        private void queryOddsHistory()
        {
            if (Request["rowindex"] != null)
            {
                string cacheName = Request.Form["rowindex"] == "0" ? "swhere" : "ewhere";
                string whereStr = Common.DataCache.GetCache(cacheName).ToString();
                if (Request["date"] != null && !String.IsNullOrEmpty(Request.Form["date"]))
                {
                    whereStr += " and a.date='" + Request.Form["date"] + "'";
                }
                DataSet ds = scheduleBLL.queryOddsHistory(whereStr);
                JArray data = new JArray();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string[] scheduleArr = dr["data"].ToString().Split(',');
                        JObject row = new JObject();
                        row.Add("h_teamname", scheduleArr[4]);
                        row.Add("g_teamname", scheduleArr[7]);
                        row.Add("score", scheduleArr[13] + "-" + scheduleArr[14]);
                        row.Add("rangqiu", scheduleArr[25]);
                        row.Add("s_time", scheduleArr[10].Replace("<br>", ""));
                        row.Add("s_win", dr["s_win"].ToString());
                        row.Add("s_draw", dr["s_draw"].ToString());
                        row.Add("s_lost", dr["s_lost"].ToString());
                        row.Add("e_win", dr["e_win"].ToString());
                        row.Add("e_draw", dr["e_draw"].ToString());
                        row.Add("e_lost", dr["e_lost"].ToString());
                        row.Add("companyid", Convert.ToInt32(dr["companyid"]));
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
                    string stypeid = Request.Form["stypeid"];
                    string[] oddsArr = Request.Form["oddsarr"].Split('^');
                    string[] scheduleArr = Request.Form["schedulearr"].Split('^');
                    List<string> swhereList = new List<string>();
                    List<string> ewhereList = new List<string>();
                    List<string> oddswhereList = new List<string>();
                    List<decimal> soddsperwin = new List<decimal>();
                    List<decimal> soddsperdraw = new List<decimal>();
                    List<decimal> soddsperlost = new List<decimal>();
                    List<decimal> eoddsperwin = new List<decimal>();
                    List<decimal> eoddsperdraw = new List<decimal>();
                    List<decimal> eoddsperlost = new List<decimal>();
                    foreach (string oddsStr in oddsArr)
                    {
                        string[] odds = oddsStr.Split('|');
                        swhereList.Add("(companyid=" + odds[0] + " and s_win=" + odds[3] +
                            " and s_draw=" + odds[4] + " and s_lost=" + odds[5] + ")");
                        if (!String.IsNullOrEmpty(odds[10]) && !String.IsNullOrEmpty(odds[11]) && !String.IsNullOrEmpty(odds[12]))
                        {
                            ewhereList.Add("(companyid=" + odds[0] + "and e_win=" + odds[10] + 
                                " and e_draw=" + odds[11] + " and e_lost=" + odds[12]+")");
                            oddswhereList.Add("(companyid=" + odds[0] + " and s_win=" + odds[3] +
                            " and s_draw=" + odds[4] + " and s_lost=" + odds[5] + " and e_win=" + odds[10] +
                                " and e_draw=" + odds[11] + " and e_lost=" + odds[12] + ")");
                        }
                        //else
                        //{
                        //    ewhereList.Add("(companyid=" + odds[0] + " and s_win=" + odds[3] +
                        //    " and s_draw=" + odds[4] + " and s_lost=" + odds[5] + " and e_win is null and e_draw is null and e_lost is null)");
                        //}
                        soddsperwin.Add(Convert.ToDecimal(odds[6]));
                        soddsperdraw.Add(Convert.ToDecimal(odds[7]));
                        soddsperlost.Add(Convert.ToDecimal(odds[8]));
                        if (!String.IsNullOrEmpty(odds[13]))
                        {
                            eoddsperwin.Add(Convert.ToDecimal(odds[13])); 
                        }
                        if (!String.IsNullOrEmpty(odds[14]))
                        {
                            eoddsperdraw.Add(Convert.ToDecimal(odds[14]));
                        }
                        if (!String.IsNullOrEmpty(odds[15]))
                        {
                            eoddsperlost.Add(Convert.ToDecimal(odds[15]));
                        }
                    }
                    //string scheduleFilter = "sclassid=" + stypeid + " and c.id<>" + scheduleArr[0];
                    string scheduleFilter = "c.id<>" + scheduleArr[0];
                    DataSet sds = scheduleBLL.statOddsHistory(String.Join(" or ", swhereList.ToArray()));
                    DataSet eds = scheduleBLL.statOddsHistory(String.Join(" or ", ewhereList.ToArray()));
                    Common.DataCache.SetCache("swhere", String.Join(" or ", swhereList.ToArray()));
                    Common.DataCache.SetCache("ewhere", String.Join(" or ", ewhereList.ToArray()));
                    //DataSet oddsds = scheduleBLL.statOddsHistory(String.Join(" or ", oddswhereList.ToArray()),"");
                    DataTable dt = new DataTable();
                    dt.Columns.Add("name", typeof(string));
                    dt.Columns.Add("perwin", typeof(float));
                    dt.Columns.Add("perdraw", typeof(float));
                    dt.Columns.Add("perlost", typeof(float));
                    dt.Columns.Add("avgscore", typeof(float));
                    dt.Columns.Add("totalCount", typeof(float));
                    DataRow dr = dt.NewRow();
                    dr["name"] = "初赔";
                    dr["perwin"] = soddsperwin.Average();
                    dr["perdraw"] = soddsperdraw.Average();
                    dr["perlost"] = soddsperlost.Average();
                    dt.Rows.Add(dr);
                    dt.ImportRow(sds.Tables[0].Rows[0]);
                    dt.Rows[1]["name"] = "初盘";
                    dr = dt.NewRow();
                    dr["name"] = "终赔";
                    dr["perwin"] = eoddsperwin.Average();
                    dr["perdraw"] = eoddsperdraw.Average();
                    dr["perlost"] = eoddsperlost.Average();
                    dt.Rows.Add(dr);
                    dt.ImportRow(eds.Tables[0].Rows[0]);
                    //dt.ImportRow(oddsds.Tables[0].Rows[0]);
                    
                    dt.Rows[3]["name"] = "终盘";
                    //dt.Rows[4]["name"] = "全局";

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
