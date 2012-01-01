using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.BLL;

namespace SeoWebSite.Web.Data.NowGoal
{
    public partial class GetOdds1x2ChangeList : System.Web.UI.Page
    {
        protected string StringJSON = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string scheduleID = Request.Form["scheduleid"];
            string[] companyids = Request.Form["companyids"].Split(',');
            string[] companynames = Request.Form["companynames"].Split(',');
            string tcompanyid = Request.Form["tcompanyid"];
            GetOdds1x2Change(scheduleID, companyids, companynames, tcompanyid);
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
