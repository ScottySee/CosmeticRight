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

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCity();
        }
    }

    void GetCity()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT CityID, City FROM City";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    ddlCity.DataSource = data;
                    ddlCity.BackColor = System.Drawing.Color.Black;
                    ddlCity.DataTextField = "City";
                    ddlCity.DataValueField = "CityID";
                    ddlCity.DataBind();
                    ddlCity.Items.Insert(0, new ListItem("Select a city...", ""));
                }
            }
        }
    }

    public bool EmailDuplicate(string email)
    {
        bool existing = true;
        SqlConnection con = new SqlConnection(Util.GetConnection());
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = @"SELECT Email FROM Users WHERE Email=@Email";

        cmd.Parameters.AddWithValue("@Email", email);
        existing = cmd.ExecuteScalar() == null ? false : true;
        con.Close();
        con.Dispose();
        return existing;
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        bool EmailIsDuplicate = EmailDuplicate(email.Text);

        if (Terms.Checked)
        {
            if (EmailIsDuplicate)
            {
                Label2.Visible = true;
                Label2.Text = "Email have been used";
            }
            else
            {
                if (password.Text == cpassword.Text)
                {
                    using (SqlConnection con = new SqlConnection(Util.GetConnection()))
                    {
                        con.Open();
                        string SQL = @"INSERT INTO Users VALUES (@UserType, @Firstname, @Lastname, @Gender,
                         @BuildingNo, @Street, @Municipality, @CityID, @Landline, @Mobile,
                         @Email, @Password, @EmailCode, @Status, @DateAdded, @DateModified)";

                        string SQL2 = @"SELECT * FROM Users WHERE Email=@Email";
                        using (SqlCommand cmd = new SqlCommand(SQL, con))
                        {
                            cmd.Parameters.AddWithValue("@UserType", "3");
                            cmd.Parameters.AddWithValue("@Firstname", Server.HtmlEncode(firstname.Text.Trim()));
                            cmd.Parameters.AddWithValue("@Lastname", Server.HtmlEncode(lastname.Text.Trim()));
                            cmd.Parameters.AddWithValue("@Gender", gender.SelectedValue);
                            cmd.Parameters.AddWithValue("@BuildingNo", Server.HtmlEncode(buildingno.Text.Trim()));
                            cmd.Parameters.AddWithValue("@Street", Server.HtmlEncode(street.Text.Trim()));
                            cmd.Parameters.AddWithValue("@Municipality", Server.HtmlEncode(municipality.Text.Trim()));
                            cmd.Parameters.AddWithValue("@CityID", ddlCity.SelectedValue);
                            cmd.Parameters.AddWithValue("@Landline", Server.HtmlEncode(landline.Text.Trim()));
                            cmd.Parameters.AddWithValue("@Mobile", Server.HtmlEncode(mobile.Text.Trim()));
                            cmd.Parameters.AddWithValue("@Email", Server.HtmlEncode(email.Text.Trim()));
                            cmd.Parameters.AddWithValue("@Password", Util.CreateSHAHash(password.Text));
                            cmd.Parameters.AddWithValue("@EmailCode", Server.HtmlEncode("dgxdhtrse33434"));
                            cmd.Parameters.AddWithValue("@Status", Server.HtmlEncode("Unverified"));
                            cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                            cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                            cmd.ExecuteNonQuery();

                            Label2.Visible = true;
                            Label2.Text = "Successfully Registered.";

                            //firstname.Text = null;
                            //lastname.Text = null;
                            //gender.SelectedIndex = 0;
                            //buildingno.Text = null;
                            //street.Text = null;
                            //municipality.Text = null;
                            //ddlCity.SelectedIndex = 0;
                            //landline.Text = null;
                            //mobile.Text = null;
                            //email.Text = null;

                        }

                        using (SqlCommand cmd = new SqlCommand(SQL2, con))
                        {
                            cmd.Parameters.AddWithValue("@Email", Server.HtmlEncode(email.Text.Trim()));
                            using (SqlDataReader data = cmd.ExecuteReader())
                            {
                                if (data.HasRows) //credentials are correct
                                {
                                    while (data.Read())
                                    {
                                        ID = data["UserID"].ToString();
                                    }
                                    using (SqlConnection com = new SqlConnection(Util.GetConnection()))
                                    {
                                        com.Open();
                                        string SQL3 = @"UPDATE Users SET EmailCode = 'dgxdhtrse33434' WHERE Email=@Email";
                                        using (SqlCommand cmd2 = new SqlCommand(SQL3, com))
                                        {
                                            cmd2.Parameters.AddWithValue("@Email", Server.HtmlEncode(email.Text.Trim()));
                                            cmd2.ExecuteNonQuery();
                                        }
                                    }

                                    //send email
                                    using (MailMessage mm = new MailMessage("scottysee98@gmail.com", email.Text))
                                    {
                                        mm.Subject = "Email Confirmation";
                                        mm.Body = "Hi, " + firstname.Text + "<br/>" + "Thank you for registering on our website!<br/>" + "<br/>" + "This email was sent for congratulating you for successfully registering on our website!" + "<br/>" + "Click the link to verify your account " + "<a href='http://localhost:58759/EmailVerification.aspx?UserID=" + ID + "'>Verify Account</a>" + "<br/>" + "<br/>" + "If you have any question, please email us at scottysee98@gmail.com" + "<br/>" + "Thank You!";

                                        mm.IsBodyHtml = true;
                                        SmtpClient smtp = new SmtpClient();
                                        smtp.Host = "smtp.gmail.com";
                                        smtp.EnableSsl = true;
                                        NetworkCredential NetworkCred = new NetworkCredential("scottysee98@gmail.com", "POOHPOOH98"); //email and password of the sender.
                                        smtp.UseDefaultCredentials = true;
                                        smtp.Credentials = NetworkCred;
                                        smtp.Port = 587;
                                        smtp.Send(mm);
                                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);
                                    }
                                }
                                else //did not match
                                {

                                }
                            }
                        }
                    }
                }
                else
                {
                    Label2.Visible = true;
                    Label2.Text = "Password must be the same.";
                }
            }
        }
        else
        {
            Label2.Visible = true;
            Label2.Text = "Terms and Condition checkbox should be checked";
        }
    }
}

////send email
//                            using (MailMessage mm = new MailMessage("scottysee98@gmail.com", email.Text))
//                            {
//                                mm.Subject = "Email Verification";
//                                mm.Body = "Hi, " + firstname.Text + "<br/>" + "Thank you for registering on our website!<br/>" + "<br/>" + "This email was sent for congratulating you for successfully registering on our website!" + "<br/>" +
//                                  "Click the link to verify your account " + "<a href='http://localhost:58759/EmailVerification.aspx?UserID=" + ID + "'>Verify Account</a>" + "<br/>" + "<br/>" + "If you have any question, please email us at scottysee98@gmail.com" + "<br/>" + "Thank You!";

//                                mm.IsBodyHtml = true;
//                                SmtpClient smtp = new SmtpClient();
//smtp.Host = "smtp.gmail.com";
//                                smtp.EnableSsl = true;
//                                NetworkCredential NetworkCred = new NetworkCredential("scottysee98@gmail.com", "POOHPOOH98"); //email and password of the sender.
//smtp.UseDefaultCredentials = true;
//                                smtp.Credentials = NetworkCred;
//                                smtp.Port = 587;
//                                smtp.Send(mm);
//                                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);
//                            }