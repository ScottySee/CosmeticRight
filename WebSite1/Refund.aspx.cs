using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Refund : System.Web.UI.Page
{
    public static string[] quantity = new string[100];
    public static string[] ProductID = new string[100];
    public static string status;
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
            string query = @"SELECT OrderNo, Status, PaymentMethod, DateOrdered
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

                            ltPaymentMethod.Text = data["PaymentMethod"].ToString();
                            ltDateOrdered.Text = data["DateOrdered"].ToString();
                        }
                    }
                    else
                    {
                        Response.Redirect("Refund.aspx");
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
                        Response.Redirect("Orders.aspx");
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

    protected void btnrefund_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"UPDATE Orders SET RefundReason=@RefundReason, RefundDate=@RefundDate, Status=@Status
                            WHERE OrderNo=@OrderNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@RefundReason", ddlReason.SelectedValue);
                cmd.Parameters.AddWithValue("@RefundDate", DateTime.Now);

                cmd.Parameters.AddWithValue("@Status", "Refund Request Submitted, Pending for Verification");
                cmd.Parameters.AddWithValue("OrderNo", ltOrderNo.Text);
                cmd.ExecuteNonQuery();

                Response.Redirect("Orders.aspx");
            }
        }
    }
}