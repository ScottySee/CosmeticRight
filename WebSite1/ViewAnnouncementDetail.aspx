<%@ Page Title="" Language="C#" MasterPageFile="~/Member.master" AutoEventWireup="true" CodeFile="ViewAnnouncementDetail.aspx.cs" Inherits="ViewAnnouncementDetail" %>

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
                <h1><asp:Literal ID="ltName" runat="server" /></h1>
            </div>
            <div class="col-lg-6">
                <asp:Image ID="imgProduct" runat="server" class="img-responsive img-circle" Width="239" Height="180" Style="object-fit: cover" />
            </div>
            <br />
            <div class="col-lg-6 text-white">
                <h3>Description</h3>
                <asp:Literal ID="ltDesc" runat="server" />
                <hr />
                <br />
            </div>
        </div>


    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

