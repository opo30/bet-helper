using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.BLL;
using SeoWebSite.Model;

namespace SeoWebSite.Web.Data.BetRecord
{
    public partial class TreeList : System.Web.UI.Page
    {
        protected string StringJSON = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            GetDTreeJSON();
        }

        public void GetDTreeJSON()
        {
            TreeBLL bll = new TreeBLL();
            string id = Request.Form["id"];
            if (id == null)
            {
                StringJSON = bll.GetBetRecordTree("1");
            }
            else if (id != null)
            {
                StringJSON = bll.GetBetRecordTree(id);
            }
            else
            {
                Response.Write("success:false");
            }

        }
    }
}
