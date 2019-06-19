<%@ Page Title="" Language="C#" MasterPageFile="~/Member.master" AutoEventWireup="true" CodeFile="Refund.aspx.cs" Inherits="Refund" ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <form runat="server" class="form-horizontal">
    <div>
        <table class="table" visible="false">
            <tbody>
                <tr>
                    <td hidden>OrderNo</td>
                    <td align="right" hidden>
                        <asp:Literal ID="ltOrderNo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td hidden>Status</td>
                    <td align="right" hidden>
                        <asp:Literal ID="ltStatus" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td hidden>Payment Method</td>
                    <td align="right" hidden>
                        <asp:Literal ID="ltPaymentMethod" runat="server" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="page-header header-filter">
        <div class="content">
            <div class="container">
                <div class="row justify-content-center">
                    <!--First column-->
                    <div class="col-md-4">
                        <div class="card">
                            <div class="card-body">
                                <div class="form-group">
                                    <label class="control-label" style="color: white" >Reasons:</label>
                                    <asp:DropDownList Style="color: black" ID="ddlReason1" runat="server" class="form-control" required>
                                        <asp:ListItem Value="" style="color: black">------------------Select Reason------------------</asp:ListItem>
                                        <asp:ListItem Value="The item was damaged" style="color: black">The item was damaged</asp:ListItem>
                                        <asp:ListItem Value="Not the same picture as shown" style="color: black">Not the same picture as shown</asp:ListItem>
                                        <asp:ListItem Value="Others / Change of mind" style="color: black">Others / Change of mind</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnRefund" runat="server" Text="Submit" class="btn btn-block btn-success submit btn-block" OnClientClick='return confirm("Are you sure to refund this order?");' OnClick="btnSubmit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
        </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

