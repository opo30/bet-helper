using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.BLL;

public partial class Data_NowGoal_LoadLiveFile : System.Web.UI.Page
{
    protected string StringJSON = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.Form["now"]))
            {
                GetLiveFile();
            }
            else
            {
                GetHistoryLiveFile();
            }
        }
       
    }

    private void GetHistoryLiveFile()
    {
        try
        {
            WebClientBLL bll = new WebClientBLL();
            StringJSON = bll.LoadHistoryFile(Request.Form["now"]);
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.Write(e);
            throw;
        }
    }

    private void GetLiveFile()
    {
        try
        {
            WebClientBLL bll = new WebClientBLL();
            StringJSON = bll.LoadLiveFile();
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.Write(e);
            throw;
        }
    }
}
