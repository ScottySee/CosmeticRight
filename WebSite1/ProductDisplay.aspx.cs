﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

public partial class ProductDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCategories();

            if (Request.QueryString["c"] != null)
            {
                GetSpecificProduct(Request.QueryString["c"].ToString());
            }
            else
            {
                GetProducts();
            }
        }
    }

    void GetCategories()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT c.CatID, c.Category,
                                (SELECT COUNT(ProductID) FROM Products WHERE CatID = c.CatID)
                                AS TotalCount FROM Categories c ORDER BY Category ";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvCategories.DataSource = data;
                    lvCategories.DataBind();
                }
            }
        }
    }

    void GetProducts()
    {
        {
            using (SqlConnection con = new SqlConnection(Util.GetConnection()))
            {
                con.Open();
                string query = @"SELECT distinct p.ProductID, p.Image, p.Product,
                                    p.Code, p.Price, c.Category, (Select Quantity from Inventory where ProductID = i.ProductID and Quantity > p.Criticallevel) as Quantity FROM Products p
                                    INNER JOIN Categories c ON p.CatID = c.CatID
                                    INNER JOIN Inventory i ON p.ProductID = i.ProductID
                                    where i.Quantity > p.Criticallevel";

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

    void GetSpecificProduct(string code)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT distinct p.ProductID, p.Image, p.Product,
                                p.Code, p.Price, c.Category, (Select Quantity from Inventory where ProductID = i.ProductID and Quantity > p.Criticallevel) as Quantity FROM Products p
                                INNER JOIN Categories c ON p.CatID = c.CatID
                                INNER JOIN Inventory i ON p.ProductID = i.ProductID
                                where i.Quantity > p.Criticallevel AND p.CatID = @Code";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Code", code);
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

    protected void lvProducts_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "addtocart")
        {
            string QTY = "";
            Literal ltID = (Literal)e.Item.FindControl("ltID");
            Label category = (Label)e.Item.FindControl("category");
            if(category.Text == "Soap")
            {
                QTY = "200";
            }else if (category.Text == "Lotion")
            {
                QTY = "3";
            }else if (category.Text == "Toner")
            {
                QTY = "3";
            }

            Util.AddToCart(ltID.Text, QTY);
        }
    }
}