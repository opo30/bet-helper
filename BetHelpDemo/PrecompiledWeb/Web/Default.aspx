<%@ page language="C#" autoeventwireup="true" inherits="SeoWebSite.Web.Default, App_Web_default.aspx.cdcab7d2" buffer="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>首页</title>
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <link href="http://live.nowodds.com/live.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="lib/ext/resources/css/ext-all.css" />
    <link href="Lib/ext/examples/ux/css/ux-all.css" rel="stylesheet" type="text/css" />
</head>
<body>

    <div id="loading-mask" style=""></div>
<div id="loading">
    <div class="loading-indicator"><img src="Lib/ext/examples/shared/extjs/images/extanim32.gif" width="32" height="32" style="margin-right:8px;float:left;vertical-align:top;"/>Bet Helper - <a href="http://www.yuuzle.com">yuuzle.com</a><br /><span id="loading-msg">Loading styles and images...</span></div>
    </div>

    <script type="text/javascript">        document.getElementById('loading-msg').innerHTML = '正在加载 Core API...';</script>
    <script src="lib/ext/adapter/ext/ext-base.js" type="text/javascript"></script>
    <script type="text/javascript">        document.getElementById('loading-msg').innerHTML = '正在加载 UI Components...';</script>
    <script src="lib/ext/ext-all.js" type="text/javascript"></script>
    <script src="Lib/ext/examples/ux/ux-all.js" type="text/javascript"></script>
    <script src="Lib/ext/src/locale/ext-lang-zh_CN.js" type="text/javascript"></script>
    <script type="text/javascript">            document.getElementById('loading-msg').innerHTML = '正在初始化...';</script>
     <script src="Js/ux/Notification.js" type="text/javascript"></script>
    
    <script type="text/javascript" src="http://live.nowodds.com/func.js"></script>
    <script type="text/javascript" src="http://live.nowodds.com/matchScore.js"></script>

    <%--<script src="Js/NowGoal/Common.js" type="text/javascript"></script>--%>
    
    <script src="Js/BettingLine/AddBettingLine.js" type="text/javascript"></script>
    
    <script src="Js/NowGoal/CompanyKellyChart.js" type="text/javascript"></script>
    <script src="Js/NowGoal/AverageKellyLineChart.js" type="text/javascript"></script>
    <script src="Js/NowGoal/Odds1x2ChangeCharts.js" type="text/javascript"></script>
    
    <script src="Js/NowGoal/LoadLiveFile.js" type="text/javascript"></script>
    <script src="Js/NowGoal/LoadBetExperience.js" type="text/javascript"></script>
    <script src="Js/NowGoal/OddsDetailManage.js" type="text/javascript"></script>
    <script src="Js/NowGoal/Odds1x2History.js" type="text/javascript"></script>
    <script src="Js/NowGoal/Odds1x2Mail.js" type="text/javascript"></script>
    <script src="Js/NowGoal/EuropeOdds.js" type="text/javascript"></script>
    <script src="Js/NowGoal/EuropeOdds_Schedule.js" type="text/javascript"></script>
    <script src="Js/NowGoal/ScheduleAnalysis.js" type="text/javascript"></script>
    <script src="Js/live.js" type="text/javascript"></script>
    <script src="Js/NowGoal/LiveData.js" type="text/javascript"></script>
    <script src="Js/Algorithm.js" type="text/javascript"></script>
    <script src="Js/NowGoal/LoadSimilarExp.js" type="text/javascript"></script>
    <script src="Js/NowGoal/LoadSimilarTrends.js" type="text/javascript"></script>
    <script src="Js/update/matchData.js" type="text/javascript"></script>
    <script src="Js/update/oddsData.js" type="text/javascript"></script>
    <script src="js/Main.js" type="text/javascript"></script>
    <script type="text/javascript">
        
        var company = new Array(40);
        <%= initCompanyJS %>
    </script>

    <div id="live">
        <div id="main">
  <div class="toptool">
  <div class="tg1"><span class="secl"><a id="li_league" href="javascript:setOrderby('league');">按联赛排序</a><a id="li_time" style="display:none;" href="javascript:setOrderby('time');">按时间排序</a></span></div>
  <div class="tg2" onclick="ShowAllMatch()" style="cursor:pointer; width:85px;">隐藏 <span class="td_scoreR" id="hiddencount">0</span> 场</div>
   <ul class="tg3">
  <li class="yu1"><a href="2in1.aspx" class="selected"><span>比分+指数</span></a></li>
  <li class="yu1"><a href="index.aspx"><span>纯比分</span></a></li>
  <li class="yu2"><a href="javascript:SetLevel(0);" title="显示当天足彩比赛"  id="liZC" onmousemove="MM_showHideLayers('divMatchType3','','show')" onmouseout="MM_showHideLayers('divMatchType3','','hide')"><span>足彩</span></a></li>
  <li style="margin-left:10px;" class="yu1"><a href="javascript:SetMatchType(0);" id="MatchType0" title="显示所有的比赛"><span>全部赛事</span></a></li>
  <li class="yu2"><a href="javascript:SetMatchType(1);" id="MatchType1" title="只显示重要的比赛，精选赛事"><span>精简</span></a></li>
  <li class="yu1"><a href="javascript:SetMatchType(2);" id="MatchType2" title="显示有赔率的比赛"><span>开盘赛事</span></a></li>
  </ul>
  <div class="tg3" style="margin-left:5px;margin-right:0px;">
	<input type="checkbox" name="yp" id="yp"  onClick="CheckFunction('yp')"/>亚赔
	<input type="checkbox" name="op" id="op"  onClick="CheckFunction('op')"/>欧赔
	<input type="checkbox" name="dx" id="dx"  onClick="CheckFunction('dx')"/>大小
  </div>
  <ul class="tg3s">
  <li class="sh2"><a href="javascript:MM_showHideLayers('DivFunction','','show','DivLeague','','hide')">设置</a></li>
  <li style="margin-left:5px" class="sh2"><a href="javascript:SetLanguage(1);" id="Language1"><span>繁体</span></a></li>
  <li class="sh2"><a href="javascript:SetLanguage(0);" id="Language0"><span>简体</span></a></li>
  <li class="sh2"><a href="http://www.nowgoal.com" id="Language2" target="_blank"><span>En</span></a></li>
  </ul>
   <div style="clear:both"></div>
  </div>
  
  
<div id="left">
    <div style="height:33px; padding-top:3px"><a href="javascript:MM_showHideLayers('DivLeague','','show')"><img src="http://live.nowodds.com/images/game_s.gif" /></a></div>
    <div style="height:36px;"><a href="javascript:" onmouseover="MM_showHideLayers('DivCountry','','show')" onmouseout="MM_showHideLayers('DivCountry','','hidden')"><img src="http://live.nowodds.com/images/country_s.gif" /></a></div>
</div>
<div id="middle">
  
    <div id="Layer1" style="position:absolute;height:1px; z-index:5;">
 <div id="DivLeague"><div class="sotit"><h1>赛事选择</h1><span class="cc"><a style="cursor:pointer;" onClick="MM_showHideLayers('DivLeague','','hide')"></a></span></div>
		<div class="rbl">
			<input type="radio" name="selectType" id="rb0" value="0" onclick="ShowAllMatch()" checked="checked" /><label for="rb0">所有比赛</label>
			  <input type="radio" name="selectType" id="rb4" value="4" onclick="ShowMatchByMatchState(4)" /><label for="rb4">滚球赛事</label>
			  <input type="radio" name="selectType" id="rb3" value="3" onclick="ShowMatchByMatchState(3)" /><label for="rb3">未开场　</label>			  
			  <input type="radio" name="selectType" id="rb2" value="2" onclick="ShowMatchByMatchState(2)" /><label for="rb2">已完场　</label>
			  <input type="radio" name="selectType" id="rb1" value="1" onclick="ShowMatchByMatchState(1)" /><label for="rb1">进行中</label>
		</div>
		<div id="myleague"></div>
		<p class="bts">
		<script language="javascript" type="text/javascript">
		    document.write('<input type="button" name="btn_all" id="btn_all" value="全部" style="cursor:pointer;" onclick="SetMatchType(0)"/>');
		</script>
			<input type="button" name="button2" id="button2" value="一级赛事" style="cursor:pointer;" onclick="SetMatchType(1)"/>
			<input type="button" name="button2" id="button4" value="全选" style="cursor:pointer;" onclick="ShowAllMatch()"/>
			<input type="button" name="button3" id="button3" value="反选" style="cursor:pointer;" onclick="SelectOtherLeague()" />
			<input type="button" name="button" id="button" value="关闭"  style="cursor:pointer;" onclick="MM_showHideLayers('DivLeague','','hide')"/>
		</p>
  </div>  
  
  <div id="DivCountry" onmouseover="MM_showHideLayers('DivCountry','','show')" onmouseout="MM_showHideLayers('DivCountry','','hidden')">
    <div id="countryList" class="gamelist"></div>
		<p class="bts">
			<input type="button" name="button2" id="button7" value="全选" style="cursor:pointer;" onclick="ShowAllMatch()"/>
			<input type="button" name="button3" id="button8" value="反选" style="cursor:pointer;" onclick="SelectOtherLeague()" />
		</p>
  </div> 
 		
  <div id="DivFunction" style="position:absolute;z-index:6;"> 
	<div class="sotit"><h1>功能选项</h1><span class="cc"><a style="cursor:pointer;" onClick="MM_showHideLayers('DivFunction','','hide')"></a></span></div>
	<table width="100%" border="0" align="center" cellpadding="2" cellspacing="0">
			<tr> 
			  <td style="line-height:25px;">
		 <div class="fontse" style="line-height:20px;"><div style="float:left;">字体： </div><a href="javascript:changeFontSize('table_live',12)">A</a><a  href="javascript:changeFontSize('table_live',14)" class="e12">A</a><a href="javascript:changeFontSize('table_live',16)"  class="e14">A</a></div>
		 <div style="clear:both"></div>
					<p><input type="checkbox" name="rank" id="rank" onclick="CheckTeamRank()"> 球队排名</p>				
					<p><input type="checkbox" name="explain" id="explain" checked="checked" onclick="CheckExplain()">文字备注信息</p>
					<p><input type="checkbox" name="redcard" id="redcard" checked="checked" onclick="CheckFunction('redcard')">红牌颜色提示</p>
					<p><input type="checkbox" name="detail" id="detail" checked="checked"  onclick="CheckFunction('detail')"/>移到比分显示入球</p>
					<p><input type="checkbox" name="vs" id="vs" checked="checked"  onclick="CheckFunction('vs')"/>移到半场显示往绩</p>
					<!--<p><input type="checkbox" name="odds" id="odds"  checked onClick="CheckFunction('odds')"/>移到走地显示指数</p>-->
					<p><input type="checkbox" name="oddsSound" id="oddsSound"  onclick="CheckFunction('oddsSound')"/>指数变化声音提示</p>
								
				<input type="checkbox" name="soundCheck" id="soundCheck" checked="checked" onclick="CheckSound()"/>进球声<select name="sound" id="sound" onchange="CheckSound()" />
				<option value="0">默认</option>
				<option value="1">警报</option>
				<option value="2">贝司</option>
				<option value="3">嘟嘟</option>
				</select><br>				
				<input type="checkbox" name="windowCheck" ID="windowCheck" checked="checked" onclick="CheckWindow()" />提示窗<select name="winLocation" id="winLocation" onchange="CheckWindow()">
				<option value="0">正上方</option>
				<option value="1">正下方</option>
				<option value="2">正左方</option>
				<option value="3">正右方</option>
				<option value="4">左上角</option>
				<option value="5">右上角</option>
				<option value="6">左下角</option>
				<option value="7">右下角</option>
				</select><br>
				<p class="bts"><input type="button" name="button2" id="button1" value="打印" style="cursor:pointer;" onclick="javascript:window.open('/score_print.aspx','','')";/><input type="button" name="button" id="button5" value="关闭"  style="cursor:pointer;" onclick="MM_showHideLayers('DivFunction','','hide')"/></p>
			  </td></tr>
		  </table>
  </div>
  <div id="divMatchType3" onmouseout="MM_showHideLayers('divMatchType3','','hide')" onmousemove="MM_showHideLayers('divMatchType3','','show')">
		<div><a href="javascript:SetLevel(0);">所有赛事</a></div>
		<div><a href="javascript:SetLevel(1);">胜负彩</a></div>
		<div><a href="javascript:SetLevel(2);">北京单场</a></div>
  </div>
</div>
<div>
	<ul class="Companys" style="margin-left:25px;">
      <li><a href="javascript:SetCompany(1);" id="company1"><span>澳门</span></a></li>
      <li><a href="javascript:SetCompany(4);" id="company4"><span>立博</span></a></li>
      <li><a href="javascript:SetCompany(3);" id="company3"><span>ＳＢ</span></a></li>
      <li><a href="javascript:SetCompany(24);" id="company24"><span>沙巴</span></a></li>
      <li><a href="javascript:SetCompany(31);" id="company31"><span>利记</span></a></li>
      <li><a href="javascript:SetCompany(17);" id="company17"><span>明陞</span></a></li>
      <li><a href="javascript:SetCompany(23);" id="company23"><span>金宝博</span></a></li>
      <li><a href="javascript:SetCompany(12);" id="company12"><span>易胜博</span></a></li>
      <li><a href="javascript:SetCompany(8);" id="company8"><span>Bet365</span></a></li>
    </ul>  
    <div style="clear:both"></div>
    </div>
    <span id="OddsHistory"></span>
	<span id="notify"></span>
	<span id="ScoreDiv"></span>
	<span id="flashsound"></span>
	<ul class="main2_tool">
	  <li class="m_on" id="LeagueKind1"><a href="javascript:showLeagueList(1)">今日赛事</a></li>
	  <li class="m_off" id="LeagueKind2"><a href="javascript:showLeagueList(2)">热门赛事</a></li>
	  <li class="m_off" id="LeagueKind3"><a href="javascript:showLeagueList(3)">热门杯赛</a></li>
	  <li class="m_off"><a href="odds/history.aspx" target="_blank">指数复查</a></li>
	  </ul>
	<div id="main2">
	  <div id="leagueList"></div>
<table width='100%' id="hotLeague" style="display:none;" border='0' align='center' cellpadding='0' cellspacing='1' bgcolor='#ffffff' class='gre' style='text-align:center; margin-bottom:5px;'><tr height=20>
<td bgcolor="#FF3333"><a href="javascript:ChangeJS(36,1)">英超</a></td>
<td bgcolor="#cc3300"><a href="javascript:ChangeJS(37,1)">英冠</a></td>
<td bgcolor="#0088FF"><a href="javascript:ChangeJS(34,1)">意甲</a></td>
<td bgcolor="#006633"><a href="javascript:ChangeJS(31,1)">西甲</a></td>
<td bgcolor="#990099"><a href="javascript:ChangeJS(8,1)">德甲</a></td>
<td bgcolor="#DDDD00"><a href="javascript:ChangeJS(4,1)">巴西甲</a></td>
<td bgcolor="#663333"><a href="javascript:ChangeJS(11,1)">法甲</a></td>
<td bgcolor="#008888"><a href="javascript:ChangeJS(23,1)">葡超</a></td>
<td bgcolor="#57A87B"><a href="javascript:ChangeJS(29,1)">苏超</a></td>
<td bgcolor="#FF6699"><a href="javascript:ChangeJS(16,1)">荷甲</a></td></tr>
<tr height=20>
<td bgcolor="#660033"><a href="javascript:ChangeJS(21,1)">美职业</a></td>
<td bgcolor="#009900"><a href="javascript:ChangeJS(25,1)">日职联</a></td>
<td bgcolor="#FC9B0A"><a href="javascript:ChangeJS(5,1)">比甲</a></td>
<td bgcolor="#004488"><a href="javascript:ChangeJS(26,1)">瑞典超</a></td>
<td bgcolor="#137AAC"><a href="javascript:ChangeJS(13,1)">芬超</a></td>
<td bgcolor="#0066FF"><a href="javascript:ChangeJS(60,1)">中超</a></td>
<td bgcolor="#666666"><a href="javascript:ChangeJS(22,1)">挪超</a></td>
<td bgcolor="#2f3fd2"><a href="javascript:ChangeJS(3,1)">奥甲</a></td>
<td bgcolor="#1ba578"><a href="javascript:ChangeJS(27,1)">瑞士超</a></td>
<td bgcolor="#006699"><a href="javascript:ChangeJS(10,1)">俄超</a></td>
</tr></table>

<table width='100%' id="hotCup" border='0' align='center' cellpadding='0' cellspacing='1' bgcolor='#ffffff' class='gre' style='display:none;text-align:center; margin-bottom:5px;'><tr style="height:20;">
<td bgcolor="#660000"><a href="javascript:ChangeJS(75,2)">世界杯</a></td>
<td bgcolor="#660033"><a href="javascript:ChangeJS(650,2)">欧洲预选</a></td>
<td bgcolor="#f36229"><a href="javascript:ChangeJS(652,2)">南美预选</a></td>
<td bgcolor=#448e08><a href="javascript:ChangeJS(653,2)">北美预选</a></td>
<td bgcolor=#49a63d><a href="javascript:ChangeJS(648,2)">亚洲预选</a></td>
<td bgcolor=#2f4e07><a href="javascript:ChangeJS(651,2)">非洲预选</a></td>
<td bgcolor=#d15023><a href="javascript:ChangeJS(649,2)">大洋预选</a></td>
<td bgcolor=#009933><a href="javascript:ChangeJS(88,2)">洲际杯</a></td>
<td bgcolor=#660000><a href="javascript:ChangeJS(67,2)">欧国杯</a></td>
<td bgcolor=#F75000><a href="javascript:ChangeJS(103,2)">欧冠杯</a></td></tr>
<tr height=20>
<td bgcolor=#6F00DD><a href="javascript:ChangeJS(113,2)">欧霸杯</a></td>
<td bgcolor=#990044><a href="javascript:ChangeJS(224,2)">美洲杯</a></td>
<td bgcolor=#37BE5A><a href="javascript:ChangeJS(95,2)">亚洲杯</a></td>
<td bgcolor=#0000DB><a href="javascript:ChangeJS(192,2)">亚冠杯</a></td>
<td bgcolor=#567576><a href="javascript:ChangeJS(93,2)">非洲杯</a></td>
<td bgcolor=#660000><a href="javascript:ChangeJS(90,2)">英足总杯</a></td>
<td bgcolor=#808080><a href="javascript:ChangeJS(84,2)">英联杯</a></td>
<td bgcolor=#3c3cff><a href="javascript:ChangeJS(83,2)">意杯</a></td>
<td bgcolor=#006666><a href="javascript:ChangeJS(81,2)">西杯</a></td>
<td bgcolor=#a00800><a href="javascript:ChangeJS(51,2)">德国杯</a></td>
</tr></table>
	  
	  <div id="divScsg" style="display:none;"> 
	  <div class="tdsolid" style=" margin:0px; line-height:inherit;" id="SubSelectDiv">        
    </div>
	  <table border="0" cellspacing="0" cellpadding="0" id="Table1">
                <tr>
                    <td id="showRound">
                        
                    </td>
                </tr>
            </table>
            <div class="tdsolid">
                <table width="680" border="0" cellpadding="0" cellspacing="1"   bgcolor="#C6C6C6" class="tdlink" id="Table3">
                <tr>
                        <td width="18" rowspan="2" bgcolor="#456da8"><div align="center" class="STYLE4"><strong>轮次</strong></div></td>
                        <td width="37" rowspan="2" bgcolor="#456da8"><div align="center" class="STYLE4"><strong>时间</strong></div></td>
                        <td width="106" rowspan="2" bgcolor="#456da8"><div align="center" class="STYLE4"><strong>主队</strong></div></td>
                        <td width="60" rowspan="2" bgcolor="#456da8"><div align="center" class="STYLE4"><strong>比分</strong></div></td>
                        <td width="106" rowspan="2" bgcolor="#456da8"><div align="center" class="STYLE4"><strong>客队</strong></div></td>
                        <td height="18" colspan="2" bgcolor="#456da8"><div align="center" class="STYLE4"><strong>让球</strong></div></td>
                        <td colspan="2" bgcolor="#456da8"><div align="center" class="STYLE4"><strong>大小</strong></div></td>

                        <td width="65" rowspan="2" bgcolor="#456da8"><div align="center" class="STYLE4"><strong>资料</strong></div></td>
                        <td width="27" rowspan="2" bgcolor="#456da8"><div align="center" class="STYLE4"><strong>半场</strong></div></td>
                    </tr><tr>
                        <td width="41" height="18" bgcolor="#83acc2"><div align="center" class="STYLE4">全场</div></td>
                        <td width="41" bgcolor="#83acc2"><div align="center" class="STYLE4">半场</div></td>
                        <td width="39" bgcolor="#9d8dad"><div align="center" class="STYLE4">全场</div></td>

                        <td width="39" bgcolor="#9d8dad"><div align="center" class="STYLE4">半场</div></td>
                    </tr>
                    </table>



<table width="680" border="1" cellpadding="2" cellspacing="0"   bgcolor="#ffffff" class="tdlink" id="Table2">
                    <tr bgcolor='#f5bcbc'><td height='21' align='center'  style=" color:#ffffff; font-weight:bold; width:550px;" bgcolor='#456da8' id="scoreTitle">总积分榜</td><td   height='21' align='center'  bgcolor='#456da8' >
                    <select name="scoreRound" id="scoreRound" onchange="scoreRoundChange(this)"><option value="0">总积分榜</option></select></td></tr></table>
                
            </div>
<div style='WIDTH: 680px; FLOAT: left'>           
<div class="nav_select">
<ul>
  <li id="menu1" class="nav_selected" onclick="SelectScore(1)" style=" cursor:pointer">总积分榜</li>
  <li id="menu2" class="nav_unselected" onclick='SelectScore(2)'  style=" cursor:pointer">主场积分榜</li>

  <li id='menu3' class='nav_unselected' onclick='SelectScore(3)'  style=" cursor:pointer">客场积分榜</li>
  <li id='menu4' class='nav_unselected' onclick='SelectScore(4)' style=" cursor:pointer">半场总积分榜</li>
  <li id='menu5' class='nav_unselected' onclick='SelectScore(5)' style=" cursor:pointer">半场主场积分榜</li>

  <li id='menu6' class='nav_unselected' onclick='SelectScore(6)' style=" cursor:pointer">半场客场积分榜</li></ul></div></div>
  <div class='nav_select_bottom'></div>
<div id='tableId' class='data' style="clear:both;"></div>
  </div>
	  <div id="divScore"  style="display:none;"><div class="tdsolid" id="SubSelectDiv2"></div>
<div class="tdsolid" id="showRound2"></div>
<div class="tdsolid" id="ScoreGroupDiv">
<table border="0" cellspacing="1" cellpadding="1" bgcolor="#cccccc" style="border-collapse:inherit" width="680" align="center" id="ScoreGroupTab"><tbody><tr><td bgcolor="#d1e2f5" width="32"><div align="center">排名</div></td>  <td bgcolor="#d1e2f5" width="254"><div align="center">球队</div></td>  <td bgcolor="#d1e2f5" width="44" align="center">总</td>  <td bgColor="#d1e2f5" width="44" align="center">胜</td>  <td bgcolor="#d1e2f5" width="44" align="center">平</td>  <td bgcolor="#d1e2f5" width="44" align="center">负</td>  <td bgcolor="#d1e2f5" width="44" align="center">得</td>  <td bgcolor="#d1e2f5" width="44" align="center">失</td>  <td bgcolor="#d1e2f5" width="44" align="center">净</td><td bgcolor="#d1e2f5" width="44" align="center">积分</td></tr></tbody></table>
</div>

            
            <div class="tdsolid" id="mainTableDiv">
                <table width="680" border="0" cellpadding="0" cellspacing="1"   bgcolor="#C6C6C6" class="tdlink" id="Table4"><tr>
                        <td width="48" rowspan="2" bgcolor="#456da8"><div align="center" class="STYLE4"><strong>轮次</strong></div></td>
                        <td width="37" rowspan="2" bgcolor="#456da8"><div align="center" class="STYLE4"><strong>时间</strong></div></td>
                        <td width="96" rowspan="2" bgcolor="#456da8"><div align="center" class="STYLE4"><strong>主队</strong></div></td>
                        <td width="60" rowspan="2" bgcolor="#456da8"><div align="center" class="STYLE4"><strong>比分</strong></div></td>
                        <td width="96" rowspan="2" bgcolor="#456da8"><div align="center" class="STYLE4"><strong>客队</strong></div></td>
                        <td height="18" colspan="2" bgcolor="#456da8"><div align="center" class="STYLE4"><strong>让球</strong></div></td>
                        <td colspan="2" bgcolor="#456da8"><div align="center" class="STYLE4"><strong>大小</strong></div></td>

                        <td width="55" rowspan="2" bgcolor="#456da8"><div align="center" class="STYLE4"><strong>资料</strong></div></td>
                        <td width="27" rowspan="2" bgcolor="#456da8"><div align="center" class="STYLE4"><strong>半场</strong></div></td>
                    </tr><tr>
                        <td width="41" height="18" bgcolor="#83acc2"><div align="center" class="STYLE4">全场</div></td>
                        <td width="41" bgcolor="#83acc2"><div align="center" class="STYLE4">半场</div></td>
                        <td width="39" bgcolor="#9d8dad"><div align="center" class="STYLE4">全场</div></td>

                        <td width="39" bgcolor="#9d8dad"><div align="center" class="STYLE4">半场</div></td>
                    </tr></table>
                
            </div></div>
	  <div id="scriptScsg"><script type="text/javascript"></script></div>
	  <div id="scriptScore"><script type="text/javascript"></script></div>
	</div>
  </div>
  <br style="clear:both">
</div>

<div id="winScore" style='position:absolute; z-index:8;top:100px;left:100px;'></div>
<div id="videoInfo" style='position:absolute; z-index:8;top:100px;left:100px;' onmouseover="MM_showHideLayers('videoInfo','','show')" onmouseout="MM_showHideLayers('videoInfo','','hidden')"></div>
<span id="allDate"><script language="javascript" type="text/javascript" defer="defer"></script></span>
<span id="videoData"><script language="javascript" src="http://www.310tv.com/js/programs.js" type="text/javascript"></script></span>

<script language="javascript" type="text/javascript">
    var loaded = 0, LoadTime = 0, nofityTimer, matchType = -1, runtimeTimer, getoddsxmlTimer, LoadLiveFileTimer;
    var difftime = new Date() - new Date(<%= DateTime.Now.Year %>, <%= DateTime.Now.Month-1 %>, <%= DateTime.Now.Day %>, <%= DateTime.Now.Hour %>, <%= DateTime.Now.Minute %>, <%= DateTime.Now.Second %>);
    var loadDetailFileTime = new Date();
    var loadVideoFileTime = new Date();
    var oldOddsXML = "";
    var lastUpdateTime, oldUpdateTime = "", oldXML = "";
    var lastUpdateFileTime = 0;
    var hiddenID = "_";
    var oXmlHttp = zXmlHttp.createRequest();
    var oddsHttp = zXmlHttp.createRequest();
    var needSound = false;
    var orderby = "time";
    var isliving = false;//即时更新
    var issendmail = false;//邮件通知
    var issendsms = false;//短信通知

    function ShowBf() {
        loaded = 0;
        hiddenID = getCookie("HiddenMatchID");
        if (hiddenID == null) hiddenID = "_";
        if (orderby == "league") {
            MakeTableOrderByLeague();
            document.getElementById("li_league").style.display = "none";
            document.getElementById("li_time").style.display = "";
        }
        else {
            MakeTable();
            document.getElementById("li_league").style.display = "";
            document.getElementById("li_time").style.display = "none";
        }
        showodds(false);
        window.clearTimeout(runtimeTimer);
        window.clearTimeout(getoddsxmlTimer);
        runtimeTimer = window.setTimeout("setMatchTime()", 1000);
        if (Config.rank == 1) ShowTeamRank();
        if (Config.explain == 0) ShowExplain();
        loadMask.hide();//document.getElementById("loading").style.display = "none";
        getoddsxmlTimer = window.setTimeout("getoddsxml()", 3000);
        window.setTimeout("showLeagueList(1)", 500);
    }


    function MakeTable() {
        var state, bg = "", line = -1, hh = 0;
        var H_redcard, G_redcard, H_yellow, G_yellow;
        var ArrayHiddenID = hiddenID.split("_");
        var oldHiddenID = true;
        var html = new Array();
        html.push("<table id='table_live' width=100% bgcolor=#C6C6C6 align=center cellspacing=1 border=0 cellpadding=0><tr class=ki1 align=center>");
        html.push("<td  width=3% bgcolor='#ff9933' height=20><font color=white>选</font></td><td  width=8%><font color=white>" + matchdate + "</font></td><td  width=5%><font color=white>时间</font></td><td  width=5%><font color=white>状态</font></td><td  width=16%><font color=white>主队</font></td><td  width=5%><font color=white>比分</font></td><td  width=16%><font color=white>客队</font></td><td  width=5%><font color=white>半场</font></td><td  width=15%><font color=white>数据</font></td><td width=19% colspan=3><font color=white>指数</font></td><td width=3%>走</td><td width=3%>总数</td><td width=6%>胜</td><td width=6%>平</td><td width=6%>负</td></tr>");

        oddsHttp.open("get", "Data/NowGoal/GetRemoteFile.aspx?f=xml&path=data/goal" + Config.companyID + ".xml?" + Date.parse(new Date()), false);
        oddsHttp.send(null);
        if (document.all)
            idList = oddsHttp.responseXML.documentElement.childNodes[1].text;
        else
            idList = oddsHttp.responseXML.documentElement.childNodes[1].textContent;

        if (hiddenID != "_") {
            for (var i = 0; i < matchcount; i++) {
                if (ArrayHiddenID[1] == A[i][0] || ArrayHiddenID[ArrayHiddenID.length - 2] == A[i][0]) {
                    oldHiddenID = false;
                    break;
                }
            }
        }
        if (oldHiddenID) hiddenID = "_";
        for (var i = 0; i < matchcount; i++) {
            try {
                if (Config.matchType == 1 && B[A[i][1]][5] == 0 || Config.matchType == 2 && idList.indexOf(A[i][0]) < 0) {
                    A[i][1] = -1;
                    continue;
                }
                if (matchType >= 0) {
                    A[i][30] = parseInt(A[i][30]);
                    if (!(matchType == 0 && A[i][30] > 0 || matchType == 1 && (A[i][30] == 1 || A[i][30] === 3) || matchType == 2 && (A[i][30] == 2 || A[i][30] == 3))) {
                        A[i][1] = -1;
                        continue;
                    }
                }

                line++;
                B[A[i][1]][8]++;
                if (hiddenID == "_" || hiddenID.indexOf("_" + A[i][0] + "_") != -1) {
                    B[A[i][1]][10]++;
                    C[A[i][31]][3]++;
                }
                for (var j = 0; j < C.length; j++) {
                    if (B[A[i][1]][9] == C[j][0]) {
                        C[j][2]++;
                        break;
                    }
                }
                state = parseInt(A[i][12]);
                switch (state) {
                    case 0:
                        if (A[i][23] == "1") match_score = "阵容"; else match_score = "-";
                        match_half = "-";
                        break;
                    case 1:
                        match_score = A[i][13] + "-" + A[i][14];
                        match_half = "-";
                        break;
                    case -11:
                    case -14:
                        match_score = "";
                        match_half = "";
                        break;
                    default:
                        match_score = A[i][13] + "-" + A[i][14];
                        if (A[i][15] == null) A[i][15] = "";
                        if (A[i][16] == null) A[i][16] = "";
                        match_half = A[i][15] + "-" + A[i][16];
                        break;
                }
                if (A[i][17] != "0") H_redcard = "<img src='images/redcard" + A[i][17] + ".gif'>"; else H_redcard = "";
                if (A[i][18] != "0") G_redcard = "<img src='images/redcard" + A[i][18] + ".gif'>"; else G_redcard = "";
                if (A[i][19] != "0") H_yellow = "<img src='images/yellow" + A[i][19] + ".gif'>"; else H_yellow = "";
                if (A[i][20] != "0") G_yellow = "<img src='images/yellow" + A[i][20] + ".gif'>"; else G_yellow = "";

                if (bg != "ts1") bg = "ts1"; else bg = "ts2";

                classx = "none";
                if (hiddenID != "_") {
                    for (var j = 1; j < ArrayHiddenID.length - 1; j++) {
                        if (ArrayHiddenID[j] == A[i][0]) {
                            classx = "";
                            break;
                        }
                    }
                }
                else
                    classx = "";
                if (classx == "none") hh++;

                html.push("<tr align=center id='tr1_" + A[i][0] + "' class='" + bg + "' index='" + i + "' odds='' style='display:" + classx + "'>");
                html.push("<td height=18><input type=checkbox checked onclick='hidematch(" + i + ");return false;' class='inp'></td>");

                if (B[A[i][1]][7] != "")
                    html.push("<td bgcolor=" + B[A[i][1]][4] + " style='color:white;'><a href='" + (parseInt(Config.language) < 2 ? "http://info.nowscore.com/" + B[A[i][1]][7] + "&lang=" + Config.language : "http://info.nowgoal.com/football/en/" + B[A[i][1]][7]) + "' target=_blank><font color=white>" + B[A[i][1]][1 + Config.language] + "</font></a></td>");
                else
                    html.push("<td bgcolor=" + B[A[i][1]][4] + " style='color:white;'>" + B[A[i][1]][1 + Config.language] + "</td>");

                html.push("<td align=center id='mt_" + A[i][0] + "'>" + A[i][10] + "</td>");

                if (state == "-1")
                    classx2 = "td_scoreR";
                else
                    classx2 = "td_score";

                html.push("<td align=center id='time_" + A[i][0] + "' class='fortime'>" + state_ch[state + 14].split(",")[Config.language] + "</td>");
                html.push("<td class=a1><span id=horder_" + A[i][0] + "></span><a id='yellow1_" + A[i][0] + "'>" + H_yellow + "</a><a id='redcard1_" + A[i][0] + "'>" + H_redcard + "</a><a id='team1_" + A[i][0] + "' href='javascript:' onclick='TeamPanlu_10(" + A[i][0] + ")' title='" + A[i][21] + "'>" + A[i][4 + Config.language] + "</a></td>");
                html.push("<td onclick='showgoallist(" + A[i][0] + ")' class='" + classx2 + "' onmouseover='showdetail(" + i + ",event)' onmouseout='hiddendetail()'>" + match_score + "</td>");
                html.push("<td class=a2><a id='team2_" + A[i][0] + "' href='javascript:' onclick='TeamPanlu_10(" + A[i][0] + ")'  title='" + A[i][22] + "'>" + A[i][7 + Config.language] + "</a><a id='redcard2_" + A[i][0] + "'>" + G_redcard + "</a><a id='yellow2_" + A[i][0] + "'>" + G_yellow + "</a><span id=gorder_" + A[i][0] + "></span></td>");
                html.push("<td class=td_half onmouseover='showpaulu(" + i + ",event)' onmouseout='hiddendetail()'>" + match_half + "</td>");
                html.push("<td class=fr style='text-align:left'><a href='javascript:' onclick=analysis(" + A[i][0] + ") title='数据分析' style='padding-left:2px;'>析</a><a style='cursor:pointer;padding-left:4px;' href=javascript: onclick=\"AsianOdds(" + A[i][0] + ");return false\" title='11家指数'>亚</a><a href='javascript:EuropeOdds(" + A[i][0] + ")' style='padding-left:4px;' title='百家欧赔'>欧</a>");
                if (A[i][29] == "1") html.push("<a href='javascript:' onclick='scheduleAnalysis("+A[i][0]+");return false;' style='color:red;padding-left:4px;' target=_blank>现</a>");
                else html.push("<a href='javascript:' onclick='scheduleAnalysis("+A[i][0]+");return false;' style='color:blue;padding-left:4px;' target=_blank>现</a>");
                //if (A[i][28] == "1") html.push("<a href='odds/recommend.aspx?id=" + A[i][0] + "' style='color:red;padding-left:4px;' target=_blank>荐</a>");
                 html.push("<a href='javascript:' onclick='Odds1x2Mail1("+A[i][0]+");return false;' style='color:red;padding-left:4px;' target=_blank>荐</a>");
                 html.push("<a href='javascript:' onclick='Odds1x2Mail("+A[i][0]+");return false;' style='color:red;padding-left:4px;' target=_blank>邮</a>");
                if (typeof (V) != "undefined" && typeof (V[A[i][0]]) != "undefined") {
                    if (V[A[i][0]][1].indexOf("外部链接") != -1) {
                        var urls = V[A[i][0]][1].split('|');
                        if (urls.length > 1)
                            html.push("<a href='" + urls[1] + "' target='_blank' style='color:red;padding-left:4px;'  onmouseover='showvideo(" + i + ",event)' onmouseout=MM_showHideLayers('videoInfo','','hidden')>直</a>");
                    }
                    else
                        html.push("<a href='http://www.310tv.com/" + V[A[i][0]][4] + "/" + V[A[i][0]][0] + "_" + V[A[i][0]][2] + "_" + V[A[i][0]][3] + ".html' target='_blank' style='color:red;padding-left:4px;'  onmouseover='showvideo(" + i + ",event)' onmouseout=MM_showHideLayers('videoInfo','','hidden')>直</a>");
                }
                html.push(A[i][25]);//增加初盘显示

                html.push("</td><td class=oddstd>&nbsp;</td>");
                html.push("<td class=oddstd onclick='oddsDetail(" + A[i][0] + "," + Config.companyID + " )' style='cursor:pointer;'>&nbsp;</td>");
                html.push("<td class=oddstd>&nbsp;</td>");

                html.push("<td>&nbsp;</td>");
                html.push("<td>&nbsp;</td>");
                html.push("<td>&nbsp;</td>");
                html.push("<td>&nbsp;</td>");

                html.push("<td>&nbsp;</td></tr>");

                if (A[i][27] + A[i][32] == "" || classx == "none") classx = "none"; else classx = "";
                html.push("<tr id='tr2_" + A[i][0] + "' style='display:" + classx + "' bgcolor='#ffffff'><td colspan=13 align=center height=18 style='color:green;padding-right:122px;' id='other_" + A[i][0] + "'>" + showExplain(A[i][32], A[i][4 + Config.language], A[i][7 + Config.language]) + (A[i][32] != "" && A[i][27] != "" ? "<br>" + A[i][27] : A[i][27] != "" ? A[i][27] : "") + "</td></tr>");

//                if (line / 2 < adinfo1.length && line % 2 == 1)
//                    html.push("<tr id=tr_ad" + (line + 1) / 2 + "><td colspan=13 bgcolor=#ffffff align=center height=18>广告：<a href='" + adinfo1[(line - 1) / 2] + "' target=_blank style='color:red'><b>" + adinfo2[(line - 1) / 2] + "</b></a></td></tr>");

            } catch (e) { }
        }
        html.push("</table>")
        document.getElementById("ScoreDiv").innerHTML = html.join("");
        document.getElementById("hiddencount").innerHTML = hh;
        //联赛/杯赛名列表
        makeMyLeague();
        //国家列表
        makeMyCountry();
    }
    function MakeTableOrderByLeague() {
        var state, bg = "", line = -1, hh = 0;
        var leagueIndex = -1, oldLeagueIndex = -1;
        var H_redcard, G_redcard, H_yellow, G_yellow;
        var ArrayHiddenID = hiddenID.split("_");
        var oldHiddenID = true;

        var html = new Array();
        html.push("<table id='table_live' width=100% bgcolor=#C6C6C6 align=center cellspacing=1 border=0 cellpadding=0><tr class=ki1 align=center>");
        html.push("<td  width=3% bgcolor='#ff9933' height=20><font color=white>选</font></td><td  width=5%><font color=white>时间</font></td><td  width=5%><font color=white>状态</font></td><td  width=16%><font color=white>主队</font></td><td  width=5%><font color=white>比分</font></td><td  width=16%><font color=white>客队</font></td><td  width=5%><font color=white>半场</font></td><td  width=15%><font color=white>数据</font></td><td width=19% colspan=3><font color=white>指数</font></td><td width=3%>走</td></tr>");

        oddsHttp.open("get", "Data/NowGoal/GetRemoteFile.aspx?f=xml&path=data/goal" + Config.companyID + ".xml?" + Date.parse(new Date()), false);
        oddsHttp.send(null);
        if (document.all)
            idList = oddsHttp.responseXML.documentElement.childNodes[1].text;
        else
            idList = oddsHttp.responseXML.documentElement.childNodes[1].textContent;

        if (hiddenID != "_") {
            for (var i = 0; i < matchcount; i++) {
                if (ArrayHiddenID[1] == A[i][0] || ArrayHiddenID[ArrayHiddenID.length - 2] == A[i][0]) {
                    oldHiddenID = false;
                    break;
                }
            }
        }

        if (oldHiddenID) hiddenID = "_";
        for (var i = 0; i < matchcount; i++) {
            try {
                leagueIndex = A[i][1];
                if (Config.matchType == 1 && B[A[i][1]][5] == 0 || Config.matchType == 2 && idList.indexOf(A[i][0]) < 0) {
                    A[i][1] = -1;
                    continue;
                }
                if (matchType >= 0) {
                    A[i][30] = parseInt(A[i][30]);
                    if (!(matchType == 0 && A[i][30] > 0 || matchType == 1 && (A[i][30] == 1 || A[i][30] === 3) || matchType == 2 && (A[i][30] == 2 || A[i][30] == 3))) {
                        A[i][1] = -1;
                        continue;
                    }
                }

                line++;
                B[A[i][1]][8]++;
                if (hiddenID == "_" || hiddenID.indexOf("_" + A[i][0] + "_") != -1) {
                    B[A[i][1]][10]++;
                    C[A[i][31]][3]++;
                }
                for (var j = 0; j < C.length; j++) {
                    if (B[A[i][1]][9] == C[j][0]) {
                        C[j][2]++;
                        break;
                    }
                }
                state = parseInt(A[i][12]);
                switch (state) {
                    case 0:
                        if (A[i][23] == "1") match_score = "阵容"; else match_score = "-";
                        match_half = "-";
                        break;
                    case 1:
                        match_score = A[i][13] + "-" + A[i][14];
                        match_half = "-";
                        break;
                    case -11:
                    case -14:
                        match_score = "";
                        match_half = "";
                        break;
                    default:
                        match_score = A[i][13] + "-" + A[i][14];
                        if (A[i][15] == null) A[i][15] = "";
                        if (A[i][16] == null) A[i][16] = "";
                        match_half = A[i][15] + "-" + A[i][16];
                        break;
                }
                if (A[i][17] != "0") H_redcard = "<img src='images/redcard" + A[i][17] + ".gif'>"; else H_redcard = "";
                if (A[i][18] != "0") G_redcard = "<img src='images/redcard" + A[i][18] + ".gif'>"; else G_redcard = "";
                if (A[i][19] != "0") H_yellow = "<img src='images/yellow" + A[i][19] + ".gif'>"; else H_yellow = "";
                if (A[i][20] != "0") G_yellow = "<img src='images/yellow" + A[i][20] + ".gif'>"; else G_yellow = "";

                if (bg != "ts1") bg = "ts1"; else bg = "ts2";

                classx = "none";
                if (hiddenID != "_") {
                    for (var j = 1; j < ArrayHiddenID.length - 1; j++) {
                        if (ArrayHiddenID[j] == A[i][0]) {
                            classx = "";
                            break;
                        }
                    }
                }
                else
                    classx = "";
                if (classx == "none") hh++;
                if (oldLeagueIndex != leagueIndex) {
                    oldLeagueIndex = leagueIndex;
                    html.push("<tr class='Leaguestitle' id='tr_" + leagueIndex + "'><td colspan='12'><span class='l1'>");
                    if (B[A[i][1]][7] != "")
                        html.push("<a href='http://info.nowscore.com/" + B[A[i][1]][7] + "' target=_blank>");
                    else
                        html.push("<a href='javascript:'>");
                    html.push(B[A[i][1]][1 + Config.language] + "</a> </span><span class='l2'>");
                    html.push("<a href='javascript:HiddenLeague(" + leagueIndex + ",false)' id='collapse" + leagueIndex + "'><img src='http://live.nowodds.com/images/collapse.gif'/></a><a href='javascript:HiddenLeague(" + leagueIndex + ",true)' id='expand" + leagueIndex + "' style='display:none;'><img src='images/expand.gif'/></a><a href='javascript:CloseLeague(" + leagueIndex + ") '><img src='images/closes.gif'/></a></span></td></tr>");
                }
                html.push("<tr align=center id='tr1_" + A[i][0] + "' class='" + bg + "' index='" + i + "' odds='' style='display:" + classx + "'>");
                html.push("<td height=18><input type=checkbox checked onclick='hidematch(" + i + ");return false;' class='inp'></td>");
                html.push("<td width=0 style='display:none;'></td>");
                html.push("<td align=center id='mt_" + A[i][0] + "'>" + A[i][10] + "</td>");

                if (state == "-1")
                    classx2 = "td_scoreR";
                else
                    classx2 = "td_score";
                
                html.push("<td align=center id='time_" + A[i][0] + "' class='fortime'>" + state_ch[state + 14].split(",")[Config.language] + "</td>");
                html.push("<td class=a1><span id=horder_" + A[i][0] + "></span><a id='yellow1_" + A[i][0] + "'>" + H_yellow + "</a><a id='redcard1_" + A[i][0] + "'>" + H_redcard + "</a><a id='team1_" + A[i][0] + "' href='javascript:' onclick='TeamPanlu_10(" + A[i][0] + ")' title='" + A[i][21] + "'>" + A[i][4 + Config.language] + "</a></td>");
                html.push("<td onclick='showgoallist(" + A[i][0] + ")' class='" + classx2 + "' onmouseover='showdetail(" + i + ",event)' onmouseout='hiddendetail()'>" + match_score + "</td>");
                html.push("<td class=a2><a id='team2_" + A[i][0] + "' href='javascript:' onclick='TeamPanlu_10(" + A[i][0] + ")'  title='" + A[i][22] + "'>" + A[i][7 + Config.language] + "</a><a id='redcard2_" + A[i][0] + "'>" + G_redcard + "</a><a id='yellow2_" + A[i][0] + "'>" + G_yellow + "</a><span id=gorder_" + A[i][0] + "></span></td>");
                html.push("<td class=td_half onmouseover='showpaulu(" + i + ",event)' onmouseout='hiddendetail()'>" + match_half + "</td>");
                html.push("<td class=fr style='text-align:left'><a href='javascript:' onclick=analysis(" + A[i][0] + ") title='数据分析' style='padding-left:2px;'>析</a><a style='cursor:pointer;padding-left:4px;' href=javascript: onclick=\"AsianOdds(" + A[i][0] + ");return false\" title='11家指数'>亚</a><a href='javascript:EuropeOdds(" + A[i][0] + ")' style='padding-left:4px;' title='百家欧赔'>欧</a>");
                if (A[i][29] == "1") html.push("<a href='data/matchInfo.aspx?id=" + A[i][0] + "' style='color:red;padding-left:4px;' target=_blank>现</a>");
                else html.push("<a href='data/matchInfo.aspx?id=" + A[i][0] + "' style='color:blue;padding-left:4px;' target=_blank>现</a>");
                if (A[i][28] == "1") html.push("<a href='odds/recommend.aspx?id=" + A[i][0] + "' style='color:red;padding-left:4px;' target=_blank>荐</a>");
                if (typeof (V) != "undefined" && typeof (V[A[i][0]]) != "undefined") {
                    if (V[A[i][0]][1].indexOf("外部链接") != -1) {
                        var urls = V[A[i][0]][1].split('|');
                        if (urls.length > 1)
                            html.push("<a href='" + urls[1] + "' target='_blank' style='color:red;padding-left:4px;'  onmouseover='showvideo(" + i + ",event)' onmouseout=MM_showHideLayers('videoInfo','','hidden')>直</a>");
                    }
                    else
                        html.push("<a href='http://www.310tv.com/" + V[A[i][0]][4] + "/" + V[A[i][0]][0] + "_" + V[A[i][0]][2] + "_" + V[A[i][0]][3] + ".html' target='_blank' style='color:red;padding-left:4px;'  onmouseover='showvideo(" + i + ",event)' onmouseout=MM_showHideLayers('videoInfo','','hidden')>直</a>");

                }

                html.push("</td><td class=oddstd>&nbsp;</td>");
                html.push("<td class=oddstd onclick='oddsDetail(" + A[i][0] + "," + Config.companyID + " )' style='cursor:pointer;'>&nbsp;</td>");
                html.push("<td class=oddstd>&nbsp;</td>");
                html.push("<td>&nbsp;</td></tr>");

                if (A[i][27] + A[i][32] == "" || classx == "none") classx = "none"; else classx = "";
                html.push("<tr id='tr2_" + A[i][0] + "' style='display:" + classx + "' bgcolor='#ffffff'><td colspan=12 align=center height=18 style='color:green;padding-right:122px;' id='other_" + A[i][0] + "'>" + showExplain(A[i][32], A[i][4 + Config.language], A[i][7 + Config.language]) + (A[i][32] != "" && A[i][27] != "" ? "<br>" + A[i][27] : A[i][27] != "" ? A[i][27] : "") + "</td></tr>");

//                if (line / 2 < adinfo1.length && line % 2 == 1)
//                    html.push("<tr id=tr_ad" + (line + 1) / 2 + "><td colspan=12 bgcolor=#ffffff align=center height=18>广告：<a href='" + adinfo1[(line - 1) / 2] + "' target=_blank style='color:red'><b>" + adinfo2[(line - 1) / 2] + "</b></a></td></tr>");

            } catch (e) { }
        }
        html.push("</table>")
        document.getElementById("ScoreDiv").innerHTML = html.join("");
        document.getElementById("hiddencount").innerHTML = hh;
        //联赛/杯赛名列表
        makeMyLeague();
        //国家列表
        makeMyCountry();
    }
    //重新计算选中状态的赛程
    function countCheckNum() {
        for (var i = 0; i < sclasscount; i++) {
            B[i][10] = 0;
        }

        for (var i = 0; i < countrycount; i++) {
            C[i][3] = 0;
        }

        for (var i = 0; i < matchcount; i++) {
            if (hiddenID == "_" || hiddenID.indexOf("_" + A[i][0] + "_") != -1) {
                if (A[i][1] != -1) {
                    B[A[i][1]][10]++;
                    C[A[i][31]][3]++;
                }
            }
        }
    }
    function CloseLeague(i) {
        document.getElementById("checkboxleague_" + i).checked = false;
        CheckLeague(i);
    }

    function HiddenLeague(i, b) {
        document.getElementById("checkboxleague_" + i).checked = b;
        if (b) {
            document.getElementById("expand" + i).style.display = "none";
            document.getElementById("collapse" + i).style.display = "";
        }
        else {
            document.getElementById("expand" + i).style.display = "";
            document.getElementById("collapse" + i).style.display = "none";
        }
        CheckLeague(i);
        document.getElementById("tr_" + i).style.display = "";
    }
    function makeMyLeague() {
        //联赛/杯赛名列表
        var leaguehtml = new Array();
        leaguehtml.push("<ul id='checkboxleague'>");
        for (var i = 0; i < sclasscount; i++) {
            if (B[i][8] > 0) {
                if (B[i][10] > 0) {
                    leaguehtml.push("<li style='width:115px;'><div style='padding-top:6px;#padding-top:1px;_padding-top:2px;float:left;'><input onclick='CheckLeague(" + i + ")' checked type=checkbox id='checkboxleague_" + i + "' value=" + i + "></div><div><span style='background-color:" + B[i][4] + ";'>&nbsp;&nbsp;&nbsp;</span><label style='cursor:pointer;padding-left:2px;' for='checkboxleague_" + i + "'>");
                    if (orderby == "league") {
                        document.getElementById("tr_" + i).style.display = "";
                        document.getElementById("expand" + i).style.display = "none";
                        document.getElementById("collapse" + i).style.display = "";
                    }
                }
                else {
                    leaguehtml.push("<li style='width:115px;'><div style='padding-top:6px;#padding-top:1px;_padding-top:2px;float:left;'><input onclick='CheckLeague(" + i + ")' type=checkbox id='checkboxleague_" + i + "' value=" + i + "></div><div><span style='background-color:" + B[i][4] + ";'>&nbsp;&nbsp;&nbsp;</span>&nbsp;<label style='cursor:pointer;padding-left:2px;' for='checkboxleague_" + i + "'>");
                    if (orderby == "league") {
                        document.getElementById("tr_" + i).style.display = "none";
                        document.getElementById("expand" + i).style.display = "none";
                        document.getElementById("collapse" + i).style.display = "none";
                    }
                }

                if (B[i][5] == "1")
                    leaguehtml.push("<font color=red>" + B[i][1 + Config.language] + "[" + B[i][8] + "]</font></label></div></li>");
                else
                    leaguehtml.push(B[i][1 + Config.language] + "<font color=#990000>[" + B[i][8] + "]</font></label></div></li>");
            }
        }
        leaguehtml.push("</ul>");
        document.getElementById("myleague").innerHTML = leaguehtml.join("");
    }

    function makeMyCountry() {
        //国家列表
        var country = new Array();
        country.push("<ul id='checkboxcountry'>");
        for (var i = 0; i < C.length; i++) {
            if (C[i][2] > 0) {
                if (C[i][3] > 0)
                    country.push("<li><input onclick='CheckCountry(" + C[i][0] + ")' checked type=checkbox id='checkboxcountry_" + C[i][0] + "' value=" + C[i][0] + "><label style='cursor:pointer' for='checkboxcountry_" + C[i][0] + "'>" + C[i][1] + "</label></li>");
                else
                    country.push("<li><input onclick='CheckCountry(" + C[i][0] + ")' type=checkbox id='checkboxcountry_" + C[i][0] + "' value=" + C[i][0] + "><label style='cursor:pointer' for='checkboxcountry_" + C[i][0] + "'>" + C[i][1] + "</label></li>");
            }
        }
        country.push("</ul>");
        document.getElementById("countryList").innerHTML = country.join("");
    }
    function showodds(needSleep) //是否需显示一场之后暂停一会
    {
        try {
            var root = oddsHttp.responseXML.documentElement.childNodes[0];
            if (root.childNodes.length == 0) return;
            var D = new Array();
            var odds, old = new Array();
            needSound = false;
            showodds2(root, 0, needSleep);
        } catch (e) { }
    }
    function showodds2(root, i, needSleep) {
        try {
            var D = new Array();
            var odds, old = new Array();

            if (document.all)
                odds = root.childNodes[i].text; //id,oddsid,goal,home,away,oddsid,hw,st,gw,oddsid,up,goal,down
            else
                odds = root.childNodes[i].textContent;
            D = odds.split(",");

            tr = document.getElementById("tr1_" + D[0]);
            if (tr != null) {
                old = tr.attributes["odds"].value.split(",");
                if (old.length == 14 && old != odds) {
                    for (var j = 3; j < 13; j++) {
                        if (old[j] != "") {
                            if (D[j] > old[j]) D[j] = "<span class=up>" + D[j] + "</span>";
                            else if (D[j] < old[j]) D[j] = "<span class=down>" + D[j] + "</span>";
                        }
                        if (j == 4) j++;
                        if (j == 8) j = j + 2;
                    }
                    window.setTimeout("restoreOddsColor(" + D[0] + ")", 30000);
                    if (Config.oddsSound == 1) {
                        if (tr.style.display != "none") needSound = true;
                    }
                }
                if (old.length == 14 && old != odds && old[2] != "") {
                    if (D[2] > old[2]) D[2] = "<span class=up>" + Goal2GoalCn(D[2]) + "</span>";
                    else if (D[2] < old[2]) D[2] = "<span class=down>" + Goal2GoalCn(D[2]) + "</span>";
                    else D[2] = Goal2GoalCn(D[2]);
                }
                else D[2] = Goal2GoalCn(D[2]);

                if (old.length == 14 && old != odds && old[10] != "") {
                    if (D[10] > old[10]) D[10] = "<span class=up>" + Goal2GoalCn2(D[10]) + "</span>";
                    else if (D[10] < old[10]) D[10] = "<span class=down>" + Goal2GoalCn2(D[10]) + "</span>";
                    else D[10] = Goal2GoalCn2(D[10]);
                }
                else D[10] = Goal2GoalCn2(D[10]);

                var tmp = "";
                if (Config.yp == 1) tmp += "<p class=odds1>" + D[3] + "</p>";
                if (Config.op == 1) tmp += "<p class=odds2>" + D[6] + "</p>";
                if (Config.dx == 1) tmp += "<p class=odds3>" + D[11] + "</p>";
                tr.cells[9].innerHTML = tmp;

                tmp = "";
                if (Config.yp == 1) tmp += "<p class=odds1>" + D[2] + "</p>";
                if (Config.op == 1) tmp += "<p class=odds2>" + D[7] + "</p>";
                if (Config.dx == 1) tmp += "<p class=odds3>" + D[10] + "</p>";
                tr.cells[10].innerHTML = tmp;

                tmp = "";
                if (Config.yp == 1) tmp += "<p class=odds1>" + D[4] + "</p>";
                if (Config.op == 1) tmp += "<p class=odds2>" + D[8] + "</p>";
                if (Config.dx == 1) tmp += "<p class=odds3>" + D[12] + "</p>";
                tr.cells[11].innerHTML = tmp;

                tmp = "";
                if (D[13] == "1") tmp = "<a href='Odds/runningDetail.aspx?scheduleID=" + D[0] + "' target='_blank'><img src='http://live.nowodds.com/images/t3.gif' height=10 width=10 title='有走地赛事'></a>";
                if (D[13] == "2") tmp = "<a href='Odds/runningDetail.aspx?scheduleID=" + D[0] + "' target='_blank'><img src='http://live.nowodds.com/images/t32.gif' height=10 width=10 title='正在走地'></a>";
                tr.cells[12].innerHTML = tmp;

                tr.attributes["odds"].value = odds;
            }
        } catch (e) { }

        i++;
        if (i < root.childNodes.length) {
            if (needSleep) setTimeout(function () { showodds2(root, i, needSleep); }, 1);
            else showodds2(root, i, needSleep);
        }
        else
            if (needSound) document.getElementById("flashsound").innerHTML = flash_sound[4];
    }

    function getoddsxml() {
        if (isliving) {
            oddsHttp.open("get", "Data/NowGoal/GetRemoteFile.aspx?f=xml&path=data/ch_goal" + Config.companyID + ".xml?" + Date.parse(new Date()), true);
            oddsHttp.onreadystatechange = oddsrefresh;
            oddsHttp.send(null);
        }
        getoddsxmlTimer = window.setTimeout("getoddsxml()", 3000);
    }
    function oddsrefresh() {
        if (oddsHttp.readyState != 4 || (oddsHttp.status != 200 && oddsHttp.status != 0)) return;
        if (oldOddsXML == oddsHttp.responseText) {
            return;
        }
        oldOddsXML = oddsHttp.responseText;
        showodds(true);
    }
    function PlayeddsSound(matchid) {
        if (Config.oddsSound == 1) {
            if (document.getElementById("tr1_" + matchid).style.display != "none") {
                document.getElementById("flashsound").innerHTML = flash_sound[4];
            }
        }
        window.setTimeout("restoreOddsColor(" + matchid + ")", 30000);
    }
    function restoreOddsColor(matchid) {
        var tr = document.getElementById("tr1_" + matchid);
        if (tr == null) return;
        tr.cells[9].innerHTML = tr.cells[9].innerHTML.toLowerCase().replace(/<span class=up>/g, "").replace(/<span class=down>/g, "").replace(/<\/span>/g, "");
        tr.cells[10].innerHTML = tr.cells[10].innerHTML.toLowerCase().replace(/<span class=up>/g, "").replace(/<span class=down>/g, "").replace(/<\/span>/g, "");
        tr.cells[11].innerHTML = tr.cells[11].innerHTML.toLowerCase().replace(/<span class=up>/g, "").replace(/<span class=down>/g, "").replace(/<\/span>/g, "");
    }

    function gettime() {
        try {
            LoadTime = (LoadTime + 1) % 60;
            if (isliving) {
                if (LoadTime == 0)
                    oXmlHttp.open("get", "Data/NowGoal/GetRemoteFile.aspx?f=xml&path=data/change2.xml?" + Date.parse(new Date()), true);
                else
                    oXmlHttp.open("get", "Data/NowGoal/GetRemoteFile.aspx?f=xml&path=data/change.xml?" + Date.parse(new Date()), true);
                oXmlHttp.onreadystatechange = refresh;
                oXmlHttp.send(null);
            }
        }
        catch (e) { }
        window.setTimeout("gettime()", 2000);
    }

    function refresh() {
        try {
            if (oXmlHttp.readyState != 4 || (oXmlHttp.status != 200 && oXmlHttp.status != 0)) return;
            lastUpdateTime = new Date();
            var root = oXmlHttp.responseXML.documentElement;
            if (oldXML == "" || oldXML == oXmlHttp.responseText) {
                oldXML = oXmlHttp.responseText;
                return;
            }
            oldXML = oXmlHttp.responseText;
            if (root.attributes[0].value != "0") {
                window.setTimeout("LoadLiveFile()", Math.floor(20000 * Math.random()));
                return;
            }

            var D = new Array();
            var matchindex, score1change, score2change, scorechange;
            var goTime, hometeam, guestteam, sclassname, score1, score2, tr;
            var matchNum = 0;
            var winStr = "";
            var notify = document.getElementById("notify").innerHTML = "";

            for (var i = 0; i < root.childNodes.length; i++) {
                if (document.all)
                    D = root.childNodes[i].text.split("^"); //0:ID,1:state,2:score1,3:score2,4:half1,5:half2,6:card1,7:card2,8:time1,9:time2,10:explain,11:lineup		
                else
                    D = root.childNodes[i].textContent.split("^");
                D[1] = parseInt(D[1]);

                tr = document.getElementById("tr1_" + D[0]);
                if (tr == null) continue;

                matchindex = tr.attributes["index"].value;
                score1change = false;
                if (A[matchindex][13] != D[2]) {
                    A[matchindex][13] = D[2];
                    score1change = true;
                    tr.cells[4].style.backgroundColor = "#bbbb22";
                }
                score2change = false;
                if (A[matchindex][14] != D[3]) {
                    A[matchindex][14] = D[3];
                    score2change = true;
                    tr.cells[6].style.backgroundColor = "#bbbb22";
                }
                scorechange = score1change || score2change;

                //附加说明改时变了'
                D[10] = D[10].toLowerCase().replace(/<a.*<\/a>/g, "");
                if (A[matchindex][27] != D[10] || A[matchindex][32] != D[15]) {
                    A[matchindex][27] = D[10];
                    A[matchindex][32] = D[15];
                    var ex = showExplain(D[15], A[matchindex][4 + Config.language], A[matchindex][7 + Config.language]) + D[10];
                    document.getElementById("other_" + D[0]).innerHTML = ex;
                    if (D[10] + D[15] == "")
                        document.getElementById("tr2_" + D[0]).style.display = "none";
                    else
                        document.getElementById("tr2_" + D[0]).style.display = "";
                }

                if (Config.redcard == 1 && (D[6] != A[matchindex][17] || D[7] != A[matchindex][18]) && tr.style.display != "none") {
                    hometeam = A[matchindex][4 + Config.language].replace("<font color=#880000>(中)</font>", " 中").substring(0, 7);
                    guestteam = A[matchindex][7 + Config.language].substring(0, 7);
                    sclassname = B[A[matchindex][1]][1 + Config.language];

                    if (D[6] != A[matchindex][17]) {
                        hometeam = "<font color=red>" + hometeam + "</font>";
                    }
                    if (D[7] != A[matchindex][18]) {
                        guestteam = "<font color=red>" + guestteam + "</font>";
                    }
                    winStr += "<tr bgcolor=#ffffff height=34 align=center class=line><td><font color=red>红牌</font></td><td> " + tr.cells[3].innerHTML + "</td><td><b>" + hometeam + "</b> " + (D[6] == "0" ? "" : "<img src=images/redcard" + D[6] + ".gif border='0'>") + "</td><td  colspan=2> vs</td><td><b>" + guestteam + "</b> " + (D[7] == "0" ? "" : "<img src=images/redcard" + D[7] + ".gif border='0'>") + "</td></tr>";
                    matchNum = matchNum + 1;
                } //redcardChange


                //红牌变化了
                if (D[6] != A[matchindex][17]) {
                    A[matchindex][17] = D[6];
                    if (D[6] == "0")
                        document.getElementById("redcard1_" + D[0]).innerHTML = "";
                    else
                        document.getElementById("redcard1_" + D[0]).innerHTML = "<img src=images/redcard" + D[6] + ".gif border='0'> ";
                    if (Config.redcard == 1) tr.cells[4].style.backgroundColor = "#ff8888";
                    window.setTimeout("timecolors(" + D[0] + "," + matchindex + ")", 12000);
                }
                if (D[7] != A[matchindex][18]) {
                    A[matchindex][18] = D[7];
                    if (D[7] == "0")
                        document.getElementById("redcard2_" + D[0]).innerHTML = "";
                    else
                        document.getElementById("redcard2_" + D[0]).innerHTML = "<img src=images/redcard" + D[7] + ".gif border='0'> ";
                    if (Config.redcard == 1) tr.cells[6].style.backgroundColor = "#ff8888";
                    window.setTimeout("timecolors(" + D[0] + "," + matchindex + ")", 12000);
                }
                //黄牌变化了
                if (D[12] != A[matchindex][19]) {
                    A[matchindex][19] = D[12];
                    if (D[12] == "0")
                        document.getElementById("yellow1_" + D[0]).innerHTML = "";
                    else
                        document.getElementById("yellow1_" + D[0]).innerHTML = "<img src=images/yellow" + D[12] + ".gif border='0'> ";
                }
                if (D[13] != A[matchindex][20]) {
                    A[matchindex][20] = D[13];
                    if (D[13] == "0")
                        document.getElementById("yellow2_" + D[0]).innerHTML = "";
                    else
                        document.getElementById("yellow2_" + D[0]).innerHTML = "<img src=images/yellow" + D[13] + ".gif border='0'> ";
                }

                //开赛
                if (A[matchindex][11] != D[8]) tr.cells[2].innerHTML = D[8];
                A[matchindex][10] = D[8];
                A[matchindex][11] = D[9];

                //半场比分
                A[matchindex][15] = D[4];
                A[matchindex][16] = D[5];

                //状态
                if (A[matchindex][12] != D[1]) {
                    A[matchindex][12] = D[1];
                    switch (A[matchindex][12]) {
                        case 0:
                            tr.cells[3].innerHTML = "";
                            break;
                        case 1:
                            var t = A[matchindex][11].split(",");
                            var t2 = new Date(t[0], t[1], t[2], t[3], t[4], t[5]);
                            goTime = Math.floor((new Date() - t2 - difftime) / 60000);
                            if (goTime > 45) goTime = "45+"
                            if (goTime < 1) goTime = "1"
                            if (goTime < 10) Odds1x2Mail(A[matchindex][0]);
                            tr.cells[3].innerHTML = goTime + "<img src='images/in.gif'>";
                            break;
                        case 2:
                        case 4:
                            tr.cells[3].innerHTML = state_ch[parseInt(D[1]) + 14].split(",")[Config.language];
                            break;
                        case 3:
                            var t = A[matchindex][11].split(",");
                            var t2 = new Date(t[0], t[1], t[2], t[3], t[4], t[5]);
                            goTime = Math.floor((new Date() - t2 - difftime) / 60000) + 46;
                            if (goTime > 90) goTime = "90+";
                            if (goTime < 46) goTime = "46";
                            tr.cells[3].innerHTML = goTime + "<img src='images/in.gif'>";
                            break;
                        case -1:
                            tr.cells[3].innerHTML = state_ch[parseInt(D[1]) + 14].split(",")[Config.language];
                            tr.cells[5].style.color = "red";
                            window.setTimeout("MoveToBottom(" + D[0] + ")", 25000);
                            break;
                        default:
                            tr.cells[3].innerHTML = state_ch[parseInt(D[1]) + 14].split(",")[Config.language];
                            MoveToBottom(D[0]);
                            break;
                    }
                }

                //score
                switch (A[matchindex][12]) {
                    case 0:
                        if (D[11] == "1")
                            tr.cells[5].innerHTML = "阵容";
                        else
                            tr.cells[5].innerHTML = "-";
                        break;
                    case 1:
                        tr.cells[5].innerHTML = A[matchindex][13] + "-" + A[matchindex][14];
                        break;
                    case -11:
                    case -14:
                        tr.cells[5].innerHTML = "-";
                        tr.cells[7].innerHTML = "-";
                        break;
                    default:  //2 3 -1 -12 -13			
                        tr.cells[5].innerHTML = A[matchindex][13] + "-" + A[matchindex][14];
                        tr.cells[7].innerHTML = A[matchindex][15] + "-" + A[matchindex][16];
                        tr.cells[7].style.color = "red";
                        break;
                }

                if (scorechange) {
                    //Odds1x2Mail(A[matchindex][0]);
                    ShowFlash(D[0], matchindex);
                    if (tr.style.display != "none") {
                        hometeam = A[matchindex][4 + Config.language].replace("<font color=#880000>(中)</font>", " 中").substring(0, 7);
                        guestteam = A[matchindex][7 + Config.language].substring(0, 7);
                        sclassname = B[A[matchindex][1]][1 + Config.language];
                        if (score1change) {
                            hometeam = "<font color=red>" + hometeam + "</font>";
                            score1 = "<font color=red>" + D[2] + "</font>";
                            score2 = "<font color=blue>" + D[3] + "</font>";
                        }
                        if (score2change) {
                            guestteam = "<font color=red>" + guestteam + "</font>";
                            score1 = "<font color=blue>" + D[2] + "</font>";
                            score2 = "<font color=red>" + D[3] + "</font>";
                        }
                        window.clearTimeout(nofityTimer);
                        if (notify == "") notify = "<font color=#6666FF><B>入球提示：</b></font>";
                        notify += sclassname + ":" + hometeam + " <font color=blue>" + score1 + "-" + score2 + "</font> " + guestteam + " &nbsp; ";
                        nofityTimer = window.setTimeout("clearNotify()", 20000);

                        if (Config.winLocation >= 0 && parseInt(D[1]) >= -1) {
                            if (matchNum % 2 == 0)
                                winStr += "<tr bgcolor=#ffffff height=32 align=center class=line><td><font color=#1705B1>" + sclassname + "</font></td><td> " + tr.cells[3].innerHTML + "</td><td><b>" + hometeam + "</b></td><td width=11% style='font-size: 18px;font-family:Verdana;font-weight:bold;'>" + score1 + "-" + score2 + "</td><td>" + Goal2GoalCn(A[matchindex][25]) + "</td><td><b>" + guestteam + "</b></td></tr>";
                            else
                                winStr += "<tr bgcolor=#FDF1E7 height=32 align=center class=line><td><font color=#1705B1>" + sclassname + "</font></td><td> " + tr.cells[3].innerHTML + "</td><td><b>" + hometeam + "</b></td><td width=11% style='font-size: 18px;font-family:Verdana;font-weight:bold;'>" + score1 + "-" + score2 + "</td><td>" + Goal2GoalCn(A[matchindex][25]) + "</td><td><b>" + guestteam + "</b></td></tr>";

                            matchNum = matchNum + 1
                        }
                    }
                } //scorechange
            }
            if (matchNum > 0) ShowCHWindow(winStr, matchNum);
            document.getElementById("notify").innerHTML = notify;
        } catch (e) { }
    }

    function ShowFlash(id, n) {
        try {
            if (Config.sound >= 0 && parseInt(A[n][12]) >= -1) {
                if (document.getElementById("tr1_" + id).style.display != "none") {
                    document.getElementById("flashsound").innerHTML = flash_sound[Config.sound];
                }
            }
        }
        catch (e) { };
        window.setTimeout("timecolors(" + id + ")", 120000);
    }

    function timecolors(matchid) {
        try {
            var tr = document.getElementById("tr1_" + matchid);
            tr.cells[4].style.backgroundColor = "";
            tr.cells[6].style.backgroundColor = "";
        }
        catch (e) { }
    }

    function clearNotify() {
        document.getElementById("notify").innerHTML = "";
    }

    function ShowAllMatch() {
        var i, j, inputs;
        inputs = document.getElementById("checkboxleague").getElementsByTagName("input");
        for (var i = 0; i < inputs.length; i++)
            inputs[i].checked = true;

        inputs = document.getElementById("table_live").getElementsByTagName("tr");
        for (var i = 0; i < inputs.length; i++)
            if (inputs[i].getAttribute("index") != null) inputs[i].style.display = "";

        for (var i = 0; i < matchcount; i++)
            if (A[i][1] >= 0 && A[i][27] != "") document.getElementById("tr2_" + A[i][0]).style.display = "";
        if (orderby == "league") {
            for (var i = 0; i < sclasscount; i++) {
                if (B[i][8] > 0) {
                    document.getElementById("tr_" + i).style.display = "";
                    document.getElementById("expand" + i).style.display = "none";
                    document.getElementById("collapse" + i).style.display = "";
                }
            }
        }
        document.getElementById("hiddencount").innerHTML = "0";
        hiddenID = "_";
        writeCookie("HiddenMatchID", hiddenID);
        countCheckNum();
        makeMyCountry();
    }

    //'按比赛状态显示
    function ShowMatchByMatchState(n) {
        var i, j;
        var hh = 0;
        var trs = document.getElementById("table_live").getElementsByTagName("tr");
        for (var i = 1; i < trs.length; i++) {
            if (trs[i].getAttribute("index") != null) {
                trs[i].style.display = "none";
                trs[i + 1].style.display = "none";
                hh++;
            }
        }
        for (var i = 0; i < matchcount; i++) {
            if (A[i][1] == -1) continue;
            if (n == 1 && parseInt(A[i][12]) > 0 || n == 2 && A[i][12] == "-1" || n == 3 && A[i][12] == "0" || n == 4 && A[i][24] == "True") {
                document.getElementById("tr1_" + A[i][0]).style.display = "";
                if (A[i][27] != "") document.getElementById("tr2_" + A[i][0]).style.display = "";
                hh--;
            }
        }
        document.getElementById("hiddencount").innerHTML = hh;
        if (orderby == "league") {
            for (var i = 1; i <= sclasscount; i++) {
                var show = false;
                for (var j = 1; j <= matchcount; j++) {
                    if (A[j][1] != i) continue;
                    if (n == 1 && parseInt(A[j][8]) > 0 || n == 2 && A[j][8] == "-1" || n == 3 && A[j][8] == "0" || n == 4 && A[j][20] == "True") show = true;
                }
                if (show)
                    document.getElementById("tr_" + i).style.display = "";
                else
                    document.getElementById("tr_" + i).style.display = "none";
            }
        }
    }


    function hidematch(i) {
        document.getElementById("tr1_" + A[i][0]).style.display = "none";
        document.getElementById("tr2_" + A[i][0]).style.display = "none";
        document.getElementById("hiddencount").innerHTML = parseInt(document.getElementById("hiddencount").innerHTML) + 1;

        if (hiddenID == "_") {
            for (var j = 0; j < matchcount; j++) {
                if (A[j][1] != -1 && j != i) hiddenID += A[j][0] + "_";
            }
        }
        else
            hiddenID = hiddenID.replace("_" + A[i][0] + "_", "_")

        writeCookie("HiddenMatchID", hiddenID);
    }

    function SelectOtherLeague() {
        var inputs = document.getElementById("checkboxleague").getElementsByTagName("input");
        var inputs2 = document.getElementById("checkboxcountry").getElementsByTagName("input");
        var hh = 0;
        hiddenID = "_";
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].checked) {
                inputs[i].checked = false;
                if (orderby == "league" && B[i][8] > 0) document.getElementById("tr_" + i).style.display = "none";
                for (var j = 0; j < matchcount; j++) {
                    if (A[j][1] == inputs[i].value) {
                        document.getElementById("tr1_" + A[j][0]).style.display = "none";
                        if (A[j][27] != "") document.getElementById("tr2_" + A[j][0]).style.display = "none";
                        hh = hh + 1;
                        //			 	 if(hiddenID.indexOf("_"+A[j][0] + "_")==-1) hiddenID+=A[j][0] + "_";
                    }
                }
            }
            else {
                inputs[i].checked = true;
                if (orderby == "league" && B[i][8] > 0) document.getElementById("tr_" + i).style.display = "";
                for (var j = 0; j < matchcount; j++) {
                    if (A[j][1] == inputs[i].value) {
                        document.getElementById("tr1_" + A[j][0]).style.display = "";
                        if (A[j][27] != "") document.getElementById("tr2_" + A[j][0]).style.display = "";
                        //hiddenID=hiddenID.replace("_"+A[j][0] + "_","_")
                        hiddenID += A[j][0] + "_";
                    }
                }
            }
        }
        for (var i = 0; i < inputs2.length; i++) {
            if (inputs2[i].checked)
                inputs2[i].checked = false;
            else
                inputs2[i].checked = true;
        }
        document.getElementById("hiddencount").innerHTML = hh;
        writeCookie("HiddenMatchID", hiddenID);
    }

    function CheckLeague(i) {
        var hh = parseInt(document.getElementById("hiddencount").innerHTML);
        if (hiddenID == "_") {
            for (var j = 0; j < matchcount; j++) {
                if (A[j][1] != -1) hiddenID += A[j][0] + "_";
            }
        }
        if (document.getElementById("checkboxleague_" + i).checked) {
            if (orderby == "league") document.getElementById("tr_" + i).style.display = "";
            for (var j = 0; j < matchcount; j++) {
                if (A[j][1] == i) {
                    document.getElementById("tr1_" + A[j][0]).style.display = "";
                    if (A[j][27] != "") document.getElementById("tr2_" + A[j][0]).style.display = "";
                    hh--;
                    if (hiddenID.indexOf("_" + A[j][0] + "_") == -1) hiddenID += A[j][0] + "_";
                }
            }
        }
        else {
            if (orderby == "league") document.getElementById("tr_" + i).style.display = "none";
            for (var j = 0; j < matchcount; j++) {
                if (A[j][1] == i) {
                    document.getElementById("tr1_" + A[j][0]).style.display = "none";
                    if (A[j][27] != "") document.getElementById("tr2_" + A[j][0]).style.display = "none";
                    hh++;
                    hiddenID = hiddenID.replace("_" + A[j][0] + "_", "_")
                    //(hiddenID.indexOf("_"+A[j][0] + "_")==-1) hiddenID+=A[j][0] + "_";
                }
            }
        }
        document.getElementById("hiddencount").innerHTML = hh;
        writeCookie("HiddenMatchID", hiddenID);
        countCheckNum();
        makeMyCountry();
    }

    //function CheckCountry(id){
    //	var i,j;
    //	var hh=0;
    //	var trs=document.getElementById("table_live").getElementsByTagName("tr");
    //	for(var i=1; i<trs.length;i++){	
    //		if(trs[i].getAttribute("index")!=null){
    //			trs[i].style.display="none";
    //			trs[i+1].style.display="none";
    //			hh++;
    //		}
    //	}
    //	for(var i=0;i<matchcount;i++){
    //		if(A[i][1]!=-1 && B[A[i][1]][9]==id)
    //		{
    //			document.getElementById("tr1_" +  A[i][0]).style.display="";
    //			if(A[i][27]!="") document.getElementById("tr2_" +  A[i][0]).style.display="";			
    //			hh--;
    //		}		
    //	}
    //   document.getElementById("hiddencount").innerHTML=hh;
    //}

    function CheckCountry(id) {
        var hh = parseInt(document.getElementById("hiddencount").innerHTML);
        if (hiddenID == "_") {
            for (var j = 0; j < matchcount; j++) {
                if (A[j][1] != -1) hiddenID += A[j][0] + "_";
            }
        }
        if (document.getElementById("checkboxcountry_" + id).checked) {
            for (var j = 0; j < matchcount; j++) {
                if (A[j][1] != -1 && B[A[j][1]][9] == id) {

                    document.getElementById("tr1_" + A[j][0]).style.display = "";
                    if (A[j][27] != "") document.getElementById("tr2_" + A[j][0]).style.display = "";
                    hh--;
                    if (hiddenID.indexOf("_" + A[j][0] + "_") == -1) hiddenID += A[j][0] + "_";
                }
            }
        }
        else {
            for (var j = 0; j < matchcount; j++) {
                if (A[j][1] != -1 && B[A[j][1]][9] == id) {
                    document.getElementById("tr1_" + A[j][0]).style.display = "none";
                    if (A[j][27] != "") document.getElementById("tr2_" + A[j][0]).style.display = "none";
                    hh++;
                    hiddenID = hiddenID.replace("_" + A[j][0] + "_", "_")
                    //(hiddenID.indexOf("_"+A[j][0] + "_")==-1) hiddenID+=A[j][0] + "_";
                }
            }
        }
        document.getElementById("hiddencount").innerHTML = hh;
        writeCookie("HiddenMatchID", hiddenID);
        countCheckNum();
        makeMyLeague();
    }
    function MoveToBottom(m) {
        try {
            document.getElementById("tr1_" + m).parentElement.insertAdjacentElement("BeforeEnd", document.getElementById("tr1_" + m));
            document.getElementById("tr2_" + m).parentElement.insertAdjacentElement("BeforeEnd", document.getElementById("tr2_" + m));
//            for (var i = 1; i <= adinfo1.length; i++) {
//                document.getElementById("table_live").rows(i * 5 + 1).insertAdjacentElement("BeforeBegin", document.getElementById("tr_ad" + i));
//            }
        } catch (e) { }
    }

    function setOrderby(a) {
        orderby = a
        writeCookie("orderby", orderby);
        LoadLiveFile();
    }
    //更新比赛进行的时间
    function setMatchTime() {
        for (var i = 0; i < matchcount; i++) {
            try {
                if (A[i][1] == -1) continue;
                if (A[i][12] == "1") {  //上半场			
                    var t = A[i][11].split(",");
                    var t2 = new Date(t[0], t[1], t[2], t[3], t[4], t[5]);
                    goTime = Math.floor((new Date() - t2 - difftime) / 60000);
                    if (goTime > 45) goTime = "45+";
                    if (goTime < 1) goTime = "1";
                    document.getElementById("time_" + A[i][0]).innerHTML = goTime + "<img src='images/in.gif' border=0>";
                }
                if (A[i][12] == "3") {  //下半场		
                    var t = A[i][11].split(",");
                    var t2 = new Date(t[0], t[1], t[2], t[3], t[4], t[5]);
                    goTime = Math.floor((new Date() - t2 - difftime) / 60000) + 46;
                    if (goTime > 90) goTime = "90+";
                    if (goTime < 46) goTime = "46";
                    document.getElementById("time_" + A[i][0]).innerHTML = goTime + "<img src='images/in.gif' border=0>";
                }
            } catch (e) { }
        }
        runtimeTimer = window.setTimeout("setMatchTime()", 30000);
    };

    function showdetail(n, event) {
        if (Config.detail == 0) return;
        if (A[n][12] == "0") return;
        try {
            if (Math.floor((new Date() - loadDetailFileTime) / 600) > 60) LoadDetailFile();
            var R = new Array();
            var html = "<table width=350 bgcolor=#E1E1E1 cellpadding=0 cellspacing=1 border=0 style='border:solid 1px #666;'>";
            html += "<tr><td height=20 colspan=5 bgcolor=#666699 align=center><font color=white><b>初盘参考：" + Goal2GoalCn(A[n][25]) + "</b></font></td></tr>";
            html += "<tr bgcolor=#D5F2B7 align=center><td height=20 colspan=2 width=44%><font color=#006600><b>" + A[n][4 + Config.language] + "[" + A[n][21] + "]</b></font></td><td width=12% bgcolor=#CCE8B5>时间</td><td colspan=2 width=44%><font color=#006600><b>" + A[n][7 + Config.language] + "[" + A[n][22] + "]</b></font></td></tr>";
            for (var i = 0; i < rq.length; i++) {
                R = rq[i].split('^');
                if (R[0] != A[n][0]) continue;
                if (R[1] == "1")
                    html += "<tr bgcolor=white align=center><td width=6% height=18><img src='http://live.nowodds.com/images/" + R[2] + ".gif'></td><td width=38%>" + R[4] + "</td><td width=12% bgcolor=#EFF4EA>" + R[3] + "'</td><td width=38%></td><td width=6%></td></tr>";
                else
                    html += "<tr bgcolor=white align=center><td width=6% height=18></td><td width=38%></td><td width=12% bgcolor=#EFF4EA>" + R[3] + "'</td><td width=38%>" + R[4] + "</td><td width=6%><img src='http://live.nowodds.com/images/" + R[2] + ".gif'></td></tr>";
            }
            html += "</table>";
            
            //document.getElementById('winScore').style.left = (document.body.clientWidth / 2 - 175) + "px";
            //document.getElementById('winScore').style.top = (document.documentElement.scrollTop + event.clientY + 15) + "px";
            //document.getElementById("winScore").innerHTML = html;
            //document.getElementById("winScore").style.display = "";
            new Ext.ToolTip({
                target:Ext.getDom("tr1_" + A[n][0]).cells[5],
                autoDestroy :true,
                unstyled :true,
                autoWidth :true,
                html:html
            }).show();
        } catch (e) { }
    }
    function showvideo(n, event) {
        try {
            if (Math.floor((new Date() - loadVideoFileTime) / 600) > 60) LoadVideoFile();
            var html = "<table width=200 bgcolor=#E1E1E1 cellpadding=0 cellspacing=1 border=0 style='border:solid 1px #666;font-size:13px;'>";
            html += "<tr><td height=20 colspan=5 bgcolor=#666699 align=center><font color=white><b>直播频道</b></font></td></tr>";
            var soft = "";
            for (var i = 0; i < V[A[n][0]].length / 5; i++) {
                var j = i + i * 2;
                if (soft != V[A[n][0]][j + 1] || i == 0) {
                    if (i > 0)
                        html += "</td></tr>";
                    html += "<tr bgcolor=white align=center><td height=18 colspan='5'>"
                }
                if (V[A[n][0]][j + 1].indexOf("外部链接") != -1) {
                    var urls = V[A[n][0]][j + 1].split('|');
                    if (urls.length > 1)
                        html += "<a href='" + urls[1] + "' target='_blank'>" + urls[0] + " </a>";
                }
                else
                //html+="<a href='http://tv.city007.net/video.aspx?pid="+V[A[n][0]][j]+"&cid="+V[A[n][0]][j+2]+"' target='_blank'>"+V[A[n][0]][j+1]+" </a>";
                    html += "<a href='http://www.310tv.com/" + V[A[n][0]][j + 4] + "/" + V[A[n][0]][j] + "_" + V[A[n][0]][j + 2] + "_" + V[A[n][0]][j + 3] + ".html' target='_blank'>" + V[A[n][0]][j + 1] + " </a>";
                soft = V[A[n][0]][j + 1];
            }
            html += "</table>";
            document.getElementById('videoInfo').style.left = (document.body.clientWidth / 2 + 50) + "px";
            document.getElementById('videoInfo').style.top = (document.documentElement.scrollTop + event.clientY) + "px";
            document.getElementById("videoInfo").innerHTML = html;
            MM_showHideLayers('videoInfo', '', 'show');
        } catch (e) {
        }
    }
    function showpaulu(n, event) {
        try {
            if (Config.vs == 0) return;
            var html = [], bg = "";
            var bigNum = 0, victoryNum = 0, singleNum = 0, j = 0, win1;
            var win = 0, standoff = 0;
            var countInfo = "";
            html.push("<div style='border:solid 1px #666; background-color:#e4e4e4'><table width='530' border='0' align='center' cellpadding='0' cellspacing='1' bgcolor='#dddddd'>");
            html.push("<tr align='center' bgcolor='#006699' style='color:white'>");
            html.push("<td width='50' height='18'>赛事</td>");
            html.push("<td width='50'>时间</td>");
            html.push("<td>主场球队</td>");
            html.push("<td width='35'>比分</td>");
            html.push("<td>客场球队</td>");
            html.push("<td width='28'>半场</td>");
            html.push("<td>盘口</td>");
            html.push("<td width='28'>盘路</td>");
            html.push("<td width='28'>胜负</td>");
            html.push("<td width='28'>大小</td>");
            html.push("<td width='28'>单双</td>");
            html.push("</tr>");
            for (var i = 0; i < p.length; i++) {
                var b = p[i];
                if (!(b[3] == A[n][2] && b[4] == A[n][3] || b[4] == A[n][2] && b[3] == A[n][3])) continue;
                if (b[7] == null) b[7] = "";
                if (b[8] == null) b[8] = "";

                bg = (bg == "ts1") ? "ts2" : "ts1";
                html.push("<tr align=center class='" + bg + "'>");
                html.push("<td bgcolor=" + b[1] + " height=22><font color=#FFFFFF>" + b[0] + "</font></td>");
                html.push("<td>" + b[2] + "</td>");

                if (b[3] == A[n][2]) //主场
                {
                    html.push("<td><font color=#880000>" + A[n][4 + Config.language] + "</td>");
                    html.push("<td style='color:red'><B>" + b[5] + "-" + b[6] + "</td>");
                    html.push("<td>" + A[n][7 + Config.language] + "</a></td>");
                    html.push("<td><font color=red>" + b[7] + "-" + b[8] + "</td>");
                    html.push("<TD>" + Goal2GoalCn(b[9]) + "</TD>");
                    if (b[5] - b[9] > b[6]) { html.push("<TD><font color=red>赢</font></TD>"); win++; }
                    if (b[5] - b[9] == b[6]) { html.push("<TD><font color=green>走</font></TD>"); standoff++ }
                    if (b[5] - b[9] < b[6]) html.push("<TD><font color=blue>输</font></TD>");
                    if (b[5] > b[6]) html.push("<TD><font color=red>胜</font></TD>");
                    if (b[5] == b[6]) html.push("<TD><font color=green>平</font></TD>");
                    if (b[5] < b[6]) html.push("<TD><font color=blue>负</font></TD>");
                    if (b[5] > b[6]) victoryNum++;
                }
                else //客场
                {
                    html.push("<td style='color:#000000'>" + A[n][7 + Config.language] + "</td>");
                    html.push("<td style='color:red'><B>" + b[5] + "-" + b[6] + "</td>");
                    html.push("<td style='color=#880000'>" + A[n][4 + Config.language] + "</td>");
                    html.push("<td><font color=red>" + b[7] + "-" + b[8] + "</td>");
                    html.push("<TD>" + Goal2GoalCn(b[9]) + "</TD>");
                    if (b[5] - b[9] < b[6]) { html.push("<TD><font color=red>赢</font></TD>"); win++; }
                    if (b[5] - b[9] == b[6]) { html.push("<TD><font color=green>走</font></TD>"); standoff++ }
                    if (b[5] - b[9] > b[6]) html.push("<TD><font color=blue>输</font></TD>");
                    if (b[5] < b[6]) html.push("<TD><font color=red>胜</font></TD>");
                    if (b[5] == b[6]) html.push("<TD><font color=green>平</font></TD>");
                    if (b[5] > b[6]) html.push("<TD><font color=blue>负</font></TD>");
                    if (b[5] < b[6]) victoryNum++;
                }

                if (b[5] + b[6] > 2.5) {
                    html.push("<td><font color=red>大</td>");
                    bigNum++;
                }
                else
                    html.push("<td><font color=blue>小</td>");
                if ((b[5] + b[6]) % 2 == 1) {
                    html.push("<td><font color=red>单</td>");
                    singleNum++;
                }
                else
                    html.push("<td><font color=blue>双</td>");
                html.push("</tr>");
                j++;
            }

            if (j > 0) {
                if (j - standoff > 0)
                    win1 = Math.round(win / (j - standoff) * 1000) / 10;
                else
                    win1 = "0";
                html.push("<tr><td height=20 align=center colspan=11 bgcolor=white>最近[ <font color=red>" + j + " </font>]场,  &nbsp;  胜率：<font color=red>" + Math.round(victoryNum / j * 1000) / 10 + "%</font> 赢盘率：<font color=red>" + win1 + "% </font> 大球：<font color=red>" + Math.round(bigNum / j * 1000) / 10 + "%</font> 单：<font color=red>" + Math.round(singleNum / j * 1000) / 10 + "%</font></td></tr>");
            }
            html.push("</table></div>");

            document.getElementById('winScore').style.left = (document.body.clientWidth / 2 - 430) + "px";
            document.getElementById('winScore').style.top = Math.min(document.documentElement.scrollTop + 15 + event.clientY, document.body.clientHeight - j * 20 - 100) + "px";
            if (j == 0) {
                document.getElementById('winScore').style.left = (document.body.clientWidth / 2 - 20) + "px";
                html = [];
                html.push("<div style='border:solid 1px #666; background-color:#FFFFFF;width:120px;line-height:40px;text-align:center;font-size:14px'><b>无对战记录</b></div>");
            }
//            document.getElementById("winScore").innerHTML = html.join("");
//            document.getElementById("winScore").style.display = "";
            new Ext.ToolTip({
                target:Ext.getDom("tr1_" + A[n][0]).cells[7],
                autoDestroy :true,
                unstyled :true,
                autoWidth :true,
                html:html.join("")
            }).show();
        } catch (e) { }
    }


    function hiddendetail() {
        document.getElementById("winScore").innerHTML = "";
        document.getElementById("winScore").style.display = "none";
    }

    function check() {
        if (oldUpdateTime == lastUpdateTime && oldUpdateTime != "" && isliving) {
            if (confirm("由于程序忙，或其他网络问题，你已经和服务器断开连接超过 5 分钟，是否要重新连接观看比分？")) window.location.reload();
        }
        oldUpdateTime = lastUpdateTime;
        window.setTimeout("check()", 300000);
    }

    function LoadLiveFile() {
        var allDate = document.getElementById("allDate");
        var s = document.createElement("script");
        s.type = "text/javascript";
        if (orderby == "league")
            s.src = "http://live.nowodds.com/data/bf1.js?" + Date.parse(new Date());
        else
            s.src = "http://live.nowodds.com/data/bf.js?" + Date.parse(new Date());
        allDate.removeChild(allDate.firstChild);
        allDate.appendChild(s, "script");
        window.clearTimeout(LoadLiveFileTimer);
        LoadLiveFileTimer = window.setTimeout("LoadLiveFile()", 3600 * 1000);
    }
    function LoadDetailFile() {
        var detail = document.getElementById("span_detail");
        var s = document.createElement("script");
        s.type = "text/javascript";
        s.charset = "gb2312";
        s.src = "data/detail.js?" + Date.parse(new Date());
        detail.removeChild(detail.firstChild);
        detail.appendChild(s, "script");
        loadDetailFileTime = new Date();
    }
    function LoadVideoFile() {
        var videoData = document.getElementById("videoData");
        var s = document.createElement("script");
        s.type = "text/javascript";
        s.src = "http://www.310tv.com/js/programs.js?" + Date.parse(new Date());
        //s.src="programs.js?" +Date.parse(new Date());
        videoData.removeChild(videoData.firstChild);
        videoData.appendChild(s, "script");
        loadVideolFileTime = new Date();
    }
    function SetMatchType(m) {
        document.getElementById("MatchType" + Config.matchType).className = "";
        document.getElementById("MatchType" + m).className = "selected";
        document.getElementById("hiddencount").innerHTML = "0";
        Config.matchType = m;
        if (m == 0)
            document.getElementById("btn_all").style.display = "none";
        else
            document.getElementById("btn_all").style.display = "";
        LoadLiveFile();
        Config.writeCookie();
    }

    function SetLanguage(l) {
        document.getElementById("Language" + Config.language).className = "";
        document.getElementById("Language" + l).className = "selected";
        Config.language = l;
        LoadLiveFile();
        Config.writeCookie();
    }
    //function SetMatchType(m)
    //{
    //	document.getElementById("MatchType"+ Config.matchType).className="";
    //	document.getElementById("MatchType"+ m).className="selected";
    //	Config.matchType=m;
    //	LoadLiveFile();
    //	Config.writeCookie();
    //}

    function SetCompany(id) {
        document.getElementById("company" + Config.companyID).className = "";
        document.getElementById("company" + id).className = "selected";
        Config.companyID = id;
        LoadLiveFile();
        Config.writeCookie();
    }


    function CheckSound() {
        if (document.getElementById("soundCheck").checked) Config.sound = parseInt(document.getElementById("sound").value)
        else Config.sound = -1;
        Config.writeCookie();
    }
    function CheckWindow() {
        if (document.getElementById("windowCheck").checked) Config.winLocation = parseInt(document.getElementById("winLocation").value)
        else Config.winLocation = -1;
        Config.writeCookie();
    }

    function CheckExplain() {
        if (document.getElementById("explain").checked) Config.explain = 1;
        else Config.explain = 0;
        Config.writeCookie();
        ShowExplain();
    }
    function ShowExplain() {
        var value = "none";
        if (Config.explain == 1) value = "";
        for (var i = 0; i < matchcount; i++)
            if (A[i][1] != -1 && A[i][27] != "") document.getElementById("tr2_" + A[i][0]).style.display = value;
    }


    function CheckTeamRank() {
        if (document.getElementById("rank").checked) Config.rank = 1;
        else Config.rank = 0;
        Config.writeCookie();
        ShowTeamRank();
    }
    function ShowTeamRank() {
        for (var i = 0; i < matchcount; i++) {
            if (A[i][1] == -1) continue;
            if (A[i][21] != "") document.getElementById("horder_" + A[i][0]).innerHTML = (Config.rank == 1 ? "<font color=#444444><sup>[" + A[i][21] + "]</sup></font>" : "");
            if (A[i][22] != "") document.getElementById("gorder_" + A[i][0]).innerHTML = (Config.rank == 1 ? "<font color=#444444><sup>[" + A[i][22] + "]</sup></font>" : "");
        }
    }
    function CheckFunction(obj) {
        if (document.getElementById(obj).checked) eval("Config." + obj + "=1");
        else eval("Config." + obj + "=0");
        Config.writeCookie();
        if (obj = "yp" || obj == "op" || obj == "dx") LoadLiveFile();
    }
    function changeFontSize(obj, size) {
        Config.fontsize = size;
        document.getElementById(obj).style.fontSize = size + "px";
        Config.writeCookie();
    }


    //赛程赛果
    function ChangeSchedule(id, t) {
        var script = document.getElementById("scriptScsg");
        var s = document.createElement("script");
        s.type = "text/javascript";
        //http://info.bet007.com/jsData/matchResult/2011-2012/s36.js?
        s.src = "http://info.nowscore.com/AjaxLeague.aspx?SclassID=" + id + "&SclassType=" + (t == 1 ? "s" : "c") + "&v=" + Date.parse(new Date());
        script.removeChild(script.firstChild);
        script.appendChild(s, "script");
    }
    function ChangeSchedule2(id, t, groupid) {
        var script = document.getElementById("scriptScsg");
        var s = document.createElement("script");
        s.type = "text/javascript";
        s.src = "http://info.nowscore.com/AjaxLeague.aspx?SclassID=" + id + "&SclassType=" + (t == 1 ? "s" : "c") + "&SubSclassID=" + groupid + "&v=" + Date.parse(new Date());
        script.removeChild(script.firstChild);
        script.appendChild(s, "script");
    }
    //积分
    function ChangeScore(file) {
        var script = document.getElementById("scriptScore");
        var s = document.createElement("script");
        s.type = "text/javascript";
        s.src = "http://info.nowscore.com/IndexPage/score/" + file + ".js?" + Date.parse(new Date());
        script.removeChild(script.firstChild);
        script.appendChild(s, "script");
    }
    function ChangeJS(sclassID, kind) {
        sclassID2 = sclassID;
        kind2 = kind;
        ChangeSchedule(sclassID, kind);
    }
    function showLeagueList(type) {
        if (type == 1) {
            var leaguehtml = new Array();
            leaguehtml.push("<table width='100%' border='0' align='center' cellpadding='0' cellspacing='1' bgcolor='#ffffff' class='gre' style='text-align:center; margin-bottom:5px;'>");
            var c = -1;
            for (var i = 0; i < sclasscount; i++) {
                if (B[i][8] > 0 && B[i][7] != "") {
                    if (++c % 10 == 0) leaguehtml.push("<tr height=20>");
                    leaguehtml.push("<td bgcolor=" + B[i][4] + "><a href='javascript:ChangeJS(" + B[i][0] + "," + B[i][6] + ")'>" + B[i][1 + Config.language] + "</a></td>");
                    if (c % 10 == 9) leaguehtml.push("</tr>");
                    if (c == 0) ChangeJS(B[i][0], B[i][6]);
                }
            }
            leaguehtml.push("</tr></table>");
            document.getElementById("leagueList").innerHTML = leaguehtml.join("");
            document.getElementById("leagueList").style.display = "";
            document.getElementById("hotLeague").style.display = "none";
            document.getElementById("hotCup").style.display = "none";
            document.getElementById("LeagueKind1").className = "m_on";
            document.getElementById("LeagueKind2").className = "m_off";
            document.getElementById("LeagueKind3").className = "m_off";
        }
        if (type == 2) {
            document.getElementById("leagueList").style.display = "none";
            document.getElementById("hotLeague").style.display = "";
            document.getElementById("hotCup").style.display = "none";
            document.getElementById("LeagueKind1").className = "m_off";
            document.getElementById("LeagueKind2").className = "m_on";
            document.getElementById("LeagueKind3").className = "m_off";
            ChangeJS(36, 1);
        }
        if (type == 3) {
            document.getElementById("leagueList").style.display = "none";
            document.getElementById("hotLeague").style.display = "none";
            document.getElementById("hotCup").style.display = "";
            document.getElementById("LeagueKind1").className = "m_off";
            document.getElementById("LeagueKind2").className = "m_off";
            document.getElementById("LeagueKind3").className = "m_on";
            ChangeJS(75, 2);
        }
    }
    function SetLevel(m) {
        matchType = m;
        LoadLiveFile();
        document.getElementById("liZC").style.color = "red";
    }

    Config.getCookie("2in1");
    if (Config.matchType == 0)
        document.getElementById("btn_all").style.display = "none";
    document.getElementById("MatchType" + Config.matchType).className = "selected";



    if (Config.companyID != 1 && Config.companyID != 3 && Config.companyID != 4 && Config.companyID != 8 && Config.companyID != 12 && Config.companyID != 17 && Config.companyID != 23 && Config.companyID != 24 && Config.companyID != 31) Config.companyID = 3;
    document.getElementById("company" + Config.companyID).className = "selected";
    orderby = getCookie("orderby");
    if (orderby == null) orderby = "time";
    //LoadLiveFile();
    if (Config.fontsize != 12) document.getElementById("ScoreDiv").style.fontSize = Config.fontsize + "px";

    window.setTimeout("gettime()", 2000);
    window.setTimeout("check()", 30000);
</script>
<span id="span_detail"><script language="javascript" src="http://live.nowodds.com/data/detail.js" type="text/javascript" charset="gb2312"></script></span>
<span id="span_panlu"><script language="javascript" src="http://live.nowodds.com/data/panlu.js" type="text/javascript"></script></span>
    </div>
    
</body>
</html>
