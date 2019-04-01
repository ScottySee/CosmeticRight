<%@ Page Title="" Language="C#" MasterPageFile="~/OfficeAdmin.master" AutoEventWireup="true" CodeFile="Logs.aspx.cs" Inherits="Logs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <div class="header bg-gradient-gray-dark pb-5 pt-5 pt-md-8">
        <div class="container-fluid">
            <div class="header-body">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <form runat="server" class="form-horizontal">
        <div class="container mt--7">
            <!-- page content -->
            <asp:ScriptManager runat="server" />
            <%--<asp:UpdatePanel ID="Announcements" runat="server">
                <ContentTemplate>--%>
            <div class="card shadow-lg">
                <div class="card-body">
                    <div class="col-lg-12">
                        <center><h1>Audit Logs</h1></center>
                        <table id="dtLogs" class="table table-hover">
                            <thead>
                                <th>#</th>
                                <th>User ID</th>
                                <th>Log Time</th>
                                <th>Activity</th>
                                <th></th>
                            </thead>
                            <tbody>
                                <asp:ListView ID="lvOrders" runat="server">
                                    <ItemTemplate>
                                        <tr class="bg-default">
                                            <td><%# Eval("LogID") %></td>
                                            <td><%# Eval("CustomerName") %></td>
                                            <td><%# Eval("LogTime", "{0:MM/dd/yyyy hh:mm tt}") %></td>
                                            <td><%# Eval("Activity") %></td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <tr>
                                            <td colspan="8">
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
        </div>

    </form>
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
<%-- for data tables --%>
    <script>
        $(document).ready(function () {
            $('#dtLogs').DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'print'
                ]
            });
        });
    </script>
</asp:Content>

