/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />

var Odds1x2Mail = function (scheduleid) {
    var scheduleArr, scheduleTypeArr;
    oddsHttp.open("get", "Data/NowGoal/GetRemoteFile.aspx?f=oddsjs&path=" + scheduleid + ".js", false);
    oddsHttp.send(null);
    eval(oddsHttp.responseText);
    oddsArr = [];
    for (var i = 0; i < game.length; i++) {
        var arr = game[i].split('|');
        if (getDate(MatchTime) - getDate(arr[20]) < 60000 * 60) {
            oddsArr.push(game[i]);
        }
    }
    for (var i = 0; i < A.length; i++) {
        if (A[i][0] == scheduleid) {
            scheduleArr = A[i];
            scheduleTypeArr = B[A[i][1]];
        }
    }
    Ext.Ajax.request({
        url: 'Data/NowGoal/GetOdds1x2History.aspx?a=statmail',
        params: {
            stypeid: scheduleTypeArr.join('^'),
            oddsarr: oddsArr.join('^'),
            schedulearr: scheduleArr.join('^'),
            odds: Ext.getDom("tr1_" + scheduleid).getAttribute("odds")
        }
    });
}