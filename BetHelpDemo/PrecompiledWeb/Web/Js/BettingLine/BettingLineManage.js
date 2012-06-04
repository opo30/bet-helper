/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />

var BettingLinestore;
var customfun = function() {
    alert("aaa");
}
var addbutton = function() {

}
BettingLineManage = function(node) {

    //指定列参数
var fields = [{ name: 'id', type: 'string' },
            { name: 'lineid', type: 'string' },
            { name: 'teamname', type: 'string' },
            { name: 'traditional', type: 'string' },
            { name: 'starttime', type: 'date', dateFormat: 'Y年m月d日' },
            { name: 'endtime', type: 'date' },
            { name: 'bettime', type: 'date' },
            { name: 'itemid', type: 'string' },
            { name: 'detailid', type: 'string' },
            { name: 'betmoney', type: 'float' },
            { name: 'returnmoney', type: 'float' },
            { name: 'result', type: 'string' },
            { name: 'sourceid', type: 'string' },
            { name: 'linename', type: 'string' },
            { name: 'linebetmoney', type: 'float' },
            { name: 'profit', type: 'float' },
            { name: 'state', type: 'string' },
            { name: 'iscomplete', type: 'string' },
            { name: 'isbetting', type: 'string' },
            { name: 'itemname', type: 'string' },
            { name: 'detailname', type: 'string'}];
            
    BettingLinestore = new Ext.data.GroupingStore({
        proxy: new Ext.data.HttpProxy(
           {
               url: "Data/BetRecord/List.aspx",
               method: "POST"
           }),
        reader: new Ext.data.JsonReader(
           {
               fields: fields,
               root: "data",
               id: "id",
               totalProperty: "totalCount"

           }),
           groupField: 'linename',
        sortInfo: { field: 'lineid', direction: "DESC" }
    });


    //--------------------------------------------------列选择模式
    var sm = new Ext.grid.CheckboxSelectionModel({
        dataIndex: "id",
        listeners: {
            // On selection change, set enabled state of the removeButton
            // which was placed into the GridPanel using the ref config
            selectionchange: function(s) {
                if (s.getCount()) {
                    if (s.getCount() == 1) {
                        grid.toolbars[0].get('delbutton').enable();
                        grid.toolbars[0].get('editbutton').enable();
                    }else{
                        grid.toolbars[0].get('delbutton').enable();
                        grid.toolbars[0].get('editbutton').disable();
                    }
                    
                } else {
                    grid.toolbars[0].get('delbutton').disable();
                    grid.toolbars[0].get('editbutton').disable();
                }
            }
        }
    });
    //--------------------------------------------------列头
    var cm = new Ext.grid.ColumnModel([
		sm, {
		    header: "球队名称",
		    dataIndex: "teamname",
		    tooltip: "注单球队名称",
		    //列不可操作
		    //menuDisabled:true,
		    //可以进行排序
		    sortable: true
		}, {
		    header: "繁体名称",
		    tooltip: "球队繁体名称",
		    dataIndex: "traditional",
		    sortable: true,
		    renderer: function(value) {
		        return "<font color='blue'>" + value + "</font>"
		    }
		}, {
		    header: "开场时间",
		    tooltip: "比赛开始时间",
		    dataIndex: "starttime",
		    sortable: true
		}, {
		    header: "完场时间",
		    tooltip: "比赛结束时间",
		    dataIndex: "endtime",
		    sortable: true
		},{
		    header: "投注项目",
		    tooltip: "投注项目",
		    dataIndex: "itemname",
		    sortable: true
		}, {
		    header: "投注细节",
		    tooltip: "投注项目",
		    dataIndex: "detailname",
		    sortable: true
		}, {
		    header: "下注金额",
		    tooltip: "下注金额",
		    dataIndex: "betmoney",
		    type: 'float',
		    sortable: true,
		    renderer: function(value) {   //将数字转换为整数
		        return '<span style="color:red;"><b>' + String.format("<font color=red>￥{0}</font>", value) + '</b>&nbsp;元</span>';
		    },
		    summaryType: 'sum'
		}, {
		    header: "返还金额",
		    tooltip: "退回金额",
		    dataIndex: "returnmoney",
		    type: 'float',
		    sortable: true,
		    renderer: function(value) {   //将数字转换为整数
		        return '<span style="color:red;"><b>' + String.format("<font color=red>￥{0}</font>", value) + '</b>&nbsp;元</span>';
		    },
		    summaryType: 'sum'
		}, {
		    header: "结果",
		    tooltip: "投注盘路胜负",
		    dataIndex: "result",
		    sortable: true,
		    summaryType: 'button'
        },{
		    header: "投资线程",
		    dataIndex: "linename",
		    sortable: false,
		    hidden: true
		}]);



//定义组别摘要的方法
Ext.ux.grid.GroupSummary.Calculations['totalBetMoney'] = function(v, record, field) {
    return v + (record.data.estimate * record.data.rate);
};

// utilize custom extension for Group Summary
var summary = new Ext.ux.grid.GroupSummary();


    //----------------------------------------------------定义grid
    var grid = new Ext.grid.GridPanel({
        id: "BettingLineGrid",
        store: BettingLinestore,
        sm: sm,
        cm: cm,
        loadMask: true,
        stripeRows: true,
        autoWidth: true,
        //超过长度带自动滚动条
        autoScroll: true,
        border: false,
        view: new Ext.grid.GroupingView({
            //自动填充
            forceFit: true,
            sortAscText: '正序排列',
            sortDescText: '倒序排列',
            columnsText: '列显示/隐藏',
            groupByText: '根据本列分组',
            showGroupsText: '是否采用分组显示',
            groupTextTpl: '{text} (<b><font color=red>{[values.rs.length]}</font> </b>{[values.rs.length > 0 ? "条投注信息" : "暂无投注信息"]})   {[values.rs[0].data["isbetting"] == "1" ? "已投注" : "未投注"]}'
        }),
        plugins: [summary],
        tbar: [
		new Ext.Toolbar.Fill()
		,{
            text: '切换',
            iconCls: "totalicon",
            tooltip: '切换显示/隐藏摘要信息',
            handler: function(){summary.toggleSummaries();}
        }, {
            text: '刷新',
            iconCls: "refreshicon",
            tooltip: '刷新列表',
            handler: function() { BettingLinestore.reload(); }
        }, "", "-", "", {
            id: "addbutton",
		    text: "新建",
		    //默认样式为按下
		    //pressed:true,
		    tooltip: "新建投注记录",
		    iconCls: "addicon",
		    handler: AddBettingLineFn
		}, "", "-", "", {
		    id: "editbutton",
		    text: "编辑",
		    tooltip: "编辑当前选中的投注记录",
		    iconCls: "editicon",
		    handler: customfun,
		    disabled: true
		}, "", "-", "", {
		    id: "delbutton",
		    text: "删除",
		    tooltip: "删除选中的投注记录",
		    iconCls: "deleteicon",
		    handler: customfun,
		    disabled: true
		}, "-"], listeners: {
		    'contextmenu': function(e) {
		        e.stopEvent();
		    },
		    "rowcontextmenu": function(grid, rowIndex, e) {
		        e.stopEvent();
		    }
		}
    });


    //传入icon样式
    //GridMain(node, grid, "openroomiconinfo");
}