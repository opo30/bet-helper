/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />

var betrecordroot = new Ext.tree.AsyncTreeNode({
    id: "0",
    text: "追杀投注",
    loader: new Ext.tree.TreeLoader({
        url: "Data/BetRecord/List.aspx",
        listeners: {
            "beforeload": function(treeloader, node) {
                treeloader.baseParams = {
                    id: node.id,
                    method: 'POST'
                };
            }
        },
        uiProviders: {
            'col': Ext.ux.tree.ColumnNodeUI
        }
    })
});


var betrecord = new Ext.ux.tree.ColumnTree({
    width: 800,
    height: 300,
    rootVisible: false,
    autoScroll: true,
    title: 'Example Tasks',
    root: betrecordroot,
    columns: [{
        header: 'Task',
        width: 330,
        dataIndex: 'task'
    }, {
        header: 'Duration',
        width: 100,
        dataIndex: 'duration'
    }, {
        header: 'Assigned To',
        width: 100,
        dataIndex: 'user'
    }]
});