<%@ Page Title="" Language="C#" MasterPageFile="~/OfficeAdmin.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Reports" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
    <div class="header bg-gradient-gray-dark pb-5 pt-5 pt-md-8">
        <div class="container-fluid" id="home">
            <div class="header-body">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div class="container mt--7">
        <form runat="server" class="form-horizontal">
            <asp:ScriptManager runat="server" />
            <!-- Table row -->
            <div class="card mt-5">
                <div class="card-body">
                    <div class="container">
                        <div class="text-white">
                            <center><h1>Inventory Log Record</h1></center>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 table">
                                <table id="dtInventory" class="table table-striped">
                                    <thead>
                                        <tr style="text-align:center">
                                            <th>#</th>
                                            <th>User</th>
                                            <th>Product</th>
                                            <th>Quantity</th>
                                            <th>Time</th>
                                            <th>Activity</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:ListView ID="lvInventory" runat="server">
                                            <ItemTemplate>
                                                <tr class="bg-default" style="text-align:center">
                                                    <td><%# Eval("LogID") %></td>
                                                    <td><%# Eval("CustomerName") %></td>
                                                    <td><%# Eval("Product") %></td>
                                                    <td><%# Eval("Quantity") %></td>
                                                    <td><%# Eval("LogTime") %></td>
                                                    <td><%# Eval("Activity") %></td>
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
                </div>
            </div>
            <!-- Table row -->
            <div class="card mt-5">
                <div class="card-body">
                    <div class="container">
                        <div class="text-white">
                            <center><h1>Inventory Report</h1></center>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 table">
                                <table id="dtInventoryReport" class="table table-striped">
                                    <thead>
                                        <tr style="text-align:center">
                                            <%--<th>Year</th>--%>
                                            <th>Month</th>
                                            <th>ID</th>
                                            <th>Product Name</th>
                                            <th>Unit Price</th>
                                            <th>Quantity Added</th>
                                            <th>Quantity Sold</th>
                                            <th>Quantity Ending Balance</th>
                                            
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:ListView ID="lvInventoryReport" runat="server">
                                            <ItemTemplate>
                                                <tr class="bg-default" style="text-align:center">
                                                    <%--<td><%# Eval("Year") %></td>--%>
                                                    <td><%# Eval("Month") %></td>
                                                    <td><%# Eval("ProductID") %></td>
                                                    <td><%# Eval("Product") %></td>
                                                    <td><%# Eval("UnitPrice") %></td>
                                                    <td><%# (Eval("QuantityAdded").ToString() == "" ? "0" : Eval("QuantityAdded"))%></td>
                                                    <td><%# (Eval("QuantitySold").ToString() == "" ? "0" : Eval("QuantitySold"))%></td>
                                                    <td><%# (Eval("RemainingQuantity").ToString() == "" ? "0" : Eval("RemainingQuantity"))%></td>
                                                    
                                                    <%--<td><%# (Eval("TotalReturned").ToString() == "" ? "0" : Eval("TotalReturned"))%></td>--%>
                                                    
                                                    <%--<td><%# Eval("Gender").ToString() == "1" ? "Male" : "Female" %></td>--%>
                                                    <%--(Eval("totaladded") == null ? "0" : Eval("totaladded"))--%>
                                                    <%--<td><%# Convert.ToDateTime(Eval("DateExpired", "{0:MMM dd, yyyy}")) < DateTime.Now ? "Expired" : Eval("Status") %></td>--%>
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
                </div>
            </div>
            <!-- Table row -->
            <%--<div class="card mt-5">
                <div class="card-body">
                    <div class="container">
                        <div class="text-white">
                            <center><h1>Sales Report</h1></center>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 table">
                                <table id="dtSales" class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th>Order #</th>
                                            <th>User</th>
                                            <th>Month</th>
                                            <th>Amount</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:ListView ID="lvSales" runat="server">
                                            <ItemTemplate>
                                                <tr class="bg-default">
                                                    <%--<td><%# Eval("OrderNo") %></td>
                                                    <td><%# Eval("Username") %></td>
                                                    <td><%# Eval("DateOrdered") %></td>
                                                    <td>Php<%# Eval("Total", "{0: #,##0.00}") %></td>
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
                </div>
            </div>--%>
        </form>
    </div>
    <br />
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" Runat="Server">
    <script>
        $(document).ready(function () {
            $('#dtInventory').DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'pdf'
                ]
            });
        });
    </script>
    <script>
        $(document).ready(function () {
            $('#dtInventoryReport').DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'pdf'
                ]
            });
        });
    </script>
    <%--<script>
        $(document).ready(function () {
            $('#dtSales').DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'pdf'
                ]
            });
        });
    </script>--%>
</asp:Content>

