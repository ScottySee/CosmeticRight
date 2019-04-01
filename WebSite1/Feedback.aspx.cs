﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Feedback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetProduct();
    }

    void GetProduct()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT ProductID, Name FROM Products WHERE Status!='Archived'";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    ddlProduct.DataSource = data;
                    ddlProduct.BackColor = System.Drawing.Color.Black;
                    ddlProduct.DataTextField = "Name";
                    ddlProduct.DataValueField = "ProductID";
                    ddlProduct.DataBind();
                    ddlProduct.Items.Insert(0, new ListItem("Select a product...", ""));
                }
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string star = "";
        if (Request.Form["star"] != null)
        {
            star = Request.Form["star"].ToString();
        }
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"INSERT INTO Feedback Values(@Datefeedback, @UserID, @Product, @Rating, @Comment)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Rating", star);
                cmd.Parameters.AddWithValue("@Comment", Server.HtmlEncode(Message.Text.Trim()));
                cmd.Parameters.AddWithValue("@Product", ddlProduct.SelectedValue);
                cmd.Parameters.AddWithValue("@Datefeedback", DateTime.Now);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                cmd.ExecuteNonQuery();

                //start of Auditlog 
                Util.Log(Session["UserID"].ToString(), "The Member has sent a feedback");
                //end of auditlog
            }
        }
    }
}