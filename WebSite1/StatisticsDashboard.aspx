<%@ Page Title="" Language="C#" MasterPageFile="~/OfficeAdmin.master" AutoEventWireup="true" CodeFile="StatisticsDashboard.aspx.cs" Inherits="StatisticsDashboard" %>

<asp:Content ContentPlaceHolderID="header" runat="server">
    <div class="header bg-gradient-gray-dark pb-5 pt-5 pt-md-8">
        <div class="container-fluid">
            <div class="header-body">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="body" runat="server">
    <div class="container mt--7">
        <!-- page content -->
        <form runat="server" classa="form-horizontal">
            <!-- Table row -->
            <div class="card mt-5">
                <div class="card-body">
                    <div class="container">
                        <div class="text-white">
                            <center><h1>Members</h1></center>
                        </div>
                        <div class="col-lg-4">
                            <div class="input-group">
                                <%--<asp:TextBox ID="txtKeyword" runat="server" class="form-control"
                                            placeholder="Search..." />
                                        <span class="input-group-btn">
                                            <asp:LinkButton ID="btnSearch" runat="server" class="btn btn-info"
                                                OnClick="btnSearch_Click">
                                                <i class="fa fa-search"></i>
                                            </asp:LinkButton>
                                        </span>--%>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 table">
                                <table id="datatable" class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th>ID</th>
                                            <th>User Type</th>
                                            <th>Firsname</th>
                                            <th>Lastname</th>
                                            <th>Gender</th>
                                            <th>Building No</th>
                                            <th>Street</th>
                                            <th>Municipality</th>
                                            <th>City</th>
                                            <th>Landline</th>
                                            <th>Mobile</th>
                                            <th>Email</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <%--OnPagePropertiesChanging="lvRates_PagePropertiesChanging"--%>
                                        <asp:ListView ID="lvUsers" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("UserID") %></td>
                                                    <td><%# Eval("UserType") %></td>
                                                    <td><%# Eval("Firstname") %></td>
                                                    <td><%# Eval("Lastname") %></td>
                                                    <td><%# Eval("Gender") %></td>
                                                    <td><%# Eval("BuildingNo") %></td>
                                                    <td><%# Eval("Street") %></td>
                                                    <td><%# Eval("Municipality") %></td>
                                                    <td><%# Eval("City") %></td>
                                                    <td><%# Eval("Landline") %></td>
                                                    <td><%# Eval("Mobile") %></td>
                                                    <td><%# Eval("Email") %></td>
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
                    <center>
                        <asp:DataPager ID="dpProducts" runat="server" PageSize="10"
                            PagedControlID="lvUsers">
                            <Fields>
                                <asp:NumericPagerField ButtonType="Button"
                                    CurrentPageLabelCssClass="btn btn-info"
                                    NumericButtonCssClass="btn btn-default"
                                    NextPreviousButtonCssClass="btn btn-default"
                                    ButtonCount="5" />
                            </Fields>
                        </asp:DataPager>
                    </center>
                    <br />
                    <%--<div id="sidebar-menu" class="main_menu_side hidden-print main_menu">
                                <span class="pull-right">
                                    <button class="btn btn-default" onclick="window.print();"><i class="fa fa-print"></i>Print</button>
                                </span>
                            </div>--%>
                </div>
            </div>
            </ContentTemplate>
                <triggers>
                    <asp:PostBackTrigger ControlID="btnAdd" />
                    <%--<asp:AsyncPostBackTrigger ControlID="btnAdd" />--%>
                </triggers>
            </asp:UpdatePanel>

             <!-- Table row -->
            <div class="card mt-5">
                <div class="card-body">
                    <div class="container">
                        <div class="text-white">
                            <center><h1>Products</h1></center>
                        </div>
                        <div class="col-lg-4">
                            <div class="input-group">
                                <%--<asp:TextBox ID="txtKeyword" runat="server" class="form-control"
                                            placeholder="Search..." />
                                        <span class="input-group-btn">
                                            <asp:LinkButton ID="btnSearch" runat="server" class="btn btn-info"
                                                OnClick="btnSearch_Click">
                                                <i class="fa fa-search"></i>
                                            </asp:LinkButton>
                                        </span>--%>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 table">
                                <table id="datatable" class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th>ID</th>
                                            <th>Product Name</th>
                                            <th>Category</th>
                                            <th>Critical Level</th>
                                            <th>Maximum</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <%--OnPagePropertiesChanging="lvRates_PagePropertiesChanging"--%>
                                        <asp:ListView ID="lvProducts" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("ProductID") %></td>
                                                    <td><%# Eval("Name") %></td>
                                                    <td><%# Eval("Category") %></td>
                                                    <td><%# Eval("Criticallevel") %></td>
                                                    <td><%# Eval("Maximum") %></td>
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
                    <center>
                        <asp:DataPager ID="DataPager1" runat="server" PageSize="10"
                            PagedControlID="lvUsers">
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

            <%--<asp:Literal ID="chart" runat="server"></asp:Literal>--%>
        </form>
        <!-- /.col -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

