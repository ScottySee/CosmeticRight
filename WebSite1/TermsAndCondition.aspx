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
                    <h3>Cancellation</h3>
		            <p>All completed transactions are final, customers wouldn't be able to cancel the order when the order is at the stage of delivery. If the customers want to cancel the order, it should been cancelled when the status of their orders are before the stage of delivery.</p>
                    <br />
                    <h3>Refunds</h3>
                    <p>For Refunds. Customers can only refund the order once they have received it and the status of the order is Done. Once you have requested for refund, the admin will get it verifiied first. Once verified we will be sending you email for the transaction ID of your order in Paypal. After we have received your reply, then the process for refund will continue. Take note that customers need to have a Paypal account to process the refunds. The refund process will take at least 3-5 business days to complete.</p>
                    <p>But in some cases, if the customer selects Paypal as their payment method and they have cancelled the order, the process of refunds will proceed once the cancellation was approved.</p>
                    <br />
                    <%--<h3>Orders</h3>
                    <p>If the customer have made the orders, and they didn't </p>--%>
                </div>
            </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

