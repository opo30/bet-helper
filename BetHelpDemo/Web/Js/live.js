/// <reference path="../Lib/ext/adapter/ext/ext-base-debug.js" />
/// <reference path="../Lib/ext/ext-all-debug.js" />

Ext.ns('HistoryScore');


HistoryScore.hiddenID = "_";

HistoryScore.A = Array();
HistoryScore.B = Array();
HistoryScore.C = Array();
HistoryScore.matchcount = 0;
HistoryScore.sclasscount = 0;
HistoryScore.countrycount = 0;
HistoryScore.matchdate = "";
HistoryScore.oddsData = "";

HistoryScore.ShowBf = function () {
    if (Ext.getCmp("MatchTypeSelect") != null) {
        Ext.getCmp("MatchTypeSelect").setText(Ext.getCmp("MatchTypeSelect").menu.items.itemAt(Config.matchType).text);
    }
    if (Ext.getCmp("LanguageSelect") != null) {
        Ext.getCmp("LanguageSelect").setText(Ext.getCmp("LanguageSelect").menu.items.itemAt(Config.language).text);
    }

    if (hiddenID == null) hiddenID = "";
    var state, bg = "";
    var H_redcard, G_redcard, H_yellow, G_yellow;

    var html = new Array();
    var scheduleData = [];
    for (var i = 0; i < this.matchcount; i++) {
        try {
            this.B[this.A[i][1]][8]++;
            for (var j = 0; j < this.C.length; j++) {
                if (this.B[this.A[i][1]][9] == this.C[j][0]) {
                    this.C[j][2]++;
                    break;
                }
            }

            state = parseInt(this.A[i][12]);
            switch (state) {
                case 0:
                    match_score = "-";
                    match_half = "-";
                    break;
                case 1:
                    match_score = this.A[i][13] + "-" + this.A[i][14];
                    match_half = "-";
                    break;
                case -11:
                case -14:
                    match_score = "";
                    match_half = "";
                    break;
                default:
                    match_score = this.A[i][13] + "-" + this.A[i][14];
                    if (this.A[i][15] == null) this.A[i][15] = "";
                    if (this.A[i][16] == null) this.A[i][16] = "";
                    match_half = this.A[i][15] + "-" + this.A[i][16];
                    break;
            }
            if (this.A[i][17] != "0") H_redcard = "<img src='images/redcard" + this.A[i][17] + ".gif'>"; else H_redcard = "";
            if (this.A[i][18] != "0") G_redcard = "<img src='images/redcard" + this.A[i][18] + ".gif'>"; else G_redcard = "";
            if (this.A[i][19] != "0") H_yellow = "<img src='images/yellow" + this.A[i][19] + ".gif'>"; else H_yellow = "";
            if (this.A[i][20] != "0") G_yellow = "<img src='images/yellow" + this.A[i][20] + ".gif'>"; else G_yellow = "";

            if (bg != "ts1") bg = "ts1"; else bg = "ts2";

            var rowData = {};
            rowData.scheduleid = this.A[i][0];
            rowData.index = i;
            rowData.league = this.B[this.A[i][1]][1 + Config.language];
            rowData.bgcolor = this.B[this.A[i][1]][4];
            rowData.matchdate = this.A[i][10];
            if (Config.matchType == 0 || (Config.matchType == 1 && this.B[this.A[i][1]][5] == "1") || (Config.matchType == 2 && this.A[i][25] != null)) {
                rowData.display = "";
            } else {
                rowData.display = "none";
            }
            if (state == "-1")
                classx2 = "td_scoreR";
            else
                classx2 = "td_score";
            rowData.state = state_ch[state + 14].split(",")[Config.language];
            rowData.h_team = H_yellow + H_redcard + this.A[i][4 + Config.language] + (this.A[i][21] == "" ? "" : "<font color=#888888>[" + this.A[i][21] + "]</font>");
            rowData.goal = match_score;
            rowData.g_team = this.A[i][7 + Config.language] + "" + G_redcard + "" + G_yellow + (this.A[i][22] == "" ? "" : "<font color=#888888>[" + this.A[i][22] + "]</font>");
            rowData.match_half = match_half;
            rowData.classx2 = classx2;

            rowData.pankou = Goal2GoalCn(this.A[i][25]);

            var goalResult = "";
            if (this.A[i][25] != null && match_score != "-") {
                var homeScore = parseInt(this.A[i][13]);
                var guestScore = parseInt(this.A[i][14]);
                var goal = parseFloat(this.A[i][25]);
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
            rowData.data = "<a href='javascript:' onclick=analysis(" + this.A[i][0] + ") title='数据分析'>析</a> <a href=javascript: onclick=\"AsianOdds(" + this.A[i][0] + ");return false\" title='11家指数'>亚</a> <a href='javascript:EuropeOdds(" + this.A[i][0] + ")' title='百家欧赔'>欧</a> <a href='javascript:Odds1x2Mail1(" + this.A[i][0] + ")' title='现'>现</a> <a href='javascript:Odds1x2Mail(" + this.A[i][0] + ")' title='邮'>邮</a>";
            if (this.A[i][24] == "True")
                rowData.zoudi = "<a href='Odds/runningDetail.aspx?scheduleID=" + this.A[i][0] + "' target='_blank'><img src='http://live.nowodds.com/images/t3.gif' height=10 width=10 title='走地'></a>";


            if (this.A[i][27] + this.A[i][28] == "") classx = "none"; else classx = "";
            //rowData.other = showExplain(this.A[i][28], this.A[i][4 + Config.language], this.A[i][7 + Config.language]) + (this.A[i][28] != "" && this.A[i][27] != "" ? "<br>" + this.A[i][27] : this.A[i][27] != "" ? this.A[i][27] : "");

            scheduleData.push(rowData);
        } catch (e) { }
    }

    var grid = Ext.getCmp("HistoryFileGrid");
    grid.getStore().loadData(scheduleData);
}


HistoryScore.showodds = function (oddsDataStr) {
    try {
        var D = new Array();
        var odds, old = new Array();
        var grid = Ext.getCmp("HistoryFileGrid");
        var oddsData = oddsDataStr.split("$");
        //for (var i = 2; i < oddsData.length; i++) {
            var oddsArray = oddsData[2].split(';');
            Ext.each(oddsArray, function (odds) {
                D = odds.split(",");

                tr = document.getElementById("htr1_" + D[0]);
                if (tr != null && D[1] == Config.companyID) {
                    var index = tr.getAttribute("index");
                    grid.getView().getCell(index, 9).innerHTML = "<p class=odds1>" + D[3] + "</p><p class=odds2>" + D[6] + "</p>";
                    grid.getView().getCell(index, 10).innerHTML = "<p class=odds1>" + Goal2GoalCn(D[2]) + "</p><p class=odds2>" + Goal2GoalCn(D[5]) + "</p>";
                    grid.getView().getCell(index, 11).innerHTML = "<p class=odds1>" + D[4] + "</p><p class=odds2>" + D[7] + "</p>";
                    

                    //                    if (old.length == 14 && old != odds && old[10] != "") {
                    //                        if (D[10] > old[10]) D[10] = "<span class=up>" + Goal2GoalCn2(D[10]) + "</span>";
                    //                        else if (D[10] < old[10]) D[10] = "<span class=down>" + Goal2GoalCn2(D[10]) + "</span>";
                    //                        else D[10] = Goal2GoalCn2(D[10]);
                    //                    }
                    //                    else D[10] = Goal2GoalCn2(D[10]);

                    //                    var tmp = "";
                    //                    if (Config.yp == 1) tmp += "<p class=odds1>" + D[3] + "</p>";
                    //                    if (Config.op == 1) tmp += "<p class=odds2>" + D[6] + "</p>";
                    //                    if (Config.dx == 1) tmp += "<p class=odds3>" + D[11] + "</p>";
                    //                    tr.cells[9].innerHTML = tmp;

                    //                    tmp = "";
                    //                    if (Config.yp == 1) tmp += "<p class=odds1>" + D[2] + "</p>";
                    //                    if (Config.op == 1) tmp += "<p class=odds2>" + D[7] + "</p>";
                    //                    if (Config.dx == 1) tmp += "<p class=odds3>" + D[10] + "</p>";
                    //                    tr.cells[10].innerHTML = tmp;

                    //                    tmp = "";
                    //                    if (Config.yp == 1) tmp += "<p class=odds1>" + D[4] + "</p>";
                    //                    if (Config.op == 1) tmp += "<p class=odds2>" + D[8] + "</p>";
                    //                    if (Config.dx == 1) tmp += "<p class=odds3>" + D[12] + "</p>";
                    //                    tr.cells[11].innerHTML = tmp;

                    //                    tmp = "";
                    //                    if (D[13] == "1") tmp = "<a href='Odds/runningDetail.aspx?scheduleID=" + D[0] + "' target='_blank'><img src='http://live.nowodds.com/images/t3.gif' height=10 width=10 title='有走地赛事'></a>";
                    //                    if (D[13] == "2") tmp = "<a href='Odds/runningDetail.aspx?scheduleID=" + D[0] + "' target='_blank'><img src='http://live.nowodds.com/images/t32.gif' height=10 width=10 title='正在走地'></a>";
                    //                    tr.cells[12].innerHTML = tmp;

                    //                    tr.attributes["odds"].value = odds;
                }
            });
        //}
    } catch (e) { alert(e) }
}

HistoryScore.SetLanguage = function (l, obj) {
    Ext.getCmp(obj.id).findParentByType('button').setText(obj.text);
    Config.language = l;
    LoadLiveFile();
    this.LoadLiveFile(this.matchdate);
    Config.writeCookie();
}

HistoryScore.SetCompany = function (id) {
    Ext.getCmp("CompanySelect").setText(company[id]);
    Config.companyID = id;
    LoadLiveFile();
    Config.writeCookie();
}

HistoryScore.SetMatchType = function (m, obj) {
    if (obj) {
        Ext.getCmp("MatchTypeSelect").setText(obj.text);
    }
    var s = Ext.getCmp("HistoryFileGrid").getStore();
    for (var i = 0; i < this.matchcount; i++) {
        var display = "none";
        if (m == 0 || (m == 1 && this.B[this.A[i][1]][5] == "1") || (m == 2 && this.A[i][25] != null)) {
            display = "";
        }
        Ext.getCmp("HistoryFileGrid").getView().getRow(i).style.display = display;
    }
}

HistoryScore.ShowAllMatch=function() {
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

HistoryScore.SelectOtherLeague=function() {
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

HistoryScore.LoadLiveFile = function () {
    if (Ext.getCmp("datefield")!= null && Ext.getCmp("datefield").getValue()) {
        HistoryScore.matchdate = Ext.getCmp("datefield").getValue().format("Y-m-d");
    } else {
        HistoryScore.matchdate = new Date().format("Y-m-d");
    }
    Ext.Ajax.request({
        url: "Data/NowGoal/GetRemoteFile.aspx?f=rootjs&path=data/score.aspx?date=" + HistoryScore.matchdate,
        success: function (res) {
            var jsdata = res.responseText;
            if (jsdata) {
                jsdata = jsdata.replace(new RegExp('var ', 'g'), "HistoryScore.");
                jsdata = jsdata.replace(new RegExp('A[[]', 'g'), "HistoryScore.A[");
                jsdata = jsdata.replace(new RegExp('B[[]', 'g'), "HistoryScore.B[");
                jsdata = jsdata.replace(new RegExp('C[[]', 'g'), "HistoryScore.C[");
                jsdata = jsdata.replace("ShowBf()", "HistoryScore.ShowBf()");
                eval(jsdata);
            }
        }
    });
};
