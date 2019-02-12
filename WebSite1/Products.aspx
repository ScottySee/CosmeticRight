<%@ Page Title="" Language="C#" MasterPageFile="~/WarehouseAdmin.master" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="Products" %>

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
            <asp:UpdatePanel ID="Product" runat="server">
                <ContentTemplate>
                    <div class="card shadow-lg">
                        <div class="card-body">
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
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="control-label">Category</label>
                                        <asp:DropDownList ID="ddlCategories" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategories_SelectedIndexChanged" class="form-control" required>
                                            <asp:ListItem Value="" style="color: black">---------------Select Category---------------</asp:ListItem>
                                            <asp:ListItem Value="1" style="color: black">Lotion</asp:ListItem>
                                            <asp:ListItem Value="2" style="color: black">Toners</asp:ListItem>
                                            <asp:ListItem Value="3" style="color: black">Soap</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
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
                                    <label for="body_fileUpload" class="btn btn-success">
                                        Upload Image Here
                                        <asp:FileUpload CssClass="btn btn alert-info" hidden ID="fileUpload" runat="server" />
                                    </label>

                                    <%--<asp:FileUpload id="fupload" runat="server" Height="21px" Width="220px" />
                                    <asp:Button id="ButtonUpload" OnClick="ButtonUpload_Click" CssClass="btn btn-lg btn-success" runat="server" />--%>
                                    <%--<div>
                                        <span class="btn btn-default btn-file"><span class="fileinput-new">Select image</span><span class="fileinput-exists">Change</span><asp:FileUpload ID="fuImage" runat="server" required /></span>
                                        <a href="#" class="btn btn-default fileinput-exists" data-dismiss="fileinput">Remove</a>
                                    </div>--%>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="control-label">Price</label>
                                        <asp:TextBox ID="txtPrice" runat="server" class="form-control" type="number" min="0.01" max="500000.00" step="0.01" required />
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="control-label">Unit Weight</label>
                                        <asp:Label ID="lblunit" runat="server" class="form-control" required />
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="control-label">Critical Level</label>
                                        <asp:TextBox ID="txtCritical" runat="server" class="form-control" type="number" min="1" max="100" required />
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="control-label">Maximum</label>
                                        <asp:TextBox ID="txtMax" runat="server" class="form-control" type="number" min="100" max="1000" required />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="container">
                            <div class="col-lg-4">
                                <div class="input-group">
                                    <asp:Button ID="btnAdd" runat="server" class="btn btn-lg btn-success" Text="Add Product" OnClick="AddProduct" />
                                    <asp:Button ID="btnEdit" runat="server" OnClientClick="return confirm('Save changes?')" class="btn btn-lg btn-success" Text="Update Product" OnClick="SaveProduct" />
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
                                    <center><h1>Products</h1></center>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12 table">
                                        <table id="datatable" class="table table-striped">
                                            <thead>
                                                <tr>
                                                    <th>Product ID</th>
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
                                                        <tr>
                                                            <td><%# Eval("ProductID") %></td>
                                                            <td><%# Eval("Name") %></td>
                                                            <td><%# Eval("CatID") %></td>
                                                            <td><%# Eval("Code") %></td>
                                                            <td><%# Eval("Price") %></td>

                                                            <td>
                                                                <img src='/Images/Products/<%# Eval("Image") %>' class="img-fluid" width="100" /></td>
                                                            <td><%# Eval("Status") %></td>
                                                            <td>
                                                                <a href='Products.aspx?EditID=<%# Eval("ProductID") %>' class="btn btn-info btn-sm"><i class="fa fa-edit"></i>Edit</a>&nbsp;
                                               
                                                                <a href='products.aspx?DeleteID=<%# Eval("ProductID") %>' class="btn btn-danger btn-sm" onclick="return confirm('Do you want to delete this item?')"><i class="fa fa-trash"></i>Delete</a>&nbsp;
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
            </asp:UpdatePanel>
        </form>
        <!-- /.col -->
    </div>
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

