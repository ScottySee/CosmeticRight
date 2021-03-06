﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OrderDetailsAdmin : System.Web.UI.Page
{
    public static string[] quantity = new string[100];
    public static string[] ProductID = new string[100];
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
                }
            }
            else
                Response.Redirect("OrdersAdmin.aspx");
        }
        else
            Response.Redirect("OrdersAdmin.aspx");
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
            string query = @"SELECT OrderNo, DateOrdered, PaymentMethod, Status, CancelReason, CancelDate, RefundReason, RefundDate
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
                            ltReason.Text = data["CancelReason"].ToString();
                            ltDate.Text = data["CancelDate"].ToString();
                            ltRefund.Text = data["RefundReason"].ToString();
                            ltRefundDate.Text = data["RefundDate"].ToString();

                            //for accepting order
                            btnAccept.Visible = ltStatus.Text == "Pending" ? true : false;
                            btnReject.Visible = ltStatus.Text == "Pending" ? true : false;

                            ltDateOrdered.Text = data["DateOrdered"].ToString();
                            ltPaymentMethod.Text = data["PaymentMethod"].ToString();



                            btnApprove.Visible = ltStatus.Text == "Cancelled, Pending for Approval" ? true : false;
                            btnDisApprove.Visible = ltStatus.Text == "Cancelled, Pending for Approval" ? true : false;
                            //for cancelling order
                            //if (!(ltStatus.Text == "Cancelled, Pending for Approval"))
                            //{
                            //    ltReason.Visible = false;
                            //    ltDate.Visible = false;
                            //    ltRefund.Visible = false;
                            //    ltRefundDate.Visible = false;
                            //}

                            //btnApprove.Visible = ltStatus.Text == "Cancelled, Pending for Approval" ? true : false;
                            //btnDisApprove.Visible = ltStatus.Text == "Cancelled, Pending for Approval" ? true : false;
                            if (ltStatus.Text == "Pending")
                            {
                                btnApprove.Visible = false;
                                btnDisApprove.Visible = false;
                            }

                            //btnPayNow.Visible = ltPaymentMethod.Text == "Paypal" ? true : false;
                            //btnRefund.Visible = ltPaymentMethod.Text == "Cash on Delivery" ? true : false;

                            //if(!(ltStatus.Text == "Done"))
                            //{
                            //    btnRefund.Visible = false;
                            //}

                            btnVerify.Visible = ltStatus.Text == "Refund Request Submitted, Pending for Verification" ? true : false;

                            //if (!(ltStatus.Text == "Refund in Progress, Pending for Approval"))
                            //{
                            //    ltReason.Visible = false;
                            //    ltDate.Visible = false;
                            //    btnApprove.Visible = false;
                            //    btnDisApprove.Visible = false;
                            //}
                        }
                    }
                    else
                    {
                        Response.Redirect("OrdersAdmin.aspx");
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
            string query = @"SELECT TOP 1 u.FirstName, u.LastName, u.BuildingNo,
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
                        txtFN.Text = data["FirstName"].ToString();
                        txtLN.Text = data["LastName"].ToString();
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

                // di ako sure kung tama

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        int count = 0;
                        while (dr.Read())
                        {
                            quantity[count] = dr["Quantity"].ToString();
                            ProductID[count] = dr["ProductID"].ToString();
                            count++;
                        }
                        Session["Quantity"] = quantity;
                        Session["ProductID"] = ProductID;

                    }
                    else
                        Response.Redirect("OrdersAdmin.aspx");
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

    protected void btnAccept_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"UPDATE Orders SET Status=@Status
                WHERE OrderNo=@OrderNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("Status", "Accepted");
                cmd.Parameters.AddWithValue("OrderNo", ltOrderNo.Text);
                cmd.ExecuteNonQuery();

            }
        }

        // for updating inventory
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            int count = 0;
            foreach (var item in quantity)
            {
                if (item != null)
                {
                    con.Close();
                    con.Open();
                    string query = @"UPDATE Inventory SET Quantity = Quantity - @Quantity WHERE ProductID = @ProductID AND Inventory.Quantity > (Select Criticallevel from Products where ProductID = @ProductID)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Quantity", item);
                        cmd.Parameters.AddWithValue("@ProductID", ProductID[count]);
                        cmd.ExecuteNonQuery();

                        //start of Auditlog 
                        Util.InventoryRecord(ProductID[count], item, "The office admin has accepted an order, inventory deducted");
                        //end of auditlog
                        count++;
                    }
                }
            }
        }
        Response.Redirect("OrderDetailsAdmin.aspx");
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        // for updating order status
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"UPDATE Orders SET Status=@Status
                WHERE OrderNo=@OrderNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("Status", "Rejected");
                cmd.Parameters.AddWithValue("OrderNo", ltOrderNo.Text);
                cmd.ExecuteNonQuery();
                Response.Redirect("OrderDetailsAdmin.aspx");
            }
        }

        //// for updating inventory
        //using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        //{
        //    int count = 0;
        //    foreach (var item in quantity)
        //    {
        //        if (item != null)
        //        {
        //            con.Close();
        //            con.Open();
        //            string query = @"UPDATE Inventory SET Quantity = Quantity + @Quantity WHERE ProductID = @ProductID AND Inventory.Quantity > (Select Criticallevel from Products where ProductID = @ProductID)";
        //            using (SqlCommand cmd = new SqlCommand(query, con))
        //            {
        //                cmd.Parameters.AddWithValue("@Quantity", item);
        //                cmd.Parameters.AddWithValue("@ProductID", ProductID[count]);
        //                cmd.ExecuteNonQuery();

        //                //start of Auditlog 
        //                Util.InventoryRecord(ProductID[count], item, "The office admin has rejected an order, inventory added");
        //                //end of auditlog
        //                count++;
        //            }
        //        }
        //    }
        //}
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (ltPaymentMethod.Text == "Cash on Delivery")
        {
            using (SqlConnection con = new SqlConnection(Util.GetConnection()))
            {
                con.Open();
                string query = @"UPDATE Orders SET Status=@Status
                WHERE OrderNo=@OrderNo";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Status", "Cancelled Approved");
                    cmd.Parameters.AddWithValue("@OrderNo", ltOrderNo.Text);
                    cmd.ExecuteNonQuery();
                }
            }

            // for updating inventory
            using (SqlConnection con = new SqlConnection(Util.GetConnection()))
            {
                int count = 0;
                foreach (var item in quantity)
                {
                    if (item != null)
                    {
                        con.Close();
                        con.Open();
                        string query = @"UPDATE Inventory SET Quantity = Quantity + @Quantity WHERE ProductID = @ProductID AND Inventory.Quantity > (Select Criticallevel from Products where ProductID = @ProductID)";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@Quantity", item);
                            cmd.Parameters.AddWithValue("@ProductID", ProductID[count]);
                            cmd.ExecuteNonQuery();

                            //start of Auditlog 
                            Util.InventoryRecord(ProductID[count], item, "The member cancels order, inventory returned.");
                            //end of auditlog
                            count++;
                        }
                    }
                }
            }
        }
        else
        {
            using (SqlConnection con = new SqlConnection(Util.GetConnection()))
            {
                con.Open();
                string query = @"UPDATE Orders SET Status=@Status
            WHERE OrderNo=@OrderNo";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Status", "Cancelled Approved, Refund in Process");
                    cmd.Parameters.AddWithValue("@OrderNo", ltOrderNo.Text);
                    cmd.ExecuteNonQuery();


                }
            }

            // for updating inventory
            using (SqlConnection con = new SqlConnection(Util.GetConnection()))
            {
                int count = 0;
                foreach (var item in quantity)
                {
                    if (item != null)
                    {
                        con.Close();
                        con.Open();
                        string query = @"UPDATE Inventory SET Quantity = Quantity + @Quantity WHERE ProductID = @ProductID AND Inventory.Quantity > (Select Criticallevel from Products where ProductID = @ProductID)";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@Quantity", item);
                            cmd.Parameters.AddWithValue("@ProductID", ProductID[count]);
                            cmd.ExecuteNonQuery();

                            //start of Auditlog 
                            Util.InventoryRecord(ProductID[count], item, "The member cancels order, inventory returned.");
                            //end of auditlog
                            count++;
                        }
                    }
                }
            }
        }
    }



    protected void btnDisApprove_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"UPDATE Orders SET Status=@Status
                WHERE OrderNo=@OrderNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", "Cancelled Disapproved");
                cmd.Parameters.AddWithValue("@OrderNo", ltOrderNo.Text);
                cmd.ExecuteNonQuery();
            }
        }
    }


    protected void btnVerify_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"UPDATE Orders SET Status=@Status
                WHERE OrderNo=@OrderNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", "Refund Request Received, Verified. Check your email");
                cmd.Parameters.AddWithValue("@OrderNo", ltOrderNo.Text);
                cmd.ExecuteNonQuery();
            }
        }
    }
}