<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create_Account.aspx.cs" Inherits="WebApplication3.Create_Account" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cards 101</title>
    <link rel="stylesheet" type="text/css" href="style.css">
    <div id="header">
        <h1>Cards 101</h1>
    </div>
</head>
<body>
    <div id="side">
<a href="Start.aspx">Start</a><br/>
New User<br/>
<a href="Login.aspx">Existing User</a> <br/>
<a href="Admin_Login.aspx">Admin</a> <br/>
    </div>
    <div id="main">
        <h2>Create an Account</h2>
        <form action="" method="post" runat="server">
            First Name: <asp:TextBox ID="FirstName" runat="server"></asp:TextBox><br />
            Surname: <asp:TextBox ID="Surname" runat="server"></asp:TextBox><br />
            Password: <asp:TextBox ID="Password" runat="server" Type="Password"></asp:TextBox><br />
            Contact Number: <asp:TextBox ID="ContactNumber" runat="server"></asp:TextBox><br />
            Email Address: <asp:TextBox ID="Email" runat="server" Type="Email"></asp:TextBox><br/>
            Address Line 1: <asp:TextBox ID="Address1" runat="server"></asp:TextBox><br/>
            Address Line 2: <asp:TextBox ID="Address2" runat="server"></asp:TextBox><br/>
            Town/City: <asp:TextBox ID="TownCity" runat="server"></asp:TextBox><br/>
            County: <asp:TextBox ID="County" runat="server"></asp:TextBox><br/>
            Postcode: <asp:TextBox ID="Postcode" runat="server"></asp:TextBox><br/>

            <asp:Label ID="Error" runat="server"></asp:Label>
            <br />

            <asp:Button ID="CreateAccount" runat="server" Text="Submit" OnClick="CreateAccount_Click" />
        </form>
    </div>
</body>
</html>
