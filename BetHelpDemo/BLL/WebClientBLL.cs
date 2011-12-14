using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeoWebSite.Common;
using System.Configuration;

namespace SeoWebSite.BLL
{
    public class WebClientBLL
    {
        public static string root = ConfigurationManager.AppSettings["RootUrl"];
        public static string info = ConfigurationManager.AppSettings["InfoUrl"];
        public static string odds = ConfigurationManager.AppSettings["OddsUrl"];

        static string LiveDataURL = ConfigurationManager.AppSettings["LiveDataURL"];
        static string HistoryDataURL = ConfigurationManager.AppSettings["HistoryDataURL"];
        static string PastLiveDataURL = ConfigurationManager.AppSettings["PastLiveDataURL"];
        static string FutureLiveDataURL = ConfigurationManager.AppSettings["FutureLiveDataURL"];
        static string showgoallistURL = ConfigurationManager.AppSettings["showgoallist"];

        static string LiveOddsURL = ConfigurationManager.AppSettings["LibeOddsURL"];
        public static string OddsDetailURL = ConfigurationManager.AppSettings["OddsDetailURL"];
        public static string OddsDataURL = ConfigurationManager.AppSettings["OddsDataURL"];
        static string Odds1x2URL = ConfigurationManager.AppSettings["Odds1x2URL"];
        static string AnalysisURL = ConfigurationManager.AppSettings["AnalysisURL"];
        static string OddsHistoryURL = ConfigurationManager.AppSettings["OddsHistoryURL"];

        static string MarketsClosingSoonURL = "http://84.20.193.75/content/LoadMarketsClosingSoonAction.do";
        static string MarketsInPlayTodayURL = "http://84.20.193.75/content/LoadMarketsInPlayTodayAction.do";
        static string MarketsURL = "http://84.20.193.75/Menu.do?timeZone=PRC&region=GBR&locale=zh&brand=betfair";

        public static void UpdateLiveDataContent()
        {
            System.Net.WebClient web = new System.Net.WebClient();
            web.Encoding = Encoding.GetEncoding("utf-8");
            TimeSpan ts = DateTime.Now - DateTime.Parse("1970-1-1 ");
            string s = web.DownloadString(LiveDataURL + "?" + (long)ts.TotalMilliseconds);
            DataCache.SetCache("LiveDataContent", s);
        }

        public static void UpdateHistoryLiveDataContent(string date)
        {
            System.Net.WebClient web = new System.Net.WebClient();
            web.Encoding = Encoding.GetEncoding("utf-8");
            string url = string.Format(HistoryDataURL, date);
            string s = web.DownloadString(url + "&" + DateTime.Now);
            DataCache.SetCache("LiveDataContent", s);
        }

        public static void UpdateOddsDetailContent(string companyID)
        {
            System.Net.WebClient web = new System.Net.WebClient();
            web.Encoding = Encoding.GetEncoding("utf-8");
            string url = string.Format(root + OddsDetailURL, companyID);
            string s = web.DownloadString(url + "&_t=" + DateTime.Now);
            DataCache.SetCache("OddsDetailContent", s);
        }

        public static void UpdateOddsDataContent(string date)
        {
            System.Net.WebClient web = new System.Net.WebClient();
            web.Encoding = Encoding.GetEncoding("utf-8");
            string url = string.Format(root + OddsDataURL, date);
            string s = web.DownloadString(url + "&_t=" + DateTime.Now);
            DataCache.SetCache("OddsDetailContent", s);
        }

        public string UpdateOdds1x2Content(string scheduleID)
        {
            System.Net.WebClient web = new System.Net.WebClient();
            web.Encoding = Encoding.GetEncoding("utf-8");
            //string url = string.Format(Odds1x2, scheduleID);
            string url = string.Format(Odds1x2URL, scheduleID);
            string s = web.DownloadString(url + "?" + DateTime.Now);
            return s;
        }

        public static void UpdateAnalysisContent(string scheduleID) 
        {
            System.Net.WebClient web = new System.Net.WebClient();
            web.Encoding = Encoding.GetEncoding("utf-8");
            //string url = string.Format(Odds1x2, scheduleID);
            string url = string.Format(AnalysisURL, scheduleID);
            string s = web.DownloadString(url + "?" + DateTime.Now);
            DataCache.SetCache("AnalysisContent", s);
        }

        public string GetOddsHistoryContent(string id)
        { 
            System.Net.WebClient web = new System.Net.WebClient();
            web.Encoding = Encoding.GetEncoding("utf-8");
            //string url = string.Format(Odds1x2, scheduleID);
            string url = string.Format(OddsHistoryURL, id);
            string s = web.DownloadString(url + "&" + DateTime.Now);
            return s;
        }

        public static void UpdateMarketsClosingSoon()
        {
            System.Net.WebClient web = new System.Net.WebClient();
            web.Encoding = Encoding.GetEncoding("utf-8");
            web.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1) ; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729)");
            web.Headers.Add("Accept", "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*");
            web.Headers.Add("Accept-Language", "zh-CN");
            string s = web.DownloadString(MarketsClosingSoonURL + "?" + DateTime.Now);
            DataCache.SetCache("MarketsClosingSoon", s);
        }

        public static void UpdateMarketsInPlayToday()
        {
            System.Net.WebClient web = new System.Net.WebClient();
            web.Encoding = Encoding.GetEncoding("utf-8");
            web.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1) ; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729)");
            web.Headers.Add("Accept", "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*");
            web.Headers.Add("Accept-Language", "zh-CN");
            string s = web.DownloadString(MarketsInPlayTodayURL + "?" + DateTime.Now);
            DataCache.SetCache("MarketsInPlayToday", s);
        }

        public static void UpdateMarkets()
        {
            System.Net.WebClient web = new System.Net.WebClient();
            web.Encoding = Encoding.GetEncoding("utf-8");
            web.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1) ; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729)");
            web.Headers.Add("Accept", "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*");
            web.Headers.Add("Accept-Language", "zh-CN");
            string s = web.DownloadString(MarketsURL + "?" + DateTime.Now);
            DataCache.SetCache("MarketsURL", s);
        }


        /// <summary>
        /// 读取即时比分数据
        /// </summary>
        /// <returns></returns>
        public string LoadLiveFile()
        {
            System.Net.WebClient web = new System.Net.WebClient();
            web.Encoding = Encoding.GetEncoding("utf-8");
            TimeSpan ts = DateTime.Now - DateTime.Parse("1970-1-1 ");
            string s = web.DownloadString(LiveDataURL + "?" + (long)ts.TotalMilliseconds);
            return s;
        }

        public string LoadHistoryFile(string date)
        {
            System.Net.WebClient web = new System.Net.WebClient();
            web.Encoding = Encoding.GetEncoding("utf-8");
            string url = string.Format(HistoryDataURL, date);
            string s = web.DownloadString(url + "&" + DateTime.Now);
            return s;
        }

        public string GetRemoteHtml(string path,string[] paramArray)
        {
            System.Net.WebClient web = new System.Net.WebClient();
            web.Encoding = Encoding.GetEncoding("utf-8");
            string url = string.Format(root + path, paramArray);
            return web.DownloadString(url);
        }
    }
}
