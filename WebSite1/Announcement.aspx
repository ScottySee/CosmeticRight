<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="Announcement.aspx.cs" Inherits="Announcement" %>

<asp:Content ContentPlaceHolderID="header" runat="server">
    <div class="header bg-gradient-gray-dark pb-5 pt-5 pt-md-8">
        <div class="container-fluid">
            <div class="header-body">
            </div>
        </div>
    </div>
</asp:Content>



<asp:Content runat="server" ContentPlaceHolderID="body">
    <div class="container mt--7">
        <!-- page content -->
        <form runat="server" classa="form-horizontal">
            <asp:ScriptManager runat="server" />
            <asp:UpdatePanel ID="upRates" runat="server">
                <ContentTemplate>
                    <div class="card shadow-lg">
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
                                                    <th>Announcement ID</th>
                                                    <th>Announcement Name</th>
                                                    <th>Announcement Detail</th>
                                                    <th>Image</th>
                                                    <th>Status</th>
                                                    <th>Actions</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <%--OnPagePropertiesChanging="lvRates_PagePropertiesChanging"--%>
                                                <asp:ListView ID="lvAnnouncements" runat="server">
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
            </asp:UpdatePanel>
        </form>
        <!-- /.col -->
    </div>
    <br />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="scripts">
<%--    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to save data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

    <script type="text/javascript">
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('table').DataTable({
                        pagingType: 'numbers',

                        responsive: {
                            details: {
                                type: 'column',

                            }
                        },


                        columnDefs: [{
                            className: 'control',
                            orderable: false,
                            targets: 0
                        }],
                        order: [1, 'asc']
                    });
                }
            });
        };

    </script>
    <script>
        $(document).ready(function () {
            $('table').DataTable({
                pagingType: 'numbers',

                responsive: {
                    details: {
                        type: 'column'
                    }
                },



                columnDefs: [{
                    className: 'control',
                    orderable: false,
                    targets: 0
                }],
                order: [1, 'asc'],

                dom: 'flrtiBp',
                buttons: [
                    {
                        extend: 'excel',
                        title: 'Saving Logistics',
                        messageTop: 'Route Report',
                        messageBottom: null,
                        //className: 'btn btn-success',


                    },
                    {
                        extend: 'colvis',
                        columns: ':not(:first-child)',
                        text: 'Show / Hide Columns'




                    }

                    //{
                    //    extend: 'print',
                    //    title: 'Saving Logistics',
                    //    messageTop: 'Booking Report',
                    //    messageBottom: null
                    //}
                    //{
                    //    extend: 'pdf',
                    //    title: 'Saving Logistics',
                    //    messageTop: 'Booking Report',
                    //    messageBottom: null
                    //}
                ],
            });

        });
    </script>--%>
</asp:Content>

