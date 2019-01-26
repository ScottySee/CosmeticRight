using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin1 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            if (Session["UserType"].ToString() != "2")
            {
                Response.Redirect("Login.aspx");
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }
}
