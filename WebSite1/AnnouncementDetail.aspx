<%@ Page Title="" Language="C#" MasterPageFile="~/Member.master" AutoEventWireup="true" CodeFile="AnnouncementDetail.aspx.cs" Inherits="AnnouncementDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
    <asp:Literal ID="ltName" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <form runat="server" class="form-horizontal">
        <div class="col-lg-6">
            <asp:Image ID="imgAnnouncement" runat="server" class="img-responsive img-circle" />
        </div>
        <div class="col-lg-6">
            <h3>Name</h3>
            <asp:Literal ID="ltDesc" runat="server" />
            <hr /><br />
            <strong>Detail: <asp:HyperLink ID="hlCategory" runat="server" /></strong>
            <br />
            <%--<div class="input-group col-lg-6">
                <asp:TextBox ID="txtQty" runat="server" class="form-control" type="number" min="1" MaxLength="99"
                    text="1" required />
                <span class="input-group-btn">
                    <asp:Button ID="btnAddToCart" runat="server" class="btn btn-success" Text="Add To Cart" />
                </span>
            </div>--%>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" Runat="Server">
</asp:Content>

