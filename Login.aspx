<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication3.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cards 101</title>
    <link rel="stylesheet" type="text/css" href="style.css">
    <div id="header">
        <h1>Cards 101</h1>
    </div>
</head>
<div id="side">
<a href="Start.aspx">Start</a><br/>
<a href="Create_Account.aspx">New User</a><br/>
Existing User <br>
<a href="Admin_Login.aspx">Admin</a> <br/>
</div>
    <div id="main">
        <form action="" method="post" runat="server">
    Email: <asp:TextBox ID="Email" runat="server"></asp:TextBox> <br />
    Password: <asp:TextBox ID="Password" Type="Password" runat="server"></asp:TextBox> <br />
        <asp:Label ID="Error" runat="server"></asp:Label> <br />
        <asp:Button ID="CustomerLogin" runat="server" Text="Login" OnClick="CustomerLogin_Click" />
    </form>
    </div>
</body>
</html>
