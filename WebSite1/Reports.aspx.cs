using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetInventoryReport();
            GetSalesReport();
        }
    }

    void GetInventoryReport()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT i.LogID , u.Lastname + ' ' + u.Firstname AS CustomerName, p.Product,
                               i.Quantity, i.LogTime, i.Activity FROM InventoryLog i
                               INNER JOIN Users u ON i.UserID = u.UserID
                               INNER JOIN Products p ON p.ProductID = i.ProductID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "InventoryLog");
                    lvInventory.DataSource = ds;
                    lvInventory.DataBind();
                }
            }
        }
    }

    void GetSalesReport()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"Select DISTINCT o.OrderNo, (SELECT u.Lastname + ' ' + u.Firstname AS Customer FROM Users u WHERE od.UserID=u.UserID) as Username, (SELECT SUM(Amount) FROM OrderDetails WHERE OrderNo = o.OrderNo) AS Total,
                    o.DateOrdered From Orders o, OrderDetails od WHERE o.Status='Done'";

            //@"Select o.OrderNo, u.Lastname + ' ' + u.Firstname AS Customer, (SELECT SUM(Amount) FROM OrderDetails WHERE OrderNo = o.OrderNo) AS Total,
            //        o.DateOrdered From Orders o
            //        INNER JOIN Users u ON s.UserID = u.UserID";

            //@"Select(SELECT SUM(Amount) FROM OrderDetails WHERE OrderNo = o.OrderNo) AS Total From Orders o Where o.Status = 'Done'";
            //string query1 = @"SELECT DISTINCT o.OrderNo,
            //                    u.Lastname + ', ' + u.Firstname AS CustomerName,
            //                    (SELECT SUM(Amount) FROM OrderDetails WHERE OrderNo= o.OrderNo) AS TotalAmount,
            //                    o.Status FROM Orders o
            //                    INNER JOIN OrderDetails od ON o.OrderNo= od.OrderNo
            //                    INNER JOIN Users u ON od.UserID = u.UserID
            //                    ORDER BY o.DateOrdered DESC";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "SalesLog");
                    lvSales.DataSource = ds;
                    lvSales.DataBind();
                }
            }
        }
    }
}