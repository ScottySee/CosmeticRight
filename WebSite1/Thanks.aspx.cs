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
    public static string[] quantity = new string[100];
    public static string[] ProductID = new string[100];

    protected void Page_Load(object sender, EventArgs e)
    {
        quantity = Session["Quantity"] as string[];
        ProductID = Session["ProductID"] as string[];
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
                    cmd.Parameters.AddWithValue("@Status", "Pending");
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

                //dagdag ng isa pa para sa minus ng inventory?
                int count = 0;
                foreach (var item in quantity)
                {
                    if (item != null)
                    {
                        con.Close();
                        con.Open();
                        string query3 = @"UPDATE Inventory SET Quantity = Quantity - @Quantity WHERE ProductID = @ProductID AND Inventory.Quantity > (Select Criticallevel from Products where ProductID = @ProductID)";
                        
                        using (SqlCommand cmd = new SqlCommand(query3, con))
                        {
                            cmd.Parameters.AddWithValue("@Quantity", item);
                            cmd.Parameters.AddWithValue("@ProductID", ProductID[count]);
                            cmd.ExecuteNonQuery();
                            

                            //start of Auditlog 
                            Util.InventoryRecord(ProductID[count], item, "The member has created an order, inventory deducted.");
                            //end of auditlog
                            count++;
                        }
                    }
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