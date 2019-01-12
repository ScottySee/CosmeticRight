<%@ Page Title="" Language="C#" MasterPageFile="~/Guest.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <div class="page-header header-filter">
        <div class="content">
            <div class="container">
                <h1>Registration</h1>
                <div class="container">
                    <asp:Label ID="Label2" CssClass="alert alert-success" Visible="false" runat="server" Text="Label"></asp:Label>

                </div>
                <div class="row">
                    <!--First column-->
                    <div class="col-md-4">
                        <div class="card">
                            <div class="card-body">
                                <div class="card-title pull-left">
                                    First Name
                                </div>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="firstname" Placeholder="First Name" CssClass="form-control" required></asp:TextBox>
                                </div>
                                <div class="card-title pull-left">
                                    Last Name
                                </div>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="lastname" Placeholder="Last Name" CssClass="form-control" required></asp:TextBox>
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
                                    <asp:TextBox runat="server" ID="buildingno" Placeholder="Building Number" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--Second Column-->
                    <div class="col-md-4">
                        <div class="card">
                            <div class="card-body">
                                <div class="card-title pull-left">
                                    Street
                                </div>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="street" Placeholder="Street" CssClass="form-control" required></asp:TextBox>
                                </div>
                                <div class="card-title pull-left">
                                    Municipality
                                </div>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="municipality" Placeholder="Barangay" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="card-title pull-left">
                                    City
                                </div>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="city" Placeholder="City" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="card-title pull-left">
                                    Landline
                                </div>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="landline" Placeholder="Phone" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--Third Column-->
                    <div class="col-md-4">
                        <div class="card">
                            <div class="card-body">
                                <div class="card-title pull-left">
                                    Mobile Number
                                </div>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="mobile" Placeholder="Mobile" CssClass="form-control" required></asp:TextBox>
                                </div>
                                <div class="card-title pull-left">
                                    Email
                                </div>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="email" Placeholder="Username" CssClass="form-control" required></asp:TextBox>
                                </div>
                                <div class="card-title pull-left">
                                    Password
                                </div>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="password" Placeholder="Password" CssClass="form-control" required></asp:TextBox>
                                </div>
                                <div class="card-title pull-left">
                                    Confirm Password
                                </div>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="cpassword" Placeholder="Password" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="text-center">
                    <asp:Button ID="btnRegister" runat="server" Text="Register" class="btn btn-default submit btn-block" OnClick="btnRegister_Click" />
                </div>
                <%--<a href="javascript:void(0)" class="btn btn-danger btn-round btn-lg">Register</a>--%>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

