using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Thanks : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(Util.GetConnection());
    SqlCommand cmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Code"] != null & Request.QueryString["code"] != null)
        {
            if (Session["Code"].ToString() == Request.QueryString["code"].ToString())
            {
                con.Open();
                string query = @"INSERT INTO Orders VALUES (@DateOrdered,
            @PaymentMethod, @Status);
            SELECT TOP 1 OrderNo FROM Orders ORDER BY OrderNo DESC;";
                int orderNo = 0;
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@DateOrdered", DateTime.Now);
                    cmd.Parameters.AddWithValue("@PaymentMethod", "Paypal");
                    cmd.Parameters.AddWithValue("@Status", "Paid");
                    orderNo = (int)cmd.ExecuteScalar();

                }
                con.Close();

                con.Open();
                string query2 = @"UPDATE OrderDetails SET OrderNo=@OrderNo
             WHERE OrderNo IS NULL AND UserID=@UserID";

                using (SqlCommand cmd = new SqlCommand(query2, con))
                {
                    cmd.Parameters.AddWithValue("@OrderNo", orderNo);
                    //cmd.Parameters.AddWithValue("@Status ", "Pending");
                    cmd.Parameters.AddWithValue("@UserID ", Session["UserID"].ToString());
                    // use Session["userid"].ToString() instead of 1
                    cmd.ExecuteNonQuery();

                }
            }
            else
            {
                Response.Redirect("CheckoutCOD.aspx");
            }
        }
        else
        {

        }


    }
}