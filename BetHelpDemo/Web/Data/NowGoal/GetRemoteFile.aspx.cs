using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Diagnostics;
using System.Text;
using System.Xml;
using SeoWebSite.BLL;

namespace SeoWebSite.Web.Data.NowGoal
{
    public partial class GetFile : System.Web.UI.Page
    {
        WebClient web = new WebClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Request.QueryString["f"])
            {
                case "rootjs":
                    ProcessRootJsFile();
                    break;
                case "infojs":
                    ProcessInfoJsFile();
                    break;
                case "oddsjs":
                    ProcessOddsjsJsFile();
                    break;
                case "xml":
                    ProcessXmlFile();
                    break;
                default:
                    break;
            }
        }

        private void ProcessOddsjsJsFile()
        {
            if (Request["path"] != null)
            {
                byte[] bytes = web.DownloadData(WebClientBLL.odds + Request.QueryString["path"] + "?" + DateTime.Now.Millisecond);
                string filename = Request.QueryString["path"].Substring(Request.QueryString["path"].LastIndexOf("/") + 1);

                DownloadFile(bytes, filename);
            }
        }

        private void ProcessRootJsFile()
        {
            if (Request["path"]!=null)
            {
                byte[] bytes = web.DownloadData(WebClientBLL.root + Request.QueryString["path"] + "?" + DateTime.Now.Millisecond);
                string filename = Request.QueryString["path"].Substring(Request.QueryString["path"].LastIndexOf("/") + 1);
                
                DownloadFile(bytes, filename);
            }
        }

        private void ProcessInfoJsFile()
        {
            if (Request["path"] != null)
            {
                byte[] bytes = web.DownloadData(WebClientBLL.info + Request.QueryString["path"] + "?" + DateTime.Now.Millisecond);
                string filename = Request.QueryString["path"].Substring(Request.QueryString["path"].LastIndexOf("/") + 1);
                DownloadFile(bytes, filename);
            }
        }

        private void ProcessXmlFile()
        {
            if (Request["path"] != null)
            {
                Response.ContentType = "text/xml";
                Response.Buffer = true; //完成整个响应后再发送
                Response.ContentEncoding = Encoding.UTF8;
                XmlDocument xml = new XmlDocument();
                xml.Load(WebClientBLL.root + Request.QueryString["path"] + "?" + DateTime.Now.Millisecond);

                Response.Write(xml.InnerXml);
                Response.End();
            }
        }

        private void DownloadFile(byte[] bytes,string filename)
        {
            try
            {
                string ContentType = "application/octet-stream";

                if (bytes != null)
                {
                    Response.Clear();
                    Response.AppendHeader("Content-Disposition", "attachment; FileName=" + filename);
                    Response.ContentType = ContentType;
                    Response.BinaryWrite(bytes);

                    Response.End();	//结束文件下载
                }
                else
                {
                    Response.Write("该附件不存在或已被删除，请联系管理员。");
                }
                Debug.WriteLine("执行附件下载");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

        }

    }
}
