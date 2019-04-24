using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

public partial class ProductDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] == null)
        {
            Response.Redirect("ProductDisplay.aspx");
        }
        else
        {
            int productID = 0;
            bool validProduct = int.TryParse(Request.QueryString["ID"].ToString(), out productID);
            if (validProduct)
            {
                if (!IsPostBack)
                {
                    GetData(productID);
                    //GetFeedback();
                }
            }
            else
            {
                Response.Redirect("ProductDisplay.aspx");
            }
        }
    }

    //void GetFeedback()
    //{
    //    using (SqlConnection con = new SqlConnection(Util.GetConnection()))
    //    {
    //        con.Open();
    //        string query = @"SELECT FeedbackID , Product, u.Lastname + ' ' + u.Firstname AS CustomerName,
    //                           f.Comment, f.Rating FROM Feedback f
    //                           INNER JOIN Users u ON f.UserID = u.UserID";

    //        using (SqlCommand cmd = new SqlCommand(query, con))
    //        {
    //            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
    //            {
    //                DataSet ds = new DataSet();
    //                da.Fill(ds, "Feedback");
    //                lvFeedback.DataSource = ds;
    //                lvFeedback.DataBind();
    //            }
    //        }
    //    }
    //}

    void GetData(int ID)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT p.Product, p.Code, p.Image, p.Description, p.CatID,
                            c.Category, p.Price, p.DateManufactured, p.DateExpired FROM Products p
                            INNER JOIN Categories c ON p.CatID = c.CatID
                            WHERE p.ProductID=@ProductID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@ProductID", ID);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ltName.Text = dr["Product"].ToString();
                            ltCode.Text = dr["Code"].ToString();
                            imgProduct.ImageUrl = "~/Images/Products/" + dr["Image"].ToString();
                            ltDesc.Text = Server.HtmlDecode(dr["Description"].ToString());
                            hlCategory.Text = dr["Category"].ToString();
                            hlCategory.NavigateUrl = "~/ProductDisplay.aspx?c=" + dr["CatID"].ToString();
                            double price = double.Parse(dr["Price"].ToString());
                            ltPrice.Text = price.ToString("#,##0.00");
                            datemaufactured.Text = Convert.ToDateTime(dr["DateManufactured"]).ToString("MM/dd/yyyy");
                            dateexpired.Text = Convert.ToDateTime(dr["DateExpired"]).ToString("MM/dd/yyyy");
                        }
                    }
                    else
                    {
                        Response.Redirect("ProductDisplay.aspx");
                    }
                }
            }
        }
    }
}