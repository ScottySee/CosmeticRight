using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditOfficeAdminProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            if (!IsPostBack)
            {
                GetData(Convert.ToInt16(Session["UserID"]));
            }
        }
        else
            Response.Redirect("Login.aspx");
    }

    void GetData(int ID)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT * FROM Users WHERE UserID=@UserID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@UserID", Session["userid"]);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            //ltFirstName.Text = Session["Firstname"].ToString();
                            //ltLastName.Text = Session["Lastname"].ToString();
                            firstname.Text = data["Firstname"].ToString();
                            lastname.Text = data["Lastname"].ToString();
                            gender.Text = data["Gender"].ToString();
                            buildingno.Text = data["BuildingNo"].ToString();
                            street.Text = data["Street"].ToString();
                            municipality.Text = data["Municipality"].ToString();
                            city.Text = data["City"].ToString();
                            landline.Text = data["Landline"].ToString();
                            mobile.Text = data["Mobile"].ToString();
                            email.Text = data["Email"].ToString();
                        }
                    }
                }
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"UPDATE Users SET FirstName=@FirstName, LastName=@LastName, Gender=@Gender, BuildingNo=@BuildingNo, Street=@Street, Municipality=@Municipality, City=@City, Landline=@Landline, Mobile=@Mobile, Email=@Email, EmailCode=@EmailCode, DateModified=@DateModified
              WHERE UserID=@UserID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Firstname", Server.HtmlEncode(firstname.Text.Trim()));
                cmd.Parameters.AddWithValue("@Lastname", Server.HtmlEncode(lastname.Text.Trim()));
                cmd.Parameters.AddWithValue("@Gender", gender.SelectedValue);
                cmd.Parameters.AddWithValue("@BuildingNo", Server.HtmlEncode(buildingno.Text.Trim()));
                cmd.Parameters.AddWithValue("@Street", Server.HtmlEncode(street.Text));
                cmd.Parameters.AddWithValue("@Municipality", Server.HtmlEncode(municipality.Text.Trim()));
                cmd.Parameters.AddWithValue("@City", Server.HtmlEncode(city.Text.Trim()));
                cmd.Parameters.AddWithValue("@Landline", Server.HtmlEncode(landline.Text.Trim()));
                cmd.Parameters.AddWithValue("@Mobile", Server.HtmlEncode(mobile.Text.Trim()));
                cmd.Parameters.AddWithValue("@Email", Server.HtmlEncode(email.Text.Trim()));
                cmd.Parameters.AddWithValue("@EmailCode", Server.HtmlEncode("dgxdhtrse33434"));
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                cmd.ExecuteNonQuery();

                //start of Auditlog 
                Util.Log(Session["UserID"].ToString(), "The Office Admin updated their profile");
                //end of auditlog

                Response.Redirect("OfficeAdminProfile.aspx");
            }
        }
    }
}