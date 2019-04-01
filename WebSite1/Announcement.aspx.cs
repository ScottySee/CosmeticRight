using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Globalization;

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
                bool validAnnouncement = int.TryParse(Request.QueryString["DeleteID"].ToString(), out DeleteID);

                if (validAnnouncement)
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
        //@"Server=MSI\MSSQLSERVER2;Database=Test;Integrated Security=true";
        {
            con.Open();
            string query = @"SELECT * FROM Announcements WHERE Status != 'Archived'";

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

    //void GetAnnouncement(string keyword)
    //{
    //    using (SqlConnection con = new SqlConnection(Util.GetConnection()))
    //    {
    //        con.Open();
    //        string query = @"SELECT * FROM Announcements
    //                            WHERE AnnouncementName LIKE @keyword";

    //        using (SqlCommand cmd = new SqlCommand(query, con))
    //        {
    //            cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
    //            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
    //            {
    //                DataSet ds = new DataSet();
    //                da.Fill(ds, "Announcements");
    //                lvAnnouncements.DataSource = ds;
    //                lvAnnouncements.DataBind();
    //            }
    //        }
    //    }
    //}

    protected void AddAnnouncement(object sender, EventArgs e)
    {
        string fileName = Path.GetFileName(fileUpload1.FileName);
        fileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/Announcement/") + fileName);
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"INSERT INTO Announcements VALUES (@AnnouncementName, @AnnouncementDetail, @Image, @DateStart, @DateEnd, @UserID, @Status, @DateAdded, @DateModified)";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AnnouncementName", Server.HtmlEncode(txtannouncement.Text.Trim()));
                cmd.Parameters.AddWithValue("@AnnouncementDetail", Server.HtmlEncode(txtdetails.Text.Trim()));
                cmd.Parameters.AddWithValue("@Image", fileUpload1.FileName);
                cmd.Parameters.AddWithValue("@DateStart", Server.HtmlEncode(datestart.Text.Trim()));
                cmd.Parameters.AddWithValue("@DateEnd", Server.HtmlEncode(dateend.Text.Trim()));
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                cmd.Parameters.AddWithValue("@Status", Server.HtmlEncode("Active"));
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                cmd.ExecuteNonQuery();

                //start of Auditlog 
                Util.Log(Session["UserID"].ToString(), "The office admin has added an announcement");
                //end of auditlog

                message.InnerText = "Announcement Successfully Added.";

                //lahat ng textbox
                txtannouncement.Text = null;
                txtdetails.Text = null;
                datestart.Text = null;
                dateend.Text = null;
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
                            Session["AnnouncementID"] = data["AnnouncementID"].ToString();
                            txtannouncement.Text = data["AnnouncementName"].ToString();
                            txtdetails.Text = data["AnnouncementDetail"].ToString();
                            Session["image"] = data["Image"].ToString();
                            datestart.Text = Convert.ToDateTime(data["DateStart"]).ToString("MM/dd/yyyy");
                            dateend.Text = Convert.ToDateTime(data["DateEnd"]).ToString("MM/dd/yyyy");
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
            string query = @"UPDATE Announcements SET AnnouncementName=@AnnouncementName, Image=@Image, DateStart=@DateStart, DateEnd=@DateEnd, AnnouncementDetail=@AnnouncementDetail, DateModified=@DateModified WHERE AnnouncementID=@AnnouncementID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AnnouncementName", Server.HtmlEncode(txtannouncement.Text.Trim()));
                cmd.Parameters.AddWithValue("@AnnouncementDetail", Server.HtmlEncode(txtdetails.Text.Trim()));
                if (fileUpload1.HasFile)
                {
                    string filename = Path.GetFileName(fileUpload1.FileName);
                    cmd.Parameters.AddWithValue("@Image", filename);
                    fileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/Announcement/") + filename);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Image", Session["image"].ToString());
                    Session.Remove("image");
                }
                cmd.Parameters.AddWithValue("@DateStart", Server.HtmlEncode(datestart.Text));
                cmd.Parameters.AddWithValue("@DateEnd", Server.HtmlEncode(dateend.Text));
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                cmd.Parameters.AddWithValue("@AnnouncementID", Session["AnnouncementID"].ToString());

                cmd.ExecuteNonQuery();

                //start of Auditlog 
                Util.Log(Session["UserID"].ToString(), "The office admin has updated an announcement");
                //end of auditlog

                message.InnerText = "Announcement Successfully Updated.";

                txtannouncement.Text = null;
                txtdetails.Text = null;
                datestart.Text = null;
                dateend.Text = null;
            }
        }
    }

    void DeleteRecord(int ID)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"UPDATE Announcements SET Status = 'Archived' WHERE AnnouncementID=@AnnouncementID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AnnouncementID", ID);
                cmd.ExecuteNonQuery();

                //start of Auditlog 
                Util.Log(Session["UserID"].ToString(), "The office admin has archived an announcement");
                //end of auditlog

                Response.Redirect("Announcement.aspx");
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Announcement.aspx");
    }
}