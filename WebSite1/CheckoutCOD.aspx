<%@ Page Title="" Language="C#" MasterPageFile="~/Member.master" AutoEventWireup="true" CodeFile="CheckoutCOD.aspx.cs" Inherits="CheckoutCOD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <div class="header bg-gradient-gray-dark pb-5 pt-5 pt-md-8">
        <div class="container-fluid">
            <div class="header-body">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">

    <form runat="server" class="form-horizontal">
        <div class="container">
            <div class="row">
                <div class="col-lg-8">
                    <table class="table table-hover">
                        <thead>
                            <th colspan="2">Item Name</th>
                            <th>Price</th>
                            <th>Quantity</th>
                            <th>Amount</th>
                        </thead>
                        <tbody>
                            <asp:ListView ID="lvCart" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <img src='../Images/Products/<%# Eval("Image") %>'
                                                width="120" alt='<%# Eval("Name") %>' />
                                        </td>
                                        <td>
                                            <h4><%# Eval("Name") %></h4>
                                            <small>Category: <%# Eval("Category") %></small>
                                        </td>
                                        <td>Php<%# Eval("Price", "{0: #,##0.00}") %>
                                        </td>
                                        <td>
                                            <%# Eval("Quantity") %>
                                        </td>
                                        <td>Php<%# Eval("Amount", "{0: #,##0.00}") %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <tr>
                                        <td colspan="6">
                                            <h3 class="text-center">No records found.
                                            </h3>
                                        </td>
                                    </tr>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </tbody>
                    </table>
                    <br />
                    <hr />

                </div>
                <div class="col-lg-4">
                    <div class="well">
                        <h4 class="text-center">Order Summary</h4>
                        <table class="table">
                            <tbody>
                                <tr>
                                    <td>Gross Amount</td>
                                    <td align="right">Php<asp:Literal ID="ltGross" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>VAT (12%)</td>
                                    <td align="right">
                                        <asp:Literal ID="ltVAT" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Total Amount</td>
                                    <td align="right">
                                        <h3>Php<asp:Literal ID="ltTotal" runat="server" /></h3>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <hr />
                        <br />
                        <div class="form-group text-white">
                            <label class="control-label">Payment Method: Cash on Delivery</label>
                            <%--                    <div class="col-lg-8">
                        Cash on Delivery
                    </div>--%>
                        </div>
                        <asp:LinkButton ID="btnCheckout" runat="server"
                            CssClass="btn btn-success btn-lg btn-block"
                            OnClientClick='return confirm("Are you sure?");'
                            OnClick="btnCheckout_Click">
                    <i class="fa fa-money"></i> Order Now
                        </asp:LinkButton>
                        <a href="Cart.aspx" class="btn btn-default btn-block btn-lg">Back to Cart
                        </a>
                    </div>
                </div>
            </div>
            <h3>Billing and Delivery Details</h3>
            <div class="row text-white mb-5">
                <div class="col-6">
                    <div class="form-group">
                        <label class="control-label col-lg-4">First Name</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtFN" runat="server" class="form-control" MaxLength="80" required />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-lg-4">Last Name</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtLN" runat="server" class="form-control" MaxLength="50" required />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-lg-4">Unit/Building No.</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtbuilding" runat="server" class="form-control" MaxLength="50" required />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-lg-4">Street</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtStreet" runat="server" class="form-control" MaxLength="50" required />
                        </div>
                    </div>
                </div>
                <div class="col-6">
                    <div class="form-group">
                        <label class="control-label col-lg-4">Municipality</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtMunicipality" runat="server" class="form-control" MaxLength="100" required />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-lg-4">City</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtCity" runat="server" class="form-control" MaxLength="50" required />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label col-lg-4">Phone</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtPhone" runat="server" class="form-control" MaxLength="12" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-lg-4">Mobile</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtMobile" runat="server" class="form-control" MaxLength="12" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>
