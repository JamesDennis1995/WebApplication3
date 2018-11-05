<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="WebApplication3.Admin" %>

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
<a href="Add_Stock.aspx">Add Stock</a> <br/>
<a href="Check_Orders_Admin.aspx">Check Orders</a> <br/>
<a href="Change_Admin_Password.aspx">Change Password</a> <br/>
</div>
<div id="main">
<h2> Admin Home </h2>
    <form action="" method="post" runat="server">
    <asp:Label ID="Welcome" runat="server"></asp:Label> <br />
    <asp:Button ID="Logout" Text="Log Out" runat="server" OnClick="Logout_Click" />
        </form>
    </div>
</body>
</html>
