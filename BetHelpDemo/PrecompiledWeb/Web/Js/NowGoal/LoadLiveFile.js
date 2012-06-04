/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />



//A[0] = [355515, 17, 7147, 7146, '夏拉祖', '夏拉祖', 'Xelaju MC', '齐奥内斯交流会', '齊奧內斯交流會', 'Club Comunicaciones', '10:00', '2009,11,14,11,13,14', 3, 1, 1, 1, 1, 0, 0, 0, 0, '危地甲1', '危地甲6', , '', , '', '', 0];
//指定列参数
var LoadLiveFile = function(node) {
    var match, sclass, matchdate, mclass;

    var store = new Ext.data.ArrayStore({
        fields: [{ name: 'matchid', type: 'int' },
            { name: 'matchtypeid', type: 'int' },
            { name: 'match_2', type: 'int' },
            { name: 'match_3', type: 'int' },
            { name: 'h_teamname', type: 'string' },
            { name: 'match_5', type: 'string' },
            { name: 'match_6', type: 'string' },
            { name: 'g_teamname', type: 'string' },
            { name: 'match_8', type: 'string' },
            { name: 'match_9', type: 'string' },
            { name: 'matchtime', type: 'string' },
            { name: 'match_11', type: 'string' },
            { name: 'matchstate', type: 'int' },
            { name: 'hfullgoal', type: 'int' },
            { name: 'gfullgoal', type: 'int' },
            { name: 'hhalfgoal', type: 'int' },
            { name: 'ghalfgoal', type: 'int' },
            { name: 'h_redcard', type: 'int' },
            { name: 'g_redcard', type: 'int' },
            { name: 'h_yellow', type: 'int' },
            { name: 'g_yellow', type: 'int' },
            { name: 'match_21', type: 'string' },
            { name: 'match_22', type: 'string' },
            { name: 'matchlineup', type: 'string' },
            { name: 'match_24', type: 'string' },
            { name: 'match_25', type: 'string' },
            { name: 'match_26', type: 'string' },
            { name: 'match_27', type: 'string' },
            { name: 'match_28', type: 'string' },
            { name: 'match_29', type: 'int'}]
    });

    //--------------------------------------------------列选择模式
    var sm = new Ext.grid.CheckboxSelectionModel({
        dataIndex: "matchid",
        listeners: {
            selectionchange: function(s) {

            }
        }
    });
    //--------------------------------------------------列头
    var cm = new Ext.grid.ColumnModel([
		sm, {
		    header: "赛事",
		    dataIndex: "matchtypeid",
		    tooltip: "赛事名称",
		    //列不可操作
		    //menuDisabled:true,
		    //可以进行排序
		    sortable: false,
		    renderer: function(value) {
		        return "<font color='white'>" + sclass[value][1] + "</span>"
		    }
		}, {
		    header: "时间",
		    dataIndex: "matchtime",
		    tooltip: "比赛开始时间",
		    sortable: false,
		    renderer: function(value) {
		        return value;
		    }
		}, {
		    header: "状态",
		    dataIndex: "matchid",
		    tooltip: "注单球队名称",
		    sortable: false,
		    renderer: function(value, last, row) {
		        return "<span id='time_" + value + "'></span>";
		    }
		}, {
		    header: "主队",
		    dataIndex: "h_teamname",
		    tooltip: "主场球队名称",
		    //列不可操作
		    //menuDisabled:true,
		    //可以进行排序
		    sortable: false,
		    renderer: function(value, last, row) {

		        if (row.data.h_redcard != 0) H_redcard = "<img src='images/redcard" + row.data.h_redcard + ".gif'>"; else H_redcard = "";
		        if (row.data.h_yellow != 0) H_yellow = "<img src='images/yellow" + row.data.h_yellow + ".gif'>"; else H_yellow = "";

		        return value + H_yellow + H_redcard;
		    }
		}, {
		    header: "比分",
		    tooltip: "实时比分",
		    dataIndex: "matchstate",
		    sortable: false,
		    align: "center",
		    renderer: function(value, last, row) {
		        switch (value) {
		            case 0:
		                if (row.data.matchlineup == "1") match_score = "阵容"; else match_score = "-";
		                break;
		            case 1:
		                match_score = row.data.hfullgoal + "-" + row.data.gfullgoal;
		                break;
		            case -11:
		            case -14:
		                match_score = "";
		                break;
		            default:
		                match_score = row.data.hfullgoal + "-" + row.data.gfullgoal;
		                break;
		        }
		        return match_score;
		    }
		}, {
		    header: "客队",
		    dataIndex: "g_teamname",
		    tooltip: "客场球队名称",
		    //列不可操作
		    //menuDisabled:true,
		    //可以进行排序
		    sortable: false,
		    renderer: function(value, last, row) {
		        if (row.data.g_redcard != 0) G_redcard = "<img src='images/redcard" + row.data.g_redcard + ".gif'>"; else G_redcard = "";
		        if (row.data.g_yellow != 0) G_yellow = "<img src='images/yellow" + row.data.g_yellow + ".gif'>"; else G_yellow = "";
		        return value + G_redcard + G_yellow;
		    }
		}, {
		    header: "半场",
		    tooltip: "半场结束比分",
		    dataIndex: "matchstate",
		    sortable: false,
		    renderer: function(value, last, row) {
		        switch (value) {
		            case 0:
		                match_half = "-";
		                break;
		            case 1:
		                match_half = "-";
		                break;
		            case -11:
		            case -14:
		                match_half = "";
		                break;
		            default:
		                if (row.data.hhalfgoal == null) row.data.hhalfgoal = "";
		                if (row.data.ghalfgoal == null) row.data.ghalfgoal = "";
		                match_half = row.data.hhalfgoal + "-" + row.data.ghalfgoal;
		                break;
		        }
		        return match_half;
		    }
		}, {
		    header: "指数",
		    tooltip: "即时赔率",
		    dataIndex: "matchstate",
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

}]);



    //----------------------------------------------------定义grid
    var grid = new Ext.grid.GridPanel({
        id: "LiveFileGrid",
        store: store,
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
		new Ext.Toolbar.Fill()
		, {
		    text: '刷新',
		    iconCls: "refreshicon",
		    tooltip: '刷新列表',
		    handler: function() { store.reload(); }
		}, "", "-", "", {
		    text: "综合赔率",
		    //默认样式为按下
		    //pressed:true,
		    tooltip: "赔率三合一",
		    iconCls: "addicon",
		    handler: function() {
		        var row = grid.getSelectionModel().getSelections();
		        if (row.length == 0) {
		            Ext.Msg.alert("提示信息", "您没有选中任何行!");
		        }
		        else if (row.length > 1) {
		            Ext.Msg.alert("提示信息", "对不起只能选择一个!");
		        } else if (row.length == 1) {
		            OddsDetailManage(row[0].data.matchid, row[0].data.h_teamname + "-" + row[0].data.g_teamname);
		        }
		    }
		}, "", "-", "", {
		    text: "百家欧赔",
		    tooltip: "百家欧赔",
		    iconCls: "editicon",
		    handler: function() {
		        var row = grid.getSelectionModel().getSelections();
		        if (row.length == 0) {
		            Ext.Msg.alert("提示信息", "您没有选中任何行!");
		        }
		        else if (row.length > 1) {
		            Ext.Msg.alert("提示信息", "对不起只能选择一个!");
		        } else if (row.length == 1) {
		            Odds1x2Manage(row[0]);
		        }

		    }
}]

    });

    store.on('load', function(s, records) {
        var gridcount = 0;
        s.each(function(r) {
            grid.getView().getCell(gridcount, 1).style.backgroundColor = sclass[r.get('matchtypeid')][4];
            gridcount++;
        });
    });

    var tabkey = "livefile";
    if (!Ext.getCmp('centertab').items.containsKey(tabkey)) {
        var tab = center.add({
            id: tabkey,
            iconCls: 'totalicon',
            xtype: "panel",
            title: node.text,
            closable: true,
            layout: "fit",
            items: [grid]
        });
    }
    Ext.getCmp('centertab').setActiveTab(tabkey);

    var LoadLiveFile = function() {
        grid.loadMask.show();
        Ext.Ajax.request({
            url: 'Data/NowGoal/LoadLiveFile.aspx',
            method: 'POST',
            success: function(res) {
                var ShowBf = function() {
                    match = A;
                    sclass = B;
                    mdate = matchdate;
                    mclass = C;
                    store.loadData(match);
                    Ext.TaskMgr.start
                    ({
                        run: function() { setMatchTime(); }, interval: 30000
                    });
                    grid.loadMask.hide();
                }
                eval(res.responseText);

            }
        });
    }

    Ext.TaskMgr.start
    ({
        run: function() { LoadLiveFile(); }, interval: 3600 * 1000
    });

    function gettime() {
        try {
            LoadTime = (LoadTime + 1) % 60;
            if (LoadTime == 0)
                oXmlHttp.open("get", "data/change2.xml?" + Date.parse(new Date()), true);
            else
                oXmlHttp.open("get", "data/change.xml?" + Date.parse(new Date()), true);
            oXmlHttp.onreadystatechange = refresh;
            oXmlHttp.send(null);
        }
        catch (e) { }
        window.setTimeout("gettime()", 2000);
    }

    //更新比赛进行的时间
    function setMatchTime() {
        for (var i = 0; i < match.length; i++) {
            try {
                if (match[i][1] == -1) continue;
                if (match[i][12] == "1") {  //上半场
                    var t = match[i][11].split(",");
                    var t2 = new Date(t[0], t[1], t[2], t[3], t[4], t[5]);
                    goTime = Math.floor((new Date() - t2 - difftime) / 60000);
                    if (goTime > 45) goTime = "45+";
                    if (goTime < 1) goTime = "1";
                    document.getElementById("time_" + match[i][0]).innerHTML = goTime + "<img src='images/in.gif' border=0>";
                }
                if (match[i][12] == "3") {  //下半场
                    var t = match[i][11].split(",");
                    var t2 = new Date(t[0], t[1], t[2], t[3], t[4], t[5]);
                    goTime = Math.floor((new Date() - t2 - difftime) / 60000) + 46;
                    if (goTime > 90) goTime = "90+";
                    if (goTime < 46) goTime = "46";
                    document.getElementById("time_" + match[i][0]).innerHTML = goTime + "<img src='images/in.gif' border=0>";
                }
            } catch (e) { }
        }
    }



}
