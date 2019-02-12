<%@ Page Title="" Language="C#" MasterPageFile="~/Member.master" AutoEventWireup="true" CodeFile="Feedback.aspx.cs" Inherits="Feedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <div class="header bg-gradient-gray-dark pb-5 pt-5 pt-md-8">
        <div class="container-fluid" id="home">
            <div class="header-body">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <form runat="server">
        <div class="page-header header-filter">
            <div class="content">
                <div class="container">
                    <h1>Feedback</h1>
                    <div class="row justify-content-center">
                        <!--First column-->
                        <div class="col-md-6">
                            <div class="card">
                                <div class="card-body">
                                    <br />
                                    <label class="control-label">Subject</label>
                                    <div class="form-group">
                                        <asp:TextBox runat="server" ID="subject" Placeholder="Subject" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <label class="control-label">Message</label>
                                    <div class="form-group">
                                        <asp:TextBox runat="server" ID="Message" Placeholder="Message" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="text-center pull-right">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-default submit btn-block" OnClick="btnSubmit_Click"/>
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
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

