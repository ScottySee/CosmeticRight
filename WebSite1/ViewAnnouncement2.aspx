<%@ Page Title="" Language="C#" MasterPageFile="~/Member.master" AutoEventWireup="true" CodeFile="ViewAnnouncement2.aspx.cs" Inherits="ViewAnnouncement2" %>

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
            <%--<div class="col-lg-3">
                    <div class="list-group">
                        <a href="ProductDisplay.aspx" class="list-group-item">
                            <span class="badge">
                                <asp:Literal ID="ltTotal" runat="server" />
                            </span>
                            All Products
                        </a>
                    </div>
                </div>--%>
            <div class="col-lg-12">
                <div class="">
                    <div class="text-white align-content-center">
                        <center><h1>Announcements</h1></center>
                    </div>
                    <div class="col-lg-4">
                        <div class="input-group">
                            <asp:TextBox ID="txtKeyword" runat="server" class="form-control"
                                placeholder="Search..." />
                            <span class="input-group-btn">
                                <asp:LinkButton ID="btnSearch" runat="server" class="btn btn-info"
                                    OnClick="btnSearch_Click">
                        <i class="fa fa-search"></i>
                                </asp:LinkButton>
                            </span>
                        </div>
                    </div>
                    <div class="card-deck">
                        <asp:ListView ID="lvAnnouncement" runat="server" OnItemCommand="lvAnnouncements_ItemCommand">
                            <ItemTemplate>
                                <div class="col-lg-4">
                                    <div class="card">
                                        <img src='Images/Announcement/<%# Eval("Image") %>' class="card-img-top" width="239" height="180" style="object-fit: cover" alt='<%# Eval("AnnouncementName") %>' />
                                        <div class="card-header">
                                            <a href='ViewAnnouncementDetail1.aspx?ID=<%# Eval("AnnouncementID") %>' style="text-decoration: none;">
                                        </div>
                                        <div class="card-body">
                                            <asp:Literal ID="ltID" runat="server" Text='<%# Eval("AnnouncementID") %>' Visible="false" />
                                            <h2><%# Eval("AnnouncementName") %></h2>
                                            <h4><%# Eval("AnnouncementDetail") %></h4>

                                            <%--<asp:LinkButton ID="btnAddToCart" runat="server" class="btn btn-success btn-block" CommandName="addtocart">
                                                    <i class="fa fa-shopping-cart"></i> Add To Cart
                                                </asp:LinkButton>--%>
                                                </a>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <div class="text-white align-content-center">
                                    <center><h1>There are no Announcements</h1></center>
                                </div>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>
                    <center>
                        <asp:DataPager ID="dpAnnouncement" runat="server" PageSize="10"
                            PagedControlID="lvAnnouncement">
                            <Fields>
                                <asp:NumericPagerField ButtonType="Button"
                                    CurrentPageLabelCssClass="btn btn-info"
                                    NumericButtonCssClass="btn btn-default"
                                    NextPreviousButtonCssClass="btn btn-default"
                                    ButtonCount="5" />
                            </Fields>
                        </asp:DataPager>
                    </center>
                </div>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

