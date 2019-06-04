using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            if (Session["UserType"].ToString() != "1")
            {
                Response.Redirect("Login.aspx");
            }
            using (SqlConnection con = new SqlConnection(Util.GetConnection()))
            {
                con.Open();
                string query = @"Select count(AnnouncementName) Count from Announcements a where a.DateEnd < GetDate() AND a.Status != 'Archived'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        if (data.HasRows)
                        {
                            while (data.Read())
                            {
                                Announcement.InnerText = "There are " + data["count"] + " Announcement that are expired.";
                            }
                        }
                    }
                }
            }
            using (SqlConnection con = new SqlConnection(Util.GetConnection()))
            {
                con.Open();
                string query = @"Select count(OrderNo) Count from Orders o where o.Status = 'Pending'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        if (data.HasRows)
                        {
                            while (data.Read())
                            {
                                Orders.InnerText = "There are " + data["count"] + " Orders that are in pending.";
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
