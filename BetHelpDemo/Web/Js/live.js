/// <reference path="../Lib/ext/adapter/ext/ext-base-debug.js" />
/// <reference path="../Lib/ext/ext-all-debug.js" />

function showdetail() { } function hiddendetail() { } function showpaulu() { }
var loaded = 0, LoadTime = 0, nofityTimer, matchType = -1, runtimeTimer, getoddsxmlTimer, LoadLiveFileTimer;
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
var islive = true;

Config.getCookie("2in1");

var ShowBf = function () {

    Ext.getCmp("MatchTypeSelect").setText(Ext.getCmp("MatchTypeSelect").menu.items.itemAt(Config.matchType).text);
    Ext.getCmp("LanguageSelect").setText(Ext.getCmp("LanguageSelect").menu.items.itemAt(Config.language).text);
    if (Config.companyID != 1 && Config.companyID != 3 && Config.companyID != 4 && Config.companyID != 8 && Config.companyID != 12 && Config.companyID != 17 && Config.companyID != 23 && Config.companyID != 24 && Config.companyID != 31) Config.companyID = 3;
    Ext.getCmp("CompanySelect").setText(company[Config.companyID]);

    hiddenID = getCookie("HiddenMatchID");
    if (hiddenID == null) hiddenID = "";


    //grid.getStore().loadData(A);
    window.clearTimeout(runtimeTimer);
    window.clearTimeout(getoddsxmlTimer);
    if (islive) {
        MakeTable();
        showodds(false);
        runtimeTimer = window.setTimeout("setMatchTime()", 1000);
        if (Config.rank == 1) ShowTeamRank();
        if (Config.explain == 0) ShowExplain();
        getoddsxmlTimer = window.setTimeout("getoddsxml()", 3000);
        //window.setTimeout("showLeagueList(1)", 500);
        window.setTimeout("gettime()", 2000);
        window.setTimeout("check()", 30000);
    } else {
        MakeHistoryTable();
    }


}

function MakeTable() {
    var state, bg = "", line = -1, hh = 0;
    var H_redcard, G_redcard, H_yellow, G_yellow;
    var ArrayHiddenID = hiddenID.split("_");
    var oldHiddenID = true;
    var html = new Array();
    //    html.push("<table id='table_live' width=100% bgcolor=#C6C6C6 align=center cellspacing=1 border=0 cellpadding=0><tr class=ki1 align=center>");
    //    html.push("<td  width=3% bgcolor='#ff9933' height=20><font color=white>选</font></td><td  width=8%><font color=white>" + matchdate + "</font></td><td  width=5%><font color=white>时间</font></td><td  width=5%><font color=white>状态</font></td><td  width=16%><font color=white>主队</font></td><td  width=5%><font color=white>比分</font></td><td  width=16%><font color=white>客队</font></td><td  width=5%><font color=white>半场</font></td><td  width=15%><font color=white>数据</font></td><td width=19% colspan=3><font color=white>指数</font></td><td width=3%>走</td></tr>");

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
    var scheduleData = [];
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
            var rowData = {};
            rowData.scheduleid = A[i][0];
            rowData.display = classx;
            rowData.bgcolor = B[A[i][1]][4];
            if (B[A[i][1]][7] != "")
                rowData.league = "<a href='" + (parseInt(Config.language) < 2 ? "http://info.nowscore.com/" + B[A[i][1]][7] + "&lang=" + Config.language : "http://info.nowgoal.com/football/en/" + B[A[i][1]][7]) + "' target=_blank><font color=white>" + B[A[i][1]][1 + Config.language] + "</font></a>";
            else
                rowData.league = B[A[i][1]][1 + Config.language];
            rowData.matchdate = A[i][10];
            if (state == "-1")
                rowData.classx2 = "td_scoreR";
            else
                rowData.classx2 = "td_score";
            rowData.state = state_ch[state + 14].split(",")[Config.language];
            rowData.h_team = "<span id=horder_" + A[i][0] + "></span><a id='yellow1_" + A[i][0] + "'>" + H_yellow + "</a><a id='redcard1_" + A[i][0] + "'>" + H_redcard + "</a><a id='team1_" + A[i][0] + "' href='javascript:' onclick='TeamPanlu_10(" + A[i][0] + ")' title='" + A[i][21] + "'>" + A[i][4 + Config.language] + "</a>";
            rowData.goal = match_score;
            rowData.g_team = "<a id='team2_" + A[i][0] + "' href='javascript:' onclick='TeamPanlu_10(" + A[i][0] + ")'  title='" + A[i][22] + "'>" + A[i][7 + Config.language] + "</a><a id='redcard2_" + A[i][0] + "'>" + G_redcard + "</a><a id='yellow2_" + A[i][0] + "'>" + G_yellow + "</a><span id=gorder_" + A[i][0] + "></span>";
            rowData.match_half = match_half;
            rowData.data = "<a href='javascript:' onclick=analysis(" + A[i][0] + ") title='数据分析' style='padding-left:2px;'>析</a><a style='cursor:pointer;padding-left:4px;' href=javascript: onclick=\"AsianOdds(" + A[i][0] + ");return false\" title='11家指数'>亚</a><a href='javascript:EuropeOdds(" + A[i][0] + ")' style='padding-left:4px;' title='百家欧赔'>欧</a>";
            if (A[i][29] == "1") rowData.data += "<a href='data/matchInfo.aspx?id=" + A[i][0] + "' style='color:red;padding-left:4px;' target=_blank>现</a>";
            else rowData.data += "<a href='data/matchInfo.aspx?id=" + A[i][0] + "' style='color:blue;padding-left:4px;' target=_blank>现</a>";
            if (A[i][28] == "1") rowData.data += "<a href='odds/recommend.aspx?id=" + A[i][0] + "' style='color:red;padding-left:4px;' target=_blank>荐</a>";

            if (typeof (V) != "undefined" && typeof (V[A[i][0]]) != "undefined") {
                if (V[A[i][0]][1].indexOf("外部链接") != -1) {
                    var urls = V[A[i][0]][1].split('|');
                    if (urls.length > 1)
                        rowData.data += "<a href='" + urls[1] + "' target='_blank' style='color:red;padding-left:4px;'  onmouseover='showvideo(" + i + ",event)' onmouseout=MM_showHideLayers('videoInfo','','hidden')>直</a>";
                }
                else
                    rowData.data += "<a href='http://www.310tv.com/" + V[A[i][0]][4] + "/" + V[A[i][0]][0] + "_" + V[A[i][0]][2] + "_" + V[A[i][0]][3] + ".html' target='_blank' style='color:red;padding-left:4px;'  onmouseover='showvideo(" + i + ",event)' onmouseout=MM_showHideLayers('videoInfo','','hidden')>直</a>";
            }

            rowData.h_odds = "&nbsp;";
            rowData.pankou = "&nbsp;";
            rowData.g_odds = "&nbsp;";
            rowData.zoudi = "&nbsp;";
            if (A[i][27] + A[i][32] == "" || classx == "none") classx = "none"; else classx = "";
            rowData.other = showExplain(A[i][32], A[i][4 + Config.language], A[i][7 + Config.language]) + (A[i][32] != "" && A[i][27] != "" ? "<br>" + A[i][27] : A[i][27] != "" ? A[i][27] : "");
            rowData.odds = "";
            rowData.index = i;
            scheduleData.push(rowData);
        } catch (e) { }
    }
    var grid = Ext.getCmp("HistoryFileGrid");
    grid.getStore().loadData(scheduleData);
    //document.getElementById("ScoreDiv").innerHTML = html.join("");
    Ext.getDom("hiddencount").innerHTML = hh;
    //联赛/杯赛名列表
    makeMyLeague();
    if (Config.matchType == 0)
        Ext.getCmp("btn_all").hide();
    //国家列表
    //makeMyCountry();
}

function gettime() {
    try {
        LoadTime = (LoadTime + 1) % 60;
        if (LoadTime == 0)
            oXmlHttp.open("get", "Data/NowGoal/GetRemoteFile.aspx?f=xml&path=data/change2.xml?" + Date.parse(new Date()), true);
        else
            oXmlHttp.open("get", "Data/NowGoal/GetRemoteFile.aspx?f=xml&path=data/change.xml?" + Date.parse(new Date()), true);
        oXmlHttp.onreadystatechange = refresh;
        oXmlHttp.send(null);
    }
    catch (e) { }
    if (islive) {
        window.setTimeout("gettime()", 2000);
    }
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
        var grid = Ext.getCmp("HistoryFileGrid");

        for (var i = 0; i < root.childNodes.length; i++) {
            if (document.all)
                D = root.childNodes[i].text.split("^"); //0:ID,1:state,2:score1,3:score2,4:half1,5:half2,6:card1,7:card2,8:time1,9:time2,10:explain,11:lineup		
            else
                D = root.childNodes[i].textContent.split("^");
            D[1] = parseInt(D[1]);

            tr = document.getElementById("tr1_" + D[0]);
            if (tr == null) continue;

            var index = grid.getStore().indexOfId(parseInt(D[0]));
            matchindex = tr.attributes["index"].value;
            score1change = false;
            if (A[matchindex][13] != D[2]) {
                A[matchindex][13] = D[2];
                score1change = true;
                grid.getView().getCell(index, 4).style.backgroundColor = "#bbbb22";
            }
            score2change = false;
            if (A[matchindex][14] != D[3]) {
                A[matchindex][14] = D[3];
                score2change = true;
                grid.getView().getCell(index, 6).style.backgroundColor = "#bbbb22";
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
                    grid.getView().getCell(index, 13).style.display = "none";
                else
                    grid.getView().getCell(index, 13).style.display = "";
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
                    document.getElementById("redcard1_" + D[0]).innerHTML = "<img src=http://live.nowscore.com/images/redcard" + D[6] + ".gif border='0'> ";
                if (Config.redcard == 1) grid.getView().getCell(index, 4).style.backgroundColor = "#ff8888";
                window.setTimeout("timecolors(" + D[0] + "," + matchindex + ")", 12000);
            }
            if (D[7] != A[matchindex][18]) {
                A[matchindex][18] = D[7];
                if (D[7] == "0")
                    document.getElementById("redcard2_" + D[0]).innerHTML = "";
                else
                    document.getElementById("redcard2_" + D[0]).innerHTML = "<img src=http://live.nowscore.com/images/redcard" + D[7] + ".gif border='0'> ";
                if (Config.redcard == 1) grid.getView().getCell(index, 6).style.backgroundColor = "#ff8888";
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
            if (A[matchindex][11] != D[8]) grid.getView().getCell(index, 2).innerHTML = D[8];
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
                        grid.getView().getCell(index, 3).innerHTML = ""; //tr.cells[3].innerHTML = "";
                        break;
                    case 1:
                        var t = A[matchindex][11].split(",");
                        var t2 = new Date(t[0], t[1], t[2], t[3], t[4], t[5]);
                        goTime = Math.floor((new Date() - t2 - difftime) / 60000);
                        if (goTime > 45) goTime = "45+"
                        if (goTime < 1) goTime = "1";
                        grid.getView().getCell(index, 3).innerHTML = goTime + "<img src='images/in.gif'>";
                        break;
                    case 2:
                    case 4:
                        grid.getView().getCell(index, 3).innerHTML = state_ch[parseInt(D[1]) + 14].split(",")[Config.language];
                        break;
                    case 3:
                        var t = A[matchindex][11].split(",");
                        var t2 = new Date(t[0], t[1], t[2], t[3], t[4], t[5]);
                        goTime = Math.floor((new Date() - t2 - difftime) / 60000) + 46;
                        if (goTime > 90) goTime = "90+";
                        if (goTime < 46) goTime = "46";
                        grid.getView().getCell(index, 3).innerHTML = goTime + "<img src='images/in.gif'>";
                        break;
                    case -1:
                        grid.getView().getCell(index, 3).innerHTML = state_ch[parseInt(D[1]) + 14].split(",")[Config.language];
                        grid.getView().getCell(index, 5).style.color = "red";
                        window.setTimeout("MoveToBottom(" + D[0] + ")", 25000);
                        break;
                    default:
                        grid.getView().getCell(index, 3).innerHTML = state_ch[parseInt(D[1]) + 14].split(",")[Config.language];
                        MoveToBottom(D[0]);
                        break;
                }
            }

            //score
            switch (A[matchindex][12]) {
                case 0:
                    if (D[11] == "1")
                        grid.getView().getCell(index, 5).innerHTML = "阵容";
                    else
                        grid.getView().getCell(index, 5).innerHTML = "-";
                    break;
                case 1:
                    grid.getView().getCell(index, 5).innerHTML = A[matchindex][13] + "-" + A[matchindex][14];
                    break;
                case -11:
                case -14:
                    grid.getView().getCell(index, 5).innerHTML = "-";
                    grid.getView().getCell(index, 7).innerHTML = "-";
                    break;
                default:  //2 3 -1 -12 -13			
                    grid.getView().getCell(index, 5).innerHTML = A[matchindex][13] + "-" + A[matchindex][14];
                    grid.getView().getCell(index, 7).innerHTML = A[matchindex][15] + "-" + A[matchindex][16];
                    grid.getView().getCell(index, 7).style.color = "red";
                    break;
            }

            if (scorechange) {
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
                    //window.clearTimeout(nofityTimer);
                    if (notify == "") notify = "<font color=#6666FF><B>入球提示：</b></font>";
                    notify += sclassname + ":" + hometeam + " <font color=blue>" + score1 + "-" + score2 + "</font> " + guestteam + " &nbsp; ";
                    //nofityTimer = window.setTimeout("clearNotify()", 20000);

                    //                    if (Config.winLocation >= 0 && parseInt(D[1]) >= -1) {
                    //                        if (matchNum % 2 == 0)
                    //                            winStr += "<tr bgcolor=#ffffff height=32 align=center class=line><td><font color=#1705B1>" + sclassname + "</font></td><td> " + grid.getView().getCell(index, 3).innerHTML + "</td><td><b>" + hometeam + "</b></td><td width=11% style='font-size: 18px;font-family:Verdana;font-weight:bold;'>" + score1 + "-" + score2 + "</td><td>" + Goal2GoalCn(A[matchindex][25]) + "</td><td><b>" + guestteam + "</b></td></tr>";
                    //                        else
                    winStr += "<tr bgcolor=#FDF1E7 height=32 align=center class=line><td><font color=#1705B1>" + sclassname + "</font></td><td> " + grid.getView().getCell(index, 3).innerHTML + "</td><td><b>" + hometeam + "</b></td><td width=11% style='font-size: 18px;font-family:Verdana;font-weight:bold;'>" + score1 + "-" + score2 + "</td><td>" + Goal2GoalCn(A[matchindex][25]) + "</td><td><b>" + guestteam + "</b></td></tr>";

                    matchNum = matchNum + 1
                    //                    }
                }
            } //scorechange
        }
        if (matchNum > 0) ShowNotify(winStr); //ShowCHWindow(winStr, matchNum);
        document.getElementById("notify").innerHTML = notify;
    } catch (e) { }
}

function ShowNotify(str) {

    var st = "<table width=460 border=0 cellpadding=0 cellspacing=0>";
    st = st + str;
    st = st + "</table>";
    st = st + "<style type='text/css'>";
    st = st + "td {font-family: 'Tahoma', '宋体';font-size: 13px;}";
    st = st + ".line td { border-bottom:solid 1px #FFD8CA; line-height:32px;}";
    st = st + "</style>";
    new Ext.ux.Notification({
        animateTarget: Ext.getCmp("statusbar").getEl(),
        animateFrom: Ext.getCmp("statusbar").getPosition(),
        autoDestroy: true,
        hideDelay: 5000,
        html: st,
        iconCls: 'x-icon-information',
        title: '雨泽比分网 入球提示',
        listeners: {
            'beforerender': function () {
            }
        }
    }).show();
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

        var grid = Ext.getCmp("HistoryFileGrid");
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

            var index = grid.getStore().indexOfId(parseInt(D[0]));

            var tmp = "";
            if (Config.yp == 1) tmp += "<p class=odds1>" + D[3] + "</p>";
            if (Config.op == 1) tmp += "<p class=odds2>" + D[6] + "</p>";
            if (Config.dx == 1) tmp += "<p class=odds3>" + D[11] + "</p>";
            grid.getView().getCell(index, 9).innerHTML = tmp;

            tmp = "";
            if (Config.yp == 1) tmp += "<p class=odds1>" + D[2] + "</p>";
            if (Config.op == 1) tmp += "<p class=odds2>" + D[7] + "</p>";
            if (Config.dx == 1) tmp += "<p class=odds3>" + D[10] + "</p>";
            grid.getView().getCell(index, 10).innerHTML = tmp;

            tmp = "";
            if (Config.yp == 1) tmp += "<p class=odds1>" + D[4] + "</p>";
            if (Config.op == 1) tmp += "<p class=odds2>" + D[8] + "</p>";
            if (Config.dx == 1) tmp += "<p class=odds3>" + D[12] + "</p>";
            grid.getView().getCell(index, 11).innerHTML = tmp;

            tmp = "";
            if (D[13] == "1") tmp = "<a href='Odds/runningDetail.aspx?scheduleID=" + D[0] + "' target='_blank'><img src='http://live.nowscore.com/images/t3.gif' height=10 width=10 title='有走地赛事'></a>";
            if (D[13] == "2") tmp = "<a href='Odds/runningDetail.aspx?scheduleID=" + D[0] + "' target='_blank'><img src='http://live.nowscore.com/images/t32.gif' height=10 width=10 title='正在走地'></a>";
            grid.getView().getCell(index, 12).innerHTML = tmp;

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
    oddsHttp.open("get", "Data/NowGoal/GetRemoteFile.aspx?f=xml&path=data/ch_goal" + Config.companyID + ".xml?" + Date.parse(new Date()), true);
    oddsHttp.onreadystatechange = oddsrefresh;
    oddsHttp.send(null);
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

function ChangeJS(sclassID, kind) {
    sclassID2 = sclassID;
    kind2 = kind;
    ChangeSchedule(sclassID, kind);
}
//赛程赛果
function ChangeSchedule(id, t) {
    var script = document.getElementById("scriptScsg");
    var s = document.createElement("script");
    s.type = "text/javascript";
    s.src = "Data/NowGoal/GetRemoteFile.aspx?f=infojs&path=AjaxLeague.aspx?SclassID=" + id + "&SclassType=" + (t == 1 ? "s" : "c") + "&v=" + Date.parse(new Date());
    script.removeChild(script.firstChild);
    script.appendChild(s, "script");
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
}


function SetLanguage(l, obj) {
    Ext.getCmp(obj.id).findParentByType('button').setText(obj.text);
    Config.language = l;
    LoadLiveFile();
    Config.writeCookie();
}

function SetCompany(id) {
    Ext.getCmp("CompanySelect").setText(company[id]);
    Config.companyID = id;
    LoadLiveFile();
    Config.writeCookie();
}

function SetMatchType(m, obj) {
    if (obj) {
        Ext.getCmp("MatchTypeSelect").setText(obj.text);
    }
    Ext.getDom("hiddencount").innerHTML = "0";
    Config.matchType = m;
    if (m == 0)
        Ext.getCmp("btn_all").hide();
    else
        Ext.getCmp("btn_all").show();
    LoadLiveFile();
    Config.writeCookie();
}

function ShowAllMatch() {
    var checkboxgroup = Ext.getCmp('myLeague-win').findByType("checkboxgroup")[0];
    checkboxgroup.eachItem(function (checkbox) {
        checkbox.setValue(true);
    });
    var grid = Ext.getCmp("HistoryFileGrid");
    grid.getStore().each(function (r, index) {
        grid.getView().getRow(index).style.display = "";
    });

    if (orderby == "league") {
        for (var i = 0; i < sclasscount; i++) {
            if (B[i][8] > 0) {
                document.getElementById("tr_" + i).style.display = "";
                document.getElementById("expand" + i).style.display = "none";
                document.getElementById("collapse" + i).style.display = "";
            }
        }
    }
    Ext.getDom("hiddencount").innerHTML = "0";
    hiddenID = "_";
    writeCookie("HiddenMatchID", hiddenID);
    countCheckNum();
    //makeMyCountry();
}

function SelectOtherLeague() {
    var checkboxgroup = Ext.getCmp('myLeague-win').findByType("checkboxgroup")[0];
    var grid = Ext.getCmp("HistoryFileGrid");
    var hh = 0;
    hiddenID = "_";
    checkboxgroup.items.each(function (checkbox, i) {
        if (checkbox.checked) {
            checkbox.setValue(false);
            if (orderby == "league" && B[i][8] > 0) document.getElementById("tr_" + i).style.display = "none";
            for (var j = 0; j < matchcount; j++) {
                if (A[j][1] == checkbox.indexB) {
                    var index = grid.getStore().indexOfId(A[j][0]);
                    grid.getView().getRow(index).style.display = "none";
                    if (A[j][27] != "") document.getElementById("tr2_" + A[j][0]).style.display = "none";
                    hh = hh + 1;
                }
            }
        }
        else {
            checkbox.setValue(true);
            if (orderby == "league" && B[i][8] > 0) document.getElementById("tr_" + i).style.display = "";
            for (var j = 0; j < matchcount; j++) {
                if (A[j][1] == checkbox.indexB) {
                    var index = grid.getStore().indexOfId(A[j][0]);
                    grid.getView().getRow(index).style.display = "";
                    if (A[j][27] != "") document.getElementById("tr2_" + A[j][0]).style.display = "";
                    //hiddenID=hiddenID.replace("_"+A[j][0] + "_","_")
                    hiddenID += A[j][0] + "_";
                }
            }
        }
    });
    Ext.getDom("hiddencount").innerHTML = hh;
    writeCookie("HiddenMatchID", hiddenID);
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

function ShowTeamRank() {
    for (var i = 0; i < matchcount; i++) {
        if (A[i][1] == -1) continue;
        if (A[i][21] != "") document.getElementById("horder_" + A[i][0]).innerHTML = (Config.rank == 1 ? "<font color=#444444><sup>[" + A[i][21] + "]</sup></font>" : "");
        if (A[i][22] != "") document.getElementById("gorder_" + A[i][0]).innerHTML = (Config.rank == 1 ? "<font color=#444444><sup>[" + A[i][22] + "]</sup></font>" : "");
    }
}

function ShowTeamRank() {
    for (var i = 0; i < matchcount; i++) {
        if (A[i][1] == -1) continue;
        if (A[i][21] != "") document.getElementById("horder_" + A[i][0]).innerHTML = (Config.rank == 1 ? "<font color=#444444><sup>[" + A[i][21] + "]</sup></font>" : "");
        if (A[i][22] != "") document.getElementById("gorder_" + A[i][0]).innerHTML = (Config.rank == 1 ? "<font color=#444444><sup>[" + A[i][22] + "]</sup></font>" : "");
    }
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
        if (A[i][1] != -1 && A[i][27] != "") {
            var grid = Ext.getCmp("HistoryFileGrid");
            var index = grid.getStore().indexOfId(A[i][0])
            grid.getView().getCell(index, 13).style.display = value;
        }
    //document.getElementById("tr2_" + A[i][0]).style.display = value;
}

function restoreOddsColor(matchid) {
    var grid = Ext.getCmp("HistoryFileGrid");
    var index = grid.getStore().indexOfId(matchid);
    if (index < 0) return;
    grid.getView().getCell(index, 9).innerHTML = grid.getView().getCell(index, 9).innerHTML.toLowerCase().replace(/<span class=up>/g, "").replace(/<span class=down>/g, "").replace(/<\/span>/g, "");
    grid.getView().getCell(index, 10).innerHTML = grid.getView().getCell(index, 10).innerHTML.toLowerCase().replace(/<span class=up>/g, "").replace(/<span class=down>/g, "").replace(/<\/span>/g, "");
    grid.getView().getCell(index, 11).innerHTML = grid.getView().getCell(index, 11).innerHTML.toLowerCase().replace(/<span class=up>/g, "").replace(/<span class=down>/g, "").replace(/<\/span>/g, "");
}

function timecolors(matchid) {
    try {
        var grid = Ext.getCmp("HistoryFileGrid");
        var index = grid.getStore().indexOfId(matchid);
        grid.getView().getCell(index, 4).style.backgroundColor = "";
        grid.getView().getCell(index, 6).style.backgroundColor = "";
    }
    catch (e) { }
}

function MoveToBottom(m) {
    try {
        var grid = Ext.getCmp("HistoryFileGrid");
        var index = grid.getStore().indexOfId(m);
        var trcontent = grid.getView().getRow(index).outerHTML;
        var record = grid.getStore().getAt(index);
        grid.getStore().removeAt(index);
        grid.getStore().add(record);
        grid.getView().getRow(grid.getStore().getCount() - 1).outerHTML = trcontent;
    } catch (e) { }
}

function LoadLiveFile(date) {
    var allDate = document.getElementById("allDate");
    var s = document.createElement("script");
    s.type = "text/javascript";
    window.clearTimeout(LoadLiveFileTimer);
    if (date != undefined) {
        islive = false;
        s.src = "http://live.nowscore.com/data/score.aspx?date=" + date;
        window.clearTimeout(LoadLiveFileTimer);
    } else {
        islive = true;
        s.src = "http://live.nowscore.com/data/bf.js?" + Date.parse(new Date());
        LoadLiveFileTimer = window.setTimeout("LoadLiveFile()", 3600 * 1000);
    }
    allDate.removeChild(allDate.firstChild);
    allDate.appendChild(s, "script");
}

function CheckFunction(obj) {
    if (document.getElementById(obj).checked) eval("Config." + obj + "=1");
    else eval("Config." + obj + "=0");
    Config.writeCookie();
    if (obj = "yp" || obj == "op" || obj == "dx") LoadLiveFile();
}

function check() {
    if (oldUpdateTime == lastUpdateTime && oldUpdateTime != "") {
        if (confirm("由于程序忙，或其他网络问题，你已经和服务器断开连接超过 5 分钟，是否要重新连接观看比分？")) window.location.reload();
    }
    oldUpdateTime = lastUpdateTime;
    if (islive) {
        window.setTimeout("check()", 300000);
    }
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

function MakeHistoryTable() {
    var state, bg = "";
    var H_redcard, G_redcard, H_yellow, G_yellow;

    var html = new Array();
    var scheduleData = [];
    for (var i = 0; i < matchcount; i++) {
        try {
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
                    match_score = "-";
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
            var rowData = {};
            rowData.scheduleid = A[i][0];
            rowData.index = i;
            rowData.league = B[A[i][1]][1 + Config.language];
            rowData.bgcolor = B[A[i][1]][4];
            rowData.matchdate = A[i][10];

            if (state == "-1")
                classx2 = "td_scoreR";
            else
                classx2 = "td_score";
            rowData.state = state_ch[state + 14].split(",")[Config.language];
            rowData.h_team = H_yellow + H_redcard + A[i][4 + Config.language] + (A[i][21] == "" ? "" : "<font color=#888888>[" + A[i][21] + "]</font>");
            rowData.goal = match_score;
            rowData.g_team = A[i][7 + Config.language] + "" + G_redcard + "" + G_yellow + (A[i][22] == "" ? "" : "<font color=#888888>[" + A[i][22] + "]</font>");
            rowData.match_half = match_half;
            rowData.classx2 = classx2;
            rowData.pankou = Goal2GoalCn(A[i][25]);

            var goalResult = "";
            if (A[i][25] != null && match_score != "-") {
                var homeScore = parseInt(A[i][13]);
                var guestScore = parseInt(A[i][14]);
                var goal = parseFloat(A[i][25]);
                var numResult = homeScore - guestScore - goal;
                if (numResult > 0) {
                    if (numResult == 0.25)
                        goalResult = "<font color='red'>赢半</font>";
                    else
                        goalResult = "<font color='red'>赢</font>";
                }
                else if (numResult == 0)
                    goalResult = "<font color='blue'>走</font>";
                else {
                    if (numResult == -0.25)
                        goalResult = "<font color='green'>输半</font>";
                    else
                        goalResult = "<font color='green'>输</font>";
                }
            }
            rowData.g_odds = goalResult;
            rowData.data = "<a href='javascript:' onclick=analysis(" + A[i][0] + ") title='数据分析'>析</a> <a href=javascript: onclick=\"AsianOdds(" + A[i][0] + ");return false\" title='11家指数'>亚</a> <a href='javascript:EuropeOdds(" + A[i][0] + ")' title='百家欧赔'>欧</a>";
            if (A[i][24] == "True")
                rowData.zoudi = "<a href='Odds/runningDetail.aspx?scheduleID=" + A[i][0] + "' target='_blank'><img src='http://live.nowscore.com/images/t3.gif' height=10 width=10 title='走地'></a>";
            

            if (A[i][27] + A[i][28] == "") classx = "none"; else classx = "";
            //rowData.other = showExplain(A[i][28], A[i][4 + Config.language], A[i][7 + Config.language]) + (A[i][28] != "" && A[i][27] != "" ? "<br>" + A[i][27] : A[i][27] != "" ? A[i][27] : "");

            scheduleData.push(rowData);
        } catch (e) { }
    }
    //document.getElementById("ScoreDiv").innerHTML = html.join("");
    var grid = Ext.getCmp("HistoryFileGrid");
    grid.getStore().loadData(scheduleData);

//    //联赛/杯赛名列表
//    var leaguehtml = new Array();
//    leaguehtml.push("<ul id='checkboxleague'>");
//    for (var i = 0; i < sclasscount; i++) {
//        if (B[i][8] > 0)
//            leaguehtml.push("<li><input onclick='CheckLeague(" + i + ")' checked type=checkbox id='checkboxleague_" + i + "' value=" + i + "><label style='cursor:pointer' for='checkboxleague_" + i + "'>" + B[i][1 + Config.language] + "<font color=#990000>[" + B[i][8] + "]</font></label></li>");
//    }
//    leaguehtml.push("</ul>");
//    document.getElementById("myleague").innerHTML = leaguehtml.join("");

//    //国家列表
//    var country = new Array();
//    for (var i = 0; i < C.length; i++) {
//        if (C[i][2] > 0) country.push("<li><a href='javascript:CheckCountry(" + C[i][0] + ")' id='country_" + C[i][0] + "'>" + C[i][1] + "</a></li>");
//    }
//    document.getElementById("countryList").innerHTML = country.join("");
}