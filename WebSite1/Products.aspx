<%@ Page Title="" Language="C#" MasterPageFile="~/WarehouseAdmin.master" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="Products" ValidateRequest="false" %>

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
            <%--<asp:UpdatePanel runat="server" ID="Upd1">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnAdd" />
                    <%--<asp:AsyncPostBackTrigger ControlID="btnAdd" />
                </Triggers>
                <ContentTemplate>--%>
            <div class="card shadow-lg">
                <div class="card-body">
                    <label runat="server" class="text-danger" id="message"></label>
                    <!-- Main Content -->
                    <div class="row">
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label class="control-label">ID</label>
                                <asp:TextBox ID="productID" runat="server" class="form-control" MaxLength="50" Disabled="true" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label class="control-label">Product Name</label>
                                <asp:TextBox ID="txtProductName" runat="server" class="form-control" MaxLength="50" required />
                            </div>
                        </div>
                        <!-- dapat ata eto para sa category -->
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label class="control-label col-lg-4">Category</label>
                                <div class="col-lg-8">
                                    <asp:DropDownList ID="ddlCategories" runat="server" class="form-control" required />
                                </div>
                            </div>
                        </div>
                        <%--<div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="control-label">Category</label>
                                        <asp:DropDownList ID="ddlCategories" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategories_SelectedIndexChanged" class="form-control" required>
                                            <asp:ListItem Value="" style="color: black">---------------Select Category---------------</asp:ListItem>
                                            <asp:ListItem Value="1" style="color: black">Lotion</asp:ListItem>
                                            <asp:ListItem Value="2" style="color: black">Toners</asp:ListItem>
                                            <asp:ListItem Value="3" style="color: black">Soap</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div--%>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label class="control-label">Code</label>
                                <asp:TextBox ID="txtCode" runat="server" class="form-control" MaxLength="10" required />
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label class="control-label">Description</label>
                                <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="5" runat="server" class="form-control" MaxLength="50" />
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <label class="control-label">Images</label><br />
                            <%--   <label for="body_fileUpload1" class="btn btn-success">--%>
                                Upload Image Here
                                        <asp:FileUpload CssClass="btn btn alert-info" ID="fileUpload1" runat="server" />
                            <asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" errormessage="Only .jpg , .png or .jpeg files are allowed." validationexpression=".*((\.jpg)|(\.jpeg)|(\.png))" controltovalidate="fileUpload1" xmlns:asp="#unknown">
                        </asp:regularexpressionvalidator>
                            <%--</label>--%>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label class="control-label">Price</label>
                                <asp:TextBox ID="txtPrice" runat="server" class="form-control" type="number" min="0.01" max="500000.00" step="0.01" required />
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label class="control-label">Critical Level</label>
                                <asp:TextBox ID="txtCritical" runat="server" class="form-control" type="number" min="1" max="200" required />
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label class="control-label">Maximum</label>
                                <asp:TextBox ID="txtMax" runat="server" class="form-control" type="number" min="201" max="1000" required />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container">
                    <div class="col-lg-4">
                        <div class="input-group">
                            <asp:Button ID="btnAdd" runat="server" class="btn btn-lg btn-success" Text="Add Product" OnClick="AddProduct" />
                            <asp:Button ID="btnEdit" runat="server" class="btn btn-lg btn-success" Text="Update Product" OnClick="SaveProduct" />
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
                            <center><h1>Products</h1></center>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 table">
                                <table id="dtproducts" class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Product Name</th>
                                            <th>Category</th>
                                            <th>Code</th>
                                            <th>Price</th>
                                            <th>Image</th>
                                            <th>Status</th>
                                            <th>Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <%--OnPagePropertiesChanging="lvRates_PagePropertiesChanging"--%>
                                        <asp:ListView ID="lvProducts" runat="server">
                                            <ItemTemplate>
                                                <tr class="bg-default">
                                                    <td><%# Eval("ProductID") %></td>
                                                    <td><%# Eval("Product") %></td>
                                                    <td><%# Eval("CatID") %></td>
                                                    <td><%# Eval("Code") %></td>
                                                    <td>Php <%# Eval("Price") %></td>
                                                    <td><img src='/Images/Products/<%# Eval("Image") %>' class="img-fluid" width="100" /></td>
                                                    <td><%# Eval("Status") %></td>
                                                    <td>
                                                        <a href='Products.aspx?EditID=<%# Eval("ProductID") %>' class="btn btn-info btn-sm"><i class="fa fa-edit"></i>Edit</a>&nbsp;
                                                        <a href='products.aspx?DeleteID=<%# Eval("ProductID") %>' class="btn btn-danger btn-sm" onclick="return confirm('Do you want to archive this item?')"><i class="fa fa-trash"></i>Archive</a>&nbsp;
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
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <%-- for data tables --%>
    <script>
        $(document).ready(function () {
            $('#dtproducts').DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'pdf'
                ]
            });
        });
    </script>
</asp:Content>

