<%@ Page Title="" Language="C#" MasterPageFile="~/OfficeAdmin.master" AutoEventWireup="true" CodeFile="ViewFeedback.aspx.cs" Inherits="ViewFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <div class="header bg-gradient-gray-dark pb-5 pt-5 pt-md-8">
        <div class="container-fluid">
            <div class="header-body">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <!-- Table row -->
    <div class="container mt--7">
        <!-- page content -->
        <form runat="server" class="form-horizontal">
            <asp:ScriptManager runat="server" />
            <%--            <asp:UpdatePanel ID="Announcements" runat="server">
                <ContentTemplate>--%>
            <%--<div class="card shadow-lg">
                        <div class="card-body">
                            <!-- Main Content -->
                            <div class="row">
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label class="control-label">ID</label>
                                        <asp:TextBox ID="announcementID" runat="server" class="form-control" MaxLength="50" Disabled="true" />
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label class="control-label">Title</label>
                                        <asp:TextBox ID="txtannouncement" runat="server" class="form-control" MaxLength="50" placeholder="Enter Announcement" required />
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <label class="control-label">Images</label><br />
                                    <label for="body_fileUpload1" class="btn btn-success">
                                        Upload Image Here
                                        <asp:FileUpload CssClass="btn btn alert-info" hidden ID="fileUpload1" runat="server" />
                                    </label>
                                </div>
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <label class="control-label">Details</label>
                                        <asp:TextBox ID="txtdetails" TextMode="MultiLine" Rows="5" runat="server" class="form-control" MaxLength="50" placeholder="Enter Announcement" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--<div class="container">
                            <div class="col-lg-4">
                                <div class="input-group">
                                    <asp:Button ID="btnAdd" runat="server" class="btn btn-lg btn-success" Text="Add Announcement" OnClick="AddAnnouncement" />
                                    <asp:Button ID="btnEdit" runat="server" class="btn btn-lg btn-success" Text="Update Announcement" OnClick="SaveAnnouncement" />
                                    <asp:Button ID="btnCancel" runat="server" hidden class="btn btn-lg btn-danger" Text="Cancel" OnClick="btnCancel_Click" />
                                </div>
                            </div>
                        </div>
                    </div>--%>

            <!-- Table row -->
            <div class="card mt-5">
                <div class="card-body">
                    <div class="container">
                        <div class="text-white">
                            <center><h1>Feedback</h1></center>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 table">

                                <table id="datatable" class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th>Feedback ID</th>
                                            <th>Comment</th>
                                            <th>Rating</th>
                                            <th>Datefeedback</th>
                                            <%--<th>Status</th>
                                            <th>Date Added</th>
                                            <th>Date Modified</th>
                                            <th>Actions</th>--%>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <%--OnPagePropertiesChanging="lvRates_PagePropertiesChanging"--%>
                                        <asp:ListView ID="lvFeedback" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("FeedbackID") %></td>
                                                    <td><%# Eval("Comment") %></td>
                                                    <td><%# Eval("Rating") %> out of 5</td>
                                                    
                                                        <%--<img src='/Images/Announcement/<%# Eval("Image") %>' class="img-fluid" width="100" /></td>
                                                    <td><%# Eval("Image") %></td>--%>
                                                    <td><%# Eval("Datefeedback") %></td>
                                                    <%--<td><%# Eval("DateAdded", "{0: MMMM dd, yyyy}") %></td>
                                                            <td><%# Eval("DateModified", "{0: MMMM dd, yyyy}") %></td>--%>
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
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

