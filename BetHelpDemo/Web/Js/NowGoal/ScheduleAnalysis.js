/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />

var scheduleAnalysis = function (scheduleid) {
    var fields = [
            { name: 'oddswin', type: 'float' },
            { name: 'oddsdraw', type: 'float' },
            { name: 'oddslost', type: 'float' },
            { name: 'perwin', type: 'float' },
            { name: 'perdraw', type: 'float' },
            { name: 'perlost', type: 'float' },
            { name: 'time', type: 'date' }
            ];

    var store = new Ext.data.JsonStore({
        root: 'data',
        fields: fields,
        baseParams: { scheduleid: scheduleid },
        proxy: new Ext.data.HttpProxy(
            {
                url: "Data/NowGoal/ScheduleAnalysis.aspx?a=list",
                method: "POST",
                timeout: 3600000
            })
    });

    //--------------------------------------------------列头
    var cm = new Ext.grid.ColumnModel([
        {
            header: "时间",
            dataIndex: "time",
            sortable: false,
            width: 150,
            renderer: function (value) {
                return value.format("m-d H:i");
            }
        }, {
            header: "胜赔",
            dataIndex: "oddswin",
            sortable: false,
            renderer: function (value, cell, row, rowIndex, colIndex, ds) {
                if (store.getAt(rowIndex + 1)) {
                    if (value > store.getAt(rowIndex + 1).get("oddswin")) {
                        cell.cellAttr = 'bgcolor="#F7CFD6"';
                    } else if (value < store.getAt(rowIndex + 1).get("oddswin")) {
                        cell.cellAttr = 'bgcolor="#DFF3B1"';
                    }
                }
                return value;
            }
        },
        {
            header: "平赔",
            dataIndex: "oddsdraw",
            sortable: false,
            renderer: function (value, cell, row, rowIndex, colIndex, ds) {
                if (store.getAt(rowIndex + 1)) {
                    if (value > store.getAt(rowIndex + 1).get("oddsdraw")) {
                        cell.cellAttr = 'bgcolor="#F7CFD6"';
                    } else if (value < store.getAt(rowIndex + 1).get("oddsdraw")) {
                        cell.cellAttr = 'bgcolor="#DFF3B1"';
                    }
                }
                return value;
            }
        }, {
            header: "负赔",
            dataIndex: "oddslost",
            sortable: false,
            renderer: function (value, cell, row, rowIndex, colIndex, ds) {
                if (store.getAt(rowIndex + 1)) {
                    if (value > store.getAt(rowIndex + 1).get("oddslost")) {
                        cell.cellAttr = 'bgcolor="#F7CFD6"';
                    } else if (value < store.getAt(rowIndex + 1).get("oddslost")) {
                        cell.cellAttr = 'bgcolor="#DFF3B1"';
                    }
                }
                return value;
            }
        }, {
            header: "胜率",
            tooltip: "比赛打平的赔率",
            dataIndex: "perwin",
            sortable: false,
            renderer: function (value, cell, row, rowIndex, colIndex, ds) {
                if (store.getAt(rowIndex + 1)) {
                    if (value > store.getAt(rowIndex + 1).get("perwin")) {
                        cell.cellAttr = 'bgcolor="#F7CFD6"';
                    } else if (value < store.getAt(rowIndex + 1).get("perwin")) {
                        cell.cellAttr = 'bgcolor="#DFF3B1"';
                    }
                }
                return value;
            }
        }, {
            header: "平率",
            tooltip: "客场球队获胜赔率",
            dataIndex: "perdraw",
            sortable: false,
            renderer: function (value, cell, row, rowIndex, colIndex, ds) {
                if (store.getAt(rowIndex + 1)) {
                    if (value > store.getAt(rowIndex + 1).get("perdraw")) {
                        cell.cellAttr = 'bgcolor="#F7CFD6"';
                    } else if (value < store.getAt(rowIndex + 1).get("perdraw")) {
                        cell.cellAttr = 'bgcolor="#DFF3B1"';
                    }
                }
                return value;
            }
        }, {
            header: "负率",
            tooltip: "主场球队获胜赔率",
            dataIndex: "perlost",
            sortable: false,
            renderer: function (value, cell, row, rowIndex, colIndex, ds) {
                if (store.getAt(rowIndex + 1)) {
                    if (value > store.getAt(rowIndex + 1).get("perlost")) {
                        cell.cellAttr = 'bgcolor="#F7CFD6"';
                    } else if (value < store.getAt(rowIndex + 1).get("perlost")) {
                        cell.cellAttr = 'bgcolor="#DFF3B1"';
                    }
                }
                return value;
            }
        }, {
            header: "胜差",
            sortable: false,
            renderer: function (value, cell, row, rowIndex, colIndex, ds) {
                value = row.get("perwin") - row.get("oddswin");
                if (store.getAt(rowIndex + 1)) {
                    value1 = store.getAt(rowIndex + 1).get("perwin") - store.getAt(rowIndex + 1).get("oddswin");
                    if (value > value1) {
                        cell.cellAttr = 'bgcolor="#F7CFD6"';
                    } else if (value < value1) {
                        cell.cellAttr = 'bgcolor="#DFF3B1"';
                    }
                }
                return value;
            }
        }, {
            header: "平差",
            sortable: false,
            renderer: function (value, cell, row, rowIndex, colIndex, ds) {
                value = row.get("perdraw") - row.get("oddsdraw");
                if (store.getAt(rowIndex + 1)) {
                    value1 = store.getAt(rowIndex + 1).get("perdraw") - store.getAt(rowIndex + 1).get("oddsdraw");
                    if (value > value1) {
                        cell.cellAttr = 'bgcolor="#F7CFD6"';
                    } else if (value < value1) {
                        cell.cellAttr = 'bgcolor="#DFF3B1"';
                    }
                }
                return value;
            }
        }, {
            header: "负差",
            sortable: false,
            renderer: function (value, cell, row, rowIndex, colIndex, ds) {
                value = row.get("perlost") - row.get("oddslost");
                if (store.getAt(rowIndex + 1)) {
                    value1 = store.getAt(rowIndex + 1).get("perlost") - store.getAt(rowIndex + 1).get("oddslost");
                    if (value > value1) {
                        cell.cellAttr = 'bgcolor="#F7CFD6"';
                    } else if (value < value1) {
                        cell.cellAttr = 'bgcolor="#DFF3B1"';
                    }
                }
                return value;
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
        }
    });

    var win = new Ext.Window({
        title: "历史分析",
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
        layout: "fit",
        buttonAlign: "center",
        items: [grid],
        listeners: {
            "show": function () {
                //当window show事件发生时清空一下表单
                store.load();
            }
        }
    });

    win.show();
}

var oddsChange = function (oddsid) {
    var grid = new Ext.grid.GridPanel({
        store: new Ext.data.JsonStore({
            fields: [{ name: 'win', type: 'float' }, { name: 'draw', type: 'float' }, { name: 'lost', type: 'float' },
                { name: 'time', type: 'date'}]
        }),
        cm: new Ext.grid.ColumnModel([{
            header: "胜",
            dataIndex: "win",
            sortable: false
        }, {
            header: "平",
            dataIndex: "draw",
            sortable: false
        }, {
            header: "负",
            dataIndex: "lost",
            sortable: false
        }, {
            header: "变化时间",
            dataIndex: "time",
            sortable: false,
            width: 150,
            renderer: function (t) {
                t = new Date(Date.UTC(t.getFullYear(), t.getMonth(), t.getDate(), t.getHours(), t.getMinutes(), t.getSeconds()));
                return t.format("m-d H:i");
            }
        }]),
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
        }
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
        layout: "fit",
        buttonAlign: "center",
        items: [grid]
    });

    Ext.Ajax.request({
        url: "Data/NowGoal/GetRemoteHtml.aspx?a=OddsHistory",
        params: { oddsid: oddsid },
        success: function (res) {
            document.getElementById("OddsHistory").innerHTML = res.responseText;
            var t = Ext.fly("OddsHistory").query("table")[0];
            var changelist = [];
            for (var i = 1; i < t.rows.length; i++) {
                changelist.push({
                    win: t.rows[i].cells[0].innerText,
                    draw: t.rows[i].cells[1].innerText,
                    lost: t.rows[i].cells[2].innerText,
                    time: eval(t.rows[i].cells[3].innerText.replace("showtime","new Date"))
                });
            }
            win.show();
            grid.getStore().loadData(changelist);
        }
    });



}