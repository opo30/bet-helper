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
            if (Request["companyid"] != null)
            {
                string strWhere = Common.DataCache.GetCache("strWhere_" + Request.Form["companyid"]).ToString();
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

                        row.Add("e_win", dr["e_win"].ToString());
                        row.Add("e_draw", dr["e_draw"].ToString());
                        row.Add("e_lost", dr["e_lost"].ToString());
                        
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
                string stypeid = Request.Form["stypeid"];
                string[] oddsArr = Request.Form["oddsarr"].Split('^');
                DataTable dt = new DataTable();
                dt.Columns.Add("companyid");
                dt.Columns.Add("companyname");
                dt.Columns.Add("sumwin",typeof(int));
                dt.Columns.Add("sumdraw", typeof(int));
                dt.Columns.Add("sumlost", typeof(int));
                dt.Columns.Add("totalCount", typeof(int));
                foreach (string oddsStr in oddsArr)
                {
                    List<string> whereList = new List<string>();
                    string[] odds = oddsStr.Split('|');
                    whereList.Add("scheduletypeid=" + stypeid);
                    whereList.Add("companyid=" + odds[0]);
                    whereList.Add("s_win=" + odds[3]);
                    whereList.Add("s_draw=" + odds[4]);
                    whereList.Add("s_lost=" + odds[5]);
                    DataSet ds = scheduleBLL.statOddsHistory(String.Join(" and ",whereList.ToArray()));
                    if (ds.Tables[0].Rows.Count > 0 && Convert.ToInt32(ds.Tables[0].Rows[0]["totalCount"]) > 0)
	                {
                        Common.DataCache.SetCache("strWhere_" + odds[0], String.Join(" and ", whereList.ToArray()));
                        DataRow dataRow = dt.NewRow();
                        dataRow["companyid"] = Convert.ToInt32(odds[0]);
                        dataRow["companyname"] = odds[21].ToString();
                        dataRow["sumwin"] =  Convert.ToInt32(ds.Tables[0].Rows[0]["sumwin"]);
                        dataRow["sumdraw"] = Convert.ToInt32(ds.Tables[0].Rows[0]["sumdraw"]);
                        dataRow["sumlost"] = Convert.ToInt32(ds.Tables[0].Rows[0]["sumlost"]);
                        dataRow["totalCount"] = Convert.ToInt32(ds.Tables[0].Rows[0]["totalCount"]);
                        dt.Rows.Add(dataRow);
	                }
                }
                
                DataRow dr = dt.NewRow();
                dr["companyid"] = 0;
                dr["companyname"] = "合计";
                dr["sumwin"] =  dt.Compute("Sum(sumwin)/Sum(totalCount)*100", "1=1");
                dr["sumdraw"] = dt.Compute("Sum(sumdraw)/Sum(totalCount)*100", "1=1");
                dr["sumlost"] = dt.Compute("Sum(sumlost)/Sum(totalCount)*100", "1=1");
                dt.Rows.Add(dr);

                JObject result = JObject.Parse("{success:true}");
                result.Add("data", JArray.FromObject(dt));
                StringJSON = result.ToString();
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
