using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.BLL;

namespace SeoWebSite.Web.Data.NowGoal
{
    public partial class LiveData : System.Web.UI.Page
    {
        protected string StringJSON = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["companyid"] != null)
                {
                    GetBetLiveData(Request.Form["now"], Convert.ToInt32(Request.Form["companyid"]));
                }
                else
                {
                    GetBetLiveData(Request.Form["now"],0);
                }
            }
        }

        public void GetBetLiveData(string date,int companyid)
        {
            try
            {
                NowGoalBLL bll = new NowGoalBLL();
                StringJSON = bll.GetBetLiveData(date, companyid);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e);
                throw;
            }

        }
    }
}
