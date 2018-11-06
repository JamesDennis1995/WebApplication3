<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="WebApplication3.Order" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script>
$(document).ready(function(){
$("#flip").click(function(){
$("#panel").slideToggle("slow");
});
});
</script>
    <title>Cards 101</title>
    <link rel="stylesheet" type="text/css" href="style.css">
    <div id="header">
        <h1>Cards 101</h1>
    </div>
</head>
<body>
    <div id="side">
    <a href="Customer.aspx">Home</a><br/>
Place an order <br/>
<a href="Check_Orders.aspx">Check your orders</a> <br/>
<a href="Edit_Details.aspx">Edit your details</a> <br/>
</div>
    <div id="main">
      <form action="" method="post" runat="server">
          <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
                  <asp:DropDownList ID="Stock" runat="server" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="Stock_SelectedIndexChanged">
                  </asp:DropDownList><br />
                  <asp:Image ID="Image" runat="server" ImageUrl="" /> <br />
                  <b>Description: </b><asp:Label ID="Description" runat="server"></asp:Label> <br />
                  <b>Price per unit: </b><asp:Label ID="Price" runat="server"></asp:Label> <br />
                  <b>Quantity:</b> <asp:TextBox ID="Quantity" Type="Number" runat="server"></asp:TextBox> <br />
                  <asp:Label ID="Error" runat="server"></asp:Label> 
                  <div id="flip">Show/hide basket</div>
                  <div id="panel">
                      <asp:GridView ID="OrderItems" runat="server" AutoGenerateColumns="False">
                          <Columns>
                              <asp:ImageField DataImageUrlField="Image" HeaderText="Image" ControlStyle-Height="100">
                              </asp:ImageField>
                              <asp:BoundField HeaderText="Stock Code" DataField="Stock Code" />
                              <asp:BoundField HeaderText="Quantity" DataField="Quantity" />
                              <asp:BoundField HeaderText="Total Price" DataField="Total Price" />
                          </Columns>
                      </asp:GridView>
                  </div><br />
                  <b>Remove item(s)</b> <asp:DropDownList ID="ItemToRemove" runat="server" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ItemToRemove_SelectedIndexChanged"></asp:DropDownList> <asp:DropDownList ID ="NumberToRemove" runat="server" AutoPostBack="true" AppendDataBoundItems="true"></asp:DropDownList> <asp:Button ID="Remove" runat="server" Text="Remove" OnClick="Remove_Click" /> <br />
                  <asp:Button ID="AddToBasket" runat="server" Text="Add to Basket" OnClick="AddToBasket_Click" /><asp:Button ID="PlaceOrder" runat="server" Text="Place Order" OnClick="PlaceOrder_Click" />
              </ContentTemplate>
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="Stock" EventName="SelectedIndexChanged" />
                  <asp:AsyncPostBackTrigger ControlID="ItemToRemove" EventName="SelectedIndexChanged" />
              </Triggers>
          </asp:UpdatePanel>
              
      </form>
    </div>
</body>
</html>
