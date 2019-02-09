<%@ Page Title="" Language="C#" MasterPageFile="~/Member.master" AutoEventWireup="true" CodeFile="Feedback.aspx.cs" Inherits="Feedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <div class="header bg-gradient-gray-dark pb-5 pt-5 pt-md-8">
        <div class="container-fluid" id="home">
            <div class="header-body">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <form runat="server">
        <div class="page-header header-filter">
            <div class="content">
                <div class="container">
                    <h1>Feedback</h1>
                    <div class="row justify-content-center">
                        <!--First column-->
                        <div class="col-md-4">
                            <div class="card">
                                <div class="card-body">
                                    <br />
                                    <div class="form-group">
                                        <asp:TextBox runat="server" ID="email" Placeholder="Email" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox runat="server" ID="password" type="password" Placeholder="Password" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <%--<div class="text-center">
                                        <asp:Button ID="btnLogin" runat="server" Text="Login" class="btn btn-default submit btn-block" OnClick="BtnLogin_Click" />
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

