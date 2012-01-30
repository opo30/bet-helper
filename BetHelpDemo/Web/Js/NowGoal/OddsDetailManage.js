/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />

//比赛编码，比赛名称，是否完场
var AsianOdds = function (scheduleID) {
    
    var scheduleName, scheduleType, matchdate;
    Ext.each(A, function (s) {
        if (scheduleID == s[0]) {
            scheduleName = s[4] + "-" + s[7];
            scheduleType = B[s[1]];
            var dateStr = s[11].split(',');
            if (s[12] == -1) {
                matchdate = dateStr[0] + "-" + (dateStr[1] + 1) + "-" + dateStr[2];
            }
        }
    });
    if (!scheduleName) {
        Ext.each(HistoryScore.A, function (s) {
            if (scheduleID == s[0]) {
                scheduleName = s[4] + "-" + s[7];
                scheduleType = HistoryScore.B[s[1]];
                var dateStr = s[10].split('<br>');
                if (s[12] == -1) {
                    matchdate = "20" + dateStr[0];
                }
            }
        });
    }

    var companyid_array = [];
    for (var i in company) {
        if (typeof (company[i]) == "string") {
            companyid_array.push(i);
        }
    }
    fields = [
          { type: 'int', name: 'companyid' },
          { type: 'string', name: 'aaa' },
          { type: 'string', name: 'aab' },
          { type: 'string', name: 'aac' },
          { type: 'string', name: 'aba' },
          { type: 'string', name: 'abb' },
          { type: 'string', name: 'abc' },
          { type: 'string', name: 'baa' },
          { type: 'string', name: 'bab' },
          { type: 'string', name: 'bac' },
          { type: 'string', name: 'bba' },
          { type: 'string', name: 'bbb' },
          { type: 'string', name: 'bbc' },
          { type: 'string', name: 'caa' },
          { type: 'string', name: 'cab' },
          { type: 'string', name: 'cac' },
          { type: 'string', name: 'cba' },
          { type: 'string', name: 'cbb' },
          { type: 'string', name: 'cbc' },

          { type: 'string', name: 'count1' },
          { type: 'string', name: 'aaa1' },
          { type: 'string', name: 'aab1' },
          { type: 'string', name: 'aac1' },
          { type: 'string', name: 'aba1' },
          { type: 'string', name: 'abb1' },
          { type: 'string', name: 'abc1' },
          { type: 'string', name: 'baa1' },
          { type: 'string', name: 'bab1' },
          { type: 'string', name: 'bac1' },
          { type: 'string', name: 'bba1' },
          { type: 'string', name: 'bbb1' },
          { type: 'string', name: 'bbc1' }
      ],
    columns = [
          {
              dataIndex: 'companyid', header: '公司', width: 150,
              renderer: function (value) {
                  return "<strong>" + company[value] + "</strong>";
              }
          },
          {
              dataIndex: 'aaa',
              header: '主队',
              renderer: function (value, last, row) {
                  return value + "<br/><span>" + row.get('aaa1') + "</span>";
              }
          },
          { dataIndex: 'aab', header: '盘口',
              renderer: function (value, last, row) {
                  return value + "<br/><span>" + row.get('aab1') + "</span>";
              }
          },
          { dataIndex: 'aac', header: '客队',
              renderer: function (value, last, row) {
                  return value + "<br/><span>" + row.get('aac1') + "</span>";
              }
          },
          { dataIndex: 'aba', header: '主队',
              renderer: function (value, last, row) {
                  return value + "<br/><span>" + row.get('aba1') + "</span>";
              }
          },
          { dataIndex: 'abb', header: '盘口',
              renderer: function (value, last, row) {
                  return value + "<br/><span>" + row.get('abb1') + "</span>";
              }
          },
          { dataIndex: 'abc', header: '客队',
              renderer: function (value, last, row) {
                  return value + "<br/><span>" + row.get('abc1') + "</span>";
              }
          },
          { dataIndex: 'baa', header: '主胜',
              renderer: function (value, last, row) {
                  return value + "<br/><span>" + row.get('baa1') + "</span>";
              }
          },
          { dataIndex: 'bab', header: '和局',
              renderer: function (value, last, row) {
                  return value + "<br/><span>" + row.get('bab1') + "</span>";
              }
          },
          { dataIndex: 'bac', header: '客胜',
              renderer: function (value, last, row) {
                  return value + "<br/><span>" + row.get('bac1') + "</span>";
              }
          },
          { dataIndex: 'bba', header: '主胜',
              renderer: function (value, last, row) {
                  return "<font color=blue>" + value + "</font><br/><span>" + row.get('bba1') + "</span>";
              }
          },
          { dataIndex: 'bbb', header: '和局',
              renderer: function (value, last, row) {
                  return "<font color=blue>" + value + "</font><br/><span>" + row.get('bbb1') + "</span>";
              }
          },
          { dataIndex: 'bbc', header: '客胜',
              renderer: function (value, last, row) {
                  return "<font color=blue>" + value + "</font><br/><span>" + row.get('bbc1') + "</span>";
              }
          },
          { dataIndex: 'caa', header: '大球' },
          { dataIndex: 'cab', header: '盘口' },
          { dataIndex: 'cac', header: '小球' },
          { dataIndex: 'cba', header: '大球',
              renderer: function (value) {
                  return "<font color=blue>" + value + "</font>";
              }
          },
          { dataIndex: 'cbb', header: '盘口',
              renderer: function (value) {
                  return "<font color=blue>" + value + "</font>";
              }
          },
          { dataIndex: 'cbc', header: '小球',
              renderer: function (value) {
                  return "<font color=blue>" + value + "</font>";
              }
          }
      ],
    data = [],
    continentGroupRow = [{ header: '', colspan: 1, align: 'center' }, { header: '让球盘', colspan: 6, align: 'center' }, { header: '标准盘', colspan: 6, align: 'center' }, { header: '大小盘', colspan: 6, align: 'center'}],
    cityGroupRow = [
          { header: '', colspan: 1, align: 'center' },
          { header: '初盘', colspan: 3, align: 'center' },
          { header: '即时盘', colspan: 3, align: 'center' },
          { header: '初盘', colspan: 3, align: 'center' },
          { header: '即时盘', colspan: 3, align: 'center' },
          { header: '初盘', colspan: 3, align: 'center' },
          { header: '即时盘', colspan: 3, align: 'center' }
      ];


    var group = new Ext.ux.grid.ColumnHeaderGroup({
        rows: [continentGroupRow, cityGroupRow]
    });

    var store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy(
           {
               url: "Data/NowGoal/GetOddsDetail.aspx",
               //url: "Server.aspx?a=getOdds3in1",
               method: "POST",
               timeout: 600000
           }),
        reader: new Ext.data.JsonReader(
           {
               fields: fields,
               root: "data",
               id: "id",
               totalProperty: "totalCount"

           }),
        sortInfo: { field: 'companyid', direction: "DESC" },
        baseParams: {
            scheduleID: scheduleID, companyID: companyid_array, date: matchdate
        }
    });

    var grid = new Ext.grid.GridPanel({
        title: "综合赔率",
        store: store,
        columns: columns,
        columnLines: true,
        loadMask: true,
        viewConfig: {
            forceFit: true
        },
        plugins: group,
        tbar: [new Ext.Toolbar.Fill(), {
            xtype: "checkbox",
            fieldLabel: "赛事",
            listeners: {
                check: function (obj, ischecked) {
                    store.baseParams.scheduleType = (ischecked ? scheduleType : 0);
                    store.reload();
                }
            }
        }, "-", {
            text: '让球盘统计',
            handler: function () {
                var rows = grid.getSelectionModel().getSelections();
                if (rows.length == 0) {
                    Ext.Msg.alert("提示信息", "您没有选中任何行!");
                } else {
                    var companyIDArray = [];
                    for (var i = 0; i < rows.length; i++) {
                        companyIDArray.push(rows[i].get("companyid"));
                    }
                    var grid1 = new Ext.grid.GridPanel({
                        title: "历史概率",
                        store: new Ext.data.JsonStore({
                            fields: [
                            { name: 'id', type: 'int' },
                                { name: 'name', type: 'string' },
                                { name: 'rqyper', type: 'float' },
                                { name: 'rqzper', type: 'float' },
                                { name: 'rqsper', type: 'float' },
                                { name: 'diffper', type: 'float' },
                                { name: 'suggest', type: 'int'}]
                        }),
                        cm: new Ext.grid.ColumnModel([{
                            header: "算法",
                            dataIndex: "name",
                            width: 40
                        }, {
                            header: "初盘赢盘",
                            dataIndex: "rqyper",
                            width: 20,
                            renderer: function (value) {
                                return Ext.util.Format.round(value, 2);
                            }
                        }, {
                            header: "初盘走盘",
                            dataIndex: "rqzper",
                            width: 20,
                            renderer: function (value) {
                                return Ext.util.Format.round(value, 2);
                            }
                        }, {
                            header: "初盘输盘",
                            dataIndex: "rqsper",
                            width: 20,
                            renderer: function (value) {
                                return Ext.util.Format.round(value, 2);
                            }
                        }, {
                            header: "差值",
                            dataIndex: "diffper",
                            width: 20,
                            renderer: function (value, last, row) {
                                if (row.get('id') == 1 && Math.abs(value) >= 25) {
                                    return "<font color=red>" + Ext.util.Format.round(value, 2) + "</font>";
                                }
                                else if (row.get('id') == 2 && Math.abs(value) >= 50) {
                                    return "<font color=red>" + Ext.util.Format.round(value, 2) + "</font>";
                                }
                                else if (row.get('id') == 3 && Math.abs(value) >= 80) {
                                    return "<font color=red>" + Ext.util.Format.round(value, 2) + "</font>";
                                }
                                return Ext.util.Format.round(value, 2);
                            }
                        }, {
                            header: "建议投注",
                            dataIndex: "suggest",
                            width: 20
                        }]),
                        height: 400,
                        loadMask: true,
                        autoScroll: true,
                        border: false,
                        sortable: false,
                        viewConfig: {
                            //自动填充
                            emptyText: '没有记录',
                            forceFit: true,
                            sortAscText: '正序排列',
                            sortDescText: '倒序排列',
                            columnsText: '显示/隐藏列'
                        },
                        listeners: {
                        }
                    });
                    var win = new Ext.Window({
                        width: 700,
                        height: 500,
                        title: '让球盘统计',
                        layout: 'column',
                        modal: false,
                        items: [grid1],
                        listeners: {
                            show: function (w) {
                                var loadMask = new Ext.LoadMask(win.getEl(), {
                                    msg: '正在读取数据，请稍候...',
                                    removeMask: true
                                });
                                loadMask.show();
                                Ext.Ajax.request({
                                    url: 'Data/NowGoal/schedule.aspx?a=queryOddsPerRQ1',
                                    timeout: 600000,
                                    params: { companyids: companyIDArray, scheduletypeid: scheduleType, scheduleid: scheduleID },
                                    success: function (res) {
                                        var result = Ext.decode(res.responseText);
                                        loadMask.hide();
                                        if (result.success) {
                                            grid1.getStore().loadData(result.data);
                                        }
                                    }
                                });
                            }
                        }
                    });
                    win.show();
                }
            }

        }, "-", {
            text: '让球盘统计',
            handler: function () {
                var rows = grid.getSelectionModel().getSelections();
                if (rows.length == 0) {
                    Ext.Msg.alert("提示信息", "您没有选中任何行!");
                } else {
                    var rowData = [];
                    for (var i = 0; i < rows.length; i++) {
                        rowData.push({ aaa: rows[i].get("aaa"), aab: rows[i].get("aab"), aac: rows[i].get("aac"), aba: rows[i].get("aba"), abb: rows[i].get("abb"), abc: rows[i].get("abc"), baa: rows[i].get("baa"), bab: rows[i].get("bab"), bac: rows[i].get("bac"), bba: rows[i].get("bba"), bbb: rows[i].get("bbb"), bbc: rows[i].get("bbc"), companyid: rows[i].get("companyid") });
                    }
                    var grid1 = new Ext.grid.GridPanel({
                        title: "历史概率",
                        store: new Ext.data.JsonStore({
                            fields: [
                            { name: 'id', type: 'int' },
                                { name: 'name', type: 'string' },
                                { name: 'rqyper', type: 'float' },
                                { name: 'rqzper', type: 'float' },
                                { name: 'rqsper', type: 'float' },
                                { name: 'diffper', type: 'float' },
                                { name: 'suggest', type: 'int'}]
                        }),
                        cm: new Ext.grid.ColumnModel([{
                            header: "算法",
                            dataIndex: "name",
                            width: 40
                        }, {
                            header: "初盘赢盘",
                            dataIndex: "rqyper",
                            width: 20,
                            renderer: function (value) {
                                return Ext.util.Format.round(value, 2);
                            }
                        }, {
                            header: "初盘走盘",
                            dataIndex: "rqzper",
                            width: 20,
                            renderer: function (value) {
                                return Ext.util.Format.round(value, 2);
                            }
                        }, {
                            header: "初盘输盘",
                            dataIndex: "rqsper",
                            width: 20,
                            renderer: function (value) {
                                return Ext.util.Format.round(value, 2);
                            }
                        }, {
                            header: "差值",
                            dataIndex: "diffper",
                            width: 20,
                            renderer: function (value, last, row) {
                                if (row.get('id') == 1 && Math.abs(value) >= 25) {
                                    return "<font color=red>" + Ext.util.Format.round(value, 2) + "</font>";
                                }
                                else if (row.get('id') == 2 && Math.abs(value) >= 50) {
                                    return "<font color=red>" + Ext.util.Format.round(value, 2) + "</font>";
                                }
                                else if (row.get('id') == 3 && Math.abs(value) >= 80) {
                                    return "<font color=red>" + Ext.util.Format.round(value, 2) + "</font>";
                                }
                                return Ext.util.Format.round(value, 2);
                            }
                        }, {
                            header: "建议投注",
                            dataIndex: "suggest",
                            width: 20
                        }]),
                        height: 400,
                        loadMask: true,
                        autoScroll: true,
                        border: false,
                        sortable: false,
                        viewConfig: {
                            //自动填充
                            emptyText: '没有记录',
                            forceFit: true,
                            sortAscText: '正序排列',
                            sortDescText: '倒序排列',
                            columnsText: '显示/隐藏列'
                        },
                        listeners: {
                        }
                    });
                    var win = new Ext.Window({
                        width: 700,
                        height: 500,
                        title: '让球盘统计',
                        layout: 'column',
                        modal: false,
                        items: [grid1],
                        listeners: {
                            show: function (w) {
                                var loadMask = new Ext.LoadMask(win.getEl(), {
                                    msg: '正在读取数据，请稍候...',
                                    removeMask: true
                                });
                                loadMask.show();
                                Ext.Ajax.request({
                                    url: 'Data/NowGoal/schedule.aspx?a=queryOddsPerRQ',
                                    timeout: 600000,
                                    params: { rowData: Ext.encode(rowData) },
                                    success: function (res) {
                                        var result = Ext.decode(res.responseText);
                                        loadMask.hide();
                                        if (result.success) {
                                            grid1.getStore().loadData(result.data);
                                        }
                                    }
                                });
                            }
                        }
                    });
                    win.show();
                }
            }

        }, "-",
        {
            text: '让球盘分析',
            handler: function () {
                var rows = grid.getSelectionModel().getSelections();
                if (rows.length == 0) {
                    Ext.Msg.alert("提示信息", "您没有选中任何行!");
                } else {
                    var rowData = [];
                    for (var i = 0; i < rows.length; i++) {
                        //rowData.push(rows[i].data);
                        rowData.push({ aaa: rows[i].get("aaa"), aab: rows[i].get("aab"), aac: rows[i].get("aac"), aba: rows[i].get("aba"), abb: rows[i].get("abb"), abc: rows[i].get("abc"), companyid: rows[i].get("companyid") });
                    }
                    var grid1 = new Ext.grid.GridPanel({
                        title: "历史概率",
                        store: new Ext.data.JsonStore({
                            fields: [
                                { name: 'blur', type: 'float' },
                                { name: 'rqycount1', type: 'int' },
                                { name: 'rqzcount1', type: 'int' },
                                { name: 'rqscount1', type: 'int' },
                                { name: 'totalCount1', type: 'int' },
                                { name: 'suggest', type: 'string'}]
                        }),
                        cm: new Ext.grid.ColumnModel([{
                            header: "模糊值",
                            dataIndex: "blur",
                            width: 20
                        }, {
                            header: "初盘赢盘",
                            dataIndex: "rqycount1",
                            width: 20,
                            renderer: function (v) {
                                var per = v / arguments[2].get('totalCount1') * 100;
                                return v + "（" + Ext.util.Format.round(per, 2) + "%）";
                            }
                        }, {
                            header: "初盘走盘",
                            dataIndex: "rqzcount1",
                            width: 20,
                            renderer: function (v) {
                                var per = v / arguments[2].get('totalCount1') * 100;
                                return v + "（" + Ext.util.Format.round(per, 2) + "%）";
                            }
                        }, {
                            header: "初盘输盘",
                            dataIndex: "rqscount1",
                            width: 20,
                            renderer: function (v) {
                                var per = v / arguments[2].get('totalCount1') * 100;
                                return v + "（" + Ext.util.Format.round(per, 2) + "%）";
                            }
                        }, {
                            header: "建议投注",
                            dataIndex: "suggest",
                            width: 20
                        }]),
                        height: 400,
                        columnWidth: 0.7,
                        loadMask: true,
                        autoScroll: true,
                        border: false,
                        sortable: false,
                        viewConfig: {
                            //自动填充
                            emptyText: '没有记录',
                            forceFit: true,
                            sortAscText: '正序排列',
                            sortDescText: '倒序排列',
                            columnsText: '显示/隐藏列'
                        },
                        listeners: {
                            rowclick: function (g, rowIndex, e) {
                                var rowdata = g.getStore().getAt(rowIndex);
                                var data1 = [{ 让球盘路: '赢' }, { 让球盘路: '走' }, { 让球盘路: '输'}];
                                data1[0].比赛总数 = rowdata.get('rqycount1');
                                data1[1].比赛总数 = rowdata.get('rqzcount1');
                                data1[2].比赛总数 = rowdata.get('rqscount1');
                                win.findByType('piechart')[0].store.loadData(data1);
                            }
                        }
                    });
                    var win = new Ext.Window({
                        width: 700,
                        height: 500,
                        title: '让球盘',
                        layout: 'column',
                        modal: false,
                        tbar: [new Ext.Toolbar.Fill(), {
                            xtype: "numberfield",
                            fieldLabel: "精确度",
                            allowBlank: false,
                            value: 0.05
                        }, "-", {
                            xtype: "checkbox",
                            fieldLabel: "赛事"
                        }, "-", {
                            xtype: "checkbox",
                            fieldLabel: "重复"
                        }, "-", {
                            text: '查询',
                            handler: function () {
                                var loadMask = new Ext.LoadMask(win.getEl(), {
                                    msg: '正在读取数据，请稍候...',
                                    removeMask: true
                                });
                                loadMask.show();
                                Ext.Ajax.request({
                                    url: 'Data/NowGoal/schedule.aspx?a=queryOddsCountRQ',
                                    timeout: 600000,
                                    params: { rowData: Ext.encode(rowData),
                                        blur: win.toolbars[0].findByType('numberfield')[0].getValue(),
                                        scheduleType: win.toolbars[0].findByType('checkbox')[0].getValue() ? scheduleType : 0
                                    },
                                    success: function (res) {
                                        loadMask.hide();
                                        var result = Ext.decode(res.responseText);
                                        if (result.success) {
                                            var row = grid1.getStore().recordType;
                                            var totalCount = grid1.getStore().getTotalCount();
                                            var p = new row(result.data[0]);
                                            grid1.getStore().insert(totalCount - 1, p);
                                            var data1 = [{ 让球盘路: '赢' }, { 让球盘路: '走' }, { 让球盘路: '输'}];
                                            data1[0].比赛总数 = result.data[0].rqycount1;
                                            data1[1].比赛总数 = result.data[0].rqzcount1;
                                            data1[2].比赛总数 = result.data[0].rqscount1;
                                            win.findByType('piechart')[0].store.loadData(data1);
                                            var blurValue = win.toolbars[0].findByType('numberfield')[0].getValue();
                                            if (blurValue > 0 && result.data[0].totalCount1 > 1 && win.toolbars[0].findByType('checkbox')[1].getValue()) {
                                                win.toolbars[0].findByType('numberfield')[0].setValue(blurValue - ((Math.floor(blurValue * 10) + 1) / 100));
                                                win.toolbars[0].findByType('button')[0].handler();
                                            }
                                        }
                                    }
                                });
                            }
                        }],
                        items: [grid1, {
                            store: new Ext.data.JsonStore({
                                fields: [
                                { name: '让球盘路', type: 'string' },
                                { name: '比赛总数', type: 'int'}]
                            }),
                            xtype: 'piechart',
                            dataField: '比赛总数',
                            categoryField: '让球盘路',
                            columnWidth: 0.3,
                            height: 200,
                            extraStyle:
                            {
                                legend:
                                {
                                    display: 'bottom',
                                    padding: 5,
                                    font:
                                    {
                                        family: 'Tahoma',
                                        size: 13
                                    }
                                }
                            },
                            series: [{
                                style: {
                                    colors: ["#FF0000", "#008000", "#0000FF"]
                                }
                            }]
                        }],
                        listeners: {
                            show: function (w) {
                                var loadMask = new Ext.LoadMask(win.getEl(), {
                                    msg: '正在读取数据，请稍候...',
                                    removeMask: true
                                });
                                loadMask.show();
                                Ext.Ajax.request({
                                    url: 'Data/NowGoal/schedule.aspx?a=queryOddsCountRQ',
                                    timeout: 600000,
                                    params: { rowData: Ext.encode(rowData), blur: 0.05, scheduleType: 0 },
                                    success: function (res) {
                                        var result = Ext.decode(res.responseText);
                                        loadMask.hide();
                                        if (result.success) {
                                            grid1.getStore().loadData(result.data)
                                            var data1 = [{ 让球盘路: '赢' }, { 让球盘路: '走' }, { 让球盘路: '输'}];
                                            data1[0].比赛总数 = result.data[0].rqycount1;
                                            data1[1].比赛总数 = result.data[0].rqzcount1;
                                            data1[2].比赛总数 = result.data[0].rqscount1;
                                            win.findByType('piechart')[0].store.loadData(data1);
                                            //                                            win.findByType('piechart')[0].store.loadData(result.data1);
                                            //                                            win.findByType('piechart')[1].store.loadData(result.data2);
                                        }
                                    }
                                });
                            }
                        }
                    });
                    win.show();
                }
            }
        }, {
            text: '标准盘统计',
            handler: function () {
                var rows = grid.getSelectionModel().getSelections();
                if (rows.length == 0) {
                    Ext.Msg.alert("提示信息", "您没有选中任何行!");
                } else {
                    var rowData = [];
                    for (var i = 0; i < rows.length; i++) {
                        rowData.push(rows[i].data);
                    }
                    var grid1 = new Ext.grid.GridPanel({
                        title: "历史概率",
                        store: new Ext.data.JsonStore({
                            fields: [
                                { name: 'blur', type: 'float' },
                                { name: 'bzscount1', type: 'int' },
                                { name: 'bzpcount1', type: 'int' },
                                { name: 'bzfcount1', type: 'int' },
                                { name: 'totalCount1', type: 'int'}]
                        }),
                        cm: new Ext.grid.ColumnModel([{
                            header: "模糊值",
                            dataIndex: "blur",
                            width: 20
                        }, {
                            header: "初盘主胜",
                            dataIndex: "bzscount1",
                            width: 20,
                            renderer: function (v) {
                                var per = v / arguments[2].get('totalCount1') * 100;
                                return v + "（" + Ext.util.Format.round(per, 2) + "%）" + "（" + ((100 / per) - ((100 / per) / 10)) + "）";
                            }
                        }, {
                            header: "初盘平局",
                            dataIndex: "bzpcount1",
                            width: 20,
                            renderer: function (v) {
                                var per = v / arguments[2].get('totalCount1') * 100;
                                return v + "（" + Ext.util.Format.round(per, 2) + "%）" + "（" + ((100 / per) - ((100 / per) / 10)) + "）";
                            }
                        }, {
                            header: "初盘客胜",
                            dataIndex: "bzfcount1",
                            width: 20,
                            renderer: function (v) {
                                var per = v / arguments[2].get('totalCount1') * 100;
                                return v + "（" + Ext.util.Format.round(per, 2) + "%）" + "（" + ((100 / per) - ((100 / per) / 10)) + "）";
                            }
                        }]),
                        height: 150,
                        columnWidth: 1,
                        loadMask: true,
                        autoScroll: true,
                        border: false,
                        sortable: false,
                        viewConfig: {
                            //自动填充
                            emptyText: '没有记录',
                            forceFit: true,
                            sortAscText: '正序排列',
                            sortDescText: '倒序排列',
                            columnsText: '显示/隐藏列'
                        },
                        listeners: {
                        }
                    });
                    var win = new Ext.Window({
                        width: 800,
                        height: 550,
                        title: '标准盘',
                        layout: 'column',
                        modal: false,
                        tbar: [new Ext.Toolbar.Fill(), {
                            xtype: "numberfield",
                            fieldLabel: "精确度",
                            allowBlank: false,
                            value: 1
                        }, "-", {
                            xtype: "checkbox",
                            fieldLabel: "赛事"
                        }, "-", {
                            xtype: "checkbox",
                            fieldLabel: "重复"
                        }, "-", {
                            text: '查询',
                            handler: function () {
                                var loadMask = new Ext.LoadMask(win.getEl(), {
                                    msg: '正在读取数据，请稍候...',
                                    removeMask: true
                                });
                                loadMask.show();
                                Ext.Ajax.request({
                                    url: 'Data/NowGoal/schedule.aspx?a=queryOddsCountBZ',
                                    timeout: 600000,
                                    params: { rowData: Ext.encode(rowData),
                                        blur: win.toolbars[0].findByType('numberfield')[0].getValue(),
                                        scheduleType: win.toolbars[0].findByType('checkbox')[0].getValue() ? scheduleType : 0
                                    },
                                    success: function (res) {
                                        loadMask.hide();
                                        var result = Ext.decode(res.responseText);
                                        if (result.success) {
                                            var row = grid1.getStore().recordType;
                                            var totalCount = grid1.getStore().getTotalCount();
                                            var p = new row(result.data[0]);
                                            grid1.getStore().insert(totalCount - 1, p);
                                            var blurValue = win.toolbars[0].findByType('numberfield')[0].getValue();
                                            if (blurValue > 0 && result.data[0].totalCount1 > 5 && win.toolbars[0].findByType('checkbox')[1].getValue()) {
                                                win.toolbars[0].findByType('numberfield')[0].setValue(blurValue - 0.02);
                                                win.toolbars[0].findByType('button')[0].handler();
                                            }
                                        }
                                    }
                                });
                            }
                        }],
                        items: [grid1],
                        listeners: {
                            show: function (w) {
                                var loadMask = new Ext.LoadMask(win.getEl(), {
                                    msg: '正在读取数据，请稍候...',
                                    removeMask: true
                                });
                                loadMask.show();
                                Ext.Ajax.request({
                                    url: 'Data/NowGoal/schedule.aspx?a=queryOddsCountBZ',
                                    timeout: 600000,
                                    params: { rowData: Ext.encode(rowData), blur: 1, scheduleType: 0 },
                                    success: function (res) {
                                        var result = Ext.decode(res.responseText);
                                        loadMask.hide();
                                        if (result.success) {
                                            grid1.getStore().loadData(result.data);
                                        }
                                    }
                                });
                            }
                        }
                    });
                    win.show();
                }
            }
        }, {
            text: '大小盘统计',
            handler: function () {
                var rows = grid.getSelectionModel().getSelections();
                if (rows.length == 0) {
                    Ext.Msg.alert("提示信息", "您没有选中任何行!");
                } else {
                    var rowData = [];
                    for (var i = 0; i < rows.length; i++) {
                        rowData.push(rows[i].data);
                    }
                    var grid1 = new Ext.grid.GridPanel({
                        title: "历史概率",
                        store: new Ext.data.JsonStore({
                            fields: [
                                { name: 'blur', type: 'float' },
                                { name: 'dxycount1', type: 'int' },
                                { name: 'dxzcount1', type: 'int' },
                                { name: 'dxscount1', type: 'int' },
                                { name: 'totalCount1', type: 'int'}]
                        }),
                        cm: new Ext.grid.ColumnModel([{
                            header: "模糊值",
                            dataIndex: "blur",
                            width: 20
                        }, {
                            header: "大球",
                            dataIndex: "dxycount1",
                            width: 20,
                            renderer: function (v) {
                                var per = v / arguments[2].get('totalCount1') * 100;
                                return v + "（" + Ext.util.Format.round(per, 2) + "%）";
                            }
                        }, {
                            header: "走盘",
                            dataIndex: "dxzcount1",
                            width: 20,
                            renderer: function (v) {
                                var per = v / arguments[2].get('totalCount1') * 100;
                                return v + "（" + Ext.util.Format.round(per, 2) + "%）";
                            }
                        }, {
                            header: "小球",
                            dataIndex: "dxscount1",
                            width: 20,
                            renderer: function (v) {
                                var per = v / arguments[2].get('totalCount1') * 100;
                                return v + "（" + Ext.util.Format.round(per, 2) + "%）";
                            }
                        }]),
                        height: 200,
                        columnWidth: 0.7,
                        loadMask: true,
                        autoScroll: true,
                        border: false,
                        sortable: false,
                        viewConfig: {
                            //自动填充
                            emptyText: '没有记录',
                            forceFit: true,
                            sortAscText: '正序排列',
                            sortDescText: '倒序排列',
                            columnsText: '显示/隐藏列'
                        },
                        listeners: {
                            rowclick: function (g, rowIndex, e) {
                                var rowdata = g.getStore().getAt(rowIndex);
                                var data1 = [{ 让球盘路: '赢' }, { 让球盘路: '走' }, { 让球盘路: '输'}];
                                data1[0].比赛总数 = rowdata.get('dxycount1');
                                data1[1].比赛总数 = rowdata.get('dxzcount1');
                                data1[2].比赛总数 = rowdata.get('dxscount1');
                                win.findByType('piechart')[0].store.loadData(data1);
                            }
                        }
                    });
                    var win = new Ext.Window({
                        width: 600,
                        height: 300,
                        title: '让球盘',
                        layout: 'column',
                        modal: false,
                        tbar: [new Ext.Toolbar.Fill(), {
                            xtype: "numberfield",
                            fieldLabel: "精确度",
                            allowBlank: false,
                            value: 0.5
                        }, "-", {
                            xtype: "checkbox",
                            fieldLabel: "赛事"
                        }, "-", {
                            text: '查询',
                            handler: function () {
                                var loadMask = new Ext.LoadMask(win.getEl(), {
                                    msg: '正在读取数据，请稍候...',
                                    removeMask: true
                                });
                                loadMask.show();
                                Ext.Ajax.request({
                                    url: 'Data/NowGoal/schedule.aspx?a=queryOddsCountDX',
                                    timeout: 600000,
                                    params: { rowData: Ext.encode(rowData),
                                        blur: win.toolbars[0].findByType('numberfield')[0].getValue(),
                                        scheduleType: win.toolbars[0].findByType('checkbox')[0].getValue() ? scheduleType : 0
                                    },
                                    success: function (res) {
                                        loadMask.hide();
                                        var result = Ext.decode(res.responseText);
                                        if (result.success) {
                                            var row = grid1.getStore().recordType;
                                            var totalCount = grid1.getStore().getTotalCount();
                                            var p = new row(result.data[0]);
                                            grid1.getStore().insert(totalCount - 1, p);
                                            var data1 = [{ 让球盘路: '赢' }, { 让球盘路: '走' }, { 让球盘路: '输'}];
                                            data1[0].比赛总数 = result.data[0].dxycount1;
                                            data1[1].比赛总数 = result.data[0].dxzcount1;
                                            data1[2].比赛总数 = result.data[0].dxscount1;
                                            win.findByType('piechart')[0].store.loadData(data1);
                                        }
                                    }
                                });
                            }
                        }],
                        items: [grid1, {
                            store: new Ext.data.JsonStore({
                                fields: [
                                { name: '让球盘路', type: 'string' },
                                { name: '比赛总数', type: 'int'}]
                            }),
                            xtype: 'piechart',
                            dataField: '比赛总数',
                            categoryField: '让球盘路',
                            columnWidth: 0.3,
                            height: 200,
                            extraStyle:
                            {
                                legend:
                                {
                                    display: 'bottom',
                                    padding: 5,
                                    font:
                                    {
                                        family: 'Tahoma',
                                        size: 13
                                    }
                                }
                            },
                            series: [{
                                style: {
                                    colors: ["#FF0000", "#008000", "#0000FF"]
                                }
                            }]
                        }],
                        listeners: {
                            show: function (w) {
                                var loadMask = new Ext.LoadMask(win.getEl(), {
                                    msg: '正在读取数据，请稍候...',
                                    removeMask: true
                                });
                                loadMask.show();
                                Ext.Ajax.request({
                                    url: 'Data/NowGoal/schedule.aspx?a=queryOddsCountDX',
                                    timeout: 600000,
                                    params: { rowData: Ext.encode(rowData), blur: 0.5, scheduleType: 0 },
                                    success: function (res) {
                                        var result = Ext.decode(res.responseText);
                                        loadMask.hide();
                                        if (result.success) {
                                            grid1.getStore().loadData(result.data)
                                            var data1 = [{ 让球盘路: '赢' }, { 让球盘路: '走' }, { 让球盘路: '输'}];
                                            data1[0].比赛总数 = result.data[0].dxycount1;
                                            data1[1].比赛总数 = result.data[0].dxzcount1;
                                            data1[2].比赛总数 = result.data[0].dxscount1;
                                            win.findByType('piechart')[0].store.loadData(data1);
                                            //                                            win.findByType('piechart')[0].store.loadData(result.data1);
                                            //                                            win.findByType('piechart')[1].store.loadData(result.data2);
                                        }
                                    }
                                });
                            }
                        }
                    });
                    win.show();
                }
            }
        }, "-", {
            text: '让球盘',
            menu: [{
                text: '初盘分析',
                handler: function () {
                    var odds = [];
                    var rows = grid.getSelectionModel().getSelections();
                    if (rows.length == 0) {
                        Ext.Msg.alert("提示信息", "您没有选中任何行!");
                    }
                    else {
                        for (var i = 0; i < rows.length; i++) {
                            if (rows[i].get('aaa') != "" && rows[i].get('aab') != "" && rows[i].get('aac') != "") {
                                odds.push({
                                    companyid: rows[i].get('companyid'),
                                    home1: rows[i].get('aaa'),
                                    away1: rows[i].get('aac'),
                                    pankou1: rows[i].get('aab'),
                                    home2: '',
                                    away2: '',
                                    pankou2: rows[i].get('abb')
                                });
                            }
                        }
                        queryOddsRQ(odds);
                    }
                }
            }, {
                text: '临场分析',
                handler: function () {
                    var odds = [];
                    var rows = grid.getSelectionModel().getSelections();
                    if (rows.length == 0) {
                        Ext.Msg.alert("提示信息", "您没有选中任何行!");
                    }
                    else {
                        for (var i = 0; i < rows.length; i++) {
                            if (rows[i].get('aba') != "" && rows[i].get('abb') != "" && rows[i].get('abc') != "") {
                                odds.push({
                                    companyid: rows[i].get('companyid'),
                                    companyname: company[rows[i].get('companyid')],
                                    home1: '',
                                    away1: '',
                                    pankou1: rows[i].get('aab'),
                                    home2: rows[i].get('aba'),
                                    away2: rows[i].get('abc'),
                                    pankou2: rows[i].get('abb')
                                });
                            }
                        }
                        queryOddsRQ(odds);
                    }
                }
            }]
        }, "-", {
            text: '标准盘',
            menu: [{
                text: '初盘分析',
                handler: function () {
                    var odds = [];
                    var rows = grid.getSelectionModel().getSelections();
                    if (rows.length == 0) {
                        Ext.Msg.alert("提示信息", "您没有选中任何行!");
                    }
                    else {
                        for (var i = 0; i < rows.length; i++) {
                            if (rows[i].get('baa') != "" && rows[i].get('bab') != "" && rows[i].get('bac') != "") {
                                odds.push({
                                    companyid: rows[i].get('companyid'),
                                    home1: rows[i].get('baa'),
                                    away1: rows[i].get('bac'),
                                    draw1: rows[i].get('bab'),
                                    home2: '',
                                    away2: '',
                                    draw2: ''
                                });
                            }
                        }
                        queryOddsBZ(odds);
                    }
                }
            }, {
                text: '临场分析',
                handler: function () {
                    var odds = [];
                    var rows = grid.getSelectionModel().getSelections();
                    if (rows.length == 0) {
                        Ext.Msg.alert("提示信息", "您没有选中任何行!");
                    }
                    else {
                        for (var i = 0; i < rows.length; i++) {
                            if (rows[i].get('bba') != "" && rows[i].get('bbb') != "" && rows[i].get('bbc') != "") {
                                odds.push({
                                    companyid: rows[i].get('companyid'),
                                    home1: '',
                                    away1: '',
                                    draw1: '',
                                    home2: rows[i].get('bba'),
                                    away2: rows[i].get('bbc'),
                                    draw2: rows[i].get('bbb')
                                });
                            }
                        }
                        queryOddsBZ(odds);
                    }
                }
            }, {
                text: '综合分析',
                handler: function () {
                    var odds = [];
                    var rows = grid.getSelectionModel().getSelections();
                    if (rows.length == 0) {
                        Ext.Msg.alert("提示信息", "您没有选中任何行!");
                    }
                    else {
                        for (var i = 0; i < rows.length; i++) {
                            if (rows[i].get('baa') != "" && rows[i].get('bab') != "" && rows[i].get('bac') != "" && rows[i].get('bba') != "" && rows[i].get('bbb') != "" && rows[i].get('bbc') != "") {
                                odds.push({
                                    companyid: rows[i].get('companyid'),
                                    home1: rows[i].get('baa'),
                                    away1: rows[i].get('bac'),
                                    draw1: rows[i].get('bab'),
                                    home2: rows[i].get('bba'),
                                    away2: rows[i].get('bbc'),
                                    draw2: rows[i].get('bbb')
                                });
                            }
                        }
                        queryOddsBZ(odds);
                    }
                }
            }]
        }, {
            text: '刷新',
            handler: function () {
                store.reload();
            }
        }],
        listeners: {
            rowclick: function (g, rowIndex, e) {

            }
        }
    });


    var tab = center.getItem("AsianOddsTab_" + scheduleID);
    if (!tab) {
        var tab = center.add({
            id: "AsianOddsTab_" + scheduleID,
            iconCls: "totalicon",
            xtype: "panel",
            title: scheduleName + "综合",
            closable: true,
            layout: "fit",
            items: [grid]

        });
    }
    center.setActiveTab(tab);
    store.load();
}

//让球盘赔率查询
var queryOddsRQ = function (oddsObject, isTrue) {
    var fields = [{ name: 'id', type: 'int' },
            { name: 'data', type: 'string' },
            { name: 'date', type: 'date' },
            { name: 'companyID', type: 'int' },
            { name: 'home1', type: 'float' },
            { name: 'pankou1', type: 'float' },
            { name: 'away1', type: 'float' },
            { name: 'time1', type: 'date' },
            { name: 'home2', type: 'float' },
            { name: 'pankou2', type: 'float' },
            { name: 'away2', type: 'float' },
            { name: 'time2', type: 'date' }, { name: 'victory', type: 'int' }, { name: 'win1', type: 'int' }, { name: 'win2', type: 'int'}];

    var store = new Ext.data.GroupingStore({
        proxy: new Ext.data.HttpProxy(
           {
               url: "Data/NowGoal/schedule.aspx?a=queryOddsRQ",
               method: "POST",
               timeout: 600000
           }),
        reader: new Ext.data.JsonReader(
           {
               fields: fields,
               root: "data",
               id: "id",
               totalProperty: "totalCount"
           }),
        baseParams: {
            odds: Ext.encode(oddsObject), isTrue: isTrue
        },
        sortInfo: { field: 'id', direction: 'DESC' },
        groupField: 'companyID'
    });

    store.on("datachanged", function (s) {
        var ycount = 0, zcount = 0, scount = 0;
        var ycount1 = 0, zcount1 = 0, scount1 = 0;
        var total = s.getTotalCount();
        for (var i = 0; i < total; i++) {
            if (s.getAt(i).get('win1') == "3") {
                ycount++;
            } else if (s.getAt(i).get('win1') == "1") {
                zcount++;
            } else if (s.getAt(i).get('win1') == "0") {
                scount++;
            }
            if (s.getAt(i).get('win2') == "3") {
                ycount1++;
            } else if (s.getAt(i).get('win2') == "1") {
                zcount1++;
            } else if (s.getAt(i).get('win2') == "0") {
                scount1++;
            }
        }
        win.setTitle("初盘 赢" + (ycount / total * 100).toFixed(0) + "% 走" + (zcount / total * 100).toFixed(0) + "% 输" + (scount / total * 100).toFixed(0) + "%  临场 赢 " + (ycount1 / total * 100).toFixed(0) + "% 走" + (zcount1 / total * 100).toFixed(0) + "% 输" + (scount1 / total * 100).toFixed(0) + "%");
    });

    var sm = new Ext.grid.CheckboxSelectionModel({
        dataIndex: "id",
        singleSelect: false
    });
    Ext.ux.grid.GroupSummary.Calculations['Victory'] = function (v, record, field, data) {
        if (v == 0) {
            return (record.get('victory') == 3 ? 1 : 0) + "/1";
        } else {
            var att = v.split('/')
            return (parseInt(att[0]) + (record.get('victory') == 3 ? 1 : 0)) + "/" + (parseInt(att[1]) + 1);
        }
    };
    Ext.ux.grid.GroupSummary.Calculations['Winning1'] = function (v, record, field, data) {
        if (v == 0) {
            return (record.get('win1') == 3 ? 1 : 0) + "/1";
        } else {
            var att = v.split('/')
            return (parseInt(att[0]) + (record.get('win1') == 3 ? 1 : 0)) + "/" + (parseInt(att[1]) + 1);
        }
    };
    Ext.ux.grid.GroupSummary.Calculations['Winning2'] = function (v, record, field, data) {
        if (v == 0) {
            return (record.get('win2') == 3 ? 1 : 0) + "/1";
        } else {
            var att = v.split('/')
            return (parseInt(att[0]) + (record.get('win2') == 3 ? 1 : 0)) + "/" + (parseInt(att[1]) + 1);
        }
    };
    //--------------------------------------------------列头
    var cm = new Ext.grid.ColumnModel([
        {
            header: "编码",
            dataIndex: "id",
            width: 20,
            sortable: true
        }, {
            header: "公司",
            dataIndex: "companyID",
            width: 20,
            sortable: true,
            renderer: function (value) {
                return company[value];
            }
        }, {
            header: "比赛数据",
            dataIndex: "data",
            sortable: true,
            renderer: function (value) {
                var arr = value.split(',')
                return arr[4] + " - " + arr[7] + "  全场 " + arr[13] + " - " + arr[14] + "  半场 " + arr[15] + " - " + arr[16];
            }
        }, {
            header: "胜负",
            tooltip: "比赛胜负",
            dataIndex: "victory",
            sortable: true,
            width: 15,
            renderer: function (v) {
                if (v == 3) {
                    return "<font color='red'>胜</font>"
                }
                else if (v == 1) {
                    return "<font color='green'>平</font>"
                }
                else {
                    return "<font color='blue'>负</font>"
                }
            },
            summaryType: 'Victory',
            summaryRenderer: function (v, params, data) {
                var vic = eval(v) * 100;
                if (vic > 50) {
                    return "<b><font color=red>" + vic.toFixed(1) + "%</font></b>";
                } else {
                    return "<b><font color=green>" + vic.toFixed(1) + "%</font></b>";
                }
            }
        }, {
            header: "主队",
            dataIndex: "home1",
            sortable: true,
            width: 15
        }, {
            header: "让球",
            dataIndex: "pankou1",
            sortable: true,
            width: 15
        }, {
            header: "客队",
            dataIndex: "away1",
            sortable: true,
            width: 15
        }, {
            header: "时间",
            dataIndex: "time1",
            width: 25,
            sortable: true,
            renderer: function (value) {
                return value ? value.dateFormat('Y-m-d H:i') : '';
            }
        }, {
            header: "盘路",
            tooltip: "比赛胜负",
            dataIndex: "win1",
            sortable: true,
            width: 15,
            renderer: function (v) {
                if (v == 3) {
                    return "<font color='red'>赢</font>"
                }
                else if (v == 1) {
                    return "<font color='green'>走</font>"
                }
                else {
                    return "<font color='blue'>输</font>"
                }
            },
            summaryType: 'Winning1',
            summaryRenderer: function (v, params, data) {
                var win = eval(v) * 100;
                if (win > 50) {
                    return "<b><font color=red>" + win.toFixed(1) + "%</font></b>";
                } else {
                    return "<b><font color=green>" + win.toFixed(1) + "%</font></b>";
                }
            }
        }, {
            header: "主队",
            dataIndex: "home2",
            sortable: true,
            width: 15
        }, {
            header: "让球",
            dataIndex: "pankou2",
            sortable: true,
            width: 15
        }, {
            header: "客队",
            dataIndex: "away2",
            sortable: true,
            width: 15
        }, {
            header: "时间",
            dataIndex: "time2",
            width: 25,
            sortable: true,
            renderer: function (value) {
                return value ? value.dateFormat('Y-m-d H:i') : '';
            }
        }, {
            header: "盘路",
            tooltip: "比赛胜负",
            dataIndex: "win2",
            sortable: true,
            width: 15,
            renderer: function (v) {
                if (v == 3) {
                    return "<font color='red'>赢</font>"
                }
                else if (v == 1) {
                    return "<font color='green'>走</font>"
                }
                else {
                    return "<font color='blue'>输</font>"
                }
            },
            summaryType: 'Winning2',
            summaryRenderer: function (v, params, data) {
                var win = eval(v) * 100;
                if (win > 50) {
                    return "<b><font color=red>" + win.toFixed(1) + "%</font></b>";
                } else {
                    return "<b><font color=green>" + win.toFixed(1) + "%</font></b>";
                }
            }
        }
]);

    var summary = new Ext.ux.grid.HybridSummary();
    //----------------------------------------------------定义grid
    var grid = new Ext.grid.GridPanel({
        store: store,
        cm: cm,
        sm: sm,
        width: 1024,
        height: 600,
        loadMask: true,
        stripeRows: true,
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
            columnsText: '显示/隐藏列'
        },
        view: new Ext.grid.GroupingView({
            forceFit: true,
            showGroupName: false,
            enableNoGroups: false,
            enableGroupingMenu: false,
            hideGroupedColumn: true,
            groupTextTpl: '{text} (<b><font color=red>{[values.rs.length]}</font> </b>条)'
        }),
        plugins: summary
    });
    var win = new Ext.Window({
        width: 1024,
        height: 600,
        title: '让球盘分析',
        layout: 'fit',
        modal: false,
        items: [grid]
    });
    win.show();
    store.load();
}


//标准盘赔率查询
var queryOddsBZ = function (oddsObject) {
    var fields = [{ name: 'id', type: 'int' },
            { name: 'data', type: 'string' },
            { name: 'date', type: 'date' },
            { name: 'time', type: 'date' },
            { name: 'companyID', type: 'int' },
            { name: 'home1', type: 'float' },
            { name: 'draw1', type: 'float' },
            { name: 'away1', type: 'float' },
            { name: 'time1', type: 'date' },
            { name: 'home2', type: 'float' },
            { name: 'draw2', type: 'float' },
            { name: 'away2', type: 'float' },
            { name: 'time2', type: 'date' }, { name: 'victory', type: 'int'}];

    var store = new Ext.data.GroupingStore({
        proxy: new Ext.data.HttpProxy(
           {
               url: "Data/NowGoal/schedule.aspx?a=queryOddsBZ",
               method: "POST",
               timeout: 600000
           }),
        reader: new Ext.data.JsonReader(
           {
               fields: fields,
               root: "data",
               id: "id",
               totalProperty: "totalCount"
           }),
        baseParams: {
            odds: Ext.encode(oddsObject)
        },
        sortInfo: { field: 'id', direction: 'DESC' },
        groupField: 'companyID'
    });

    store.on("datachanged", function (s) {
        var ycount = 0, zcount = 0, scount = 0;
        var total = s.getTotalCount();
        for (var i = 0; i < total; i++) {
            if (s.getAt(i).get('victory') == "3") {
                ycount++;
            } else if (s.getAt(i).get('victory') == "1") {
                zcount++;
            } else if (s.getAt(i).get('victory') == "0") {
                scount++;
            }
        }
        win.setTitle(win.title + "胜" + (ycount / total * 100).toFixed(1) + "% 平" + (zcount / total * 100).toFixed(1) + "% 负" + (scount / total * 100).toFixed(1) + "%");
    });

    var sm = new Ext.grid.CheckboxSelectionModel({
        dataIndex: "id",
        singleSelect: false
    });
    Ext.ux.grid.GroupSummary.Calculations['home'] = function (v, record, field, data) {
        if (v == 0) {
            return (record.get('victory') == 3 ? 1 : 0) + "/1";
        } else {
            var att = v.split('/')
            return (parseInt(att[0]) + (record.get('victory') == 3 ? 1 : 0)) + "/" + (parseInt(att[1]) + 1);
        }
    };
    Ext.ux.grid.GroupSummary.Calculations['draw'] = function (v, record, field, data) {
        if (v == 0) {
            return (record.get('victory') == 1 ? 1 : 0) + "/1";
        } else {
            var att = v.split('/')
            return (parseInt(att[0]) + (record.get('victory') == 1 ? 1 : 0)) + "/" + (parseInt(att[1]) + 1);
        }
    };
    Ext.ux.grid.GroupSummary.Calculations['away'] = function (v, record, field, data) {
        if (v == 0) {
            return (record.get('victory') == 0 ? 1 : 0) + "/1";
        } else {
            var att = v.split('/')
            return (parseInt(att[0]) + (record.get('victory') == 0 ? 1 : 0)) + "/" + (parseInt(att[1]) + 1);
        }
    };
    //--------------------------------------------------列头
    var cm = new Ext.grid.ColumnModel([
        {
            header: "编码",
            dataIndex: "id",
            width: 20,
            sortable: true
        }, {
            header: "公司",
            dataIndex: "companyID",
            width: 20,
            sortable: true,
            renderer: function (value) {
                return company[value];
            }
        }, {
            header: "比赛数据",
            dataIndex: "data",
            sortable: true,
            renderer: function (value) {
                var arr = value.split(',')
                return arr[4] + " - " + arr[7] + "  全场 " + arr[13] + " - " + arr[14] + "  半场 " + arr[15] + " - " + arr[16];
            }
        }, {
            header: "主胜",
            dataIndex: "home1",
            sortable: true,
            width: 15,
            summaryType: 'home',
            summaryRenderer: function (v, params, data) {
                var win = eval(v) * 100;
                if (win > 50) {
                    return "<b><font color=red>" + win.toFixed(1) + "%</font></b>";
                } else {
                    return "<b><font color=green>" + win.toFixed(1) + "%</font></b>";
                }
            }
        }, {
            header: "平局",
            dataIndex: "draw1",
            sortable: true,
            width: 15,
            summaryType: 'draw',
            summaryRenderer: function (v, params, data) {
                var vic = eval(v) * 100;
                if (vic > 50) {
                    return "<b><font color=red>" + vic.toFixed(1) + "%</font></b>";
                } else {
                    return "<b><font color=green>" + vic.toFixed(1) + "%</font></b>";
                }
            }
        }, {
            header: "客胜",
            dataIndex: "away1",
            sortable: true,
            width: 15,
            summaryType: 'away',
            summaryRenderer: function (v, params, data) {
                var win = eval(v) * 100;
                if (win > 50) {
                    return "<b><font color=red>" + win.toFixed(1) + "%</font></b>";
                } else {
                    return "<b><font color=green>" + win.toFixed(1) + "%</font></b>";
                }
            }
        }, {
            header: "时间",
            dataIndex: "time1",
            width: 25,
            sortable: true,
            renderer: function (value) {
                return value ? value.dateFormat('Y-m-d H:i') : '';
            }
        }, {
            header: "主胜",
            dataIndex: "home2",
            sortable: true,
            width: 15,
            summaryType: 'home',
            summaryRenderer: function (v, params, data) {
                var win = eval(v) * 100;
                if (win > 50) {
                    return "<b><font color=red>" + win.toFixed(1) + "%</font></b>";
                } else {
                    return "<b><font color=green>" + win.toFixed(1) + "%</font></b>";
                }
            }
        }, {
            header: "平局",
            dataIndex: "draw2",
            sortable: true,
            width: 15,
            summaryType: 'draw',
            summaryRenderer: function (v, params, data) {
                var vic = eval(v) * 100;
                if (vic > 50) {
                    return "<b><font color=red>" + vic.toFixed(1) + "%</font></b>";
                } else {
                    return "<b><font color=green>" + vic.toFixed(1) + "%</font></b>";
                }
            }
        }, {
            header: "客胜",
            dataIndex: "away2",
            sortable: true,
            width: 15,
            summaryType: 'away',
            summaryRenderer: function (v, params, data) {
                var win = eval(v) * 100;
                if (win > 50) {
                    return "<b><font color=red>" + win.toFixed(1) + "%</font></b>";
                } else {
                    return "<b><font color=green>" + win.toFixed(1) + "%</font></b>";
                }
            }
        }, {
            header: "时间",
            dataIndex: "time2",
            width: 25,
            sortable: true,
            renderer: function (value) {
                return value ? value.dateFormat('Y-m-d H:i') : '';
            }
        }, {
            header: "胜负",
            tooltip: "比赛胜负",
            dataIndex: "victory",
            sortable: false,
            width: 15,
            renderer: function (v) {
                if (v == 3) {
                    return "<font color='red'>胜</font>"
                }
                else if (v == 1) {
                    return "<font color='green'>平</font>"
                }
                else {
                    return "<font color='blue'>负</font>"
                }
            }
        }
]);

    var summary = new Ext.ux.grid.HybridSummary();
    //----------------------------------------------------定义grid
    var grid = new Ext.grid.GridPanel({
        store: store,
        cm: cm,
        sm: sm,
        width: 950,
        height: 400,
        loadMask: true,
        stripeRows: true,
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
            columnsText: '显示/隐藏列'
        },
        view: new Ext.grid.GroupingView({
            forceFit: true,
            showGroupName: false,
            enableNoGroups: false,
            enableGroupingMenu: false,
            hideGroupedColumn: true,
            groupTextTpl: '{text} (<b><font color=red>{[values.rs.length]}</font> </b>条)'
        }),
        plugins: summary
    });
    var win = new Ext.Window({
        width: 1024,
        height: 600,
        title: '标准盘',
        layout: 'fit',
        modal: false,
        items: [grid]
    })
    win.show();
    store.load();
}