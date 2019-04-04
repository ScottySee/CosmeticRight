<%@ Page Title="" Language="C#" MasterPageFile="~/Member.master" AutoEventWireup="true" CodeFile="EditMemberProfile.aspx.cs" Inherits="EditMemberProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <form runat="server" class="form-horizontal">
        <div class="page-header header-filter">
            <div class="content">
                <div class="container">
                    <div class="row">
                        <div class="container">
                            <asp:Label ID="Label2" CssClass="alert alert-success" Visible="false" runat="server" Text="Label"></asp:Label>

                        </div>

                        <!--First column-->
                        <div class="card card-register m-5">
                            <div class="card-header">
                                <img class="card-img" src="bootstrap/assets/img/square1.png" alt="Card image">
                                <h4 class="card-title">Register</h4>
                            </div>

                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-4">

                                        <div class="card-title pull-left">
                                            First Name
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="firstname" placeholder="First Name" CssClass="form-control" required></asp:TextBox>
                                        </div>
                                        <div class="card-title pull-left">
                                            Last Name
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="lastname" placeholder="Last Name" CssClass="form-control" required></asp:TextBox>
                                        </div>
                                        <div class="card-title pull-left">
                                            Gender
                                        </div>
                                        <div class="form-group">
                                            <asp:DropDownList ID="gender" runat="server" CssClass="form-control" required>
                                                <asp:ListItem Value="" style="color: black">---------------Select Gender---------------</asp:ListItem>
                                                <asp:ListItem Value="1" style="color: black">Male</asp:ListItem>
                                                <asp:ListItem Value="2" style="color: black">Female</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="card-title pull-left">
                                            Unit/Building Number
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="buildingno" placeholder="Building Number" CssClass="form-control"></asp:TextBox>
                                        </div>


                                    </div>
                                    <!--Second Column-->
                                    <div class="col-md-4">


                                        <div class="card-title pull-left">
                                            Street
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="street" placeholder="Street" CssClass="form-control" required></asp:TextBox>
                                        </div>
                                        <div class="card-title pull-left">
                                            Municipality
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="municipality" placeholder="Barangay" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="card-title pull-left">
                                                City
                                            </div>
                                            <div style="color: red">*</div>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" required />
                                            </div>
                                        <div class="card-title pull-left">
                                            Landline
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="landline" placeholder="Phone" CssClass="form-control"></asp:TextBox>
                                        </div>

                                    </div>
                                    <!--Third Column-->
                                    <div class="col-md-4">
                                        <div class="card-title pull-left">
                                            Mobile Number
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="mobile" placeholder="Mobile" CssClass="form-control" required></asp:TextBox>
                                        </div>
                                        <div class="card-title pull-left">
                                            Email
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="email" placeholder="Username" CssClass="form-control" required></asp:TextBox>
                                        </div>
                                        <%--<div class="card-title pull-left">
                                            Password
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="password" type="password" placeholder="Password" CssClass="form-control" required></asp:TextBox>
                                        </div>--%>
<%--                                        <div class="card-title pull-left">
                                            Confirm Password
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="cpassword" type="password" placeholder="Password" CssClass="form-control" required></asp:TextBox>
                                        </div>--%>

                                    </div>
                                </div>
                                <span class="pull-right">
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn btn-block btn-success submit btn-block" OnClick="btnUpdate_Click" />
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
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

