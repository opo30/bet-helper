var OddsCompareList = {
    id:"OddsCompareListWin",
    init:function(){
        var fields = [{ name: 'companyid', type: 'string' }, { name: 'fullname', type: 'string' }, { name: 'isprimary', type: 'bool' }, { name: 'isexchange', type: 'bool' }, { name: 'scount', type: 'int' }, { name: 'swin', type: 'float' }, { name: 'sdraw', type: 'float' }, { name: 'slost', type: 'float' }, { name: 'type', type: 'int' }, { name: 'time', type: 'date' }];

        var store = new Ext.data.GroupingStore({
            reader: new Ext.data.JsonReader(
                {
                    fields: fields
                }),
            data: [],
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
                header: "平",
                dataIndex: "sdraw",
                sortable: true,
                align: "middle",
                width: 50,
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
                header: "负",
                dataIndex: "slost",
                sortable: true,
                align: "middle",
                width: 50,
                summaryType: 'average',
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
                    return value.format("Y-m-d H:i");
                }
            }
        ]);
        var summary = new Ext.ux.grid.GroupSummary();

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
            })
        });

        win = new Ext.Window({
            id: this.id,
            layout: 'fit',
            closeAction: 'hide',
            title: "赔率比较",
            width: 960,
            height: 500,
            resizeable: true,
            autoScroll: true,
            items: [grid]
        });
    },
    show:function(){
        Ext.getCmp(this.id).show();
    },
    add:function(){
        var grid = Ext.getCmp(this.id).findByType('grid')[0];
        var Compare = grid.getStore().recordType;
        var c = new Compare({
            companyid: 1,
            type: 1,
            fullname: "1211221",
            time: (new Date()).clearTime()
        });
        grid.getStore().add(c);
    }
};

//OddsCompareList.init();
//OddsCompareList.add();
//OddsCompareList.show();