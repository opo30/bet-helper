<%@ Application Language="C#" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        //在应用程序启动时运行的代码
        System.Timers.Timer myTimer = new System.Timers.Timer();
        myTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
        myTimer.Interval = 3000;
        myTimer.Enabled = true;
    }

    private static void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
    {
        try
        {
            SeoWebSite.BLL.NowGoalBLL bll = new SeoWebSite.BLL.NowGoalBLL();
            System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
            xmldoc.Load("http://live.nowscore.com/data/ch_goal8.xml");
            //SeoWebSite.Common.DataCache.SetCache("ch_goal8.xml", xmldoc);
            System.Xml.XmlNodeList nodelist = xmldoc.SelectNodes("c/match/m");
            foreach (System.Xml.XmlNode item in nodelist)
            {
                string scheduleid = item.InnerText.Split(',')[0];
                bll.updateOdds1x2Stat(scheduleid);
            }
        }
        catch (Exception)
        {
            
        }

    }

    void Application_End(object sender, EventArgs e)
    {
        //在应用程序关闭时运行的代码

    }

    void Application_Error(object sender, EventArgs e)
    {
        //在出现未处理的错误时运行的代码

    }

    void Session_Start(object sender, EventArgs e)
    {
        //在新会话启动时运行的代码

    }

    void Session_End(object sender, EventArgs e)
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。

    }
       
</script>
