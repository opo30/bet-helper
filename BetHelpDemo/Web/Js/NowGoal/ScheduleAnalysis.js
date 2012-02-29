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
            { name: 'chawin', type: 'float' },
            { name: 'chadraw', type: 'float' },
            { name: 'chalost', type: 'float' },
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
                if (value > 0) {
                    cell.cellAttr = 'bgcolor="#F7CFD6"';
                } else if (value < 0) {
                    cell.cellAttr = 'bgcolor="#DFF3B1"';
                }
                return value;
            }
        }, {
            header: "平差",
            sortable: false,
            renderer: function (value, cell, row, rowIndex, colIndex, ds) {
                value = row.get("perdraw") - row.get("oddsdraw");
                if (value > 0) {
                    cell.cellAttr = 'bgcolor="#F7CFD6"';
                } else if (value < 0) {
                    cell.cellAttr = 'bgcolor="#DFF3B1"';
                }
                return value;
            }
        }, {
            header: "负差",
            sortable: false,
            renderer: function (value, cell, row, rowIndex, colIndex, ds) {
                value = row.get("perlost") - row.get("oddslost");
                if (value > 0) {
                    cell.cellAttr = 'bgcolor="#F7CFD6"';
                } else if (value < 0) {
                    cell.cellAttr = 'bgcolor="#DFF3B1"';
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

    var chartStore = new Ext.data.JsonStore({
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


    var chart = new Ext.chart.LineChart({
        store: chartStore,
        autoShow: true,
        flex: 1,
        series: [
                {
                    type: 'line',
                    displayName: '胜',
                    xField: 'time',
                    yField: 'oddswin',
                    style: {
                        color: 0xFF0000,
                        size: 5
                    }
                }, {
                    type: 'line',
                    displayName: '平',
                    xField: 'time',
                    yField: 'oddsdraw',
                    style: {
                        color: 0x008000,
                        size: 5
                    }
                }, {
                    type: 'line',
                    displayName: '负',
                    xField: 'time',
                    yField: 'oddslost',
                    style: {
                        color: 0x0000FF,
                        size: 5
                    }
                }],
        tipRenderer: function (chart, record, index, series) {
            var s = "时间 " + record.data.time.format('m月d日 H时i分') + "\r\n";
            s += "胜差 " + Ext.util.Format.round(record.data.oddswin, 2) + " ";
            s += "平差 " + Ext.util.Format.round(record.data.oddsdraw, 2) + " ";
            s += "负差 " + Ext.util.Format.round(record.data.oddslost, 2) + "\r\n";
            return s;
        },
        yAxis: new Ext.chart.NumericAxis(),
        xAxis: new Ext.chart.TimeAxis({
            labelRenderer: Ext.util.Format.dateRenderer('H:i'),
            majorUnit: 10
        }),
        chartStyle: {
            padding: 10,
            animationEnabled: true,
            font: {
                name: 'Tahoma',
                color: 0x444444,
                size: 11
            },
            dataTip: {
                padding: 5,
                border: {
                    color: 0x99bbe8,
                    size: 1
                },
                background: {
                    color: 0xDAE7F6,
                    alpha: .9
                },
                font: {
                    name: 'Tahoma',
                    color: 0x15428B,
                    size: 10,
                    bold: true
                }
            },
            xAxis: {
                color: 0x69aBc8,
                majorTicks: { color: 0x69aBc8, length: 4 },
                minorTicks: { color: 0x69aBc8, length: 2 },
                majorGridLines: { size: 1, color: 0xeeeeee }
            },
            yAxis: {
                color: 0x69aBc8,
                majorTicks: { color: 0x69aBc8, length: 4 },
                minorTicks: { color: 0x69aBc8, length: 2 },
                majorGridLines: { size: 1, color: 0xdfe8f6 }
            }
        },
        listeners: {
            itemclick: function (o) {
                if (o.seriesIndex == 0) {
                    var rec = store.getAt(o.index);
                    var rec1 = store.getAt(o.index - 1);
                    var html = "<font color=" + TdBgColor(rec.get('homek'), rec1.get('homek')) + ">" + rec.get('homek') + "</font>";
                    Ext.Msg.show({
                        title: '提示',
                        msg: html,
                        modal: false,
                        buttons: Ext.Msg.OK
                    });
                }
                if (o.seriesIndex == 1) {
                    var rec = store.getAt(o.index);
                    var rec1 = store.getAt(o.index - 1);
                    var html = "<font color=" + TdBgColor(rec.get('drawk'), rec1.get('drawk')) + ">" + rec.get('drawk') + "</font>&nbsp;&nbsp;";
                    Ext.Msg.show({
                        title: '提示',
                        msg: html,
                        modal: false,
                        buttons: Ext.Msg.OK
                    });
                }
                if (o.seriesIndex == 2) {
                    var rec = store.getAt(o.index);
                    var rec1 = store.getAt(o.index - 1);
                    var html = "<font color=" + TdBgColor(rec.get('awayk'), rec1.get('awayk')) + ">" + rec.get('awayk') + "</font>&nbsp;&nbsp;";
                    Ext.Msg.show({
                        title: '提示',
                        msg: html,
                        modal: false,
                        buttons: Ext.Msg.OK
                    });
                }

            }
        }
    });


    var chart1 = new Ext.chart.LineChart({
        flex: 1,
        store: chartStore,
        //autoShow: true,

        series: [
                {
                    type: 'line',
                    displayName: '胜',
                    xField: 'time',
                    yField: 'perwin',
                    style: {
                        color: 0xFF0000,
                        size: 5
                    }
                }, {
                    type: 'line',
                    displayName: '平',
                    xField: 'time',
                    yField: 'perdraw',
                    style: {
                        color: 0x008000,
                        size: 5
                    }
                }, {
                    type: 'line',
                    displayName: '负',
                    xField: 'time',
                    yField: 'perlost',
                    style: {
                        color: 0x0000FF,
                        size: 5
                    }
                }],
        tipRenderer: function (chart, record, index, series) {
            var s = "时间 " + record.data.time.format('m月d日 h时i分') + "\r\n";
            s += "胜差 " + Ext.util.Format.round(record.data.perwin, 2) + " ";
            s += "平差 " + Ext.util.Format.round(record.data.perdraw, 2) + " ";
            s += "负差 " + Ext.util.Format.round(record.data.perlost, 2) + "\r\n";
            return s;
        },
        yAxis: new Ext.chart.NumericAxis(),
        xAxis: new Ext.chart.TimeAxis({
            labelRenderer: Ext.util.Format.dateRenderer('H:i'),
            majorUnit: 10
        }),
        chartStyle: {
            padding: 10,
            animationEnabled: true,
            font: {
                name: 'Tahoma',
                color: 0x444444,
                size: 11
            },
            dataTip: {
                padding: 5,
                border: {
                    color: 0x99bbe8,
                    size: 1
                },
                background: {
                    color: 0xDAE7F6,
                    alpha: .9
                },
                font: {
                    name: 'Tahoma',
                    color: 0x15428B,
                    size: 10,
                    bold: true
                }
            },
            xAxis: {
                color: 0x69aBc8,
                majorTicks: { color: 0x69aBc8, length: 4 },
                minorTicks: { color: 0x69aBc8, length: 2 },
                majorGridLines: { size: 1, color: 0xeeeeee }
            },
            yAxis: {
                color: 0x69aBc8,
                majorTicks: { color: 0x69aBc8, length: 4 },
                minorTicks: { color: 0x69aBc8, length: 2 },
                majorGridLines: { size: 1, color: 0xdfe8f6 }
            }
        },
        listeners: {

        }
    });

    var chart2 = new Ext.chart.LineChart({
        store: chartStore,
        autoShow: true,
        flex: 1,
        series: [
                {
                    type: 'line',
                    displayName: '胜',
                    xField: 'time',
                    yField: 'chawin',
                    style: {
                        color: 0xFF0000,
                        size: 5
                    }
                }, {
                    type: 'line',
                    displayName: '平',
                    xField: 'time',
                    yField: 'chadraw',
                    style: {
                        color: 0x008000,
                        size: 5
                    }
                }, {
                    type: 'line',
                    displayName: '负',
                    xField: 'time',
                    yField: 'chalost',
                    style: {
                        color: 0x0000FF,
                        size: 5
                    }
                }],
        tipRenderer: function (chart, record, index, series) {
            var s = "时间 " + record.data.time.format('m月d日 H时i分') + "\r\n";
            s += "胜差 " + Ext.util.Format.round(record.data.oddswin, 2) + " ";
            s += "平差 " + Ext.util.Format.round(record.data.oddsdraw, 2) + " ";
            s += "负差 " + Ext.util.Format.round(record.data.oddslost, 2) + "\r\n";
            return s;
        },
        yAxis: new Ext.chart.NumericAxis(),
        xAxis: new Ext.chart.TimeAxis({
            labelRenderer: Ext.util.Format.dateRenderer('H:i'),
            majorUnit: 10
        }),
        chartStyle: {
            padding: 10,
            animationEnabled: true,
            font: {
                name: 'Tahoma',
                color: 0x444444,
                size: 11
            },
            dataTip: {
                padding: 5,
                border: {
                    color: 0x99bbe8,
                    size: 1
                },
                background: {
                    color: 0xDAE7F6,
                    alpha: .9
                },
                font: {
                    name: 'Tahoma',
                    color: 0x15428B,
                    size: 10,
                    bold: true
                }
            },
            xAxis: {
                color: 0x69aBc8,
                majorTicks: { color: 0x69aBc8, length: 4 },
                minorTicks: { color: 0x69aBc8, length: 2 },
                majorGridLines: { size: 1, color: 0xeeeeee }
            },
            yAxis: {
                color: 0x69aBc8,
                majorTicks: { color: 0x69aBc8, length: 4 },
                minorTicks: { color: 0x69aBc8, length: 2 },
                majorGridLines: { size: 1, color: 0xdfe8f6 }
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
        layout: "vbox",
        buttonAlign: "center",
        items: [chart, chart1, chart2],
        listeners: {
            "show": function () {
                //当window show事件发生时清空一下表单
                //store.load();
                chartStore.load();
            }
        }
    });

    win.show();
}

var OddsHistory = function (oddsArr) {
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
                return t.format("m-d H:i");
            }
        }]),
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
    win.show();

    Ext.Ajax.request({
        url: "Data/NowGoal/OddsHistory.aspx",
        params: { odds: oddsArr.join('^') },
        success: function (res) {
            changelist = [];

            grid.getStore().loadData(changelist);
        }
    });
}