<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/Guest.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <script>
        function lettersOnly() {
            var charCode = event.keyCode;

            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32 || charCode == 151)

                return true;
            else
                return false;
        }
        function numbersOnly() {
            var charCode = event.keyCode;

            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32 || charCode == 151)

                return false;
            else
                return true;
        }


    </script>
    <asp:ScriptManager runat="server" ID="script1" />
    <asp:UpdatePanel runat="server" ID="Upd1">

        <ContentTemplate>
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
                                    <br />
                                    <p>fields that are required. (*)</p>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="card-title pull-left">
                                                First Name
                                            </div>
                                            <div style="color: red">*</div>
                                            <div class="form-group">
                                                <asp:TextBox runat="server" ID="firstname" Placeholder="First Name" onkeypress="return lettersOnly(event)" CssClass="form-control" MaxLength="80" required autocomplete="off"></asp:TextBox>
                                            </div>
                                            <div class="card-title pull-left">
                                                Last Name
                                            </div>
                                            <div style="color: red">*</div>
                                            <div class="form-group">
                                                <asp:TextBox runat="server" ID="lastname" Placeholder="Last Name" onkeypress="return lettersOnly(event)" CssClass="form-control" MaxLength="50" required autocomplete="off"></asp:TextBox>
                                            </div>
                                            <div class="card-title pull-left">
                                                Gender
                                            </div>
                                            <div style="color: red">*</div>
                                            <div class="form-group">
                                                <asp:DropDownList ID="gender" runat="server" CssClass="form-control" required autocomplete="off">
                                                    <asp:ListItem Value="" style="color: black">---------------Select Gender---------------</asp:ListItem>
                                                    <asp:ListItem Value="1" style="color: black">Male</asp:ListItem>
                                                    <asp:ListItem Value="2" style="color: black">Female</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="card-title pull-left">
                                                Unit/Building Number
                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox runat="server" ID="buildingno" Placeholder="Building Number" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <!--Second Column-->
                                        <div class="col-md-4">
                                            <div class="card-title pull-left">
                                                Street
                                            </div>
                                            <div style="color: red">*</div>
                                            <div class="form-group">
                                                <asp:TextBox runat="server" ID="street" Placeholder="Street" CssClass="form-control" MaxLength="50" required autocomplete="off"></asp:TextBox>
                                            </div>
                                            <div class="card-title pull-left">
                                                Municipality
                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox runat="server" ID="municipality" Placeholder="Municipality" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
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
                                            <%--  <asp:RequiredFieldValidator runat="server" ControlToValidate="landline" ErrorMessage="Required" CssClass="text-danger">

                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ControlToValidate="landline" runat="server" ValidationExpression=""></asp:RegularExpressionValidator>\--%>
                                            <asp:TextBox runat="server" ID="landline" Placeholder="Phone" CssClass="form-control" MaxLength="12" onkeypress="return numbersOnly(event)" autocomplete="off"></asp:TextBox>
                                        </div>

                                    </div>
                                    <!--Third Column-->
                                    <div class="col-md-4">
                                        <div class="card-title pull-left">
                                            Mobile Number
                                        </div>
                                        <div style="color: red">*</div>
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="mobile" onkeypress="return numbersOnly(event)" Placeholder="Mobile" CssClass="form-control" MaxLength="12" required autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="card-title pull-left">
                                            Email
                                        </div>
                                        <div style="color: red">*</div>
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="email" Placeholder="Email" CssClass="form-control" type="email" MaxLength="80" required autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="card-title pull-left">
                                            Password
                                        </div>
                                        <div style="color: red">*</div>
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="password" Placeholder="Password" CssClass="form-control" TextMode="Password" MaxLength="20" required autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="card-title pull-left">
                                            Confirm Password
                                        </div>
                                        <div style="color: red">*</div>
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="cpassword" Placeholder="Password" CssClass="form-control" TextMode="Password" MaxLength="20" required autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <span class="pull-right">
                                    <asp:Button ID="btnRegister" runat="server" Text="Register" class="btn btn-block btn-success submit btn-block" OnClick="btnRegister_Click" />
                                </span>
                            </div>
                        </div>
                        <%--<a href="javascript:void(0)" class="btn btn-danger btn-round btn-lg">Register</a>--%>
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="main">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <script>
        grecaptcha.ready(function () {
            grecaptcha.execute('6Le2EosUAAAAAAB6E21zXgsTUoTvmoUpamPEBEXL', { action: 'login' })
                .then(function (token) {
                    // Verify the token on the server.
                });
        });
    </script>
</asp:Content>

