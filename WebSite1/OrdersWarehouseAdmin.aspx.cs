using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OrdersWarehouseAdmin : System.Web.UI.Page
{
    int editID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnEdit.Visible = false;
            //FOR VIEWING THE EDIT INTERFACE
            //EDITVIEW START
            if (Request.QueryString["EditID"] != null)
            {

                bool validOrders = int.TryParse(Request.QueryString["EditID"].ToString(), out editID);

                if (validOrders)
                {
                    btnEdit.Visible = true;
                    EditOrders(editID);
                }
                else
                    Response.Redirect("OrdersWarehouseAdmin.aspx");
            }
            //EDITVIEW END
            //DELETE ID CHECKING START
            //else
            //{
            //    //int DeleteID = 0;
            //    //bool validOrders = int.TryParse(Request.QueryString["DeleteID"].ToString(), out DeleteID);

            //    //if (validOrders)
            //    //{
            //    //    DeleteRecord(DeleteID);
            //    //}
            //    //else
            //        Response.Redirect("OrdersWarehouseAdmin.aspx");
            //}
            //DELETE ID CHECKING END
            GetOrders();
        }
        //FOR VIEWING
        GetOrders();
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
                                 (SELECT SUM(Amount) FROM OrderDetails WHERE OrderNo= o.OrderNo) AS TotalAmount,
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

    protected void EditOrders(int ID)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT * FROM Orders WHERE OrderNo=@OrderNo";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo", ID);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            orderno.Text = data["OrderNo"].ToString();
                            dateordered.Text = data["DateOrdered"].ToString();
                            payment.Text = data["PaymentMethod"].ToString();
                            ddlStatus.SelectedValue = data["Status"].ToString();
                        }
                    }
                    else
                    {
                        Response.Redirect("OrdersWarehouseAdmin.aspx");
                    }
                }
            }
        }
    }
    protected void SaveOrders(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"UPDATE Orders SET Status=@Status WHERE OrderNo=@OrderNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", ddlStatus.Text);
                cmd.Parameters.AddWithValue("@OrderNo", orderno.Text);
                //cmd.Parameters.AddWithValue("@Image", "Sample.jpg");

                cmd.ExecuteNonQuery();

                //start of Auditlog 
                Util.Log(Session["UserID"].ToString(), "The warehouse admin has updated the order status");
                //end of auditlog

                Response.Redirect("OrdersWarehouseAdmin.aspx");
            }
        }
    }

    //void DeleteRecord(int ID)
    //{
    //    using (SqlConnection con = new SqlConnection(Util.GetConnection()))
    //    {
    //        con.Open();
    //        string query = @"DELETE FROM Orders WHERE OrderNo=@OrderNo";

    //        using (SqlCommand cmd = new SqlCommand(query, con))
    //        {
    //            cmd.Parameters.AddWithValue("@OrderNo", ID);
    //            cmd.ExecuteNonQuery();
    //            Response.Redirect("OrdersWarehouseAdmin.aspx");
    //        }
    //    }
    //}

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("OrdersWarehouseAdmin.aspx");
    }

    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    dpAnnouncements.SetPageProperties(0, dpAnnouncements.MaximumRows, false);

    //    if (txtKeyword.Text == "")
    //        GetAnnouncement();
    //    else
    //        GetAnnouncement(txtKeyword.Text);
    //}
}