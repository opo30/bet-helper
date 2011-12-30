/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />



var GetPrediction = function(scheduleID) {
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
         success: function(p1, p2) {
            loadMask.hide();
            var result = p1.responseText;
            Ext.Msg.alert("预测结果", result);
         },
         failure: function() {
             Ext.Msg.alert("预测结果", "无结果或预测失败");
         }

     });
 }

 var LoadHistoryMatch = function (node) {
     var companyMenu = new Ext.menu.Menu({
         id: 'mainMenu',
         items: [{
             text: '所有',
             handler: function () { LoadLiveFile(0) }
         },
            {
                text: '澳门',
                handler: function () { LoadLiveFile(1) }
            },
            {
                text: '立博',
                handler: function () { LoadLiveFile(4) }
            },
            {
                text: 'ＳＢ',
                handler: function () { LoadLiveFile(3) }
            },
            {
                text: '沙巴',
                handler: function () { LoadLiveFile(24) }
            },
            {
                text: '利记',
                handler: function () { LoadLiveFile(31) }
            },
            {
                text: '明陞',
                handler: function () { LoadLiveFile(17) }
            },
            {
                text: '金宝博',
                handler: function () { LoadLiveFile(23) }
            },
            {
                text: '易胜博',
                handler: function () { LoadLiveFile(12) }
            },
            {
                text: 'Bet365',
                handler: function () { LoadLiveFile(8) }
            }]
     });

     //指定列参数
     var fields = [{ name: 'match_0', type: 'int' },
            { name: 'match_1', type: 'int' },
            { name: 'match_2', type: 'int' },
            { name: 'match_3', type: 'int' },
            { name: 'match_4', type: 'string' },
            { name: 'match_5', type: 'string' },
            { name: 'match_6', type: 'string' },
            { name: 'match_7', type: 'string' },
            { name: 'match_8', type: 'string' },
            { name: 'match_9', type: 'string' },
            { name: 'match_10', type: 'string' },
            { name: 'match_11', type: 'string' },
            { name: 'match_12', type: 'int' },
            { name: 'match_13', type: 'int' },
            { name: 'match_14', type: 'int' },
            { name: 'match_15', type: 'int' },
            { name: 'match_16', type: 'int' },
            { name: 'match_17', type: 'int' },
            { name: 'match_18', type: 'int' },
            { name: 'match_19', type: 'int' },
            { name: 'match_20', type: 'int' },
            { name: 'match_21', type: 'string' },
            { name: 'match_22', type: 'string' },
            { name: 'match_23', type: 'string' },
            { name: 'match_24', type: 'string' },
            { name: 'match_25', type: 'string' },
            { name: 'match_26', type: 'string' },
            { name: 'match_27', type: 'string' },
            { name: 'match_28', type: 'string' },
            { name: 'match_29', type: 'int' },
            { name: 'match_30', type: 'int' },
            { name: 'exist', type: 'string'}];

     var store = new Ext.data.ArrayStore({
         fields: fields,
         id: "match_0"
     });

     var date = ""; //比赛日期

     var LoadLiveFile = function (companyid) {
         grid.loadMask.show();
         if (companyid == undefined) {
             companyid = 8; //默认为365
         }
         Ext.Ajax.request({
             url: 'Data/NowGoal/LoadHistoryFile.aspx',
             method: 'POST',
             params: {
                 now: date, companyid: companyid
             },
             success: function (res) {
                 var ShowBf = function () {
                     //store.mdate = matchdate;
                     store.A = A;
                     store.B = B;
                     store.C = C;
                     store.showlist = showlist;
                     store.loadData(A);

                     var items = [];
                     Ext.each(B, function (v) {
                         items.push({
                             boxLabel: v[5] == "1" ? ("<font color=red>" + v[1] + "</font>") : v[1],
                             name: v[0],
                             checked: v[5] == "1"
                         });
                     });
                     createGameSelection(items);
                     grid.loadMask.hide();
                 }
                 if (res.responseText != "") {
                     eval(res.responseText);
                 }
                 else {
                     Ext.Msg.alert("提示", "请求数据失败！");
                 }
             }
         });
     }

     store.on('load', function (s, records) {
         var HiddenMatchID = [];
         if (Ext.util.Cookies.get("HiddenMatchID"))
             HiddenMatchID = Ext.util.Cookies.get("HiddenMatchID").split(',');
         s.each(function (r, index) {
             grid.getView().getCell(index, 1).style.backgroundColor = s.B[r.get('match_1')][4];
             //grid.getView().getRow(gridcount).style.display = (s.B[r.get('match_1')][5] == 1) ? "" : "none";
             var hidelist = Ext.util.Cookies.get("HiddenMatchID");
             if (HiddenMatchID.indexOf(r.get('match_0').toString()) != -1 || (s.showlist.length > 0 && s.showlist.indexOf(r.get('match_0')) == -1)) {
                 grid.getView().getRow(index).style.display = "none";
             }
         });
         Ext.getCmp('hiddencount').setText('隐藏<span class="td_scoreR" id="hiddencount">' + HiddenMatchID.length + '</span>场');
     });

     //--------------------------------------------------列选择模式
     var sm = new Ext.grid.CheckboxSelectionModel({
         dataIndex: "match_0",
         listeners: {
             selectionchange: function (s) {

             }
         }
     });
     //--------------------------------------------------列头
     var cm = new Ext.grid.ColumnModel([
		sm, {
		    header: "赛事",
		    dataIndex: "match_1",
		    tooltip: "赛事名称",
		    //列不可操作
		    //menuDisabled:true,
		    //可以进行排序
		    sortable: false,
		    renderer: function (value) {
		        return "<font color='white'>" + store.B[value][1] + "</span>"
		    }
		}, {
		    header: "时间",
		    dataIndex: "match_10",
		    tooltip: "比赛开始时间",
		    sortable: false,
		    renderer: function (value, last, row) {
		        return "<span ext:qtip='" + row.data["match_11"] + "'>" + value.replace("<br>", " ") + "</span>";
		    }
		}, {
		    header: "状态",
		    dataIndex: "match_3",
		    tooltip: "注单球队名称",
		    sortable: false,
		    renderer: function (value, last, row) {
		        try {
		            var t = row.data["match_11"].split(",");
		            var t2 = new Date(t[0], t[1], t[2], t[3], t[4], t[5]);
		            if (row.data["match_12"] == "-1")
		                return "<font color='blue'>完</font>";
		            if (row.data["match_12"] == "0") {
		                var min = Math.abs(Math.floor((new Date() - t2 - difftime) / 60000));
		                if (min > 60)
		                    return Math.floor(min / 60) + "时" + Math.floor(min % 60) + "分开赛";
		                else
		                    return min + "分开赛";
		            }
		            if (row.data["match_12"] == "1") {  //上半场
		                var goTime = Math.floor((new Date() - t2 - difftime) / 60000);
		                if (goTime > 45) goTime = "45+";
		                if (goTime < 1) goTime = "1";
		                return goTime + "<img src='Images/live/in.gif' border=0>";
		            }
		            if (row.data["match_12"] == "2") {
		                return "<font color='red'>中</font>";
		            }
		            if (row.data["match_12"] == "3") {  //下半场
		                goTime = Math.floor((new Date() - t2 - difftime) / 60000) + 46;
		                if (goTime > 90) goTime = "90+";
		                if (goTime < 46) goTime = "46";
		                return goTime + "<img src='Images/live/in.gif' border=0>";
		            }
		        } catch (e) { }
		    }
		}, {
		    header: "主队",
		    dataIndex: "match_4",
		    tooltip: "主场球队名称",
		    //列不可操作
		    //menuDisabled:true,
		    //可以进行排序
		    sortable: false,
		    renderer: function (value, last, row) {
		        if (row.data["match_17"] != "0")
		            H_redcard = "<img src='images/live/redcard" + row.data["match_17"] + ".gif'>";
		        else
		            H_redcard = "";
		        if (row.data["match_19"] != "0")
		            H_yellow = "<img src='images/live/yellow" + row.data["match_19"] + ".gif'>";
		        else
		            H_yellow = "";
		        return value + H_redcard + H_yellow;
		    }
		}, {
		    header: "比分",
		    tooltip: "实时比分",
		    dataIndex: "match_13",
		    sortable: false,
		    align: "center",
		    renderer: function (value, last, row) {
		        var state = parseInt(row.data["match_12"]);
		        var text = "";
		        switch (state) {
		            case 0:
		                if (row.data["match_23"] == "1") text = "阵容"; else text = "-";
		                break;
		            case 1:
		                text = row.data["match_13"] + "-" + row.data["match_14"];
		                break;
		            case -11:
		            case -14:
		                text = "";
		                break;
		            default:
		                text = row.data["match_13"] + "-" + row.data["match_14"];
		                break;
		        }
		        return text;
		    }
		}, {
		    header: "客队",
		    dataIndex: "match_7",
		    tooltip: "客场球队名称",
		    //列不可操作
		    //menuDisabled:true,
		    //可以进行排序
		    sortable: false,
		    renderer: function (value, last, row) {
		        if (row.data["match_18"] != "0") G_redcard = "<img src='images/live/redcard" + row.data["match_18"] + ".gif'>"; else G_redcard = "";
		        if (row.data["match_20"] != "0") G_yellow = "<img src='images/live/yellow" + row.data["match_20"] + ".gif'>"; else G_yellow = "";

		        return value + G_redcard + G_yellow;
		    }
		}, {
		    header: "半场",
		    tooltip: "半场结束比分",
		    dataIndex: "match_15",
		    sortable: false,
		    renderer: function (value, last, row, p4, p5, store) {
		        return "<font color='blue'>" + value + "-" + row.data["match_16"] + "</font>"
		    }
		}, {
		    header: "数据",
		    tooltip: "比赛分析",
		    dataIndex: "match_11",
		    sortable: true,
		    renderer: function (value, last, row, p4, p5, store) {
		        return "<font color='blue' onclick='showDetailWindow(\"" + row.get("match_0") + "\",\"分析\",\"showgoallist\")'>析</font>&nbsp;&nbsp;&nbsp;<font color='blue' onclick='showDetailWindow(\"" + row.get("match_0") + "\",\"亚盘\",\"AsianOdds\")'>亚</font>&nbsp;&nbsp;&nbsp;<font color='blue' onclick='GetPredictionBig(" + parseInt(row.data["match_0"]) + ")'>大小</font>&nbsp;&nbsp;&nbsp;<font color='blue' onclick='LiveChartManage(" + parseInt(row.data["match_0"]) + ",8)'>图</font>";
		    }
		}, {
		    header: "指数",
		    tooltip: "即时赔率",
		    dataIndex: "match_25",
		    sortable: true
		}, {
		    header: "走",
		    tooltip: "是否开滚球盘",
		    dataIndex: "match_13",
		    sortable: true,
		    renderer: function (value) {
		        if (value == "1")
		            return "<img src='images/live/t3.gif' height=10 width=10 ext:qtip='有走地赛事'/>";
		        if (value == "2")
		            return "<img src='images/live/t32.gif' height=10 width=10 ext:qtip='正在走地'/>";
		    }
		}, {
		    header: "经验",
		    tooltip: "属否存在分析数据",
		    dataIndex: "exist",
		    sortable: true,
		    renderer: function (value, last, row) {
		        var isexist = value;
		        if (store.A[0].length == 29) {
		            isexist = row.get("match_28");
		        }
		        if (isexist == "True")
		            return "<font color=green>完成</font>";
		        else if (isexist == "False")
		            return "<font color=blue>已分析</font>";
		        else
		            return "<font color=red>未分析</font>";
		    }
		}]);


     var datefield = new Ext.form.DateField({
         name: 'date',
         onSelect: function (q, p) {
             this.setValue(p.format("Y-m-d"));
             date = p.format("Y-m-d");
             LoadLiveFile(0);
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
         viewConfig: {
             //自动填充
             forceFit: true,
             sortAscText: '正序排列',
             sortDescText: '倒序排列',
             columnsText: '列显示/隐藏'
         },
         tbar: [
        datefield, {
            id: 'gameselection',
            text: '赛事选择',
            iconCls: 'totalicon',
            handler: function () {
                Ext.getCmp('gameSelection-win').show();
            }
        }, "-", {
            id: 'companyselection',
            text: '开盘公司',
            iconCls: 'totalicon',
            menu: companyMenu
        }, "-", {
            id: 'sclassmode',
            text: '分组',
            iconCls: 'totalicon',
            enableToggle: true,
            toggleHandler: function (button, state) {
                if (state) {

                } else {

                }
            }
        }, "|", {
            text: '删除',
            iconCls: "deleteicon",
            tooltip: '删除记录',
            handler: function () {
                var rows = grid.getSelectionModel().getSelections();
                if (rows.length > 0) {
                    var HiddenMatchID = [];
                    if (Ext.util.Cookies.get("HiddenMatchID")) {
                        HiddenMatchID = Ext.util.Cookies.get("HiddenMatchID").split(',');
                    }
                    Ext.each(rows, function (r, index) {
                        HiddenMatchID.push(r.get("match_0"));
                        var grid_index = grid.getStore().indexOf(r);
                        grid.getView().getRow(grid_index).style.display = "none";
                    });
                    Ext.util.Cookies.set("HiddenMatchID", HiddenMatchID.join(','));
                    Ext.getCmp('hiddencount').setText('隐藏<span class="td_scoreR" id="hiddencount">' + HiddenMatchID.length + '</span>场');
                }
            }
        }, {
            id: 'hiddencount',
            xtype: 'button',
            text: '',
            iconCls: 'publishicon',
            handler: function () {
                if (Ext.util.Cookies.get("HiddenMatchID")) {
                    var HiddenMatchID = Ext.util.Cookies.get("HiddenMatchID").split(',');
                    Ext.each(HiddenMatchID, function (sid) {
                        var grid_index = grid.getStore().find("match_0", sid);
                        grid.getView().getRow(grid_index).style.display = "";
                    });
                    Ext.util.Cookies.set("HiddenMatchID", "");
                    Ext.getCmp('hiddencount').setText('隐藏<span class="td_scoreR" id="hiddencount">0</span>场');
                }
            }
        },
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
		    text: '今日',
		    iconCls: "refreshicon",
		    tooltip: '刷新列表',
		    handler: function () { date = ""; LoadLiveFile(); }
		}
		, {
		    text: '刷新',
		    iconCls: "refreshicon",
		    tooltip: '刷新列表',
		    handler: function () { LoadLiveFile(3); }
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
		            OddsDetailManage(row[0].data.match_0, row[0].data.match_4 + "-" + row[0].data.match_7, store.B[row[0].data.match_1][0]);
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
		            var store = grid.getStore();
		            Odds1x2Manage(store.A[store.indexOf(row[0])], store.B[row[0].get("match_1")]);
		        }

		    }
		}],
         listeners: {
             cellclick: function (grid, rowIndex, cellIndex, e) {
                 if (cellIndex == 5) {
                     var matchid = store.getAt(rowIndex).get("match_0");
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

     var createGameSelection = function (items) {
         if (Ext.getCmp('gameSelection-win')) {
             Ext.getCmp('gameSelection-win').destroy();
         }
         var gameSelectionWin = new Ext.Window({
             id: 'gameSelection-win',
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
                         var gridcount = 0;
                         store.each(function (r) {
                             var flag = false;
                             Ext.each(value, function (v) {
                                 if (v.name == store.B[r.get("match_1")][0]) {
                                     flag = true;
                                 }
                             });
                             grid.getView().getRow(gridcount).style.display = flag ? "" : "none";
                             gridcount++;
                         });
                     }
                 }
             }]
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

     LoadLiveFile();

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