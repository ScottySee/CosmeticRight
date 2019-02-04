<%@ Page Title="" Language="C#" MasterPageFile="~/Member.master" AutoEventWireup="true" CodeFile="Cart.aspx.cs" Inherits="Cart" %>

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
                <h3><i class="fa fa-shopping-cart"></i> My Shopping Cart</h3>
            </div>
            <div class="row">
                <div class="col-lg-9">
                    <table class="table table-hover">
                        <thead>
                            <th colspan="2">Item Name</th>
                            <th>Price</th>
                            <th>Quantity</th>
                            <th>Amount</th>
                            <th>Actions</th>
                        </thead>
                        <tbody>
                            <asp:ListView ID="lvCart" runat="server" OnItemCommand="lvCart_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Literal ID="ltRefNo" runat="server"
                                                Text='<%# Eval ("RefNo") %>' Visible="false" />
                                            <asp:Literal ID="ltProductID" runat="server"
                                                Text='<%# Eval("ProductID") %>' Visible="false" />
                                            <img src='../Images/Products/<%# Eval("Image") %>'
                                                width="120" alt='<%# Eval ("Name") %>' />
                                        </td>
                                        <td>
                                            <h4><%# Eval("Name") %></h4>
                                            <small>Category: <%# Eval("Category") %></small>
                                        </td>
                                        <td>Php<%# Eval("Price", "{0: #,##0.00}") %>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtQty" runat="server"
                                                class="form-control" type="number"
                                                min="1" max="99" Text='<%# Eval("Quantity") %>'
                                                required />
                                        </td>
                                        <td>Php<%# Eval("Amount", "{0: #,##0.00}") %>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="btnUpdate" runat="server"
                                                class="btn btn-xs btn-info" CommandName="updateqty"
                                                ToolTip="Update quantity">
                                        <i class="fa fa-refresh"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnDelete" runat="server"
                                                class="btn btn-xs btn-danger" CommandName="deleteitem"
                                                ToolTip="Remove an Item">
                                        <i class="fa fa-trash-o"></i>
                                            </asp:LinkButton>
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

                </div>
                <div class="col-lg-3">
                    <div class="well">
                        <h4 class="text-center">Order Summary</h4>
                        <table class="table">
                            <tbody>
                                <tr>
                                    <td>Gross Amount</td>
                                    <td align="right">Php
                                    <asp:Literal ID="ltGross" runat="server" />

                                    </td>
                                </tr>
                                <tr>
                                    <td>VAT (12%)</td>
                                    <td align="right">
                                        <asp:Literal ID="ltVAT" runat="server" />

                                    </td>
                                </tr>
                                <tr>
                                    <td>Delivery</td>
                                    <td align="right">
                                        <asp:Literal ID="ltDelivery" runat="server" />

                                    </td>
                                </tr>
                                <tr>
                                    <td>Total Amount</td>
                                    <td align="right">
                                        <h3>Php
                                        <asp:Literal ID="ltTotal" runat="server" /></h3>

                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <a href="Checkout.aspx" class="btn btn-block btn-lg btn-success">
                            <i class="fa fa-money">Checkout </i>
                        </a>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

