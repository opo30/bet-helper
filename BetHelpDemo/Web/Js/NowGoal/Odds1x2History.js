/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />
var Odds1x2History = function (scheduleArr, scheduleTypeArr, oddeArr) {

    var fields = [
            { name: 'companyid', type: 'int' },
            { name: 'companyname', type: 'string' },
            { name: 'sumwin', type: 'int' },
            { name: 'sumdraw', type: 'int' },
            { name: 'sumlost', type: 'int'}];
    var params = {
        stypeid: scheduleTypeArr[0],
        oddsarr: oddeArr.join('^')
    };

    var store = new Ext.data.JsonStore({
        root: 'data',
        fields: fields,
        baseParams: params,
        proxy: new Ext.data.HttpProxy(
            {
                url: "Data/NowGoal/GetOdds1x2History.aspx?a=stat",
                method: "POST",
                timeout: 3600000
            })
        //        reader: new Ext.data.JsonReader(
        //           {
        //               fields: fields,
        //               root:"data",
        //               id: "companyid"
        //           }),
        //           sortInfo: { field: 'perwin', direction: "DESC" },
    });

    //--------------------------------------------------列头
    var cm = new Ext.grid.ColumnModel([
        {
            header: "公司",
            dataIndex: "companyname",
            sortable: true
        },
		{
		    header: "主胜",
		    tooltip: "主场球队获胜赔率",
		    dataIndex: "sumwin",
		    sortable: true,
		    width: 30
		}, {
		    header: "和局",
		    tooltip: "比赛打平的赔率",
		    dataIndex: "sumdraw",
		    sortable: true,
		    width: 30
		}, {
		    header: "客胜",
		    tooltip: "客场球队获胜赔率",
		    dataIndex: "sumlost",
		    sortable: true,
		    width: 30
		}
]);


    //----------------------------------------------------定义grid
    var grid = new Ext.grid.GridPanel({
        id: "oddsstat-grid",
        store: store,
        cm: cm,
        region: "west",
        loadMask: true,
        stripeRows: true,
        width:300,
        //超过长度带自动滚动条
        autoScroll: true,
        border: false,
        viewConfig: {
            //自动填充
            forceFit: true,
            sortAscText: '正序排列',
            sortDescText: '倒序排列',
            columnsText: '显示/隐藏列',
            getRowClass: function (record, rowIndex, rowParams, store) {
            }
        },
        listeners: {
            rowclick: function (grid, rowIndex, cellIndex, e) {
                if (store.getAt(rowIndex).get('companyid') != 0) {
                    grid1.getStore().baseParams.companyid = store.getAt(rowIndex).get('companyid');
                    grid1.getStore().load();
                }
            }
        }
    });

    var grid1 = new Ext.grid.GridPanel({
        id: "oddslist-grid",
        region: "center",
        store: new Ext.data.JsonStore({
            root: 'data',
            fields: [{ name: 'h_teamname', type: 'string' },
                { name: 'score', type: 'string' },
                { name: 'g_teamname', type: 'string' },
                { name: 'e_win', type: 'string' },
                { name: 'e_draw', type: 'string' },
                { name: 'e_lost', type: 'string' }, { name: 'rangqiu', type: 'float' }, { name: 's_time', type: 'string'}],
            baseParams: { companyid: 0 },
            proxy: new Ext.data.HttpProxy({
                url: "Data/NowGoal/GetOdds1x2History.aspx?a=list",
                method: "POST",
                timeout: 3600000
            })
        }),
        cm: new Ext.grid.ColumnModel([
        {
            header: "主队",
            dataIndex: "h_teamname",
            sortable: true
        },
		{
		    header: "比分",
		    dataIndex: "score",
		    sortable: true,
		    width: 30
		}, {
		    header: "客队",
		    dataIndex: "g_teamname",
		    sortable: true,
		    width: 30
		}, {
		    header: "胜",
		    dataIndex: "e_win",
		    sortable: true,
		    width: 30
		}, {
		    header: "平",
		    dataIndex: "e_draw",
		    sortable: true,
		    width: 30
		}, {
		    header: "负",
		    dataIndex: "e_lost",
		    sortable: true,
		    width: 30
		}, {
		    header: "指数",
		    dataIndex: "rangqiu",
		    sortable: true,
		    width: 30
		}, {
		    header: "时间",
		    dataIndex: "s_time",
		    sortable: true,
		    width: 30
		}
]),
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
            columnsText: '显示/隐藏列',
            getRowClass: function (record, rowIndex, rowParams, store) {
            }
        }
    });

    var win = new Ext.Window({
        title: "历史赔率",
        width: 900,
        height: 400,
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
        layout: "border",
        buttonAlign: "center",
        items: [grid, grid1],
        listeners: {
            "show": function () {
                //当window show事件发生时清空一下表单
                store.load();
            }
        }
    });

    win.show();
}