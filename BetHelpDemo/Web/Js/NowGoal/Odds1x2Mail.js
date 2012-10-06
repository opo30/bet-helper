/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />

var Odds1x2Mail = function (scheduleid) {
    if (!issendmail) {
        showNotify("提示", "请先开启邮件提醒功能！", false);
        return;
    }
    var scheduleArr, scheduleTypeArr;
    var minite = 60 * 24;
    oddsHttp.open("get", "Data/NowGoal/GetRemoteFile.aspx?f=oddsjs&path=" + scheduleid + ".js", false);
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
    var minite = 60 * 24;
    oddsHttp.open("get", "Data/NowGoal/GetRemoteFile.aspx?f=oddsjs&path=" + scheduleid + ".js", false);
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

    var CompanyGrid = function (title, query) {
        var group = new Ext.ux.grid.ColumnHeaderGroup({
            rows: [[
          { header: '', colspan: 1, align: 'center' },
          { header: '初  盘', colspan: 4, align: 'center' },
          { header: '即时盘', colspan: 4, align: 'center' }
      ]]
        });
        var fields = [{ name: 'companyid', type: 'string' }, { name: 'fullname', type: 'string' }, { name: 'isprimary', type: 'bool' }, { name: 'isexchange', type: 'bool' }, { name: 'scount', type: 'int' }, { name: 'swin', type: 'float' }, { name: 'sdraw', type: 'float' }, { name: 'slost', type: 'float' }, { name: 'ecount', type: 'int' }, { name: 'ewin', type: 'float' }, { name: 'edraw', type: 'float' }, { name: 'elost', type: 'float' }, { name: 'so', type: 'float'}];

        var store = new Ext.data.Store({
            proxy: new Ext.data.HttpProxy(
               {
                   url: "Data/NowGoal/GetOdds1x2History.aspx?a=stat1",
                   method: "POST"
               }),
            reader: new Ext.data.JsonReader(
               {
                   fields: fields,
                   id: "companyid"
               }),
               sortInfo: {field: 'so', direction: 'ASC'} ,
            baseParams: {
                query: query,
                stypeid: scheduleTypeArr.join('^'),
                oddsarr: oddsArr.join('^'),
                schedulearr: scheduleArr.join('^'),
                odds: Ext.getDom("tr1_" + scheduleid) ? Ext.getDom("tr1_" + scheduleid).getAttribute("odds") : ""
            }
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
		        renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		            if (value > 0) {
		                cell.cellAttr = 'bgcolor="#F7CFD6"';
		            }
		            else if (value < 0) {
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
		        renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		            if (value > 0) {
		                cell.cellAttr = 'bgcolor="#F7CFD6"';
		            }
		            else if (value < 0) {
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
		        renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		            if (value > 0) {
		                cell.cellAttr = 'bgcolor="#F7CFD6"';
		            }
		            else if (value < 0) {
		                cell.cellAttr = 'bgcolor="#DFF3B1"';
		            }
		            return value;
		        }
		    }, {
		        header: "总数",
		        dataIndex: "ecount",
		        sortable: true,
		        align: "middle",
		        width: 50,
		        renderer: function (value, last, row) {
		            return value;
		        }
		    }, {
		        header: "胜",
		        dataIndex: "ewin",
		        sortable: true,
		        align: "middle",
		        width: 50,
		        renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		            if (value > row.get("swin") && value > 0) {
		                cell.cellAttr = 'bgcolor="#F7CFD6"';
		            }
		            else if (value < row.get("swin") && value < 0) {
		                cell.cellAttr = 'bgcolor="#DFF3B1"';
		            }
		            return value;
		        }
		    }, {
		        header: "平",
		        dataIndex: "edraw",
		        sortable: true,
		        align: "middle",
		        width: 50,
		        renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		            if (value > row.get("sdraw") && value > 0) {
		                cell.cellAttr = 'bgcolor="#F7CFD6"';
		            }
		            else if (value < row.get("sdraw") && value < 0) {
		                cell.cellAttr = 'bgcolor="#DFF3B1"';
		            }
		            return value;
		        }
		    }, {
		        header: "负",
		        dataIndex: "elost",
		        sortable: true,
		        align: "middle",
		        width: 50,
		        renderer: function (value, cell, row, rowIndex, colIndex, ds) {
		            if (value > row.get("slost") && value > 0) {
		                cell.cellAttr = 'bgcolor="#F7CFD6"';
		            }
		            else if (value < row.get("slost") && value < 0) {
		                cell.cellAttr = 'bgcolor="#DFF3B1"';
		            }
		            return value;
		        }
		    }
    ]);

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
            plugins: [group],
            viewConfig: {
                //自动填充
                emptyText: '没有记录',
                forceFit: true,
                getRowClass: function (record, rowIndex, rowParams, store) {
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
            items: [CompanyGrid("全部", 1), CompanyGrid("国家", 2), CompanyGrid("赛事", 3)],
            listeners: {
                tabchange: function (tab, p) {
                    p.getStore().load();
                }
            }
        }],
        listeners: {
            show: function () {
                //this.findByType("tabpanel")[0].activeTab.getStore().load();
            }
        }
    });
    win.show();
}
