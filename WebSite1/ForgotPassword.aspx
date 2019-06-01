<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/Guest.master" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div class="page-header header-filter">
        <div class="content">
            <div class="container">
                <h1>Forgot Password</h1>
                <div class="row justify-content-center">
                    <!--First column-->
                    <div class="col-md-4">
                        <div class="card">
                            <div class="card-body">
                                <br />
                                <div class="container">
                                    <asp:Label ID="MessageBox" CssClass="alert alert-success" Visible="false" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="form-group pull-left">
                                    <asp:Label runat="server" ID="label1"><h5>Please enter your email to search for your account.</h5></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="email" Placeholder="Email" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="text-center">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-default submit btn-block" OnClick="btnSubmit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" Runat="Server">
</asp:Content>

