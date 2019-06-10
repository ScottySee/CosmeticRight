using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmailVerification : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        //    {
        //        con.Open();
        //        string query = @"UPDATE Users SET Status=@Status WHERE UserID=@UserID";
        //        using (SqlCommand cmd = new SqlCommand(query, con))
        //        {
        //            cmd.Parameters.AddWithValue("@Status", Server.HtmlEncode("Verified"));
        //            cmd.Parameters.AddWithValue("@UserID", Request.QueryString["UserID"]);
        //            cmd.ExecuteNonQuery();

        //            start of Auditlog
        //            Util.Log(Request.QueryString["UserID"], "The member has verified their account");
        //            end of auditlog

        //            Response.Redirect("EmailVerification.aspx");
        //        }
        //    }
        //}
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"UPDATE Users SET Status=@Status WHERE UserID=@UserID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", Server.HtmlEncode("Verified"));
                cmd.Parameters.AddWithValue("@UserID", Request.QueryString["UserID"]);
                cmd.ExecuteNonQuery();

                //start of Auditlog 
                Util.Log(Request.QueryString["UserID"], "The member has verified their account");
                //end of auditlog

                Response.Redirect("Login.aspx");
            }
        }
    }
}