<%@ Page Title="" Language="C#" MasterPageFile="~/Admin1.master" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="Products" %>

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
            <asp:scriptmanager runat="server" />
            <asp:updatepanel id="Product" runat="server">
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
                                        <asp:DropDownList ID="ddlCategories" runat="server" class="form-control" required />
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
                                    <div class="input-group">
                                        <label class="btn btn-success">
                                            Upload Here
                                            <input type="file" id="ImageUpload" hidden runat="server" />
                                        </label>
                                    </div>

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
                                        <label class="control-label">Critical Level</label>
                                        <asp:TextBox ID="txtCritical" runat="server" class="form-control" type="number" min="1" max="100" required />
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="control-label">Maximum</label>
                                        <asp:TextBox ID="txtMax" runat="server" class="form-control" type="number" min="1" max="100" required />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="container">
                            <div class="col-lg-4">
                                <div class="input-group">
                                    <asp:Button ID="btnAdd" runat="server" class="btn btn-lg btn-success" Text="Add Product" OnClick="AddProduct"/>
                                    <asp:Button ID="btnEdit" runat="server" class="btn btn-lg btn-success" Text="Update Product" OnClick="SaveProduct"/>
                                    <asp:Button ID="btnCancel" runat="server" hidden class="btn btn-lg btn-danger" Text="Cancel" OnClick="btnCancel_Click"/>
                                </div>
                            </div>
                        </div>
                    </div>
    </div>
    <!-- Table row -->
    <div class="card mt-5">
        <div class="card-body">
            <div class="container">
                <div class="row">
                    <div class="col-xs-12 table">
                        <table id="datatable" class="table table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Product ID</th>
                                    <th>Product Name</th>
                                    <th>Category</th>
                                    <th>Code</th>
                                    <th>Price</th>
                                    <th>Image</th>
                                    <th>Status</th>
                                    <th>Date Added</th>
                                    <th>Date Modified</th>
                                    <th>Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%--OnPagePropertiesChanging="lvRates_PagePropertiesChanging"--%>
                                <asp:listview id="lvAnnouncements" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("ProductID") %></td>
                                            <td><%# Eval("ProductName") %></td>
                                            <td><%# Eval("Categories") %></td>
                                            <td><%# Eval("Code") %></td>
                                            <td><%# Eval("Price") %></td>
                                            <td><%# Eval("Image") %></td>
                                            <td><%# Eval("Status") %></td>
                                            <td><%# Eval("DateAdded", "{0: MMMM dd, yyyy}") %></td>
                                            <td><%# Eval("DateModified", "{0: MMMM dd, yyyy}") %></td>
                                            <td>
                                                <a href='Announcement.aspx?EditID=<%# Eval("ProductID") %>'><i class="fa fa-edit"></i></a>&nbsp;
                                                <a href='Announcement.aspx?DeleteID=<%# Eval("ProductID") %>' onclick="return confirm('Do you want to delete this item?')"><i class="fa fa-trash"></i></a>&nbsp;
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
                                </asp:listview>
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
            </asp:UpdatePanel>
        </form>
        <!-- /.col -->
    </div>
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" Runat="Server">
</asp:Content>

