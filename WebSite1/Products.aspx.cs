using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Products : System.Web.UI.Page
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

                bool validProduct = int.TryParse(Request.QueryString["EditID"].ToString(), out editID);

                if (validProduct)
                {
                    btnAdd.Visible = false;
                    btnEdit.Visible = true;
                    EditProduct(editID);
                }
                else
                    Response.Redirect("Products.aspx");
            }
            //EDITVIEW END
            //DELETE ID CHECKING START
            else if (Request.QueryString["DeleteID"] != null)
            {
                int DeleteID = 0;
                bool validProduct = int.TryParse(Request.QueryString["DeleteID"].ToString(), out DeleteID);

                if (validProduct)
                {
                    DeleteRecord(DeleteID);
                }
                else
                    Response.Redirect("Products.aspx");
            }
            //DELETE ID CHECKING END
            GetProducts();
        }
        //FOR VIEWING
        GetProducts();
        //GetCategories();
    }

    void GetProducts()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT * FROM Products";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Products");
                    lvProducts.DataSource = ds;
                    lvProducts.DataBind();
                }
            }
        }
    }

    // di ako sure
    //void GetCategories()
    //{
    //    using (SqlConnection con = new SqlConnection(Util.GetConnection()))
    //    {
    //        con.Open();
    //        string query = @"SELECT * FROM Categories";

    //        using (SqlCommand cmd = new SqlCommand(query, con))
    //        {
    //            using (SqlDataAdapter data = new SqlDataAdapter())
    //            {
    //                ddlCategories.DataSource = data;
    //                ddlCategories.DataTextField = "Categories";
    //                ddlCategories.DataValueField = "CatID";
    //                ddlCategories.DataBind();

    //                ddlCategories.Items.Insert(0, new ListItem("Select a Category...", ""));
    //            }
    //        }
    //    }
    //}

    protected void AddProduct(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"INSERT INTO Products VALUES (@Name, @CatID, @Code, @Description, @Price, @Available, @Criticallevel, @Maximum, @Status, @DateAdded, @DateModified)";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Name", txtProductName.Text);
                cmd.Parameters.AddWithValue("@CatID", ddlCategories.SelectedValue);
                cmd.Parameters.AddWithValue("@Code", txtCode.Text);
                cmd.Parameters.AddWithValue("@Description", Server.HtmlEncode(txtDescription.Text));

                //string fileExt = Path.GetExtension(ImageUpload.FileName);
                //string id = Guid.NewGuid().ToString();
                //cmd.Parameters.AddWithValue("@Image", id + fileExt);
                //fuImage.SaveAs(Server.MapPath("~/Images/Products/" + id + fileExt));

                cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@Available", 0);
                cmd.Parameters.AddWithValue("@Criticallevel", txtCritical.Text);
                cmd.Parameters.AddWithValue("@Maximum", txtMax.Text);
                cmd.Parameters.AddWithValue("@Status", "Active");
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                cmd.ExecuteNonQuery();

                Response.Redirect("Products.aspx");
            }
        }
    }
    
    protected void EditProduct(int ID)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT * FROM Products WHERE ProductID=@ProductID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@ProductID", ID);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            productID.Text = data["ProductID"].ToString();
                            txtProductName.Text = data["Name"].ToString();
                            ddlCategories.SelectedValue = data["CatID"].ToString();
                            txtCode.Text = data["Code"].ToString();
                            txtDescription.Text = data["Description"].ToString();
                            txtPrice.Text = data["Price"].ToString();
                            txtCritical.Text = data["Criticallevel"].ToString();
                            txtMax.Text = data["Maximum"].ToString();
                        }
                    }
                    else
                    {
                        Response.Redirect("Products.aspx");
                    }
                }
            }
        }
    }

    protected void SaveProduct(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"UPDATE Products SET Name=@Name, CatID=@CatID, Code=@Code,
                Description=@Description, Price=@Price,
                Criticallevel=@Criticallevel, Maximum=@Maximum,
                DateModified=@DateModified WHERE ProductID=@ProductID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Name", txtProductName.Text);
                cmd.Parameters.AddWithValue("@CatID", ddlCategories.SelectedValue);
                cmd.Parameters.AddWithValue("@Code", txtCode.Text);
                cmd.Parameters.AddWithValue("@Description", Server.HtmlEncode(txtDescription.Text));
                //if (fuImage.HasFile)
                //{
                //    string fileExt = Path.GetExtension(fuImage.FileName);
                //    string id = Guid.NewGuid().ToString();
                //    cmd.Parameters.AddWithValue("@Image", id + fileExt);
                //    fuImage.SaveAs(Server.MapPath("~/Images/Products/" + id + fileExt));
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@Image", Session["image"].ToString());
                //    Session.Remove("image");
                //}
                cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@Criticallevel", txtCritical.Text);
                cmd.Parameters.AddWithValue("@Maximum", txtMax.Text);
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                cmd.Parameters.AddWithValue("@ProductID", Request.QueryString["ID"].ToString());
                //cmd.Parameters.AddWithValue("@Image", "Sample.jpg");

                cmd.ExecuteNonQuery();

                Response.Redirect("Products.aspx");
            }
        }
    }

    void DeleteRecord(int ID)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"DELETE FROM Products WHERE ProductID=@ProductID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@ProductID", ID);
                cmd.ExecuteNonQuery();
                Response.Redirect("Products.aspx");
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Products.aspx");
    }
}