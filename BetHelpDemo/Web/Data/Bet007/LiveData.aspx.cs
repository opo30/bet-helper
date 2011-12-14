using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.BLL;

namespace SeoWebSite.Web.Data.Bet007
{
    public partial class LiveData : System.Web.UI.Page
    {
        protected string StringJSON = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            GetBetLiveData();
        }

        public void GetBetLiveData()
        {
            try
            {
                Bet007BLL bll = new Bet007BLL();
                StringJSON = bll.GetBetLiveData();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e);
                throw;
            }

        }
    }
}
