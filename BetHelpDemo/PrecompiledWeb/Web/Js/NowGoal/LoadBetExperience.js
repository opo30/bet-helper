/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />

var LoadBetExperience = function (node) {
    var pageSize = 10;
    //指定列参数
    var fields = [{ name: 'id', type: 'string' },
            { name: 'hometeam', type: 'string' },
            { name: 'awayteam', type: 'string' },
            { name: 'homescore', type: 'int' },
            { name: 'awayscore', type: 'int' },
            { name: 'victory', type: 'int' },
            { name: 'win', type: 'int' },
            { name: 'asia', type: 'float' },
            { name: 'exp', type: 'string' },
            { name: 'hasstatistics', type: 'bool' }];

    var store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy(
           {
               url: "Data/NowGoal/BetExp.aspx?a=list",
               method: "POST"
           }),
        reader: new Ext.data.JsonReader(
           {
               fields: fields,
               root: "data",
               id: "id",
               totalProperty: "totalCount"
           }),
        baseParams: {
            start: 0, limit: pageSize, where: 'isexp=1'
        }
    });

    //--------------------------------------------------列头
    var cm = new Ext.grid.ColumnModel([
		{
		    header: "主队",
		    dataIndex: "hometeam",
		    tooltip: "主队名称",
		    sortable: false,
		    align: "middle",
		    width: 50
		}, {
		    header: "主队入球",
		    dataIndex: "homescore",
		    width: 20,
		    sortable: false
		}, {
		    header: "亚洲盘",
		    tooltip: "亚洲让球盘",
		    dataIndex: "asia",
		    sortable: false,
		    width: 20
		}, {
		    header: "客队入球",
		    dataIndex: "awayscore",
		    width: 20,
		    sortable: false
		}, {
		    header: "客队",
		    tooltip: "客队名称",
		    dataIndex: "awayteam",
		    sortable: false,
		    width: 50
		}, {
		    header: "胜负",
		    tooltip: "比赛胜负",
		    dataIndex: "victory",
		    sortable: false,
		    width: 20,
		    renderer: function (v) {
		        if (v == 3) {
		            return "<font color='red'>胜</font>"
		        }
		        else if (v == 1) {
		            return "<font color='green'>平</font>"
		        }
		        else {
		            return "<font color='blue'>付</font>"
		        }
		    }
		}, {
		    header: "盘路",
		    tooltip: "比赛胜负",
		    dataIndex: "win",
		    sortable: false,
		    width: 20,
		    renderer: function (v) {
		        if (v == 3) {
		            return "<font color='red'>赢</font>"
		        }
		        else if (v == 1) {
		            return "<font color='green'>走</font>"
		        }
		        else {
		            return "<font color='blue'>输</font>"
		        }
		    }
		},{
		    header: "统计",
		    tooltip: "已统计",
		    dataIndex: "hasstatistics",
		    sortable: false,
		    width: 20,
		    renderer: function (v) {
		        if (v == 1) {
		            return "<font color='red'>ok</font>"
		        }
		    }
		}, {
		    header: "经验",
		    tooltip: "经验总结",
		    dataIndex: "exp",
		    sortable: false,
		    width: 100,
		    renderer: function (v) {
		        return "<font ext:qtip='" + v + "'>" + v + "</font>";
		    }
		}
]);

    //----------------------------------------------------定义grid
    var grid = new Ext.grid.GridPanel({
        store: store,
        cm: cm,
        sm:new Ext.grid.CheckboxSelectionModel({ 
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
            //自动填充
            forceFit: true,
            sortAscText: '正序排列',
            sortDescText: '倒序排列',
            columnsText: '显示/隐藏列',
            getRowClass: function (record, rowIndex, rowParams, store) {
            }
        },
        //分页
        bbar: new Ext.PagingToolbar({
            store: store,
            pageSize: pageSize,
            //显示右下角信息
            displayInfo: true,
            displayMsg: '当前记录 {0} -- {1} 条 共 {2} 条记录',
            emptyMsg: "没有记录",
            prevText: "上一页",
            nextText: "下一页",
            refreshText: "刷新",
            lastText: "最后页",
            firstText: "第一页",
            beforePageText: "当前页",
            afterPageText: "共{0}页"

        }),
        listeners: {
            rowclick: function (o, i, e) {
                chartStore.loadData(Ext.decode(store.getAt(i).json.data));
            }
        }
    });


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
        height: 200,
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
        height: 200,
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

var tab = center.getItem("BetExperienceTab");
if (!tab) {
    var tab = center.add({
        id: "BetExperienceTab",
        iconCls: "totalicon",
        xtype: "panel",
        title: node.text,
        closable: true,
        layout: "column",
        items: [{
            xtype: 'panel',
            columnWidth: 1,
            split: true,
            height: 400,
            layout: 'column',
            items: [chart1, chart]
        }, grid],
        tbar: [{
            text: '总结',
            iconCls:'experience',
            handler: function () {
                var rows = grid.getSelectionModel().getSelections();
                if (rows.length == 0) {
                    Ext.Msg.alert("提示信息", "您没有选中任何行!");
                }
                else {
                    var win = new Ext.Window({
                        layout: 'fit',
                        height: 240,
                        width: 640,
                        items: [{
                            xtype: 'htmleditor'
                        }],
                        buttons: [{
                            text: '保存',
                            handler: function () {
                                Ext.Ajax.request({
                                    url: "Data/NowGoal/BetExp.aspx?a=saveexp",
                                    method: 'POST',
                                    waitMsg: '正在提交...',
                                    params: { matchid: rows[0].data.id, content: Ext.util.Format.htmlEncode(win.findByType('htmleditor')[0].getValue()) },
                                    success: function (req, res) {
                                        var result = Ext.decode(req.responseText);
                                        if (!result.success) {
                                            Ext.Msg.alert("提 示", result.error);
                                        }
                                        else {
                                            store.reload();
                                            win.destroy();
                                        }
                                    },
                                    failure: function (res, req) {

                                    }
                                });
                            }
                        }],
                        listeners:{
                            show:function(){
                                win.findByType('htmleditor')[0].setValue(rows[0].data.exp)
                            }
                        }
                    });
                    content: Ext.util.Format.htmlEncode(win.findByType('htmleditor')[0].getValue())
                    win.show();
                }
                
            }
        },"-",{
            text: '分析',
            iconCls:'totalicon',
            handler: showForecastResult.createCallback()
        }]
    });
}
center.setActiveTab(tab);
store.load();
}


function showForecastResult(res,req)
{
var rows = Ext.getCmp('BetExperienceTab').findByType("grid")[0].getSelectionModel().getSelections();
Ext.Ajax.request({
                url: 'Data/NowGoal/GetPrediction.aspx',
                params: { scheduleID: rows[0].get('id') },
                success: function (res) {
                    Ext.Msg.alert("埃罗预测法", res.responseText);
                }
            });
    var dataStore = Ext.getCmp('BetExperienceTab').findByType("linechart")[0].store;
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
            { name: 'away', type: 'int' }];

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
           listeners:{
                datachanged:function(s){
                    var berow = Ext.getCmp('BetExperienceTab').findByType("grid")[0].getSelectionModel().getSelections()[0];
                    var scriptids =[], wins = [], victorys = [];
                    for (var i = 0; i < s.getTotalCount(); i++) {
                        var row = s.getAt(i);
                        eval(row.get('content'));
                        var resultArray = forecastFunction(dataStore);
                        row.data.home =resultArray[0];
                        row.data.draw =resultArray[1];
                        row.data.away =resultArray[2];
                        if (!berow.get('hasstatistics')) {
                        var maxValue = Math.max(resultArray[0],resultArray[1],resultArray[2])
                        var addwin = false,addvictory = false;
                        if ((maxValue == row.data.home && berow.data.victory == 3) || (maxValue == row.data.draw && berow.data.victory == 1) || (maxValue == row.data.away && berow.data.victory == 0)) {
                            addvictory = true;
                        }
                        if ((row.data.home > row.data.away && berow.data.win == 3) || (row.data.home < row.data.away && berow.data.win == 0) || berow.data.win == 1) {
                            addwin = true;
                        }
                        scriptids.push(row.id);
                        wins.push(addwin);
                        victorys.push(addvictory);
                    }
                        
                    }
                    if (scriptids.length > 0) {
                        Ext.Ajax.request({
                            url:"Data/NowGoal/ForecastScripts.aspx?a=statistics",
                            params:{
                                matchid:berow.id,scriptids:scriptids,wins:wins,victorys:victorys
                            },
                            success:function(res ,req){
                                var result = Ext.decode(res.responseText);
                                if (result.success) {
    Ext.getCmp('BetExperienceTab').findByType("grid")[0].getStore().reload();
}else {
    Ext.Msg.alert("提 示" ,result.error);
}
                            }
                        });
                    }
                }
           }
    });

    //--------------------------------------------------列头
    var cm = new Ext.grid.ColumnModel([
        new Ext.grid.RowNumberer(),{
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
		},{
		    header: "赢盘率",
            dataIndex: "win",
		    sortable: false,
            align: "middle",
		    width: 50,
            renderer:function(v,i,r){
                return (r.get('win')/(r.get('win') + r.get('lost'))*100).toFixed(2) + "%";
            }
		},{
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
		},{
		    header: "胜负率",
            dataIndex: "resultwin",
		    sortable: false,
            align: "middle",
		    width: 50,
            renderer:function(v,i,r){
                return (r.get('resultwin')/(r.get('resultwin') + r.get('resultlost'))*100).toFixed(2) + "%";
            }
		}
]);

    //----------------------------------------------------定义grid
    var grid = new Ext.grid.GridPanel({
        store: store,
        cm: cm,
        sm:new Ext.grid.CheckboxSelectionModel({ 
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
            forceFit: true,//自动填充
            sortAscText: '正序排列',
            sortDescText: '倒序排列',
            columnsText: '显示/隐藏列'
        }
    });
    var win = new Ext.Window({
        width:800,
        height:480,
        layout:'fit',
        items:[grid]
    })
    win.show();
    store.load();
}