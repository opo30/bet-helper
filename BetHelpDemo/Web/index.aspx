<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" 
            EnableModelValidation="True" AutoGenerateColumns="False" DataKeyNames="id">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" 
                    ReadOnly="True" SortExpression="id" />
                <asp:BoundField DataField="companyID" HeaderText="companyID" 
                    SortExpression="companyID" />
                <asp:BoundField DataField="scheduleID" HeaderText="scheduleID" 
                    SortExpression="scheduleID" />
                <asp:BoundField DataField="home" HeaderText="home" SortExpression="home" />
                <asp:BoundField DataField="draw" HeaderText="draw" SortExpression="draw" />
                <asp:BoundField DataField="away" HeaderText="away" SortExpression="away" />
                <asp:BoundField DataField="time" HeaderText="time" SortExpression="time" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="Data Source=.\SQLEXPRESS;AttachDbFilename=E:\SourceCode\BetHelpDemo\Web\App_Data\Database.mdf;Integrated Security=True;User Instance=True" 
            ProviderName="System.Data.SqlClient" 
            SelectCommand="SELECT top 10 * FROM [odds_bz]"></asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
