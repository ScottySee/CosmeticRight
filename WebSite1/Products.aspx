<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
    <div class="header bg-gradient-gray-dark pb-5 pt-5 pt-md-8">
        <div class="container-fluid">
            <div class="header-body">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div class="container mt--7">
        <!-- page content -->
        <form runat="server" classa="form-horizontal">
            <asp:scriptmanager runat="server" />
            <asp:updatepanel id="Products" runat="server">
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
                                        <label class="control-label">Title</label>
                                        <asp:TextBox ID="txtproduct" runat="server" class="form-control" MaxLength="50" placeholder="Enter Product" required />
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <label class="control-label">Images</label><br />
                                    <div class="input-group">
                                        <label class="btn btn-success">
                                            Upload Here
                                            <input type="file" id="imageupload" hidden runat="server" />
                                        </label>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <label class="control-label">Details</label>
                                        <asp:TextBox ID="txtdetails" TextMode="MultiLine" Rows="5" runat="server" class="form-control" MaxLength="50" placeholder="Enter Announcement" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="container">
                            <div class="col-lg-4">
                                <div class="input-group">
                                    <asp:Button ID="btnAdd" runat="server" class="btn btn-lg btn-success" Text="Add Announcement" OnClick="AddAnnouncement" />
                                    <asp:Button ID="btnEdit" runat="server" class="btn btn-lg btn-success" Text="Update Announcement"  OnClick="SaveAnnouncement" />
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
                <div class="row">
                    <div class="col-xs-12 table">
                        <table id="datatable" class="table table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Product ID</th>
                                    <th>Product Name</th>
                                    <th>CatID</th>
                                    <th>Code</th>
                                    <th>Description</th>
                                    <th>Image</th>
                                    <th>Price</th>
                                    <th>Available</th>
                                    <th>Critical Level</th>
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
                                            <td><%# Eval("AnnouncementID") %></td>
                                            <td><%# Eval("AnnouncementName") %></td>
                                            <td><%# Eval("AnnouncementDetail") %></td>
                                            <td><%# Eval("Image") %></td>
                                            <td><%# Eval("Status") %></td>
                                            <td>
                                                <a href='Announcement.aspx?EditID=<%# Eval("AnnouncementID") %>'><i class="fa fa-edit"></i></a>&nbsp;
                                                <a href='Announcement.aspx?DeleteID=<%# Eval("AnnouncementID") %>' onclick="return confirm('Do you want to delete this item?')"><i class="fa fa-trash"></i></a>&nbsp;
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

