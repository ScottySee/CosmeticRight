using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewAnnouncement2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAnnouncement();
        }
    }

    void GetAnnouncement()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT AnnouncementID, Image, AnnouncementName,
                                AnnouncementDetail, DateStart, DateEnd FROM Announcements
                                where DateEnd > GETDATE() AND Status != 'Archived'";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Announcements");
                    lvAnnouncement.DataSource = ds;
                    lvAnnouncement.DataBind();
                }
            }
        }
    }

    void GetAnnouncement(string keyword)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT AnnouncementID, Image, AnnouncementName,
                                AnnouncementDetail, DateStart, DateEnd FROM Announcements
                                WHERE AnnouncementName LIKE @keyword And DateEnd > GETDATE() AND Status != 'Archived'";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Announcements");
                    lvAnnouncement.DataSource = ds;
                    lvAnnouncement.DataBind();
                }
            }
        }
    }

    protected void lvAnnouncements_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "addtocart")
        {
            Literal ltID = (Literal)e.Item.FindControl("ltID");
            Util.AddToCart(ltID.Text, "1");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        dpAnnouncement.SetPageProperties(0, dpAnnouncement.MaximumRows, false);

        if (txtKeyword.Text == "")
            GetAnnouncement();
        else
            GetAnnouncement(txtKeyword.Text);
    }

    protected void lvProducts_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        dpAnnouncement.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        if (txtKeyword.Text == "")
            GetAnnouncement();
        else
            GetAnnouncement(txtKeyword.Text);
    }
}