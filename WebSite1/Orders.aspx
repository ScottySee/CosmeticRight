<%@ Page Title="" Language="C#" MasterPageFile="~/Member.master" AutoEventWireup="true" CodeFile="Orders.aspx.cs" Inherits="Orders" %>

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
        <form runat="server" class="form-horizontal">
            <asp:ScriptManager runat="server" />
            <div class="card shadow-lg">
                <div class="card-body">
                    <div class="col-lg-offset-6 col-lg-3 text-white">
                        START<asp:TextBox ID="txtStart" runat="server" CssClass="form-control"
                            type="date" AutoPostBack="true" OnTextChanged="SearchByDate" />
                    </div>
                    <br />
                    <div class="col-lg-3 text-white">
                        END<asp:TextBox ID="txtEnd" runat="server" CssClass="form-control"
                            type="date" AutoPostBack="true" OnTextChanged="SearchByDate" />
                    </div>
                    <%--<div>
                        <table class="table" visible="false">
                            <tbody>
                                <tr>
                                    <td hidden>OrderNo</td>
                                    <td align="right">
                                        <asp:Literal ID="ltOrderNo" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td hidden>Status</td>
                                    <td align="right">
                                        <asp:Literal ID="ltStatus" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td hidden>Payment Method</td>
                                    <td align="right">
                                        <asp:Literal ID="ltPaymentMethod" runat="server" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>--%>
                        <!-- Button trigger refund modal -->
                        <%--<button id="refund" class="btn btn-primary btn-block btn-lg" data-toggle="modal" data-target="#Refund">
                            Refund
                        </button>--%>

                        <!-- Refund Modal -->
                        <%--<div class="modal fade" id="Refund" tabindex="-1" role="dialog">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                        <h4 class="modal-title">Refund</h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="form-group">
                                            <label class="control-label">Reasons:</label>
                                            <asp:DropDownList Style="color: black" ID="ddlReason1" runat="server" class="form-control" required>
                                                <asp:ListItem Value="" style="color: black">--------------------------Select Reason--------------------------</asp:ListItem>
                                                <asp:ListItem Value="The item was damaged" style="color: black">The item was damaged</asp:ListItem>
                                                <asp:ListItem Value="Not the same picture as shown" style="color: black">Not the same picture as shown</asp:ListItem>
                                                <asp:ListItem Value="Others / Change of mind" style="color: black">Others / Change of mind</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="btnRefund" runat="server" Text="Submit" class="btn btn-block btn-success submit btn-block" OnClientClick='return confirm("Are you sure to refund this order?");' OnClick="btnRefund_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>--%>

                    <div class="col-lg-12">
                        <center><h1>Orders</h1></center>
                        <table class="table table-hover">
                            <thead style="text-align: center">
                                <th>#</th>
                                <th>Order Date</th>
                                <th>Payment Method</th>
                                <th>Customer</th>
                                <th>Total Amount</th>
                                <th>Status</th>
                                <th></th>
                            </thead>
                            <tbody>
                                <asp:ListView ID="lvOrders" runat="server">
                                    <ItemTemplate>
                                        <tr class="bg-default" style="text-align: center">
                                            <td><%# Eval("OrderNo") %></td>
                                            <td><%# Eval("DateOrdered", "{0:MMM dd, yyyy}") %></td>
                                            <td><%# Eval("PaymentMethod") %></td>
                                            <td><%# Eval("CustomerName") %></td>
                                            <td>Php<%# Eval("TotalAmount", "{0: #,##0.00}") %></td>
                                            <td><%# Eval("Status") %></td>
                                            <td>
                                                <a href='OrderDetails.aspx?ID=<%# Eval("OrderNo") %>' class="btn btn-xs btn-info"
                                                    title="View Details">
                                                    <i class="fa fa-list"></i>
                                                </a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <tr>
                                            <td colspan="8">
                                                <h2 class="text-center">No records found.</h2>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </tbody>
                        </table>
                    </div>

                </div>
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>



