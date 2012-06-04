<%@ page language="C#" autoeventwireup="true" inherits="UpdateSchedule, App_Web_updateschedule.aspx.cdcab7d2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="lib/ext/resources/css/ext-all.css" />
    <script src="lib/ext/adapter/ext/ext-base-debug.js" type="text/javascript"></script>
    <script src="lib/ext/ext-all-debug.js" type="text/javascript"></script>
    <script src="Lib/ext/src/locale/ext-lang-zh_CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        var url = '<%= HistoryDataURL %>';
        Ext.onReady(function () {

            Ext.QuickTips.init();

            var start = new Ext.form.DateField({
            id:'startfield',
                width: 150,
                format:'Y-m-d'
            });
            start.render('startDate')
            var end = new Ext.form.DateField({
                id: 'endfield',
                width: 150,
                format: 'Y-m-d',
                value:new Date()
            });
            end.render('endDate')
        });

        function Button1_ClientClick() {
            iscontinue = true;
            var allDate = document.getElementById("allDate");
            var stime = Ext.getCmp("startfield").getValue().getTime();
//            var etime = Ext.getCmp("endfield").getValue().getTime();
//            while (stime <= etime) {
//                new Date(Ext.getCmp("startfield").getValue().getTime() + 24 * 3600 * 1000);
//            }
            var s = document.createElement("script");
            s.type = "text/javascript";
            s.src = url.replace("{0}", Ext.getCmp("startfield").value);
            allDate.removeChild(allDate.firstChild);
            allDate.appendChild(s, "script");
        }
        var iscontinue = true;
        function Button2_ClientClick() {
            iscontinue = false;
        }
        function ShowBf() {
                var upRows = [];
                Ext.each(A, function (mi) {
                    if (B[mi[1]][5] == '1' && (mi[12] == 2 || mi[12] == -1)) {
                        upRows.push(mi);
                    }
                });
                if (matchcount == 0 || upRows.length == 0) {
                    Ext.getDom('result').innerHTML += Ext.getCmp("startfield").value + " 无数据<br />";
                    continueExeu();
                    return;
                }
                Ext.getDom('result').innerHTML += "正在更新 " + Ext.getCmp("startfield").value + "<br />";
                Ext.Ajax.request({
                    url: 'Server.aspx?a=updateSchedules',
                    timeout: 3600000,
                    params: { date: Ext.getCmp("startfield").getValue(), matchs: Ext.util.Format.htmlEncode(upRows.join('^')) },
                    success: function (res) {
                        var result = Ext.decode(res.responseText);
                        if (result.success) {
                            Ext.getDom('result').innerHTML += result.content + "<br />";
                            continueExeu();
                        } else {
                            Ext.getDom('result').innerHTML += " 已经停止更新<br />";
                        }
                    },
                    failure: function (resp) {

                    }
                });
            }
            function continueExeu() {
                if (iscontinue) {
                    Ext.getCmp("startfield").setValue(new Date(Ext.getCmp("startfield").getValue().getTime() + 3600 * 1000 * 24));
                    if (Ext.getCmp("startfield").getValue() < Ext.getCmp("endfield").getValue()) {
                        Button1_ClientClick();
                    } else {
                        Ext.getDom('result').innerHTML += "更新完成<br />";
                        return;
                    }
                } else {
                    Ext.getDom('result').innerHTML += "更新终止<br />";
                }
                
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
                <table>
                    <tr>
                        <td>
                            <div id="startDate">
                            </div>
                            <div id="endDate">
                            </div>
                        </td>
                        <td>
                        </td>
                        <td>
                           <input type="button" onclick="Button1_ClientClick()" value="更新" />
                           <input type="button" onclick="Button2_ClientClick()" value="停止" />
                           <input type="button" onclick="Ext.getDom('result').innerHTML = '';" value="清除" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" id="result">
                            &nbsp;
                        </td>
                    </tr>
                </table>
        <span id="allDate">
            <script language="javascript" type="text/javascript" defer="defer"></script>
        </span>
    </div>
    </form>
</body>
</html>
