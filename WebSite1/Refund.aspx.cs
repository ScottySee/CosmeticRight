using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Refund : System.Web.UI.Page
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
                    GetOrderInfo(orderNo);
                }
            }
            else
                Response.Redirect("Refund.aspx");
        }
        else
            Response.Redirect("Refund.aspx");
    }

    void GetOrderInfo(int ID)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT OrderNo, Status, PaymentMethod
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"UPDATE Orders SET RefundReason=@RefundReason, RefundDate=@RefundDate, Status=@Status
                            WHERE OrderNo=@OrderNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@RefundReason", ddlReason1.SelectedValue);
                cmd.Parameters.AddWithValue("@RefundDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@Status", "Cancelled, Pending for Approval");
                cmd.Parameters.AddWithValue("OrderNo", ltOrderNo.Text);
                cmd.ExecuteNonQuery();

                Response.Redirect("Orders.aspx");
            }
        }
    }
}