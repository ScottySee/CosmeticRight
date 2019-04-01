<%@ Page Title="" Language="C#" MasterPageFile="~/Guest.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <div class="header bg-gradient-gray-dark pb-5 pt-5 pt-md-8">
        <div class="container-fluid">
            <div class="header-body" id="home">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="page-header header-filter">
        <div class="content">
            <div class="container">
                <h1>Welcome to Cosmetic Right</h1>
                <div class="row justify-content-center">
                </div>
            </div>
        </div>


    </div>
    <div class="main">
        <div class="container py-md">
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <h2 class="display-3" id="about">About Us 
                    </h2>
                    <div>
                        <h3>Sure Options is a company that produces cosmedic products that are sold to dermatology clinics. The company aims to provide the best quality of skincare products to our clients and to develop a better environment thru beautiful people.
                        </h3>
                    </div>
                </div>
            </div>
        </div>

        <div class="container">
            <div class="row row-grid justify-content-between align-items-center">
                <div class="col-lg-6">
                    <h3 class="display-3 text-white" id="contact">Contact Us
                    </h3>
                    <h5 class="text-white">Reach us through the following contact details</h5>

                    <div class="card shadow shadow-lg--hover mt-5">
                        <div class="card-body">
                            <div class="d-flex px-3">
                                <div>
                                    <div class="icon icon-shape bg-gradient-success rounded-circle text-white">
                                        <i class="fa fa-map-marker-alt"></i>
                                    </div>
                                </div>
                                <div class="pl-4">
                                    <h5 class="title text-success">Location</h5>
                                    <p>
                                        2915 Purok 1, by Pass Road, Barangay Biga 1,
                                        Silang, Cavite
                                    </p>
                                </div>
                            </div>
                            <div class="d-flex px-3">
                                <div class="icon icon-shape bg-gradient-success rounded-circle text-white">
                                    <i class="fa fa-mobile-alt"></i>
                                </div>
                                <div class="pl-4">
                                    <h5 class="title text-success">Mobile Number</h5>
                                    <p>0933-898-8751</p>
                                    <p>0927-879-2286</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

