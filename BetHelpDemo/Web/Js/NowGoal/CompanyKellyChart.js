/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />

///公司凯利图形
var KellyChangeCharts = function(scheduleID, companyids, companynames, action) {
    var yAxisMin = 70;
    var yAxisMax = 110;
    var fields = [{ name: 'time', dateFormat: 'Y-m-d H:i' },
            { name: 'homek', type: 'float' },
            { name: 'drawk', type: 'float' },
            { name: 'awayk', type: 'float' },
            { name: 'returnrate', type: 'float'}];

    var store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy(new Ext.data.Connection(
           {
               url: "Data/NowGoal/GetChartsData.aspx",
               method: "POST",
               timeout: 300000
           })),
        reader: new Ext.data.JsonReader(
           {
               fields: fields,
               root: "data",
               id: "time",
               totalProperty: "total"
           }),
        baseParams: {
            scheduleID: scheduleID, companyids: companyids.join(","), action: action
        },
        listeners: {
            datachanged: function(p1, p2) {
                var numArr = new Array();
                for (var i = 0; i < p1.data.items.length; i++) {
                    numArr.push(p1.getAt(i).get('homek'));
                    numArr.push(p1.getAt(i).get('drawk'));
                    numArr.push(p1.getAt(i).get('awayk'));
                }
                chart.yAxis.minimum = eval('Math.min(' + numArr.join(',') + ')') - 1;
                chart.yAxis.maximum = eval('Math.max(' + numArr.join(',') + ')') + 1;
                chart.setYAxis(chart.yAxis);

                var lasttime = p1.getAt(0).data.time;
                var firsttime = p1.getAt(p1.getTotalCount() - 1).data.time;
                var starttime = new Date(lasttime - (24 * 1000 * 3600));
                chart.xAxis.minimum = (starttime <= firsttime ? firsttime : starttime);
                chart.setXAxis(chart.xAxis);

                chart.setStyles(Ext.apply({}, chart.extraStyle, chart.chartStyle));
                chart.render();
            }
        }
    });

    var chart = new Ext.chart.LineChart({
        id: "kellychart-" + companyids,
        store: store,
        autoShow: true,
        series: [
                {
                    type: 'line',
                    displayName: '主胜',
                    xField: 'time',
                    yField: 'homek',
                    style: {
                        color: 0xFF0000,
                        size: 5
                    }
                }, {
                    type: 'line',
                    displayName: '平局',
                    xField: 'time',
                    yField: 'drawk',
                    style: {
                        color: 0x008000,
                        size: 5
                    }
                }, {
                    type: 'line',
                    displayName: '客胜',
                    xField: 'time',
                    yField: 'awayk',
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
        tipRenderer: function(chart, record, index, series) {
            var s = "时间 " + record.data.time.format('m月d日 h时i分') + "\r\n";
            s += "主胜凯利 " + Ext.util.Format.round(record.data.homek, 2) + " ";
            s += "平局凯利 " + Ext.util.Format.round(record.data.drawk, 2) + " ";
            s += "客胜凯利 " + Ext.util.Format.round(record.data.awayk, 2) + "\r\n";
            s += "返还率 " + record.data.returnrate;
            return s;
        },
        yAxis: new Ext.chart.NumericAxis({
            labelRenderer: Ext.util.Format.numberRenderer('0,0'),
            title: '凯利'
        }),
        xAxis: new Ext.chart.TimeAxis({
            labelRenderer: Ext.util.Format.dateRenderer('m-d H:i'),
            title: '时间',
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
            itemclick: function(o) {
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


    var handleAction = function(action) {
        store.baseParams.time = action;
        store.reload();
    };

    var tabkey = "kelly-panel-" + companyids;
    if (Ext.getCmp('kellywin') == undefined) {
        var win = new Ext.Window({
            id: 'kellywin',
            title: "凯利变化",
            width: 1000,
            height: 600,
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
            items: [{ xtype: 'tabpanel', id: 'kelly-tab', activeItem: 0, items: [{
                id: tabkey,
                iconCls: "totalicon",
                xtype: "panel",
                title: companynames.join(','),
                closable: true,
                layout: "fit",
                items: [chart],
                tbar: [new Ext.Toolbar.Fill(), {
                    xtype: 'spinnerfield',
                    fieldLabel: '时间',
                    id: 'time-spinner-' + companyids,
                    minValue: 0,
                    maxValue: 100,
                    value: 24,
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
                    handler: function() {
                        var hourValue = Ext.getCmp('time-spinner-' + companyids).getValue();
                        var count = Ext.getCmp("kellychart-" + companyids).store.getTotalCount();
                        var lasttime = Ext.getCmp("kellychart-" + companyids).store.getAt(0).data.time;
                        var firsttime = Ext.getCmp("kellychart-" + companyids).store.getAt(count - 1).data.time;
                        var starttime = new Date(lasttime - (hourValue * 1000 * 3600));
                        chart.xAxis.minimum = (starttime <= firsttime ? firsttime : starttime);
                        chart.setXAxis(chart.xAxis);

                        chart.setStyles(Ext.apply({}, chart.extraStyle, chart.chartStyle));
                        chart.render();
                    } }]

}]}],

                listeners: {
                    "close": function() {
                        chart.destroy();
                    }
                }
            });
            win.show();
        }
        else {
            if (!Ext.getCmp('kelly-tab').items.containsKey(tabkey)) {
                var tab = Ext.getCmp('kelly-tab').add({
                    id: tabkey,
                    iconCls: "totalicon",
                    xtype: "panel",
                    title: companynames.join(','),
                    closable: true,
                    layout: "fit",
                    items: [chart],
                    tbar: [{ text: '重载',
                        handler: function() {
                            store.reload();
                        } 
                    }, new Ext.Toolbar.Fill(), {
                        xtype: 'spinnerfield',
                        fieldLabel: '时间',
                        id: 'time-spinner-' + companyids,
                        minValue: 0,
                        maxValue: 100,
                        value: 24,
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
                        handler: function() {
                            var hourValue = Ext.getCmp('time-spinner-' + companyids).getValue();
                            var count = Ext.getCmp("kellychart-" + companyids).store.getTotalCount();
                            var lasttime = Ext.getCmp("kellychart-" + companyids).store.getAt(0).data.time;
                            var firsttime = Ext.getCmp("kellychart-" + companyids).store.getAt(count - 1).data.time;
                            var starttime = new Date(lasttime - (hourValue * 1000 * 3600));
                            chart.xAxis.minimum = (starttime <= firsttime ? firsttime : starttime);
                            chart.setXAxis(chart.xAxis);

                            chart.setStyles(Ext.apply({}, chart.extraStyle, chart.chartStyle));
                            chart.render();
                        } }]
                    });
                }
                Ext.getCmp('kelly-tab').setActiveTab(tab);
            }





            var loadMarsk = new Ext.LoadMask(Ext.getCmp('kellywin').getEl(), {
                msg: '正在读取数据，请稍候...',
                removeMask: true,
                store: store
            });
            store.load();

        }
