<%@ Page Title="" Language="C#" MasterPageFile="~/OfficeAdmin.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" ValidateRequest="false" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <div class="header bg-gradient-gray-dark pb-5 pt-5 pt-md-8">
        <div class="container-fluid">
            <div class="header-body">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <!-- Main content -->
    <%--<div class="main-content">--%>
    <div class="container mt--7" id="printpage">
        <div class="header bg-info pb-5 pt-5 pt-md-8">
           <div class="text-uppercase mb-0 text-muted">
                <center><h1><b>DASHBOARD</b></h1></center>
           </div>
            
            <!-- Top navbar -->
            <%--<nav class="navbar navbar-top navbar-expand-md navbar-dark" id="navbar-main">--%>
            <%--<div class="container-fluid">--%>
                <!-- Brand -->
                
                <!-- Form -->
            <%--</div>--%>
        </div>
        <!-- Header -->
        <div class="container-fluid">
            <div class="header-body" >
                <!-- Card stats -->
                <div class="row">
                    <div class="col-xl-3 col-lg-6">
                        <div class="card card-stats mb-4 mb-xl-0">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col">
                                        <h3 class="card-title text-uppercase mb-0 text-muted">Total Visits</h3>
                                        <asp:Label ID="count" runat="server" class="h1 font-weight-bold mb-0 text-muted form-control" />
                                        <%--<span class="h2 font-weight-bold mb-0 text-white">350,897</span>--%>
                                    </div>
                                    <div class="col-auto">
                                        <div class="icon icon-shape bg-danger text-black rounded-circle shadow">
                                            <i class="fas fa-chart-bar"></i>
                                        </div>
                                    </div>
                                </div>
                                <%--<p class="mt-3 mb-0 text-muted text-sm">
                                        <span class="text-success mr-2"><i class="fa fa-arrow-up"></i>3.48%</span>
                                        <span class=" text-white">Since last month</span>
                                    </p>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-3 col-lg-6">
                        <div class="card card-stats mb-4 mb-xl-0">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col">
                                        <h3 class="card-title text-uppercase mb-0 text-muted">Orders</h3>
                                        <asp:Label ID="count1" runat="server" class="h1 font-weight-bold mb-0 text-muted form-control" />
                                    </div>
                                    <div class="col-auto">
                                        <div class="icon icon-shape bg-warning text-black rounded-circle shadow">
                                            <i class="fas fa-chart-pie"></i>
                                        </div>
                                    </div>
                                </div>
                                <%--<p class="mt-3 mb-0 text-muted text-sm">
                                        <span class="text-danger mr-2"><i class="fas fa-arrow-down"></i>3.48%</span>
                                        <span class="text-nowrap">Since last week</span>
                                    </p>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-3 col-lg-6">
                        <div class="card card-stats mb-4 mb-xl-0">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col">
                                        <h3 class="card-title text-uppercase text-muted mb-0">Users</h3>
                                        <asp:Label ID="count2" runat="server" class="h1 font-weight-bold mb-0 text-muted form-control" />
                                    </div>
                                    <div class="col-auto">
                                        <div class="icon icon-shape bg-info text-black rounded-circle shadow">
                                            <i class="fas fa-users"></i>
                                        </div>
                                    </div>
                                </div>

                                <%--<p class="mt-3 mb-0 text-muted text-sm">
                                        <span class="text-warning mr-2"><i class="fas fa-arrow-down"></i>1.10%</span>
                                        <span class="text-nowrap">Since yesterday</span>
                                    </p>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-3 col-lg-6">
                        <div class="card card-stats mb-4 mb-xl-0">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col">
                                        <h3 class="card-title text-uppercase text-muted mb-0">Sales</h3>
                                        <asp:Label ID="sales" runat="server" class="h1 font-weight-bold mb-0 text-muted form-control"></asp:Label>
                                    </div>
                                    <div class="col-auto">
                                        <div class="icon icon-shape bg-success text-black rounded-circle shadow">
                                            <i class="fas fa-money-bill-alt"></i>
                                        </div>
                                    </div>
                                </div>
                                <%--<p class="mt-3 mb-0 text-muted text-sm">
                                        <span class="text-success mr-2"><i class="fas fa-arrow-up"></i>12%</span>
                                        <span class="text-nowrap">Since last month</span>
                                    </p>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--</div>--%>
        <br />
        <!-- Page content -->
        <div class="container-fluid mt--7">
            <div class="row">
                <div class="col-xl-6">
                    <div class="card bg-gradient-default shadow">
                        <div class="card-header bg-transparent">
                            <div class="row align-items-center">
                                <div class="col">
                                    <h6 class="text-uppercase text-muted ls-1 mb-1">Overview</h6>
                                    <h2 class="text-muted mb-0">Sales value</h2>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <!-- Chart -->
                            <asp:Chart ID="Chart1" runat="server" Width="350">
                                <%--<Titles>
                                    <asp:Title Text="Total marks of students"></asp:Title>
                                </Titles>--%>
                                <Series>
                                    <asp:Series Name="Series1"  ChartArea="ChartArea1">
                                        <%--<Points>
                                            <asp:DataPoint AxisLabel="Jan" YValues="19000" />
                                            <asp:DataPoint AxisLabel="Feb" YValues="50900" />
                                            <asp:DataPoint AxisLabel="March" YValues="48700" />
                                            <asp:DataPoint AxisLabel="April" YValues="65423" />
                                            <asp:DataPoint AxisLabel="May" YValues="44090" />
                                        </Points>--%>
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                        <AxisX Title="Month"></AxisX>
                                        <AxisY Title="Sales Value"></AxisY>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </div>
                    </div>
                </div>
                <div class="col-xl-6">
                    <div class="card shadow">
                        <div class="card-header bg-transparent">
                            <div class="row align-items-center">
                                <div class="col">
                                    <h6 class="text-uppercase text-muted ls-1 mb-1">Performance</h6>
                                    <h2 class=" text-muted mb-0">Total orders</h2>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">

                            <!-- Chart -->
                            <!-- Chart -->
                            <asp:Chart ID="Chart2" runat="server" Width="350" OnCustomize="Chart2_Customize">
                                <%--<Titles>
                                    <asp:Title Text="Total marks of students"></asp:Title>
                                </Titles>--%>
                                <Series>
                                    <asp:Series Name="Series1"  ChartArea="ChartArea1" Color="blue" >
                                        <%--<Points>
                                            <asp:DataPoint AxisLabel="Jan" YValues="38" />
                                            <asp:DataPoint AxisLabel="Feb" YValues="24" />
                                            <asp:DataPoint AxisLabel="March" YValues="30" />
                                            <asp:DataPoint AxisLabel="April" YValues="50" />
                                            <asp:DataPoint AxisLabel="May" YValues="44" />
                                        </Points>--%>
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                        <AxisX Title="Month"></AxisX>
                                        <AxisY Title="Orders"></AxisY>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                            <%--<div class="chart">
                                <canvas id="chart-orders" class="chart-canvas"></canvas>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
            
        </div>
        <input name="b_print" type="button" class="ipt btn btn-danger" onclick="printdiv('printpage');" value="Print" />
    </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <script>
        function printdiv(printpage) {
            var headstr = "<html><head><title>Sales Report</title></head><body>";
            var footstr = "</body>"
            var newstr = document.getElementById(printpage).innerHTML;
            document.body.innerHTML = headstr + newstr + footstr;
            //var printButton = document.getElementById("printpage");
            //printButton.style.visibility = 'hidden';
            //var oldstr = document.body.innerHTML
            window.print();
            //document.body.innerHTML = oldstr;
        }
    </script>
</asp:Content>