/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />
var Odds1x2History = function (scheduleArr,scheduleTypeArr, oddeArr) {

    var fields = [
            { name: 'companyid', type: 'int' },
            { name: 'companyname', type: 'string' },
            { name: 'perwin', type: 'float' },
            { name: 'perdraw', type: 'float' },
            { name: 'perlost', type: 'float'}];
    var params = {
        stypeid: scheduleTypeArr[0],
        oddsarr:oddeArr.join('^')
    };

    var store = new Ext.data.JsonStore({
        //url: 'Data/NowGoal/GetOdds1x2History.aspx?a=stat',
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

    //    store.on("beforeload", function(store) {
    //        Ext.apply(this.baseParams, { scheduleArr[0]: scheduleArr[0], companyids: companyids.join(','), companynames: companynames.join(','), tcompanyid: "" });
    //    });

    //--------------------------------------------------列头
    var cm = new Ext.grid.ColumnModel([
        {
            header: "公司",
            dataIndex: "companyname",
            sortable: false
        },
		{
		    header: "主胜",
		    tooltip: "主场球队获胜赔率",
		    dataIndex: "perwin",
		    sortable: false,
		    width: 30
		}, {
		    header: "和局",
		    tooltip: "比赛打平的赔率",
		    dataIndex: "perdraw",
		    sortable: false,
		    width: 30
		}, {
		    header: "客胜",
		    tooltip: "客场球队获胜赔率",
		    dataIndex: "perlost",
		    sortable: false,
		    width: 30
		}
]);


    //----------------------------------------------------定义grid
    var grid = new Ext.grid.GridPanel({
        id: "Odds1x2ChangeGrid",
        store: store,
        cm: cm,
        title: '历史赔率',
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
        },
        tbar: [
        new Ext.Toolbar.Fill()
		, {
		    id: 'refreshbutton',
		    text: '刷新',
		    iconCls: "refreshicon",
		    tooltip: '刷新列表',
		    menu: []
		}, "", "-", "", {
		    text: '统计',
		    iconCls: "totalicon",
		    tooltip: '统计数据',
		    handler: function () {
		        var inithomes = 0;
		        var initdraws = 0;
		        var initaways = 0;
		        var endhomes = 0;
		        var enddraws = 0;
		        var endaways = 0;
		        var tempgroup;
		        var inithomekArr = new Array();
		        var initdrawkArr = new Array();
		        var initawaykArr = new Array();
		        var endhomekArr = new Array();
		        var enddrawkArr = new Array();
		        var endawaykArr = new Array();

		        var rr = new Array();
		        for (var i = 0; i < store.data.items.length; i++) {
		            var row = store.data.items[i].data;
		            rr.push(row.returnrate / 100);
		            if (tempgroup != row.companyid) {
		                tempgroup = row.companyid;
		                endhomekArr.push(row.homek / (row.returnrate / 100));
		                enddrawkArr.push(row.drawk / (row.returnrate / 100));
		                endawaykArr.push(row.awayk / (row.returnrate / 100));
		                if (row.homek < row.returnrate * 100)
		                    endhomes++;
		                else if (row.homek > 100)
		                    endhomes--;
		                if (row.drawk < row.returnrate * 100)
		                    enddraws++;
		                else if (row.drawk > 100)
		                    enddraws--;
		                if (row.awayk < row.returnrate * 100)
		                    endaways++;
		                else if (row.awayk > 100)
		                    endaways--;
		            }
		            if (i == store.data.items.length - 1 || tempgroup != store.data.items[i + 1].data.companyid) {
		                inithomekArr.push(row.homek / (row.returnrate / 100));
		                initdrawkArr.push(row.drawk / (row.returnrate / 100));
		                initawaykArr.push(row.awayk / (row.returnrate / 100));
		                if (row.homek < row.returnrate * 100)
		                    inithomes++;
		                else if (row.homek > 100)
		                    inithomes--;
		                if (row.drawk < row.returnrate * 100)
		                    initdraws++;
		                else if (row.drawk > 100)
		                    initdraws--;
		                if (row.awayk < row.returnrate * 100)
		                    initaways++;
		                else if (row.awayk > 100)
		                    initaways--;
		            }

		        }
		        var averr = eval(rr.join('+')) / rr.length;
		        aveinithomekelly = eval(inithomekArr.join('+')) / inithomekArr.length * averr;
		        aveinitdrawkelly = eval(initdrawkArr.join('+')) / initdrawkArr.length * averr;
		        aveinitawaykelly = eval(initawaykArr.join('+')) / initawaykArr.length * averr;

		        aveendhomekelly = eval(endhomekArr.join('+')) / endhomekArr.length * averr;
		        aveenddrawkelly = eval(enddrawkArr.join('+')) / enddrawkArr.length * averr;
		        aveendawaykelly = eval(endawaykArr.join('+')) / endawaykArr.length * averr;

		        var html = "初盘凯利 " + aveinithomekelly.toFixed(2) + " " + aveinitdrawkelly.toFixed(2) + " " + aveinitawaykelly.toFixed(2) + "<br />";
		        html += "临场凯利 " + aveendhomekelly.toFixed(2) + " " + aveenddrawkelly.toFixed(2) + " " + aveendawaykelly.toFixed(2) + "<br />";
		        html += "初盘支持 " + inithomes + " " + initdraws + " " + initaways + "<br />";
		        html += "临场支持 " + endhomes + " " + enddraws + " " + endaways + "<br />";
		        Ext.Msg.alert("统计结果", html);
		    }
		}]
    });



    var win = new Ext.Window({
        title: "历史赔率",
        width: 900,
        height: 600,
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
        modal: 'true',
        layout: "fit",
        buttonAlign: "center",
        //        bodyStyle: "padding:10px 0 0 15px",
        items: [grid
        //        {
        //            iconCls: "serchopenroomrecord",
        //            xtype: "panel",
        //            layout: "fit",
        //            items: [grid]
        //        }
        ],
        listeners: {
            "show": function () {
                //当window show事件发生时清空一下表单
                store.load();
            }
        }
    });

    win.show();
    var handleAction = function (action) {
        store.baseParams.tcompanyid = action;
        store.reload();
    };
//    var companymenu = grid.topToolbar.get('refreshbutton').menu;
//    companymenu.addItem(new Ext.menu.Item({ text: "默认算法刷新", handler: handleAction.createCallback("") }));
//    for (var i = 0; i < companyids.length; i++) {
//        var item = new Ext.menu.Item({ text: companynames[i], handler: handleAction.createCallback(companyids[i]) });
//        companymenu.addItem(item);
//    }
}