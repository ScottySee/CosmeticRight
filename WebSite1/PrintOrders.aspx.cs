using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PrintOrders : System.Web.UI.Page
{
    public string Date;
    public string Customer;
    public string Address;
    public string Contact;
    public double TotalAmount;

    SqlCommand cmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null)
        {
            int ID = 0;
            bool check = int.TryParse(Request.QueryString["ID"], out ID);
            if (check != false)
            {
                GetOrderDetails(ID);
            }

        }
        else
        {
           
            Response.Redirect("OrderDetailsAdmin.aspx");
        }

    }

    void GetOrderDetails(int ID)
    {

        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            string query = @"SELECT od.RefNo, p.ProductID, p.Image, p.Product,
                                p.Price, od.Quantity, od.Amount, FORMAT (o.DateOrdered, 'MMM dd yyyy') as DateOrdered, 
								CONCAT(u.Firstname, ' ' ,u.Lastname) as Customer,
								CONCAT(u.BuildingNo, ' ', u.Street, ' ', u.Municipality, ' ', u.City) as Address,
								CONCAT(u.Landline, '/', u.Mobile) as Contact
								FROM OrderDetails od
                                INNER JOIN Products p ON od.ProductID = p.ProductID 
								inner join Orders o ON o.OrderNo = od.OrderNo
								inner join Users u on u.UserID = od.UserID 
                                WHERE od.OrderNo = @OrderNo";

            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@OrderNo", ID);
            con.Open();

            using (SqlDataReader data = cmd.ExecuteReader())
            {
                while (data.Read())
                {   
                    Date = data["DateOrdered"].ToString();
                    Customer = data["Customer"].ToString();
                    Address = data["Address"].ToString();
                    Contact = data["Contact"].ToString();
                    TotalAmount = TotalAmount + Convert.ToDouble(data["Amount"].ToString());


                }
                
            }
            using (SqlDataReader data = cmd.ExecuteReader())
            {                
                lvPrintOrders.DataSource = data;
                lvPrintOrders.DataBind();
            }

            con.Close();

        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("OrderDetailsAdmin.aspx");
    }
}