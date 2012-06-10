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

            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("fullname");
            dt.Columns.Add("isprimary", typeof(bool));
            dt.Columns.Add("isexchange", typeof(bool));
            dt.Columns.Add("result", typeof(int));
            dt.Columns.Add("query", typeof(int));
            dt.Columns.Add("time", typeof(int));
            dt.Merge(scheduleBLL.queryOddsHistory2(null, null, swhereStr, "query=1,time=1").Tables[0]);
            dt.Merge(scheduleBLL.queryOddsHistory2(null, null, ewhereStr, "query=1,time=2").Tables[0]);
            dt.Merge(scheduleBLL.queryOddsHistory2(sclassArr[9], null, swhereStr, "query=2,time=1").Tables[0]);
            dt.Merge(scheduleBLL.queryOddsHistory2(sclassArr[9], null, ewhereStr, "query=2,time=2").Tables[0]);
            dt.Merge(scheduleBLL.queryOddsHistory2(null, sclassArr[0], swhereStr, "query=3,time=1").Tables[0]);
            dt.Merge(scheduleBLL.queryOddsHistory2(null, sclassArr[0], ewhereStr, "query=3,time=2").Tables[0]);

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
            foreach (var r in new int[3] { 3, 1, 0 })
            {
                foreach (var q in new int[3] { 1, 2, 3 })
                {
                    int t1 = Convert.ToInt32(dt.Compute("count(id)", "query=" + q + " and time=1 and result=" + r));
                    int t2 = Convert.ToInt32(dt.Compute("count(id)", "query=" + q + " and time=2 and result=" + r));
                    myCol.Add("q" + q + "t1r" + r, t1.ToString());
                    myCol.Add("q" + q + "t2r" + r, t2.ToString());
                    if (t2 < t1)
                    {
                        myCol.Add("q" + q + "t2r" + r + "_bgcolor", "#DCFFB9");
                    }
                    else if (t2 > t1)
                    {
                        myCol.Add("q" + q + "t2r" + r + "_bgcolor", "#FFb0c8");
                    }
                    else
                    {
                        myCol.Add("q" + q + "t2r" + r + "_bgcolor", "#FFFFFF");
                    }
                }
            }
            foreach (var q in new int[3] { 1, 2, 3 })
            {
                foreach (var r in new int[3] { 3, 1, 0 })
                {
                    foreach (var t in new int[2] { 1, 2 })
                    {
                        string s = "";
                        for (int i = 0; i < Convert.ToInt32(myCol.Get("q" + q + "t" + t + "r" + r)); i++)
                        {

                            DataRow dr = dt.Select("query=" + q + " and time=" + t + " and result=" + r, "isprimary desc")[i];
                            bool isreproduce = t == 2 && Convert.ToInt32(dt.Compute("count(id)", "time=1 and id=" + dr["id"])) > 0;
                            string reproduce = isreproduce ? "<img alt='*' src='http://bet.yuuzle.com/Images/icons/star.png'/>" : "";
                            if (Convert.ToBoolean(dr["isprimary"]))
                            {
                                s += "<font color=blue>" + dr["name"] + "</font>";
                            }
                            else if (Convert.ToBoolean(dr["isexchange"]))
                            {
                                s += "<font color=green>" + dr["name"] + "</font>";
                            }
                            else
                            {
                                s += dr["name"];
                            }
                            s += reproduce + "<br>";
                        }
                        myCol.Add("q" + q + "t" + t + "r" + r + "_list", s);
                    }
                }
            }

            //bool support =
            //    int.Parse(myCol.Get("q1t1r3")) < int.Parse(myCol.Get("q1t2r3")) &&
            //    int.Parse(myCol.Get("q2t1r3")) < int.Parse(myCol.Get("q2t2r3")) &&
            //    int.Parse(myCol.Get("q3t1r3")) < int.Parse(myCol.Get("q3t2r3")) ||
            //    int.Parse(myCol.Get("q1t1r1")) < int.Parse(myCol.Get("q1t2r1")) &&
            //    int.Parse(myCol.Get("q2t1r1")) < int.Parse(myCol.Get("q2t2r1")) &&
            //    int.Parse(myCol.Get("q3t1r1")) < int.Parse(myCol.Get("q3t2r1")) ||
            //    int.Parse(myCol.Get("q1t1r0")) < int.Parse(myCol.Get("q1t2r0")) &&
            //    int.Parse(myCol.Get("q2t1r0")) < int.Parse(myCol.Get("q2t2r0")) &&
            //    int.Parse(myCol.Get("q3t1r0")) < int.Parse(myCol.Get("q3t2r0"));

            //bool support1 = false;
            //foreach (var dr in dt.Select("time=1 and isprimary=1").ToArray())
            //{
            //    if (Convert.ToInt32(dt.Compute("count(id)", "time=2 and id=" + dr["id"])) > 0)
            //    {
            //        support1 = true;
            //        break;
            //    }
            //}

            if (Convert.ToInt32(dt.Compute("count(id)", "result=3")) >= 3 || Convert.ToInt32(dt.Compute("count(id)", "result=1")) >= 3 || Convert.ToInt32(dt.Compute("count(id)", "result=0")) >= 3 || Convert.ToInt32(dt.Compute("count(id)", "isprimary=1")) > 0)
            {
                string title = String.Format(sclassArr[1] + " {4}-{7}", scheduleArr);
                string templetpath = Server.MapPath("~/Template/mail.htm");
                string mailBody = TemplateHelper.BulidByFile(templetpath, myCol);
                MailSender.Send("seo1214@gmail.com", title, mailBody);
            }
        }
    }
}