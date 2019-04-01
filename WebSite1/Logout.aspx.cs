using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ////start of Auditlog 
        //Util.Log(Session["UserID"].ToString(), "The user has logged out");
        ////end of auditlog
        Session.Abandon();
        Response.Redirect("Login.aspx");
    }
}