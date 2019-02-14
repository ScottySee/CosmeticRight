using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewAnnouncementDetail2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] == null)
        {
            Response.Redirect("ViewAnnouncement1.aspx");
        }
        else
        {
            int announcementID = 0;
            bool validAnnouncement = int.TryParse(Request.QueryString["ID"].ToString(), out announcementID);
            if (validAnnouncement)
            {
                if (!IsPostBack)
                {
                    GetData(announcementID);
                }
            }
            else
            {
                Response.Redirect("ViewAnnouncement1.aspx");
            }
        }
    }

    void GetData(int ID)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT AnnouncementID, Image, AnnouncementName,
                                AnnouncementDetail FROM Announcements";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AnnouncementID", ID);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ltName.Text = dr["AnnouncementName"].ToString();
                            imgAnnouncement.ImageUrl = "~/Images/Announcement/" + dr["Image"].ToString();
                            ltDesc.Text = dr["AnnouncementDetail"].ToString();
                        }
                    }
                    else
                    {
                        Response.Redirect("ViewAnnouncement1.aspx");
                    }
                }
            }
        }
    }
}