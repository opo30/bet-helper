using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Newtonsoft.Json.Linq;

namespace SeoWebSite.Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected string initCompanyJS = "";
        protected string live_url = SeoWebSite.BLL.WebClientBLL.root;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BLL.CompanyAsiaBLL companyBLL = new BLL.CompanyAsiaBLL();
                DataSet ds = companyBLL.GetList("isasia=1");
                JArray array = new JArray();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    initCompanyJS += "company[" + dr["id"].ToString() + "] = \""+dr["name"].ToString()+"\";";
                }
            }
        }
    }
}
