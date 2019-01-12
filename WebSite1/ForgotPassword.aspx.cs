using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

public partial class ForgotPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {


        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT * FROM Users
                                WHERE Email=@Email";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Email", email.Text);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows) //credentials are correct
                    {
                        using (SqlConnection com = new SqlConnection(Util.GetConnection()))
                        {
                            com.Open();
                            string SQL = @"UPDATE Users SET EmailCode = 'dgxdhtrse33434' WHERE Email=@Email";
                            using (SqlCommand cmd2 = new SqlCommand(SQL, com))
                            {
                                cmd2.Parameters.AddWithValue("@Email", email.Text);

                                cmd2.ExecuteNonQuery();


                               
                            }
                        }

                        using (MailMessage mm = new MailMessage("scottysee98@gmail.com", email.Text))
                        {
                            mm.Subject = "Forgot Password";
                            mm.Body = "Test mail";

                            mm.IsBodyHtml = false;
                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = "smtp.gmail.com";
                            smtp.EnableSsl = true;
                            NetworkCredential NetworkCred = new NetworkCredential("scottysee98@gmail.com", "POOHPOOH98");
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = NetworkCred;
                            smtp.Port = 587;
                            smtp.Send(mm);
                            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);
                        }
                    }
                    else //did not match </3
                    {

                    }
                }
            }
        }
    }
}