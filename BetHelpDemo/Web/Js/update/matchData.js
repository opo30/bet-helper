/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />

var LoadMatchUpdate = function (node) {
    var companyid_array = [];
    for (var i in company) {
        if (typeof (company[i]) == "string") {
            companyid_array.push(i);
        }
    }

    var pageSize = 20;
    //指定列参数
    var fields = [{ name: 'id', type: 'int' },
            { name: 'data', type: 'string' },
            { name: 'date', type: 'date' },
            { name: 'updated', type: 'bool' },
            { name: 'scheduletype', type: 'string'}];

    var store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy(
           {
               url: "Data/NowGoal/schedule.aspx?a=list",
               method: "POST"
           }),
        reader: new Ext.data.JsonReader(
           {
               fields: fields,
               root: "data",
               id: "id",
               totalProperty: "totalCount"
           }),
        baseParams: {
            start: 0, limit: pageSize, where: ''
        }
    });

    store.on('load', function (s, records) {
        s.each(function (r, index) {
            grid.getView().getCell(index, 2).style.backgroundColor = r.get('scheduletype').split(',')[4];
            grid.getView().getCell(index, 2).style.color = "white";
            //grid.getView().getRow(gridcount).style.display = (s.sclass[r.get('match_1')][5] == 1) ? "" : "none";
        });
    });

    var sm = new Ext.grid.CheckboxSelectionModel({
        dataIndex: "id",
        singleSelect: false
    });

    //--------------------------------------------------列头
    var cm = new Ext.grid.ColumnModel([
    sm,
        {
            header: "编码",
            dataIndex: "id",
            width: 10,
            sortable: false
        }, {
            header: "赛事",
            dataIndex: "scheduletype",
            width: 10,
            sortable: false,
            renderer: function (value) {
                return value.split(',')[1];
            }
        }, {
            header: "比赛数据",
            dataIndex: "data",
            sortable: false,
            renderer: function (value) {
                var arr = value.split(',')
                return arr[4] + " - " + arr[7] + "  全场 " + arr[13] + " - " + arr[14] + "  半场 " + arr[15] + " - " + arr[16];
            }
        }, {
            header: "时间",
            dataIndex: "date",
            width: 10,
            sortable: false,
            renderer: function (value) {
                return value ? value.dateFormat('Y-m-d') : '';
            }
        }, {
            header: "数据",
            dataIndex: "updated",
            sortable: false,
            width: 10,
            renderer: function (value) {
                if (value) {
                    return "<img src='Images/icon-01.gif' width='20' alt='有数据' title='有数据' />";
                }
                else {
                    return "<img src='Images/icon-02.gif' width='20' alt='无数据' title='无数据' />";
                }
            }
        }
]);

    //定义一个搜索栏
    var query_textfield = new Ext.form.TextField({
        id: "query_textfield",
        fieldLabel: '快捷检索',
        emptyText: '请输入关键字...',
        blankText: '请输入关键字（汉字/拼音/首字母）...',
        maxLength: 10,
        listeners: {
            'render': {
                fn: function () {
                    Ext.getCmp("query_textfield").getEl().on('keyup', function () {
                        store.baseParams.where = Ext.getCmp("query_textfield").getEl().dom.value;
                        store.load(); // 重载当前tab列参数
                    }, this, { buffer: 500 })
                }, scope: this
            }
        }
    });

    //----------------------------------------------------定义grid
    var grid = new Ext.grid.GridPanel({
        store: store,
        cm: cm,
        sm: sm,
        columnWidth: 1,
        loadMask: true,
        stripeRows: true,
        columnLines: true,
        //超过长度带自动滚动条
        autoScroll: true,
        border: false,
        sortable: false,
        viewConfig: {
            //自动填充
            emptyText: '没有记录',
            forceFit: true,
            sortAscText: '正序排列',
            sortDescText: '倒序排列',
            columnsText: '显示/隐藏列',
            getRowClass: function (record, rowIndex, rowParams, store) {
            }
        },
        tbar: [new Ext.Toolbar.Fill(), query_textfield, "-",
            {
                text: "更新比赛",
                handler: function () {
                    var start = new Ext.form.DateField({
                        id: 'startfield',
                        width: 150,
                        format: 'Y-m-d',
                        value: new Date().add(Date.DAY, -1),
                        fieldLabel: "开始",
                        emptyText: '请选择日期',
                        allowBlank: false
                    });
                    var end = new Ext.form.DateField({
                        id: 'endfield',
                        width: 150,
                        format: 'Y-m-d',
                        value: new Date().add(Date.DAY, -1),
                        fieldLabel: "结束",
                        emptyText: '请选择日期',
                        allowBlank: false
                    });
                    var pbar = new Ext.ProgressBar({
                        text: '初始化...'
                    });
                    var win = new Ext.Window({
                        width: 250,
                        height: 200,
                        layout: 'fit',
                        modal: true,
                        title: '更新比赛数据',
                        closeAction: 'destroy',
                        items: [{
                            xtype: 'form',
                            labelWidth: 30,
                            frame: true,
                            items: [start, end, { id: 'pbar_text', style: 'color:#555;' }, pbar]
                        }],
                        buttons: [{
                            id: 'update-btn',
                            text: '更新',
                            handler: function () {
                                var fp = win.findByType("form")[0];
                                if (fp.getForm().isValid()) {
                                    this.el.dom.disabled = true;
                                    var stime = start.getValue();
                                    var etime = end.getValue();
                                    var count = (etime - stime) / 1000 / 3600 / 24 + 1;
                                    var i = 0;

                                    var loadDateFile = function (dateString) {
                                        LoadHistoryFile(dateString, function (res) {
                                            var ShowBf = function () {
                                                Ext.Ajax.request({
                                                    url: 'Server.aspx?a=updateSchedules',
                                                    timeout: 3600000,
                                                    params: {
                                                        date: dateString,
                                                        schedulelist: Ext.util.Format.htmlEncode(A.join('^')),
                                                        scheduletypelist: Ext.util.Format.htmlEncode(B.join('^'))
                                                    },
                                                    success: function (res) {
                                                        var result = Ext.decode(res.responseText);
                                                        if (result.success) {
                                                            i++;
                                                            var per = i / count;
                                                            pbar.updateProgress(per, Math.round(100 * per) + '% 完成...');
                                                            if (i >= count) {
                                                                Ext.fly('pbar_text').update('更新完成！');
                                                                Ext.fly('update-btn').dom.disabled = false;
                                                            } else {
                                                                var tempdate = stime.add(Date.DAY, i).dateFormat('Y-m-d');
                                                                Ext.fly('pbar_text').update('正在更新' + tempdate);
                                                                loadDateFile(tempdate);
                                                            }
                                                        } else {
                                                            Ext.fly('pbar_text').update('出现错误：' + result.message);
                                                            Ext.fly('update-btn').dom.disabled = false;
                                                        }
                                                    }
                                                });
                                            }
                                            if (res.responseText != "") {
                                                eval(res.responseText);
                                            }
                                            else {
                                                Ext.Msg.alert("提示", "请求数据失败！");
                                            }
                                        });
                                    }
                                    Ext.fly('pbar_text').update('正在更新' + start.value);
                                    loadDateFile(start.value);
                                }
                            }
                        }, {
                            text: '关闭',
                            handler: function () {
                                win.destroy();
                            }
                        }]
                    });
                    win.show();
                }
            }, "-", {
                text: '更新赔率',
                handler: function () {
                    var rowlist = grid.getSelectionModel().getSelections();
                    var isstop = false;
                    var row = new Array();
                    for (var i = 0; i < rowlist.length; i++) {
                        if (!rowlist[i].get('updated')) {
                            row.push(rowlist[i]);
                        }
                    }

                    if (row.length == 0) {
                        Ext.Msg.alert("错误提示", "您没有选中任何没有数据的比赛!");
                    }
                    else {
                        var pbar1 = new Ext.ProgressBar({
                            text: '初始化...'
                        });
                        var pbar2 = new Ext.ProgressBar({
                            text: '初始化...'
                        });
                        var win = new Ext.Window({
                            width: 550,
                            height: 200,
                            layout: 'form',
                            modal: true,
                            title: '更新赔率数据',
                            bodyStyle: 'padding:5px 5px 0',
                            closeAction: 'destroy',
                            items: [{ xtype: 'label', text: '总体进度', style: 'padding:5px 5px 0' }, { id: 'pbar1_text', style: 'color:#555;' }, pbar1, { xtype: 'label', text: '赔率进度', style: 'padding:5px 5px 0' }, { id: 'pbar2_text', style: 'color:#555;' }, pbar2],
                            buttons: [{
                                id: 'update-btn',
                                text: '更新',
                                handler: function () {
                                    isstop = false;
                                    this.el.dom.disabled = true;
                                    var companyIndex = 0;
                                    var scheduleIndex = 0;

                                    var updateOdds = function (item, companyid) {
                                        var dataArray = item.get('data').split(',');
                                        var gamename = dataArray[4] + "-" + dataArray[7];
                                        var gametime = dataArray[10].replace("<br>", " ");
                                        Ext.fly('pbar1_text').update('正在更新 ' + gamename + ' ' + gametime + '...');
                                        Ext.fly('pbar2_text').update('正在更新 ' + company[companyid] + ' 的数据');
                                        Ext.Ajax.request({
                                            url: 'Server.aspx?a=updateOdds',
                                            params: { scheduleid: item.get('id'), companyid: companyid },
                                            timeout: 600 * 1000,
                                            success: function (res) {
                                                var result = Ext.decode(res.responseText);
                                                if (result.success) {
                                                    companyIndex++;
                                                    var per = companyIndex / companyid_array.length;
                                                    pbar2.updateProgress(per, Math.round(100 * per) + '% 完成...');
                                                    if (companyIndex >= companyid_array.length) {
                                                        companyIndex = 0;
                                                        scheduleIndex++;
                                                        Ext.Ajax.request({
                                                            url: 'Server.aspx?a=changeScheduleUpdated',
                                                            params: { scheduleid: item.get('id') }
                                                        });
                                                    }
                                                    per = scheduleIndex / row.length;
                                                    pbar1.updateProgress(per, Math.round(100 * per) + '% 完成...');
                                                    if (isstop) {
                                                        store.reload();
                                                    }
                                                    else if (scheduleIndex >= row.length) {
                                                        Ext.fly('pbar1_text').update('更新完成');
                                                        Ext.fly('pbar2_text').update('更新完成');
                                                        store.reload();
                                                    } else {
                                                        updateOdds(row[scheduleIndex], companyid_array[companyIndex]);
                                                    }
                                                } else {
                                                    Ext.fly('pbar1_text').update('出现错误：' + result.message);
                                                    Ext.fly('update-btn').dom.disabled = false;
                                                }
                                            }
                                        });
                                    }
                                    updateOdds(row[scheduleIndex], companyid_array[companyIndex]);
                                }
                            }, {
                                text: '停止',
                                handler: function () {
                                    isstop = true;
                                }
                            }]
                        });
                        win.show();
                    }
                }
            }, "-", {
                text: '更新全部赔率',
                handler: function () {
                    var scheduleid_arr = [];
                    var isstop = false;
                    var scheduleIndex = 0;
                    var pbar = new Ext.ProgressBar({
                        text: '初始化...'
                    });

                    var win = new Ext.Window({
                        width: 550,
                        height: 200,
                        layout: 'form',
                        modal: true,
                        title: '更新赔率数据',
                        bodyStyle: 'padding:5px 5px 0',
                        closeAction: 'destroy',
                        items: [{ xtype: 'label', text: '总体进度', style: 'padding:5px 5px 0' }, { id: 'pbar_text', style: 'color:#555;' }, { id: 'pbar_text1', style: 'color:#555;' }, pbar],
                        buttons: [{
                            id: 'update-btn',
                            text: '更新',
                            handler: function () {
                                this.disable();
                                isstop = false;
                                var stime = new Date();
                                var updateOdds = function (scheduleid) {
                                    Ext.fly('pbar_text').update('正在更新 ' + scheduleid + '  已更新' + scheduleIndex + "/" + scheduleid_arr.length + '条...');
                                    if (scheduleIndex % 10 == 0) {
                                        var needtime = (new Date() - stime) / scheduleIndex * (scheduleid_arr.length - scheduleIndex) / 1000 / 3600;
                                        Ext.fly('pbar_text1').update('预计剩余' + Math.floor(needtime) + '小时' + Math.floor(needtime % 1 * 60) + '分钟...');

                                    }
                                    Ext.Ajax.request({
                                        url: 'Server.aspx?a=updateOdds',
                                        params: { scheduleid: scheduleid },
                                        success: function (res) {
                                            var result = Ext.decode(res.responseText);
                                            if (result.success) {
                                                scheduleIndex++;
                                                per = scheduleIndex / scheduleid_arr.length;


                                                pbar.updateProgress(per, Math.round(100 * per) + '% 完成...');
                                                if (isstop) {
                                                    Ext.getCmp("update-btn").enable();
                                                    store.reload();
                                                }
                                                else if (scheduleIndex >= scheduleid_arr.length) {
                                                    Ext.fly('pbar_text').update('更新完成');
                                                    store.reload();
                                                } else {
                                                    updateOdds(scheduleid_arr[scheduleIndex]);
                                                }
                                            } else {
                                                Ext.fly('pbar_text').update(scheduleid + '出现错误：' + result.message);
                                                Ext.getCmp("update-btn").enable();
                                            }
                                        }
                                    });
                                }
                                updateOdds(scheduleid_arr[scheduleIndex]);
                            }
                        }, {
                            text: '停止',
                            handler: function () {
                                isstop = true;
                            }
                        }]
                    });

                    Ext.Ajax.request({
                        url: 'Server.aspx?a=getNoUpdatedScheduleID',
                        success: function (res) {
                            var result = Ext.decode(res.responseText);
                            if (result.success) {
                                scheduleid_arr = result.data.split(',');
                                win.show();
                            }
                        }
                    });
                }
            }],
        //分页
        bbar: new Ext.PagingToolbar({
            store: store,
            pageSize: pageSize,
            //显示右下角信息
            displayInfo: true,
            displayMsg: '当前记录 {0} -- {1} 条 共 {2} 条记录',
            emptyMsg: "没有记录",
            prevText: "上一页",
            nextText: "下一页",
            refreshText: "刷新",
            lastText: "最后页",
            firstText: "第一页",
            beforePageText: "当前页",
            afterPageText: "共{0}页"

        })
    });

    var tab = center.getItem("ScheduleDataTab");
    if (!tab) {
        var tab = center.add({
            id: "ScheduleDataTab",
            iconCls: "totalicon",
            xtype: "panel",
            title: node.text,
            closable: true,
            layout: "fit",
            items: [grid]
        });
        center.setActiveTab(tab);
        store.load();
    }

    var Runner = function () {
        var f = function (v, pbar, btn, count, cb) {
            return function () {
                if (v > count) {
                    btn.dom.disabled = false;
                    cb();
                } else {
                    var i = v / count;
                    pbar.updateProgress(i, Math.round(100 * i) + '% 完成...');
                }
            };
        };
        return {
            run: function (pbar, btn, count, cb) {
                btn.dom.disabled = true;
                var ms = 5000 / count;
                for (var i = 1; i < (count + 2); i++) {
                    setTimeout(f(i, pbar, btn, count, cb), i * ms);
                }
            }
        }
    } ();
}

var LoadOddsUpdate = function (node) {

}

var LoadHistoryFile = function (date,func) {
    Ext.Ajax.request({
        url: 'Data/NowGoal/LoadHistoryFile.aspx',
        method: 'POST',
        params: {
            now: date
        },
        success: func
    });
}
