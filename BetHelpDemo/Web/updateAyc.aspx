<%@ Page Language="C#" AutoEventWireup="true" CodeFile="updateAyc.aspx.cs" Inherits="updateAyc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="lib/ext/resources/css/ext-all.css" />
    <script src="lib/ext/adapter/ext/ext-base-debug.js" type="text/javascript"></script>
    <script src="lib/ext/ext-all-debug.js" type="text/javascript"></script>
    <script src="Js/Algorithm.js" type="text/javascript"></script>
    <script type="text/javascript">
        var allData = <%= allData %>;
        var foreData = <%= foreData %>;
        
        Ext.each(allData,function(exp){
            var fields = [{ name: 'time', dateFormat: 'Y-m-d H:i' },
            { name: 'avehome', type: 'float' },
            { name: 'avedraw', type: 'float' },
            { name: 'aveaway', type: 'float' },
            { name: 'varhome', type: 'float' },
            { name: 'vardraw', type: 'float' },
            { name: 'varaway', type: 'float' },
            { name: 'returnrate', type: 'float'}];

            var store = new Ext.data.Store({
                
                reader: new Ext.data.JsonReader(
               {
                   fields: fields
               })
           });
           store.loadData(Ext.decode(exp.data));
           var rowsData=[];
       Ext.each(foreData,function(fore){
                var resultArray = eval(fore.content + "(store)");
                rowsData.push(fore.content+"("+resultArray[3]+")");
       })
       Ext.Ajax.request({
                url: 'Data/NowGoal/BetExp.aspx?a=saveanalysis',
                params: { matchid: exp.id, data: rowsData.join(',') },
                success: function (res) {
                }
            });
        });




       


    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    </form>
</body>
</html>
