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
        <div class="container">
            <div class="row">
                <div class="col-lg-3">
                    <div class="list-group">
                        <a href="ProductDisplay.aspx" class="list-group-item">
                            <span class="badge">
                                <asp:Literal ID="ltTotal" runat="server" />
                            </span>
                            All Products
                        </a>
                        <asp:ListView ID="lvCategories" runat="server">
                            <ItemTemplate>
                                <a href='ProductDisplay.aspx?c=<%# Eval("CatID") %>'
                                    class="list-group-item">
                                    <span class="badge"><%# Eval("TotalCount") %></span>
                                    <%# Eval("Category") %>
                                </a>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
                <div class="col-lg-9">
                    <div class="">
                        <div class="card-deck">
                            <asp:ListView ID="lvProducts" runat="server" OnItemCommand="lvProducts_ItemCommand">
                                <ItemTemplate>
                                    <div class="col-lg-4">
                                        <div class="card">
                                            <img src='Images/Products/<%# Eval("Image") %>' class="card-img-top" width="239" height="180" style="object-fit: cover" alt='<%# Eval("Product") %>' />
                                            <div class="card-header">
                                                <a href='ProductDetail.aspx?ID=<%# Eval("ProductID") %>' style="text-decoration: none;">
                                            </div>
                                            <div class="card-body">
                                                <asp:Literal ID="ltID" runat="server" Text='<%# Eval("ProductID") %>' Visible="false" />
                                                <h3><%# Eval("Product") %></h3>
                                                <small>Code: <%# Eval("Code") %></small><br />

                                                <small><em>Category: <asp:Label runat="server" ID="category" Text='<%# Eval("Category") %>'>  </asp:Label></em></small>
                                                <br />
                                                <p>
                                                    Available: <%# Eval("Available") %>
                                                </p>
                                                <p>
                                                    Php <%# Eval("Price", "{0: #,##0.00}") %>
                                                </p>
                                                <asp:LinkButton ID="btnAddToCart" runat="server" class="btn btn-success btn-block" CommandName="addtocart">
                                                    <i class="fa fa-shopping-cart"></i> Add To Cart
                                                </asp:LinkButton>
                                                </a>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <p class="text-white">There are no Products.</p>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>
