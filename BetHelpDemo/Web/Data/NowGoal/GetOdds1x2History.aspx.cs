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
            if (Request["companyid"] != null && Request["type"] != null)
            {
                string strWhere = Common.DataCache.GetCache(Request.Form["type"] + "_" + Request.Form["companyid"]).ToString();
                DataSet ds = scheduleBLL.queryOddsHistory(strWhere);
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
                        if (Request.Form["type"] == "1")
                        {
                            row.Add("e_win", dr["e_win"].ToString());
                            row.Add("e_draw", dr["e_draw"].ToString());
                            row.Add("e_lost", dr["e_lost"].ToString());
                        }
                        else
                        {
                            row.Add("s_win", dr["s_win"].ToString());
                            row.Add("s_draw", dr["s_draw"].ToString());
                            row.Add("s_lost", dr["s_lost"].ToString());
                        }

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
            if (Request["stypeid"] != null && Request["oddsarr"] != null && Request["type"] != null)
            {
                try
                {
                    string stypeid = Request.Form["stypeid"];
                    string[] oddsArr = Request.Form["oddsarr"].Split('^');
                    List<string> swhereList = new List<string>();
                    List<string> ewhereList = new List<string>();
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
                    }
                    DataSet sds = scheduleBLL.statOddsHistory(sclassFilter + " and (" + String.Join(" or ", swhereList.ToArray()) + ")");
                    DataSet eds = scheduleBLL.statOddsHistory(sclassFilter + " and (" + String.Join(" or ", ewhereList.ToArray()) + ")");
                    DataTable dt = sds.Tables[0];
                    dt.ImportRow(eds.Tables[0].Rows[0]);
                    dt.Columns.Add("name", typeof(string));
                    dt.Rows[0]["name"] = "初盘(" + dt.Rows[0]["totalCount"] + ")";
                    dt.Rows[1]["name"] = "终盘(" + dt.Rows[1]["totalCount"] + ")";
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
