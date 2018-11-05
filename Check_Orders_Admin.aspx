<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Check_Orders_Admin.aspx.cs" Inherits="WebApplication3.Check_Orders_Admin" %>

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
Check Orders <br/>
<a href="Change_Admin_Password.aspx">Change Password</a> <br/>
</div>
    <div id="main">
        <form action="" method="post" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="OrderNumbers" AutoPostBack="true" AppendDataBoundItems="true" runat="server" OnSelectedIndexChanged="OrderNumbers_SelectedIndexChanged"></asp:DropDownList><br />
                    <b>Placed by:</b> <asp:Label ID="Name" runat="server"></asp:Label> <br />
                    <b>Address:</b> <asp:Label ID="Address" runat="server"></asp:Label> <br />
                    <b>Contact Number:</b> <asp:Label ID="Number" runat="server"></asp:Label> <br />
                    <b>Order Total:</b> <asp:Label ID="Total" runat="server"></asp:Label> <br />
                    <asp:GridView ID="Items" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:ImageField DataImageUrlField="Image" HeaderText="Image" ControlStyle-Height="100">

                              </asp:ImageField>
                              <asp:BoundField HeaderText="Stock Code" DataField="Stock Code" />
                            <asp:BoundField HeaderText="Description" DataField="Description" />
                              <asp:BoundField HeaderText="Quantity" DataField="Quantity" />
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="OrderNumbers" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </form>
    </div>
</body>
</html>
