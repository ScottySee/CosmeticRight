﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FusionCharts.Charts;

public partial class StatisticsDashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            GetUser();
            GetProducts();
        }

        //Chart newChart = new Chart("column2d", "simplechart", "600", "400", "jsonurl", "data.json");
        //chart.Text = newChart.Render();
    }

    void GetUser()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT * FROM Users";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Users");
                    lvUsers.DataSource = ds;
                    lvUsers.DataBind();
                }
            }
        }
    }

    void GetProducts()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT ProductID, Name, Categories.Category, Criticallevel, Maximum FROM Products
                                INNER JOIN Categories ON Products.CatID=Categories.CatID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Products");
                    lvProducts.DataSource = ds;
                    lvProducts.DataBind();
                }
            }
        }
    }
}