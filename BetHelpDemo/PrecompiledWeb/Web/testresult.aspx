<%@ page language="C#" autoeventwireup="true" inherits="testresult, App_Web_testresult.aspx.cdcab7d2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    平均胜率
    胜率： <%= winCount %>
    总数： <%= totalCount %>

    最高支持
    胜率： <%= winCount1 %>
    总数： <%= totalCount1 %>

    支持公司
    胜率： <%= winCount2 %>
    总数： <%= totalCount2 %>

    按照场次
    胜率： <%= winCount3 %>
    总数： <%= totalCount3 %>
    </div>
    </form>
</body>
</html>
