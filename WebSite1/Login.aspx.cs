using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            // eto yung para madistinguish kung anong type ang user
            if (Session["UserType"].ToString() == "1") //OFFICE ADMIN
            {
                Response.Redirect("OfficeAdminHome.aspx");
            }
            else if (Session["UserType"].ToString() == "2") //WAREHOUSE ADMIN
            {
                Response.Redirect("WarehouseAdminHome.aspx");
            }
            else if (Session["UserType"].ToString() == "3") // USER/MEMBER
            {
                Response.Redirect("Member.aspx");
            }
        }
        
    }

    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT * FROM Users
                                WHERE Email=@Email AND Password=@Password";
            string query1 = @"INSERT INTO WebsiteVisit VALUES (@Date)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Email", Server.HtmlEncode(email.Text.Trim()));
                cmd.Parameters.AddWithValue("@Password", Util.CreateSHAHash(password.Text));
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows) //credentials are correct
                    {
                        while (data.Read())
                        {
                            Session["UserID"] = data["UserID"].ToString();
                            Session["UserType"] = data["UserType"].ToString();

                            // eto yung para madistinguish kung anong type ang user
                            if (data["UserType"].ToString() == "1") //OFFICE ADMIN
                            {
                                Util.Log(Session["UserID"].ToString(), "The office admin has logged in");
                                Response.Redirect("OfficeAdminHome.aspx");
                            }
                            else if (data["UserType"].ToString() == "2") //WAREHOUSE ADMIN
                            {
                                Util.Log(Session["UserID"].ToString(), "The warehouse admin has logged in");
                                Response.Redirect("WarehouseAdminHome.aspx");
                            }
                            else if (data["UserType"].ToString() == "3") // USER/MEMBER
                            {
                                Util.Log(Session["UserID"].ToString(), "The user has logged in");
                                con.Close();
                                con.Open();
                                using (SqlCommand cmd1 = new SqlCommand(query1, con))
                                {
                                    cmd1.Parameters.AddWithValue("@Date", DateTime.Now);
                                    cmd1.ExecuteNonQuery();
                                }
                                Response.Redirect("Member.aspx");
                            }
                        }

                        //start of Auditlog 
                        Util.Log(Session["UserID"].ToString(), "The user has logged in");
                        //end of auditlog

                        Response.Redirect("Member.aspx");
                    }
                    else //did not match </3
                    {
                        error.Visible = true;
                    }
                }
            }
        }
    }
}