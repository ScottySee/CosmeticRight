<%@ Page Title="" Language="C#" MasterPageFile="~/Member.master" AutoEventWireup="true" CodeFile="ProductDisplay.aspx.cs" Inherits="ProductDisplay" %>

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
        <div class="row">
            <div class="container">
                <div class="col-lg-3">
                    <div class="list-group">
                        <a href="Products.aspx" class="list-group-item">
                            <span class="badge">
                                <asp:Literal ID="ltTotal" runat="server" /></span>
                            All Products
                        </a>
                        <asp:ListView ID="lvCategories" runat="server">
                            <ItemTemplate>
                                <a href='ProductsDisplay.aspx?c=<%# Eval("CatID") %>'
                                    class="list-group-item">
                                    <span class="badge"><%# Eval("TotalCount") %></span>
                                    <%# Eval("Category") %>
                                </a>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
                <div class="col-lg-9">
                    <div class="row">
                        <div class="card-columns">
                            <asp:ListView ID="lvProducts" runat="server" OnItemCommand="lvProducts_ItemCommand">
                                <ItemTemplate>
                                    <div class="col">
                                        <div class="card">
                                            <div class="card-image">
                                                <img src='Images/Products/<%# Eval("Image") %>' alt='<%# Eval("Name") %>' />
                                            </div>
                                            <div class="card-header">
                                                <a href='Details.aspx?ID=<%# Eval("ProductID") %>' style="text-decoration: none;">
                                            </div>
                                            <div class="card-body">
                                                <asp:Literal ID="ltID" runat="server" Text='<%# Eval("ProductID") %>' Visible="false" />
                                                <h3><%# Eval("Name") %></h3>
                                                <small><%# Eval("Code") %></small><br />
                                                <small><em>Category: <%# Eval("Category") %></em></small><br />
                                                <p>
                                                    Php <%# Eval("Price", "{0: #,##0.00}") %>
                                                </p>
                                                <asp:LinkButton ID="btnAddToCart" runat="server" class="btn btn-success btn-block" CommandName="addtocart">
                                                    <i class="fa fa-shopping-cart"></i> Add To Cart
                                                </asp:LinkButton>
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    There are no Products.
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>




