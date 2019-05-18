﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Member.master" AutoEventWireup="true" CodeFile="TermsAndCondition.aspx.cs" Inherits="TermsAndCondition" %>

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
            <a href="CheckoutCOD.aspx">Back to Order Summarry page</a>
            <div class="card shadow-lg">
                <div class="card-body">
                    <h1>Terms and Conditions</h1>
                    <br />
                    <h3>No Refunds and No Cancellation</h3>
		            <p>All completed transactions are final, customer would not be able to cancel or return a good once a purchase has been made.</p>
		            
                </div>
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

