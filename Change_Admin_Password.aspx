<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Change_Admin_Password.aspx.cs" Inherits="WebApplication3.Change_Admin_Password" %>

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
<a href="Admin.aspx">Home</a><br/>
<a href="Add_Stock.aspx">Add Stock</a> <br/>
<a href="Check_Orders_Admin.aspx">Check Orders</a> <br />
Change Password <br />
</div>
    <div id="main">
        <form action="" method="post" runat="server">
            Old Password: <asp:TextBox ID="OldPassword" Type="Password" runat="server"></asp:TextBox> <br />
            New Password: <asp:TextBox ID="NewPassword" Type="Password" runat="server"></asp:TextBox><br />
            Confirm New Password: <asp:TextBox ID="ConfirmNewPassword" Type="Password" runat="server"></asp:TextBox><br />
            <asp:Label ID="Error" runat="server"></asp:Label> <br />
            <asp:Button ID="Submit" Text="Submit" runat="server" OnClick="Submit_Click" />
        </form>
    </div>
</body>
</html>
