var GoalCn1 = ["0", "0/0.5", "0.5", "0.5/1", "1", "1/1.5", "1.5", "1.5/2", "2", "2/2.5", "2.5", "2.5/3", "3", "3/3.5", "3.5", "3.5/4", "4", "4/4.5", "4.5", "4.5/5", "5", "5/5.5", "5.5", "5.5/6", "6", "6/6.5", "6.5", "6.5/7", "7", "7/7.5", "7.5", "7.5/8", "8", "8/8.5", "8.5", "8.5/9", "9", "9/9.5", "9.5", "9.5/10", "10", "10/10.5", "10.5", "10.5/11", "11", "11/11.5", "11.5", "11.5/12", "12", "12/12.5", "12.5", "12.5/13", "13", "13/13.5", "13.5", "13.5/14", "14"];
var GoalCn2 = ["0", "0/-0.5", "-0.5", "-0.5/-1", "-1", "-1/-1.5", "-1.5", "-1.5/-2", "-2", "-2/-2.5", "-2.5", "-2.5/-3", "-3", "-3/-3.5", "-3.5", "-3.5/-4", "-4", "-4/-4.5", "-4.5", "-4.5/-5", "-5", "-5/-5.5", "-5.5", "-5.5/-6", "-6", "-6/-6.5", "-6.5", "-6.5/-7", "-7", "-7/-7.5", "-7.5", "-7.5/-8", "-8", "-8/-8.5", "-8.5", "-8.5/-9", "-9", "-9/-9.5", "-9.5", "-9.5/-10", "-10"];
var week = new Array("(日)", "(一)", "(二)", "(三)", "(四)", "(五)", "(六)");

var GoalCn = "平手,平/半,半球,半/一,一球,一/球半,球半,球半/两,两球,两/两球半,两球半,两球半/三,三球,三/三球半,三球半,三球半/四,四球,四/四球半,四球半,四球半/五球,五球,五/五球半,五球半,五球半/六,六球,六球/六球半,六球半,六球半/七球,七球,七球/七球半,七球半,七球半/八球,八球,八球/八球半,八球半,八球半/九球,九球,九球/九球半,九球半,九球半/十球,十球".split(",");

function Goal2GoalCn(goal) { //数字盘口转汉汉字	
    if (goal == null || goal + "" == "")
        return "";
    else {
        if (goal >= 0) return GoalCn[parseInt(goal * 4)];
        else return "受" + GoalCn[Math.abs(parseInt(goal * 4))];
    }
}

function Goal2GoalCn2(goal) {
    if (goal == "")
        return "";
    else {
        if (goal >= 0) return GoalCn1[parseInt(goal * 4)];
        else return GoalCn2[Math.abs(parseInt(goal * 4))];
    }
}

function round(a, b) {
    return (Math.round(a * Math.pow(10, b)) * Math.pow(10, -b)).toFixed(2); 
}

function BgColor(odds1, odds2) {
    var bg = "normal";
    if (odds1 < odds2) bg = "up";
    if (odds1 > odds2) bg = "down";
    return bg;
}
function TdBgColor(odds1, odds2) {
    var bg = "";
    if (odds1 < odds2) bg = "green";
    if (odds1 > odds2) bg = "red";
    return bg;
}

var state_ch = Array(17);
state_ch[0] = "推迟,推遲,Postp.";
state_ch[1] = "中断,中斷,Pause";
state_ch[2] = "腰斩,腰斬,Abd";
state_ch[3] = "待定,待定,Pending";
state_ch[13] = "<font color=red>完</font>,<font color=red>完</font>,<font color=red>Ft</font>";
state_ch[14] = ",,";
state_ch[15] = "上,上,Part1";
state_ch[16] = "<font color=blue>中</font>,<font color=blue>中</font>,<font color=blue>HT</font>";
state_ch[17] = "下,下,Part2";


var company = new Array(35);
company[0] = "足彩";
company[1] = "澳彩";
company[2] = "波音";
company[3] = "ＳＢ";
company[4] = "立博";
company[5] = "云鼎";
company[7] = "SNAI";
company[8] = "Bet365";
company[9] = "威廉";
company[12] = "易胜博";
company[14] = "韦德";
company[15] = "SSP";
company[17] = "明陞";
company[18] = "Eurobet";
company[19] = "Interwetten";
company[22] = "10bet";
company[23] = "金宝博";
company[24] = "12bet";
company[29] = "乐天堂";
company[31] = "利记";
company[33] = "永利高"; 



var riseColor = "#FFB0B0";
var fallColor = "#00FF44";
var changePkColor = "#D06666";
var nofityTimer = "";
var oldLevel = -1;
var selDate = "";
var matchType = 0;

//定义namespace
var _glodds = new Object();
//公共变量
_glodds.SplitDomain = "$";
_glodds.SplitRecord = ";";
_glodds.SplitColumn = ",";


//通用列表类
_glodds.List = function() {
    this.items = new Array();
    this.keys = new Object();

    this.Add = function(key, value) {
        if (typeof (key) != "undefined") {
            var vv = typeof (value) == "undefined" ? null : value;
            var idx = this.keys[key];
            if (idx == null) {
                idx = this.items.length;
                this.keys[key] = idx;
            }
            this.items[idx] = vv;
        }
    }

    this.Get = function(key) {
        var idx = this.keys[key];
        if (idx != null)
            return this.items[idx];
        return null;
    }

    this.Clear = function() {
        for (var k in this.keys) {
            delete this.keys[k];
        }
        delete this.keys;
        this.keys = null;
        this.keys = new Object();

        for (var i = 0; i < this.items.length; i++) {
            delete this.items(i);
        }
        delete this.items;
        this.items = null;
        this.items = new Array();
    }
}


//联赛项类
_glodds.League = function(infoStr) {
    var infoArr = infoStr.split(_glodds.SplitColumn);
    this.lId = infoArr[0];
    this.type = infoArr[1];
    this.color = infoArr[2];
    this.cnName = infoArr[3];
    this.trName = infoArr[4];
    this.enName = infoArr[5];
    this.url = infoArr[6];
    this.matchNum = 0;
    this.show = true;

    this.getName = function() {
        if (lang == "2")
            return this.enName;
        else if (lang == "1")
            return this.trName;
        else
            return this.cnName;
    }
}


//比赛项类
_glodds.Match = function(infoStr) {
    var infoArr = infoStr.split(_glodds.SplitColumn); //265454,539,2009-5-6 23:00:00,,6734,学生体育,學生體育,Sportul Studentesc,14,6730,德尔塔,德爾塔,Delta Tulcea,2,0,0,0,,False;
    this.mId = infoArr[0];
    this.lId = infoArr[1];
    this.time = new Date(parseInt(infoArr[2]));
    if (infoArr[3] != "") this.time2 = new Date(parseInt(infoArr[3]));
    this.t1Id = infoArr[4];
    this.t1CnName = infoArr[5];
    this.t1TrName = infoArr[6];
    this.t1EnName = infoArr[7];
    this.t1Position = infoArr[8] != "" ? "[" + infoArr[8] + "]" : "";
    this.t2Id = infoArr[9];
    this.t2CnName = infoArr[10];
    this.t2TrName = infoArr[11];
    this.t2EnName = infoArr[12];
    this.t2Position = infoArr[13] != "" ? "[" + infoArr[13] + "]" : "";
    this.state = infoArr[14];
    this.homeScore = infoArr[15];
    this.guestScore = infoArr[16];
    this.tv = infoArr[17];
    this.flag = "";
    if (infoArr[18] == "True") this.flag = "(中)";
    this.level = infoArr[19];

    this.getT1Name = function() {
        if (lang == "2")
            return this.t1EnName;
        else if (lang == "1")
            return this.t1TrName;
        else
            return this.t1CnName;
    }

    this.getT2Name = function() {
        if (lang == "2")
            return this.t2EnName;
        else if (lang == "1")
            return this.t2TrName;
        else
            return this.t2CnName;
    }
}


//亚赔信息
_glodds.OddsAsian = function(infoStr) {
    var infoArr = infoStr.split(_glodds.SplitColumn); //209092,8,0.5,0.95,0.95,0.5,1.025,0.875,False,False;
    this.mId = infoArr[0];
    this.cId = infoArr[1];
    this.goalF = infoArr[2];
    this.homeF = infoArr[3];
    this.awayF = infoArr[4];
    this.goal = infoArr[5];
    this.home = infoArr[6];
    this.away = infoArr[7];
    this.close = infoArr[8];
    this.zoudi = infoArr[9];
}
//欧赔信息
_glodds.Odds1x2 = function(infoStr) {
    var infoArr = infoStr.split(_glodds.SplitColumn); //209092,8,2.25,3.95,2.95,2.25,3.025,2.875
    this.mId = infoArr[0];
    this.cId = infoArr[1];
    this.hwF = infoArr[2];
    this.stF = infoArr[3];
    this.awF = infoArr[4];
    this.hw = infoArr[5];
    this.st = infoArr[6];
    this.aw = infoArr[7];
}
//大小赔率信息
_glodds.OddsOU = function(infoStr) {
    var infoArr = infoStr.split(_glodds.SplitColumn); //209092,8,0.5,0.95,0.95,0.5,1.025,0.875
    this.mId = infoArr[0];
    this.cId = infoArr[1];
    this.goalF = infoArr[2];
    this.overF = infoArr[3];
    this.underF = infoArr[4];
    this.goal = infoArr[5];
    this.over = infoArr[6];
    this.under = infoArr[7];
}


var _oddsUitl = new Object();
var matchdata = new Object();

_oddsUitl.getDayStr = function(dt) {
    return (dt.getMonth() + 1) + "-" + dt.getDate();
}

_oddsUitl.getTimeStr = function(dt) {
    return dt.getHours() + ":" + (dt.getMinutes() < 10 ? "0" : "") + dt.getMinutes();
}

_oddsUitl.getDtStr = function(dt) {
    return (dt.getMonth() + 1) + "-" + dt.getDate() + " " + (dt.getHours() < 10 ? "0" : "") + dt.getHours() + ":" + (dt.getMinutes() < 10 ? "0" : "") + dt.getMinutes();
}

_oddsUitl.getDateTimeStr = function(dt) {
    return dt.getFullYear() + "-" + (dt.getMonth() + 1) + "-" + dt.getDate() + " " + (dt.getHours() < 10 ? "0" : "") + dt.getHours() + ":" + (dt.getMinutes() < 10 ? "0" : "") + dt.getMinutes();
}

_oddsUitl.getDate = function(str) {
    var p = str.split("-");
    return new Date(p[0], parseInt(p[1], 10) - 1, p[2]);
}

function getCookie(name) {
    var cname = name + "=";
    var dc = document.cookie;
    if (dc.length > 0) {
        begin = dc.indexOf(cname);
        if (begin != -1) {
            begin += cname.length;
            end = dc.indexOf(";", begin);
            if (end == -1) end = dc.length;
            return dc.substring(begin, end);
        }
    }
    return null;
}
function writeCookie(name, value) {
    var expire = "";
    var hours = 365;
    expire = new Date((new Date()).getTime() + hours * 3600000);
    expire = ";path=/;expires=" + expire.toGMTString();
    document.cookie = name + "=" + value + expire;
}