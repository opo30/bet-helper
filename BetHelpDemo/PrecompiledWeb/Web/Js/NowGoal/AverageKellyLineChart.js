/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />

///公司凯利图形
var AverageKellyLineChart = function (scheduleID, action) {
    if (Ext.getCmp('avekellywin-' + scheduleID)) {
        return;
    }
    var fields = [{ name: 'time', dateFormat: 'Y-m-d H:i' },
            { name: 'avehome', type: 'float' },
            { name: 'avedraw', type: 'float' },
            { name: 'aveaway', type: 'float' },
            { name: 'varhome', type: 'float' },
            { name: 'vardraw', type: 'float' },
            { name: 'varaway', type: 'float' },
            { name: 'returnrate', type: 'float'}];

    var store = new Ext.data.Store({
        reader: new Ext.data.JsonReader(
           {
               fields: fields
           })
    });


    var chart = new Ext.chart.LineChart({
        id: "avekellychart-" + scheduleID,
        columnWidth: 1,
        store: store,
        height: 200,
        autoShow: true,
        series: [
                {
                    type: 'line',
                    displayName: '主胜',
                    xField: 'time',
                    yField: 'avehome',
                    style: {
                        color: 0xFF0000,
                        size: 5
                    }
                }, {
                    type: 'line',
                    displayName: '平局',
                    xField: 'time',
                    yField: 'avedraw',
                    style: {
                        color: 0x008000,
                        size: 5
                    }
                }, {
                    type: 'line',
                    displayName: '客胜',
                    xField: 'time',
                    yField: 'aveaway',
                    style: {
                        color: 0x0000FF,
                        size: 5
                    }
                }, {
                    type: 'line',
                    displayName: '返还率',
                    xField: 'time',
                    yField: 'returnrate',
                    style: {
                        color: 0xFFFF00,
                        size: 5
                    }
                }],
        tipRenderer: function (chart, record, index, series) {
            var s = "时间 " + record.data.time.format('m月d日 h时i分') + "\r\n";
            s += "主胜变化 " + Ext.util.Format.round(record.data.avehome, 2) + " ";
            s += "平局变化 " + Ext.util.Format.round(record.data.avedraw, 2) + " ";
            s += "客胜变化 " + Ext.util.Format.round(record.data.aveaway, 2) + "\r\n";
            s += "返还变化 " + record.data.returnrate;
            return s;
        },
        yAxis: new Ext.chart.NumericAxis(),
        xAxis: new Ext.chart.TimeAxis({
            labelRenderer: Ext.util.Format.dateRenderer('m-d H:i'),
            majorUnit: 1
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
        id: "varkellychart-" + scheduleID,
        columnWidth: 1,
        store: store,
        height: 200,
        autoShow: true,
        series: [
                {
                    type: 'line',
                    displayName: '主胜',
                    xField: 'time',
                    yField: 'varhome',
                    style: {
                        color: 0xFF0000,
                        size: 5
                    }
                }, {
                    type: 'line',
                    displayName: '平局',
                    xField: 'time',
                    yField: 'vardraw',
                    style: {
                        color: 0x008000,
                        size: 5
                    }
                }, {
                    type: 'line',
                    displayName: '客胜',
                    xField: 'time',
                    yField: 'varaway',
                    style: {
                        color: 0x0000FF,
                        size: 5
                    }
                }],
        tipRenderer: function (chart, record, index, series) {
            var s = "时间 " + record.data.time.format('m月d日 h时i分') + "\r\n";
            s += "主胜方差 " + Ext.util.Format.round(record.data.varhome, 2) + " ";
            s += "平局方差 " + Ext.util.Format.round(record.data.vardraw, 2) + " ";
            s += "客胜方差 " + Ext.util.Format.round(record.data.varaway, 2) + "\r\n";
            return s;
        },
        yAxis: new Ext.chart.NumericAxis(),
        xAxis: new Ext.chart.TimeAxis({
            labelRenderer: Ext.util.Format.dateRenderer('m-d H:i'),
            majorUnit: 1
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


var handleAction = function (action) {
    store.baseParams.time = action;
    store.reload();
};

var rowsData = [];
var gfields = [{ name: 'id', type: 'int' },
            { name: 'name', type: 'string' },
            { name: 'content', type: 'string' },
            { name: 'win', type: 'int' },
            { name: 'lost', type: 'int' },
            { name: 'resultwin', type: 'int' },
            { name: 'resultlost', type: 'int' },
            { name: 'home', type: 'int' },
            { name: 'draw', type: 'int' },
            { name: 'away', type: 'int'}];

var gstore = new Ext.data.Store({
    proxy: new Ext.data.HttpProxy(
           {
               url: "Data/NowGoal/ForecastScripts.aspx?a=list",
               method: "POST"
           }),
    reader: new Ext.data.JsonReader(
           {
               fields: gfields,
               root: "data",
               id: "id"
           }),
    listeners: {
        datachanged: function (s) {
            var scriptids = [], wins = [], victorys = [];
            for (var i = 0; i < s.getTotalCount(); i++) {
                var row = s.getAt(i);
                var resultArray = eval(row.get('content') + "(store)");
                row.data.home = resultArray[0];
                row.data.draw = resultArray[1];
                row.data.away = resultArray[2];
                rowsData.push(row.get('content') + "(" + resultArray[3] + ")");
            }
            Ext.Ajax.request({
                url: 'Data/NowGoal/BetExp.aspx?a=saveanalysis',
                params: { matchid: scheduleID, data: rowsData.join(',') },
                success: function (res) {
                    var result = Ext.decode(res.responseText);
                    if (result.success && result.data.length > 0) {
                        recordinfo.setText("记录" + result.data.length);
                        recordinfo.enable();
                        Ext.each(result.data, function (r) {
                            recordinfo.menu.add({
                                text: r.id,
                                //handler: showDetailWindow.createCallback(r.id, "比分", "showgoallist")
                                handler: AverageKellyLineChart.createCallback(r.id, action)
                            });
                        });
                    }
                    if (result.result) {
                        matchinfo.setText(result.result);
                    }
                }
            });
        }
    }
});
var matchinfo = new Ext.Toolbar.TextItem('加载中...');
//--------------------------------------------------列头
var cm = new Ext.grid.ColumnModel([
        new Ext.grid.RowNumberer(), {
            header: "预测法",
            dataIndex: "name",
            sortable: false,
            align: "middle",
            width: 100
        }, {
            header: "主",
            dataIndex: "home",
            width: 50,
            align: "middle",
            sortable: false,
            renderer: function (v, o, r) {
                if (v == Math.max(r.get('home'), r.get('draw'), r.get('away'))) {
                    return "<font color=red>" + v + "</font>";
                } else if (v == Math.min(r.get('home'), r.get('draw'), r.get('away'))) {
                    return "<font color=green>" + v + "</font>";
                }
                return v;
            }
        }, {
            header: "平",
            dataIndex: "draw",
            sortable: false,
            align: "middle",
            width: 50,
            renderer: function (v, o, r) {
                if (v == Math.max(r.get('home'), r.get('draw'), r.get('away'))) {
                    return "<font color=red>" + v + "</font>";
                } else if (v == Math.min(r.get('home'), r.get('draw'), r.get('away'))) {
                    return "<font color=green>" + v + "</font>";
                }
                return v;
            }
        }, {
            header: "客",
            dataIndex: "away",
            width: 50,
            align: "middle",
            sortable: false,
            renderer: function (v, o, r) {
                if (v == Math.max(r.get('home'), r.get('draw'), r.get('away'))) {
                    return "<font color=red>" + v + "</font>";
                } else if (v == Math.min(r.get('home'), r.get('draw'), r.get('away'))) {
                    return "<font color=green>" + v + "</font>";
                }
                return v;
            }
        }, {
            header: "赢盘",
            dataIndex: "win",
            sortable: false,
            align: "middle",
            width: 50
        }, {
            header: "输盘",
            dataIndex: "lost",
            sortable: false,
            align: "middle",
            width: 50
        }, {
            header: "赢盘率",
            dataIndex: "win",
            sortable: false,
            align: "middle",
            width: 50,
            renderer: function (v, i, r) {
                return (r.get('win') / (r.get('win') + r.get('lost')) * 100).toFixed(2) + "%";
            }
        }, {
            header: "胜负对",
            dataIndex: "resultwin",
            sortable: false,
            align: "middle",
            width: 50
        }, {
            header: "胜负错",
            dataIndex: "resultlost",
            sortable: false,
            align: "middle",
            width: 50
        }, {
            header: "胜负率",
            dataIndex: "resultwin",
            sortable: false,
            align: "middle",
            width: 50,
            renderer: function (v, i, r) {
                return (r.get('resultwin') / (r.get('resultwin') + r.get('resultlost')) * 100).toFixed(2) + "%";
            }
        }
]);

//----------------------------------------------------定义grid
var grid = new Ext.grid.GridPanel({
    store: gstore,
    cm: cm,
    sm: new Ext.grid.CheckboxSelectionModel({
        singleSelect: true
    }),
    columnWidth: 1,
    loadMask: true,
    stripeRows: true,
    height: 200,
    //超过长度带自动滚动条
    autoScroll: true,
    border: false,
    sortable: false,
    viewConfig: {
        forceFit: true, //自动填充
        sortAscText: '正序排列',
        sortDescText: '倒序排列',
        columnsText: '显示/隐藏列'
    }
});

var recordinfo = new Ext.Toolbar.Button({
    text: '无记录',
    disabled: true,
    menu: []
});

var tab = center.getItem('AverageKelly-' + scheduleID);
if (!tab) {
    var tab = center.add({
        id: 'AverageKelly-' + scheduleID,
        iconCls: "totalicon",
        xtype: "panel",
        title: "平均凯利",
        closable: true,
        layout: "column",
        buttonAlign: "center",
        tbar: [matchinfo, new Ext.Toolbar.Fill(), recordinfo, "-", {
            text: '相似',
            menu: [
            {
                text: '相似变化',
                handler: function () {
                    LoadSimilarExp("相似变化", rowsData.join(','), true);
                }
            }, {
                text: '相同临场',
                handler: function () {
                    LoadSimilarTrends("相同临场", store);
                }
            }, {
                text: '近似变化',
                handler: function () {
                    LoadSimilarExp("近似变化", rowsData.join(','), false);
                }
            }, {
                text: '临场变化',
                handler: function () {
                    var reqData = [];
                    Ext.each(rowsData, function (r) {
                        var anastr = r.split('(')[1].replace(')', '');
                        if (anastr.split(',').length > 3) {
                            reqData.push(r);
                        }
                    })
                    LoadSimilarExp("临场变化", reqData.join(','));
                }
            }, {
                text: '数据变化',
                handler: function () {
                    var reqData = [];
                    Ext.each(rowsData, function (r) {
                        var anastr = r.split('(')[1].replace(')', '');
                        if (anastr.split(',').length == 3) {
                            reqData.push(r);
                        }
                    })
                    LoadSimilarExp("数据变化", reqData.join(','));
                }
            }
        ]
        }, "-", {
            text: '赛果',
            handler: showDetailWindow.createCallback(scheduleID, "比分", "showgoallist")
        }, "-", {
            xtype: 'spinnerfield',
            fieldLabel: '时间',
            id: 'time-spinner-' + scheduleID,
            minValue: 0,
            maxValue: 100,
            value: 3,
            allowDecimals: true,
            decimalPrecision: 1,
            incrementValue: 0.5,
            alternateIncrementValue: 2.1,
            accelerate: true,
            width: 80
        }, {
            html: '&nbsp;&nbsp;&nbsp;小时',
            xtype: 'label'
        }, { text: '查询',
            handler: function () {
                updateLineChart(1);
            }
        }],
        items: [{
            xtype: 'panel',
            columnWidth: 1,
            split: true,
            height: 400,
            layout: 'column',
            items: [chart1, chart]
        }, grid],
        listeners: {
            "close": function () {
                chart.destroy();
                chart1.destroy();
            }
        }

    });
}
center.setActiveTab(tab);


//var win = new Ext.Window({
//    id: 'AverageKelly-' + scheduleID,
//    title: "平均凯利",
//    width: 1000,
//    height: 700,
//    plain: true,
//    iconCls: "total",
//    //不可以随意改变大小
//    resizable: true,
//    //是否可以拖动
//    draggable: true,
//    defaultType: "textfield",
//    labelWidth: 100,
//    collapsible: true, //允许缩放条
//    closeAction: 'close',
//    closable: true,
//    maximizable: true,
//    //弹出模态窗体
//    modal: false,
//    layout: "column",
//    buttonAlign: "center",
//    tbar: [matchinfo, new Ext.Toolbar.Fill(), recordinfo, "-", {
//        text: '相似',
//        menu: [
//            {
//                text: '相似变化',
//                handler: function () {
//                    LoadSimilarExp("相似变化", rowsData.join(','), true);
//                }
//            }, {
//                text: '相同临场',
//                handler: function () {
//                    LoadSimilarTrends("相同临场", store);
//                }
//            }, {
//                text: '近似变化',
//                handler: function () {
//                    LoadSimilarExp("近似变化", rowsData.join(','), false);
//                }
//            }, {
//                text: '临场变化',
//                handler: function () {
//                    var reqData = [];
//                    Ext.each(rowsData, function (r) {
//                        var anastr = r.split('(')[1].replace(')', '');
//                        if (anastr.split(',').length > 3) {
//                            reqData.push(r);
//                        }
//                    })
//                    LoadSimilarExp("临场变化", reqData.join(','));
//                }
//            }, {
//                text: '数据变化',
//                handler: function () {
//                    var reqData = [];
//                    Ext.each(rowsData, function (r) {
//                        var anastr = r.split('(')[1].replace(')', '');
//                        if (anastr.split(',').length == 3) {
//                            reqData.push(r);
//                        }
//                    })
//                    LoadSimilarExp("数据变化", reqData.join(','));
//                }
//            }
//        ]
//    }, "-", {
//        text: '赛果',
//        handler: showDetailWindow.createCallback(scheduleID, "比分", "showgoallist")
//    }, "-", {
//        xtype: 'spinnerfield',
//        fieldLabel: '时间',
//        id: 'time-spinner-' + scheduleID,
//        minValue: 0,
//        maxValue: 100,
//        value: 3,
//        allowDecimals: true,
//        decimalPrecision: 1,
//        incrementValue: 0.5,
//        alternateIncrementValue: 2.1,
//        accelerate: true,
//        width: 80
//    }, {
//        html: '&nbsp;&nbsp;&nbsp;小时',
//        xtype: 'label'
//    }, { text: '查询',
//        handler: function () {
//            updateLineChart(1);
//        }
//    }],
//    items: [{
//        xtype: 'panel',
//        columnWidth: 1,
//        split: true,
//        height: 400,
//        layout: 'column',
//        items: [chart1, chart]
//    }, grid],
//    listeners: {
//        "close": function () {
//            chart.destroy();
//            chart1.destroy();
//        }
//    }
//});
//win.show();
var loadMask = new Ext.LoadMask(tab.getEl(), {
    msg: '正在读取数据，请稍候...',
    removeMask: true
});

var updateLineChart = function (isRefresh) {
    loadMask.show();
    var hourValue = Ext.getCmp('time-spinner-' + scheduleID).getValue();
    Ext.Ajax.request({
        url: "Data/NowGoal/GetChartsData.aspx",
        method: "POST",
        timeout: 300000,
        params: {
            scheduleID: scheduleID, action: action, time: hourValue * 3600, isrefresh: isRefresh
        },
        success: function (res, req) {
            store.loadData(Ext.decode(res.responseText));
            gstore.load();
            loadMask.hide();
        },
        failure: function () {
            loadMask.hide();
        }
    });
}
updateLineChart(0);

}



function ycjgFunction(store) {
    var suphome = 0, supdraw = 0, supaway = 0;
    var supvarhome = 0, supvardraw = 0, supvaraway = 0;
    var supvarhome1 = 0, supvardraw1 = 0, supvaraway1 = 0;
    var supvarhome2 = 0, supvardraw2 = 0, supvaraway2 = 0;
    Ext.each(store.data.items, function (v, i, a) {
        if (i > 0) {
            var minvalue = Math.min(v.get('avehome'), v.get('avedraw'), v.get('aveaway'));
            if (minvalue < v.get('returnrate')) {
                switch (minvalue) {
                    case v.get('avehome'):
                        suphome++;
                        break;
                    case v.get('avedraw'):
                        supdraw++;
                        break;
                    case v.get('aveaway'):
                        supaway++;
                        break;
                    default:
                        break;
                }
            }
        }
    });
    var end = store.getAt(store.getTotalCount() - 1);
    for (var i = 0; i < store.getTotalCount(); i++) {
        var start = store.getAt(i);
        if (start.get("returnrate") > end.get("returnrate")) {//不看好
            if (start.get("varhome") > end.get("varhome")) {
                supvarhome++;
            } else if (start.get("varhome") < end.get("varhome")) {
                supvarhome--;
            }
            if (start.get("vardraw") > end.get("vardraw")) {
                supvardraw++;
            } else if (start.get("vardraw") < end.get("vardraw")) {
                supvardraw--;
            }
            if (start.get("varaway") > end.get("varaway")) {
                supvaraway++;
            } else if (start.get("varaway") < end.get("varaway")) {
                supvaraway--;
            }
        } else if (start.get("returnrate") < end.get("returnrate")) {//看好
            if (start.get("varhome") > end.get("varhome")) {
                supvarhome--;
            } else if (start.get("varhome") < end.get("varhome")) {
                supvarhome++;
            }
            if (start.get("vardraw") > end.get("vardraw")) {
                supvardraw--;
            } else if (start.get("vardraw") < end.get("vardraw")) {
                supvardraw++;
            }
            if (start.get("varaway") > end.get("varaway")) {
                supvaraway--;
            } else if (start.get("varaway") < end.get("varaway")) {
                supvaraway++;
            }
        }
        var source = store.getAt(0);
        if (start.get("returnrate") > 0) {//看好
            if (start.get("varhome") > source.get("varhome")) {
                supvarhome1++;
            } else if (start.get("varhome") < source.get("varhome")) {
                supvarhome1--;
            }
            if (start.get("vardraw") > source.get("vardraw")) {
                supvardraw1++;
            } else if (start.get("vardraw") < source.get("vardraw")) {
                supvardraw1--;
            }
            if (start.get("varaway") > end.get("varaway")) {
                supvaraway1++;
            } else if (start.get("varaway") < source.get("varaway")) {
                supvaraway1--;
            }
        } else if (start.get("returnrate") < 0) {//不看好
            if (start.get("varhome") > source.get("varhome")) {
                supvarhome1--;
            } else if (start.get("varhome") < source.get("varhome")) {
                supvarhome1++;
            }
            if (start.get("vardraw") > source.get("vardraw")) {
                supvardraw1--;
            } else if (start.get("vardraw") < source.get("vardraw")) {
                supvardraw1++;
            }
            if (start.get("varaway") > source.get("varaway")) {
                supvaraway1--;
            } else if (start.get("varaway") < source.get("varaway")) {
                supvaraway1++;
            }
        }
        var next = store.getAt(i + 1);
        if (!next) {
            continue;
        }
        if (start.get("returnrate") > next.get("returnrate")) {//不看好
            if (start.get("varhome") > next.get("varhome")) {
                supvarhome2++;
            } else if (start.get("varhome") < next.get("varhome")) {
                supvarhome2--;
            }
            if (start.get("vardraw") > next.get("vardraw")) {
                supvardraw2++;
            } else if (start.get("vardraw") < next.get("vardraw")) {
                supvardraw2--;
            }
            if (start.get("varaway") > next.get("varaway")) {
                supvaraway2++;
            } else if (start.get("varaway") < next.get("varaway")) {
                supvaraway2--;
            }
        } else if (start.get("returnrate") < next.get("returnrate")) {//看好
            if (start.get("varhome") > next.get("varhome")) {
                supvarhome2--;
            } else if (start.get("varhome") < next.get("varhome")) {
                supvarhome2++;
            }
            if (start.get("vardraw") > next.get("vardraw")) {
                supvardraw2--;
            } else if (start.get("vardraw") < next.get("vardraw")) {
                supvardraw2++;
            }
            if (start.get("varaway") > next.get("varaway")) {
                supvaraway2--;
            } else if (start.get("varaway") < next.get("varaway")) {
                supvaraway2++;
            }
        }
    }
    Ext.Msg.alert("提示", "平均凯利 主：" + suphome + " 平：" + supdraw + " 客：" + supaway + "<br/><br/>" + "方差临场 主：" + supvarhome + " 平：" + supvardraw + " 客：" + supvaraway  + "<br/>" + "方差初盘 主：" + supvarhome1 + " 平：" + supvardraw1 + " 客：" + supvaraway1 + "<br/>" + "方差局部 主：" + supvarhome2 + " 平：" + supvardraw2 + " 客：" + supvaraway2);
}


function ycjgFunction(dataStore) {
    var yucejieguo = [];
    var fields = [{ name: 'id', type: 'int' },
            { name: 'name', type: 'string' },
            { name: 'content', type: 'string' },
            { name: 'win', type: 'int' },
            { name: 'lost', type: 'int' },
            { name: 'resultwin', type: 'int' },
            { name: 'resultlost', type: 'int' },
            { name: 'home', type: 'int' },
            { name: 'draw', type: 'int' },
            { name: 'away', type: 'int'}];

    var store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy(
           {
               url: "Data/NowGoal/ForecastScripts.aspx?a=list",
               method: "POST"
           }),
        reader: new Ext.data.JsonReader(
           {
               fields: fields,
               root: "data",
               id: "id"
           }),
        listeners: {
            datachanged: function (s) {
                var scriptids = [], wins = [], victorys = [];
                for (var i = 0; i < s.getTotalCount(); i++) {
                    var row = s.getAt(i);
                    var resultArray = eval(row.get('content') + "(dataStore)");
                    row.data.home = resultArray[0];
                    row.data.draw = resultArray[1];
                    row.data.away = resultArray[2];
                }
            }
        }
    });

    //--------------------------------------------------列头
    var cm = new Ext.grid.ColumnModel([
        new Ext.grid.RowNumberer(), {
            header: "预测法",
            dataIndex: "name",
            sortable: false,
            align: "middle",
            width: 100
        }, {
            header: "主",
            dataIndex: "home",
            width: 50,
            align: "middle",
            sortable: false
        }, {
            header: "平",
            dataIndex: "draw",
            sortable: false,
            align: "middle",
            width: 50
        }, {
            header: "客",
            dataIndex: "away",
            width: 50,
            align: "middle",
            sortable: false
        }, {
            header: "赢盘",
            dataIndex: "win",
            sortable: false,
            align: "middle",
            width: 50
        }, {
            header: "输盘",
            dataIndex: "lost",
            sortable: false,
            align: "middle",
            width: 50
        }, {
            header: "赢盘率",
            dataIndex: "win",
            sortable: false,
            align: "middle",
            width: 50,
            renderer: function (v, i, r) {
                return (r.get('win') / (r.get('win') + r.get('lost')) * 100).toFixed(2) + "%";
            }
        }, {
            header: "胜负对",
            dataIndex: "resultwin",
            sortable: false,
            align: "middle",
            width: 50
        }, {
            header: "胜负错",
            dataIndex: "resultlost",
            sortable: false,
            align: "middle",
            width: 50
        }, {
            header: "胜负率",
            dataIndex: "resultwin",
            sortable: false,
            align: "middle",
            width: 50,
            renderer: function (v, i, r) {
                return (r.get('resultwin') / (r.get('resultwin') + r.get('resultlost')) * 100).toFixed(2) + "%";
            }
        }
]);

    //----------------------------------------------------定义grid
    var grid = new Ext.grid.GridPanel({
        store: store,
        cm: cm,
        sm: new Ext.grid.CheckboxSelectionModel({
            singleSelect: true
        }),
        columnWidth: 1,
        loadMask: true,
        stripeRows: true,
        height: 250,
        //超过长度带自动滚动条
        autoScroll: true,
        border: false,
        sortable: false,
        viewConfig: {
            forceFit: true, //自动填充
            sortAscText: '正序排列',
            sortDescText: '倒序排列',
            columnsText: '显示/隐藏列'
        }
    });
    var win = new Ext.Window({
        width: 800,
        height: 480,
        layout: 'fit',
        items: [grid]
    })
    win.show();
    store.load();

}