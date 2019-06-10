<%@ Page Title="" Language="C#" MasterPageFile="~/Guest.master" AutoEventWireup="true" CodeFile="EmailVerification.aspx.cs" Inherits="EmailVerification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="page-header header-filter">
        <div class="content">
            <div class="container">
                <h1>Confirming Email</h1>
                <p>Click the button below to verify your email. Once you have clicked the button, you will be redirected to the login page and you can now access your account through our website by entering your registered email address and password.</p>
                <div class="btn btn-sm">
                    <asp:Button ID="btnConfirm" runat="server" Text="Confirm" class="btn btn-block btn-success submit btn-block" OnClick="btnConfirm_Click" />
                </div>


                <%--<p>Thank you for verifying your email. You can now access your account through our website by entering your registered email address and password.</p>--%>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

