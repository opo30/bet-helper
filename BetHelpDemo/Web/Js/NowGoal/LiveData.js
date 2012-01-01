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

     var companyIdArr = [1, 4, 3, 24, 31, 17, 23, 12, 8];
     var companyMenuItems = [];
     for (var i = 0; i < 9; i++) {
         companyMenuItems.push({
             text: company[companyIdArr[i]],
             handler: SetCompany.createCallback(companyIdArr[i])
         });
     }
     var companyMenu = new Ext.menu.Menu({
         id: 'companyMenu',
         items: companyMenuItems
     });

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
            { name: 'g_odds', type: 'string' }, { name: 'zoudi', type: 'string' }, { name: 'other', type: 'string' }, { name: 'odds', type: 'string' }, { name: 'index', type: 'string'}];

     var store = new Ext.data.JsonStore({
         fields: fields,
         id: "scheduleid"
     });

     var date = ""; //比赛日期

     store.on('load', function (s, records) {
         grid.getView().getHeaderCell(1).innerText = matchdate;

         s.each(function (r, index) {
             grid.getView().getCell(index, 1).style.backgroundColor = r.get("bgcolor"); //设置颜色
             grid.getView().getRow(index).style.display = r.get("display");
         });
         if (grid.loadMask) {
             grid.loadMask.hide();
         }
     });

     //--------------------------------------------------列选择模式
     var sm = new Ext.grid.CheckboxSelectionModel({
         dataIndex: "scheduleid",
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
		    menuDisabled: true,
		    align: "center",
		    width: 8,
		    renderer: function (value) {
		        return "<span style='color:white;'>" + value + "</span>";
		    }
		}, {
		    header: "时间",
		    dataIndex: "matchdate",
		    tooltip: "比赛开始时间",
		    menuDisabled: true,
		    align: "center",
		    width: 5
		}, {
		    header: "状态",
		    dataIndex: "state",
		    tooltip: "注单球队名称",
		    menuDisabled: true,
		    align: "center",
		    width: 5
		}, {
		    header: "主队",
		    dataIndex: "h_team",
		    tooltip: "主场球队名称",
		    menuDisabled: true,
		    width: 16,
		    align: "right"
		}, {
		    header: "比分",
		    tooltip: "实时比分",
		    dataIndex: "goal",
		    menuDisabled: true,
		    width: 5,
		    align: "center"
		}, {
		    header: "客队",
		    dataIndex: "g_team",
		    tooltip: "客场球队名称",
		    menuDisabled: true,
		    width: 16,
		    align: "left"
		}, {
		    header: "半场",
		    tooltip: "半场结束比分",
		    dataIndex: "match_half",
		    menuDisabled: true,
		    width: 5,
		    align: "center"
		}, {
		    header: "数据",
		    tooltip: "比赛分析",
		    dataIndex: "data",
		    menuDisabled: true,
		    width: 15
		    //		    renderer: function (value, last, row, p4, p5, store) {
		    //		        return "<font color='blue' onclick='showDetailWindow(\"" + row.get("scheduleid") + "\",\"分析\",\"showgoallist\")'>析</font>&nbsp;&nbsp;&nbsp;<font color='blue' onclick='showDetailWindow(\"" + row.get("scheduleid") + "\",\"亚盘\",\"AsianOdds\")'>亚</font>&nbsp;&nbsp;&nbsp;<font color='blue' onclick='GetPredictionBig(" + parseInt(row.data["scheduleid"]) + ")'>大小</font>&nbsp;&nbsp;&nbsp;<font color='blue' onclick='LiveChartManage(" + parseInt(row.data["scheduleid"]) + ",8)'>图</font>";
		    //		    }
		}, {
		    header: "指数",
		    tooltip: "即时赔率",
		    dataIndex: "h_odds",
		    menuDisabled: true,
		    align: "center",
		    width: 5
		}, {
		    header: "指数",
		    tooltip: "即时赔率",
		    dataIndex: "pankou",
		    menuDisabled: true,
		    align: "center",
		    width: 5
		}, {
		    header: "指数",
		    tooltip: "即时赔率",
		    dataIndex: "g_odds",
		    menuDisabled: true,
		    align: "center",
		    width: 5
		}, {
		    header: "走",
		    tooltip: "是否开滚球盘",
		    dataIndex: "zoudi",
		    align: "center",
		    menuDisabled: true,
		    width: 3
		}, {
		    header: "其他",
		    dataIndex: "other",
		    align: "center",
		    menuDisabled: true,
		    width: 10
		}]);


     var datefield = new Ext.form.DateField({
         name: 'date',
         onSelect: function (q, p) {
             this.setValue(p.format("Y-m-d"));
             date = p.format("Y-m-d");
             grid.loadMask.show();
             LoadLiveFile(date);
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
                Ext.getCmp('myLeague-win').show();
            }
        }, "-", {
            id: 'CompanySelect',
            text: "公司选择",
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
        }, "-", {
            text: '删除',
            iconCls: "deleteicon",
            tooltip: '删除记录',
            handler: function () {
                var rows = grid.getSelectionModel().getSelections();
                if (rows.length > 0) {
                    Ext.each(rows, function (r, index) {
                        var grid_index = grid.getStore().indexOf(r);
                        grid.getView().getRow(grid_index).style.display = "none";
                        Ext.getDom("hiddencount").innerHTML = parseInt(Ext.getDom("hiddencount").innerHTML) + 1;
                        if (hiddenID == "_") {
                            for (var j = 0; j < matchcount; j++) {
                                if (A[j][1] != -1 && j != i) hiddenID += A[j][0] + "_";
                            }
                        } else
                            hiddenID = hiddenID.replace("_" + r.get("scheduleid") + "_", "_")
                    });
                    grid.getSelectionModel().clearSelections()
                    writeCookie("HiddenMatchID", hiddenID);
                }
            }
        }, {
            id: 'hiddenMatch',
            xtype: 'button',
            text: '隐藏<span class="td_scoreR" id="hiddencount">0</span>场',
            iconCls: 'publishicon',
            handler: function () {
                ShowAllMatch();
            }
        }, "-", {
            id: 'LanguageSelect',
            text: '语言',
            iconCls: 'totalicon',
            menu: [{
                text: '简体',
                handler: function () {
                    SetLanguage(0, this);
                }
            }, {
                text: '繁体', handler: function () {
                    SetLanguage(1, this);
                }
            }, {
                text: '英语', handler: function () {
                    SetLanguage(2, this);
                }
            }]
        }, "-", {
            id: 'MatchTypeSelect',
            text: '赛事',
            iconCls: 'totalicon',
            menu: [{
                text: '全部赛事',
                handler: function () {
                    SetMatchType(0, this);
                }
            }, {
                text: '精简', handler: function () {
                    SetMatchType(1, this);
                }
            }, {
                text: '开盘赛事', handler: function () {
                    SetMatchType(2, this);
                }
            }]
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
		    text: '刷新',
		    iconCls: "refreshicon",
		    tooltip: '刷新列表',
		    handler: function () {
		        LoadLiveFile();
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
		            OddsDetailManage(row[0].data.scheduleid, row[0].data.match_4 + "-" + row[0].data.match_7, B[row[0].data.match_1][0]);
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
		            Odds1x2Manage(A[store.indexOf(row[0])], B[row[0].get("match_1")]);
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
     LoadLiveFile();

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