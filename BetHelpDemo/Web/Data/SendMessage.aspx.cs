using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SeoWebSite.BLL;
using SeoWebSite.Common;
using System.Collections.Specialized;
using System.Text;

public partial class Data_SendMessage : System.Web.UI.Page
{
    ScheduleBLL scheduleBLL = new ScheduleBLL();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["a"] != null)
            {
                switch (Request.QueryString["a"])
                {
                    case "mail":
                        SendMessageByMail();
                        break;
                    case "sms":
                        SendMessageBySMS();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void SendMessageBySMS()
    {
        FetionProxy fetion = new FetionProxy("13871459996", "thinkpadt60");
        StringBuilder sb = new StringBuilder();
        bool result = fetion.SendToSelf("asd");
        fetion.logout();
    }

    private void SendMessageByMail()
    {
        if (Request["stypeid"] != null && Request["oddsarr"] != null)
        {
            string[] sclassArr = Request.Form["stypeid"].Split('^');
            string[] oddsArr = Request.Form["oddsarr"].Split('^');
            string[] scheduleArr = Request.Form["schedulearr"].Split('^');
            string[] oddsInfo = Request.Form["odds"].Split(',');
            List<string> swhereList = new List<string>();
            List<string> ewhereList = new List<string>();
            foreach (string oddsStr in oddsArr)
            {
                string[] odds = oddsStr.Split('|');
                swhereList.Add("(companyid=" + odds[0] + " and s_win=" + odds[3] +
                        " and s_draw=" + odds[4] + " and s_lost=" + odds[5] + ")");
                if (!String.IsNullOrEmpty(odds[10]) && !String.IsNullOrEmpty(odds[11]) && !String.IsNullOrEmpty(odds[12]))
                {
                    ewhereList.Add("(companyid=" + odds[0] + " and (e_win=" + odds[10] +
                        ") and (e_draw=" + odds[11] + ") and (e_lost=" + odds[12] + "))");
                }
            }
            string swhereStr = "(" + String.Join(" or ", swhereList.ToArray()) + ")";
            string ewhereStr = "(" + String.Join(" or ", ewhereList.ToArray()) + ")";

            DataTable dt = scheduleBLL.queryCompanyHistory(swhereStr, ewhereStr).Tables[0];

            dt.Columns.Add("so", typeof(float));

            foreach (DataRow dr in dt.Rows)
            {
                foreach (string oddsStr in oddsArr)
                {
                    string[] odds = oddsStr.Split('|');
                    if (dr["companyid"].ToString() == odds[0])
                    {
                        dr.SetField("swin", Convert.ToDecimal(dr["swin"]) - Convert.ToDecimal(odds[6]));
                        dr.SetField("sdraw", Convert.ToDecimal(dr["sdraw"]) - Convert.ToDecimal(odds[7]));
                        dr.SetField("slost", Convert.ToDecimal(dr["slost"]) - Convert.ToDecimal(odds[8]));
                        dr.SetField("ewin", Convert.ToDecimal(dr["ewin"]) - Convert.ToDecimal(odds[13]));
                        dr.SetField("edraw", Convert.ToDecimal(dr["edraw"]) - Convert.ToDecimal(odds[14]));
                        dr.SetField("elost", Convert.ToDecimal(dr["elost"]) - Convert.ToDecimal(odds[15]));

                        List<decimal> decimalList = new List<decimal>();
                        decimalList.Add(Math.Abs(Convert.ToDecimal(dr["swin"])));
                        decimalList.Add(Math.Abs(Convert.ToDecimal(dr["sdraw"])));
                        decimalList.Add(Math.Abs(Convert.ToDecimal(dr["slost"])));
                        dr.SetField("so", decimalList.Max());
                    }
                }
            }

            bool ismail = false;
            string limit = "swin<1 and sdraw<1 and slost<1 and swin>-1 and sdraw>-1 and slost>-1 and ecount>=50";
            int count = toInt(dt.Compute("count(companyid)", limit));
            if (count >= 2)
            {
                if (Math.Abs(Convert.ToDouble(oddsInfo[2])) < 1)
                {
                    if (Convert.ToDouble(oddsInfo[2]) > 0)
                    {
                        ismail = toInt(dt.Compute("count(companyid)", limit + " and ewin>swin and ewin>0")) == 0 ||
                            toInt(dt.Compute("count(companyid)", limit + " and (edraw>sdraw and edraw>0 or elost>slost and elost>0)")) == 0;
                    }
                    else if (Convert.ToDouble(oddsInfo[2]) < 0)
                    {
                        ismail = toInt(dt.Compute("count(companyid)", limit + " and elost>slost and elost>0")) == 0 ||
                            toInt(dt.Compute("count(companyid)", limit + " and (edraw>sdraw and edraw>0 or ewin>swin and ewin>0)")) == 0;
                    }
                    else
                    {
                        ismail = toInt(dt.Compute("count(companyid)", limit + " and ewin>swin and ewin>0")) == 0 ||
                            toInt(dt.Compute("count(companyid)", limit + " and elost>slost and elost>0")) == 0;
                    }
                }
                else if (Convert.ToDouble(oddsInfo[2]) >= 1)
                {
                    ismail = toInt(dt.Compute("count(companyid)", limit + " and ewin>swin and ewin>0")) == 0;
                }
                else if (Convert.ToDouble(oddsInfo[2]) <= -1)
                {
                    ismail = toInt(dt.Compute("count(companyid)", limit + " and elost>slost and elost>0")) == 0;
                }
            }

            if (ismail)
            {
                NameValueCollection myCol = new NameValueCollection();
                for (int i = 0; i < scheduleArr.Length; i++)
                {
                    myCol.Add("scheduleArr" + i, scheduleArr[i]);
                }
                for (int i = 0; i < sclassArr.Length; i++)
                {
                    myCol.Add("sclassArr" + i, sclassArr[i]);
                }
                for (int i = 0; i < oddsInfo.Length; i++)
                {
                    myCol.Add("oddsArr" + i, oddsInfo[i]);
                }

                StringBuilder sb = new StringBuilder();
                foreach (DataRow dr in dt.Select("swin<3 and sdraw<3 and slost<3", "so"))
                {
                    sb.Append("<tr>");
                    string color = "black";
                    if (Convert.ToBoolean(dr["isprimary"]))
                    {
                        color = "blue";
                    }
                    else if (Convert.ToBoolean(dr["isexchange"]))
                    {
                        color = "green";
                    }
                    sb.Append("<td align=\"center\" bgcolor=\"White\" style=\"overflow:hidden;text-overflow:ellipsis;white-space:nowrap;line-height: 21px;font-size: 10px;color:" + color + ";\">" + dr["fullname"] + "</td>");
                    sb.Append("<td align=\"center\" bgcolor=\"White\" style=\"line-height: 21px; font-size: 10px;\">" + dr["scount"] + "</td>");
                    sb.Append("<td align=\"center\" bgcolor=\"" + getBGColor(0, dr["swin"]) + "\" style=\"line-height: 21px; font-size: 10px;\">" + dRound(dr["swin"]) + "</td>");
                    sb.Append("<td align=\"center\" bgcolor=\"" + getBGColor(0, dr["sdraw"]) + "\" style=\"line-height: 21px; font-size: 10px;\">" + dRound(dr["sdraw"]) + "</td>");
                    sb.Append("<td align=\"center\" bgcolor=\"" + getBGColor(0, dr["slost"]) + "\" style=\"line-height: 21px; font-size: 10px;\">" + dRound(dr["slost"]) + "</td>");
                    sb.Append("<td align=\"center\" bgcolor=\"White\" style=\"line-height: 21px; font-size: 10px;\">" + dr["ecount"] + "</td>");
                    sb.Append("<td align=\"center\" bgcolor=\"" + getBGColor(dr["swin"], dr["ewin"]) + "\" style=\"line-height: 21px; font-size: 10px;\">" + dRound(dr["ewin"]) + "</td>");
                    sb.Append("<td align=\"center\" bgcolor=\"" + getBGColor(dr["sdraw"], dr["edraw"]) + "\" style=\"line-height: 21px; font-size: 10px;\">" + dRound(dr["edraw"]) + "</td>");
                    sb.Append("<td align=\"center\" bgcolor=\"" + getBGColor(dr["slost"], dr["elost"]) + "\" style=\"line-height: 21px; font-size: 10px;\">" + dRound(dr["elost"]) + "</td>");
                    sb.Append("</tr>");
                }
                myCol.Add("companyHistory", sb.ToString());

                string title = String.Format(sclassArr[1] + " {4}-{7}", scheduleArr);
                string templetpath = Server.MapPath("~/Template/mail.htm");
                string mailBody = TemplateHelper.BulidByFile(templetpath, myCol);
                MailSender.Send("seo1214@gmail.com", title, mailBody);
            }
        }
    }

    private string getBGColor(object o1, object o2)
    {
        string color = "White";
        if (Convert.ToDecimal(o2) > Convert.ToDecimal(o1) && Convert.ToDecimal(o2) > 0)
        {
            return "#F7CFD6";
        }
        else if (Convert.ToDecimal(o2) < Convert.ToDecimal(o1) && Convert.ToDecimal(o2) < 0)
        {
            return "#DFF3B1";
        }
        return color;
    }

    private decimal dRound(object o)
    {
        return decimal.Round(Convert.ToDecimal(o), 2);
    }

    private int toInt(object o)
    {
        if (o == System.DBNull.Value)
        {
            return 0;
        }
        else
        {
            return Convert.ToInt32(o);
        }
    }
}