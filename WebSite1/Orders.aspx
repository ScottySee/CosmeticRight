﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Member.master" AutoEventWireup="true" CodeFile="Orders.aspx.cs" Inherits="OrderHistory" %>

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
        <div class="col-lg-offset-6 col-lg-3 text-white">
            START<asp:TextBox ID="txtStart" runat="server" CssClass="form-control"
                type="date" AutoPostBack="true"  OnTextChanged="SearchByDate" />
        </div>
        <br />
        <div class="col-lg-3 text-white">
            END<asp:TextBox ID="txtEnd" runat="server" CssClass="form-control"
                type="date" AutoPostBack="true" OnTextChanged="SearchByDate" />
        </div>
        <div class="col-lg-12">
            <center><h1>Orders</h1></center>
            <table class="table table-hover">
                <thead>
                    <th>#</th>
                    <th>Order Date</th>
                    <th>Payment Method</th>
                    <th>Customer</th>
                    <th>Total Amount</th>
                    <th></th>
                </thead>
                <tbody>
                    <asp:ListView ID="lvOrders" runat="server">
                        <ItemTemplate>
                            <tr class="bg-default">
                                <td><%# Eval("OrderNo") %></td>
                                <td><%# Eval("DateOrdered") %></td>
                                <td><%# Eval("PaymentMethod") %></td>
                                <td><%# Eval("CustomerName") %></td>
                                <td><%# Eval("TotalAmount", "{0: #,##0.00}") %></td>
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
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

