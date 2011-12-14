using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using SeoWebSite.BLL;
using SeoWebSite.Model;
using System.Data;
using Winista.Text.HtmlParser.Lex;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Util;
using Winista.Text.HtmlParser.Filters;
using SeoWebSite.Common;

public partial class Server : System.Web.UI.Page
{
    protected string JsonStr = "";
    NowGoalBLL nbll = new NowGoalBLL();
    HistoryOddsBLL hbll = new HistoryOddsBLL();
    ScheduleBLL sbll = new ScheduleBLL();
    SeoWebSite.BLL.Schedule scheduleBLL = new SeoWebSite.BLL.Schedule();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            switch (Request.QueryString["a"])
            {
                case "updateMatchs"://更新比赛信息
                    updateMatchs();
                    break;
                case "updateAnalysiss":
                    updateAnalysiss();
                    break;
                case "updateSchedules":
                    updateSchedules();
                    break;
                case "updateOdds":
                    updateOdds();
                    break;
                case "getOdds3in1":
                    getOdds3in1();
                    break;
                case "getNoUpdatedScheduleID":
                    getNoUpdatedScheduleID();
                    break;
                case "changeScheduleUpdated":
                    changeScheduleUpdated();
                    break;
                default:
                    break;
            }
        }
    }

    private void getOdds3in1()
    {
        SeoWebSite.BLL.odds_rq odds_rqbll = new SeoWebSite.BLL.odds_rq();
        WebClientBLL webClientBLL = new WebClientBLL();
        try
        {
            if (Request["scheduleid"] != null && Request["companyid"] != null)
            {
                string scheduleid = Request.Form["scheduleid"];
                string[] companyid_Array = Request.Form["companyid"].Split(',');
                DataTable dt = new DataTable();
                dt.Columns.Add("scheduleid", typeof(int));
                dt.Columns.Add("companyid", typeof(int));
                dt.Columns.Add("aaa", typeof(double));
                dt.Columns.Add("aab", typeof(double));
                dt.Columns.Add("aac", typeof(double));
                dt.Columns.Add("aba", typeof(double));
                dt.Columns.Add("abb", typeof(double));
                dt.Columns.Add("abc", typeof(double));
                dt.Columns.Add("baa", typeof(double));
                dt.Columns.Add("bab", typeof(double));
                dt.Columns.Add("bac", typeof(double));
                dt.Columns.Add("bba", typeof(double));
                dt.Columns.Add("bbb", typeof(double));
                dt.Columns.Add("bbc", typeof(double));
                dt.Columns.Add("caa", typeof(double));
                dt.Columns.Add("cab", typeof(double));
                dt.Columns.Add("cac", typeof(double));
                dt.Columns.Add("cba", typeof(double));
                dt.Columns.Add("cbb", typeof(double));
                dt.Columns.Add("cbc", typeof(double));

                dt.Columns.Add("aaa1", typeof(string));
                dt.Columns.Add("aab1", typeof(string));
                dt.Columns.Add("aac1", typeof(string));
                dt.Columns.Add("aba1", typeof(string));
                dt.Columns.Add("abb1", typeof(string));
                dt.Columns.Add("abc1", typeof(string));
                dt.Columns.Add("baa1", typeof(string));
                dt.Columns.Add("bab1", typeof(string));
                dt.Columns.Add("bac1", typeof(string));
                dt.Columns.Add("bba1", typeof(string));
                dt.Columns.Add("bbb1", typeof(string));
                dt.Columns.Add("bbc1", typeof(string));
                foreach (string companyid in companyid_Array)
                {
                    string[] paramArr = { companyid, scheduleid };
                    string s = webClientBLL.GetRemoteHtml("odds/detail.aspx?companyID={0}&scheduleid={1}", paramArr);
                    Parser parser = Parser.CreateParser(s, "utf-8");
                    AndFilter andFilter = new AndFilter(new TagNameFilter("table"), new HasAttributeFilter("bgColor", "#bbbbbb"));
                    NodeList tableList = parser.ExtractAllNodesThatMatch(andFilter);
                    parser = Parser.CreateParser(s, "utf-8");
                    //NodeList h3tag = parser.ExtractAllNodesThatMatch(new TagNameFilter("h3"));
                    //string year = h3tag[0].ToPlainTextString().Remove(5);
                    if (tableList.Count == 3)
                    {
                        DataRow dr = dt.NewRow();
                        dr["scheduleid"] = scheduleid;
                        dr["companyid"] = companyid;
                        NodeList tdList;
                        string pankou;
                        #region 让球盘
                        AndFilter andFilter1 = new AndFilter(new TagNameFilter("tr"), new HasAttributeFilter("class", "ts1"));
                        NodeList list = tableList[0].Children.ExtractAllNodesThatMatch(andFilter1);
                        
                        if (list.Count > 0)
                        {
                            tdList = list[list.Count - 1].Children.SearchFor(typeof(Winista.Text.HtmlParser.Tags.TableColumn));//初盘
                            pankou = CommonHelper.GoalCnToGoal(tdList[1].ToPlainTextString());
                            double per1 = 0, per2 = 0, per3 = 0;
                            double per4 = 0, per5 = 0, per6 = 0;
                            if (!string.IsNullOrEmpty(pankou))
                            {
                                dr["aaa"] = tdList[0].ToPlainTextString();
                                dr["aab"] = pankou;
                                dr["aac"] = tdList[2].ToPlainTextString();
                                DataSet ds = odds_rqbll.queryCompanyOddsCount(companyid, tdList[0].ToPlainTextString(), pankou, tdList[2].ToPlainTextString(), Convert.ToInt32(Request.Form["scheduleType"]), "Chupan");
                                if (Convert.ToDouble(ds.Tables[0].Rows[0][3]) > 0)
                                {
                                    per1 = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0][0]) / Convert.ToDouble(ds.Tables[0].Rows[0][3]) * 100);
                                    per2 = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0][1]) / Convert.ToDouble(ds.Tables[0].Rows[0][3]) * 100);
                                    per3 = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0][2]) / Convert.ToDouble(ds.Tables[0].Rows[0][3]) * 100);
                                    dr["aaa1"] = ds.Tables[0].Rows[0][0].ToString() + "(" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0][0]) / Convert.ToDouble(ds.Tables[0].Rows[0][3]) * 100) + "%)";
                                    dr["aab1"] = ds.Tables[0].Rows[0][1].ToString() + "(" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0][1]) / Convert.ToDouble(ds.Tables[0].Rows[0][3]) * 100) + "%)";
                                    dr["aac1"] = ds.Tables[0].Rows[0][2].ToString() + "(" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0][2]) / Convert.ToDouble(ds.Tables[0].Rows[0][3]) * 100) + "%)";
                                }
                            }
                            tdList = list[0].Children.SearchFor(typeof(Winista.Text.HtmlParser.Tags.TableColumn));//终盘
                            pankou = CommonHelper.GoalCnToGoal(tdList[1].ToPlainTextString());
                            if (!string.IsNullOrEmpty(pankou))
                            {
                                dr["aba"] = tdList[0].ToPlainTextString();
                                dr["abb"] = pankou;
                                dr["abc"] = tdList[2].ToPlainTextString();
                                DataSet ds = odds_rqbll.queryCompanyOddsCount(companyid, tdList[0].ToPlainTextString(), pankou, tdList[2].ToPlainTextString(), Convert.ToInt32(Request.Form["scheduleType"]), "Zhongpan");
                                if (Convert.ToDouble(ds.Tables[0].Rows[0][3]) > 0)
                                {
                                    per4 = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0][0]) / Convert.ToDouble(ds.Tables[0].Rows[0][3]) * 100);
                                    per5 = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0][1]) / Convert.ToDouble(ds.Tables[0].Rows[0][3]) * 100);
                                    per6 = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0][2]) / Convert.ToDouble(ds.Tables[0].Rows[0][3]) * 100);
                                    dr["aba1"] = ds.Tables[0].Rows[0][0].ToString() + "(<font color=" + (per4 > per1 ? "red" : "green") + ">" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0][0]) / Convert.ToDouble(ds.Tables[0].Rows[0][3]) * 100) + "%</font>)";
                                    dr["abb1"] = ds.Tables[0].Rows[0][1].ToString() + "(<font color=" + (per5 > per2 ? "red" : "green") + ">" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0][1]) / Convert.ToDouble(ds.Tables[0].Rows[0][3]) * 100) + "%</font>)";
                                    dr["abc1"] = ds.Tables[0].Rows[0][2].ToString() + "(<font color=" + (per6 > per3 ? "red" : "green") + ">" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0][2]) / Convert.ToDouble(ds.Tables[0].Rows[0][3]) * 100) + "%</font>)";
                                }
                            }
                        }
                        #endregion
                        #region 标准盘
                        list = tableList[1].Children.ExtractAllNodesThatMatch(andFilter1);
                        if (list.Count > 0)
                        {
                            tdList = list[list.Count - 1].Children.SearchFor(typeof(Winista.Text.HtmlParser.Tags.TableColumn));
                            dr["baa"] = tdList[0].ToPlainTextString();
                            dr["bab"] = tdList[1].ToPlainTextString();
                            dr["bac"] = tdList[2].ToPlainTextString();
                            tdList = list[0].Children.SearchFor(typeof(Winista.Text.HtmlParser.Tags.TableColumn));//终盘
                            dr["bba"] = tdList[0].ToPlainTextString();
                            dr["bbb"] = tdList[1].ToPlainTextString();
                            dr["bbc"] = tdList[2].ToPlainTextString();
                        }
                        #endregion
                        #region 大小盘
                        list = tableList[2].Children.ExtractAllNodesThatMatch(andFilter1);
                        if (list.Count > 0)
                        {
                            tdList = list[list.Count - 1].Children.SearchFor(typeof(Winista.Text.HtmlParser.Tags.TableColumn));
                            pankou = CommonHelper.BallSizeToBall(tdList[1].ToPlainTextString());
                            if (!string.IsNullOrEmpty(pankou))
                            {
                                dr["caa"] = tdList[0].ToPlainTextString();
                                dr["cab"] = pankou;
                                dr["cac"] = tdList[2].ToPlainTextString();
                            }
                            tdList = list[0].Children.SearchFor(typeof(Winista.Text.HtmlParser.Tags.TableColumn));
                            pankou = CommonHelper.BallSizeToBall(tdList[1].ToPlainTextString());
                            if (!string.IsNullOrEmpty(pankou))
                            {
                                dr["cba"] = tdList[0].ToPlainTextString();
                                dr["cbb"] = pankou;
                                dr["cbc"] = tdList[2].ToPlainTextString();
                            }
                        }
                        #endregion
                        dt.Rows.Add(dr);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr1 = dt.NewRow();


                    #region 亚洲盘统计
                    List<double> pankouList1 = new List<double>();
                    List<double> homefcList1 = new List<double>();
                    List<double> awayfcList1 = new List<double>();
                    List<double> pankouList2 = new List<double>();
                    List<double> homefcList2 = new List<double>();
                    List<double> awayfcList2 = new List<double>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["aab"] != DBNull.Value && !pankouList1.Contains(Convert.ToDouble(dr["aab"])))
                        {
                            pankouList1.Add(Convert.ToDouble(dr["aab"]));
                        }
                        if (dr["abb"] != DBNull.Value && !pankouList2.Contains(Convert.ToDouble(dr["abb"])))
                        {
                            pankouList2.Add(Convert.ToDouble(dr["abb"]));
                        }
                    }
                    foreach (decimal item in pankouList2)
                    {
                        if (dt.Select("aab=" + item).Count() > 1)
                        {
                            double fc = Convert.ToDouble(dt.Compute("Var(aaa)", "aab=" + item));
                            homefcList1.Add(fc);
                            fc = Convert.ToDouble(dt.Compute("Var(aac)", "aab=" + item));
                            awayfcList1.Add(fc);
                        }
                        if (dt.Select("abb=" + item).Count() > 1)
                        {
                            double fc = Convert.ToDouble(dt.Compute("Var(aba)", "abb=" + item));
                            homefcList2.Add(fc);
                            fc = Convert.ToDouble(dt.Compute("Var(abc)", "abb=" + item));
                            awayfcList2.Add(fc);
                        }
                    }
                    dr1["aaa"] = Convert.ToDouble(dt.Compute("Avg(aaa)", "1=1"));
                    dr1["aac"] = Convert.ToDouble(dt.Compute("Avg(aac)", "1=1"));
                    dr1["aaa1"] = homefcList1.Average();
                    dr1["aac1"] = awayfcList1.Average(); 
                    dr1["aba"] = Convert.ToDouble(dt.Compute("Avg(aba)", "1=1"));
                    dr1["abc"] = Convert.ToDouble(dt.Compute("Avg(abc)", "1=1"));
                    dr1["aba1"] = "<font color=" + (homefcList2.Average() > homefcList1.Average() ? "red" : "green") + ">" + homefcList2.Average() + "</font>";
                    dr1["abc1"] = "<font color=" + (awayfcList2.Average() > awayfcList1.Average() ? "red" : "green") + ">" + awayfcList2.Average() + "</font>";
                    #endregion

                    dr1["baa"] = Convert.ToDouble(dt.Compute("Avg(baa)", "1=1"));
                    dr1["bab"] = Convert.ToDouble(dt.Compute("Avg(bab)", "1=1"));
                    dr1["bac"] = Convert.ToDouble(dt.Compute("Avg(bac)", "1=1"));
                    dr1["bba"] = Convert.ToDouble(dt.Compute("Avg(bba)", "1=1"));
                    dr1["bbb"] = Convert.ToDouble(dt.Compute("Avg(bbb)", "1=1"));
                    dr1["bbc"] = Convert.ToDouble(dt.Compute("Avg(bbc)", "1=1"));
                    dr1["baa1"] = Convert.ToDouble(dt.Compute("Var(baa)", "1=1"));
                    dr1["bab1"] = Convert.ToDouble(dt.Compute("Var(bab)", "1=1"));
                    dr1["bac1"] = Convert.ToDouble(dt.Compute("Var(bac)", "1=1"));
                    dr1["bba1"] = "<font color=" + (Convert.ToDouble(dt.Compute("Var(bba)", "1=1")) > Convert.ToDouble(dr1["baa1"]) ? "red" : "green") + ">" + Convert.ToDouble(dt.Compute("Var(bba)", "1=1")) + "</font>";
                    dr1["bbb1"] = "<font color=" + (Convert.ToDouble(dt.Compute("Var(bbb)", "1=1")) > Convert.ToDouble(dr1["bab1"]) ? "red" : "green") + ">" + Convert.ToDouble(dt.Compute("Var(bbb)", "1=1")) + "</font>";
                    dr1["bbc1"] = "<font color=" + (Convert.ToDouble(dt.Compute("Var(bbc)", "1=1")) > Convert.ToDouble(dr1["bac1"]) ? "red" : "green") + ">" + Convert.ToDouble(dt.Compute("Var(bbc)", "1=1")) + "</font>";
                    dt.Rows.Add(dr1);
                }
                
                JObject result = new JObject();
                result.Add(new JProperty("success", true));
                result.Add(new JProperty("totlalCount", dt.Rows.Count));
                result.Add(new JProperty("data", JArray.FromObject(dt)));
                JsonStr = result.ToString();
            }
            else
            {
                JsonStr = "{success:false,message:'请求数据异常！'}";
            }
        }
        catch (Exception e)
        {
            JsonStr = "{success:false,message:'" + e.Message + "'}";
        }
    }

    private void changeScheduleUpdated()
    {
        if (Request["scheduleid"] != null)
        {
            scheduleBLL.SetUpdated(Request.Form["scheduleid"], true);
        }
    }

    private void getNoUpdatedScheduleID()
    {
        DataSet ds = scheduleBLL.GetScheduleIDList("updated<>1");
        List<string> scheduleidList = new List<string>();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            scheduleidList.Add(dr[0].ToString());
        }
        JsonStr = "{success:true,data:'" + string.Join(",", scheduleidList.ToArray()) + "'}";
    }

    private void updateOdds()
    {
        //SeoWebSite.BLL.odds_bz odds_bzBLL = new SeoWebSite.BLL.odds_bz();
        //SeoWebSite.BLL.odds_rq odds_rqBLL = new SeoWebSite.BLL.odds_rq();
        //SeoWebSite.BLL.odds_dx odds_dxBLL = new SeoWebSite.BLL.odds_dx();
        //SeoWebSite.BLL.Company companyBLL = new SeoWebSite.BLL.Company();
        NowGoalBLL nowGoalBLL = new NowGoalBLL();
        WebClientBLL webClientBLL = new WebClientBLL();
        try
        {
            //if (Request["scheduleid"] != null && Request["companyid"] != null)
            //{
            //    string scheduleid = Request.Form["scheduleid"];
            //    string companyid = Request.Form["companyid"];
            //    string[] paramArr = { companyid, scheduleid };
            //    string s = webClientBLL.GetRemoteHtml("odds/detail.aspx?companyID={0}&scheduleid={1}", paramArr);
            //    if (string.IsNullOrEmpty(s))
            //    {
            //        scheduleBLL.Delete(int.Parse(scheduleid));
            //        JsonStr = "{success:false,message:'已删除错误的比赛数据'}";
            //    }
            //    Parser parser = Parser.CreateParser(s, "utf-8");
            //    AndFilter andFilter = new AndFilter(new TagNameFilter("table"), new HasAttributeFilter("bgColor", "#bbbbbb"));
            //    NodeList tableList = parser.ExtractAllNodesThatMatch(andFilter);
            //    parser = Parser.CreateParser(s, "utf-8");
            //    NodeList h3tag = parser.ExtractAllNodesThatMatch(new TagNameFilter("h3"));
            //    string year = h3tag[0].ToPlainTextString().Remove(5);
            //    if (tableList.Count == 3)
            //    {
            //        odds_rqBLL.Delete(companyid, scheduleid);
            //        odds_bzBLL.Delete(companyid, scheduleid);
            //        odds_dxBLL.Delete(companyid, scheduleid);
            //        AndFilter andFilter1 = new AndFilter(new TagNameFilter("tr"), new HasAttributeFilter("class", "ts1"));
            //        #region 让球盘
            //        NodeList list = tableList[0].Children.ExtractAllNodesThatMatch(andFilter1);
            //        for (int i = 0; i < list.Count; i++)
            //        {
            //            NodeList tdList = list[i].Children.SearchFor(typeof(Winista.Text.HtmlParser.Tags.TableColumn));
            //            string pankou = CommonHelper.GoalCnToGoal(tdList[1].ToPlainTextString());
            //            if (!string.IsNullOrEmpty(pankou))
            //            {
            //                SeoWebSite.Model.odds_rq model = new SeoWebSite.Model.odds_rq();
            //                model.scheduleID = int.Parse(scheduleid);
            //                model.companyID = int.Parse(companyid);
            //                model.home = decimal.Parse(tdList[0].ToPlainTextString());
            //                model.pankou = decimal.Parse(pankou);
            //                model.away = decimal.Parse(tdList[2].ToPlainTextString());
            //                model.time = DateTime.Parse(year + "-" + tdList[3].ToPlainTextString().Replace("早餐", ""));
            //                odds_rqBLL.Add(model);
            //            }
            //        }
            //        #endregion
            //        #region 标准盘
            //        list = tableList[1].Children.ExtractAllNodesThatMatch(andFilter1);
            //        for (int i = 0; i < list.Count; i++)
            //        {
            //            NodeList tdList = list[i].Children.SearchFor(typeof(Winista.Text.HtmlParser.Tags.TableColumn));
            //            SeoWebSite.Model.odds_bz model = new SeoWebSite.Model.odds_bz();
            //            model.scheduleID = int.Parse(scheduleid);
            //            model.companyID = int.Parse(companyid);
            //            model.home = decimal.Parse(tdList[0].ToPlainTextString());
            //            model.draw = decimal.Parse(tdList[1].ToPlainTextString());
            //            model.away = decimal.Parse(tdList[2].ToPlainTextString());
            //            model.time = DateTime.Parse(year + "-" + tdList[3].ToPlainTextString().Replace("早餐", ""));
            //            odds_bzBLL.Add(model);
            //        }
            //        #endregion
            //        #region 大小盘
            //        list = tableList[2].Children.ExtractAllNodesThatMatch(andFilter1);
            //        for (int i = 0; i < list.Count; i++)
            //        {
            //            NodeList tdList = list[i].Children.SearchFor(typeof(Winista.Text.HtmlParser.Tags.TableColumn));
            //            string pankou = CommonHelper.BallSizeToBall(tdList[1].ToPlainTextString());
            //            if (!string.IsNullOrEmpty(pankou))
            //            {
            //                SeoWebSite.Model.odds_dx model = new SeoWebSite.Model.odds_dx();
            //                model.scheduleID = int.Parse(scheduleid);
            //                model.companyID = int.Parse(companyid);
            //                model.big = decimal.Parse(tdList[0].ToPlainTextString());
            //                model.pankou = decimal.Parse(pankou);
            //                model.small = decimal.Parse(tdList[2].ToPlainTextString());
            //                model.time = DateTime.Parse(year + "-" + tdList[3].ToPlainTextString().Replace("早餐", ""));
            //                odds_dxBLL.Add(model);
            //            }
            //        }
            //        #endregion
            //    }
            //    JsonStr = "{success:true}";
            //}
            if (Request["scheduleid"] != null)
            {
                nowGoalBLL.updateOdds1x2(Request.Form["scheduleid"]);
                scheduleBLL.SetUpdated(Request.Form["scheduleid"], true);
                JsonStr = "{success:true}";
            }
            else
            {
                JsonStr = "{success:false,message:'请求数据异常！'}";
            }
        }
        catch (Exception e)
        {
            if (e.Message == "远程服务器返回错误: (404) 未找到。")
            {
                scheduleBLL.Delete(int.Parse(Request.Form["scheduleid"]));
                JsonStr = "{success:true}";
            }
            else
            {
                JsonStr = "{success:false,message:'" + e.Message + "'}";
            }
        }
    }

    private void updateSchedules()
    {
        SeoWebSite.BLL.ScheduleType scheduleTypeBLL = new SeoWebSite.BLL.ScheduleType();
        try
        {
            if (Request["schedulelist"] != null && Request["scheduletypelist"] != null && Request["date"] != null)
            {
                string[] scheduleList = HttpUtility.HtmlDecode(Request.Form["schedulelist"]).Split('^');
                string[] scheduleTypeList = HttpUtility.HtmlDecode(Request.Form["scheduletypelist"]).Split('^');
                string date = Request.Form["date"];
                foreach (string item in scheduleList)
                {
                    string[] schedule = item.Split(',');
                    if (!scheduleBLL.Exists(int.Parse(schedule[0])) && schedule[12] == "-1")
                    {
                        SeoWebSite.Model.Schedule model = new SeoWebSite.Model.Schedule();
                        model.id = int.Parse(schedule[0]);
                        model.data = item;
                        model.date = DateTime.Parse(date);
                        model.updated = false;
                        if (string.IsNullOrEmpty(schedule[13]) || string.IsNullOrEmpty(schedule[14]) || string.IsNullOrEmpty(schedule[15]) || string.IsNullOrEmpty(schedule[16]) || string.IsNullOrEmpty(schedule[1]))
                        {
                            continue;
                        }
                        model.home = int.Parse(schedule[13]);
                        model.away = int.Parse(schedule[14]);
                        model.halfhome = int.Parse(schedule[15]);
                        model.halfaway = int.Parse(schedule[16]);
                        model.scheduleTypeID = int.Parse(scheduleTypeList[int.Parse(schedule[1])].Split(',')[0]);
                        scheduleBLL.Add(model);
                    }
                }
                foreach (string item in scheduleTypeList)
                {
                    string[] scheduleType = item.Split(',');
                    if (!scheduleTypeBLL.Exists(int.Parse(scheduleType[0])))
                    {
                        SeoWebSite.Model.ScheduleType model = new SeoWebSite.Model.ScheduleType();
                        model.id = int.Parse(scheduleType[0]);
                        model.name = scheduleType[1];
                        model.data = item;
                        scheduleTypeBLL.Add(model);
                    }
                }
                JsonStr = "{success:true }";
            }
            else
            {
                JsonStr = "{success:false,error:'更新失败！'}";
            }
        }
        catch (Exception e)
        {
            JsonStr = "{success:false,message:\"" + e.Message + "\"}";
        }
    }

    private void updateAnalysiss()
    {
        //Schedule model = sbll.GetTopOne_NoExp();
        //if (model != null)
        //{
        //    string[] match = HttpUtility.HtmlDecode(model.Data).Split(',');
        //    string[] timeStr = match[10].Replace("<br>", "-").Split(new char[2] { '-', ':' });
        //    DateTime time = new DateTime(int.Parse("20" + timeStr[0]), int.Parse(timeStr[1]), int.Parse(timeStr[2]), int.Parse(timeStr[3]), int.Parse(timeStr[4]), 0);
        //    if (hbll.GetCount(model.ScheduleID) < 300)
        //    {
        //        sbll.Delete(model.id);
        //        hbll.Delete(model.ScheduleID);
        //        JsonStr = "{success:true,content:'" + match[4] + "-" + match[7] + match[10] + " 历史赔率不足300条！清除数据'}";
        //        return;
        //    }
        //    JsonStr = hbll.GetKellyAverageLineChart(match[0], null, 10800, false);
        //    if (string.IsNullOrEmpty(JsonStr))
        //    {
        //        JsonStr = "{success:false,content:'<font color=red>" + match[4] + "-" + match[7] + match[10] + " 更新失败！</font>'}";
        //        return;
        //    }
        //    hbll.Delete(model.ScheduleID);
        //    JsonStr = "{success:true,content:'" + match[4] + "-" + match[7] + match[10] + " 更新成功'}";
        //}
        //else
        //{
        //    JsonStr = "{success:false,content:'没有为更新数据！'}";
        //}
    }

    private void updateMatchs()
    {
        //Schedule model = sbll.GetTopOne();
        //if (model != null)
        //{
        //    string[] match = HttpUtility.HtmlDecode(model.Data).Split(',');
        //    string[] timeStr = match[10].Replace("<br>", "-").Split(new char[2] { '-', ':' });
        //    DateTime time = new DateTime(int.Parse("20" + timeStr[0]), int.Parse(timeStr[1]), int.Parse(timeStr[2]), int.Parse(timeStr[3]), int.Parse(timeStr[4]), 0);
        //    string json = nbll.GetOdds1x2(match[0]);
        //    if (string.IsNullOrEmpty(json))
        //    {
        //        sbll.Delete(model.id);
        //        hbll.Delete(model.ScheduleID);
        //        JsonStr = "{success:true,content:'" + match[4] + "-" + match[7] + match[10] + " 百家欧赔无数据！清除数据'}";
        //        return;
        //    }
        //    JObject obj = JObject.Parse(json);
        //    if ((int)obj["totlalCount"] <= 100)
        //    {
        //        sbll.Delete(model.id);
        //        hbll.Delete(model.ScheduleID);
        //        JsonStr = "{success:true,content:'" + match[4] + "-" + match[7] + match[10] + " 开盘公司低于100家！清除数据'}";
        //        return;
        //    }
        //    List<string> clist = obj["data"].Select(m => (string)m.SelectToken("Odds_0")).ToList();
        //    List<string> hlist = obj["data"].Select(m => (string)m.SelectToken("Odds_1")).ToList();
        //    string s = hbll.Add(match[0], string.Join(",", clist.ToArray()), string.Join(",", hlist.ToArray()), time);
        //    obj = JObject.Parse(s);
        //    if (obj["totalCount"].ToString() == "0")
        //    {
        //        sbll.Delete(model.id);
        //        hbll.Delete(model.ScheduleID);
        //        JsonStr = "{success:true,content:'" + match[4] + "-" + match[7] + match[10] + " 公司历史赔率无数据！清除数据'}";
        //        return;
        //    }
        //    sbll.UpdateState(model.id, true);
        //    JsonStr = "{success:true,content:'" + match[4] + "-" + match[7] + match[10] + " 更新成功'}";
        //}
        //else
        //{
        //    JsonStr = "{success:false,content:'没有未更新数据！'}";
        //}

    }

    //private void updateAnalysiss()
    //{
    //    string matchid = Request.Form["matchid"];
    //    double time = double.Parse(Request.Form["time"]);
    //    JsonStr = hbll.GetKellyAverageLineChart(matchid, null, time, false);
    //}

}