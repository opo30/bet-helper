using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.DBUtility;
using System.Data;
using SeoWebSite.BLL;
using SeoWebSite.DAL;

public partial class Default7 : System.Web.UI.Page
{
    ScheduleDAO dao = new ScheduleDAO();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet ds = DbHelperSQL.Query("select * from scheduleClass", 999);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string[] sclass = dr["data"].ToString().Split(',');
                DbHelperSQL.ExecuteSql("update scheduleclass set cclassid=" + sclass[9] + " where id=" + dr["id"]);
            }
            //DataSet ds = DbHelperSQL.Query("select a.id from Schedule a where a.updated=1 and a.id not in (select b.scheduleid from ScheduleRecord b) and year(a.date)=2011",999);

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //        foreach (DataRow dr in ds.Tables[0].Rows)
            //        {
            //            DataSet avgPer = DbHelperSQL.Query("select AVG(s_winper),AVG(s_drawper),AVG(s_lostper),AVG(e_winper),AVG(e_drawper),AVG(e_lostper) from odds where scheduleid =" + dr["id"], 999);
            //            DataRow avgdr = avgPer.Tables[0].Rows[0];
            //            DataSet dsOdds = DbHelperSQL.Query("select companyid,s_win,s_draw,s_lost,e_win,e_draw,e_lost from odds where scheduleid =" + dr["id"], 999);
            //            List<string> swhereList = new List<string>();
            //            List<string> ewhereList = new List<string>();
            //            foreach (DataRow oddsdr in dsOdds.Tables[0].Rows)
            //            {
            //                swhereList.Add("(companyid=" + oddsdr[0] + " and s_win=" + oddsdr[1] +
            //                " and s_draw=" + oddsdr[2] + " and s_lost=" + oddsdr[3] + ")");
            //                if (!String.IsNullOrEmpty(oddsdr[4].ToString()) && !String.IsNullOrEmpty(oddsdr[5].ToString()) && !String.IsNullOrEmpty(oddsdr[6].ToString()))
            //                {
            //                    ewhereList.Add("(companyid=" + oddsdr[0] + " and e_win=" + oddsdr[4] +
            //                        " and e_draw=" + oddsdr[5] + " and e_lost=" + oddsdr[6] + ")");
            //                }
            //            }
            //            if (ewhereList.Count > 0)
            //            {
            //                DataSet sds = dao.statOddsHistory("(" + String.Join(" or ", swhereList.ToArray()) + ")");
            //                DataRow sdr = sds.Tables[0].Rows[0];
            //                DataSet eds = dao.statOddsHistory("(" + String.Join(" or ", ewhereList.ToArray()) + ")");
            //                DataRow edr = eds.Tables[0].Rows[0];
            //                if (Convert.ToInt32(sdr[3]) > 0 && Convert.ToInt32(edr[3]) > 0)
            //                {
            //                    string result = "";
            //                    List<decimal> list = new List<decimal>();
            //                    list.Add(Convert.ToDecimal(avgdr[0]));
            //                    list.Add(Convert.ToDecimal(avgdr[1]));
            //                    list.Add(Convert.ToDecimal(avgdr[2]));
            //                    list.Add(Convert.ToDecimal(avgdr[3]));
            //                    list.Add(Convert.ToDecimal(avgdr[4]));
            //                    list.Add(Convert.ToDecimal(avgdr[5]));
            //                    list.Add(Convert.ToDecimal(sdr[0]));
            //                    list.Add(Convert.ToDecimal(sdr[1]));
            //                    list.Add(Convert.ToDecimal(sdr[2]));
            //                    list.Add(Convert.ToDecimal(edr[0]));
            //                    list.Add(Convert.ToDecimal(edr[1]));
            //                    list.Add(Convert.ToDecimal(edr[2]));
            //                    decimal[] arrnum = list.ToArray();
            //                    list.Sort();
            //                    foreach (decimal item in arrnum)
            //                    {
            //                        result += list.IndexOf(item) + ",";
            //                    }
            //                    DbHelperSQL.ExecuteSql("insert into schedulerecord (scheduleid,result) values(" + dr["id"] + ",'" + result + "')");
            //                }
            //            }
                        
            //        }
            //}
            
        }
    }
}