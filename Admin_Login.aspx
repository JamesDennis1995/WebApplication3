<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_Login.aspx.cs" Inherits="WebApplication3.Admin_Login" %>

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
<a href="Start.aspx">Start</a><br>
<a href="Create_Account.aspx">New User</a><br/>
<a href="Login.aspx">Existing User</a> <br/>
Admin <br>
</div>
    <div id="main">
    <form action="" method="post" runat="server">
    Password <asp:TextBox ID="AdminPassword" Type="Password" runat="server"></asp:TextBox> <br />
        <asp:Label ID="Error" runat="server"></asp:Label> <br />
        <asp:Button ID="Login" runat="server" Text="Login" OnClick="Login_Click" />
    </form>
    </div>
</body>
</html>
