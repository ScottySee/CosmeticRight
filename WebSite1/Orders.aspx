<%@ Page Title="" Language="C#" MasterPageFile="~/OfficeAdmin.master" AutoEventWireup="true" CodeFile="Orders.aspx.cs" Inherits="OrderHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <div class="header bg-gradient-gray-dark pb-5 pt-5 pt-md-8">
        <div class="container-fluid">
            <div class="header-body">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="container mt--7">
        <!-- page content -->
        <form runat="server" class="form-horizontal">
            <asp:ScriptManager runat="server" />
            <%--<asp:UpdatePanel ID="Announcements" runat="server">
                <ContentTemplate>--%>
            <!-- Table row -->
            <div class="card mt-5">
                <div class="card-body">
                    <div class="container">
                        <div class="text-white">
                            <center><h1>Orders</h1></center>
                        </div>
                        <div class="col-lg-offset-6 col-lg-3">
                            Start Date<asp:TextBox ID="txtStart" runat="server" CssClass="form-control"
                                type="date" AutoPostBack="true" OnTextChange="SearchByDate" />
                        </div>
                        <br />
                        <div class="col-lg-3">
                            End Date<asp:TextBox ID="txtEnd" runat="server" CssClass="form-control"
                                type="date" AutoPostBack="true" OnTextChange="SearchByDate" />
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-xs-12 table">
                                <table id="datatable" class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Order Date</th>
                                            <th>Payment Method</th>
                                            <th>Customer</th>
                                            <th>Total Amount</th>
                                            <th>Status</th>
                                            <th></th>
                                            <%--<th>Date Added</th>
                                            <th>Date Modified</th>
                                            <th>Actions</th>--%>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <%--OnPagePropertiesChanging="lvRates_PagePropertiesChanging"--%>
                                        <asp:ListView ID="lvOrders" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval ("OrderNo") %></td>
                                                    <td><%# Eval("DateOrdered") %></td>
                                                    <td><%# Eval("PaymentMethod") %></td>
                                                    <td><%# Eval("CustomerName") %></td>
                                                    <td><%# Eval("TotalAmount","{0: #,##0.00}") %></td>
                                                    <td><%# Eval("Status") %></td>
                                                    <td><%# Eval("DateDelivered") %></td>
                                                    <td>
                                                        <a href='Details.aspx?ID=<%# Eval("OrderNo") %>' class="btn btn-xs btn-info"
                                                            title="View Details">
                                                            <i class="fa fa-list"></i>
                                                        </a>
                                                    </td>
                                                    <%--<td>
                                                        <a href='Announcement.aspx?EditID=<%# Eval("AnnouncementID") %>'><i class="fa fa-edit"></i></a>&nbsp;
                                                                <a href='Announcement.aspx?DeleteID=<%# Eval("AnnouncementID") %>' onclick="return confirm('Do you want to delete this item?')"><i class="fa fa-trash"></i></a>&nbsp;
                                                            </td>--%>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <tr>
                                                    <td colspan="10">
                                                        <h2 class="text-center">No records found.</h2>
                                                    </td>
                                                </tr>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <br />
                    <%--<div id="sidebar-menu" class="main_menu_side hidden-print main_menu">
                                <span class="pull-right">
                                    <button class="btn btn-default" onclick="window.print();"><i class="fa fa-print"></i>Print</button>
                                </span>
                            </div>--%>
                </div>
            </div>
            <%--                </ContentTemplate>
            </asp:UpdatePanel>--%>
        </form>
        <!-- /.col -->
    </div>
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

