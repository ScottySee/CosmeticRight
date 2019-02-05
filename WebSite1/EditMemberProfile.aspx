<%@ Page Title="" Language="C#" MasterPageFile="~/Member.master" AutoEventWireup="true" CodeFile="EditMemberProfile.aspx.cs" Inherits="EditMemberProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <div class="page-header header-filter">
        <div class="content">
            <div class="container">
                <div class="row">
                    <div class="container">
                        <asp:label id="Label2" cssclass="alert alert-success" visible="false" runat="server" text="Label"></asp:label>

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
                                        <asp:textbox runat="server" id="firstname" placeholder="First Name" cssclass="form-control" required></asp:textbox>
                                    </div>
                                    <div class="card-title pull-left">
                                        Last Name
                                    </div>
                                    <div class="form-group">
                                        <asp:textbox runat="server" id="lastname" placeholder="Last Name" cssclass="form-control" required></asp:textbox>
                                    </div>
                                    <div class="card-title pull-left">
                                        Gender
                                    </div>
                                    <div class="form-group">
                                        <asp:dropdownlist id="gender" runat="server" cssclass="form-control" required>
                                            <asp:ListItem Value="" style="color: black">---------------Select Gender---------------</asp:ListItem>
                                            <asp:ListItem Value="1" style="color: black">Male</asp:ListItem>
                                            <asp:ListItem Value="2" style="color: black">Female</asp:ListItem>
                                        </asp:dropdownlist>
                                    </div>
                                    <div class="card-title pull-left">
                                        Unit/Building Number
                                    </div>
                                    <div class="form-group">
                                        <asp:textbox runat="server" id="buildingno" placeholder="Building Number" cssclass="form-control"></asp:textbox>
                                    </div>


                                </div>
                                <!--Second Column-->
                                <div class="col-md-4">


                                    <div class="card-title pull-left">
                                        Street
                                    </div>
                                    <div class="form-group">
                                        <asp:textbox runat="server" id="street" placeholder="Street" cssclass="form-control" required></asp:textbox>
                                    </div>
                                    <div class="card-title pull-left">
                                        Municipality
                                    </div>
                                    <div class="form-group">
                                        <asp:textbox runat="server" id="municipality" placeholder="Barangay" cssclass="form-control"></asp:textbox>
                                    </div>
                                    <div class="card-title pull-left">
                                        City
                                    </div>
                                    <div class="form-group">
                                        <asp:textbox runat="server" id="city" placeholder="City" cssclass="form-control"></asp:textbox>
                                    </div>
                                    <div class="card-title pull-left">
                                        Landline
                                    </div>
                                    <div class="form-group">
                                        <asp:textbox runat="server" id="landline" placeholder="Phone" cssclass="form-control"></asp:textbox>
                                    </div>

                                </div>
                                <!--Third Column-->
                                <div class="col-md-4">
                                    <div class="card-title pull-left">
                                        Mobile Number
                                    </div>
                                    <div class="form-group">
                                        <asp:textbox runat="server" id="mobile" placeholder="Mobile" cssclass="form-control" required></asp:textbox>
                                    </div>
                                    <div class="card-title pull-left">
                                        Email
                                    </div>
                                    <div class="form-group">
                                        <asp:textbox runat="server" id="email" placeholder="Username" cssclass="form-control" required></asp:textbox>
                                    </div>
                                    <div class="card-title pull-left">
                                        Password
                                    </div>
                                    <div class="form-group">
                                        <asp:textbox runat="server" id="password" placeholder="Password" cssclass="form-control" required></asp:textbox>
                                    </div>
                                    <div class="card-title pull-left">
                                        Confirm Password
                                    </div>
                                    <div class="form-group">
                                        <asp:textbox runat="server" id="cpassword" placeholder="Password" cssclass="form-control" required></asp:textbox>
                                    </div>

                                </div>
                            </div>
                            <div class="form-group">
                                <asp:button id="btnUpdate" runat="server" text="Update" class="btn btn-block btn-success submit btn-block" onclick="btnUpdate_Click" />
                            </div>
                        </div>
                    </div>
                    <%--<a href="javascript:void(0)" class="btn btn-danger btn-round btn-lg">Register</a>--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

