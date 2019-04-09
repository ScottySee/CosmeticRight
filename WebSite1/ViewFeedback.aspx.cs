using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewFeedback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetFeedback();
            //GetProduct();
        }
    }

    //void GetProduct()
    //{
    //    using (SqlConnection con = new SqlConnection(Util.GetConnection()))
    //    {
    //        con.Open();
    //        string query = @"SELECT Product FROM Products WHERE Status!='Archived'";

    //        using (SqlCommand cmd = new SqlCommand(query, con))
    //        {
    //            using (SqlDataReader data = cmd.ExecuteReader())
    //            {
    //                DataSet ds = new DataSet();
    //                data.Fill(ds, "Products");
    //                lvFeedback.DataSource = ds;
    //                lvFeedback.DataBind();
    //            }
    //        }
    //    }
    //}

    void GetFeedback()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT FeedbackID , Product, u.Lastname + ' ' + u.Firstname AS CustomerName,
                               f.Comment, f.Rating, f.Datefeedback FROM Feedback f
                               INNER JOIN Users u ON f.UserID = u.UserID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Feedback");
                    lvFeedback.DataSource = ds;
                    lvFeedback.DataBind();
                }
            }
        }
    }
}