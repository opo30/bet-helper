﻿/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />

Array.prototype.max = function () {
    return Math.max.apply({}, this)
}
Array.prototype.min = function () {
    return Math.min.apply({}, this)
}

var Odds1x2History = function (scheduleArr, scheduleTypeArr, oddsArr) {

    var fields = [
            { name: 'companyid', type: 'int' },
            { name: 'name', type: 'string' },
            { name: 'perwin', type: 'float' },
            { name: 'perdraw', type: 'float' },
            { name: 'perlost', type: 'float' },
            { name: 'rqwin', type: 'float' },
            { name: 'rqdraw', type: 'float' },
            { name: 'rqlost', type: 'float' },
            { name: 'avgscore', type: 'float' },
            { name: 'totalCount', type: 'int' }
            ];
    var params = {
        stypeid: scheduleTypeArr.length > 0 ? scheduleTypeArr[0] : '',
        oddsarr: oddsArr.join('^'),
        schedulearr: scheduleArr.join('^')
    };

    var store = new Ext.data.JsonStore({
        root: 'data',
        fields: fields,
        baseParams: params,
        proxy: new Ext.data.HttpProxy(
            {
                url: "Data/NowGoal/GetOdds1x2History.aspx?a=stat",
                method: "POST",
                timeout: 3600000
            })
    });

    store.on('load', function (s, records) {
        var perwin = [], perdraw = [], perlost = [];
        Ext.each(oddsArr, function (oddsStr) {
            var oddsVal = oddsStr.split('|');
            perwin.push(parseFloat(oddsVal[6]));
            perdraw.push(parseFloat(oddsVal[7]));
            perlost.push(parseFloat(oddsVal[8]));
        });
        s.each(function (r, index) {
            if (index % 2 > 0) {
                if (r.get("perwin") > store.getAt(index - 1).get("perwin")) {
                    grid.getView().getCell(index, 2).style.backgroundColor = "#F7CFD6"; //上涨#F7CFD6;下降#DFF3B1;
                } else if (r.get("perwin") < store.getAt(index - 1).get("perwin")) {
                    grid.getView().getCell(index, 2).style.backgroundColor = "#DFF3B1"; //上涨#F7CFD6;下降#DFF3B1;
                }
                if (r.get("perdraw") > store.getAt(index - 1).get("perdraw")) {
                    grid.getView().getCell(index, 3).style.backgroundColor = "#F7CFD6"; //上涨#F7CFD6;下降#DFF3B1;
                } else if (r.get("perdraw") < store.getAt(index - 1).get("perdraw")) {
                    grid.getView().getCell(index, 3).style.backgroundColor = "#DFF3B1"; //上涨#F7CFD6;下降#DFF3B1;
                }
                if (r.get("perlost") > store.getAt(index - 1).get("perlost")) {
                    grid.getView().getCell(index, 4).style.backgroundColor = "#F7CFD6"; //上涨#F7CFD6;下降#DFF3B1;
                } else if (r.get("perlost") < store.getAt(index - 1).get("perlost")) {
                    grid.getView().getCell(index, 4).style.backgroundColor = "#DFF3B1"; //上涨#F7CFD6;下降#DFF3B1;
                }
                if (r.get("rqwin") > store.getAt(index - 1).get("rqwin")) {
                    grid.getView().getCell(index, 5).style.backgroundColor = "#F7CFD6"; //上涨#F7CFD6;下降#DFF3B1;
                } else if (r.get("rqwin") < store.getAt(index - 1).get("rqwin")) {
                    grid.getView().getCell(index, 5).style.backgroundColor = "#DFF3B1"; //上涨#F7CFD6;下降#DFF3B1;
                }
                if (r.get("rqdraw") > store.getAt(index - 1).get("rqdraw")) {
                    grid.getView().getCell(index, 6).style.backgroundColor = "#F7CFD6"; //上涨#F7CFD6;下降#DFF3B1;
                } else if (r.get("rqdraw") < store.getAt(index - 1).get("rqdraw")) {
                    grid.getView().getCell(index, 6).style.backgroundColor = "#DFF3B1"; //上涨#F7CFD6;下降#DFF3B1;
                }
                if (r.get("rqlost") > store.getAt(index - 1).get("rqlost")) {
                    grid.getView().getCell(index, 7).style.backgroundColor = "#F7CFD6"; //上涨#F7CFD6;下降#DFF3B1;
                } else if (r.get("rqlost") < store.getAt(index - 1).get("rqlost")) {
                    grid.getView().getCell(index, 7).style.backgroundColor = "#DFF3B1"; //上涨#F7CFD6;下降#DFF3B1;
                }
                if (r.get("avgscore") > store.getAt(index - 1).get("avgscore")) {
                    grid.getView().getCell(index, 8).style.backgroundColor = "#F7CFD6"; //上涨#F7CFD6;下降#DFF3B1;
                } else if (r.get("avgscore") < store.getAt(index - 1).get("avgscore")) {
                    grid.getView().getCell(index, 8).style.backgroundColor = "#DFF3B1"; //上涨#F7CFD6;下降#DFF3B1;
                }
            }
        });
    });

    //--------------------------------------------------列头
    var cm = new Ext.grid.ColumnModel([
        {
            header: "统计",
            dataIndex: "name",
            sortable: false
        },
        {
            header: "总数",
            dataIndex: "totalCount",
            sortable: false
        }, {
            header: "主胜",
            tooltip: "主场球队获胜赔率",
            dataIndex: "perwin",
            sortable: false,
            renderer: function (value, last, row, index) {
                var change = "";
                if (index == 3) {
                    var num1 = store.getAt(1).get("perwin") - store.getAt(0).get("perwin");
                    var num2 = value - store.getAt(2).get("perwin");
                    var num3 = store.getAt(2).get("perwin") - store.getAt(0).get("perwin");
                    var num4 = value - store.getAt(1).get("perwin");
                    change = "(" + (num1 + num2 + num3 + num4).toFixed(2) + ")";
                }
                return value.toFixed(2) + change;
            }
        }, {
            header: "和局",
            tooltip: "比赛打平的赔率",
            dataIndex: "perdraw",
            sortable: false,
            renderer: function (value, last, row, index) {
                var change = "";
                if (index == 3) {
                    var num1 = store.getAt(1).get("perdraw") - store.getAt(0).get("perdraw");
                    var num2 = value - store.getAt(2).get("perdraw");
                    var num3 = store.getAt(2).get("perdraw") - store.getAt(0).get("perdraw");
                    var num4 = value - store.getAt(1).get("perdraw");
                    change = "(" + (num1 + num2 + num3 + num4).toFixed(2) + ")";
                }
                return value.toFixed(2) + change;
            }
        }, {
            header: "客胜",
            tooltip: "客场球队获胜赔率",
            dataIndex: "perlost",
            sortable: false,
            renderer: function (value, last, row, index) {
                var change = "";
                if (index == 3) {
                    var num1 = store.getAt(1).get("perlost") - store.getAt(0).get("perlost");
                    var num2 = value - store.getAt(2).get("perlost");
                    var num3 = store.getAt(2).get("perlost") - store.getAt(0).get("perlost");
                    var num4 = value - store.getAt(1).get("perlost");
                    change = "(" + (num1 + num2 + num3 + num4).toFixed(2) + ")";
                }
                return value.toFixed(2) + change;
            }
        }, {
            header: "赢",
            tooltip: "主场球队获胜赔率",
            dataIndex: "rqwin",
            sortable: false
        }, {
            header: "走",
            tooltip: "比赛打平的赔率",
            dataIndex: "rqdraw",
            sortable: false
        }, {
            header: "输",
            tooltip: "客场球队获胜赔率",
            dataIndex: "rqlost",
            sortable: false
        }, {
            header: "进球数",
            dataIndex: "avgscore",
            sortable: false,
            renderer: function (value) {
                return value.toFixed(2);
            }
        }
    ]);


    //----------------------------------------------------定义grid
    var grid = new Ext.grid.GridPanel({
        store: store,
        cm: cm,
        region: "north",
        loadMask: true,
        stripeRows: true,
        columnLines: true,
        height: 200,
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
            }
        },
        listeners: {
            rowclick: function (grid, rowIndex, cellIndex, e) {
                grid1.getStore().baseParams.rowindex = rowIndex;
                grid1.getStore().load();
            }
        }
    });
    var grid1 = new Ext.grid.GridPanel({
        width: 250,
        store: new Ext.data.JsonStore({
            fields: [
            { name: 'sdate', type: 'date' },
            { name: 'sumwin', type: 'int' },
            { name: 'sumdraw', type: 'int' },
            { name: 'sumlost', type: 'int' },
            { name: 'avgscore', type: 'float' },
            { name: 'totalCount', type: 'int'}],
            root: "data",
            baseParams: { rowindex: 0 },
            proxy: new Ext.data.HttpProxy({
                url: "Data/NowGoal/GetOdds1x2History.aspx?a=statdate",
                method: "POST",
                timeout: 3600000
            })
        }),
        cm: new Ext.grid.ColumnModel([
        {
            header: "时间",
            dataIndex: "sdate",
            sortable: false,
            renderer: function (value) {
                return Ext.util.Format.date(value, "Y-m-d");
            }
        }, {
            header: "胜",
            dataIndex: "sumwin",
            sortable: false,
            width: 20
        }, {
            header: "平",
            dataIndex: "sumdraw",
            sortable: false,
            width: 20
        }, {
            header: "负",
            dataIndex: "sumlost",
            sortable: false,
            width: 20
        }, {
            header: "进球数",
            dataIndex: "avgscore",
            sortable: false,
            width: 40,
            renderer: function (value) {
                return value.toFixed(2);
            }
        }
    ]),
        region: "west",
        loadMask: true,
        stripeRows: true,
        columnLines: true,
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
            }
        },
        listeners: {
            rowclick: function (g, rowIndex, cellIndex, e) {
                grid2.getStore().baseParams.date = Ext.util.Format.date(grid1.getStore().getAt(rowIndex).get("sdate"), 'Y-m-d');
                grid2.getStore().load();
            }
        }
    });
    var summary = new Ext.ux.grid.HybridSummary();
    var grid2 = new Ext.grid.GridPanel({
        region: "center",
        columnLines: true,
        store: new Ext.data.GroupingStore({
            baseParams: { rowindex: 0 },
            proxy: new Ext.data.HttpProxy({
                url: "Data/NowGoal/GetOdds1x2History.aspx?a=list",
                method: "POST",
                timeout: 3600000
            }),
            reader: new Ext.data.JsonReader(
           {
               fields: [{ name: 'h_teamname', type: 'string' },
                { name: 'score', type: 'string' },
                { name: 'g_teamname', type: 'string' },
                { name: 'e_win', type: 'string' },
                { name: 'e_draw', type: 'string' },
                { name: 'e_lost', type: 'string' }, { name: 'rangqiu', type: 'float' }, { name: 's_time', type: 'string' }, { name: 'companyid', type: 'int'}],
               root: "data"
           }),
            groupField: 'companyid'
        }),
        cm: new Ext.grid.ColumnModel([
        {
            header: "主队",
            dataIndex: "h_teamname",
            sortable: true,
            width: 120
        },
		{
		    header: "比分",
		    dataIndex: "score",
		    sortable: true,
		    width: 60
		}, {
		    header: "客队",
		    dataIndex: "g_teamname",
		    sortable: true,
		    width: 120
		}, {
		    header: "胜",
		    dataIndex: "e_win",
		    sortable: true,
		    summaryType: 'sum'
		}, {
		    header: "平",
		    dataIndex: "e_draw",
		    sortable: true,
		    summaryType: 'sum'
		}, {
		    header: "负",
		    dataIndex: "e_lost",
		    sortable: true,
		    summaryType: 'average'
		}, {
		    header: "指数",
		    dataIndex: "rangqiu",
		    sortable: true,
		    summaryType: 'sum'
		}, {
		    header: "时间",
		    dataIndex: "s_time",
		    sortable: true
		}, {
		    header: "公司",
		    dataIndex: "companyid",
		    hidden: true,
		    renderer: function (value) {
		        var showname;
		        var eodds;
		        Ext.each(oddsArr, function (oddsStr) {
		            var oddsInfo = oddsStr.split("|");
		            if (!showname && value == parseInt(oddsInfo[0])) {
		                showname = oddsInfo[21];
		                eodds = oddsInfo[10] + " " + oddsInfo[11] + " " + oddsInfo[12];
		            }
		        });
		        return showname + " " + eodds;
		    }
		}
]),
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
        plugins: summary
    });

    var win = new Ext.Window({
        title: "历史赔率",
        width: 800,
        height: 500,
        plain: true,
        iconCls: "total",
        //不可以随意改变大小
        resizable: true,
        //是否可以拖动
        draggable: true,
        defaultType: "textfield",
        labelWidth: 100,
        collapsible: true, //允许缩放条
        closeAction: 'close',
        closable: true,
        maximizable: true,
        //弹出模态窗体
        modal: false,
        layout: "border",
        buttonAlign: "center",
        items: [grid, grid1, grid2],
        listeners: {
            "show": function () {
                //当window show事件发生时清空一下表单
                store.load();
            }
        }
    });

    win.show();
}