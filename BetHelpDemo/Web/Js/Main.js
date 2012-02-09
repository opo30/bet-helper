/// <reference path="../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../lib/ext/ext-all-debug.js" />

var loadMask = new Ext.LoadMask(document.body, {
    msg: '正在读取数据，请稍候...',
    removeMask: true
});

//加载中样式
var hideMask = function() {
    Ext.get('loading').remove();
    Ext.fly('loading-mask').fadeOut({
        remove: true
    });
    loadMask.show();
    LoadLiveFile();
}

//--设置一些共有参数
Ext.chart.Chart.CHART_URL = 'Lib/ext/resources/charts.swf';
Ext.BLANK_IMAGE_URL = "Images/s.gif";
Ext.QuickTips.init();

//覆盖方法。。。因为报错 原因不明
Ext.override(Ext.chart.Chart, {
    onDestroy: function() {
        Ext.chart.Chart.superclass.onDestroy.call(this);
        this.bindStore(null);
        var tip = this.tipFnName;
    }
});

function delHtmlTag(str) {
    return str.replace(/<[^>]+>/g, ""); //去掉所有的html标记 
} 

//添加左边
var west = new Ext.Panel({
    //自动收缩按钮
    collapsible: true,
    collapsed: false, //默认展开还是收缩
    border: false,
    width: 150,
    layout: "accordion",
    extraCls: "roomtypegridbbar",
    //添加动画效果
    layoutConfig: {
        animate: true
    },
    region: "west",
    title: '菠菜辅助',
    //
    items: [{
        title: "<b>博彩</b>",
        autoScroll: true,
        iconCls: "hotelmanageicon",
        xtype: 'panel',
        layout: 'fit',
        items: [{
            xtype: 'treepanel',
            width: 200,
            autoScroll: true,
            split: true,
            loader: new Ext.tree.TreeLoader(),
            root: new Ext.tree.AsyncTreeNode({
                expanded: true,
                children: [{
                    text: '即时比分',
                    leaf: true
                }, {
                    text: '历史数据',
                    leaf: true
                }, {
                    text: '经验总结',
                    leaf: true
                }]
                }),
                rootVisible: false,
                listeners: {
                    click: function(node) {
                        switch (node.attributes.text) {
                            case "历史数据":
                                LoadHistoryMatch(node);
                                break;
                            case "经验总结":
                                LoadBetExperience(node);
                                break;
                            default:
                                break;
                        }
                    }
                }
}]
        }, {
            title: "<b>更新</b>",
            autoScroll: true,
            iconCls: "update-icon",
            xtype: 'panel',
            layout: 'fit',
            items: [{
                xtype: 'treepanel',
                width: 200,
                autoScroll: true,
                split: true,
                loader: new Ext.tree.TreeLoader(),
                root: new Ext.tree.AsyncTreeNode({
                    expanded: true,
                    children: [{
                        text: '比赛数据',
                        leaf: true,
                        listeners: {
                            click: function (node) {
                                LoadMatchUpdate(node);
                            }
                        }
                    }, {
                        text: '赔率数据',
                        leaf: true,
                        listeners: {
                            click: function (node) {
                                LoadOddsUpdateRQ(node);
                            }
                        }
                    }]
                }),
                rootVisible: false
            }]
        }]

            });


    ////-------------------------------------------------中间

            var center = new Ext.TabPanel({
                id: 'centertab',
                //距两边间距
                style: "padding:0 5px 0 5px",
                region: "center",
                //默认选中第一个
                activeItem: 0,
                //如果Tab过多会出现滚动条
                enableTabScroll: true,
                //加载时渲染所有
                //deferredRender:false,
                layoutOnTabChange: true,
                items: [{
                    xtype: "panel",
                    id: "livetab",
                    iconCls: "usericon",
                    title: "即时比分",
                    layout: "fit",
                    contentEl: "live",
                    autoScroll: true,
                    tbar: [{ text: 'dads' }, { xtype: 'tbfill' }]
                }], 
                plugins: new Ext.ux.TabCloseMenu()
            });

 var south = new Ext.ux.StatusBar({
     id: 'statusbar',
     //距两边间距
     style: "padding:0 5px 0 5px",
     region: "south",
     defaultText: 'Default status text',
     //defaultIconCls: 'default-icon',

     // values to set initially:
     text: 'Ready',
     iconCls: 'x-status-valid',
     items:[new Ext.Toolbar.Fill()]
 });

//        var mainTabChange = function(tabpanel, activetab) {
//            if (activetab.items != undefined && activetab.items.length > 0) {
//                DeptMemberListstore = activetab.items.items[0].store; //赋值全局store
//                ActiveCenterGrid = activetab.items.items[0];
//            }
//        }

//        center.on('tabchange', mainTabChange, this); //定义主tab切换事件


    var company = new Array(40);
    company[1] = "澳门";
    company[3] = "ＳＢ";
    company[4] = "立博";
    company[7] = "SNAI";
    company[8] = "Bet365";
    company[9] = "威廉希尔";
    company[12] = "易胜博";
    company[14] = "韦德";
    company[17] = "明陞";
    company[18] = "Eurobet";
    company[19] = "Interwetten";
    company[22] = "10Bet";
    company[23] = "金宝博";
    company[24] = "12bet";
    company[29] = "乐天堂";
    company[31] = "利记";
    company[33] = "永利高";
    company[35] = "盈禾";

        //////////////////////////////////////////////////////////
    Ext.onReady(function () {
        hideMask.defer(250);
        var vp = new Ext.Viewport({
            layout: "border",
            items: [west, center, south]
        });
    });

    var showInfoNotify = function (title, content) {
        new Ext.ux.Notification({
            animateTarget: Ext.getCmp("statusbar").getEl(),
            animateFrom: Ext.getCmp("statusbar").getPosition(),
            autoDestroy: true,
            hideDelay: 5000,
            html: content,
            iconCls: 'x-icon-information',
            title: title,
            listeners: {
                'beforerender': function () {
                }
            }
        }).show();
    }

    var showErrorNotify = function (title,content) {
        new Ext.ux.Notification({
            animateTarget: Ext.getCmp("statusbar").getEl(),
            animateFrom: Ext.getCmp("statusbar").getPosition(),
            autoDestroy: true,
            hideDelay: 5000,
            html: content,
            iconCls: 'x-icon-error',
            title: "错误",
            listeners: {
                'beforerender': function () {
                }
            }
        }).show();
    }


    var analysis = function (scheduleid) {
        Ext.Ajax.request({
            url: "Data/NowGoal/GetRemoteFile.aspx?f=oddsjs&path=" + scheduleid + ".js",
            method: 'POST',
            success: function (res) {
                if (res.responseText != "") {
                    var arrayData = [];
                    eval(res.responseText);
                    Ext.Ajax.request({
                        url: 'Data/NowGoal/GetOdds1x2History.aspx?a=update',
                        params: { scheduleid: scheduleid, odds: game.join('^') },
                        success: function (res) {
                            if (res.responseText) {
                                showInfoNotify("提示", hometeam_cn + "-" + guestteam_cn + "[" + matchname_cn + "]<br>" + res.responseText);
                            }
                        }
                    });
                }
                else {
                    Ext.Msg.alert("提示", "请求数据失败！");
                }
            }
        });
    }
        
		