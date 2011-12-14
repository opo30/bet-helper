/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />

var Odds1x2CompanyCharts = function(scheduleID, companyids) {
    var yAxisMin = 0;
    var yAxisMax = 0;
    var fields = [{ name: 'time' },
            { name: 'home', type: 'int' },
            { name: 'draw', type: 'int' },
            { name: 'away', type: 'int' },
            { name: 'nhome', type: 'int' },
            { name: 'ndraw', type: 'int' },
            { name: 'naway', type: 'int' },
            { name: 'count', type: 'int'}];

    var store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy(
           {
               url: "Data/NowGoal/GetChartsData.aspx",
               method: "POST",
               timeout: 3600000
           }),
        reader: new Ext.data.JsonReader(
           {
               fields: fields,
               root: "data",
               id: "time",
               totalProperty: "totalCount"
           }),
        baseParams: {
            scheduleID: scheduleID, companyids: companyids.join(","), time: 24, action: "company"
        }
    });

    store.on("beforeload", function(store) {
    });

    store.on("datachanged", function(store) {
    });

    var chart = new Ext.chart.LineChart({
        id: "companychart",
        store: store,
        autoShow: true,
        series: [
                {
                    type: 'line',
                    displayName: '主胜',
                    xField: 'time',
                    yField: 'home',
                    style: {
                        color: 0xFF0000,
                        size: 5
                    }
                }, {
                    type: 'line',
                    displayName: '平局',
                    xField: 'time',
                    yField: 'draw',
                    style: {
                        color: 0x008000,
                        size: 5
                    }
                }, {
                    type: 'line',
                    displayName: '客胜',
                    xField: 'time',
                    yField: 'away',
                    style: {
                        color: 0x0000FF,
                        size: 5
                    }
                }, {
                    type: 'line',
                    displayName: '主胜',
                    xField: 'time',
                    yField: 'nhome',
                    style: {
                        color: 0xFF0000,
                        size: 5
                    }
                }, {
                    type: 'line',
                    displayName: '平局',
                    xField: 'time',
                    yField: 'ndraw',
                    style: {
                        color: 0x008000,
                        size: 5
                    }
                }, {
                    type: 'line',
                    displayName: '客胜',
                    xField: 'time',
                    yField: 'naway',
                    style: {
                        color: 0x0000FF,
                        size: 5
                    }
                }, {
                    type: 'line',
                    displayName: '总数',
                    xField: 'time',
                    yField: 'count',
                    style: {
                        color: 0xFFFF00,
                        size: 5
                    }
}],
        tipRenderer: function(chart, record, index, series) {
            return "公司总数 " + record.data.count + "\r\n主" + record.data.home + " 平" + record.data.draw + " 客" + record.data.away + "\r\n时间 " + record.data.time;
        },
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


            }
        }
    });


    var handleAction = function(action) {
        store.baseParams.time = action;
        store.reload();
    };



    var win = new Ext.Window({
        id: 'companychartwin',
        title: "公司支持",
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
        modal:false,
        layout: "fit",
        buttonAlign: "center",
        items: [chart],
        tbar: [new Ext.Toolbar.Fill(), {
            text: "时段选择",
            menu: [{
                text: "3小时",
                handler: handleAction.createCallback(3)
            }, {
                text: "6小时",
                handler: handleAction.createCallback(6)
            }, {
                text: "9小时",
                handler: handleAction.createCallback(9)
            }, {
                text: "12小时",
                handler: handleAction.createCallback(12)
            }, {
                text: "24小时",
                handler: handleAction.createCallback(24)
            }, {
                text: "完整",
                handler: handleAction.createCallback(0)
}]
}],
                listeners: {
                    "show": function() {
                        
                    },
                    "close": function() {
                        chart.destroy();
                    }
                }
            });

            win.show();
            var loadMarsk = new Ext.LoadMask(win.getEl(), {
                msg: '正在读取数据，请稍候...',
                removeMask: true,
                store: store
            });
            store.load();
        }

        


                    var Odds1x2PointCharts = function(scheduleID, companyids) {
                        var fields = [
                                    { name: 'name' },
                                    { name: 'point', type: 'int' },
                                    { name: 'xpoint', type: 'int'}];

                        var store = new Ext.data.Store({
                            proxy: new Ext.data.HttpProxy(
                           {
                               url: "Data/NowGoal/GetChartsData.aspx",
                               method: "POST",
                               timeout : 3600000 
                           }),
                            reader: new Ext.data.JsonReader(
                           {
                               fields: fields,
                               root: "data",
                               id: "name",
                               totalProperty: "totalCount"
                           }),
                            baseParams: {
                                scheduleID: scheduleID, companyids: companyids.join(","), action: "point"
                            }
                        });

                        store.on("beforeload", function(store) {
                        });

                        store.on("datachanged", function(store) {
                        });

                        var chart = new Ext.chart.ColumnChart({
                            id: "mychart",
                            store: store,
                            autoShow: true,
                            xField: 'name',
//                            tipRenderer: function(chart, record, index, series) {
//                                return "主" + record.data.home + " 平" + record.data.draw + " 客" + record.data.away;
//                            },
                            series: [
                            {
                                type: 'column',
                                displayName: '支持',
                                yField: 'point',
                                style: {
                                    size: 15
                                }
                            }, {
                                type: 'column',
                                displayName: '不支持',
                                yField: 'xpoint',
                                style: {
                                    size: 10
                                }
                            }]
                        });




                        var win = new Ext.Window({
                            title: "赔率变化",
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
                            modal: 'true',
                            layout: "fit",
                            buttonAlign: "center",
                            items: [chart],
                                    listeners: {
                                        "show": function() {
                                        },
                                        "close": function() {
                                            chart.destroy();
                                        }
                                    }
                                });

                                win.show();
                                var loadMarsk = new Ext.LoadMask(win.getEl(), {
                                    msg: '正在读取数据，请稍候...',
                                    removeMask: true,
                                    store: store
                                });
                                store.load();
                    }