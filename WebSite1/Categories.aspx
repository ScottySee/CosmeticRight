<%@ Page Title="" Language="C#" MasterPageFile="~/WarehouseAdmin.master" AutoEventWireup="true" CodeFile="Categories.aspx.cs" Inherits="Categories" %>

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
                <div class="card-body">
                    <label runat="server" class="text-danger" id="message"></label>
                    <!-- Main Content -->
                    <div class="row">
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label class="control-label">ID</label>
                                <asp:TextBox ID="catID" runat="server" class="form-control" MaxLength="50" Disabled="true" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label class="control-label">Category</label>
                                <asp:TextBox ID="txtCategory" runat="server" class="form-control" MaxLength="50" placeholder="Enter Category" required />

                                <%--<asp:regularexpressionvalidator id="rev" runat="server" controltovalidate="txtCategory" xmlns:asp="#unknown">
    ErrorMessage="Spaces are not allowed!" ValidationExpression="^[^\s]+$" />
                                </asp:regularexpressionvalidator>--%>

                                <%--<asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" errormessage="Only .jpg , .png or .jpeg files are allowed." validationexpression=".*((\.jpg)|(\.jpeg)|(\.png))" controltovalidate="fileUpload1" xmlns:asp="#unknown">
                        </asp:regularexpressionvalidator>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container">
                    <div class="col-lg-4">
                        <div class="input-group">
                            <asp:Button ID="btnAdd" runat="server" class="btn btn-lg btn-success" Text="Add Category" OnClick="AddCategory" />
                            <asp:Button ID="btnEdit" runat="server" class="btn btn-lg btn-success" Text="Update Category" OnClick="SaveCategory" />
                            <asp:Button ID="btnCancel" runat="server" hidden class="btn btn-lg btn-danger" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- Table row -->
            <div class="card mt-5">
                <div class="card-body">
                    <div class="container">
                        <div class="text-white">
                            <center><h1>Category</h1></center>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 table">
                                <table id="dtcategories" class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th>Cat ID</th>
                                            <th>Category</th>
                                            <th>User</th>
                                            <th>Status</th>
                                            <th>Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:ListView ID="lvCategories" runat="server">
                                            <ItemTemplate>
                                                <tr class="bg-default">
                                                    <td><%# Eval("CatID") %></td>
                                                    <td><%# Eval("Category") %></td>
                                                    <td><%# Eval("Username") %></td>
                                                    <td><%# Eval("Status") %></td>
                                                    <td>
                                                        <a href='Categories.aspx?EditID=<%# Eval("CatID") %>' class="btn btn-info btn-sm"><i class="fa fa-edit"></i>Edit</a>&nbsp;
                                                                <a href='Categories.aspx?DeleteID=<%# Eval("CatID") %>' class="btn btn-danger btn-sm" onclick="return confirm('Do you want to archive this item?')"><i class="fa fa-trash"></i>Archive</a>&nbsp;
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
        </form>
    </div>
    <br />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="scripts">

    <%-- for data tables --%>
    <script>
            $(document).ready(function () {
                $('#dtcategories').DataTable({
                    dom: 'Bfrtip',
                    buttons: [
                    ]
                });
            });
    </script>

    <%-- <!-- Data tables Scripts -->
    <script src="https://code.jquery.com/jquery-3.3.1.js" type="text/javascript"></script>

    <script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.4/js/dataTables.buttons.min.js" type="text/javascript"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.4/js/buttons.flash.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.36/pdfmake.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.36/vfs_fonts.js" type="text/javascript"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.4/js/buttons.html5.min.js" type="text/javascript"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.4/js/buttons.print.min.js" type="text/javascript"></script>

    <!-- eto yung code para sa data tables -->
    <style>
        @media print {
            div {
                font-size: large;
            }

            @page {
                text-size-adjust: auto;
                background-size: auto;
                font-size: larger;
            }
        }
    </style>
    <script>
        $(document).ready(function () {
            $('#dtReport').DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'pdf'
                ]
            });
        });
    </script>--%>
</asp:Content>
