using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WarehouseAdminProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                GetData(Convert.ToInt16(Session["userid"]));
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
            string query = @"SELECT u.UserID, u.UserType, u.Firstname, u.Lastname, u.Gender, u.BuildingNo, u.Street, u.Municipality, 
                                c.City AS City, u.Landline, u.Mobile, u.Email FROM Users u
                                INNER JOIN City c ON u.CityID=c.CityID
                                WHERE UserID=@UserID";

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

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditWarehouseAdminProfile.aspx");
    }
}