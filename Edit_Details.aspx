<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit_Details.aspx.cs" Inherits="WebApplication3.Edit_Details" %>

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
<a href="Customer.aspx">Home</a><br/>
<a href="Order.aspx">Place an order</a> <br/>
<a href="Check_Orders.aspx">Check your orders</a> <br/>
Edit your details <br/>
</div>
    <div id="main">
        <form action="" method="post" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
            First Name: <asp:TextBox ID="FirstName" runat="server"></asp:TextBox><br />
            Surname: <asp:TextBox ID="Surname" runat="server"></asp:TextBox><br />
            Password: <asp:TextBox ID="Password" runat="server" Type="Password"></asp:TextBox><br />
            Contact Number: <asp:TextBox ID="ContactNumber" runat="server"></asp:TextBox><br />
            Email Address: <asp:TextBox ID="Email" runat="server" Type="Email"></asp:TextBox><br/>
            Address Line 1: <asp:TextBox ID="Address1" runat="server"></asp:TextBox><br/>
            Address Line 2: <asp:TextBox ID="Address2" runat="server"></asp:TextBox> <asp:CheckBox ID="LeaveBlank" Text="Leave blank" AutoPostBack="true" runat="server" OnCheckedChanged="LeaveBlank_CheckedChanged" /><br/>
            Town/City: <asp:TextBox ID="TownCity" runat="server"></asp:TextBox><br/>
            County: <asp:TextBox ID="County" runat="server"></asp:TextBox><br/>
            Postcode: <asp:TextBox ID="Postcode" runat="server"></asp:TextBox><br/>
                    <asp:Label ID="Error" runat="server"></asp:Label><br />
                    </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="LeaveBlank" EventName="CheckedChanged" />
                </Triggers>
                </asp:UpdatePanel>
            <asp:Button ID="Submit" Text="Submit" runat="server" OnClick="Submit_Click" />
            </form>
    </div>
</body>
</html>
