using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        if (password.Text == cpassword.Text)
        {
            using (SqlConnection con = new SqlConnection(Util.GetConnection()))
            {
                con.Open();
                string SQL = @"INSERT INTO Users VALUES ( @Firstname, @Lastname, @Gender,
                         @BuildingNo, @Street, @Municipality, @City, @Landline, @Mobile,
                         @Email, @Password, @DateAdded, @DateModified)";
                using (SqlCommand cmd = new SqlCommand(SQL, con))
                {
                    cmd.Parameters.AddWithValue("@Firstname", firstname.Text);
                    cmd.Parameters.AddWithValue("@Lastname", lastname.Text);
                    cmd.Parameters.AddWithValue("@Gender", gender.SelectedValue);
                    cmd.Parameters.AddWithValue("@BuildingNo", buildingno.Text);
                    cmd.Parameters.AddWithValue("@Street", street.Text);
                    cmd.Parameters.AddWithValue("@Municipality", municipality.Text);
                    cmd.Parameters.AddWithValue("@City", city.Text);
                    cmd.Parameters.AddWithValue("@Landline", landline.Text);
                    cmd.Parameters.AddWithValue("@Mobile", mobile.Text);
                    cmd.Parameters.AddWithValue("@Email", email.Text);
                    cmd.Parameters.AddWithValue("@Password", Util.CreateSHAHash(password.Text));
                    cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                    cmd.ExecuteNonQuery();

                   
                    Response.Redirect("Login.aspx");
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