using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (password.Text == cpassword.Text)
        {
            using (SqlConnection con = new SqlConnection(Util.GetConnection()))
            {
                con.Open();

                string query = @"UPDATE Users SET Password=@Password WHERE UserID=@UserID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Password", Util.CreateSHAHash(password.Text));
                    cmd.Parameters.AddWithValue("@UserID", Request.QueryString["UserID"]);
                    //cmd.Parameters.AddWithValue("@Image", "Sample.jpg");
                    cmd.ExecuteNonQuery();

                    //start of Auditlog 
                    Util.Log(Session["UserID"].ToString(), "The user has changed password");
                    //end of auditlog

                    Response.Redirect("Login.aspx");
                }
            }
        }

    }
}