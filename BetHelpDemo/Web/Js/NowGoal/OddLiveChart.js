/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />


var LiveChartManage = function(scheduleID, companyID) {

    var store = new Ext.data.JsonStore({
        fields: ['name', 'visits', 'views'],
        data: [
                { name: 'Jul 07', visits: 245000, views: 3000000 },
                { name: 'Aug 07', visits: 240000, views: 3500000 },
                { name: 'Sep 07', visits: 355000, views: 4000000 },
                { name: 'Oct 07', visits: 375000, views: 4200000 },
                { name: 'Nov 07', visits: 490000, views: 4500000 },
                { name: 'Dec 07', visits: 495000, views: 5800000 },
                { name: 'Jan 08', visits: 520000, views: 6000000 },
                { name: 'Feb 08', visits: 620000, views: 7500000 }
            ]
    });



    var tab = center.getItem("ChartTab");
    if (!tab) {
        var tab = center.add({
            id: "ChartTab",
            iconCls: "totalicon",
            xtype: "panel",
            title: "盘赔动态",
            closable: true,
            layout: "fit",
            items: [{
                xtype: 'linechart',
                store: store,
                xField: 'name',
                yField: 'visits',
                listeners: {
                    itemclick: function(o) {
                        var rec = store.getAt(o.index);
                        Ext.example.msg('Item Selected', 'You chose {0}.', rec.get('name'));
                    }
                }
            }]

            });
        }
        center.setActiveTab(tab);
    }

