﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Guest.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" ValidateRequest="false" %>

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
                                    <asp:label id="Label2" runat="server" text="Label"></asp:label>
                                </div>
                                <div class="alert alert-danger" id="error" runat="server" visible="false">
                                    <strong>Invalid Email or Password Or Email not verified.</strong>
                                </div>
                                <br />
                                <div class="form-group">
                                    <asp:textbox runat="server" id="email" placeholder="Email" cssclass="form-control" type="email" maxlength="80"></asp:textbox>
                                </div>
                                <div class="form-group">
                                    <asp:textbox runat="server" id="password" type="password" placeholder="Password" cssclass="form-control" maxlength="20"></asp:textbox>
                                </div>
                                <div class="form-group">
                                    <asp:textbox runat="server" id="txtstatus" cssclass="form-control" maxlength="20" Visible="false"></asp:textbox>
                                </div>
                                <div class="g-recaptcha" data-sitekey="6LfAbqUUAAAAALSwsXhZ-dTJIsNujGLIsmLvB3Pa"></div>
                                <div class="text-center">
                                    <asp:button id="btnLogin" runat="server" text="Login" class="btn btn-default submit btn-block" onclick="BtnLogin_Click" />
                                </div>
                            </div>
                        </div>
                        <a href="ForgotPassword.aspx">Forgot Password?</a>
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

