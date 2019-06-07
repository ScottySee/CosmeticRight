using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin1 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            if (Session["UserType"].ToString() != "2")
            {
                Response.Redirect("Login.aspx");
            }
            using (SqlConnection con = new SqlConnection(Util.GetConnection()))
            {
                con.Open();
                string query = @"Select count(i.quantity) Count from Inventory i, Products p, ProductInventory pi where i.ProductID = p.ProductID and i.Quantity <= p.Criticallevel and pi.Status != 'Archived'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        if (data.HasRows)
                        {
                            while (data.Read())
                            {
                                Products.InnerText = "There are " + data["count"] + " Products below their critical levels.";
                            }
                        }
                    }
                }
            }
            using (SqlConnection con = new SqlConnection(Util.GetConnection()))
            {
                con.Open();
                string query = @"Select count(OrderNo) Count from Orders o where o.Status = 'Accepted'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        if (data.HasRows)
                        {
                            while (data.Read())
                            {
                                Orders.InnerText = "There are " + data["count"] + " Orders that are accepted.";
                            }
                        }
                    }
                }
            }
        }

        else
        {
            Response.Redirect("Login.aspx");
        }
    }
}
