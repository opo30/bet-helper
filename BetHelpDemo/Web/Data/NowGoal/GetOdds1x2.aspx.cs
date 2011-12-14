using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.BLL;

namespace SeoWebSite.Web.Data.NowGoal
{
    public partial class Odds1x2 : System.Web.UI.Page
    {
        protected string StringJSON = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string scheduleID = Request.Form["scheduleID"];

            GetOdds1x2(scheduleID);
        }

        public void GetOdds1x2(string scheduleID)
        {
            try
            {
                NowGoalBLL bll = new NowGoalBLL();
                StringJSON = bll.GetOdds1x2(scheduleID);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e);
                throw;
            }

        }
    }
}
