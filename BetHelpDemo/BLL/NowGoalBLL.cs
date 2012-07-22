using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeoWebSite.Common;
using System.Text.RegularExpressions;
using SeoWebSite.Common.JSON;
using Model.NowGoal;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Xml;
using SeoWebSite.DAL;
using SeoWebSite.Model;

namespace SeoWebSite.BLL
{
    public class NowGoalBLL
    {
        private readonly SeoWebSite.DAL.Odds1x2HistoryDAO dal = new SeoWebSite.DAL.Odds1x2HistoryDAO();
        private readonly SeoWebSite.DAL.ScheduleDAO sdal = new SeoWebSite.DAL.ScheduleDAO();

        public string GetBetLiveData(string date, int companyid)
        {
            WebClientBLL web = new WebClientBLL();
            string actual = "";
            if (string.IsNullOrEmpty(date))
            {
                actual = web.LoadLiveFile();
            }
            else
            {
                actual = web.LoadHistoryFile(date);
            }

            List<string> existList = new List<string>();
            //string[] sArray = Regex.Split(actual, "\r\n", RegexOptions.IgnoreCase);
            //SeoWebSite.DAL.BetExpDAO betExpDAO = new SeoWebSite.DAL.BetExpDAO();
            //foreach (string i in sArray)
            //{
            //    if (i.StartsWith("A[") && i.EndsWith("];"))
            //    {
            //        string matchid = i.Split(new char[2] { '[', ']' })[3].Split(',')[0];

            //        //existList.Add(betExpDAO.GetIsExpByID(matchid));
            //    }
            //}
            actual = ("var favorites=['" + string.Join("','", existList.ToArray()) + "'];") + actual;
            string showids = "";
            if (companyid != 0)
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load("http://live.nowodds.com/data/goal" + companyid + ".xml");
                showids = xmldoc.SelectNodes("c/ids")[0].InnerText;
            }
            actual = ("var showlist=[" + showids + "];") + actual;

            return actual;
        }

        public void updateOdds1x2(string scheduleID)
        {
            try
            {
                OddsDAO oddsDAO = new OddsDAO();
                CompanyDAO companyDAO = new CompanyDAO();
                WebClientBLL bll = new WebClientBLL();
                string actual = bll.UpdateOdds1x2Content(scheduleID);

                //获取赔率原始数据
                Regex reg = new Regex("game\\=Array\\(\"" + "\\w[^;" + "]*;");
                Match mat = reg.Match(actual);
                if (mat != null && !String.IsNullOrEmpty(mat.Value))
                {

                    //所有公司数据
                    string source = mat.Value.Substring(12, mat.Value.Length - 10 - 4);
                    //分解出每个公司数据
                    string[] compstrs = Regex.Split(source, "\",\"", RegexOptions.IgnoreCase);
                    foreach (string compstr in compstrs)
                    {
                        JObject item = new JObject();
                        string[] oddsArr = compstr.Replace("\"", "").Split('|');

                        #region 插入公司数据
                        if (!companyDAO.Exists(int.Parse(oddsArr[0])) && oddsArr.Length > 22)
                        {
                            SeoWebSite.Model.Company company = new SeoWebSite.Model.Company();
                            company.id = int.Parse(oddsArr[0]);
                            company.fullname = oddsArr[21];
                            company.name = oddsArr[2];
                            company.isprimary = Convert.ToBoolean(int.Parse(oddsArr[22]));
                            company.isexchange = Convert.ToBoolean(int.Parse(oddsArr[23]));
                            companyDAO.Add(company);
                        }
                        #endregion

                        #region 插入欧赔数据
                        if (!oddsDAO.Exists(int.Parse(oddsArr[1])))
                        {
                            Odds odds = new Odds();
                            odds.scheduleid = int.Parse(scheduleID);
                            odds.companyid = int.Parse(oddsArr[0]);
                            odds.id = int.Parse(oddsArr[1]);
                            odds.s_win = decimal.Parse(oddsArr[3]);
                            odds.s_draw = decimal.Parse(oddsArr[4]);
                            odds.s_lost = decimal.Parse(oddsArr[5]);
                            odds.s_winper = decimal.Parse(oddsArr[6]);
                            odds.s_drawper = decimal.Parse(oddsArr[7]);
                            odds.s_lostper = decimal.Parse(oddsArr[8]);
                            if (!String.IsNullOrEmpty(oddsArr[9]))
                            {
                                odds.s_return = decimal.Parse(oddsArr[9]);
                            }
                            if (!String.IsNullOrEmpty(oddsArr[10]))
                            {
                                odds.e_win = decimal.Parse(oddsArr[10]);
                            }
                            if (!String.IsNullOrEmpty(oddsArr[11]))
                            {
                                odds.e_draw = decimal.Parse(oddsArr[11]);
                            }
                            if (!String.IsNullOrEmpty(oddsArr[12]))
                            {
                                odds.e_lost = decimal.Parse(oddsArr[12]);
                            }
                            if (!String.IsNullOrEmpty(oddsArr[13]))
                            {
                                odds.e_winper = decimal.Parse(oddsArr[13]);
                            }
                            if (!String.IsNullOrEmpty(oddsArr[14]))
                            {
                                odds.e_drawper = decimal.Parse(oddsArr[14]);
                            }
                            if (!String.IsNullOrEmpty(oddsArr[15]))
                            {
                                odds.e_lostper = decimal.Parse(oddsArr[15]);
                            }
                            if (!String.IsNullOrEmpty(oddsArr[16]))
                            {
                                odds.e_return = decimal.Parse(oddsArr[16]);
                            }
                            if (!String.IsNullOrEmpty(oddsArr[17]))
                            {
                                odds.winkelly = decimal.Parse(oddsArr[17]);
                            }
                            if (!String.IsNullOrEmpty(oddsArr[18]))
                            {
                                odds.drawkelly = decimal.Parse(oddsArr[18]);
                            }
                            if (!String.IsNullOrEmpty(oddsArr[19]))
                            {
                                odds.lostkelly = decimal.Parse(oddsArr[19]);
                            }
                            string[] timeArr = oddsArr[20].Split(',');
                            odds.lastupdatetime = new DateTime(int.Parse(timeArr[0]), int.Parse(timeArr[1].Remove(2)), int.Parse(timeArr[2]), int.Parse(timeArr[3]), int.Parse(timeArr[4]), int.Parse(timeArr[5])).AddHours(8);
                            oddsDAO.Add(odds);
                        }
                        #endregion
                    }
                }
            }
            catch (Exception e)
            {
                sdal.Delete(Convert.ToInt32(scheduleID));
                throw e;
            }
        }

        public void updateOdds1x2Stat(string scheduleID)
        {
            try
            {
                ScheduleAnalysisBLL scheduleAnalysisBLL = new ScheduleAnalysisBLL();
                ScheduleBLL scheduleBLL = new ScheduleBLL();
                WebClientBLL bll = new WebClientBLL();
                string actual = bll.UpdateOdds1x2Content(scheduleID);

                //获取赔率原始数据
                Regex reg = new Regex("game\\=Array\\(\"" + "\\w[^;" + "]*;");
                Match mat = reg.Match(actual);
                if (mat != null && !String.IsNullOrEmpty(mat.Value))
                {

                    List<string> swhereList = new List<string>();
                    List<string> ewhereList = new List<string>();
                    List<decimal> swlist = new List<decimal>();
                    List<decimal> sdlist = new List<decimal>();
                    List<decimal> sllist = new List<decimal>();
                    List<decimal> ewlist = new List<decimal>();
                    List<decimal> edlist = new List<decimal>();
                    List<decimal> ellist = new List<decimal>();

                    //所有公司数据
                    string source = mat.Value.Substring(12, mat.Value.Length - 10 - 4);
                    //分解出每个公司数据
                    string[] oddsArr = Regex.Split(source, "\",\"", RegexOptions.IgnoreCase);
                    foreach (string oddsStr in oddsArr)
                    {
                        string[] oddsArray = oddsStr.Replace("\"", "").Split('|');
                        swhereList.Add("(companyid=" + oddsArray[0] + " and s_win=" + oddsArray[3] +
                                    " and s_draw=" + oddsArray[4] + " and s_lost=" + oddsArray[5] + ")");
                        swlist.Add(Convert.ToDecimal(oddsArray[6]));
                        sdlist.Add(Convert.ToDecimal(oddsArray[7]));
                        sllist.Add(Convert.ToDecimal(oddsArray[8]));
                        if (!string.IsNullOrEmpty(oddsArray[10]) && !string.IsNullOrEmpty(oddsArray[11]) && !string.IsNullOrEmpty(oddsArray[12]) && !string.IsNullOrEmpty(oddsArray[13]) && !string.IsNullOrEmpty(oddsArray[14]) && !string.IsNullOrEmpty(oddsArray[15]))
                        {
                            ewhereList.Add("(companyid=" + oddsArray[0] + " and e_win=" + oddsArray[10] +
                                    " and e_draw=" + oddsArray[11] + " and e_lost=" + oddsArray[12] + ")");
                            ewlist.Add(Convert.ToDecimal(oddsArray[13]));
                            edlist.Add(Convert.ToDecimal(oddsArray[14]));
                            ellist.Add(Convert.ToDecimal(oddsArray[15]));
                        }
                    }
                    DataSet sds = scheduleBLL.statOddsHistory("(" + String.Join(" or ", swhereList.ToArray()) + ")");
                    DataSet eds = scheduleBLL.statOddsHistory("(" + String.Join(" or ", ewhereList.ToArray()) + ")");
                    if (sds != null && eds != null)
                    {
                        ScheduleAnalysis model = new ScheduleAnalysis();
                        model.scheduleid = Convert.ToInt32(scheduleID);
                        model.oddswin = ewlist.Average() - swlist.Average();
                        model.oddsdraw = edlist.Average() - sdlist.Average();
                        model.oddslost = ellist.Average() - sllist.Average();
                        model.perwin = Convert.ToDecimal(eds.Tables[0].Rows[0][0]) - Convert.ToDecimal(sds.Tables[0].Rows[0][0]);
                        model.perdraw = Convert.ToDecimal(eds.Tables[0].Rows[0][1]) - Convert.ToDecimal(sds.Tables[0].Rows[0][1]);
                        model.perlost = Convert.ToDecimal(eds.Tables[0].Rows[0][2]) - Convert.ToDecimal(sds.Tables[0].Rows[0][2]);
                        model.time = DateTime.Now;
                        if (!scheduleAnalysisBLL.Exists(model))
                        {
                            scheduleAnalysisBLL.Add(model);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 获得综合赔率
        /// </summary>
        /// <param name="scheduleID">比赛编码</param>
        /// <param name="companyID">公司编码列表</param>
        /// <returns></returns>
        public string GetOddsDetail(string scheduleID, string companyID, string dateStr)
        {
            if (string.IsNullOrEmpty(dateStr))
            {
                WebClientBLL.UpdateOddsDetailContent(companyID);
            }
            else
            {
                WebClientBLL.UpdateOddsDataContent(dateStr);
            }
            string actual = DataCache.GetCache("OddsDetailContent").ToString();

            List<string> alist = new List<string>();
            List<string> blist = new List<string>();
            List<string> clist = new List<string>();

            //2让球盘 3标准盘 4大小盘
            string oddsStr = actual.Split('$')[2];//2=让球盘
            string[] oddsArray = oddsStr.Split(';');
            foreach (string odds in oddsArray)
            {
                string[] oddsInfo = odds.Split(',');
                if (oddsInfo[0] == scheduleID)
                {
                    alist.Add(odds);
                }
            }
            oddsStr = actual.Split('$')[3];//2=让球盘
            oddsArray = oddsStr.Split(';');
            foreach (string odds in oddsArray)
            {
                string[] oddsInfo = odds.Split(',');
                if (oddsInfo[0] == scheduleID)
                {
                    blist.Add(odds);
                }
            }

            oddsStr = actual.Split('$')[4];//2=让球盘
            oddsArray = oddsStr.Split(';');
            foreach (string odds in oddsArray)
            {
                string[] oddsInfo = odds.Split(',');
                if (oddsInfo[0] == scheduleID)
                {
                    clist.Add(odds);
                }
            }

            JArray data = new JArray();
            foreach (string companyid in companyID.Split(','))
            {
                JObject obj = JObject.Parse("{scheduleid:" + scheduleID + ",companyid:" + companyid + "}");
                foreach (string a in alist)
                {
                    string[] aArr = a.Split(',');
                    if (aArr[1] == companyid)
                    {
                        obj.Add("aaa", aArr[3]);
                        obj.Add("aab", aArr[2]);
                        obj.Add("aac", aArr[4]);
                        obj.Add("aba", aArr[6]);
                        obj.Add("abb", aArr[5]);
                        obj.Add("abc", aArr[7]);
                    }
                }
                foreach (string b in blist)
                {
                    string[] bArr = b.Split(',');
                    if (bArr[1] == companyid)
                    {
                        obj.Add("baa", bArr[2]);
                        obj.Add("bab", bArr[3]);
                        obj.Add("bac", bArr[4]);
                        obj.Add("bba", bArr[5]);
                        obj.Add("bbb", bArr[6]);
                        obj.Add("bbc", bArr[7]);
                    }
                }
                foreach (string c in clist)
                {
                    string[] cArr = c.Split(',');
                    if (cArr[1] == companyid)
                    {
                        obj.Add("caa", cArr[3]);
                        obj.Add("cab", cArr[2]);
                        obj.Add("cac", cArr[4]);
                        obj.Add("cba", cArr[6]);
                        obj.Add("cbb", cArr[5]);
                        obj.Add("cbc", cArr[7]);
                    }
                }
                data.Add(obj);
            }
            JObject result = new JObject();
            result.Add(new JProperty("totlalCount", data.Count));
            result.Add(new JProperty("data", data));
            return result.ToString();
        }





        public string GetOdds1x2(string scheduleID)
        {
            try
            {
                WebClientBLL bll = new WebClientBLL();
                string actual = bll.UpdateOdds1x2Content(scheduleID);

                JArray data = new JArray();
                //获取赔率原始数据
                Regex reg = new Regex("game\\=Array\\(\"" + "\\w[^;" + "]*;");
                Match mat = reg.Match(actual);
                if (mat != null)
                {

                    //所有公司数据
                    string source = mat.Value.Substring(12, mat.Value.Length - 10 - 4);
                    //分解出每个公司数据
                    string[] compstrs = Regex.Split(source, "\",\"", RegexOptions.IgnoreCase);
                    DataSet ds = dal.GetCompanyGroupOddsCount(scheduleID);
                    foreach (string compstr in compstrs)
                    {
                        JObject item = new JObject();
                        string[] oddsArr = compstr.Replace("\"", "").Split('|');

                        for (int i = 0; i < oddsArr.Length; i++)
                        {
                            item.Add("Odds_" + i, oddsArr[i]);
                        }
                        AddGameInfo(actual, item);
                        if (ds.Tables[0].Select("companyid='" + oddsArr[0] + "'").Length > 0)
                        {
                            item.Add("OddsCount", int.Parse(ds.Tables[0].Select("companyid='" + oddsArr[0] + "'")[0]["oddscount"].ToString()));
                        }
                        data.Add(item);
                    }
                }
                JObject result = new JObject();
                result.Add(new JProperty("totlalCount", data.Count));
                result.Add(new JProperty("data", data));
                return result.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        private void AddGameInfo(string Source, JObject item)
        {
            //获取联赛英文名称
            Regex reg = new Regex("var matchname\\=\"" + "\\w[^\"" + "]*\";");
            Match mat = reg.Match(Source);
            if (mat != null)
                item.Add("matchname", Split(mat.Value));
            //获取联赛中文名称
            reg = new Regex("var matchname_cn\\=\"" + "\\w[^\"" + "]*\";");
            mat = reg.Match(Source);
            if (mat != null)
                item.Add("matchname_cn", Split(mat.Value));
            //获取比赛开始时间
            reg = new Regex("var MatchTime\\=\"" + "\\w[^\"" + "]*\";");
            mat = reg.Match(Source);
            if (mat != null)
                item.Add("MatchTime", Split(mat.Value));
            //获取比赛编码
            reg = new Regex("ScheduleID\\=" + "\\w[^\"" + "]*;");
            mat = reg.Match(Source);
            if (mat != null)
                item.Add("ScheduleID", Split(mat.Value));

            //获取主队英文名称
            reg = new Regex("hometeam\\=\"" + "\\w[^\"" + "]*\";");
            mat = reg.Match(Source);
            if (mat != null)
                item.Add("hometeam", Split(mat.Value));
            //获取客队英文名称
            reg = new Regex("guestteam\\=\"" + "\\w[^\"" + "]*\";");
            mat = reg.Match(Source);
            if (mat != null)
                item.Add("guestteam", Split(mat.Value));
            //获取主队中文名称
            reg = new Regex("hometeam_cn\\=\"" + "\\w[^\"" + "]*\";");
            mat = reg.Match(Source);
            if (mat != null)
                item.Add("hometeam_cn", Split(mat.Value));
            //获取客队中文名称
            reg = new Regex("guestteam_cn\\=\"" + "\\w[^\"" + "]*\";");
            mat = reg.Match(Source);
            if (mat != null)
                item.Add("guestteam_cn", Split(mat.Value));
            //获取主队编码
            reg = new Regex("hometeamID\\=\"" + "\\w[^\"" + "]*\";");
            mat = reg.Match(Source);
            if (mat != null)
                item.Add("hometeamID", Split(mat.Value));
            //获取客队编码
            reg = new Regex("guestteamID\\=\"" + "\\w[^\"" + "]*\";");
            mat = reg.Match(Source);
            if (mat != null)
                item.Add("guestteamID", Split(mat.Value));
        }

        private string Split(string source)
        {
            if (source == string.Empty)
                return string.Empty;

            int start, end;
            start = source.IndexOf("\"");
            end = source.IndexOf("\"", start + 1);
            if (start == -1)
                return StringPlus.DelLastChar(source.Split('=')[1], ";");
            if (end <= start)
                return string.Empty;

            return source.Substring(start + 1, end - start - 1);
        }


        public string Analysis(string scheduleID)
        {
            WebClientBLL.UpdateAnalysisContent(scheduleID);
            string actual = DataCache.GetCache("AnalysisContent").ToString();
            string h_data = "";
            Regex reg = new Regex("var h_data\\=\\[\\[" + "\\S[^;" + "]*\\]\\];");
            Match mat = reg.Match(actual);
            if (mat != null)
                h_data = mat.Value;
            h_data = h_data.Replace("var h_data=[[", "").Replace("]];", "");

            string h2h_home = "";
            reg = new Regex("var h2h_home \\= " + "\\d+;");
            mat = reg.Match(actual);
            if (mat != null)
                h2h_home = StringPlus.DelLastChar(mat.Value.Replace("var h2h_home = ", ""), ";");

            string a_data = "";
            reg = new Regex("var a_data\\=\\[\\[" + "\\S[^;" + "]*\\]\\];");
            mat = reg.Match(actual);
            if (mat != null)
                a_data = mat.Value;
            a_data = a_data.Replace("var a_data=[[", "").Replace("]];", "");

            string h2h_away = "";
            reg = new Regex("var h2h_away \\= " + "\\d+;");
            mat = reg.Match(actual);
            if (mat != null)
                h2h_away = StringPlus.DelLastChar(mat.Value.Replace("var h2h_away = ", ""), ";");


            //分解出主队过去成绩
            string[] h_dataArr = Regex.Split(h_data, "\\],\\[", RegexOptions.IgnoreCase);
            float h_point = 100f;
            float h_goalcount = 0f;
            int h_sixpoint = 0;
            foreach (string arr in h_dataArr)
            {
                string[] match = arr.Split(',');
                if (h2h_home == match[4])
                {
                    h_point = PredictionHelper.EloHomePointsCalculation(h_point, match[17]);
                    h_goalcount += int.Parse(match[8]);
                }
                else
                {
                    h_point = PredictionHelper.EloAwayPointsCalculation(h_point, match[17]);
                    h_goalcount += int.Parse(match[9]);
                }
                h_sixpoint = PredictionHelper.SixGamePointCalculation(h_sixpoint, match[17]);
            }

            //分解出客队过去成绩
            string[] a_dataArr = Regex.Split(a_data, "\\],\\[", RegexOptions.IgnoreCase);
            float a_point = 100;
            float a_goalcount = 0f;
            int a_sixpoint = 0;
            foreach (string arr in a_dataArr)
            {
                string[] match = arr.Split(',');
                if (h2h_away == match[4])
                {
                    a_point = PredictionHelper.EloHomePointsCalculation(a_point, match[17]);
                    a_goalcount += int.Parse(match[8]);
                }
                else
                {
                    a_point = PredictionHelper.EloAwayPointsCalculation(a_point, match[17]);
                    a_goalcount += int.Parse(match[9]);
                }
                a_sixpoint = PredictionHelper.SixGamePointCalculation(a_sixpoint, match[17]);
            }

            string HomePossible = PredictionHelper.EloHomePrediction(h_point, a_point) * 100 + "%";
            string AwayPossible = PredictionHelper.EloAwayPrediction(h_point, a_point) * 100 + "%";


            //string SixGame = PredictionHelper.SixGamePrediction(h_sixpoint, a_sixpoint);
            StringBuilder sb = new StringBuilder();
            sb.Append("埃罗预测法 ：主队积分为" + h_point + ",客队积分为" + a_point + "，按照埃罗预测法主队获胜的可能性有" + HomePossible + "，客队获胜的可能性有" + AwayPossible);



            return sb.ToString();
        }


        public string GetPrediction(string scheduleID)
        {
            return this.Analysis(scheduleID);
        }


        public string GetPrediction(string scheduleID, float num)
        {
            WebClientBLL.UpdateAnalysisContent(scheduleID);
            string actual = DataCache.GetCache("AnalysisContent").ToString();
            string h_data = "";
            Regex reg = new Regex("var h_data\\=\\[\\[" + "\\S[^;" + "]*\\]\\];");
            Match mat = reg.Match(actual);
            if (mat != null)
                h_data = mat.Value;
            h_data = h_data.Replace("var h_data=[[", "").Replace("]];", "");

            string h2h_home = "";
            reg = new Regex("var h2h_home \\= " + "\\d+;");
            mat = reg.Match(actual);
            if (mat != null)
                h2h_home = StringPlus.DelLastChar(mat.Value.Replace("var h2h_home = ", ""), ";");

            string a_data = "";
            reg = new Regex("var a_data\\=\\[\\[" + "\\S[^;" + "]*\\]\\];");
            mat = reg.Match(actual);
            if (mat != null)
                a_data = mat.Value;
            a_data = a_data.Replace("var a_data=[[", "").Replace("]];", "");

            string h2h_away = "";
            reg = new Regex("var h2h_away \\= " + "\\d+;");
            mat = reg.Match(actual);
            if (mat != null)
                h2h_away = StringPlus.DelLastChar(mat.Value.Replace("var h2h_away = ", ""), ";");

            float big = 0f;
            float small = 0f;
            //分解出主队过去成绩
            string[] h_dataArr = Regex.Split(h_data, "\\],\\[", RegexOptions.IgnoreCase);
            foreach (string arr in h_dataArr)
            {
                string[] match = arr.Split(',');
                float goal = float.Parse(match[8]) + float.Parse(match[9]);
                if (goal > num)
                {
                    big++;
                }
                else if (goal < num)
                {
                    small++;
                }
            }

            //分解出客队过去成绩
            string[] a_dataArr = Regex.Split(a_data, "\\],\\[", RegexOptions.IgnoreCase);
            foreach (var arr in a_dataArr)
            {
                string[] match = arr.Split(',');
                float goal = float.Parse(match[8]) + float.Parse(match[9]);
                if (goal > num)
                {
                    big++;
                }
                else if (goal < num)
                {
                    small++;
                }
            }

            float bigprobability = big / (h_dataArr.Length + a_dataArr.Length);

            float smallprobability = small / (h_dataArr.Length + a_dataArr.Length);

            StringBuilder sb = new StringBuilder();
            sb.Append("大于" + num + "的机会有 ：" + bigprobability);

            sb.Append("<br /><br />");

            sb.Append("小于" + num + "的机会有 ：" + smallprobability);

            sb.Append("<br /><br />");

            sb.Append("走盘的机会有 ：" + (1f - bigprobability - smallprobability));
            return sb.ToString();

        }

        public string GetOddsData(string scheduleID, string dateStr, bool p)
        {
            throw new NotImplementedException();
        }
    }
}
