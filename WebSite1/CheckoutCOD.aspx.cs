using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CheckoutCOD : System.Web.UI.Page
{
    public static string[] quantity = new string[100];
    public static string[] ProductID = new string[100];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCart();
            GetOrderSummary();
            GetUserInfo();
            GetCity();
        }
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

    void GetCart()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT od.RefNo, p.ProductID, p.Image, p.Product, c.Category,
                p.Price, od.Quantity, od.Amount FROM OrderDetails od
                INNER JOIN Products p ON od.ProductID = p.ProductID 
                INNER JOIN Categories c ON p.CatID = c.CatID
                WHERE od.OrderNo IS NULL AND od.UserID=@UserID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                //cmd.Parameters.AddWithValue("@OrderNo", 0);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                // use Session["userid"].ToString() instead of 1
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        lvCart.DataSource = dr;
                        lvCart.DataBind();
                    }
                    else
                        Response.Redirect("Cart.aspx");
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
                        Response.Redirect("Cart.aspx");
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
                cmd.Parameters.AddWithValue("@OrderNo", 0);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                // use Session["userid"].ToString() instead of 1
                double totalAmount = cmd.ExecuteScalar() == null ? 0 :
                    Convert.ToDouble((decimal)cmd.ExecuteScalar());

                ltGross.Text = (totalAmount * .88).ToString("#,##0.00");
                ltVAT.Text = (totalAmount * .12).ToString("#,##0.00");
                //ltDelivery.Text = (totalAmount * .1).ToString("#,##0.00");
                ltTotal.Text = (totalAmount * 1).ToString("#,##0.00");
            }
        }
    }

    void GetUserInfo()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT FirstName, LastName, BuildingNo, Street, Municipality, CityID, Landline, Mobile FROM Users WHERE UserID=@UserID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                // use Session["userid"].ToString() instead of 1
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        txtFN.Text = dr["FirstName"].ToString();
                        txtLN.Text = dr["LastName"].ToString();
                        txtbuilding.Text = dr["BuildingNo"].ToString();
                        txtStreet.Text = dr["Street"].ToString();
                        txtMunicipality.Text = dr["Municipality"].ToString();
                        ddlCity.Text = dr["CityID"].ToString();
                        txtPhone.Text = dr["Landline"].ToString();
                        txtMobile.Text = dr["Mobile"].ToString();
                    }
                }
            }
        }
    }

    protected void btnCheckout_Click(object sender, EventArgs e)
    {
        #region Step #1: Update Customer Information 

        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"UPDATE Users SET FirstName=@FirstName, LastName=@LastName, BuildingNo=@BuildingNo, Street=@Street, Municipality=@Municipality,
            CityID=@CityID, Landline=@Landline, Mobile=@Mobile, DateModified=@DateModified WHERE UserID=@UserID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FirstName", Server.HtmlEncode(txtFN.Text.Trim()));
                cmd.Parameters.AddWithValue("@LastName", Server.HtmlEncode(txtLN.Text.Trim()));
                cmd.Parameters.AddWithValue("@BuildingNo", Server.HtmlEncode(txtbuilding.Text.Trim()));
                cmd.Parameters.AddWithValue("@Street", Server.HtmlEncode(txtStreet.Text.Trim()));
                cmd.Parameters.AddWithValue("@Municipality", Server.HtmlEncode(txtMunicipality.Text.Trim()));
                cmd.Parameters.AddWithValue("@CityID", ddlCity.SelectedValue);
                cmd.Parameters.AddWithValue("@Landline", Server.HtmlEncode(txtPhone.Text.Trim()));
                cmd.Parameters.AddWithValue("@Mobile", Server.HtmlEncode(txtMobile.Text.Trim()));
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                // use Session["userid"].ToString() instead of 1
                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Step #2: Insert Order Record 

        int orderNo = 0;
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"INSERT INTO Orders (DateOrdered, PaymentMethod, Status) VALUES (@DateOrdered,
            @PaymentMethod, @Status);
            SELECT TOP 1 OrderNo FROM Orders ORDER BY OrderNo DESC;";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@DateOrdered", DateTime.Now);
                cmd.Parameters.AddWithValue("@PaymentMethod", Server.HtmlEncode("Cash on Delivery"));
                cmd.Parameters.AddWithValue("@Status", Server.HtmlEncode("Pending"));
                orderNo = (int)cmd.ExecuteScalar();
            }

        }

        #endregion

        #region Step #3: Update Cart Items
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"UPDATE OrderDetails SET OrderNo=@OrderNo
             WHERE OrderNo IS NULL AND UserID=@UserID";

            //string query2 = @"Select Sum(Amount) as Total Where OrderNo=@OrderNo From OrderDetails";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo", orderNo);
                //cmd.Parameters.AddWithValue("@Status ", "Pending");
                cmd.Parameters.AddWithValue("@UserID ", Session["UserID"].ToString());
                // use Session["userid"].ToString() instead of 1
                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Step #4: Minus Inventory Record 
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
                    //string query2 = @"INSERT INTO InventoryLog VALUES (@UserID, @ProductID, @Quantity, @LogTime, @Activity)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
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
        #endregion

        Response.Redirect("Orders.aspx");
    }
}