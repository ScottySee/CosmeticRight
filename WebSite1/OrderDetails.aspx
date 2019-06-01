﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Member.master" AutoEventWireup="true" CodeFile="OrderDetails.aspx.cs" Inherits="OrderDetails" %>

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
            <div class="text-white">
                <i class="fa fa-money"></i>Order #<asp:Literal ID="ltOrderNo" runat="server" />
                Details
            </div>
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
                                            <img runat="server" src='<%# string.Concat("~/Images/Products/", Eval("Image")) %>' width="120" alt='<%# Eval("Product") %>' />
                                        </td>
                                        <td>
                                            <h4><%# Eval("Product") %></h4>
                                            <small>Category: <%# Eval("Category") %></small>
                                        </td>
                                        <td>Php<%# Eval("Price", "{0: #,##0.00}") %></td>
                                        <td><%# Eval("Quantity") %>
                                        <td>Php<%# Eval("Amount", "{0: #,##0.00}") %></td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <tr>
                                        <td colspan="6">
                                            <h3 class="text-center">No records found.</h3>
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
                                    <td>Status</td>
                                    <td align="right">
                                        <asp:Literal ID="ltStatus" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Payment Method</td>
                                    <td align="right">
                                        <asp:Literal ID="ltPaymentMethod" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Date Ordered</td>
                                    <td align="right">
                                        <asp:Literal ID="ltDateOrdered" runat="server" />
                                    </td>
                                </tr>
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
                                <%--<tr>
                                    <td>Delivery</td>
                                    <td align="right">
                                        <asp:Literal ID="ltDelivery" runat="server" />
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td>Total Amount</td>
                                    <td align="right">
                                        <h3>Php<asp:Literal ID="ltTotal" runat="server" /></h3>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                      
                        <asp:LinkButton ID="btnCancel" runat="server"
                            CssClass="btn btn-danger btn-lg btn-block"
                            OnClientClick='return confirm("Cancel order?");' OnClick="btnCancel_Click">
                    <i class="fa fa-thumbs-down"></i> Cancel
                        </asp:LinkButton>
                        <a href="Orders.aspx" class="btn btn-default btn-block btn-lg">Back to Orders
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
                            <asp:Label ID="txtFN" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-lg-4">Last Name</label>
                        <div class="col-lg-8">
                            <asp:Label ID="txtLN" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-lg-4">Unit/Building No.</label>
                        <div class="col-lg-8">
                            <asp:Label ID="txtbuilding" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-lg-4">Street</label>
                        <div class="col-lg-8">
                            <asp:Label ID="txtStreet" runat="server" class="form-control" />
                        </div>
                    </div>
                </div>
                <div class="col-6">
                    <div class="form-group">
                        <label class="control-label col-lg-4">Municipality</label>
                        <div class="col-lg-8">
                            <asp:Label ID="txtMunicipality" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-lg-4">City</label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" required />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label col-lg-4">Phone</label>
                        <div class="col-lg-8">
                            <asp:Label ID="txtPhone" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-lg-4">Mobile</label>
                        <div class="col-lg-8">
                            <asp:Label ID="txtMobile" runat="server" class="form-control" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

