<%@ Page Title="" Language="C#" MasterPageFile="~/OfficeAdmin.master" AutoEventWireup="true" CodeFile="Announcement.aspx.cs" Inherits="Announcement" %>


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
            <asp:scriptmanager runat="server" />
            <asp:updatepanel runat="server" id="Upd1">
                <Triggers>  
                    <asp:PostBackTrigger ControlID="btnAdd" />  
                </Triggers>
                    <contenttemplate>
            <div class="card shadow-lg">
                <div class="card-body">
                    <label runat="server" id="message"></label>
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
                         <%--   <label for="body_fileUpload1" class="btn btn-success">--%>
                                Upload Image Here
                                        <asp:FileUpload  CssClass="btn btn alert-info"  ID="fileUpload1"  runat="server" />
                            <asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" errormessage="Only .jpg , .png or .jpeg files are allowed." validationexpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.jpg|.png|.jpeg)$" controltovalidate="fileUpload1" xmlns:asp="#unknown">
                        </asp:regularexpressionvalidator>
                            <%--</label>--%>
                        </div>

                        <div class="col-lg-3">
                            <div class="form-group">
                                <label class="control-label">Date Start:</label>
                                <%--<input type="text" id="dateControl" runat="server">--%>
                                <asp:TextBox ID="datestart" runat="server" class="form-control" placeholder="mm/dd/yyyy" />
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label class="control-label">Date End:</label>
                                <asp:TextBox ID="dateend" runat="server" class="form-control" placeholder="mm/dd/yyyy" />
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
                            <asp:Button ID="btnEdit" runat="server" class="btn btn-lg btn-success" Text="Update Announcement" OnClick="SaveAnnouncement" />
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
                            <center><h1>Announcements</h1></center>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 table">
                                <table id="dtannouncement" class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th>Announcement ID</th>
                                            <th>Announcement Name</th>
                                            <th>Announcement Detail</th>
                                            <th>Image</th>
                                            <th>Date Start</th>
                                            <th>Date End</th>
                                            <th>Status</th>
                                            <th>Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:listview id="lvAnnouncements" runat="server">
                                            <ItemTemplate>
                                                <tr class="bg-default">
                                                    <td><%# Eval("AnnouncementID") %></td>
                                                    <td><%# Eval("AnnouncementName") %></td>
                                                    <td><%# Eval("AnnouncementDetail") %></td>
                                                    <td>
                                                        <img src='/Images/Announcement/<%# Eval("Image") %>' class="img-fluid" width="100" /></td>
                                                    <td><%# Eval("DateStart", "{0:MMM dd, yyyy}")  %></td>
                                                    <td><%# Eval("DateEnd", "{0:MMM dd, yyyy}") %></td>
                                                    <td><%# Eval("Status") %></td>
                                                    <td>
                                                        <a href='Announcement.aspx?EditID=<%# Eval("AnnouncementID") %>' class="btn btn-info btn-sm"><i class="fa fa-edit"></i>Edit</a>&nbsp;
                                                                <a href='Announcement.aspx?DeleteID=<%# Eval("AnnouncementID") %>' class="btn btn-danger btn-sm" onclick="return confirm('Do you want to archive this item?')"><i class="fa fa-trash"></i>Archive</a>&nbsp;
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
                </div>
            </div>
                         </contenttemplate>
                </asp:updatepanel>
        </form>
    </div>
    <br />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="scripts">

    <%-- for data tables --%>
    <script>
        $(document).ready(function () {
            $('#dtannouncement').DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'print'
                ]
            });
        });
    </script>

<%--    <script>
$(document).ready(function () {
 $("#dateControl").datepicker();
});
    </script>--%>
    

    <%--<script type="text/javascript">
        $(document).ready(function () {
            $('#fileUpload1').uploadify({
                'uploader': 'uploadify/multiupload/uploadify.swf',
                'script': 'uploadify/multiupload/SampleUpload.ashx',
                'folder': 'Images/Announcement/',
                'auto': true,
                'multi': false,
                'removeCompleted': false,
                'sizeLimit': 512000,
                'fileExt': '*.jpg;*.jpeg;*.png',
                'fileDesc': 'Image Files',
            });
        });
    </script>--%>

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

