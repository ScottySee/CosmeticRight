using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CheckoutCOD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCart();
            GetOrderSummary();
            GetUserInfo();
        }
    }

    void GetCart()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT od.RefNo, p.ProductID, p.Image, p.Name, c.Category,
                p.Price, od.Quantity, od.Amount FROM OrderDetails od
                INNER JOIN Products p ON od.ProductID = p.ProductID 
                INNER JOIN Categories c ON p.CatID = c.CatID
                WHERE od.OrderNo=@OrderNo AND od.UserID=@UserID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo", 0);
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
            }

        }
    }

    void GetOrderSummary()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT SUM(Amount) FROM OrderDetails
                WHERE OrderNo=@OrderNo AND UserID=@UserID
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
            string query = @"SELECT FirstName, LastName, BuildingNo, Street, Municipality, City, Landline, Mobile FROM Users WHERE UserID=@UserID";
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
                        txtCity.Text = dr["City"].ToString();
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
            City=@City, Landline=@Landline, Mobile=@Mobile, DateModified=@DateModified WHERE UserID=@UserID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FirstName", txtFN.Text);
                cmd.Parameters.AddWithValue("@LastName", txtLN.Text);
                cmd.Parameters.AddWithValue("@BuildingNo", txtbuilding.Text);
                cmd.Parameters.AddWithValue("@Street", txtStreet.Text);
                cmd.Parameters.AddWithValue("@Municipality", txtMunicipality.Text);
                cmd.Parameters.AddWithValue("@City", txtCity.Text);
                cmd.Parameters.AddWithValue("@Landline", txtPhone.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
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
            string query = @"INSERT INTO Orders VALUES (@DateOrdered,
            @PaymentMethod, @Status);
            SELECT TOP 1 OrderNo FROM Orders ORDER BY OrderNo DESC;";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@DateOrdered", DateTime.Now);
                cmd.Parameters.AddWithValue("@PaymentMethod", "Cash on Delivery");
                cmd.Parameters.AddWithValue("@Status", "Pending");
                orderNo = (int)cmd.ExecuteScalar();

            }

        }

        #endregion

        #region Step #3: Update Cart Items

        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"UPDATE OrderDetails SET OrderNo=@OrderNo,
            Status=@Status WHERE OrderNo=0 AND UserID=@UserID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo ", orderNo);
                cmd.Parameters.AddWithValue("@Status ", "Pending");
                cmd.Parameters.AddWithValue("@UserID ", Session["UserID"].ToString());
                // use Session["userid"].ToString() instead of 1
                cmd.ExecuteNonQuery();

            }
        }
        #endregion

        #region Step #4: Insert Delivery Record 
        //using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        //{
        //    con.Open();
        //    string query = @"INSERT INTO Deliveries VALUES (@OrderNo, @Deadline,
        //    @DateDelivered, @Status)";
        //    using (SqlCommand cmd = new SqlCommand(query, con))
        //    {
        //        cmd.Parameters.AddWithValue("@OrderNo ", orderNo);
        //        cmd.Parameters.AddWithValue("@Deadline ", DateTime.Now.AddDays(7));
        //        cmd.Parameters.AddWithValue("@DateDelivered ", DBNull.Value);
        //        cmd.Parameters.AddWithValue("@Status ", "Pending");
        //        cmd.ExecuteNonQuery();
        //    }

        //}

        #endregion

        Response.Redirect("Orders.aspx");
    }
}