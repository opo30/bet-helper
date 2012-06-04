/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />

function showForecastScript(res, req) {
    var dataStore = Ext.getCmp('BetExperienceTab').findByType("linechart")[0].store;
    var yucejieguo = [];
    var fields = [{ name: 'id', type: 'int' },
            { name: 'name', type: 'string' },
            { name: 'content', type: 'string' },
            { name: 'win', type: 'int' },
            { name: 'lost', type: 'int' },
            { name: 'resultwin', type: 'int' },
            { name: 'resultlost', type: 'int' }];

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
        listeners: {
            datachanged: function (s) {
   
            }
        }
    });

    //--------------------------------------------------列头
    var cm = new Ext.grid.ColumnModel([
        new Ext.grid.RowNumberer(), {
            header: "预测法",
            dataIndex: "name",
            sortable: false,
            align: "middle",
            width: 100
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
        }, {
            header: "赢盘率",
            dataIndex: "win",
            sortable: false,
            align: "middle",
            width: 50,
            renderer: function (v, i, r) {
                return (r.get('win') / (r.get('win') + r.get('lost')) * 100).toFixed(2) + "%";
            }
        }, {
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
        }, {
            header: "胜负率",
            dataIndex: "resultwin",
            sortable: false,
            align: "middle",
            width: 50,
            renderer: function (v, i, r) {
                return (r.get('resultwin') / (r.get('resultwin') + r.get('resultlost')) * 100).toFixed(2) + "%";
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
        height: 250,
        //超过长度带自动滚动条
        autoScroll: true,
        border: false,
        sortable: false,
        viewConfig: {
            forceFit: true, //自动填充
            sortAscText: '正序排列',
            sortDescText: '倒序排列',
            columnsText: '显示/隐藏列'
        }
    });
    var win = new Ext.Window({
        width: 800,
        height: 480,
        layout: 'fit',
        items: [grid]
    })
    win.show();
    store.load();
}