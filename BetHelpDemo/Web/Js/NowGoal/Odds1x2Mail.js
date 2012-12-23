/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />

var Odds1x2Mail = function (scheduleid) {
    if (!issendmail) {
        showNotify("提示", "请先开启邮件提醒功能！", false);
        return;
    }
    var scheduleArr, scheduleTypeArr;
    var minite = 60 * 12;
    oddsHttp.open("get", "Data/NowGoal/GetRemoteHtml.aspx?a=EuropeOddsJS&matchid=" + scheduleid, false);
    oddsHttp.send(null);
    if (oddsHttp.responseText == "") {
        showNotify("提示", "目前没有欧赔数据", false);
        return;
    }
    eval(oddsHttp.responseText);
    oddsArr = [];
    for (var i = 0; i < game.length; i++) {
        var arr = game[i].split('|');
        if (getDate(MatchTime) - getDate(arr[20]) < 60000 * minite) {
            oddsArr.push(game[i]);
        }
    }
    if (oddsArr.length == 0) {
        showNotify("提示", "临场" + minite + "分钟内没有开出赔率！", false);
        return;
    }
    for (var i = 0; i < A.length; i++) {
        if (A[i][0] == scheduleid) {
            scheduleArr = A[i];
            scheduleTypeArr = B[A[i][1]];
        }
    }
    if (scheduleArr == undefined) {
        for (var i = 0; i < HistoryScore.A.length; i++) {
            if (HistoryScore.A[i][0] == scheduleid) {
                scheduleArr = HistoryScore.A[i];
                scheduleTypeArr = HistoryScore.B[HistoryScore.A[i][1]];
            }
        }
    }
    Ext.Ajax.request({
        url: 'Data/SendMessage.aspx?a=mail',
        params: {
            stypeid: scheduleTypeArr.join('^'),
            oddsarr: oddsArr.join('^'),
            schedulearr: scheduleArr.join('^'),
            odds: Ext.getDom("tr1_" + scheduleid) ? Ext.getDom("tr1_" + scheduleid).getAttribute("odds") : ""
        }
    });
}

var Odds1x2Mail1 = function (scheduleid) {
    var scheduleArr, scheduleTypeArr;
    var minite = 60 * 12;
    oddsHttp.open("get", "Data/NowGoal/GetRemoteHtml.aspx?a=EuropeOddsJS&matchid=" + scheduleid, false);
    oddsHttp.send(null);
    if (oddsHttp.responseText == "") {
        showNotify("提示", "目前没有欧赔数据", false);
        return;
    }
    eval(oddsHttp.responseText);
    oddsArr = [];
    for (var i = 0; i < game.length; i++) {
        var arr = game[i].split('|');
        if (getDate(MatchTime) - getDate(arr[20]) < 60000 * minite) {
            oddsArr.push(game[i]);
        }
    }
    if (oddsArr.length == 0) {
        showNotify("提示", "没有开出赔率！", false);
        return;
    }
    for (var i = 0; i < A.length; i++) {
        if (A[i][0] == scheduleid) {
            scheduleArr = A[i];
            scheduleTypeArr = B[A[i][1]];
        }
    }
    if (scheduleArr == undefined) {
        for (var i = 0; i < HistoryScore.A.length; i++) {
            if (HistoryScore.A[i][0] == scheduleid) {
                scheduleArr = HistoryScore.A[i];
                scheduleTypeArr = HistoryScore.B[HistoryScore.A[i][1]];
            }
        }
    }

    var CompanyGrid = function (title, query) {
        var fields = [{ name: 'companyid', type: 'string' }, { name: 'fullname', type: 'string' }, { name: 'isprimary', type: 'bool' }, { name: 'isexchange', type: 'bool' }, { name: 'scount', type: 'int' }, { name: 'swin', type: 'float' }, { name: 'sdraw', type: 'float' }, { name: 'slost', type: 'float' }, { name: 'type', type: 'int' }, { name: 'time', type: 'date'}];

        var store = new Ext.data.GroupingStore({
            proxy: new Ext.data.HttpProxy(
               {
                   url: "Data/NowGoal/GetOdds1x2History.aspx?a=stat1",
                   method: "POST"
               }),
            reader: new Ext.data.JsonReader(
               {
                   fields: fields
               }),
            baseParams: {
                query: query,
                stypeid: scheduleTypeArr.join('^'),
                oddsarr: oddsArr.join('^'),
                schedulearr: scheduleArr.join('^'),
                odds: Ext.getDom("tr1_" + scheduleid) ? Ext.getDom("tr1_" + scheduleid).getAttribute("odds") : ""
            },
            groupField: 'type',
            sortInfo: { field: "time", direction: "DESC" }
        });

        //--------------------------------------------------列头
        var cm = new Ext.grid.ColumnModel([
		    {
		        header: "公司",
		        dataIndex: "fullname",
		        sortable: true,
		        align: "middle",
		        width: 50,
		        renderer: function (value, last, row) {
		            var color = "";
		            if (row.get("isprimary")) {
		                color = "blue";
		            } else if (row.get("isexchange")) {
		                color = "green";
		            }
		            return "<font color=" + color + ">" + value + "</font>";
		        }
		    }, {
		        header: "盘口",
		        dataIndex: "type",
		        sortable: true,
		        align: "middle",
		        width: 50,
		        renderer: function (value, last, row) {
		            if (value == 1) {
		                return "初  盘";
		            }
		            else if (value == 2) {
		                return "临场盘";
		            }
		            return value;
		        }
		    }, {
		        header: "总数",
		        dataIndex: "scount",
		        sortable: true,
		        align: "middle",
		        width: 50,
		        renderer: function (value, last, row) {
		            return value;
		        }
		    }, {
		        header: "胜",
		        dataIndex: "swin",
		        sortable: true,
		        align: "middle",
		        width: 50,
		        summaryType: 'average',
		        renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		            if (value > row.get("sdraw") && value > row.get("slost")) {
		                cell.cellAttr = 'bgcolor="#F7CFD6"';
		            }
		            else if (value < row.get("sdraw") && value < row.get("slost")) {
		                cell.cellAttr = 'bgcolor="#DFF3B1"';
		            }
		            return value;
		        }
		    }, {
		        header: "平",
		        dataIndex: "sdraw",
		        sortable: true,
		        align: "middle",
		        width: 50,
		        summaryType: 'average',
		        renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		            if (value > row.get("swin") && value > row.get("slost")) {
		                cell.cellAttr = 'bgcolor="#F7CFD6"';
		            }
		            else if (value < row.get("swin") && value < row.get("slost")) {
		                cell.cellAttr = 'bgcolor="#DFF3B1"';
		            }
		            return value;
		        }
		    }, {
		        header: "负",
		        dataIndex: "slost",
		        sortable: true,
		        align: "middle",
		        width: 50,
		        summaryType: 'average',
		        renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		            if (value > row.get("swin") && value > row.get("sdraw")) {
		                cell.cellAttr = 'bgcolor="#F7CFD6"';
		            }
		            else if (value < row.get("swin") && value < row.get("sdraw")) {
		                cell.cellAttr = 'bgcolor="#DFF3B1"';
		            }
		            return value;
		        }
		    }, {
		        header: "时间",
		        dataIndex: "time",
		        sortable: true,
		        align: "middle",
		        width: 50,
		        renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		            return value.format("Y-m-d H:i");
		        }
		    }
    ]);
        var summary = new Ext.ux.grid.GroupSummary();

        //----------------------------------------------------定义grid
        var grid = new Ext.grid.GridPanel({
            title: title,
            store: store,
            cm: cm,
            sm: new Ext.grid.CheckboxSelectionModel({
                singleSelect: true
            }),
            loadMask: true,
            stripeRows: true,
            height: 250,
            //超过长度带自动滚动条
            autoScroll: true,
            border: false,
            sortable: false,
            plugins: [summary],
            view: new Ext.grid.GroupingView({
                //自动填充
                emptyText: '没有记录',
                forceFit: true,
                sortAscText: '正序排列',
                sortDescText: '倒序排列',
                columnsText: '列显示/隐藏',
                groupByText: '根据本列分组',
                showGroupsText: '是否采用分组显示',
                groupTextTpl: '{text} (<b><font color=red>{[values.rs.length]}</font> </b>{[values.rs.length > 0 ? "条" : "暂无历史记录"]})'
            }),
            listeners: {
                show: function (p) {
                    p.getStore().load();
                }
            }
        });
        return grid;
    };

    var win = new Ext.Window({
        layout: 'fit',
        closeAction: 'destroy',
        title: scheduleTypeArr[1] + " " + scheduleArr[4] + "-" + scheduleArr[7],
        width: 960,
        height: 500,
        resizeable: true,
        autoScroll: true,
        items: [{
            xtype: 'tabpanel',
            activeItem: 0,
            items: [CompanyGrid("全部", 1), CompanyGrid("国家", 2), CompanyGrid("赛事", 3)]
        }],
        listeners: {
            show: function () {
                this.findByType("tabpanel")[0].activeTab.getStore().load();
            }
        }
    });
    win.show();
}
