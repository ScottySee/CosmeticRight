<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <asp:Label ID="MessageBox" CssClass="alert alert-success" Visible="false" runat="server" Text="Label"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="email" Placeholder="email" CssClass="form-control" required></asp:TextBox>
        </div>
        <div class="text-center">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-default submit btn-block" OnClick="btnSubmit_Click"/>
        </div>
    </form>
</body>
</html>
