<%@ Page Title="" Language="C#" MasterPageFile="~/Member.master" AutoEventWireup="true" CodeFile="ViewAnnouncementDetail2.aspx.cs" Inherits="ViewAnnouncementDetail2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
    <div class="header bg-gradient-gray-dark pb-5 pt-5 pt-md-8">
        <div class="container-fluid">
            <div class="header-body">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <%--<form runat="server" class="form-horizontal">--%>
        <div class="container">
            <div class="text-white">
                <h1><asp:Literal ID="ltName" runat="server" /></h1>
            </div>
            <div class="col-lg-6">
                <asp:Image ID="imgAnnouncement" runat="server" class="img-responsive img-circle" Width="239" Height="180" Style="object-fit: cover" />
            </div>
            <br />
            <div class="col-lg-6 text-white">
                <h3>Description</h3>
                <asp:Literal ID="ltDesc" runat="server" />
                <hr />
                <br />
                <h3>Duration</h3>
                <asp:Literal ID="ltdatestart" runat="server" /> - <asp:Literal ID="ltdateend" runat="server" />
                <br />
            </div>
        </div>
<%--    </form>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" Runat="Server">
</asp:Content>

