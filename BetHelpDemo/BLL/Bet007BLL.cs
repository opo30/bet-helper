using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeoWebSite.Common;
using SeoWebSite.Common.JSON;
using System.Text.RegularExpressions;

namespace SeoWebSite.BLL
{
    public class Bet007BLL
    {
        static string LiveDataURL = System.Configuration.ConfigurationSettings.AppSettings["LiveDataURL"];


        public string GetBetLiveData()
        {
            HttpHelper target = new HttpHelper(); // TODO: 初始化为适当的值
            string url = string.Empty; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            actual = target.GetHtml("http://live.bet007.com/VbsXml/bfdata.js?" + DateTime.Now);

            string[] sArray = Regex.Split(actual, "\r\n", RegexOptions.IgnoreCase);

            int matchcount = 0;
            int sclasscount = 0;
            string matchdate = "";
            List<string> matchs = new List<string>();
            List<string> sclasss = new List<string>();
            foreach (string i in sArray)
            {
                if (i.StartsWith("A[") && i.EndsWith("\".split('^');"))
                {
                    matchs.Add(i.Split('"')[1]);
                }
                else if (i.StartsWith("B[") && i.EndsWith("\".split('^');"))
	            {
            		sclasss.Add(i.Split('"')[1]);
	            }
                else if (i.StartsWith("var matchcount="))
                {
                    matchcount = int.Parse(StringPlus.DelLastChar(i.Replace("var matchcount=", ""),";"));
                }
                else if (i.StartsWith("var sclasscount="))
                {
                    matchcount = int.Parse(StringPlus.DelLastChar(i.Replace("var sclasscount=", ""),";"));
                }
                else if (i.StartsWith("var matchdate=\""))
	            {
                    matchdate = StringPlus.DelLastChar(i.Replace("var matchdate=\"", ""),"\"");
	            }
            }

            LiveDataJSONHelper json = new LiveDataJSONHelper();
            
            if (matchs.Count == matchcount)
            {
                foreach (string paramStr in matchs)
                {
                    string[] paramArr = paramStr.Split('^');
                    json.success = true;
                    for (int i = 1; i <= paramArr.Length; i++)
                    {
                        json.AddMatch("match_"+i, paramArr[i]);
                        json.MatchOk();
                    }
                }
                json.mtotlalCount = matchcount;
            }
            if (sclasss.Count == sclasscount)
            {
                foreach (string paramStr in sclasss)
                {
                    string[] paramArr = paramStr.Split('^');
                    json.success = true;
                    for (int i = 1; i <= paramArr.Length; i++)
                    {
                        json.AddClass("class_"+i, paramArr[i]);
                        json.ClassOk();
                    }
                }
                json.ctotlalCount = sclasscount;
            }
            json.singleInfo = matchdate;
            return json.ToString();
        }
    }
}
