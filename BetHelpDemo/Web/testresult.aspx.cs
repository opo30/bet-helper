using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.BLL;
using System.Data;
using SeoWebSite.DBUtility;

public partial class testresult : System.Web.UI.Page
{
    protected int winCount = 0;
    protected int totalCount = 0;
    protected int winCount1 = 0;
    protected int totalCount1 = 0;
    protected int winCount2 = 0;
    protected int totalCount2 = 0;
    protected int winCount3 = 0;
    protected int totalCount3 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ScheduleBLL schedulebll = new ScheduleBLL();
            odds_rq rqbll = new odds_rq();
            odds_bz bzbll = new odds_bz();

            DataSet schedule = DbHelperSQL.Query("select top 200 id,scheduletypeid,Data,Date,home,away FROM Schedule where updated=1 order by id desc");
            List<int> companyidList = new List<int>();
            foreach (DataRow dr in schedule.Tables[0].Rows)
            {
                DataSet odds = DbHelperSQL.Query("select a.companyid, a.home aaa,a.pankou aab,a.away aac,b.home aba,b.pankou abb,b.away abc from tempdb..TempTable_Companys_Chupan_RQ a join tempdb..TempTable_Companys_Zhongpan_RQ b on a.scheduleid=b.scheduleid and a.companyid=b.companyid where a.scheduleid=" + dr["id"]);
                List<string> oddsList = new List<string>();
                foreach (DataRow item in odds.Tables[0].Rows)
                {
                    string[] sa = { item["aaa"].ToString(), item["aab"].ToString(), item["aac"].ToString(), item["aba"].ToString(), item["abb"].ToString(), item["abc"].ToString(), item["companyid"].ToString() };
                    oddsList.Add(string.Join(",", sa));
                    companyidList.Add(Convert.ToInt32(item["companyid"]));
                }

                if (oddsList.Count > 0)
                {
                    //DataTable dt = rqbll.queryOddsPer(oddsList, 0.02f, 0, Convert.ToInt32(dr["id"]));
                    //DataTable dt = rqbll.queryCompanyOddsPer(oddsList, 0, Convert.ToInt32(dr["id"]));
                    DataTable dt = bzbll.queryCompanyOddsPer(oddsList, 0, Convert.ToInt32(dr["id"]));
                    //DataTable dt = rqbll.queryCompanyOddsPer1(Convert.ToInt32(dr["scheduletypeid"]), Convert.ToInt32(dr["id"]), companyidList);
                    try
                    {

                        double pankou = Convert.ToDouble(odds.Tables[0].Rows[0]["aab"]);
                        if (dt.Rows.Count > 0)
                        {
                            if (Math.Abs(Convert.ToInt32(dt.Compute("avg(rqy)", "1=1")) - Convert.ToInt32(dt.Compute("avg(rqs)", "1=1"))) > 10)
                            {
                                if (Convert.ToInt32(dt.Compute("avg(rqy)", "1=1")) - Convert.ToInt32(dt.Compute("avg(rqs)", "1=1")) > 10 && Convert.ToInt32(dr["home"]) - Convert.ToInt32(dr["away"]) > pankou)
                                {
                                    winCount++;
                                }
                                else if (Convert.ToInt32(dt.Compute("avg(rqy)", "1=1")) - Convert.ToInt32(dt.Compute("avg(rqs)", "1=1")) < 10 && Convert.ToInt32(dr["home"]) - Convert.ToInt32(dr["away"]) < pankou)
                                {
                                    winCount++;
                                }
                                totalCount++;
                            }
                            if (Math.Abs(Convert.ToInt32(dt.Compute("max(rqy)", "1=1")) - Convert.ToInt32(dt.Compute("max(rqs)", "1=1"))) > 0)
                            {
                                if (Convert.ToInt32(dt.Compute("max(rqy)", "1=1")) - Convert.ToInt32(dt.Compute("max(rqs)", "1=1")) > 0 && Convert.ToInt32(dr["home"]) - Convert.ToInt32(dr["away"]) > pankou)
                                {
                                    winCount1++;
                                }
                                else if (Convert.ToInt32(dt.Compute("max(rqy)", "1=1")) - Convert.ToInt32(dt.Compute("max(rqs)", "1=1")) < 0 && Convert.ToInt32(dr["home"]) - Convert.ToInt32(dr["away"]) < pankou)
                                {
                                    winCount1++;
                                }
                                totalCount1++;
                            }
                            double ycount = Convert.ToDouble(dt.Compute("count(rqy)", "rqy > rqs"));
                            double scount = Convert.ToDouble(dt.Compute("count(rqy)", "rqy < rqs"));
                            if (ycount + scount > 0)
                            {
                                if (ycount / (ycount + scount) > 0.7d || scount / (ycount + scount) > 0.7d)
                                {
                                    if (ycount / (ycount + scount) > 0.7d && Convert.ToInt32(dr["home"]) - Convert.ToInt32(dr["away"]) > pankou)
                                    {
                                        winCount2++;
                                    }
                                    else if (scount / (ycount + scount) > 0.7d && Convert.ToInt32(dr["home"]) - Convert.ToInt32(dr["away"]) < pankou)
                                    {
                                        winCount2++;
                                    }
                                    totalCount2++;
                                }
                            }
                            

                            if (Convert.ToInt32(dt.Compute("max(rqy)", "1=1")) - Convert.ToInt32(dt.Compute("max(rqs)", "1=1")) < 0 && Convert.ToInt32(dt.Compute("count(rqy)", "rqy > rqs")) - Convert.ToInt32(dt.Compute("count(rqy)", "rqy < rqs")) >= 10)
                            {
                                if (Convert.ToInt32(dr["home"]) - Convert.ToInt32(dr["away"]) > pankou)
                                {
                                    winCount3++;
                                }
                                totalCount3++;
                            }
                            else if (Convert.ToInt32(dt.Compute("max(rqy)", "1=1")) - Convert.ToInt32(dt.Compute("max(rqs)", "1=1")) >0 && Convert.ToInt32(dt.Compute("count(rqy)", "rqy > rqs")) - Convert.ToInt32(dt.Compute("count(rqy)", "rqy < rqs")) <= 10)
                            {
                                if (Convert.ToInt32(dr["home"]) - Convert.ToInt32(dr["away"]) < pankou)
                                {
                                    winCount3++;
                                }
                                totalCount3++;
                            }
                            
                        }
                       
                        //DataSet ds = rqbll.queryOddsCount(oddsList, 0.05f, 0, Convert.ToInt32(dr["id"]));//Convert.ToInt32(dr["scheduleTypeid"])
                        //if (ds != null && Convert.ToInt32(ds.Tables[0].Rows[0]["totalCount"]) > 0)
                        //{
                        //    int diff = Convert.ToInt32(ds.Tables[0].Rows[0][0]) - Convert.ToInt32(ds.Tables[0].Rows[0][2]);
                        //    //if (Math.Abs(diff) >= 5 && Math.Min(Convert.ToInt32(ds.Tables[0].Rows[0][0]), Convert.ToInt32(ds.Tables[0].Rows[0][2])) == 0)
                        //    //{
                        //    if (diff > 0 && Convert.ToInt32(dr["home"]) - Convert.ToInt32(dr["away"]) > pankou)
                        //    {
                        //        winCount3++;
                        //    }
                        //    else if (diff < 0 && Convert.ToInt32(dr["home"]) - Convert.ToInt32(dr["away"]) < pankou)
                        //    {
                        //        winCount3++;
                        //    }
                        //    totalCount3++;
                        //    //}
                        //}
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }

               

            }
        }
    }
}