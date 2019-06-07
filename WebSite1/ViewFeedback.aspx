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
    <div class="container mt--7">
        <form runat="server" class="form-horizontal">
            <asp:ScriptManager runat="server" />
            <!-- Table row -->
            <div class="card mt-5">
                <div class="card-body">
                    <div class="container">
                        <div class="text-white">
                            <center><h1>Feedback</h1></center>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 table">

                                <table id="dtFeedback" class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th>Feedback ID</th>
                                            <th>Product</th>
                                            <th>User</th>
                                            <th>Comment</th>
                                            <th>Rating</th>
                                            <th>Datefeedback</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:ListView ID="lvFeedback" runat="server">
                                            <ItemTemplate>
                                                <tr class="bg-default">
                                                    <td><%# Eval("FeedbackID") %></td>
                                                    <td><%# Eval("Product") %></td>
                                                    <td><%# Eval("CustomerName") %></td>
                                                    <td><%# Eval("Comment") %></td>
                                                    <td><%# Eval("Rating") %> out of 5</td>
                                                    <td><%# Eval("Datefeedback") %></td>
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
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <%-- for data tables --%>
    <script>
        $(document).ready(function () {
            $('#dtFeedback').DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'pdf'
                ]
            });
        });
    </script>
</asp:Content>

