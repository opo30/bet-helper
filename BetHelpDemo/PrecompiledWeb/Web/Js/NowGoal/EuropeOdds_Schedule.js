/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />

var EuropeOdds_Schedule = function (scheduleArr, scheduleTypeArr, oddsArr) {
    //指定列参数
    var fields = [{ name: 'scheduleid', type: 'int' },
            { name: 'display', type: 'string' },
            { name: 'bgcolor', type: 'string' },
            { name: 'league', type: 'string' },
            { name: 'matchdate', type: 'string' },
            { name: 'state', type: 'string' },
            { name: 'h_team', type: 'string' },
            { name: 'goal', type: 'string' },
            { name: 'g_team', type: 'string' },
            { name: 'match_half', type: 'string' },
            { name: 'data', type: 'string' },
            { name: 'h_odds', type: 'string' },
            { name: 'pankou', type: 'string' },
            { name: 'g_odds', type: 'string' }, { name: 'zoudi', type: 'string' }, { name: 'other', type: 'string' }, { name: 'index', type: 'string' }, { name: 'classx2', type: 'string'}];

    var store = new Ext.data.ArrayStore({
        url: "Data/NowGoal/GetOdds1x2History.aspx?a=query",
        baseParams: { odds: oddsArr.join('^') },
        fields: fields,
        id: "scheduleid"
    });

    //--------------------------------------------------列选择模式
    var sm = new Ext.grid.CheckboxSelectionModel({
        dataIndex: "scheduleid",
        css: 'vertical-align: inherit;',
        listeners: {
            selectionchange: function (s) {

            }
        }
    });
    //--------------------------------------------------列头
    var cm = new Ext.grid.ColumnModel([
		sm, {
		    header: '赛事',
		    dataIndex: "league",
		    menuDisabled: true,
		    align: "center",
		    width: 8,
		    css: 'vertical-align: inherit;color:white;',
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.cellAttr = 'bgcolor="' + row.get("bgcolor") + '"';
		        return value;
		    }
		}, {
		    header: "时间",
		    dataIndex: "matchdate",
		    tooltip: "比赛开始时间",
		    menuDisabled: true,
		    align: "center",
		    css: 'vertical-align: inherit;',
		    width: 5,
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.cellAttr = 'id="mt_' + row.get("scheduleid") + '"';
		        return value;
		    }
		}, {
		    header: "状态",
		    dataIndex: "state",
		    tooltip: "注单球队名称",
		    menuDisabled: true,
		    align: "center",
		    css: 'vertical-align: inherit;',
		    width: 5,
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.cellAttr = 'id="time_' + row.get("scheduleid") + '"';
		        cell.css = 'fortime';
		        return value;
		    }
		}, {
		    header: "主队",
		    dataIndex: "h_team",
		    tooltip: "主场球队名称",
		    menuDisabled: true,
		    width: 16,
		    align: "right",
		    css: 'vertical-align: inherit;',
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.css = 'a1';
		        return value;
		    }
		}, {
		    header: "比分",
		    tooltip: "实时比分",
		    dataIndex: "goal",
		    menuDisabled: true,
		    width: 5,
		    align: "center",
		    css: 'vertical-align: inherit;',
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.css = row.get("classx2");
		        cell.cellAttr = "onmouseover='showdetail(" + row.get("index") + ",event)'";
		        return value;
		    }
		}, {
		    header: "客队",
		    dataIndex: "g_team",
		    tooltip: "客场球队名称",
		    menuDisabled: true,
		    width: 16,
		    align: "left",
		    css: 'vertical-align: inherit;',
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.css = 'a2';
		        return value;
		    }
		}, {
		    header: "半场",
		    tooltip: "半场结束比分",
		    dataIndex: "match_half",
		    menuDisabled: true,
		    width: 5,
		    align: "center",
		    css: 'vertical-align: inherit;',
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.css = 'td_half';
		        return value;
		    }
		}, {
		    header: "数据",
		    tooltip: "比赛分析",
		    dataIndex: "data",
		    menuDisabled: true,
		    width: 15,
		    css: 'vertical-align: inherit;',
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.css = 'fr';
		        return value;
		    }
		}, {
		    header: "指数",
		    tooltip: "即时赔率",
		    dataIndex: "h_odds",
		    menuDisabled: true,
		    align: "center",
		    css: 'vertical-align: inherit;',
		    width: 5,
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.css = 'oddstd';
		        return value;
		    }
		}, {
		    header: "指数",
		    tooltip: "即时赔率",
		    dataIndex: "pankou",
		    menuDisabled: true,
		    align: "center",
		    css: 'vertical-align: inherit;',
		    width: 5,
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.css = 'oddstd';
		        return value;
		    }
		}, {
		    header: "指数",
		    tooltip: "即时赔率",
		    dataIndex: "g_odds",
		    menuDisabled: true,
		    align: "center",
		    css: 'vertical-align: inherit;',
		    width: 5,
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.css = 'oddstd';
		        return value;
		    }
		}, {
		    header: "走",
		    tooltip: "是否开滚球盘",
		    dataIndex: "zoudi",
		    align: "center",
		    css: 'vertical-align: inherit;',
		    menuDisabled: true,
		    width: 3
		}, {
		    header: "其他",
		    dataIndex: "other",
		    align: "center",
		    menuDisabled: true,
		    width: 10,
		    css: 'vertical-align: inherit;color:green;',
		    renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		        cell.cellAttr = 'id="other_' + A[i][0] + '"';
		        return value;
		    }
		}]);

    //----------------------------------------------------定义grid
		var grid = new Ext.grid.GridPanel({
		    id: "EuropeOdds_Schedule_" + scheduleArr[0],
        store: store,
        sm: sm,
        cm: cm,
        columnLines: true,
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