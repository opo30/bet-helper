/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />

Array.prototype.max = function () {
    return Math.max.apply({}, this)
}
Array.prototype.min = function () {
    return Math.min.apply({}, this)
}

var Odds1x2History = function (scheduleArr, scheduleTypeArr, oddsArr) {
    if (typeof (scheduleArr) == "number") {
        if (oddsArr == undefined) {
            oddsHttp.open("get", "Data/NowGoal/GetRemoteFile.aspx?f=oddsjs&path=" + scheduleArr + ".js", false);
            oddsHttp.send(null);
            eval(oddsHttp.responseText);
            oddsArr = [];
            for (var i = 0; i < game.length; i++) {
                var arr = game[i].split('|');
                if (arr[20] == "1" && arr[10] != "" && arr[11] != "" && arr[12] != "") {
                    oddsArr.push(game[i]);
                }
            }
        }

        for (var i = 0; i < A.length; i++) {
            if (A[i][0] == scheduleArr) {
                scheduleArr = A[i];
                scheduleTypeArr = B[A[i][1]];
            }
        }
        if (typeof (scheduleArr) == "number")
            for (var i = 0; i < HistoryScore.A.length; i++) {
                if (HistoryScore.A[i][0] == scheduleArr) {
                    scheduleArr = HistoryScore.A[i];
                    scheduleTypeArr = HistoryScore.B[HistoryScore.A[i][1]];
                }
            }
    }



    var fields = [
            { name: 'companyid', type: 'int' },
            { name: 'name', type: 'string' },
            { name: 'perwin', type: 'float' },
            { name: 'perdraw', type: 'float' },
            { name: 'perlost', type: 'float' },
            { name: 'oddswin', type: 'float' },
            { name: 'oddsdraw', type: 'float' },
            { name: 'oddslost', type: 'float' },
            { name: 'avgscore', type: 'float' },
            { name: 'totalCount', type: 'int' }
            ];

    var params = {
        stypeid: scheduleTypeArr.join('^'),
        oddsarr: oddsArr.join('^'),
        schedulearr: scheduleArr.join('^')
    };

    //    if (Ext.getDom('tr1_' + scheduleArr[0])) {
    //        params.pankou = Ext.getDom('tr1_' + scheduleArr[0]).getAttribute("odds");
    //    }

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
                if (r.get("oddswin") > store.getAt(index - 1).get("oddswin")) {
                    grid.getView().getCell(index, 5).style.backgroundColor = "#F7CFD6"; //上涨#F7CFD6;下降#DFF3B1;
                } else if (r.get("oddswin") < store.getAt(index - 1).get("oddswin")) {
                    grid.getView().getCell(index, 5).style.backgroundColor = "#DFF3B1"; //上涨#F7CFD6;下降#DFF3B1;
                }
                if (r.get("oddsdraw") > store.getAt(index - 1).get("oddsdraw")) {
                    grid.getView().getCell(index, 6).style.backgroundColor = "#F7CFD6"; //上涨#F7CFD6;下降#DFF3B1;
                } else if (r.get("oddsdraw") < store.getAt(index - 1).get("oddsdraw")) {
                    grid.getView().getCell(index, 6).style.backgroundColor = "#DFF3B1"; //上涨#F7CFD6;下降#DFF3B1;
                }
                if (r.get("oddslost") > store.getAt(index - 1).get("oddslost")) {
                    grid.getView().getCell(index, 7).style.backgroundColor = "#F7CFD6"; //上涨#F7CFD6;下降#DFF3B1;
                } else if (r.get("oddslost") < store.getAt(index - 1).get("oddslost")) {
                    grid.getView().getCell(index, 7).style.backgroundColor = "#DFF3B1"; //上涨#F7CFD6;下降#DFF3B1;
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
            renderer: function (value, cell, row, rowIndex, colIndex, ds) {
                return value.toFixed(2);
            }
        }, {
            header: "和局",
            tooltip: "比赛打平的赔率",
            dataIndex: "perdraw",
            sortable: false,
            renderer: function (value, cell, row, rowIndex, colIndex, ds) {
                return value.toFixed(2);
            }
        }, {
            header: "客胜",
            tooltip: "客场球队获胜赔率",
            dataIndex: "perlost",
            sortable: false,
            renderer: function (value, cell, row, rowIndex, colIndex, ds) {
                return value.toFixed(2);
            }
        }, {
            header: "主胜",
            tooltip: "主场球队获胜赔率",
            dataIndex: "oddswin",
            sortable: false,
            renderer: function (value, cell, row, rowIndex, colIndex, ds) {
                return value.toFixed(2);
            }
        }, {
            header: "和局",
            tooltip: "比赛打平的赔率",
            dataIndex: "oddsdraw",
            sortable: false,
            renderer: function (value, cell, row, rowIndex, colIndex, ds) {
                return value.toFixed(2);
            }
        }, {
            header: "客胜",
            tooltip: "客场球队获胜赔率",
            dataIndex: "oddslost",
            sortable: false,
            renderer: function (value, cell, row, rowIndex, colIndex, ds) {
                return value.toFixed(2);
            }
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
                { name: 'half', type: 'string' },
                { name: 'g_teamname', type: 'string' },
                { name: 'league', type: 'string' },
                { name: 'bgcolor', type: 'string' }, { name: 'rangqiu', type: 'float' }, { name: 's_time', type: 'string' }, { name: 'scount', type: 'int' }, { name: 'numResult', type: 'float'}],
               root: "data"
           }),
            //groupField: "scount",
            sortInfo: { field: "s_time", direction: "DESC" }
        }),
        cm: new Ext.grid.ColumnModel([{
            header: "赛事",
            dataIndex: "league",
            sortable: true,
            align: "center",
            width: 8,
            css: 'vertical-align: inherit;color:white;',
            renderer: function (value, cell, row, rowIndex, colIndex, ds) {
                cell.cellAttr = 'bgcolor="' + row.get("bgcolor") + '"';
                return value;
            }
        },
        {
            header: "时间",
            dataIndex: "s_time",
            sortable: true,
            align: "center",
            css: 'vertical-align: inherit;',
            width: 10
        }, {
            header: "主队",
            dataIndex: "h_teamname",
            sortable: true,
            width: 16,
            align: "right",
            css: 'vertical-align: inherit;',
            renderer: function (value, cell, row, rowIndex, colIndex, ds) {
                cell.css = 'a1';
                return value;
            }
        },
		{
		    header: "比分",
		    dataIndex: "score",
		    sortable: true,
		    width: 5,
		    align: "center",
		    css: 'vertical-align: inherit;',
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.css = "td_score";
		        return value;
		    }
		}, {
		    header: "客队",
		    dataIndex: "g_teamname",
		    sortable: true,
		    width: 16,
		    align: "left",
		    css: 'vertical-align: inherit;',
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.css = 'a2';
		        return value;
		    }
		}, {
		    header: "半场",
		    dataIndex: "half",
		    sortable: true,
		    summaryType: 'sum',
		    width: 5,
		    align: "center",
		    css: 'vertical-align: inherit;',
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.css = 'td_half';
		        return value;
		    }
		}, {
		    header: "指数",
		    dataIndex: "rangqiu",
		    sortable: true,
		    width: 5,
		    summaryType: 'sum'
		}, {
		    header: "盘路",
		    dataIndex: "numResult",
		    sortable: true,
		    width: 5,
		    renderer: function (value) {
		        var goalResult = "";
		        if (value > 0) {
		            if (value == 0.25)
		                goalResult = "<font color='red'>赢半</font>";
		            else
		                goalResult = "<font color='red'>赢</font>";
		        }
		        else if (value == 0)
		            goalResult = "<font color='blue'>走</font>";
		        else {
		            if (value == -0.25)
		                goalResult = "<font color='green'>输半</font>";
		            else
		                goalResult = "<font color='green'>输</font>";
		        }
		        return goalResult;
		    },
		    summaryType: 'sum'
		}, {
		    header: "相同",
		    dataIndex: "scount",
		    width: 5,
		    sortable: true
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
        })
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
        items: [grid, grid1],
        listeners: {
            "show": function () {
                //当window show事件发生时清空一下表单
                store.load();
            }
        }
    });

    win.show();
}