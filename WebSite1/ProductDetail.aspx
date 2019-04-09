<%@ Page Title="" Language="C#" MasterPageFile="~/Member.master" AutoEventWireup="true" CodeFile="ProductDetail.aspx.cs" Inherits="ProductDetail" %>

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
        <div class="container">
            <div class="col-lg-6">
                <div>
                    <a href="ProductDisplay.aspx">Back to Products page</a>
                </div>
                <br />
                <div class="text-white">
                    <h1>
                        <asp:Literal ID="ltName" runat="server" />
                        (<asp:Literal ID="ltCode" runat="server" />)
                    </h1>
                </div>
                <div class="col-lg-12">
                    <asp:Image ID="imgProduct" runat="server" class="img-responsive img-circle" Width="239" Height="180" Style="object-fit: cover" />
                </div>
                <div class="col-lg-6 text-white">
                    <br />
                    <h3>Description</h3>
                    <asp:Literal ID="ltDesc" runat="server" />
                    <hr />
                    <br />
                    <strong>Category: 
                    <asp:HyperLink ID="hlCategory" runat="server" /></strong>
                    <br />
                    <strong>Price: </strong>Php
                <asp:Literal ID="ltPrice" runat="server" />
                    <br />
                    <br />
                </div>
            </div>
            <div class="col-lg-3">
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
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

