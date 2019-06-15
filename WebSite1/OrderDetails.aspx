<%@ Page Title="" Language="C#" MasterPageFile="~/Member.master" AutoEventWireup="true" CodeFile="OrderDetails.aspx.cs" Inherits="OrderDetails" ValidateRequest="false" %>

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
                <i class="fa fa-money"></i>Order #<asp:literal id="ltOrderNo" runat="server" />
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
                            <asp:listview id="lvCart" runat="server">
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
                                        <td>
                                            <asp:Label ID="txtquantity" runat="server" Text='<%# Bind("Quantity") %>' /></td>
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
                            </asp:listview>
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
                                        <asp:literal id="ltStatus" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Payment Method</td>
                                    <td align="right">
                                        <asp:literal id="ltPaymentMethod" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Date Ordered</td>
                                    <td align="right">
                                        <asp:literal id="ltDateOrdered" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Gross Amount</td>
                                    <td align="right">Php<asp:literal id="ltGross" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>VAT (12%)</td>
                                    <td align="right">Php
                                        <asp:literal id="ltVAT" runat="server" />
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
                                        <h3>Php<asp:literal id="ltTotal" runat="server" /></h3>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <!-- Button trigger modal -->
                    <button id="cancel" class="btn btn-primary btn-block btn-lg" data-toggle="modal" data-target="#Cancelation">
                        Cancel
                    </button>

                    <!-- Modal -->
                    <div class="modal fade" id="Cancelation" tabindex="-1" role="dialog">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title">Cancel</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group">
                                        <label class="control-label">Reasons:</label>
                                        <asp:dropdownlist style="color: black" id="ddlReason" runat="server" class="form-control" required>
                                            <asp:ListItem Value="" style="color: black">--------------------------Select Reason--------------------------</asp:ListItem>
                                            <asp:ListItem Value="Seller is not responsive to my inquiries" style="color: black">Seller is not responsive to my inquiries</asp:ListItem>
                                            <asp:ListItem Value="Seller ask me to cancel" style="color: black">Seller ask me to cancel</asp:ListItem>
                                            <asp:ListItem Value="Found cheaper price" style="color: black">Found cheaper price</asp:ListItem>
                                            <asp:ListItem Value="Others / Change of mind" style="color: black">Others / Change of mind</asp:ListItem>
                                        </asp:dropdownlist>
                                    </div>
                                    <div class="form-group">
                                        <asp:button id="btnCancel" runat="server" text="Submit" class="btn btn-block btn-success submit btn-block" onclientclick='return confirm("Are you sure to cancel this order?");' onclick="btnCancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--<asp:LinkButton ID="btnCancel" runat="server"
                        CssClass="btn btn-danger btn-lg btn-block"
                        OnClick="btnCancel_Click" data-toggle="modal" data-target="#myModal">
                    <i class="fa fa-thumbs-down"></i> Cancel
                    </asp:LinkButton>--%>
                    <a href="Orders.aspx" class="btn btn-default btn-block btn-lg">Back to Orders
                    </a>
                </div>
            </div>
            <h3>Billing and Delivery Details</h3>
            <div class="row text-white mb-5">
                <div class="col-6">
                    <div class="form-group">
                        <label class="control-label col-lg-4">First Name</label>
                        <div class="col-lg-8">
                            <asp:label id="txtFN" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-lg-4">Last Name</label>
                        <div class="col-lg-8">
                            <asp:label id="txtLN" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-lg-4">Unit/Building No.</label>
                        <div class="col-lg-8">
                            <asp:label id="txtbuilding" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-lg-4">Street</label>
                        <div class="col-lg-8">
                            <asp:label id="txtStreet" runat="server" class="form-control" />
                        </div>
                    </div>
                </div>
                <div class="col-6">
                    <div class="form-group">
                        <label class="control-label col-lg-4">Municipality</label>
                        <div class="col-lg-8">
                            <asp:label id="txtMunicipality" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-lg-4">City</label>
                        <div class="col-lg-8">
                            <asp:dropdownlist id="ddlCity" runat="server" cssclass="form-control" required />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label col-lg-4">Phone</label>
                        <div class="col-lg-8">
                            <asp:label id="txtPhone" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-lg-4">Mobile</label>
                        <div class="col-lg-8">
                            <asp:label id="txtMobile" runat="server" class="form-control" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

