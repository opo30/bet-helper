/// <reference path="../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../lib/ext/ext-all-debug.js" />

//关闭TabPanel标签
Ext.ux.TabCloseMenu = function() {
    var tabs, menu, ctxItem;
    this.init = function(tp) {
        tabs = tp;
        tabs.on('contextmenu', onContextMenu);
    }
    function onContextMenu(ts, item, me) {
        if (!menu) { // create context menu on first right click
            menu = new Ext.menu.Menu([{
                id: tabs.id + '-close',
                text: '关闭当前标签',
                iconCls: "closetabone",
                handler: function() {
                    tabs.remove(ctxItem);
                }
            }, {
                id: tabs.id + '-close-others',
                text: '除此之外全部关闭',
                iconCls: "closetaball",
                handler: function() {
                    tabs.items.each(function(item) {
                        if (item.closable && item != ctxItem) {
                            tabs.remove(item);
                        }
                    });
                }
}]);
            }
            ctxItem = item;
            var items = menu.items;
            items.get(tabs.id + '-close').setDisabled(!item.closable);
            var disableOthers = true;
            tabs.items.each(function() {
                if (this != item && this.closable) {
                    disableOthers = false;
                    return false;
                }
            });
            items.get(tabs.id + '-close-others').setDisabled(disableOthers);
            menu.showAt(me.getXY());
        }
    };


