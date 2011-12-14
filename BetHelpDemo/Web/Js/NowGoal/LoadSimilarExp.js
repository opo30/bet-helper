/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />

var LoadSimilarExp = function (title, data, spot) {
    var pageSize = 10;
    //指定列参数
    var fields = [{ name: 'id', type: 'string'}];

    var store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy(
           {
               url: "Data/NowGoal/BetExp.aspx?a=similarlist",
               method: "POST"
           }),
        reader: new Ext.data.JsonReader(
           {
               fields: fields,
               root: "data",
               id: "id"
           }),
        baseParams: {
            data: data, spot: spot
        }
    });

    //--------------------------------------------------列头
    var cm = new Ext.grid.ColumnModel([
		{
		    header: "编号",
		    dataIndex: "id",
		    sortable: false,
		    align: "middle",
		    width: 50
		}
]);

    //----------------------------------------------------定义grid
    var grid = new Ext.grid.GridPanel({
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
        viewConfig: {
            //自动填充
            emptyText: '没有记录',
            forceFit: true,
            getRowClass: function (record, rowIndex, rowParams, store) {
            }
        },
        listeners: {
            rowclick: function (o, i, e) {
                //chartStore.loadData(Ext.decode(store.getAt(i).json.data));
                // showDetailWindow(store.getAt(i).json.id, "比分", "showgoallist")
                AverageKellyLineChart(store.getAt(i).json.id, 'avekelly')
            }
        }
    });

    var win = new Ext.Window({
        layout: "fit",
        items: [grid],
        width: 150
    });

    win.show();
    store.load();
}
