<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="WebApplication3.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cards 101</title>
    <link rel="stylesheet" type="text/css" href="style.css"/>
    <div id="header">
<h1>Cards 101</h1>
</div>
</head>
<body>
    <div id="side">
Home<br>
<a href="Order.aspx">Place an order</a> <br />
<a href="Check_Orders.aspx">Check your orders</a> <br />
<a href="Edit_Details.aspx">Edit your details</a> <br />
</div>
    <div id="main">
        <form action="" method="post" runat="server">
        <asp:Label ID="Welcome" runat="server"><%=Session["Message"] %></asp:Label> <br />
        <asp:Button ID="Logout" Text="Log Out" runat="server" OnClick="Logout_Click" />
            </form>
    </div>
</body>
</html>
