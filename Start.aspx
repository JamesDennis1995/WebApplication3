<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Start.aspx.cs" Inherits="WebApplication3.Start" %>
<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8" />
    <title>Cards 101</title>
    <link rel="stylesheet" type="text/css" href="style.css">
    <div id="header">
        <h1>Cards 101</h1>
    </div>
</head>
<body>
    <div id="side">
        <a href="Create_Account.aspx">New User</a><br/>
        <a href="Login.aspx">Existing User</a><br />
        <a href="Admin_Login.aspx">Admin</a>
    </div>
    <div id="main">
        Welcome to Cards 101.
    </div>
</body>
</html>
