using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditWarehouseAdminProfile : System.Web.UI.Page
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
                cmd.Parameters.AddWithValue("@EmailCode", "dgxdhtrse33434");
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                cmd.ExecuteNonQuery();

                //start of Auditlog 
                Util.Log(Session["UserID"].ToString(), "The Warehouse Admin has updated their profile");
                //end of auditlog

                Response.Redirect("WarehouseAdminProfile.aspx");
            }
        }
    }
}