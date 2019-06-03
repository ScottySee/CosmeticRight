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
                string query = @"Select count(quantity) Count from ProductInventory pi, Products p where pi.ProductID = p.ProductID and pi.Quantity < p.Criticallevel";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        if (data.HasRows)
                        {
                            while (data.Read())
                            {
                                CriticalMessage.InnerText = "There are " + data["count"] + " Products below their critical levels.";
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
