/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />

var LiveDatastore;
var customfun = function() {
    alert("aaa");
}
var addbutton = function() {

}


//A[0] = [355515, 17, 7147, 7146, '夏拉祖', '夏拉祖', 'Xelaju MC', '齐奥内斯交流会', '齊奧內斯交流會', 'Club Comunicaciones', '10:00', '2009,11,14,11,13,14', 3, 1, 1, 1, 1, 0, 0, 0, 0, '危地甲1', '危地甲6', , '', , '', '', 0];
    //指定列参数
var fields = [{ name: 'match_0', type: 'int' }, 
            { name: 'match_1', type: 'int' },
            { name: 'match_2', type: 'int' },
            { name: 'match_3', type: 'int' },
            { name: 'match_4', type: 'string' },
            { name: 'match_5', type: 'string' },
            { name: 'match_6', type: 'string' },
            { name: 'match_7', type: 'string' },
            { name: 'match_8', type: 'string' },
            { name: 'match_9', type: 'string' },
            { name: 'match_10', type: 'string' },
            { name: 'match_11', type: 'string' },
            { name: 'match_12', type: 'int' },
            { name: 'match_13', type: 'int' },
            { name: 'match_14', type: 'int' },
            { name: 'match_15', type: 'int' },
            { name: 'match_16', type: 'int' },
            { name: 'match_17', type: 'int' },
            { name: 'match_18', type: 'int' },
            { name: 'match_19', type: 'int' },
            { name: 'match_20', type: 'int' },
            { name: 'match_21', type: 'string' },
            { name: 'match_22', type: 'string' },
            { name: 'match_23', type: 'string'},
            { name: 'match_24', type: 'string'},
            { name: 'match_25', type: 'string' },
            { name: 'match_26', type: 'string' },
            { name: 'match_27', type: 'string' },
            { name: 'match_28', type: 'string' },
            { name: 'match_29', type: 'int' },
            { name: 'class_0', type: 'string' },
            { name: 'class_1', type: 'string' },
            { name: 'class_2', type: 'string' },
            { name: 'class_3', type: 'string' },
            { name: 'class_4', type: 'string' },
            { name: 'class_5', type: 'string' },
            { name: 'class_6', type: 'string' },
            { name: 'class_7', type: 'string' },
            { name: 'class_8', type: 'string'}];

            var store = new Ext.data.ArrayStore({
                fields: fields
            });

            LiveDatastore = new Ext.data.Store({
                proxy: new Ext.data.HttpProxy(
           {
               url: "Data/NowGoal/LiveData.aspx?" + Date.parse(new Date()),
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
                now: ""
                }
            });
//       LiveDatastore.on("beforeload", function(store) {
//           Ext.apply(this.baseParams, { now: 0 });
//       });

//    var saph;
//    var sapd;
//    var sapa;
//    var eaph;
//    var eapd;
//    var eapa;

//    LiveDatastore.on("datachanged", function(store) {
//        var saphcount = 0;
//        var sapdcount = 0;
//        var sapacount = 0;
//        var eaphcount = 0;
//        var eapdcount = 0;
//        var eapacount = 0;
//        var count = Odds1x2store.data.items.length;
//        for (var i = 0; i < count; i++) {
//            saphcount += parseFloat(Odds1x2store.data.items[i].data["Odds_6"]);
//            sapdcount += parseFloat(Odds1x2store.data.items[i].data["Odds_7"]);
//            sapacount += parseFloat(Odds1x2store.data.items[i].data["Odds_8"]);
//            if (Odds1x2store.data.items[i].data["Odds_13"] != "") {
//                eaphcount += parseFloat(Odds1x2store.data.items[i].data["Odds_13"]);
//                eapdcount += parseFloat(Odds1x2store.data.items[i].data["Odds_14"]);
//                eapacount += parseFloat(Odds1x2store.data.items[i].data["Odds_15"]);
//            } else {
//                eaphcount += parseFloat(Odds1x2store.data.items[i].data["Odds_6"]);
//                eapdcount += parseFloat(Odds1x2store.data.items[i].data["Odds_7"]);
//                eapacount += parseFloat(Odds1x2store.data.items[i].data["Odds_8"]);
//            }
//        }
//        saph = saphcount / count;
//        sapd = sapdcount / count;
//        sapa = sapacount / count;
//        eaph = eaphcount / count;
//        eapd = eapdcount / count;
//        eapa = eapacount / count;
//    });
    
    //--------------------------------------------------列选择模式
    var sm = new Ext.grid.CheckboxSelectionModel({
        dataIndex: "match_0",
        listeners: {
            // On selection change, set enabled state of the removeButton
            // which was placed into the GridPanel using the ref config
            selectionchange: function(s) {
//                if (s.getCount()) {
//                    if (s.getCount() == 1) {
//                        grid.toolbars[0].get('delbutton').enable();
//                        grid.toolbars[0].get('editbutton').enable();
//                    } else {
//                        grid.toolbars[0].get('delbutton').enable();
//                        grid.toolbars[0].get('editbutton').disable();
//                    }

//                } else {
//                    grid.toolbars[0].get('delbutton').disable();
//                    grid.toolbars[0].get('editbutton').disable();
//                }
            }
        }
    });
    //--------------------------------------------------列头
    var cm = new Ext.grid.ColumnModel([
		sm, {
		    header: "赛事",
		    dataIndex: "class_1",
		    tooltip: "赛事名称",
		    //列不可操作
		    //menuDisabled:true,
		    //可以进行排序
		    sortable: false,
		    renderer: function(value) {
		        return "<font color='white'>" + value + "</span>"
		    }
		}, {
		    header: "时间",
		    dataIndex: "match_10",
		    tooltip: "比赛开始时间",
		    sortable: false,
		    renderer: function(value, last, row) {
		        return "<span ext:qtip='" + row.data["match_11"] + "'>" + value + "</span>"
		    }
		}, {
		    header: "状态",
		    dataIndex: "match_3",
		    tooltip: "注单球队名称",
		    sortable: false,
		    renderer: function(value, last, row) {
		        try {
		            var t = row.data["match_11"].split("-");
		            var t2 = new Date(t[0], t[1], t[2], t[3], t[4], t[5]);
		            if (row.data["match_12"] == "-1")
		                return "<font color='blue'>完</font>";
		            if (row.data["match_12"] == "0") {
		                var min = Math.abs(Math.floor((new Date() - t2 - difftime) / 60000));
		                if (min > 60)
		                    return Math.floor(min / 60) + "时" + Math.floor(min % 60) + "分开赛";
		                else
		                    return min + "分开赛";
		            }
		            if (row.data["match_12"] == "1") {  //上半场
		                var goTime = Math.floor((new Date() - t2 - difftime) / 60000);
		                if (goTime > 45) goTime = "45+";
		                if (goTime < 1) goTime = "1";
		                return goTime + "<img src='Images/live/in.gif' border=0>";
		            }
		            if (row.data["match_12"] == "2") {
		                return "<font color='red'>中</font>";
		            }
		            if (row.data["match_12"] == "3") {  //下半场
		                goTime = Math.floor((new Date() - t2 - difftime) / 60000) + 46;
		                if (goTime > 90) goTime = "90+";
		                if (goTime < 46) goTime = "46";
		                return goTime + "<img src='Images/live/in.gif' border=0>";
		            }
		        } catch (e) { }
		    }
		}, {
		    header: "主队",
		    dataIndex: "match_4",
		    tooltip: "主场球队名称",
		    //列不可操作
		    //menuDisabled:true,
		    //可以进行排序
		    sortable: false,
		    renderer: function(value, last, row) {
		        if (row.data["match_17"] != "0")
		            H_redcard = "<img src='images/live/redcard" + row.data["match_17"] + ".gif'>";
		        else
		            H_redcard = "";
		        if (row.data["match_19"] != "0")
		            H_yellow = "<img src='images/live/yellow" + row.data["match_19"] + ".gif'>";
		        else
		            H_yellow = "";
		        return value + H_redcard + H_yellow;
		    }
		}, {
		    header: "比分",
		    tooltip: "实时比分",
		    dataIndex: "match_13",
		    sortable: false,
		    align: "center",
		    renderer: function(value, last, row) {


		        var state = parseInt(row.data["match_12"]);
		        var text = "";
		        switch (state) {
		            case 0:
		                if (row.data["match_23"] == "1") text = "阵容"; else text = "-";
		                break;
		            case 1:
		                text = row.data["match_13"] + "-" + row.data["match_14"];
		                break;
		            case -11:
		            case -14:
		                text = "";
		                break;
		            default:
		                text = row.data["match_13"] + "-" + row.data["match_14"];
		                break;
		        }
		        return text;
		    }
		}, {
		    header: "客队",
		    dataIndex: "match_7",
		    tooltip: "客场球队名称",
		    //列不可操作
		    //menuDisabled:true,
		    //可以进行排序
		    sortable: false,
		    renderer: function(value, last, row) {
		        if (row.data["match_18"] != "0") G_redcard = "<img src='images/live/redcard" + row.data["match_18"] + ".gif'>"; else G_redcard = "";
		        if (row.data["match_20"] != "0") G_yellow = "<img src='images/live/yellow" + row.data["match_20"] + ".gif'>"; else G_yellow = "";

		        return value + G_redcard + G_yellow;
		    }
		}, {
		    header: "半场",
		    tooltip: "半场结束比分",
		    dataIndex: "match_15",
		    sortable: false,
		    renderer: function(value, last, row, p4, p5, store) {
		        return "<font color='blue'>" + value + "-" + row.data["match_16"] + "</font>"
		    }
		}, {
		    header: "数据",
		    tooltip: "比赛分析",
		    dataIndex: "match_11",
		    sortable: true,
		    renderer: function(value, last, row, p4, p5, store) {
		    return "<font color='blue' onclick='GetPrediction(" + parseInt(row.data["match_0"]) + ")'>析</font>&nbsp;&nbsp;&nbsp;<font color='blue' onclick='GetPredictionBig(" + parseInt(row.data["match_0"]) + ")'>大小</font>&nbsp;&nbsp;&nbsp;<font color='blue' onclick='LiveChartManage(" + parseInt(row.data["match_0"]) + ",8)'>图</font>";
		    }
		}, {
		    header: "指数",
		    tooltip: "即时赔率",
		    dataIndex: "match_12",
		    sortable: true
		}, {
		    header: "走",
		    tooltip: "是否开滚球盘",
		    dataIndex: "match_13",
		    sortable: true,
		    renderer: function(value) {
		        if (value == "1")
		            return "<img src='images/live/t3.gif' height=10 width=10 ext:qtip='有走地赛事'/>";
		        if (value == "2")
		            return "<img src='images/live/t32.gif' height=10 width=10 ext:qtip='正在走地'/>";
		}
//		,{
//		    header: "主凯利",
//		    tooltip: "主胜的凯利指数",
//		    dataIndex: "match_13",
//		    sortable: true,
//		    renderer: function(value) {
//		        
//		    }

}]);


var datefield = new Ext.form.DateField({
    name: 'date',
    onSelect: function(q, p) {
        this.setValue(p.format("Y-m-d"));
        LiveDatastore.baseParams.now = p.format("Y-m-d");
        LiveDatastore.reload(); // 重载当前tab列参数
    }
});


    //----------------------------------------------------定义grid
var LiveDataGrid = new Ext.grid.GridPanel({
    id: "LiveDataGrid",
    store: LiveDatastore,
    sm: sm,
    cm: cm,
    loadMask: true,
    stripeRows: true,
    autoWidth: true,
    //超过长度带自动滚动条
    autoScroll: true,
    border: false,
    viewConfig: {
        //自动填充
        forceFit: true,
        sortAscText: '正序排列',
        sortDescText: '倒序排列',
        columnsText: '列显示/隐藏'
    },
    tbar: [
        datefield,
		new Ext.Toolbar.Fill()
		, {
		    text: '刷新',
		    iconCls: "refreshicon",
		    tooltip: '刷新列表',
		    handler: function() { LiveDatastore.baseParams.now = ""; LiveDatastore.reload(); }
		}, "", "-", "", {
		    text: "综合赔率",
		    //默认样式为按下
		    //pressed:true,
		    tooltip: "赔率三合一",
		    iconCls: "addicon",
		    handler: function() {
		        var row = LiveDataGrid.getSelectionModel().getSelections();
		        if (row.length == 0) {
		            Ext.Msg.alert("提示信息", "您没有选中任何行!");
		        }
		        else if (row.length > 1) {
		            Ext.Msg.alert("提示信息", "对不起只能选择一个!");
		        } else if (row.length == 1) {
		            OddsDetailManage(row[0].data.match_0, row[0].data.match_4 + "-" + row[0].data.match_7);
		        }
		    }
		}, "", "-", "", {
		    text: "百家欧赔",
		    tooltip: "百家欧赔",
		    iconCls: "editicon",
		    handler: function() {
		        var row = LiveDataGrid.getSelectionModel().getSelections();
		        if (row.length == 0) {
		            Ext.Msg.alert("提示信息", "您没有选中任何行!");
		        }
		        else if (row.length > 1) {
		            Ext.Msg.alert("提示信息", "对不起只能选择一个!");
		        } else if (row.length == 1) {
		            Odds1x2Manage(row[0].data.match_0, row[0].data.match_4 + "-" + row[0].data.match_7);
		        }
		        
		    }
		}]

});

LiveDataGrid.getStore().on('load', function(s, records) {
    var gridcount = 0;
    s.each(function(r) {
        LiveDataGrid.getView().getCell(gridcount, 1).style.backgroundColor = r.get('class_4');
        gridcount++;
    });
});

    //传入icon样式
//GridMain(node, grid, "openroomiconinfo");

var GetPrediction = function(scheduleID) {
    Ext.MessageBox.show({
        msg: '正在分析比赛数据，请稍等...',
        progressText: 'Saving...',
        width: 300,
        wait: true,
        waitConfig: { interval: 200 },
        icon: 'download',
        animEl: 'saving'
    });

    Ext.Ajax.request({
        url: "Data/NowGoal/GetPrediction.aspx",
        method: "POST",
        timeout: 30000,
        params: {
            scheduleID: scheduleID
        },
        success: function(p1, p2) {
                        var result = p1.responseText;
                        Ext.MessageBox.hide();
                        Ext.Msg.alert("预测结果",result);
        },
        failure: function() {
            Ext.Msg.alert("预测结果","无结果或预测失败");
        }

    });
}

var GetPredictionBig = function(scheduleID) {
Ext.Msg.prompt('输入', '请输入您判断大小球大小:', function(btn, text) {
    if (btn == 'ok') {
        if (text == '') {
            Ext.Msg.alert('提示', '大小球数字不能为空');
        }
        else {
            Ext.MessageBox.show({
                msg: '正在分析比赛数据，请稍等...',
                progressText: 'Saving...',
                width: 300,
                wait: true,
                waitConfig: { interval: 200 },
                icon: 'download',
                animEl: 'saving'
            });

            Ext.Ajax.request({
                url: "Data/NowGoal/GetPrediction.aspx",
                method: "POST",
                timeout: 30000,
                params: {
                    scheduleID: scheduleID,
                    goalNum: text
                },
                success: function(p1, p2) {
                    var result = p1.responseText;
                    Ext.MessageBox.hide();
                    Ext.Msg.alert("预测结果", result);
                },
                failure: function() {
                    Ext.Msg.alert("预测结果", "无结果或预测失败");
                }

            });
        }
    }
});

    
}