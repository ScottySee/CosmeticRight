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
    <form runat="server" id="ratingsForm">
        <div class="page-header header-filter">
            <div class="content">
                <div class="container">
                    <h1>Product Feedback</h1>
                    <div class="row justify-content-center">
                        <!--First column-->
                        <div class="col-md-6">
                            <div class="card">
                                <label runat="server" id="message1"></label>
                                <div class="card-body">
                                    <label class="control-label">Rating</label>
                                    <asp:ScriptManager runat="server"></asp:ScriptManager>


                                    <div class="stars" required>
                                        <input type="radio" name="star" class="star-1" value="1" id="star-1" />
                                        <label class="star-1" for="star-1">1</label>
                                        <input type="radio" name="star" class="star-2" value="2" id="star-2" />
                                        <label class="star-2" for="star-2">2</label>
                                        <input type="radio" name="star" class="star-3" value="3" id="star-3" />
                                        <label class="star-3" for="star-3">3</label>
                                        <input type="radio" name="star" class="star-4" value="4" id="star-4" />
                                        <label class="star-4" for="star-4">4</label>
                                        <input type="radio" name="star" class="star-5" value="5" id="star-5" />
                                        <label class="star-5" for="star-5">5</label>
                                        <span></span>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label col-lg-4">Product</label>
                                            <center><asp:DropDownList ID="ddlProduct" runat="server" class="form-control" required /></center>
                                    </div>
                                    <label class="control-label">Message</label>
                                    <div class="form-group">
                                        <asp:TextBox runat="server" ID="Message" Placeholder="Message" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="text-center pull-right">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-default submit btn-block" OnClick="btnSubmit_Click" />
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

