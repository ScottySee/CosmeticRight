using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Categories : System.Web.UI.Page
{
    int editID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnEdit.Visible = false;
            //FOR VIEWING THE EDIT INTERFACE
            //EDITVIEW START
            if (Request.QueryString["EditID"] != null)
            {

                bool validCategory = int.TryParse(Request.QueryString["EditID"].ToString(), out editID);

                if (validCategory)
                {
                    btnAdd.Visible = false;
                    btnEdit.Visible = true;
                    EditCategory(editID);
                }
                else
                    Response.Redirect("Categories.aspx");
            }
            //EDITVIEW END
            //DELETE ID CHECKING START
            else if (Request.QueryString["DeleteID"] != null)
            {
                int DeleteID = 0;
                bool validCategory = int.TryParse(Request.QueryString["DeleteID"].ToString(), out DeleteID);

                if (validCategory)
                {
                    DeleteRecord(DeleteID);
                }
                else
                    Response.Redirect("Categories.aspx");
            }
            //DELETE ID CHECKING END
            GetCategory();
        }
        //FOR VIEWING
        GetCategory();
    }

    void GetCategory()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        //@"Server=MSI\MSSQLSERVER2;Database=Test;Integrated Security=true";
        {
            con.Open();
            string query = @"SELECT c.CatID, Category, u.Lastname + ' ' + u.Firstname AS Username, Status FROM Category                   c INNER JOIN Users u ON c.UserID = u.UserID WHERE Status != 'Archived'";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Category");
                    lvCategories.DataSource = ds;
                    lvCategories.DataBind();
                }
            }
        }
    }

    void GetCategory(string keyword)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT * FROM Category
                                WHERE Category LIKE @keyword";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Category");
                    lvCategories.DataSource = ds;
                    lvCategories.DataBind();
                }
            }
        }
    }

    protected void AddCategory(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"INSERT INTO Category VALUES (@Category, @UserID, @Status, @DateAdded, @DateModified)";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Category", Server.HtmlEncode(txtCategory.Text.Trim()));
                cmd.Parameters.AddWithValue("@UserID", Server.HtmlEncode("2"));
                cmd.Parameters.AddWithValue("@Status", Server.HtmlEncode("Active"));
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                cmd.ExecuteNonQuery();

                //start of Auditlog 
                Util.Log(Session["UserID"].ToString(), "The office admin has added a category");
                //end of auditlog

                Response.Redirect("Categories.aspx");
            }
        }
    }

    protected void EditCategory(int ID)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT * FROM Category WHERE CatID=@CatID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@CatID", ID);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            catID.Text = data["CatID"].ToString();
                            Session["CatID"] = data["CatID"].ToString();
                            txtCategory.Text = data["Category"].ToString();
                        }
                    }
                    else
                    {
                        Response.Redirect("Categories.aspx");
                    }
                }
            }
        }
    }
    protected void SaveCategory(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"UPDATE Category SET Category=@Category WHERE CatID=@CatID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Category", Server.HtmlEncode(txtCategory.Text.Trim()));
                cmd.Parameters.AddWithValue("@CatID", Session["CatID"].ToString());
                //cmd.Parameters.AddWithValue("@Image", "Sample.jpg");

                cmd.ExecuteNonQuery();

                //start of Auditlog 
                Util.Log(Session["UserID"].ToString(), "The office admin has updated a category");
                //end of auditlog

                Response.Redirect("Categories.aspx");
            }
        }
    }

    void DeleteRecord(int ID)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"UPDATE Category SET Status = 'Archived' WHERE CatID=@CatID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@CattID", ID);
                cmd.ExecuteNonQuery();

                //start of Auditlog 
                Util.Log(Session["UserID"].ToString(), "The office admin has archived a category");
                //end of auditlog

                Response.Redirect("Categories.aspx");
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Categories.aspx");
    }
}