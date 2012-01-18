/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />

var Odds1x2store;
var customfun = function() {
    alert("aaa");
}
function formatFloat(src, pos) {
    return Math.round(src * Math.pow(10, pos)) / Math.pow(10, pos);
}

var EuropeOdds = function (scheduleid) {
    var index,scheduleArr,scheduleTypeArr;
    if (Ext.fly("tr1_" + scheduleid)) {
        index = Ext.getDom("tr1_" + scheduleid).getAttribute("index");
        scheduleArr = A[index];
        scheduleTypeArr = B[A[index][1]];
    }else {
        index = Ext.getDom("htr1_" + scheduleid).getAttribute("index");
        scheduleArr = HistoryScore.A[index];
        scheduleTypeArr = HistoryScore.B[HistoryScore.A[index][1]];
    }
    
    var schedule = scheduleArr[4] + "-" + scheduleArr[7];
    var upcolor = "red";
    var downcolor = "green";
    var importantconpany = new Array();

    function showDate(t1) {
        var t2 = t1.split(",");
        var t = new Date(t2[0], eval(t2[1]), t2[2], t2[3], t2[4], t2[5]);
        t = new Date(Date.UTC(t.getFullYear(), t.getMonth(), t.getDate(), t.getHours(), t.getMinutes(), t.getSeconds()));
        var y = t.getFullYear();
        var M = t.getMonth() + 1;
        var d = t.getDate();
        var h = t.getHours();
        var m = t.getMinutes();
        if (M < 10) M = "0" + M;
        if (d < 10) d = "0" + d;
        if (h < 10) h = "0" + h;
        if (m < 10) m = "0" + m;
        return (y + '-' + M + "-" + d + " " + h + ":" + m);
    }

    //指定列参数
    var fields = [{ name: 'companyid', type: 'string' },
            { name: 'oddsid', type: 'string' },
            { name: 'companyname', type: 'string' },
            { name: 'h_win', type: 'string' },
            { name: 'h_draw', type: 'string' },
            { name: 'h_lost', type: 'string' },
            { name: 'h_winper', type: 'string' },
            { name: 'h_drawper', type: 'string' },
            { name: 'h_lostper', type: 'string' },
            { name: 'h_return', type: 'string' },
            { name: 'g_win', type: 'string' },
            { name: 'g_draw', type: 'string' },
            { name: 'g_lost', type: 'string' },
            { name: 'g_winper', type: 'string' },
            { name: 'g_drawper', type: 'string' },
            { name: 'g_lostper', type: 'string' },
            { name: 'g_return', type: 'string' },
            { name: 'winkelly', type: 'string' },
            { name: 'drawkelly', type: 'string' },
            { name: 'lostkelly', type: 'string' },
            { name: 'lastupdatetime', type: 'string' },
            { name: 'companyfullname', type: 'string' },
            { name: 'isprimary', type: 'boolean' },
            { name: 'isexchange', type: 'string' },
            { name: 'data', type: 'string' }
            ];

    Odds1x2store = new Ext.data.ArrayStore({
        fields: fields
    });

    Odds1x2store.on('load', function (s, records) {
        s.each(function (r, index) {
            var game = gameData[index].split('|');
            if (game[10] != "") {
                for (var i = 10; i < 13; i++) {
                    var stra = Number(game[i]);
                    var strb = Number(game[i - 7]);
                    if (stra > strb) {
                        grid.getView().getCell(index, i - 1).style.backgroundColor = "#F7CFD6"; //上涨#F7CFD6;下降#DFF3B1;
                    }
                    else if (stra < strb) {
                        grid.getView().getCell(index, i - 1).style.backgroundColor = "#DFF3B1"; //上涨#F7CFD6;下降#DFF3B1;
                    }
                }
            }
            for (var i = 12; i <= 15; i++) {
                grid.getView().getCell(index, i).style.color = "#666";
            }
        });
    });

    var gameData = [];

    var LoadOddsJS = function () {
        grid.loadMask.show();
        Ext.Ajax.request({
            url: "Data/NowGoal/GetRemoteFile.aspx?f=oddsjs&path=" + scheduleArr[0] + ".js",
            method: 'POST',
            success: function (res) {
                if (res.responseText != "") {
                    var arrayData = [];
                    eval(res.responseText + "gameData = game;");
                    Ext.each(gameData, function (oddsinfo) {
                        var arr = oddsinfo.split('|');
                        arr.push(oddsinfo);
                        arrayData.push(arr);
                    });
                    Odds1x2store.loadData(arrayData)
                }
                else {
                    Ext.Msg.alert("提示", "请求数据失败！");
                }
                grid.loadMask.hide();
            }
        });
    }

    Odds1x2store.on("datachanged", function (store) {

    });



    //--------------------------------------------------列选择模式
    var sm = new Ext.grid.RowSelectionModel({
        dataIndex: "oddsid",
        listeners: {
            selectionchange: function (s) {

            }
        }
    });

    var oddsGroupRow = [
          { header: '', colspan: 2, align: 'center' },
          { header: '初  盘', colspan: 7, align: 'center' },
          { header: '即时盘', colspan: 7, align: 'center' },
          { header: '凯利指数', colspan: 3, align: 'center' },
          { header: '', colspan: 1, align: 'center' }
      ];

    //--------------------------------------------------列头
    var cm = new Ext.grid.ColumnModel([
		new Ext.grid.RowNumberer(), {
		    header: "公司",
		    dataIndex: "companyfullname",
		    tooltip: "公司名称",
		    sortable: true,
		    align: "middle",
		    width: 250,
		    renderer: function (value, last, row) {
		        return "<font color=" + (row.get("isprimary") ? 'blue' : '') + ">" + value + "</font>";
		    }
		}, {
		    header: "主胜",
		    tooltip: "主场球队获胜赔率",
		    dataIndex: "h_win",
		    sortable: true
		}, {
		    header: "和局",
		    tooltip: "比赛打平的赔率",
		    dataIndex: "h_draw",
		    sortable: true
		}, {
		    header: "客胜",
		    tooltip: "客场球队获胜赔率",
		    dataIndex: "h_lost",
		    sortable: true
		}, {
		    header: "主胜率",
		    tooltip: "主场获胜概率",
		    dataIndex: "h_winper",
		    sortable: true,
		    renderer: function (value) {
		        return value + "%";
		    }
		}, {
		    header: "和局率",
		    tooltip: "打平的概率",
		    dataIndex: "h_drawper",
		    sortable: true,
		    renderer: function (value) {
		        return value + "%";
		    }
		}, {
		    header: "客胜率",
		    tooltip: "客场获胜概率",
		    dataIndex: "h_lostper",
		    sortable: true,
		    renderer: function (value) {
		        return value + "%";
		    }
		}, {
		    header: "返还率",
		    tooltip: "总体投注的返还率",
		    dataIndex: "h_return",
		    sortable: true,
		    renderer: function (value) {
		        return value + "%";
		    }
		}, {
		    header: "主胜",
		    tooltip: "主场球队获胜赔率",
		    dataIndex: "g_win",
		    sortable: true
		}, {
		    header: "和局",
		    tooltip: "比赛打平的赔率",
		    dataIndex: "g_draw",
		    sortable: true
		}, {
		    header: "客胜",
		    tooltip: "客场球队获胜赔率",
		    dataIndex: "g_lost",
		    sortable: true
		}, {
		    header: "主胜率",
		    tooltip: "主场获胜概率",
		    dataIndex: "g_winper",
		    sortable: true,
		    renderer: function (value) {
		        return value ? (value + "%") : "";
		    }
		}, {
		    header: "和局率",
		    tooltip: "打平的概率",
		    dataIndex: "g_drawper",
		    sortable: true,
		    renderer: function (value) {
		        return value ? (value + "%") : "";
		    }
		}, {
		    header: "客胜率",
		    tooltip: "客场获胜概率",
		    dataIndex: "g_lostper",
		    sortable: true,
		    renderer: function (value) {
		        return value ? (value + "%") : "";
		    }
		}, {
		    header: "返还率",
		    tooltip: "总体投注的返还率",
		    dataIndex: "g_return",
		    sortable: true,
		    renderer: function (value) {
		        return value ? (value + "%") : "";
		    }
		}, {
		    header: "胜",
		    tooltip: "凯利指数",
		    dataIndex: "winkelly",
		    sortable: true
		}, {
		    header: "平",
		    tooltip: "凯利指数",
		    dataIndex: "drawkelly",
		    sortable: true
		}, {
		    header: "负",
		    tooltip: "凯利指数",
		    dataIndex: "lostkelly",
		    sortable: true
		}, {
		    header: "更新时间",
		    dataIndex: "lastupdatetime",
		    sortable: true,
		    width: 200,
		    renderer: function (value) {
		        return showDate(value);
		    }
		}
]);



    //定义组别摘要的方法
    //    Ext.ux.grid.GroupSummary.Calculations['totalOdds1x2'] = function(v, record, field) {
    //        return v + (record.data.estimate * record.data.rate);
    //    };


    var group = new Ext.ux.grid.ColumnHeaderGroup({
        rows: [oddsGroupRow]
    });

    //----------------------------------------------------定义grid
    var grid = new Ext.grid.GridPanel({
        id: "EuropeOdds_" + scheduleid,
        store: Odds1x2store,
        sm: sm,
        cm: cm,
        title: '百家欧赔',
        loadMask: true,
        stripeRows: true,
        columnLines: true,
        plugins: group,
        autoWidth: true,
        //超过长度带自动滚动条
        autoScroll: true,
        border: false,
        viewConfig: {
            //自动填充
            forceFit: true,
            sortAscText: '正序排列',
            sortDescText: '倒序排列',
            columnsText: '显示/隐藏列',
            getRowClass: function (record, rowIndex, rowParams, store) {
                //                //禁用数据显示灰色   
                //                if (record.data["Odds_22"] == '1') {   //用户状态不正常
                //                    return 'importantcompany';
                //                } else {
                //                    return '';
                //                }
            }
        },
        tbar: [
        {
            text: "重点",
            //默认样式为按下
            //pressed:true,
            tooltip: "选中重点的公司",
            iconCls: "addicon",
            handler: function () {
                var items = grid.store.data.items;
                var rows = new Array();
                for (var i = 0; i < items.length; i++) {
                    if (items[i].get("isprimary") == '1')
                        rows.push(i);
                }
                grid.getSelectionModel().selectRows(rows); //选择指定一些行
            }
        }, "", "-", "", {
            text: "反选",
            //默认样式为按下
            //pressed:true,
            tooltip: "反选所有项",
            iconCls: "addicon",
            handler: function () {
                var items = grid.store.data.items;
                var row = grid.getSelectionModel().getSelections();
                var rows = new Array();
                for (var i = 0; i < items.length; i++) {
                    if (row.indexOf(items[i]) == -1)
                        rows.push(i);
                }
                grid.getSelectionModel().selectRows(rows); //选择指定一些行
            }
        }, "-", {
            text: "不选",
            tooltip: "取消选择所有项",
            iconCls: "addicon",
            handler: function () {
                grid.getSelectionModel().deselectRange(0, grid.store.data.items.length);
            }
        },
		{ xtype: 'tbfill' }
		, {
		    text: '历史统计',
		    iconCls: "totalicon",
		    tooltip: '统计所选公司的历史相似指数',
		    handler: function () {
		        var row = grid.getSelectionModel().getSelections();
		        if (row.length == 0) {
		            Ext.Msg.alert("提示信息", "您没有选中任何行!");
		            return;
		        } else {
		            var oddsArr = [];
		            Ext.each(row, function (r) {
		                oddsArr.push(r.get("data"));
		            })
		            Odds1x2History(scheduleArr, scheduleTypeArr, oddsArr);
		        }
		    }
		}, {
		    text: "凯利变化",
		    iconCls: "totalicon",
		    handler: function () {
		        var row = grid.getSelectionModel().getSelections();
		        if (row.length == 0) {
		            Ext.Msg.alert("提示信息", "您没有选中任何行!");
		            return;
		        } else {

		            for (var i = 0; i < row.length; i++) {
		                var companyids = new Array();
		                var companynames = new Array();
		                companyids.push(row[i].get("companyid"));
		                companynames.push(row[i].get("companyfullname"));
		                KellyChangeCharts(scheduleArr[0], companyids, companynames, "kelly");
		            }
		        }
		    }
		}, {
		    text: "平均凯利",
		    iconCls: "totalicon",
		    handler: function () {
		        AverageKellyLineChart(scheduleArr[0], "avekelly");
		    }
		}, {
		    text: '平均',
		    iconCls: "totalicon",
		    tooltip: '计算所选的平均赔率变化',
		    menu: [
		    {
		        text: "凯利方差",
		        handler: function () {
		            var row = grid.getSelectionModel().getSelections();
		            if (row.length == 0) {
		                Ext.Msg.alert("提示信息", "您没有选中任何行!");
		                return;
		            } else {
		                var companyids = new Array();
		                var companynames = new Array();
		                for (var i = 0; i < row.length; i++) {
		                    companyids.push(row[i].get("companyid"));
		                    companynames.push(row[i].get("companyfullname"));
		                }
		                KellyVarianceChangeCharts(scheduleArr[0], companyids, "odds");
		            }
		        }
		    }, {
		        text: "公司支持",
		        handler: function () {
		            var row = grid.getSelectionModel().getSelections();
		            if (row.length == 0) {
		                Ext.Msg.alert("提示信息", "您没有选中任何行!");
		                return;
		            } else {
		                var companyids = new Array();
		                var companynames = new Array();
		                for (var i = 0; i < row.length; i++) {
		                    companyids.push(row[i].get("companyid"));
		                    companynames.push(row[i].get("companyfullname"));
		                }
		                Odds1x2CompanyCharts(scheduleArr[0], companyids);
		            }
		        }
		    }, {
		        text: "点数支持",
		        handler: function () {
		            var row = grid.getSelectionModel().getSelections();
		            if (row.length == 0) {
		                Ext.Msg.alert("提示信息", "您没有选中任何行!");
		                return;
		            } else {
		                var companyids = new Array();
		                var companynames = new Array();
		                for (var i = 0; i < row.length; i++) {
		                    companyids.push(row[i].get("companyid"));
		                    companynames.push(row[i].get("companyfullname"));
		                }
		                Odds1x2PointCharts(scheduleArr[0], companyids);
		            }

		        }
		    }]
		}, "", "-", "", {
		    text: '刷新',
		    iconCls: "refreshicon",
		    tooltip: '刷新列表',
		    handler: function () {
		        Odds1x2store.reload();
		    }
		}, "-"], listeners: {
		    'contextmenu': function (e) {
		        e.stopEvent();
		    },
		    "rowcontextmenu": function (grid, rowIndex, e) {
		        e.stopEvent();
		    }
		}
    });


    var tab = center.getItem("EuropeOddsTab_" + scheduleid);
    if (!tab) {
        var tab = center.add({
            id: "EuropeOddsTab_" + scheduleid,
            iconCls: "totalicon",
            xtype: "panel",
            title: schedule + "欧赔",
            closable: true,
            layout: "fit",
            items: [grid]

        });
    }
    center.setActiveTab(tab);

    LoadOddsJS();
}