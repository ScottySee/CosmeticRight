<%@ Page Title="" Language="C#" MasterPageFile="~/PrintPage.master" AutoEventWireup="true" CodeFile="PrintDelivery.aspx.cs" Inherits="PrintDelivery" %>

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
        <!-- page content -->
        <form runat="server" class="form-horizontal">
            <asp:ScriptManager runat="server" />
            <asp:UpdatePanel ID="Announcements" runat="server">
                <ContentTemplate>
                    <div class="card shadow-lg">
                        <div class="card-body">
                            <div class="container" id="printpage">
                                <div class="row m-3">
                                    <div class="col-xs-6 col-sm-6 col-md-6 text-white">
                                        <address>
                                            From:<br />
                                            <strong>Sure Option</strong>
                                            <br>
                                            2915 Purok 1, by Pass Road, Barangay Biga 1, Silang, Cavite
                                            <br>
                                            <abbr title="Phone">Phone:</abbr>
                                            0933-898-8751 / 0927-879-2286
                   
                                        </address>
                                    </div>
                                    <div class="col-xs-6 col-sm-6 col-md-6 text-right">
                                        <p>
                                            <em>Date: <%= Date %></em>
                                        </p>
                                    </div>
                                </div>
                                <div class="row m-3">
                                    <div class="col-xs-6 col-sm-6 col-md-6 text-white">
                                        <address>
                                            Sold To:
                                            <br />
                                            <strong><%=Customer %></strong><br />
                                            <%=Address %><br />
                                            <abbr title="Contact">Contact: </abbr>
                                            <%=Contact %>
                                        </address>
                                    </div>
                                </div>
                                <div class="row m-3">
                                    <div class="container">
                                        <div class="text-center">
                                            <h1>Delivery Receipt</h1>
                                        </div>
                                        <table class="table-active table" style="width: 100%;">
                                            <thead>
                                                <tr class="text-center">
                                                    <th>Product</th>
                                                    <th>Quantity</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:ListView ID="lvPrintOrders" runat="server">
                                                    <ItemTemplate>
                                                        <tr class="text-center">
                                                            <td><h4><%# Eval("Name") %></h4></td>
                                                            <td><%# Eval("Quantity") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <EmptyDataTemplate>
                                                        <tr>
                                                            <td colspan="6">
                                                                <h3 class="text-center">No records found.
                                                                </h3>
                                                            </td>
                                                        </tr>
                                                    </EmptyDataTemplate>
                                                </asp:ListView>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <input name="b_print" type="button" class="ipt btn btn-success" onclick="printdiv('printpage');" value="Print" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <script>
        function printdiv(printpage) {
            var headstr = "<html><head><title>Delivery Receipt</title></head><body>";
            var footstr = "</body>"
            var newstr = document.getElementById(printpage).innerHTML;
            var oldstr = document.body.innerHTML
            document.body.innerHTML = headstr + newstr + footstr;
            window.print();
            document.body.innerHTML = oldstr;
        }
    </script>
</asp:Content>