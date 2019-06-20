<%@ Page Title="" Language="C#" MasterPageFile="~/Member.master" AutoEventWireup="true" CodeFile="Refund.aspx.cs" Inherits="Refund" %>

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
        <asp:ScriptManager runat="server" />
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
                                        <%--<td><%# Eval("OrderNo") %></td>--%>

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
                                        <h3>Php<asp:Literal ID="ltTotal" runat="server" /></h3>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>

                    <!-- Button trigger cancellation modal -->
                    <% if (ltStatus.Text != "Pending" && ltStatus.Text != "Accepted" && ltStatus.Text != "Rejected" && ltStatus.Text != "Processing" && ltStatus.Text != "Packaging" && ltStatus.Text != "For Delivery" && ltStatus.Text != "Cancelled, Pending for Approval" && ltStatus.Text != "Cancelled Approved" && ltStatus.Text != "Cancelled Approve, Refund in Process" && ltStatus.Text != "Cancelled Disapproved" && ltStatus.Text != "Refund Request Submitted, Pending for Verification" && ltStatus.Text != "Refund Request Received, Verified. Check your email" && ltStatus.Text != "Refund Completed")
                        { %>
                    <button id="refund" class="btn btn-primary btn-block btn-lg" data-toggle="modal" data-target="#refunds">
                        Refund
                    </button>
                    <%} %>



                    <!-- Refund Modal -->
                    <div class="modal fade" id="refunds" tabindex="-1" role="dialog">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title">Return/Refund</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group text-black-50">
                                        <label class="control-label">Reasons:</label>
                                        <asp:DropDownList Style="color: black" ID="ddlReason" runat="server" class="form-control" required>
                                            <asp:ListItem Value="" style="color: black">--------------------------Select Reason--------------------------</asp:ListItem>
                                            <asp:ListItem Value="The item was damaged" style="color: black">The item was damaged</asp:ListItem>
                                            <asp:ListItem Value="Not the same picture as shown" style="color: black">Not the same picture as shown</asp:ListItem>
                                            <asp:ListItem Value="Others / Change of mind" style="color: black">Others / Change of mind</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="btnrefund" runat="server" Text="Submit" class="btn btn-block btn-success submit btn-block" OnClientClick='return confirm("Are you sure to refund this order?");' OnClick="btnrefund_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <a href="Orders.aspx" class="btn btn-default btn-block btn-lg">Back to Orders</a>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

