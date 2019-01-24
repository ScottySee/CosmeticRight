using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Announcement : System.Web.UI.Page
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
               
                bool validAnnouncement = int.TryParse(Request.QueryString["EditID"].ToString(), out editID);

                if (validAnnouncement)
                {
                    btnAdd.Visible = false;
                    btnEdit.Visible = true;
                    EditAnnouncement(editID);
                }
                else
                    Response.Redirect("Announcement.aspx");
            }
            //EDITVIEW END
            //DELETE ID CHECKING START
            else if (Request.QueryString["DeleteID"] != null)
            {
                int DeleteID = 0;
                bool validBooking = int.TryParse(Request.QueryString["DeleteID"].ToString(), out DeleteID);

                if (validBooking)
                {
                    DeleteRecord(DeleteID);
                }
                else
                    Response.Redirect("Announcement.aspx");
            }
            //DELETE ID CHECKING END

            
            GetAnnouncement();
        }
        //FOR VIEWING
        GetAnnouncement();
    }


    void GetAnnouncement()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT * FROM Announcements";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Announcements");
                    lvAnnouncements.DataSource = ds;
                    lvAnnouncements.DataBind();
                }
            }
        }
    }
    protected void AddAnnouncement(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"INSERT INTO Announcements VALUES (@AnnouncementName, @AnnouncementDetail, @Image, @AdminID, @Status)";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AnnouncementName", txtannouncement.Text);
                cmd.Parameters.AddWithValue("@AnnouncementDetail", txtdetails.Text);
                cmd.Parameters.AddWithValue("@Image", "Sample.jpg");
                cmd.Parameters.AddWithValue("@AdminID", "1");
                cmd.Parameters.AddWithValue("@Status", "Active");
                cmd.ExecuteNonQuery();

                Response.Redirect("Announcement.aspx");
            }
        }
    }
    protected void EditAnnouncement(int ID)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT * FROM Announcements WHERE AnnouncementID=@AnnouncementID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AnnouncementID", ID);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            announcementID.Text = data["AnnouncementID"].ToString();
                            txtannouncement.Text = data["AnnouncementName"].ToString();
                            txtdetails.Text = data["AnnouncementDetail"].ToString();
                        }
                    }
                    else
                    {
                        Response.Redirect("Announcement.aspx");
                    }
                }
            }
        }
    }
    protected void SaveAnnouncement(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"UPDATE Announcements SET AnnouncementName=@AnnouncementName, AnnouncementDetail=@AnnouncementDetail WHERE AnnouncementID=@AnnouncementID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AnnouncementName", txtannouncement.Text);
                cmd.Parameters.AddWithValue("@AnnouncementDetail", txtdetails.Text);
                cmd.Parameters.AddWithValue("@AnnouncementID", announcementID.Text);
                //cmd.Parameters.AddWithValue("@Image", "Sample.jpg");

                cmd.ExecuteNonQuery();

                Response.Redirect("Announcement.aspx");
            }
        }
    }
    void DeleteRecord(int ID)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"DELETE FROM Announcements WHERE AnnouncementID=@AnnouncementID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AnnouncementID", ID);
                cmd.ExecuteNonQuery();
                Response.Redirect("Announcement.aspx");
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Announcement.aspx");
    }

    //protected void lvRates_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    //{
    //    GetAnnouncement();
    //}

}