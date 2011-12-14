using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Model.NowGoal
{
    public class Game
    {
        //联赛英文名称
        public string matchname { get; set; }
        //联赛中文名称
        public string matchname_cn { get; set; }
        //比赛编码
        public string ScheduleID { get; set; }
        //主队编码
        public string hometeamID { get; set; }
        //主队英文名称
        public string hometeam {get; set;}
        //主队中文名称
        public string hometeam_cn { get; set; }
        //客队编码
        public string guestteamID { get; set; }
        //客队英文名称
        public string guestteam { get; set; }
        //客队中文名称
        public string guestteam_cn { get; set; }
        //公司赔率数据
        public List<Company> Companys {get; set;}

        //平均主胜率
        public float AvgWin { get; set; }
        //平均和率
        public float AvgEquals { get; set; }
        //平均客胜率
        public float AvgFail { get; set; }
        //网页源码
        public string Source { get; set; }


        private string Split(string source)
        {
            if (source == string.Empty)
                return string.Empty;

            int start, end;
            start = source.IndexOf("\"");
            end = source.IndexOf("\"", start + 1);
            if (end <= start)
                return string.Empty;

            return source.Substring(start + 1, end - start - 1);
        }

        public Game()
        {
            Companys = new List<Company>();
        }

        public bool IsValidSource()
        {
            if (Source == string.Empty)
                return false;

            Regex reg = new Regex(@"game\=Array\(\);");
            return !reg.IsMatch(Source);
        }

        /// <summary>
        /// 根据数据生成公司赔率对象
        /// </summary>
        /// <param name="compay">公司对象列表</param>
        /// <param name="data">数据来源字符串</param>
        private void CreateCompany(List<Company> companys, string data)
        {
            if (companys == null)
                return;

            if (data == string.Empty)
                return;

            //所有公司数据
            string source = data.Substring(11, data.Length - 10 - 3);
            //分解出每个公司数据
            string[] compstrs = Regex.Split(source, "\",\"", RegexOptions.IgnoreCase);
            foreach (string compstr in compstrs)
            {
                
                Company company = new Company();
                company.source = compstr;
                company.Calculate();
                companys.Add(company);
            }

            //计算即时平均赔率
            CalculateAVG();
        }

        /// <summary>
        /// //计算即时平均赔率
        /// </summary>
        private void CalculateAVG()
        {
            float avgWin, avgEqual, avgFail;

            avgWin = 0F;
            avgEqual = 0F;
            avgFail = 0F;
            foreach (Company company in Companys)
            {
                avgWin += company.ewin;
                avgEqual += company.eequal;
                avgFail += company.efail;
            }

            AvgWin = (avgWin / Companys.Count);
            AvgEquals = (avgEqual / Companys.Count);
            AvgFail = (avgFail / Companys.Count);
        }


        //针对网页内容进行分析
        public void AnalyseSource()
        {
            //获取联赛英文名称
            Regex reg = new Regex("var matchname\\=\"" + "\\w[^\"" + "]*\";");
            Match mat = reg.Match(Source);
            if (mat != null)
                matchname = Split(mat.Value);
            //获取联赛中文名称
            reg = new Regex("var matchname_cn\\=\"" + "\\w[^\"" + "]*\";");
            mat = reg.Match(Source);
            if (mat != null)
                matchname_cn = Split(mat.Value);
            //获取比赛编码
            reg = new Regex("ScheduleID\\=\\w*;");
            mat = reg.Match(Source);
            if (mat != null)
                ScheduleID = Split(mat.Value);
            //获取主队编码
            reg = new Regex("hometeamID\\=\"" + "\\w[^\"" + "]*\";");
            mat = reg.Match(Source);
            if (mat != null)
                hometeamID = Split(mat.Value);
            //获取主队英文名称
            reg = new Regex("hometeam\\=\"" + "\\w[^\"" + "]*\";");
            mat = reg.Match(Source);
            if (mat != null)
                hometeam = Split(mat.Value);
            //获取主队中文名称
            reg = new Regex("hometeam_cn\\=\"" + "\\w[^\"" + "]*\";");
            mat = reg.Match(Source);
            if (mat != null)
                hometeam_cn = Split(mat.Value);
            //获取客队编码
            reg = new Regex("guestteamID\\=\"" + "\\w[^\"" + "]*\";");
            mat = reg.Match(Source);
            if (mat != null)
                guestteamID = Split(mat.Value);
            //获取客队英文名称
            reg = new Regex("guestteam\\=\"" + "\\w[^\"" + "]*\";");
            mat = reg.Match(Source);
            if (mat != null)
                guestteam = Split(mat.Value);
            //获取主队中文名称
            reg = new Regex("guestteam_cn\\=\"" + "\\w[^\"" + "]*\";");
            mat = reg.Match(Source);
            if (mat != null)
                guestteam_cn = Split(mat.Value);
            //获取赔率原始数据
            reg = new Regex("game\\=Array\\(\"" + "\\w[^;" + "]*;");
            mat = reg.Match(Source);
            if (mat != null)
            {
                CreateCompany(Companys, mat.Value);
            }


        }
    }

    public class GameMatch
    {
        public string Name { get; set; }
        public string Url {get; set; }
        public bool Checked { get; set; }
    }
}
