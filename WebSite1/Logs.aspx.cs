using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetLogs();
        }
    }

    void GetLogs()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT LogID, u.Lastname + ' ' + u.Firstname AS CustomerName, l.LogTime, l.Activity FROM Logs l
                                INNER JOIN Users u ON l.UserID = u.UserID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Logs");
                    lvOrders.DataSource = ds;
                    lvOrders.DataBind();
                }
            }
        }
    }
}