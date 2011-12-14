using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.BLL;

namespace SeoWebSite.Web.Data.BetRecord
{
    public partial class List : System.Web.UI.Page
    {
        protected string StringJSON = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            GetBetRecordList();
        }
        public void GetBetRecordList()
        {
            try
            {
                BetRecordBLL bll = new BetRecordBLL();
                StringJSON = bll.GetBetRecordList("01");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e);
                throw;
            }

        }
    }
}
