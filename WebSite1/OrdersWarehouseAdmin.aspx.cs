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
            string query = @"SELECT * FROM Orders WHERE Status != 'Archived'";

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

    //protected void AddAnnouncement(object sender, EventArgs e)
    //{
    //    //string fileName = Path.GetFileName(fileUpload1.FileName);
    //    //fileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/Announcement/") + fileName);
    //    using (SqlConnection con = new SqlConnection(Util.GetConnection()))
    //    {
    //        con.Open();
    //        string query = @"INSERT INTO Announcements VALUES (@AnnouncementName, @AnnouncementDetail, @Image, @AdminID, @Status, @DateAdded, @DateModified)";

    //        using (SqlCommand cmd = new SqlCommand(query, con))
    //        {
    //            cmd.Parameters.AddWithValue("@AnnouncementName", txtannouncement.Text);
    //            cmd.Parameters.AddWithValue("@AnnouncementDetail", txtdetails.Text);
    //            cmd.Parameters.AddWithValue("@Image", fileUpload1.FileName);
    //            cmd.Parameters.AddWithValue("@AdminID", "1");
    //            cmd.Parameters.AddWithValue("@Status", "Active");
    //            cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
    //            cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
    //            cmd.ExecuteNonQuery();

    //            Response.Redirect("OrdersWarehouseAdmin.aspx");
    //        }
    //    }
    //}

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
                Util.Log(Session["UserID"].ToString(), "The Warehouse Admin has updated the order status");
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