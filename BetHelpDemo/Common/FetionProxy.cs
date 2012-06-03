using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;

namespace SeoWebSite.Common
{
    /// <summary>
    /// 飞信代理类
    /// FetioProxy.SendMsg(mobile,msg);
    /// </summary>
    public class FetionProxy
    {
        string host = "http://f.10086.cn";
        string mobile;
        string password;
        bool isLogin = false;
        CookieCollection cookies = null;

        public FetionProxy(string mobile, string password)
        {
            this.mobile = mobile;
            this.password = password;
        }

        public bool Login()
        {
            string result = httpGet("/im5/login/login.action");
            Regex regex = new Regex(@"\d{17,18}");
            Match match = regex.Match(result);
            string postData = string.Format(
                "pass={0}&loginstatus=1&m={1}",
                password,
                mobile);
            isLogin = httpPost("/im5/login/loginHtml5.action?t=" + match.Value, postData).IndexOf("退出") != -1;
            return isLogin;
        }

        public bool logout()
        {
            httpPost("/im5/index/logoutsubmit.action", "");
            isLogin = false;
            return true;
        }

        public bool SendToSelf(string msg)
        {
            if (!isLogin)
            {
                Login();
            }
            string result = httpPost("/im5/user/sendMsgToMyselfs.action", "msg=" + msg);
            return true;
        }

        public bool SendMsg(string mobile, string msg)
        {
            if (!isLogin)
            {
                Login();
            }
            if (this.mobile == mobile)
            {
                SendToSelf(msg);
            }
            else
            {
                SendToFriend(searchFid(mobile), msg);
            }
            return true;
        }

        public bool SendToFriend(string fid, string msg)
        {
            if (!isLogin)
            {
                Login();
            }
            if (string.IsNullOrEmpty(fid))
            {
                return false;
            }
            string result = httpPost("/im5/chat/sendNewMsg.action", "touserid=" + fid + "&msg=" + msg);
            if (result.IndexOf("成功") != -1)
            {
                return true;
            }
            return false;
        }

        public string searchFid(string mobile)
        {
            if (!isLogin)
            {
                Login();
            }
            string result = httpPost("/im5/index/searchFriendsByQueryKey.action", "queryKey=" + mobile);
            string fid = mobile;
            if ((new Regex("total\\\":[1-9]+")).IsMatch(result))
            {
                Regex regex = new Regex("idContact\\\":\\d{9,}");
                Match match = regex.Match(result);
                fid = match.Value;
                fid = fid.Substring(fid.LastIndexOf(":") + 1);
            }
            return fid;
        }

        private string httpPost(string url, string postData)
        {
            byte[] data = Encoding.UTF8.GetBytes(postData);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(host + url);
            if (this.cookies != null)
            {
                req.CookieContainer = new CookieContainer();
                req.CookieContainer.Add(this.cookies);
            }
            req.Method = "POST";
            req.Referer = host + url;
            req.ContentType = "application/x-www-form-urlencoded";
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.3 Safari/535.19";
            req.Timeout = 30000;
            req.ContentLength = data.Length;
            Stream stream = req.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();
            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            if (rsp.Cookies.Count != 0)
            {
                cookies = rsp.Cookies;
            }
            StreamReader reader = new StreamReader(rsp.GetResponseStream(), Encoding.UTF8);
            string content = reader.ReadToEnd();
            return content;
        }

        private string httpGet(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(host + url);
            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            StreamReader reader = new StreamReader(rsp.GetResponseStream(), Encoding.UTF8);
            string content = reader.ReadToEnd();
            cookies = rsp.Cookies;
            return content;
        }
    }
}
