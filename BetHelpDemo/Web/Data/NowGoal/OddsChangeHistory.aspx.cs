using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.BLL;

namespace SeoWebSite.Web.Data.NowGoal
{
    public partial class OddsChangeHistory : System.Web.UI.Page
    {
        protected string StringJSON = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string scheduleID = Request.Form["scheduleID"];
            string companyID = Request.Form["companyID"];
            GetOddsDetail(scheduleID, companyID);
        }

        public void GetOddsDetail(string scheduleID, string companyID)
        {
            try
            {
                NowGoalBLL bll = new NowGoalBLL();
                StringJSON = bll.GetOddsDetail(scheduleID, companyID,"undefined");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e);
                throw;
            }

        }
    }
}
