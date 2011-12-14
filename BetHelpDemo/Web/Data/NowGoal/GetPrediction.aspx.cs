using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.BLL;

namespace SeoWebSite.Web.Data.NowGoal
{
    public partial class Prediction : System.Web.UI.Page
    {
        protected string StringJSON = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string scheduleID = Request.Form["scheduleID"];
            string goalNum = Request.Form["goalNum"];
            GetPrediction(scheduleID, goalNum);
        }

        public void GetPrediction(string scheduleID, string goalNum)
        {
            try
            {
                NowGoalBLL bll = new NowGoalBLL();
                if (goalNum == null)
                {
                    StringJSON = bll.GetPrediction(scheduleID);
                }
                else
                {
                    StringJSON = bll.GetPrediction(scheduleID, float.Parse(goalNum));
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e);
                throw;
            }

        }
    }
}
