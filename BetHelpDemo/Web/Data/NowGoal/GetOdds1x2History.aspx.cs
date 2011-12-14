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
                        default:
                            break;
                    }
                }
            }
            
        }

        private void statOddsHistory()
        {
            if (Request["stypeid"] != null && Request["oddsarr"] != null)
            {
                string stypeid = Request.Form["stypeid"];
                string[] oddsArr = Request.Form["oddsarr"].Split('^');
                JArray data = new JArray();
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
                        JObject row = new JObject();
                        row.Add("companyid", Convert.ToInt32(odds[0]));
                        row.Add("companyname", odds[21].ToString());
                        row.Add("perwin", Convert.ToDecimal(ds.Tables[0].Rows[0]["perwin"]));
                        row.Add("perdraw", Convert.ToDecimal(ds.Tables[0].Rows[0]["perdraw"]));
                        row.Add("perlost", Convert.ToDecimal(ds.Tables[0].Rows[0]["perlost"]));
                        data.Add(row);
	                }
                }
                JObject result = JObject.Parse("{success:true}");
                result.Add("data", data);
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
