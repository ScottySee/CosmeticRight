<%@ Page Title="" Language="C#" MasterPageFile="~/Guest.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <div class="page-header header-filter">
        <div class="content">
            <div class="container">
                <h1>Login</h1>
                <div class="row justify-content-center">
                    <!--First column-->
                    <div class="col-md-4">
                        <div class="card">
                            <div class="card-body">
                                <div class="alert alert-success" id="success" runat="server" visible="false">
                                    <strong>Login Successful.</strong>
                                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="error" runat="server" visible="false">
                                    <strong>Invalid Username or Password.</strong>
                                </div>
                                <br />
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="email" Placeholder="Email" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="password" type="password" Placeholder="Password" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="text-center">
                                    <asp:Button ID="btnLogin" runat="server" Text="Login" class="btn btn-default submit btn-block" OnClick="BtnLogin_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

