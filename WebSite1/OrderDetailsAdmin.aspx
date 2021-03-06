﻿<%@ Page Title="" Language="C#" MasterPageFile="~/OfficeAdmin.master" AutoEventWireup="true" CodeFile="OrderDetailsAdmin.aspx.cs" ValidateRequest="false" Inherits="OrderDetailsAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <div class="header bg-gradient-gray-dark pb-5 pt-5 pt-md-8">
        <div class="container-fluid">
            <div class="header-body">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    
    <div class="container mt--7">
        <!-- page content -->
        <form runat="server" class="form-horizontal">
            <asp:ScriptManager runat="server" />
            <%--<asp:UpdatePanel ID="Announcements" runat="server">
                <ContentTemplate>--%>
            <div class="card shadow-lg">
                <div class="card-body">
                    <div class="container">
                        <span class="pull-right">
                            <a href="PrintOrders.aspx?ID=<%= Request.QueryString["ID"] %>" class="btn btn-sm btn-success btn-block">
                                Print Order Receipt
                            </a>
                            <a href="PrintDelivery.aspx?ID=<%= Request.QueryString["ID"] %>" class="btn btn-sm btn-success btn-block">
                                Print Delivery Receipt
                            </a>
                        </span>
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
                                                    <td><asp:label ID="txtquantity"  runat="server" Text='<%# Bind("Quantity") %>'/></td>
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
                                                <td>Payment Method</td>
                                                <td align="right">
                                                    <asp:Literal ID="ltPaymentMethod" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Status</td>
                                                <td align="right">
                                                    <asp:Literal ID="ltStatus" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Reason for Cancelation</td>
                                                <td align="right">
                                                    <asp:Literal ID="ltReason" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Date of Cancelation</td>
                                                <td align="right">
                                                    <asp:Literal ID="ltDate" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Reason for Refund</td>
                                                <td align="right">
                                                    <asp:Literal ID="ltRefund" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Date of Refund</td>
                                                <td align="right">
                                                    <asp:Literal ID="ltRefundDate" runat="server" />
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
                                                <td align="right">Php
                                                    <asp:Literal ID="ltGross" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>VAT (12%)</td>
                                                <td align="right">Php
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
                                                    <h3>Php
                                                        <asp:Literal ID="ltTotal" runat="server" /></h3>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <asp:LinkButton ID="btnAccept" runat="server"
                                        CssClass="btn btn-success btn-lg btn-block"
                                        OnClientClick='return confirm("Accept this order?");'
                                        OnClick="btnAccept_Click">
                    <i class="fa fa-thumbs-up"></i> Accept
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnReject" runat="server"
                                        CssClass="btn btn-danger btn-lg btn-block"
                                        OnClientClick='return confirm("Reject this order?");'
                                        OnClick="btnReject_Click">
                    <i class="fa fa-thumbs-down"></i> Reject
                                    </asp:LinkButton>


                                    <asp:LinkButton ID="btnApprove" runat="server"
                                        CssClass="btn btn-success btn-lg btn-block"
                                        OnClientClick='return confirm("Accept the cancelation reason?");'
                                       OnClick="btnApprove_Click">Approve Cancelation
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnDisApprove" runat="server"
                                        CssClass="btn btn-danger btn-lg btn-block"
                                        OnClientClick='return confirm("Disapprove the cancelation reason?");'
                                       OnClick="btnDisApprove_Click">Disapprove Cancelation
                                    </asp:LinkButton>


                                    <asp:LinkButton ID="btnVerify" runat="server"
                                        CssClass="btn btn-success btn-lg btn-block"
                                        OnClientClick='return confirm("Accept the reason for refund?");'
                                       OnClick="btnVerify_Click">Approve Refund
                                    </asp:LinkButton>

                                    <%--PAYPAL--%>

                                    <%--<%
                                        string code = Guid.NewGuid().ToString();
                                        Session["Code"] = code;
                                    %>--%>
                                    <%--
                                    <input type="hidden" name="cmd" value="_xclick" />
                                    <input type="hidden" name="business" value="david.aligaen-buyer@benilde.edu.ph" />
                                    <input type="hidden" name="currency_code" value="PHP" />

                                    <input type="hidden" name="item_name" value="My Cart" />
                                    <input type="hidden" name="amount" value="<%= Session["total"].ToString() %>" />
                                    <input type="hidden" name="return" value="http://localhost:58759/Thanks.aspx?code=<%=code%>" />--%>
                                    <%--<input type="submit" class="btn btn-lg btn-success btn-block" value="Paypal" />--%>

                                    <%--<asp:LinkButton ID="btnPayNow" CssClass="btn btn-success btn-lg btn-block" runat="server" target="_blank" ImageUrl="~/Images/Logo/paypal.png" Text=" Paypal Refund" PostBackUrl="https://www.sandbox.paypal.com/cgi-bin/webscr" OnClick="btnPayNow_Click"/>--%>

                                    <a href="OrdersAdmin.aspx" class="btn btn-default btn-block btn-lg">Back to Orders
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
                                    <label class="control-label col-lg-4">Landline</label>
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
                                <div class="form-group">
                                    <label class="control-label col-lg-4">Email</label>
                                    <div class="col-lg-8">
                                        <asp:Label ID="txtEmail" runat="server" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
        <br />
    </div>
</asp:Content>

