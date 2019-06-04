using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCart();
            GetOrderSummary();
        }

    }

    void GetCart()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT od.RefNo, p.ProductID, p.Image, p.Product, p.CatID, c.Category,
                                p.Price, od.Quantity, od.Amount FROM OrderDetails od
                                INNER JOIN Products p ON od.ProductID = p.ProductID
								INNER JOIN Categories c ON c.CatID = p.CatID
                                WHERE od.OrderNo IS NULL AND od.UserID=@UserID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo", 0);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                //use Session["userid"].ToString() instead of 1

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    lvCart.DataSource = dr;
                    lvCart.DataBind();
                }
            }
        }
    }

    void GetOrderSummary()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT SUM(Amount) FROM OrderDetails
                                WHERE OrderNo IS NULL AND UserID=@UserID
                                   HAVING COUNT(RefNo) > 0";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                //cmd.Parameters.AddWithValue("@OrderNo", 0);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                // use Session ["userid"].ToString() instead of 1
                double totalAmount = cmd.ExecuteScalar() == null ? 0 :
                    Convert.ToDouble((decimal)cmd.ExecuteScalar());

                ltGross.Text = (totalAmount * .88).ToString("#,##0.00");
                ltVAT.Text = (totalAmount * .12).ToString("#,##0.00");
                //ltDelivery.Text = (totalAmount * .1).ToString("#,##0.00");
                ltTotal.Text = (totalAmount * 1).ToString("#,##0.00");
                Session["total"] = (totalAmount * 1);
            }
        }
    }

    public bool QuantityIsSufficient(string quantity)
    {
        bool existing = true;
        SqlConnection con = new SqlConnection(Util.GetConnection());
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = @"SELECT Quantity FROM Inventory WHERE Quantity >= @Quantity";
        cmd.Parameters.AddWithValue("@Quantity", quantity);
        //cmd.Parameters.AddWithValue("@ProductID", Session["ProductID"].ToString());
        existing = cmd.ExecuteScalar() == null ? false : true;
        con.Close();
        con.Dispose();
        return existing;
    }

    //    (Select Sum(Quantity) from ProductInventory where ProductID = pi.ProductID and Quantity > p.Criticallevel AND DateExpired > GETDATE() AND Status = 'Active')

    //using (SqlConnection con = new SqlConnection(Util.GetConnection()))
    //    {
    //        con.Open();
    //        string query = @"SELECT Quantity FROM ProductInventory pi WHERE Quantity >= @Quantity AND ProductID=@ProductID";
    //        using (SqlCommand cmd = new SqlCommand(query, con))
    //        {
    //            cmd.Parameters.AddWithValue("@Quantity", quantity);
    //            cmd.Parameters.AddWithValue("@ProductID", ID);
    //            cmd.ExecuteNonQuery();
    //            return cmd.ExecuteScalar() == null ? false : true;
    //        }
    //    }
    //}

    protected void lvCart_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        Literal ltRefNo = (Literal)e.Item.FindControl("ltRefNo");
        Literal ltProductID = (Literal)e.Item.FindControl("ltProductID");
        TextBox txtQty = e.Item.FindControl("txtQty") as TextBox;
        double price = Util.GetPrice(ltProductID.Text);

        if (e.CommandName == "deleteitem")
        {
            using (SqlConnection con = new SqlConnection(Util.GetConnection()))
            {
                con.Open();
                string query = @"DELETE FROM OrderDetails WHERE RefNo=@RefNo";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@RefNo", ltRefNo.Text);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        else if (e.CommandName == "updateqty")
        {
            bool QuantitySufficient = QuantityIsSufficient(txtQty.Text);

            if (!QuantitySufficient)
            {
                message.InnerText = "Quantity is Insufficient.";
            }
            else
            {
                using (SqlConnection con = new SqlConnection(Util.GetConnection()))
                {
                    con.Open();
                    string query = @"UPDATE OrderDetails SET Quantity=@Quantity,
                            Amount=@Amount WHERE RefNo=@RefNo";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Quantity", txtQty.Text);
                        cmd.Parameters.AddWithValue("Amount",
                            int.Parse(txtQty.Text) * price);
                        cmd.Parameters.AddWithValue("RefNo", ltRefNo.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                message.InnerText = "";
                CriticalMessage.Visible = false;
            }
        }
        GetCart();
        GetOrderSummary();
    }

    protected void btnCheckout_Click(object sender, EventArgs e)
    {
        Response.Redirect("CheckoutCOD.aspx");
    }
}