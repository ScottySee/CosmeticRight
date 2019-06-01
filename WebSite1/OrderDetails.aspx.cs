﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

public partial class OrderDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null)
        {
            int orderNo = 0;
            bool validOrder = int.TryParse(Request.QueryString["ID"].ToString(), out orderNo);

            if (validOrder)
            {
                if (!IsPostBack)
                {
                    GetOrderDetails(orderNo);
                    GetOrderSummary(orderNo);
                    GetOrderInfo(orderNo);
                    GetCustomerInfo(orderNo);
                    GetCity();
                    if (ltStatus.Text == "Cancelled" || ltStatus.Text == "For Delivery")
                    {
                        btnCancel.Visible = false;
                    }
                }
            }
            else
                Response.Redirect("Orders.aspx");
        }
        else
            Response.Redirect("Orders.aspx");
    }

    void GetCity()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT CityID, City FROM City";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    ddlCity.DataSource = data;
                    ddlCity.BackColor = System.Drawing.Color.Black;
                    ddlCity.DataTextField = "City";
                    ddlCity.DataValueField = "CityID";
                    ddlCity.DataBind();
                    ddlCity.Items.Insert(0, new ListItem("Select a city...", ""));
                }
            }
        }
    }

    void GetOrderInfo(int ID)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT OrderNo, DateOrdered, PaymentMethod, Status
                            FROM Orders WHERE OrderNo=@OrderNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo", ID);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            ltOrderNo.Text = data["OrderNo"].ToString();
                            ltStatus.Text = data["Status"].ToString();

                            //btnAccept.Visible = ltStatus.Text == "Pending" ? true : false;
                            //btnReject.Visible = ltStatus.Text == "Pending" ? true : false;

                            ltDateOrdered.Text = data["DateOrdered"].ToString();
                            ltPaymentMethod.Text = data["PaymentMethod"].ToString();
                        }
                    }
                    else
                    {
                        Response.Redirect("Orders.aspx");
                    }
                }
            }
        }
    }

    void GetCustomerInfo(int ID)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT TOP 1 u.Firstname, u.Lastname, u.BuildingNo,
                                u.Street, u.Municipality, u.CityID, u.Landline,  
                                u.Mobile FROM OrderDetails od
                                    INNER JOIN Users u ON od.UserID= u.UserID
                                      WHERE od.OrderNo=@OrderNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo", ID);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    while (data.Read())
                    {
                        txtFN.Text = data["Firstname"].ToString();
                        txtLN.Text = data["Lastname"].ToString();
                        txtbuilding.Text = data["BuildingNo"].ToString();
                        txtStreet.Text = data["Street"].ToString();
                        txtMunicipality.Text = data["Municipality"].ToString();
                        ddlCity.Text = data["CityID"].ToString();
                        txtPhone.Text = data["Landline"].ToString();
                        txtMobile.Text = data["Mobile"].ToString();
                    }
                }
            }
        }
    }
    void GetOrderDetails(int ID)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT od.RefNo, p.ProductID, p.Image, p.Product, c.Category,
                p.Price, od.Quantity, od.Amount FROM OrderDetails od
                INNER JOIN Products p ON od.ProductID = p.ProductID 
                INNER JOIN Categories c ON p.CatID = c.CatID
                WHERE od.OrderNo=@OrderNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo", ID);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    lvCart.DataSource = dr;
                    lvCart.DataBind();
                }
            }
        }
    }

    void GetOrderSummary(int ID)
    {

        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT SUM(Amount) FROM OrderDetails
                WHERE OrderNo=@OrderNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo", ID);
                double totalAmount = cmd.ExecuteScalar() == null ? 0 :
                    Convert.ToDouble((decimal)cmd.ExecuteScalar());

                ltGross.Text = (totalAmount * .88).ToString("#,##0.00");
                ltVAT.Text = (totalAmount * .12).ToString("#,##0.00");
                //ltDelivery.Text = (totalAmount * .1).ToString("#,##0.00");
                ltTotal.Text = (totalAmount * 1).ToString("#,##0.00");
            }
        }
    }

    //protected void btnAccept_Click(object sender, EventArgs e)
    //{
    //    using (SqlConnection con = new SqlConnection(Util.GetConnection()))
    //    {
    //        con.Open();
    //        string query = @"UPDATE Orders SET Status =@Status
    //                        WHERE OrderNo=@OrderNo;
    //                          UPDATE OrderDetails SET Status=@Status2
    //                            WHERE OrderNo=@OrderNo;";
    //        using (SqlCommand cmd = new SqlCommand(query, con))
    //        {
    //            cmd.Parameters.AddWithValue("@Status", "Accepted");
    //            cmd.Parameters.AddWithValue("OrderNo", ltOrderNo.Text);
    //            //cmd.Parameters.AddWithValue("@Status2", "For Delivery");
    //            cmd.ExecuteNonQuery();
    //            Response.Redirect("Orders.aspx");
    //        }
    //    }
    //}

    //protected void btnReject_Click(object sender, EventArgs e)
    //{
    //    using (SqlConnection con = new SqlConnection(Util.GetConnection()))
    //    {
    //        con.Open();
    //        string query = @"UPDATE Orders SET Status =@Status
    //                        WHERE OrderNo=@OrderNo;
    //                          UPDATE OrderDetails SET Status=@Status2
    //                            WHERE OrderNo=@OrderNo";
    //        using (SqlCommand cmd = new SqlCommand(query, con))
    //        {
    //            cmd.Parameters.AddWithValue("@Status", "Rejected");
    //            cmd.Parameters.AddWithValue("OrderNo", ltOrderNo.Text);
    //            //cmd.Parameters.AddWithValue("@Status2", "Cancelled");
    //            cmd.ExecuteNonQuery();
    //            Response.Redirect("Orders.aspx");
    //        }
    //    }
    //}

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"UPDATE Orders SET Status=@Status
                            WHERE OrderNo=@OrderNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", "Cancelled");
                cmd.Parameters.AddWithValue("OrderNo", ltOrderNo.Text);
                cmd.ExecuteNonQuery();
                Response.Redirect("Orders.aspx");
            }
        }
    }
}