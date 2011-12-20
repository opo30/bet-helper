using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.BLL;
using System.Data;
using Newtonsoft.Json.Linq;

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
                        case "list":
                            queryOddsHistory();
                            break;
                        default:
                            break;
                    }
                }
            }

        }

        private void queryOddsHistory()
        {
            if (Request["rowindex"] != null)
            {
                string cacheName = Request.Form["rowindex"] == "0" ? "swhere" : "ewhere"; ;
                DataSet ds = scheduleBLL.queryOddsHistory(Common.DataCache.GetCache(cacheName).ToString());
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
                    List<string> swhereList = new List<string>();
                    List<string> ewhereList = new List<string>();
                    List<decimal> soddsperwin = new List<decimal>();
                    List<decimal> soddsperdraw = new List<decimal>();
                    List<decimal> soddsperlost = new List<decimal>();
                    List<decimal> eoddsperwin = new List<decimal>();
                    List<decimal> eoddsperdraw = new List<decimal>();
                    List<decimal> eoddsperlost = new List<decimal>();
                    string sclassFilter = "sclassid=" + stypeid;
                    foreach (string oddsStr in oddsArr)
                    {
                        string[] odds = oddsStr.Split('|');
                        swhereList.Add("(companyid=" + odds[0] + " and s_win=" + odds[3] +
                            " and s_draw=" + odds[4] + " and s_lost=" + odds[5] + ")");
                        if (!String.IsNullOrEmpty(odds[10]) && !String.IsNullOrEmpty(odds[11]) && !String.IsNullOrEmpty(odds[12]))
                        {
                            ewhereList.Add("(companyid=" + odds[0] + "and e_win=" + odds[10] + 
                                " and e_draw=" + odds[11] + " and e_lost=" + odds[12]+")");
                        }
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
                    string sWhere = sclassFilter + " and (" + String.Join(" or ", swhereList.ToArray()) + ")";
                    DataSet sds = scheduleBLL.statOddsHistory(sWhere);
                    string eWhere = sclassFilter + " and (" + String.Join(" or ", ewhereList.ToArray()) + ")";
                    Common.DataCache.SetCache("swhere", sWhere);
                    Common.DataCache.SetCache("ewhere", eWhere);
                    DataSet eds = scheduleBLL.statOddsHistory(eWhere);
                    DataTable dt = sds.Tables[0];
                    dt.ImportRow(eds.Tables[0].Rows[0]);
                    dt.Columns.Add("name", typeof(string));
                    dt.Columns.Add("oddsperwin", typeof(float));
                    dt.Columns.Add("oddsperdraw", typeof(float));
                    dt.Columns.Add("oddsperlost", typeof(float));
                    dt.Columns.Add("kellywin", typeof(float));
                    dt.Columns.Add("kellydraw", typeof(float));
                    dt.Columns.Add("kellylost", typeof(float));
                    dt.Rows[0]["name"] = "初盘";
                    dt.Rows[1]["name"] = "终盘";
                    dt.Rows[0]["oddsperwin"] = soddsperwin.Average();
                    dt.Rows[1]["oddsperwin"] = eoddsperwin.Average();
                    dt.Rows[0]["oddsperdraw"] = soddsperdraw.Average();
                    dt.Rows[1]["oddsperdraw"] = eoddsperdraw.Average();
                    dt.Rows[0]["oddsperlost"] = soddsperlost.Average();
                    dt.Rows[1]["oddsperlost"] = eoddsperlost.Average();
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
