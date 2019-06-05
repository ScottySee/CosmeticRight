<%@ Page Title="" Language="C#" MasterPageFile="~/WarehouseAdmin.master" AutoEventWireup="true" CodeFile="ProductInventory.aspx.cs" ValidateRequest="false" Inherits="ProductInventory" %>

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
        <form runat="server" class="form-horizontal">
            <asp:ScriptManager runat="server" />
            <div class="card shadow-lg">
                <label runat="server" id="message1"></label>
                <div class="card-body">
                    <!-- Main Content -->
                    <div class="row">
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label class="control-label">ID</label>
                                <asp:TextBox ID="catID" runat="server" class="form-control" MaxLength="50" Disabled="true" />
                            </div>
                        </div>
                        <div class="col-lg-5">
                            <div class="form-group">
                                <label class="control-label">Product</label>
                                <div class="col-lg-8">
                                    <asp:DropDownList ID="ddlProduct" runat="server" class="form-control" required />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label class="control-label">Quantity</label>
                                <asp:TextBox ID="txtavailable" runat="server" class="form-control" type="number" min="1" Text="1" required/>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label class="control-label">Date Manufactured</label>
                                <asp:TextBox TextMode="date" ID="datestart" runat="server" class="form-control" min="01-01-2019" max="12-31-2019" required />
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label class="control-label">Date Expired</label>
                                <asp:TextBox TextMode="date" ID="dateend" runat="server" class="form-control" min="01-02-2019" max="12-31-2019" required />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container">
                    <div class="col-lg-4">
                        <div class="input-group">
                            <asp:Button ID="btnAdd" runat="server" class="btn btn-lg btn-success" Text="Add Inventory" OnClick="AddInventory" />
                            <%--<asp:Button ID="UpdateStatus" runat="server" class="btn btn-lg btn-success" Text="Update Status" OnClick="UpdateStatus_Click" />--%>
                            <%--<asp:Button ID="btnCancel" runat="server" hidden class="btn btn-lg btn-danger" Text="Cancel" OnClick="btnCancel_Click" />--%>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Table row -->
            <div class="card mt-5">
                <div class="card-body">
                    <div class="container">
                        <div class="text-white">
                            <center><h1>Inventory Record</h1></center>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 table">
                                <table id="dtProductInventory" class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th>ID</th>
                                            <th>Product</th>
                                            <th>Quantity</th>
                                            <th>Date Manufactured</th>
                                            <th>Date Expired</th>
                                            <th>Date Added</th>
                                            <th>Status</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:ListView ID="lvProductInventory" runat="server">
                                            <ItemTemplate>
                                                <tr class="bg-default">
                                                    <td><%# Eval("InventoryID") %></td>
                                                    <td><%# Eval("Product") %></td>
                                                    <td><%# Eval("Quantity") %></td>
                                                    <td><%# Eval("DateManufactured", "{0:MMM dd, yyyy}") %></td>
                                                    <td><%# Eval("DateExpired", "{0:MMM dd, yyyy}") %>
                                                    <td><%# Eval("DateAdded") %></td>
                                                    <td><%# Convert.ToDateTime(Eval("DateExpired", "{0:MMM dd, yyyy}")) < DateTime.Now ? "Expired" : Eval("Status") %></td>
                                                    <%--<td>
                                                        <a href='ProductInventory.aspx?EditID=<%# Eval("ID") %>' class="btn btn-info btn-sm"><i class="fa fa-edit"></i>Edit</a>&nbsp;--%>
                                                                <td><a href='ProductInventory.aspx?DeleteID=<%# Eval("InventoryID") %>' class="btn btn-danger btn-sm" onclick="return confirm('Do you want to archive this item?')"><i class="fa fa-trash"></i>Archive</a>&nbsp;
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
                    <br />
                </div>
            </div>

            <!-- Table row -->
            <div class="card mt-5">
                <div class="card-body">
                    <div class="container">
                        <div class="text-white">
                            <center><h1>Total Inventory</h1></center>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 table">
                                <table id="dtInventory" class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th>Product</th>
                                            <th>Quantity</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:ListView ID="lvInventory" runat="server">
                                            <ItemTemplate>
                                                <tr class="bg-default">
                                                    <td><%# Eval("Product") %></td>
                                                    <td><%# Eval("Quantity") %></td>
                                                    <%--<td>
                                                        <a href='ProductInventory.aspx?EditID=<%# Eval("ID") %>' class="btn btn-info btn-sm"><i class="fa fa-edit"></i>Edit</a>&nbsp;--%>
                                                                <%--<td><a href='ProductInventory.aspx?DeleteID=<%# Eval("InventoryID") %>' class="btn btn-danger btn-sm" onclick="return confirm('Do you want to archive this item?')"><i class="fa fa-trash"></i>Archive</a>&nbsp;
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
                </div>
            </div>
        </form>
    </div>
    <br />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="scripts">

    <%-- for data tables --%>
    
    <script>
        $(document).ready(function () {
            $('#dtProductInventory').DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'print'
                ]
            });
        });
    </script>
</asp:Content>