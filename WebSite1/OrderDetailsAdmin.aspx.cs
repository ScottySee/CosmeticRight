using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OrderDetailsAdmin : System.Web.UI.Page
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

                }
            }
            else
                Response.Redirect("Orders.aspx");
        }
        else
            Response.Redirect("Orders.aspx");
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
            string query = @"SELECT TOP 1 u.FirstName, u.LastName, u.BuildingNo,
                                u.Street, u.Municipality, u.City, u.Landline,  
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
                        txtCity.Text = data["City"].ToString();
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
            string query = @"SELECT od.RefNo, p.ProductID, p.Image, p.Name, c.Category,
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
    //                        WHERE OrderNo=@OrderNo;";
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
    //                        WHERE OrderNo=@OrderNo WHERE OrderNo=@OrderNo";
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
}