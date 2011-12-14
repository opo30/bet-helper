using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.BLL;

namespace SeoWebSite.Web.Url
{
    public partial class Add1x2History : System.Web.UI.Page
    {
        protected string StringJSON = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string companyids = Request.Form["companyid"];
            string historyids = Request.Form["historyid"];
            string scheduleID = Request.Form["scheduleID"];
            DateTime time = DateTime.Parse(Request.Form["time"]);
            AddHistoryOdds(scheduleID,companyids,historyids,time);
        }
        public void AddHistoryOdds(string scheduleID,string companyids,string historyids,DateTime time)
        {
            try
            {
                HistoryOddsBLL bll = new HistoryOddsBLL();
                StringJSON = bll.Add(scheduleID, companyids, historyids,time);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e);
                throw;
            }

        }
    }
}
