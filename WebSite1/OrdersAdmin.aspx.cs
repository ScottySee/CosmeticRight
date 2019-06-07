using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OrdersAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetOrders();
        }
    }

    void GetOrders()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT DISTINCT o.OrderNo, o.DateOrdered, o.PaymentMethod, 
                                u.Lastname + ', ' + u.Firstname AS CustomerName,
                                (SELECT SUM(Amount) FROM OrderDetails WHERE OrderNo= o.OrderNo) AS TotalAmount,
                                o.Status FROM Orders o
                                INNER JOIN OrderDetails od ON o.OrderNo= od.OrderNo
                                INNER JOIN Users u ON od.UserID = u.UserID
                                ORDER BY o.DateOrdered DESC";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Orders");
                    lvOrders.DataSource = ds;
                    lvOrders.DataBind();
                }
            }
        }
    }

    void GetOrders(DateTime start, DateTime end)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT DISTINCT o.OrderNo, o.DateOrdered, o.PaymentMethod,
                                 u.LastName + ', ' + u.FirstName AS CustomerName,
                                  (SELECT SUM(Amount) FROM OrderDetails
                                    WHERE OrderNo= o.OrderNo) AS TotalAmount,
                                    o.Status FROM Orders o
                                    INNER JOIN OrderDetails od ON o.OrderNo= od.OrderNo
                                    INNER JOIN Users u ON od.UserID = u.UserID
                                        WHERE o.DateOrdered BETWEEN @start AND @end
                                        ORDER BY o.DateOrdered DESC";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end.AddHours(23).AddMinutes(59).AddSeconds(59));

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Orders");
                    lvOrders.DataSource = ds;
                    lvOrders.DataBind();
                }
            }
        }
    }


    protected void SearchByDate(object sender, EventArgs e)
    {
        DateTime start = DateTime.Now;
        DateTime end = DateTime.Now;

        bool validStart = DateTime.TryParse(txtStart.Text, out start);
        bool validEnd = DateTime.TryParse(txtEnd.Text, out end);

        if (validStart && validEnd)
        {
            // search records by date range
            GetOrders(start, end);
        }
        else
        {
            // use default
            GetOrders();
        }
    }
}