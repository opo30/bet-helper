<%@ page language="C#" autoeventwireup="true" inherits="Default2, App_Web_default2.aspx.cdcab7d2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <span id="ScoreDiv"></span>
    <script type="text/javascript">
        function MakeTable() {
            var state, bg = "", line = -1;
            var H_redcard, G_redcard, H_yellow, G_yellow;

            var html = new Array();
            html.push("<table id='table_live' width=100% bgcolor=#C6C6C6 align=center cellspacing=1 border=0 cellpadding=0><tr class=ki1 align=center>");
            html.push("<td  width=3% bgcolor='#ff9933' height=20><font color=white>选</font></td><td  width=8%><font color=white>" + matchdate + "</font></td><td  width=5%><font color=white>时间</font></td><td  width=5%><font color=white>状态</font></td><td  width=18%><font color=white>主队</font></td><td  width=6%><font color=white>比分</font></td><td  width=17%><font color=white>客队</font></td><td  width=5%><font color=white>半场</font></td><td  width=10%><font color=white>数据</font></td><td width=20% colspan=3><font color=white>指数</font></td><td width=3%>走</td></tr>");

            //            oddsHttp.open("get", "Data/NowGoal/GetRemoteFile.aspx?f=xml&path=data/goal" + Config.companyID + ".xml&" + Date.parse(new Date()), false);
            //            oddsHttp.send(null);
            //            if (document.all)
            //                idList = oddsHttp.responseXML.documentElement.childNodes[1].text;
            //            else
            //                idList = oddsHttp.responseXML.documentElement.childNodes[1].textContent;

            for (var i = 0; i < matchcount; i++) {
                try {
                    line++;
                    B[A[i][1]][8]++;
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
                    if (A[i][17] != "0") H_redcard = "<img src='images/live/redcard" + A[i][17] + ".gif'>"; else H_redcard = "";
                    if (A[i][18] != "0") G_redcard = "<img src='images/live/redcard" + A[i][18] + ".gif'>"; else G_redcard = "";
                    if (A[i][19] != "0") H_yellow = "<img src='images/live/yellow" + A[i][19] + ".gif'>"; else H_yellow = "";
                    if (A[i][20] != "0") G_yellow = "<img src='images/live/yellow" + A[i][20] + ".gif'>"; else G_yellow = "";

                    if (bg != "ts1") bg = "ts1"; else bg = "ts2";
                    html.push("<tr align=center id='tr1_" + A[i][0] + "' class='" + bg + "' index='" + i + "' odds=''>");
                    html.push("<td height=18><input type=checkbox checked onclick='hidematch(" + i + ");return false;' class='inp'></td>");

                    if (B[A[i][1]][7] != "")
                        html.push("<td bgcolor=" + B[A[i][1]][4] + " style='color:white;'><a href='http://info.nowscore.com/" + B[A[i][1]][7] + "' target=_blank><font color=white>" + B[A[i][1]][1 + Config.language] + "</font></a></td>");
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
                    html.push("<td class=fr style='text-align:left'><a href='javascript:' onclick=analysis(" + A[i][0] + ") title='数据分析' style='padding-left:2px;'>析</a> <a style='cursor:pointer' href=javascript: onclick=\"AsianOdds(" + A[i][0] + ");return false\" title='11家指数'>亚</a> <a href='javascript:EuropeOdds(" + A[i][0] + ")' title='百家欧赔'>欧</a> ");
                    if (A[i][28] == "1") html.push("<a href='http://www.nowscore.com/odds/recommend.aspx?id=" + A[i][0] + "' style='color:red' target=_blank>荐</a>");

                    html.push("</td><td class=oddstd>&nbsp;</td>");
                    html.push("<td class=oddstd onclick='oddsDetail(" + A[i][0] + "," + Config.companyID + " )' style='cursor:pointer;'>&nbsp;</td>");
                    html.push("<td class=oddstd>&nbsp;</td>");
                    html.push("<td>&nbsp;</td></tr>");

                    if (A[i][27] == "") classx = "none"; else classx = "";
                    html.push("<tr id='tr2_" + A[i][0] + "' style='display:" + classx + "' bgcolor='#ffffff'><td colspan=13 align=center height=18 style='color:green;padding-right:108px;' id='other_" + A[i][0] + "'>" + A[i][27] + "</td></tr>");

                    if (line / 2 < adinfo1.length && line % 2 == 1)
                        html.push("<tr id=tr_ad" + (line + 1) / 2 + "><td colspan=13 bgcolor=#ffffff align=center height=18>广告：<a href='" + adinfo1[(line - 1) / 2] + "' target=_blank style='color:red'><b>" + adinfo2[(line - 1) / 2] + "</b></a></td></tr>");

                } catch (e) { }
            }
            html.push("</table>")
            document.getElementById("ScoreDiv").innerHTML = html.join("");


            //联赛/杯赛名列表
            var leaguehtml = new Array();
            leaguehtml.push("<ul id='checkboxleague'>");
            for (var i = 0; i < sclasscount; i++) {
                if (B[i][8] > 0) {
                    if (B[i][5] == "1")
                        leaguehtml.push("<li><input onclick='CheckLeague(" + i + ")' checked type=checkbox id='checkboxleague_" + i + "' value=" + i + "><label style='cursor:pointer' for='checkboxleague_" + i + "'><font color=red>" + B[i][1] + "[" + B[i][8] + "]</font></label></li>");
                    else
                        leaguehtml.push("<li><input onclick='CheckLeague(" + i + ")' checked type=checkbox id='checkboxleague_" + i + "' value=" + i + "><label style='cursor:pointer' for='checkboxleague_" + i + "'>" + B[i][1] + "<font color=#990000>[" + B[i][8] + "]</font></label></li>");
                }
            }
            leaguehtml.push("</ul>");
            //国家列表
            var country = new Array();
            for (var i = 0; i < C.length; i++) {
                if (C[i][2] > 0) country.push("<li><a href='javascript:CheckCountry(" + C[i][0] + ")' id='country_" + C[i][0] + "'>" + C[i][1] + "</a></li>");
            }
        }

        var ShowBf = function () {
            MakeTable();
        }</script>
    <script type="text/javascript" src="Data/NowGoal/GetRemoteFile.aspx?f=rootjs&path=data/bf.js"></script>
    </div>
    </form>
</body>
</html>
