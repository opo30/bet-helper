/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />

var Odds1x2Mail = function (scheduleid) {
    if (!issendmail) {
        showNotify("提示", "请先开启邮件提醒功能！", false);
        return;
    }
    var scheduleArr, scheduleTypeArr;
    var minite = 60 * 3;
    oddsHttp.open("get", "Data/NowGoal/GetRemoteFile.aspx?f=oddsjs&path=" + scheduleid + ".js", false);
    oddsHttp.send(null);
    if (oddsHttp.responseText == "") {
        showNotify("提示", "目前没有欧赔数据", false);
        return;
    }
    eval(oddsHttp.responseText);
    oddsArr = [];
    for (var i = 0; i < game.length; i++) {
        var arr = game[i].split('|');
        if (getDate(MatchTime) - getDate(arr[20]) < 60000 * minite) {
            oddsArr.push(game[i]);
        }
    }
    if (oddsArr.length == 0) {
        showNotify("提示", "临场" + minite + "分钟内没有开出赔率！", false);
        return;
    }
    for (var i = 0; i < A.length; i++) {
        if (A[i][0] == scheduleid) {
            scheduleArr = A[i];
            scheduleTypeArr = B[A[i][1]];
        }
    }
    if (scheduleArr == undefined) {
        for (var i = 0; i < HistoryScore.A.length; i++) {
            if (HistoryScore.A[i][0] == scheduleid) {
                scheduleArr = HistoryScore.A[i];
                scheduleTypeArr = HistoryScore.B[HistoryScore.A[i][1]];
            }
        }
    }
    Ext.Ajax.request({
        url: 'Data/SendMessage.aspx?a=mail',
        params: {
            stypeid: scheduleTypeArr.join('^'),
            oddsarr: oddsArr.join('^'),
            schedulearr: scheduleArr.join('^'),
            odds: Ext.getDom("tr1_" + scheduleid) ? Ext.getDom("tr1_" + scheduleid).getAttribute("odds") : ""
        }
    });
}

var Odds1x2Mail1 = function (scheduleid) {
    var scheduleArr, scheduleTypeArr;
    var minite = 60 * 3;
    oddsHttp.open("get", "Data/NowGoal/GetRemoteFile.aspx?f=oddsjs&path=" + scheduleid + ".js", false);
    oddsHttp.send(null);
    if (oddsHttp.responseText == "") {
        showNotify("提示", "目前没有欧赔数据", false);
        return;
    }
    eval(oddsHttp.responseText);
    oddsArr = [];
    for (var i = 0; i < game.length; i++) {
        var arr = game[i].split('|');
        if (getDate(MatchTime) - getDate(arr[20]) < 60000 * minite) {
            oddsArr.push(game[i]);
        }
    }
    if (oddsArr.length == 0) {
        showNotify("提示", "临场" + minite + "分钟内没有开出赔率！", false);
        return;
    }
    for (var i = 0; i < A.length; i++) {
        if (A[i][0] == scheduleid) {
            scheduleArr = A[i];
            scheduleTypeArr = B[A[i][1]];
        }
    }
    if (scheduleArr == undefined) {
        for (var i = 0; i < HistoryScore.A.length; i++) {
            if (HistoryScore.A[i][0] == scheduleid) {
                scheduleArr = HistoryScore.A[i];
                scheduleTypeArr = HistoryScore.B[HistoryScore.A[i][1]];
            }
        }
    }
    var win = new Ext.Window({
        layout: 'fit',
        closeAction: 'destroy',
        title: scheduleTypeArr[1] + " " + scheduleArr[4] + "-" + scheduleArr[7],
        autoLoad: {
            url: 'Data/NowGoal/GetOdds1x2History.aspx?a=stat1',
            params: {
                stypeid: scheduleTypeArr.join('^'),
                oddsarr: oddsArr.join('^'),
                schedulearr: scheduleArr.join('^'),
                odds: Ext.getDom("tr1_" + scheduleid)?Ext.getDom("tr1_" + scheduleid).getAttribute("odds"):""
            }
        },
        width: 960,
        height: 500,
        resizeable: true,
        autoScroll: true
    });
    win.show();
}
