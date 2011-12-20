/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />
var Odds1x2History = function (scheduleArr, scheduleTypeArr, oddsArr) {

    var fields = [
            { name: 'companyid', type: 'int' },
            { name: 'name', type: 'string' },
            { name: 'perwin', type: 'float' },
            { name: 'perdraw', type: 'float' },
            { name: 'perlost', type: 'float' },
            { name: 'oddsperwin', type: 'float' },
            { name: 'oddsperdraw', type: 'float' },
            { name: 'oddsperlost', type: 'float' },
            { name: 'avgscore', type: 'float' },
            { name: 'totalCount', type: 'int' }
            ];
    var params = {
        stypeid: scheduleTypeArr[0],
        oddsarr: oddsArr.join('^')
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
        s.each(function (r, index) {
            if (index > 0) {
                if (r.get("perwin") > s.getAt(index - 1).get("perwin")) {
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
                if (r.get("avgscore") > store.getAt(index - 1).get("avgscore")) {
                    grid.getView().getCell(index, 5).style.backgroundColor = "#F7CFD6"; //上涨#F7CFD6;下降#DFF3B1;
                } else if (r.get("avgscore") < store.getAt(index - 1).get("avgscore")) {
                    grid.getView().getCell(index, 5).style.backgroundColor = "#DFF3B1"; //上涨#F7CFD6;下降#DFF3B1;
                }
            }
        });
    });

    //--------------------------------------------------列头
    var cm = new Ext.grid.ColumnModel([
        {
            header: "统计",
            dataIndex: "name",
            sortable: false,
            width: 40
        },
        {
            header: "总数",
            dataIndex: "totalCount",
            sortable: false,
            width: 30
        }, {
            header: "主胜",
            tooltip: "主场球队获胜赔率",
            dataIndex: "perwin",
            sortable: false,
            renderer: function (value) {
                return value.toFixed(2);
            }
        }, {
            header: "和局",
            tooltip: "比赛打平的赔率",
            dataIndex: "perdraw",
            sortable: false,
            renderer: function (value) {
                return value.toFixed(2);
            }
        }, {
            header: "客胜",
            tooltip: "客场球队获胜赔率",
            dataIndex: "perlost",
            sortable: false,
            renderer: function (value) {
                return value.toFixed(2);
            }
        }, {
            header: "进球数",
            dataIndex: "avgscore",
            sortable: false,
            renderer: function (value) {
                return value.toFixed(2);
            }
        }, {
            header: "主胜",
            tooltip: "主场球队获胜赔率",
            dataIndex: "oddsperwin",
            sortable: false,
            renderer: function (value) {
                return value.toFixed(2);
            }
        }, {
            header: "和局",
            tooltip: "比赛打平的赔率",
            dataIndex: "oddsperdraw",
            sortable: false,
            renderer: function (value) {
                return value.toFixed(2);
            }
        }, {
            header: "客胜",
            tooltip: "客场球队获胜赔率",
            dataIndex: "oddsperlost",
            sortable: false,
            renderer: function (value) {
                return value.toFixed(2);
            }
        }
    ]);


    //----------------------------------------------------定义grid
    var grid = new Ext.grid.GridPanel({
        id: "oddsstat-grid",
        store: store,
        cm: cm,
        region: "north",
        loadMask: true,
        stripeRows: true,
        columnLines: true,
        height: 100,
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
    var summary = new Ext.ux.grid.HybridSummary();
    var grid1 = new Ext.grid.GridPanel({
        id: "oddslist-grid",
        region: "center",
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