/// <reference path="../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../lib/ext/ext-all-debug.js" />

Ext.ns("YzBet.today");

YzBet.today = {
    index: 0,
    ShowBf: function () {
        if (hiddenID == null) hiddenID = "";
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
                if (Config.matchType == 0 || (Config.matchType == 1 && B[A[i][1]][5] == "1") || (Config.matchType == 2 && A[i][25] != null)) {
                    rowData.display = "";
                } else {
                    rowData.display = "none";
                }
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
                rowData.data = "<a href='javascript:' onclick=analysis(" + A[i][0] + ") title='数据分析'>析</a> <a href=javascript: onclick=\"AsianOdds(" + A[i][0] + ");return false\" title='11家指数'>亚</a> <a href='javascript:EuropeOdds(" + A[i][0] + ")' title='百家欧赔'>欧</a> <a href='javascript:Odds1x2Mail1(" + A[i][0] + ")' title='现'>现</a> <a href='javascript:Odds1x2Mail(" + A[i][0] + ")' title='邮'>邮</a>";
                if (A[i][24] == "True")
                    rowData.zoudi = "<a href='Odds/runningDetail.aspx?scheduleID=" + A[i][0] + "' target='_blank'><img src='http://live.nowodds.com/images/t3.gif' height=10 width=10 title='走地'></a>";


                if (A[i][27] + A[i][28] == "") classx = "none"; else classx = "";
                //rowData.other = showExplain(A[i][28], A[i][4 + Config.language], A[i][7 + Config.language]) + (A[i][28] != "" && A[i][27] != "" ? "<br>" + A[i][27] : A[i][27] != "" ? A[i][27] : "");

                scheduleData.push(rowData);
            } catch (e) { }
        }

        var grid = Ext.getCmp("ShowTodayGrid");
        grid.getStore().loadData(scheduleData);
    },
    showodds: function (oddsDataStr) {
        try {
            var D = new Array();
            var odds, old = new Array();
            var grid = Ext.getCmp("ShowTodayGrid");
            var oddsData = oddsDataStr.split("$");
            //for (var i = 2; i < oddsData.length; i++) {
            var oddsArray = oddsData[2].split(';');
            Ext.each(oddsArray, function (odds) {
                D = odds.split(",");

                tr = document.getElementById("ttr1_" + D[0]);
                if (tr != null && D[1] == Config.companyID) {
                    var index = tr.getAttribute("index");
                    grid.getView().getCell(index, 9).innerHTML = "<p class=odds1>" + D[3] + "</p><p class=odds2>" + D[6] + "</p>";
                    grid.getView().getCell(index, 10).innerHTML = "<p class=odds1>" + Goal2GoalCn(D[2]) + "</p><p class=odds2>" + Goal2GoalCn(D[5]) + "</p>";
                    grid.getView().getCell(index, 11).innerHTML = "<p class=odds1>" + D[4] + "</p><p class=odds2>" + D[7] + "</p>";

                }
            });
        } catch (e) { alert(e) }
    },
    loadData: function (rows) {
        if (rows[this.index] == null) {
            return;
        }
        var scheduleid = rows[this.index].get("scheduleid");
        oddsHttp.open("get", "Data/NowGoal/GetRemoteFile.aspx?f=oddsjs&path=" + scheduleid + ".js", false);
        oddsHttp.send(null);
        if (oddsHttp.responseText == "") {
            YzBet.today.index++;
            YzBet.today.loadData(rows);
        }
        eval(oddsHttp.responseText);
        oddsArr = [];
        for (var i = 0; i < game.length; i++) {
            var arr = game[i].split('|');
            oddsArr.push(game[i]);
        }
        for (var i = 0; i < A.length; i++) {
            if (A[i][0] == scheduleid) {
                scheduleArr = A[i];
                scheduleTypeArr = B[A[i][1]];
            }
        }
        Ext.Ajax.request({
            url: 'Data/NowGoal/GetOdds1x2History.aspx?a=today',
            params: {
                stypeid: scheduleTypeArr.join('^'),
                oddsarr: oddsArr.join('^'),
                schedulearr: scheduleArr.join('^'),
                odds: Ext.getDom("tr1_" + scheduleid) ? Ext.getDom("tr1_" + scheduleid).getAttribute("odds") : ""
            },
            success: function (res) {
                var result = Ext.decode(res.responseText);
                rows[YzBet.today.index].set("pan", result.pan);
                rows[YzBet.today.index].set("ppan", result.ppan);
                rows[YzBet.today.index].set("maxw", result.maxw);
                rows[YzBet.today.index].set("maxd", result.maxd);
                rows[YzBet.today.index].set("maxl", result.maxl);
                rows[YzBet.today.index].set("maxwp", result.maxwp);
                rows[YzBet.today.index].set("maxdp", result.maxdp);
                rows[YzBet.today.index].set("maxlp", result.maxlp);

                YzBet.today.index++;
                YzBet.today.loadData(rows);
            }
        });
    }
};


YzBet.today.show = function (node) {
    var fields = [{ name: 'scheduleid', type: 'int' },
            { name: 'display', type: 'string' },
            { name: 'bgcolor', type: 'string' },
            { name: 'league', type: 'string' },
            { name: 'matchdate', type: 'string' },
            { name: 'state', type: 'string' },
            { name: 'h_team', type: 'string' },
            { name: 'goal', type: 'string' },
            { name: 'g_team', type: 'string' },
            { name: 'match_half', type: 'string' },
            { name: 'data', type: 'string' },
            { name: 'h_odds', type: 'string' },
            { name: 'pankou', type: 'string' },
            { name: 'g_odds', type: 'string' }, { name: 'zoudi', type: 'string' }, { name: 'other', type: 'string' }, { name: 'index', type: 'string' }, { name: 'classx2', type: 'string' },
            { name: 'pan', type: 'int' }, { name: 'ppan', type: 'int' },
            { name: 'maxw', type: 'float' }, { name: 'maxd', type: 'float' }, { name: 'maxl', type: 'float' }, { name: 'maxwp', type: 'float' }, { name: 'maxdp', type: 'float' }, { name: 'maxlp', type: 'float' }];

    var store = new Ext.data.GroupingStore({
        id: "scheduleid",
        reader: new Ext.data.JsonReader(
           {
               fields: fields
           })
    });

    var date = ""; //比赛日期

    store.on('load', function (s, records) {
        //grid.getView().getHeaderCell(1).innerText = matchdate;
        s.each(function (r, index) {
            //grid.getView().getCell(index, 1).style.backgroundColor = r.get("bgcolor"); //设置颜色
            grid.getView().getRow(index).style.display = r.get("display"); //设置隐藏
            grid.getView().getRow(index).setAttribute("id", "ttr1_" + r.get("scheduleid")); //设置id
            grid.getView().getRow(index).setAttribute("index", r.get("index")); //设置序列
            grid.getView().getRow(index).setAttribute("odds", ""); //设置赔率数据
        });
        if (grid.loadMask) {
            grid.loadMask.hide();
        }
        Ext.Ajax.request({
            url: 'Data/NowGoal/GetRemoteFile.aspx?f=rootjs&path=odds/oddsData.aspx',
            success: function (res1) {
                YzBet.today.showodds(res1.responseText);
            }
        });
    });

    //--------------------------------------------------列选择模式
    var sm = new Ext.grid.CheckboxSelectionModel({
        dataIndex: "scheduleid",
        css: 'vertical-align: inherit;',
        listeners: {
            selectionchange: function (s) {

            }
        }
    });
    //--------------------------------------------------列头
    var cm = new Ext.grid.ColumnModel([
		sm, {
		    header: '赛事',
		    dataIndex: "league",
		    align: "center",
		    width: 8,
		    sortable: true,
		    css: 'vertical-align: inherit;color:white;',
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.cellAttr = 'bgcolor="' + row.get("bgcolor") + '"';
		        return value;
		    }
		}, {
		    header: "时间",
		    dataIndex: "matchdate",
		    tooltip: "比赛开始时间",
		    align: "center",
		    sortable: true,
		    css: 'vertical-align: inherit;',
		    width: 5,
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.cellAttr = 'id="mt_' + row.get("scheduleid") + '"';
		        return value;
		    }
		}, {
		    header: "状态",
		    dataIndex: "state",
		    tooltip: "注单球队名称",
		    align: "center",
		    sortable: true,
		    css: 'vertical-align: inherit;',
		    width: 5,
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.cellAttr = 'id="time_' + row.get("scheduleid") + '"';
		        cell.css = 'fortime';
		        return value;
		    }
		}, {
		    header: "主队",
		    dataIndex: "h_team",
		    tooltip: "主场球队名称",
		    width: 16,
		    align: "right",
		    css: 'vertical-align: inherit;',
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.css = 'a1';
		        return value;
		    }
		}, {
		    header: "比分",
		    tooltip: "实时比分",
		    dataIndex: "goal",
		    width: 5,
		    align: "center",
		    css: 'vertical-align: inherit;',
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.css = row.get("classx2");
		        //cell.cellAttr = "onmouseover='showdetail(" + row.get("index") + ",event)'";
		        return value;
		    }
		}, {
		    header: "客队",
		    dataIndex: "g_team",
		    tooltip: "客场球队名称",
		    width: 16,
		    align: "left",
		    css: 'vertical-align: inherit;',
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.css = 'a2';
		        return value;
		    }
		}, {
		    header: "半场",
		    tooltip: "半场结束比分",
		    dataIndex: "match_half",
		    width: 5,
		    align: "center",
		    css: 'vertical-align: inherit;',
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.css = 'td_half';
		        return value;
		    }
		}, {
		    header: "数据",
		    tooltip: "比赛分析",
		    dataIndex: "data",
		    width: 15,
		    css: 'vertical-align: inherit;',
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.css = 'fr';
		        return value;
		    }
		}, {
		    header: "指数",
		    tooltip: "即时赔率",
		    dataIndex: "h_odds",
		    align: "center",
		    css: 'vertical-align: inherit;',
		    width: 5,
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.css = 'oddstd';
		        return value;
		    }
		}, {
		    header: "指数",
		    tooltip: "即时赔率",
		    dataIndex: "pankou",
		    align: "center",
		    css: 'vertical-align: inherit;',
		    width: 5,
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.css = 'oddstd';
		        return value;
		    }
		}, {
		    header: "指数",
		    tooltip: "即时赔率",
		    dataIndex: "g_odds",
		    align: "center",
		    css: 'vertical-align: inherit;',
		    width: 5,
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.css = 'oddstd';
		        return value;
		    }
		}, {
		    header: "走",
		    tooltip: "是否开滚球盘",
		    dataIndex: "zoudi",
		    align: "center",
		    css: 'vertical-align: inherit;',
		    width: 3
		}, {
		    header: "盘路",
		    dataIndex: "pan",
		    align: "center",
		    width: 5,
		    sortable: true,
		    css: 'vertical-align: inherit;color:green;',
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.cellAttr = 'id="other_' + row.id + '"';
		        return value;
		    }
		}, {
		    header: "主盘路",
		    dataIndex: "ppan",
		    align: "center",
		    width: 5,
		    sortable: true,
		    css: 'vertical-align: inherit;color:green;',
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.cellAttr = 'id="other_' + row.id + '"';
		        return value;
		    }
		}, {
		    header: "最高",
		    dataIndex: "max",
		    align: "center",
		    width: 5,
		    sortable: true,
		    css: 'vertical-align: inherit;color:gray;',
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        var max = Math.max(row.get("maxw"), row.get("maxd"), row.get("maxl"));
		        switch (max) {
		            case 0:
		                value = 0;
		                break;
		            case row.get("maxw"):
		                cell.style += "color:red";
		                value = max - row.get("maxd") - row.get("maxl");
		                break;
		            case row.get("maxl"):
		                cell.style += "color:green";
		                value = max - row.get("maxw") - row.get("maxd");
		                break;
		            default:
		                value = max - row.get("maxw") - row.get("maxl");
		                break;
		        }
		        return value.toFixed(2);
		    }
		}, {
		    header: "主最高",
		    dataIndex: "maxp",
		    align: "center",
		    width: 5,
		    sortable: true,
		    css: 'vertical-align: inherit;color:gray;',
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        var max = Math.max(row.get("maxwp"), row.get("maxdp"), row.get("maxlp"));
		        switch (max) {
		            case 0:
		                value = 0;
		                break;
		            case row.get("maxwp"):
		                cell.style += "color:red";
		                value = max - row.get("maxdp") - row.get("maxlp");
		                break;
		            case row.get("maxlp"):
		                cell.style += "color:green";
		                value = max - row.get("maxwp") - row.get("maxdp");
		                break;
		            default:
		                value = max - row.get("maxwp") - row.get("maxlp");
		                break;
		        }
		        return value.toFixed(2);
		    }
		}]);

    //----------------------------------------------------定义grid
    var grid = new Ext.grid.GridPanel({
        id: "ShowTodayGrid",
        store: store,
        sm: sm,
        cm: cm,
        sortable: true,
        columnLines: true,
        loadMask: true,
        stripeRows: true,
        autoWidth: true,
        //超过长度带自动滚动条
        autoScroll: true,
        border: false,
        viewConfig: {
            forceFit: true
        },
        tbar: [{
            xtype:'tbfill'
        }, {
            text: '分析',
            iconCls:'database_icon',
            handler: function () {
                var rows = grid.getSelectionModel().getSelections();
                if (rows.length > 0) {
                    YzBet.today.index = 0;
                    YzBet.today.loadData(rows);
                }
            }
        }]
    });

    var tab = center.getItem("ShowTodayTab");
    if (!tab) {
        var tab = center.add({
            id: "ShowTodayTab",
            iconCls: "totalicon",
            xtype: "panel",
            title: node.text,
            closable: true,
            layout: "fit",
            items: [grid]
        });
    }
    center.setActiveTab(tab);
    grid.loadMask.show();
    this.ShowBf();
};