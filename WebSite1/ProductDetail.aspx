<%@ Page Title="" Language="C#" MasterPageFile="~/Member.master" AutoEventWireup="true" CodeFile="ProductDetail.aspx.cs" Inherits="ProductDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <div class="header bg-gradient-gray-dark pb-5 pt-5 pt-md-8">
        <div class="container-fluid">
            <div class="header-body">
            </div>
        </div>
    </div>
    <asp:Literal ID="ltName" runat="server" />
    (<asp:Literal ID="ltCode" runat="server" />)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <form runat="server" class="form-horizontal">
        <div class="card">
            <div class="col-lg-12">
                <asp:Image ID="imgProduct" runat="server" class="img-responsive img-circle" Width="239" Height="180" Style="object-fit: cover" />
            </div>
            <div class="col-lg-6">
                <h3>Description</h3>
                <asp:Literal ID="ltDesc" runat="server" />
                <hr />
                <br />
                <strong>Category:
                    <asp:HyperLink ID="hlCategory" runat="server" /></strong>
                <br />
                <strong>Price </strong>Php<asp:Literal ID="ltPrice" runat="server" />
                <br />
                <div class="input-group col-lg-6">
                    <asp:TextBox ID="txtQty" runat="server" class="form-control" type="number" min="1" MaxLength="99"
                        Text="1" required />
                    <span class="input-group-btn">
                        <asp:Button ID="btnAddToCart" runat="server" class="btn btn-success" Text="Add To Cart" />
                    </span>
                </div>
            </div>
        </div>

    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

