using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.BLL;
using Newtonsoft.Json.Linq;

namespace SeoWebSite.Web.Data.NowGoal
{
    public partial class GetOddsDetail : System.Web.UI.Page
    {
        protected string StringJSON = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["scheduleID"] != null && Request["companyID"] != null && Request["date"] != null)
                {
                    GetOddsDetailInfo();
                }
            }
            
        }

        public void GetOddsDetailInfo()
        {
            try
            {
                NowGoalBLL bll = new NowGoalBLL();
                string scheduleID = Request.Form["scheduleID"];
                string companyID = Request.Form["companyID"];
                string dateStr = Request.Form["date"];
                StringJSON = bll.GetOddsDetail(scheduleID, companyID, dateStr);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e);
                throw;
            }

        }
    }
}
