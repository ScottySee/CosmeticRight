<%@ Page Title="" Language="C#" MasterPageFile="~/Member.master" AutoEventWireup="true" CodeFile="ProductDetail.aspx.cs" Inherits="ProductDetail" %>

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
                <h1>
                    <asp:Literal ID="ltName" runat="server" />
                    (<asp:Literal ID="ltCode" runat="server" />)</h1>
            </div>
            <div class="col-lg-12">
                <asp:Image ID="imgProduct" runat="server" class="img-responsive img-circle" Width="239" Height="180" Style="object-fit: cover" />
            </div>
            <div class="col-lg-6 text-white">
                <h3>Description</h3>
                <asp:Literal ID="ltDesc" runat="server" />
                <hr />
                <br />
                <strong>Category: 
                    <asp:HyperLink ID="hlCategory" runat="server" /></strong>
                <br />
                <strong>Price: </strong>Php
                <asp:Literal ID="ltPrice" runat="server" />
                <br />
                <div class="input-group col-lg-8">
                    <%--<asp:TextBox ID="txtQty" runat="server" class="form-control" type="number" min="50" MaxLength="2000"
                        Text="50" required />--%>
                    <asp:DropDownList ID="ddlCategories" runat="server" class="form-control" required>
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
                    </asp:DropDownList>

                    <span class="input-group-btn">
                        <asp:LinkButton ID="btnAddToCart" runat="server" class="btn btn-success btn-block" OnClick="btnAddToCart_Click" CommandName="addtocart">
                            <i class="fa fa-shopping-cart"></i> Add To Cart                    
                        </asp:LinkButton>
                    </span>
                </div>
                <br />
                <div>
                    <a href="ProductDisplay.aspx">Back to Products</a>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

