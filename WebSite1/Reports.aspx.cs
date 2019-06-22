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
            GetInventoryLogRecord();
            GetInventoryReport();
            GetSalesReport();
        }
    }

    void GetInventoryLogRecord()
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

    void GetInventoryReport()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"Select DISTINCT YEAR(LogTime) Year, Datename(Month, il.LogTime) Month, p.ProductID, p.Product, p.Price AS UnitPrice,
                                (Select Sum(Quantity) TotalAdded from InventoryLog where Activity LIKE '%added%' and ProductID = p.ProductID and (MONTH(LogTime) = MONTH(GETDATE()) and YEAR(LogTime) = Year(GETDATE())) GROUP BY ProductID) QuantityAdded,
                                (SELECT Sum(Quantity) TotalDeducted from InventoryLog where Activity LIKE '%deducted%' and ProductID = p.ProductID and (MONTH(LogTime) = MONTH(GETDATE()) and YEAR(LogTime) = Year(GETDATE())) GROUP BY ProductID) QuantitySold,
                                (SELECT Sum(Quantity) TotalReturned from InventoryLog where Activity LIKE '%cancel%' and ProductID = p.ProductID and (MONTH(LogTime) = MONTH(GETDATE()) and YEAR(LogTime) = Year(GETDATE())) GROUP BY ProductID) QuantityReturned,
                                (Select Quantity From Inventory where ProductID=p.ProductID) RemainingQuantity
                                from Products p, InventoryLog il where (MONTH(LogTime) = MONTH(GETDATE()) and YEAR(LogTime) = Year(GETDATE()))";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "InventoryLog");
                    lvInventoryReport.DataSource = ds;
                    lvInventoryReport.DataBind();
                }
            }
        }
    }

    void GetSalesReport()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT SUM(total) Total, DateOrdered  FROM (SELECT (SELECT Amount FROM OrderDetails WHERE OrderNo = o.OrderNo) Total, DATENAME(Month, DateOrdered) DateOrdered FROM Orders o WHERE o.Status='Done') SalesPerMonth GROUP BY DateOrdered";

            //string query = @"SELECT SUM(total) Total, DateOrdered  FROM (SELECT (SELECT Amount FROM OrderDetails WHERE OrderNo = o.OrderNo) Total FROM Orders o WHERE o.Status='Done') OrderDetails";

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