/// <reference path="../../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../../lib/ext/ext-all-debug.js" />


AddBettingLineFn = function(lineid) {
    var AddBettingLinefp = new Ext.form.FormPanel({
        width: 375,
        height: 210,
        plain: true,
        layout: "form",
        defaultType: "textfield",
        labelWidth: 75,
        baseCls: "x-plain",
        //锚点布局-
        defaults: { anchor: "95%", msgTarget: "side" },
        buttonAlign: "center",
        bodyStyle: "padding:0 0 0 0",
        items: [
			{
			    name: "lineid",
			    fieldLabel: "<font color=red>投资名称</font>",
			    allowBlank: true
			    
			}, {
			    name: "teamname",
			    fieldLabel: "球队名称",
			    allowBlank: false,
			    blankText: "球队名称不允许为空"
			}, {
			    name: "traditional",
			    fieldLabel: "繁体名称",
			    allowBlank: false,
			    blankText: "繁体名称不允许为空"
			}, {
			    name: "date",
			    xtype: "datefield",
			    fieldLabel: "日期",
			    allowBlank: false,
			    regexText: "当天日期不允许空"
			}, {
			    name: "starttime",
			    xtype: "timefield",
			    fieldLabel: "开场时间",
			    allowBlank: false,
			    regexText: "开场时间不允许为空"

			}, {
			    name: "endtime",
			    xtype: "timefield",
			    fieldLabel: "完场时间",
			    allowBlank: false,
			    regexText: "完场时间不允许为空"
			}, {
			    xtype: 'radiogroup',
			    fieldLabel: '投注项目',
			    items: [
                { boxLabel: '让球', name: 'rb-auto', inputValue: "01" ,checked: true},
                { boxLabel: '大小球', name: 'rb-auto', inputValue: "02"}
            ]
			}, {
			    name: "detailid",
			    fieldLabel: "投注细节",
			    allowBlank: false,
			    regexText: "投注细节必填"
			}, {
			    name: "odds",
			    xtype: "numberfield",
			    fieldLabel: "赔率",
			    allowBlank: false,
			    regexText: "赔率不允许为空"
			}, {
			    name: "betmoney",
			    xtype: "numberfield",
			    fieldLabel: "投注金额",
			    allowBlank: true,
			    regex: /^[\s\S]{1,50}$/,
			    regexText: "投注金额"
}]
    });


    var AddBettingLineWin = new Ext.Window({
        title: "添加投注信息",
        width: 410,
        height: 300,
        plain: true,
        iconCls: "addicon",
        //不可以随意改变大小
        resizable: false,
        //是否可以拖动
        //draggable:false,
        defaultType: "textfield",
        labelWidth: 100,
        collapsible: true, //允许缩放条
        closeAction: 'hide',
        closable: true,
        plain: true,
        //弹出模态窗体
        modal: 'true',
        buttonAlign: "center",
        bodyStyle: "padding:10px 0 0 15px",
        items: [AddBettingLinefp],
        listeners: {
            "show": function() {
                //当window show事件发生时清空一下表单
                AddBettingLinefp.getForm().reset();
            }
        },
        buttons: [{
            text: "保存信息",
            minWidth: 70,
            handler: function() {
                if (AddBettingLinefp.getForm().isValid()) {
                    //弹出效果
                    Ext.MessageBox.show
                                (
                                    {
                                        msg: '正在保存，请稍等...',
                                        progressText: 'Saving...',
                                        width: 300,
                                        wait: true,
                                        waitConfig: { interval: 200 },
                                        icon: 'download',
                                        animEl: 'saving'
                                    }
                                );
                    setTimeout(function() {
                        Ext.MessageBox.hide();
                    }, 1000);
                    AddBettingLinefp.form.submit({
                        url: "url/member/AddBettingLine.aspx",
                        method: "POST",
                        params: {
                            lineid: lineid
                        },
                        success: function(form, action) {
                            //成功后
                            var flag = action.result.success;
                            if (flag == "true") {
                                //填写开房房间信息
                                AddBettingLineWin.hide();
                                BettingLinestore.reload();
                            }
                        },
                        failure: function(form, action) {
                            Ext.MessageBox.alert("提示!", "保存成员信息失败!");
                        }
                    });
                }
            }
        }, {
            text: "重置",
            minWidth: 70,
            qtip: "重置数据",
            handler: function() {
                AddBettingLinefp.getForm().reset();
            }
        }, {
            text: "取 消",
            minWidth: 70,
            handler: function() {
                AddBettingLineWin.hide();
            }
}]

        });

        AddBettingLineWin.show();
    }