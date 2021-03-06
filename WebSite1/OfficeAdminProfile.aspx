﻿<%@ Page Title="" Language="C#" MasterPageFile="~/OfficeAdmin.master" AutoEventWireup="true" CodeFile="OfficeAdminProfile.aspx.cs" ValidateRequest="false" Inherits="OfficeAdminProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
    <form runat="server" class="form-horizontal">
        <div class="page-header header-filter">
            <div class="content">
                <div class="container">
                    <div class="row">
                        <div class="container">
                            <asp:Label ID="Label1" CssClass="alert alert-success" Visible="false" runat="server" Text="Label"></asp:Label>
                        </div>
                        <!--First column-->
                        <div class="card card-register m-5">
                            <div class="card-header">
                                <img class="card-img" src="bootstrap/assets/img/square1.png" alt="Card image">
                                <h4 class="card-title">Profile</h4>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="control-label">First Name</label>
                                            <asp:Label ID="firstname" runat="server" class="form-control" />
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Last Name</label>
                                            <asp:Label ID="lastname" runat="server" class="form-control" />
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Gender</label>
                                            <asp:Label ID="gender" runat="server" class="form-control" />
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Unit/Building Number</label>
                                            <asp:Label ID="buildingno" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                    <!--Second Column-->
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="control-label">Street</label>
                                            <asp:Label ID="street" runat="server" class="form-control" />
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Municipality</label>
                                            <asp:Label ID="municipality" runat="server" class="form-control" />
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">City</label>
                                            <asp:Label ID="city" runat="server" class="form-control" />
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Landline</label>
                                            <asp:Label ID="landline" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                    <!--Third Column-->
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="control-label">Mobile Number</label>
                                            <asp:Label ID="mobile" runat="server" class="form-control" />
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Email</label>
                                            <asp:Label ID="email" runat="server" class="form-control" />
                                        </div>
                                        <%--<div class="form-group">
                                            <label class="control-label">Password</label>
                                            <asp:Label ID="password" type="password" runat="server" class="form-control" />
                                        </div>--%>
                                    </div>
                                </div>
                                <span class="pull-right">
                                    <asp:Button ID="btnEdit" runat="server" class="btn btn-success"
                                        Text="Edit" OnClick="btnEdit_Click" />
                                </span>
                            </div>
                        </div>
                        <%--<a href="javascript:void(0)" class="btn btn-danger btn-round btn-lg">Register</a>--%>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" Runat="Server">
</asp:Content>

