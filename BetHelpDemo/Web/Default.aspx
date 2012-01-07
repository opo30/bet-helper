<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="SeoWebSite.Web.Default" Buffer="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>首页</title>
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <link href="http://live.nowscore.com/live.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="lib/ext/resources/css/ext-all.css" />
    <script src="lib/ext/adapter/ext/ext-base.js" type="text/javascript"></script>
    <script src="lib/ext/ext-all.js" type="text/javascript"></script>
    <link href="Lib/ext/examples/ux/css/ux-all.css" rel="stylesheet" type="text/css" />
    <script src="Lib/ext/examples/ux/ux-all.js" type="text/javascript"></script>
    <script src="Lib/ext/src/locale/ext-lang-zh_CN.js" type="text/javascript"></script>
    
    <script src="Js/ux/Notification.js" type="text/javascript"></script>
    
    <script type="text/javascript" src="http://live.nowscore.com/func.js"></script>

    <script src="Js/NowGoal/Common.js" type="text/javascript"></script>
    
    <script src="Js/BettingLine/AddBettingLine.js" type="text/javascript"></script>
    
    <script src="Js/NowGoal/CompanyKellyChart.js" type="text/javascript"></script>
    <script src="Js/NowGoal/AverageKellyLineChart.js" type="text/javascript"></script>
    <script src="Js/NowGoal/Odds1x2ChangeCharts.js" type="text/javascript"></script>
    
    <script src="Js/NowGoal/LoadLiveFile.js" type="text/javascript"></script>
    <script src="Js/NowGoal/LoadBetExperience.js" type="text/javascript"></script>
    <script src="Js/NowGoal/OddsDetailManage.js" type="text/javascript"></script>
    <script src="Js/NowGoal/Odds1x2History.js" type="text/javascript"></script>
    <script src="Js/NowGoal/Odds1x2Manage.js" type="text/javascript"></script>
    <script src="Js/live.js" type="text/javascript"></script>
    <script src="Js/NowGoal/LiveData.js" type="text/javascript"></script>
    <script src="Js/Algorithm.js" type="text/javascript"></script>
    <script src="Js/NowGoal/LoadSimilarExp.js" type="text/javascript"></script>
    <script src="Js/NowGoal/LoadSimilarTrends.js" type="text/javascript"></script>
    <script src="Js/update/matchData.js" type="text/javascript"></script>
    <script src="Js/update/oddsData.js" type="text/javascript"></script>
    <script src="js/Main.js" type="text/javascript"></script>
    <script type="text/javascript">
        var difftime = new Date() - new Date(<%= DateTime.Now.Year %>, <%= DateTime.Now.Month-1 %>, <%= DateTime.Now.Day %>, <%= DateTime.Now.Hour %>, <%= DateTime.Now.Minute %>, <%= DateTime.Now.Second %>);//客户端时间和服务器时间差
        var company = new Array(40);
        <%= initCompanyJS %>

        
    </script>
</head>
<body>
<span id="span_detail"><script language="javascript" src="http://live.nowscore.com/data/detail.js" type="text/javascript" charset="gb2312"></script></span>
<span id="span_panlu"><script language="javascript" src="http://live.nowscore.com/data/panlu.js" type="text/javascript"></script></span>
    <span id="notify"></span>
    <span id="ScoreDiv"></span>
	<span id="flashsound"></span>
    <div id="scriptScsg"><script type="text/javascript"></script></div>
    <span id="allDate"><script language="javascript" type="text/javascript" defer="defer"></script></span>
    <div id="loading-mask" style="">
    </div>
    <div id="loading">
        <div class="loading-indicator" >
            <img src="lib/ext/examples/shared/extjs/images/extanim32.gif" width="32" height="32"
                style="margin-right: 8px; float: left; vertical-align: top;" />
            - <a href="http://user.qzone.qq.com/6720941">seosite.com</a><br />
            <span id="loading-msg">正在加载...</span></div>
    </div>
</body>
</html>
