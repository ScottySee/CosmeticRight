<%@ Page Title="" Language="C#" MasterPageFile="~/WarehouseAdmin.master" AutoEventWireup="true" CodeFile="OrdersWarehouseAdmin.aspx.cs" Inherits="OrdersWarehouseAdmin" %>

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
            <asp:ScriptManager runat="server" />
            <%--<asp:UpdatePanel ID="Orders" runat="server">--%>
                <ContentTemplate>
                    <div class="card shadow-lg">
                        <div class="card-body">
                            <!-- Main Content -->
                            <div class="row">
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label class="control-label">OrderNo</label>
                                        <asp:TextBox ID="orderno" runat="server" class="form-control" MaxLength="50" ReadOnly="true" />
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label class="control-label">Date Ordered</label>
                                        <asp:TextBox ID="dateordered" runat="server" class="form-control" MaxLength="50" Disabled="true" required />
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label class="control-label">Payment Method</label>
                                        <asp:TextBox ID="payment" runat="server" class="form-control" MaxLength="50" Disabled="true" required />
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="control-label">Status</label>
                                       
                                        <asp:DropDownList ID="ddlStatus" runat="server"  class="form-control" required>
                                            <asp:ListItem Value="" style="color: black">---------------Select Status---------------</asp:ListItem>
                                            <asp:ListItem Value="Processing" style="color: black">Processing</asp:ListItem>
                                            <asp:ListItem Value="Packaging" style="color: black">Packagiing</asp:ListItem>
                                            <asp:ListItem Value="For Delivery" style="color: black">For Delivery</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="container">
                            <div class="col-lg-4">
                                <div class="input-group">
                                    <%--<asp:Button ID="btnAdd" runat="server" class="btn btn-lg btn-success" Text="Add Product" OnClick="AddProduct" />--%>
                                    <asp:Button ID="btnEdit" runat="server" OnClientClick="return confirm('Save changes?')" class="btn btn-lg btn-success" Text="Update Order" OnClick="SaveOrders" />
                                    <asp:Button ID="btnCancel" runat="server" hidden class="btn btn-lg btn-danger" Text="Cancel" OnClick="btnCancel_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    </div>

   
    <!-- Table row -->
                    <div class="card mt-5">
                        <div class="card-body">
                            <div class="container">
                                <div class="text-white">
                                    <center><h1>Orders</h1></center>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12 table">
                                        <table id="dtOrders1" class="table table-striped">
                                            <thead>
                                                <tr>
                                                    <th>Order No</th>
                                                    <th>Date Ordered</th>
                                                    <th>Payment Method</th>
                                                    <th>Status</th>
                                                    <th>Actions</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <%--OnPagePropertiesChanging="lvRates_PagePropertiesChanging"--%>
                                                <asp:ListView ID="lvOrders" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("OrderNo") %></td>
                                                            <td><%# Eval("DateOrdered") %></td>
                                                            <td><%# Eval("PaymentMethod") %></td>
                                                            <td><%# Eval("Status") %></td>
                                                            <td>
                                                                <a href='OrdersWarehouseAdmin.aspx?EditID=<%# Eval("OrderNo") %>' class="btn btn-info btn-sm"><i class="fa fa-edit"></i>Edit</a>&nbsp;
                                               
                                                                <%--<a href='products.aspx?DeleteID=<%# Eval("ProductID") %>' class="btn btn-danger btn-sm" onclick="return confirm('Do you want to delete this item?')"><i class="fa fa-trash"></i>Delete</a>&nbsp;--%>
                                            </td>
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
                        <%--<asp:DataPager ID="dpOrders" runat="server" PageSize="10"
                            PagedControlID="lvOrders">
                            <Fields>
                                <asp:NumericPagerField ButtonType="Button"
                                    CurrentPageLabelCssClass="btn btn-info"
                                    NumericButtonCssClass="btn btn-default"
                                    NextPreviousButtonCssClass="btn btn-default"
                                    ButtonCount="5" />
                            </Fields>
                        </asp:DataPager>--%>
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
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnAdd" />
                    <%--<asp:AsyncPostBackTrigger ControlID="btnAdd" />--%>
                </Triggers>
            <%--</asp:UpdatePanel>--%>
        </form>
        <!-- /.col -->
    </div>
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
     <%-- for data tables --%>
    <script>
        $(document).ready(function () {
            $('#dtOrders1').DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'print'
                ]
            });
        });
    </script>
</asp:Content>