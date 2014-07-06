var OddsCompareList = {
    id:"OddsCompareListWin",
    init:function(){
        var fields = [{ name: 'id', type: 'int' }, { name: 'fullname', type: 'string' }, { name: 'score', type: 'string' }, { name: 'o1', type: 'float' }, { name: 'o2', type: 'float' }, { name: 'o3', type: 'float' }, { name: 's1', type: 'int' }, { name: 's2', type: 'int' }, { name: 'time', type: 'date' }];

        var store = new Ext.data.GroupingStore({
            reader: new Ext.data.JsonReader(
                {
                    fields: fields
                }),
            data: [],
            groupField: 'id',
            groupOnSort: true,
            sortInfo: { field: "time", direction: "ASC" }
        });

        //--------------------------------------------------列头
        var cm = new Ext.grid.ColumnModel([
            {
                header: "编号",
                dataIndex: "id",
                hidden: true,
            }, {
                header: "比赛",
                dataIndex: "fullname",
                hidden: true
            }, {
                header: "比分",
                dataIndex: "score",
                sortable: true,
                align: "middle",
                width: 20,
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
                header: "主",
                dataIndex: "o1",
                sortable: true,
                align: "middle",
                width: 20,
                summaryType: 'average',
                renderer: function (value, last, row) {
                    return value;
                }
            }, {
                header: "盘口",
                dataIndex: "o2",
                sortable: true,
                align: "middle",
                width: 20,
                summaryType: 'average',
                renderer: function (value, cell, row, rowIndex, colIndex, ds) {
                    if (rowIndex != null) {
                        if (value > row.get("sdraw") && value > row.get("slost")) {
                            cell.cellAttr = 'bgcolor="#F7CFD6"';
                        }
                        else if (value < row.get("sdraw") && value < row.get("slost")) {
                            cell.cellAttr = 'bgcolor="#DFF3B1"';
                        }
                    }
                    return value;
                }
            }, {
                header: "客",
                dataIndex: "o3",
                sortable: true,
                align: "middle",
                width: 20,
                summaryType: 'average',
                renderer: function (value, cell, row, rowIndex, colIndex, ds) {
                    if (rowIndex != null) {
                        if (value > row.get("swin") && value > row.get("slost")) {
                            cell.cellAttr = 'bgcolor="#F7CFD6"';
                        }
                        else if (value < row.get("swin") && value < row.get("slost")) {
                            cell.cellAttr = 'bgcolor="#DFF3B1"';
                        }
                    }
                    return value;
                }
            }, {
                header: "赢盘",
                dataIndex: "s1",
                sortable: true,
                align: "middle",
                width: 20,
                summaryType: 'sum',
                renderer: function (value, cell, row, rowIndex, colIndex, ds) {
                    if (rowIndex != null) {
                        if (value > row.get("swin") && value > row.get("sdraw")) {
                            cell.cellAttr = 'bgcolor="#F7CFD6"';
                        }
                        else if (value < row.get("swin") && value < row.get("sdraw")) {
                            cell.cellAttr = 'bgcolor="#DFF3B1"';
                        }
                    }
                    return value;
                }
            }, {
                header: "输盘",
                dataIndex: "s2",
                sortable: true,
                align: "middle",
                width: 20,
                summaryType: 'sum',
                renderer: function (value, cell, row, rowIndex, colIndex, ds) {
                    if (rowIndex != null) {
                        if (value > row.get("swin") && value > row.get("sdraw")) {
                            cell.cellAttr = 'bgcolor="#F7CFD6"';
                        }
                        else if (value < row.get("swin") && value < row.get("sdraw")) {
                            cell.cellAttr = 'bgcolor="#DFF3B1"';
                        }
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
                    return value.format("Y-m-d H:i:s");
                }
            }
        ]);
        var summary = new Ext.ux.grid.GroupSummary();

        //----------------------------------------------------定义grid
        var grid = new Ext.grid.GridPanel({
            store: store,
            cm: cm,
            sm: new Ext.grid.CheckboxSelectionModel({
                singleSelect: false
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
                groupTextTpl: '{[values.rs[0].data["fullname"]]} (<b><font color=red>{[values.rs.length]}</font> </b>{[values.rs.length > 0 ? "条" : "暂无历史记录"]})'
            }),
            tbar: [{
                text: '删除',
                handler: function () {
                    var rows = grid.getSelectionModel().getSelections();
                    Ext.each(rows,function (row) {
                        store.remove(row);
                    })
                    
                }
            },{
                text: '清除',
                handler: function () {
                    store.removeAll();
                }
            }]
        });

        win = new Ext.Window({
            id: this.id,
            layout: 'fit',
            closeAction: 'hide',
            title: "赔率比较",
            width: 640,
            height: 500,
            resizeable: true,
            autoScroll: true,
            items: [grid]
        });
    },
    show:function(){
        Ext.getCmp(this.id).show();
    },
    add:function(obj){
        var grid = Ext.getCmp(this.id).findByType('grid')[0];
        var Compare = grid.getStore().recordType;
        var c = new Compare(obj);
        grid.getStore().add(c);
        grid.getStore().groupBy("id", true);
    },
    getCount: function (id) {
        var grid = Ext.getCmp(this.id).findByType('grid')[0];
        var c = 0;
        grid.getStore().each(function (row) {
            if (id == row.get("id")) {
                c++;
            }
        });
        return c;
    }
};

OddsCompareList.init();
//OddsCompareList.add();
//OddsCompareList.show();