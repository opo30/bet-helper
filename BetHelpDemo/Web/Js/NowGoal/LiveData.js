﻿/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />


var GetPrediction = function (scheduleID) {
    var loadMask = new Ext.LoadMask(Ext.getCmp('historyfile').getEl(), {
        msg: '正在分析比赛数据，请稍等...',
        removeMask: true// 完成后移除
    });
    loadMask.show();
    Ext.Ajax.request({
        url: "Data/NowGoal/GetPrediction.aspx",
        method: "POST",
        timeout: 30000,
        waitMsg: '正在分析比赛数据，请稍等...',
        params: {
            scheduleID: scheduleID
        },
        success: function (p1, p2) {
            loadMask.hide();
            var result = p1.responseText;
            Ext.Msg.alert("预测结果", result);
        },
        failure: function () {
            Ext.Msg.alert("预测结果", "无结果或预测失败");
        }

    });
}

var LoadHistoryMatch = function (node) {
    //指定列参数
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
            { name: 'g_odds', type: 'string' }, { name: 'zoudi', type: 'string' }, { name: 'other', type: 'string' }, { name: 'index', type: 'string' }, { name: 'classx2', type: 'string'}];

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
            grid.getView().getRow(index).setAttribute("id", "htr1_" + r.get("scheduleid")); //设置id
            grid.getView().getRow(index).setAttribute("index", r.get("index")); //设置序列
            grid.getView().getRow(index).setAttribute("odds", ""); //设置赔率数据
        });
        if (grid.loadMask) {
            grid.loadMask.hide();
        }
        Ext.Ajax.request({
            url: 'Data/NowGoal/GetRemoteFile.aspx?f=rootjs&path=odds/oddsData.aspx?date=' + HistoryScore.matchdate,
            success: function (res1) {
                HistoryScore.showodds(res1.responseText);
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
		    header: "其他",
		    dataIndex: "other",
		    align: "center",
		    width: 10,
		    css: 'vertical-align: inherit;color:green;',
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.cellAttr = 'id="other_' + row.id + '"';
		        return value;
		    }
		}]);


    var datefield = new Ext.form.DateField({
        id: 'datefield',
        name: 'date',
        onSelect: function (q, p) {
            this.setValue(p.format("Y-m-d"));
            grid.loadMask.show();
            HistoryScore.LoadLiveFile(p.format("Y-m-d"));
        }
    });

    //----------------------------------------------------定义grid
    var grid = new Ext.grid.GridPanel({
        id: "HistoryFileGrid",
        store: store,
        sm: sm,
        cm: cm,
        columnLines: true,
        loadMask: true,
        stripeRows: true,
        autoWidth: true,
        //超过长度带自动滚动条
        autoScroll: true,
        border: false,
        view: new Ext.grid.GroupingView({
            //自动填充
            forceFit: true,
            sortAscText: '正序排列',
            sortDescText: '倒序排列',
            columnsText: '显示/隐藏列',
            getRowClass: function (record, rowIndex, rowParams, store) {
            },
            groupTextTpl: '{text} ({[values.rs.length]} {[values.rs.length > 1 ? "Items" : "Item"]})'
        }),
        tbar: [
        datefield, {
            id: 'gameselection',
            text: '赛事选择',
            iconCls: 'totalicon',
            handler: function () {
                Ext.getCmp('myLeague-win').show();
            }
        }, "-", {
            id: 'LanguageSelect',
            text: '语言',
            iconCls: 'totalicon',
            menu: [{
                text: '简体',
                handler: function () {
                    HistoryScore.SetLanguage(0, this);
                }
            }, {
                text: '繁体', handler: function () {
                    HistoryScore.SetLanguage(1, this);
                }
            }, {
                text: '英语', handler: function () {
                    HistoryScore.SetLanguage(2, this);
                }
            }]
        }, "-", {
            id: 'MatchTypeSelect',
            text: '全部赛事',
            iconCls: 'totalicon',
            menu: [{
                text: '全部赛事',
                handler: function () {
                    HistoryScore.SetMatchType(0, this);
                }
            }, {
                text: '精简', handler: function () {
                    HistoryScore.SetMatchType(1, this);
                }
            }, {
                text: '开盘赛事', handler: function () {
                    HistoryScore.SetMatchType(2, this);
                }
            }]
        }, "-",
		new Ext.Toolbar.Fill(), {
		    text: '收藏',
		    iconCls: 'publishicon',
		    handler: function () {
		        var rows = grid.getSelectionModel().getSelections();
		        if (rows.length == 0) {
		            Ext.Msg.alert("提示信息", "您没有选中任何行!");
		        }
		        else {
		            var rowData = [];
		            Ext.each(rows, function (row) {
		                rowData.push(row.data);
		            });
		            Ext.Ajax.request({
		                url: "Data/NowGoal/BetExp.aspx?a=save",
		                method: 'POST',
		                waitMsg: '正在提交...',
		                params: { row: delHtmlTag(Ext.encode(rowData)) },
		                success: function (res, req) {
		                    var result = Ext.decode(res.responseText);
		                    if (!result.success) {
		                        Ext.Msg.alert("提 示", result.error);
		                    }
		                    else if (result.errorData.length > 0) {
		                        var arr = [];
		                        Ext.each(result.errorData, function (v) {
		                            arr.push(v.error);
		                        });
		                        Ext.Msg.alert("提 示", arr.join('<br />') + "还没有进行分析，保存失败！");
		                    }
		                    else {
		                        Ext.Msg.alert("提 示", "保存成功");
		                    }
		                },
		                failure: function (res, req) {

		                }
		            });
		        }
		    }
		}, "-", {
		    text: '刷新',
		    iconCls: "refreshicon",
		    tooltip: '刷新列表',
		    handler: function () {
		        grid.loadMask.show();
		        HistoryScore.LoadLiveFile();
		    }
		}, "-", {
		    text: "综合赔率",
		    //默认样式为按下
		    //pressed:true,
		    tooltip: "赔率三合一",
		    iconCls: "addicon",
		    handler: function () {
		        var row = grid.getSelectionModel().getSelections();
		        if (row.length == 0) {
		            Ext.Msg.alert("提示信息", "您没有选中任何行!");
		        }
		        else if (row.length > 1) {
		            Ext.Msg.alert("提示信息", "对不起只能选择一个!");
		        } else if (row.length == 1) {
		            AsianOdds(row[0].get("scheduleid"));
		        }
		    }
		}, "-", {
		    text: "百家欧赔",
		    tooltip: "百家欧赔",
		    iconCls: "editicon",
		    handler: function () {
		        var row = grid.getSelectionModel().getSelections();
		        if (row.length == 0) {
		            Ext.Msg.alert("提示信息", "您没有选中任何行!");
		        }
		        else if (row.length > 1) {
		            Ext.Msg.alert("提示信息", "对不起只能选择一个!");
		        } else if (row.length == 1) {
		            EuropeOdds(row[0].get("scheduleid"));
		        }

		    }
		}],
        listeners: {
            cellclick: function (grid, rowIndex, cellIndex, e) {
                if (cellIndex == 5) {
                    var matchid = store.getAt(rowIndex).get("scheduleid");
                    var win = new Ext.Window({
                        x: e.getXY()[0],
                        y: e.getXY()[1],
                        layout: "fit",
                        autoLoad: "Data/NowGoal/GetRemoteHtml.aspx?a=showgoallist&matchid=" + matchid
                    });
                    win.show();
                }
            }
        }

    });

    var GetPredictionBig = function (scheduleID) {
        Ext.Msg.prompt('输入', '请输入您判断大小球大小:', function (btn, text) {
            if (btn == 'ok') {
                if (text == '') {
                    Ext.Msg.alert('提示', '大小球数字不能为空');
                }
                else {
                    Ext.MessageBox.show({
                        msg: '正在分析比赛数据，请稍等...',
                        progressText: 'Saving...',
                        width: 300,
                        wait: true,
                        waitConfig: { interval: 200 },
                        icon: 'download',
                        animEl: 'saving'
                    });

                    Ext.Ajax.request({
                        url: "Data/NowGoal/GetPrediction.aspx",
                        method: "POST",
                        timeout: 30000,
                        params: {
                            scheduleID: scheduleID,
                            goalNum: text
                        },
                        success: function (p1, p2) {
                            var result = p1.responseText;
                            Ext.MessageBox.hide();
                            Ext.Msg.alert("预测结果", result);
                        },
                        failure: function () {
                            Ext.Msg.alert("预测结果", "无结果或预测失败");
                        }

                    });
                }
            }
        });
    }
    var tabkey = "historyfile";
    if (!Ext.getCmp('centertab').items.containsKey(tabkey)) {
        var tab = center.add({
            id: tabkey,
            iconCls: 'totalicon',
            xtype: "panel",
            title: node.text,
            closable: true,
            layout: "fit",
            items: [grid]
        });

    }
    Ext.getCmp('centertab').setActiveTab(tabkey);

    grid.loadMask.show();
    HistoryScore.LoadLiveFile();

}

var makeMyLeague = function () {
    if (Ext.getCmp('myLeague-win')) {
        Ext.getCmp('myLeague-win').destroy();
    }
    var items = [];
    for (var i = 0; i < sclasscount; i++) {
        if (B[i][8] > 0) {
            var checkbox = {
                boxLabel: "<span style='background-color:" + B[i][4] + ";'>&nbsp;&nbsp;&nbsp;</span><label style='cursor:pointer;padding-left:2px;' for='checkboxleague_" + i + "'>",
                indexB: i,
                checked: B[i][10] > 0
            };
            if (B[i][10] > 0) {
                if (orderby == "league") {
                    document.getElementById("tr_" + i).style.display = "";
                    document.getElementById("expand" + i).style.display = "none";
                    document.getElementById("collapse" + i).style.display = "";
                }
            }
            else {
                if (orderby == "league") {
                    document.getElementById("tr_" + i).style.display = "none";
                    document.getElementById("expand" + i).style.display = "none";
                    document.getElementById("collapse" + i).style.display = "none";
                }
            }

            if (B[i][5] == "1")
                checkbox.boxLabel += "<font color=red>" + B[i][1 + Config.language] + "[" + B[i][8] + "]</font></label>";
            else
                checkbox.boxLabel += B[i][1 + Config.language] + "<font color=#990000>[" + B[i][8] + "]</font></label>";

            items.push(checkbox);
        }
    }
    var myLeagueWin = new Ext.Window({
        id: 'myLeague-win',
        layout: 'fit',
        frame: true,
        title: "赛事选择",
        iconCls: 'totalicon',
        labelWidth: 110,
        width: 600,
        closeAction: 'hide',
        bodyStyle: 'padding:10 10 10 10;',
        items: [{
            xtype: 'checkboxgroup',
            columns: 5,
            items: items,
            listeners: {
                change: function (obj, value) {
                    var grid = Ext.getCmp("HistoryFileGrid");
                    var hh = parseInt(Ext.getDom("hiddencount").innerHTML);
                    if (hiddenID == "_") {
                        for (var j = 0; j < matchcount; j++) {
                            if (A[j][1] != -1) hiddenID += A[j][0] + "_";
                        }
                    }
                    obj.items.each(function (v) {
                        if (v.checked) {
                            if (orderby == "league") document.getElementById("tr_" + i).style.display = "";
                            for (var j = 0; j < matchcount; j++) {
                                if (A[j][1] == v.indexB) {
                                    var index = grid.getStore().indexOfId(A[j][0]);
                                    grid.getView().getRow(index).style.display = "";
                                    //document.getElementById("tr1_" + A[j][0]).style.display = "";
                                    if (A[j][27] != "") document.getElementById("tr2_" + A[j][0]).style.display = "";
                                    hh--;
                                    if (hiddenID.indexOf("_" + A[j][0] + "_") == -1) hiddenID += A[j][0] + "_";
                                }
                            }
                        }
                        else {
                            if (orderby == "league") document.getElementById("tr_" + i).style.display = "none";
                            for (var j = 0; j < matchcount; j++) {
                                if (A[j][1] == v.indexB) {
                                    var index = grid.getStore().indexOfId(A[j][0]);
                                    grid.getView().getRow(index).style.display = "none";
                                    //document.getElementById("tr1_" + A[j][0]).style.display = "none";
                                    if (A[j][27] != "") document.getElementById("tr2_" + A[j][0]).style.display = "none";
                                    hh++;
                                    hiddenID = hiddenID.replace("_" + A[j][0] + "_", "_")
                                    //(hiddenID.indexOf("_"+A[j][0] + "_")==-1) hiddenID+=A[j][0] + "_";
                                }
                            }
                        }
                    });

                    Ext.getDom("hiddencount").innerHTML = hh;
                    writeCookie("HiddenMatchID", hiddenID);

                    countCheckNum();
                    //                     makeMyCountry();
                }
            }
        }],
        buttons: [{
            id: 'btn_all',
            text: '全部',
            handler: SetMatchType.createCallback(0)
        }, {
            text: '一级赛事',
            handler: SetMatchType.createCallback(1)
        }, {
            text: '全选',
            handler: ShowAllMatch.createCallback()
        }, {
            text: '反选',
            handler: SelectOtherLeague.createCallback()
        }, {
            text: '关闭',
            handler: function () {
                myLeagueWin.hide();
            }
        }]
    });

}

var showDetailWindow = function (matchid, title, action) {
    new Ext.Window({
        title: title,
        layout: "fit",
        height: 500,
        x: 0,
        y: 0,
        autoScroll: true,
        autoLoad: "Data/NowGoal/GetRemoteHtml.aspx?a=" + action + "&matchid=" + matchid
    }).show(); ;
}