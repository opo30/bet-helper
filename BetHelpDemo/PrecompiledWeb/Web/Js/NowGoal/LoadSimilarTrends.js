/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />

var LoadSimilarTrends = function (title, sourceStore) {
    var defaultSimilar = 5;
    var sourceCount = sourceStore.getCount();
    var arr = new Array();
    for (var i = 0; i < defaultSimilar; i++) {
        arr.push(sourceStore.getAt(sourceCount - (defaultSimilar - i)).data);
    }
    //指定列参数
    var fields = [{ name: 'scheduleid', type: 'string' },
    { name: 'hometeam', type: 'string' },
    { name: 'fullscore', type: 'string' },
    { name: 'awayteam', type: 'string' },
    { name: 'halfscore', type: 'string' },
    { name: 'victory', type: 'float' },
    { name: 'asia', type: 'float' },
    { name: 'asiawin', type: 'float' },
    { name: 'chartdata', type: 'string' }
    ];

    var store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy(
           {
               url: "Data/NowGoal/BetExp.aspx?a=similartrends",
               method: "POST"
           }),
        reader: new Ext.data.JsonReader(
           {
               fields: fields,
               root: "data",
               id: "scheduleid"
           }),
        baseParams: {
            trends: Ext.encode(arr), match: "avekelly"
        }
    });

    //--------------------------------------------------列头
    var cm = new Ext.grid.ColumnModel([
    new Ext.grid.RowNumberer(),
		{
		    header: "编号",
		    dataIndex: "scheduleid",
		    sortable: true,
		    align: "middle",
		    width: 50
		}, {
		    header: "主队",
		    dataIndex: "hometeam",
		    sortable: true,
		    align: "middle",
		    width: 50
		}, {
		    header: "完场比分",
		    dataIndex: "fullscore",
		    sortable: true,
		    align: "middle",
		    width: 50
		}, {
		    header: "客队",
		    dataIndex: "awayteam",
		    sortable: true,
		    align: "middle",
		    width: 50
		}, {
		    header: "半场比分",
		    dataIndex: "halfscore",
		    sortable: true,
		    align: "middle",
		    width: 50
		}, {
		    header: "胜负",
		    dataIndex: "victory",
		    sortable: true,
		    align: "middle",
		    width: 50,
		    renderer: function (value) {
		        if (value > 0) {
		            return "<font color=red>胜</font>"
		        } else if (value == 0) {
		            return "<font color=green>平</font>"
		        } else {
		            return "<font color=blue>负</font>"
		        }
		    }
		}, {
		    header: "亚盘",
		    dataIndex: "asia",
		    sortable: true,
		    align: "middle",
		    width: 50
		}, {
		    header: "盘路",
		    dataIndex: "asiawin",
		    sortable: true,
		    align: "middle",
		    width: 50,
		    renderer: function (value) {
		        if (value > 0) {
		            return "<font color=red>赢盘</font>"
		        } else if (value == 0) {
		            return "<font color=green>走盘</font>"
		        } else {
		            return "<font color=blue>输盘</font>"
		        }
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
        height: 300,
        width: 400,
        //超过长度带自动滚动条
        autoScroll: true,
        border: false,
        sortable: false,
        emptyMsg: '没有记录',
        tbar: [new Ext.Toolbar.Fill()],
        viewConfig: {
            //自动填充
            emptyText: '没有记录',
            forceFit: true,
            getRowClass: function (record, rowIndex, rowParams, store) {
            }
        },
        listeners: {
            rowclick: function (o, i, e) {
                chartStore.loadData(Ext.decode(store.getAt(i).json.chartdata));
            }
        }
    });

    var tBarHandleAction = function (v) {
        var arr = new Array();
        for (var i = 0; i < v; i++) {
            arr.push(sourceStore.getAt(sourceStore.getCount() - (v - i)).data);
        }
        store.baseParams.trends = Ext.encode(arr);
        store.reload();
    }

    for (var i = 0; i < sourceCount; i++) {
        var value = sourceCount - i;
        grid.topToolbar.addButton({
            text: value,
            handler: tBarHandleAction.createCallback(value)
        });
    }


    var chartStore = new Ext.data.Store({
        reader: new Ext.data.JsonReader(
           {
               fields: [{ name: 'time', dateFormat: 'Y-m-d H:i' },
            { name: 'avehome', type: 'float' },
            { name: 'avedraw', type: 'float' },
            { name: 'aveaway', type: 'float' },
            { name: 'varhome', type: 'float' },
            { name: 'vardraw', type: 'float' },
            { name: 'varaway', type: 'float' },
            { name: 'returnrate', type: 'float'}]
           })
    });


    var chart = new Ext.chart.LineChart({
        store: chartStore,
        height: 150,
        autoShow: true,
        columnWidth: 1,
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
        height: 150,
        columnWidth: 1,
        store: chartStore,
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

var win = new Ext.Window({
    title: title,
    closable: true,
    layout: "column",
    items: [{
        xtype: 'panel',
        columnWidth: 1,
        split: true,
        height: 300,
        layout: 'column',
        items: [chart1, chart]
    }, grid],
    width: 720
});

win.show();
store.load();
}
