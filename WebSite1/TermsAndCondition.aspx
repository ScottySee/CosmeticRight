<%@ Page Title="" Language="C#" MasterPageFile="~/Guest.master" AutoEventWireup="true" CodeFile="TermsAndCondition.aspx.cs" Inherits="TermsAndCondition" %>

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
            <asp:ScriptManager runat="server" />
            <a href="Register.aspx">Back to Register page</a>
            <div class="card shadow-lg">
                <div class="card-body">
                    <h1>Terms and Conditions</h1>
                    <br />
                    <h3>Refunds and Cancellation</h3>
		            <p>All completed transactions are final, the customers wouldn't be able to cancel the order within 24 hours after they have made the transaction.</p>
                    <br />
			        <p>If the customers want to cancel the order, it should been cancelled when the status of their orders are in pending, accepted or processing. And if the customer chooses Paypal for the payment, refund is still applicable, but the duration of the process will take at least 7-14 business days to complete.</p>
                    <br />
                    <%--<h3>Orders</h3>
                    <p>If the customer have made the orders, and they didn't </p>--%>
                </div>
            </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

