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

    <div class="container">
        <div class="text-white">
            <h3><i class="fa fa-shopping-cart"></i>My Shopping Cart</h3>
        </div>
        <div class="row">
            <form runat="server">
                <div class="col-lg-9">
                    <table class="table table-hover">
                        <thead>
                            <th colspan="2">Item Name</th>
                            <th>Price</th>
                            <th>
                                <center>Quantity</center>
                            </th>
                            <th>Amount</th>
                            <th>Actions</th>
                        </thead>
                        <a href="ProductDisplay.aspx">Back to Products</a>
                        <tbody>
                            <asp:listview id="lvCart" runat="server" onitemcommand="lvCart_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Literal ID="ltRefNo" runat="server"
                                                Text='<%# Eval ("RefNo") %>' Visible="false" />

                                            <asp:Literal ID="ltProductID" runat="server"
                                                Text='<%# Eval("ProductID") %>' Visible="false" />
                                            <%--<img src='Images/Products/<%# Eval("Image") %>' class="card-img-top" width="239" height="180" style="object-fit:cover" alt='<%# Eval("Name") %>' />--%>
                                            <img src='../Images/Products/<%# Eval("Image") %>'
                                                width="200" alt='<%# Eval ("Name") %>' />
                                        </td>
                                        <td>
                                            <h4><%# Eval("Name") %></h4>
                                            <small>Category: <%# Eval("Category") %></small>
                                        </td>
                                        <td>Php<%# Eval("Price", "{0: #,##0.00}") %>
                                        </td>
                                        <td>
                                            <%--<asp:DropDownList ID="ddlQuantity" runat="server" class="form-control" required>
                                                <asp:ListItem Value="" style="color: black">----Select Quantity----</asp:ListItem>
                                                <asp:ListItem Value="200" style="color: black">200</asp:ListItem>
                                                <asp:ListItem Value="300" style="color: black">300</asp:ListItem>
                                                <asp:ListItem Value="400" style="color: black">400</asp:ListItem>
                                                <asp:ListItem Value="500" style="color: black">500</asp:ListItem>
                                                <asp:ListItem Value="600" style="color: black">600</asp:ListItem>
                                                <asp:ListItem Value="700" style="color: black">700</asp:ListItem>
                                                <asp:ListItem Value="800" style="color: black">800</asp:ListItem>
                                                <asp:ListItem Value="900" style="color: black">900</asp:ListItem>
                                                <asp:ListItem Value="1000" style="color: black">1000</asp:ListItem>
                                                <asp:ListItem Value="1100" style="color: black">1100</asp:ListItem>
                                                <asp:ListItem Value="1200" style="color: black">1200</asp:ListItem>
                                                <asp:ListItem Value="1300" style="color: black">1300</asp:ListItem>
                                                <asp:ListItem Value="1400" style="color: black">1400</asp:ListItem>
                                                <asp:ListItem Value="1500" style="color: black">1500</asp:ListItem>
                                                <asp:ListItem Value="1600" style="color: black">1600</asp:ListItem>
                                                <asp:ListItem Value="1700" style="color: black">1700</asp:ListItem>
                                                <asp:ListItem Value="1800" style="color: black">1800</asp:ListItem>
                                                <asp:ListItem Value="1900" style="color: black">1900</asp:ListItem>
                                                <asp:ListItem Value="2000" style="color: black">2000</asp:ListItem>
                                            </asp:DropDownList>--%>
                                            <%--<asp:TextBox ID="txtQty" runat="server"
                                                class="form-control" type="number"
                                                min="1" max="50000" Text='<%# Eval("Quantity") %>'
                                                required />--%>
                                            <%# Eval("Quantity") %>
                                        </td>
                                        <td>Php<%# Eval("Amount", "{0: #,##0.00}") %>
                                        </td>
                                        <td>
                                            <%--<asp:LinkButton ID="btnUpdate" runat="server"
                                                class="btn btn-xs btn-info" CommandName="updateqty"
                                                ToolTip="Update quantity">
                                        <i class="fa fa-refresh"></i>&nbsp;Update
                                            </asp:LinkButton>--%>
                                            <asp:LinkButton ID="btnDelete" runat="server"
                                                class="btn btn-sm btn-danger" CommandName="deleteitem"
                                                ToolTip="Remove an Item">
                                        <i class="fa fa-trash-o"></i>&nbsp;Remove
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
                            </asp:listview>
                        </tbody>
                    </table>
                </div>
            </form>
            <div class="col-lg-3">
                <div class="well">
                    <h4 class="text-center">Order Summary</h4>
                    <table class="table">
                        <tbody>
                            <tr>
                                <td>Gross Amount</td>
                                <td align="right">Php
                                    <asp:literal id="ltGross" runat="server" />

                                </td>
                            </tr>
                            <tr>
                                <td>VAT (12%)</td>
                                <td align="right">
                                    <asp:literal id="ltVAT" runat="server" />

                                </td>
                            </tr>
                            <%--<tr>
                                <td>Delivery</td>
                                <td align="right">
                                    <asp:literal id="ltDelivery" runat="server" />

                                </td>
                            </tr>--%>

                            <tr>
                                <td>Total Amount</td>
                                <td align="right">
                                    <h3>Php
                                        <asp:literal id="ltTotal" runat="server" />
                                    </h3>

                                </td>
                            </tr>                            
                        </tbody>
                    </table>
                        <a href="CheckoutCOD.aspx">Cash on Delivery</a>
                    
                    <%--PAYPAL--%>
                    <form action="https://www.sandbox.paypal.com/cgi-bin/webscr" target="paypal" method="post">

                        <input type="hidden" name="cmd" value="_xclick" />
                        <input type="hidden" name="business" value="david.aligaen-buyer@benilde.edu.ph" />
                        <input type="hidden" name="currency_code" value="PHP" />

                        <input type="hidden" name="item_name" value="My Cart" />
                        <input type="hidden" name="amount" value="<%= Session["total"].ToString() %>" />
                        <input type="submit" class="btn btn-xs btn-success" value="Paypal" />

                    </form>
                    <br />
                </div>
            </div>

        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

