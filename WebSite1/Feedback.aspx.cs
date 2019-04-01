using System;
using System.Collections.Generic;
using System.Data.SqlClient;


public partial class Feedback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string star = "";
        if (Request.Form["star"] != null)
        {
            star = Request.Form["star"].ToString();
        }
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"INSERT INTO Feedback Values(@Datefeedback, @UserID, @Rating, @Comment)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Rating", star);
                cmd.Parameters.AddWithValue("@Comment", Message.Text);
                cmd.Parameters.AddWithValue("@Datefeedback", DateTime.Now);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                cmd.ExecuteNonQuery();

                //start of Auditlog 
                Util.Log(Session["UserID"].ToString(), "The Member has sent a feedback");
                //end of auditlog
            }
        }
    }
}