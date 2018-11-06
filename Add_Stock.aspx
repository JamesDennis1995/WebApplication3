<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_Stock.aspx.cs" Inherits="WebApplication3.Add_Stock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cards 101</title>
    <link rel="stylesheet" type="text/css" href="style.css"/>
    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#Preview").prop("hidden", true);
            $("#Preview").prop("style", "height:125px; width:auto;");
            function readURL(input) {
                if (input.files && input.files[0]) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $("#Preview").prop("src", e.target.result);
                    }
                    reader.readAsDataURL(input.files[0]);
                }
            }
            $("#Image").change(function () {
                if ($(this).val() != "") {
                    var ext = $(this).val().match(/\.(.+)$/)[1];
                    if (ext == "jpg") {
                        $("#Preview").prop("hidden", false);
                        readURL(this);
                    }
                    else {
                        $("#Preview").prop("hidden", true);
                    }
                }
                else {
                    $("#Preview").prop("hidden", true);
                }
            });
        });
    </script>
    <div id="header">
<h1>Cards 101</h1>
</div>
</head>
<body>
    <div id="side">
<a href="Admin.aspx">Home</a><br />
Add Stock <br>
<a href="Check_Orders_Admin.aspx">Check Orders</a> <br />
<a href="Change_Admin_Password.aspx">Change Password</a> <br />
</div>
    <div id="main">
        <h2>Add Stock</h2>
        <form action="" method="post" runat="server">
            Code: <asp:TextBox ID="Code" runat="server"></asp:TextBox><br />
            Description: <asp:TextBox ID="Description" runat="server"></asp:TextBox> <br />
            Price per unit (£): <asp:TextBox ID="PriceUnit" runat="server"></asp:TextBox> <br />
            Image: <asp:FileUpload ID="Image" runat="server" /> <br />
            <asp:Image ID="Preview" runat="server" ImageUrl="" /> <br />
            <asp:Label ID="Error" runat="server"></asp:Label><br />
            <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="Submit_Click" />
        </form>
    </div>
</body>
</html>
