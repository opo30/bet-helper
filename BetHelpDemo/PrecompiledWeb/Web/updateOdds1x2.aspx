<%@ page language="C#" autoeventwireup="true" inherits="updateData, App_Web_updateodds1x2.aspx.cdcab7d2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="lib/ext/resources/css/ext-all.css" />
    <script src="lib/ext/adapter/ext/ext-base-debug.js" type="text/javascript"></script>
    <script src="lib/ext/ext-all-debug.js" type="text/javascript"></script>
    <script src="Js/Algorithm.js" type="text/javascript"></script>
    <script type="text/javascript" >
        var mstop = false;
        function updateMatchs() {
            Ext.Ajax.request({
                url: 'Server.aspx?a=updateMatchs',
                timeout: 3600000,
                params: {},
                success: function (resp) {
                    if (Ext.getDom('match_td').innerHTML.length > 4000) {
                        Ext.getDom('match_td').innerHTML = '';
                    }
                    var result = Ext.decode(resp.responseText);
                    Ext.getDom('match_td').innerHTML += result.content + " <br />";
                    if (mstop && result.success) {
                        Ext.getDom('match_td').innerHTML += "已终止 <br />";
                        document.getElementById('Button1').disabled = false;
                    } else {
                        updateMatchs()
                    }
                },
                failure: function (resp) {

                }
            });
        }


        var astop = false;
        
        function updateAnalysiss() {
            Ext.Ajax.request({
                url: 'Server.aspx?a=updateAnalysiss',
                timeout: 3600000,
                params: {},
                success: function (resp) {
                    if (Ext.getDom('analysis_td').innerHTML.length > 4000) {
                        Ext.getDom('analysis_td').innerHTML = '';
                    }
                    var result = Ext.decode(resp.responseText);
                    Ext.getDom('analysis_td').innerHTML += result.content + " <br />";
                    if (astop && result.success) {
                        Ext.getDom('analysis_td').innerHTML += "已终止 <br />";
                        document.getElementById('Button3').disabled = false;
                    } else {
                        updateAnalysiss()
                    }
                },
                failure: function (resp) {

                }
            });
            
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;" border="1">
            <tr>
                <td style="width:50%">
                    &nbsp;
                    更新数据</td>
                <td style="width:50%">
                    &nbsp;
                    更新分析</td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                    <input id="Button1" 
                        onclick="mstop = false;this.disabled=true;document.getElementById('Button2').disabled=false;updateMatchs();" 
                        type="button" value="开始" />
                    <input id="Button2" disabled="disabled" onclick="mstop=true;this.disabled=true;" 
                        type="button" value="停止" />
                    <input id="Button5" onclick="Ext.getDom('match_td').innerHTML = '';" 
                        type="button" value="清除" /></td>
                <td>
                    &nbsp;
                    <input id="Button3" 
                        onclick="astop = false;this.disabled=true;document.getElementById('Button4').disabled=false;updateAnalysiss()" 
                        type="button" value="开始" />
                    <input id="Button4" disabled="disabled" onclick="astop=true;this.disabled=true;" 
                        type="button" value="停止" /><input id="Button6" onclick="Ext.getDom('analysis_td').innerHTML = '';" 
                        type="button" value="清除" /></td>
            </tr>
            <tr>
                <td id="match_td" style="vertical-align:top;">
                    &nbsp;</td>
                <td id="analysis_td" style="vertical-align:top;">
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
