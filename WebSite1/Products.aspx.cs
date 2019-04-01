using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Products : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(Util.GetConnection());
    SqlCommand cmd;
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
        string query = @"SELECT * FROM Products WHERE Status != 'Archived'";

        cmd = new SqlCommand(query, con);
        con.Open();
        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        {
            DataSet ds = new DataSet();
            da.Fill(ds, "Products");
            lvProducts.DataSource = ds;
            lvProducts.DataBind();
        }
        con.Close();
    }

    void GetProducts(string keyword)
    {
        string query = @"SELECT * FROM Products
                            WHERE Name LIKE @keyword OR
                            Code LIKE @keyword";

        cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
        con.Open();
        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        {
            DataSet ds = new DataSet();
            da.Fill(ds, "Products");
            lvProducts.DataSource = ds;
            lvProducts.DataBind();
        }
        con.Close();
    }

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
        string fileName = Path.GetFileName(fileUpload.FileName);
        fileUpload.PostedFile.SaveAs(Server.MapPath("~/Images/Products/") + fileName);

        con.Open();
        string query = @"INSERT INTO Products VALUES (@Name, @CatID, @Code, @Description, @Image, @Price, @Available, @Criticallevel, @Maximum, @Status, @DateAdded, @DateModified)";

        cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@Name", txtProductName.Text);
        cmd.Parameters.AddWithValue("@CatID", ddlCategories.SelectedValue);
        cmd.Parameters.AddWithValue("@Code", txtCode.Text);
        cmd.Parameters.AddWithValue("@Description", Server.HtmlEncode(txtDescription.Text));
        cmd.Parameters.AddWithValue("@Image", fileUpload.FileName);
        cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
        cmd.Parameters.AddWithValue("@Available", Available.Text);
        cmd.Parameters.AddWithValue("@Criticallevel", txtCritical.Text);
        cmd.Parameters.AddWithValue("@Maximum", txtMax.Text);
        cmd.Parameters.AddWithValue("@Status", "Active");
        cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
        cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
        cmd.ExecuteNonQuery();

        //start of Auditlog 
        Util.Log(Session["UserID"].ToString(), "The Warehouse Admin has addded a product.");
        //end of auditlog

        con.Close();

        Response.Redirect("Products.aspx");

    }

    protected void EditProduct(int ID)
    {
        con.Open();
        string query = @"SELECT * FROM Products WHERE ProductID=@ProductID";

        cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@ProductID", ID);
        using (SqlDataReader data = cmd.ExecuteReader())
        {
            if (data.HasRows)
            {
                while (data.Read())
                {
                    productID.Text = data["ProductID"].ToString();
                    Session["ProductID"] = data["ProductID"].ToString();
                    txtProductName.Text = data["Name"].ToString();
                    ddlCategories.SelectedValue = data["CatID"].ToString();
                    txtCode.Text = data["Code"].ToString();
                    Session["image"] = data["Image"].ToString();
                    txtDescription.Text = data["Description"].ToString();
                    Available.Text = data["Available"].ToString();
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
        con.Close();
    }

    protected void SaveProduct(object sender, EventArgs e)
    {
        con.Open();
        string query = @"UPDATE Products SET Name=@Name, CatID=@CatID, Code=@Code,
                Description=@Description, Price=@Price, Image=@Image,
                Criticallevel=@Criticallevel, Maximum=@Maximum, Available=@Available,
                DateModified=@DateModified WHERE ProductID=@ProductID";
        cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@Name", txtProductName.Text);
        cmd.Parameters.AddWithValue("@CatID", ddlCategories.SelectedValue);
        cmd.Parameters.AddWithValue("@Code", txtCode.Text);
        cmd.Parameters.AddWithValue("@Description", Server.HtmlEncode(txtDescription.Text));
        //cmd.Parameters.AddWithValue("@Image", fileUpload.FileName);

        if (fileUpload.HasFile)
        {
            string fileExt = Path.GetFileName(fileUpload.FileName);
            cmd.Parameters.AddWithValue("@Image", fileExt);
            //fileUpload1.SaveAs("~/Images/Announcement/" + fileExt);
            fileUpload.PostedFile.SaveAs(Server.MapPath("~/Images/Products/") + fileExt);
        }
        else
        {
            string fileExt = Path.GetFileName(fileUpload.FileName);

           cmd.Parameters.AddWithValue("@Image", Session["image"].ToString());
            Session.Remove("image");
        }
        cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
        cmd.Parameters.AddWithValue("@Available", Available.Text);
        cmd.Parameters.AddWithValue("@Criticallevel", txtCritical.Text);
        cmd.Parameters.AddWithValue("@Maximum", txtMax.Text);
        cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
        cmd.Parameters.AddWithValue("@ProductID", Session["ProductID"].ToString());
        //cmd.Parameters.AddWithValue("@Image", "Sample.jpg");

        //start of Auditlog 
        Util.Log(Session["UserID"].ToString(), "The Warehouse Admin has updated the product");
        //end of auditlog

        cmd.ExecuteNonQuery();
        con.Close();
        Response.Redirect("Products.aspx");

    }

    void DeleteRecord(int ID)
    {
        con.Open();
        string query = @"UPDATE Products SET Status = 'Archived' WHERE ProductID=@ProductID";

        cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@ProductID", ID);
        cmd.ExecuteNonQuery();

        //start of Auditlog 
        Util.Log(Session["UserID"].ToString(), "The Warehouse Admin has archived a product");
        //end of auditlog

        con.Close();
        Response.Redirect("Products.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Products.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        dpProducts.SetPageProperties(0, dpProducts.MaximumRows, false);

        if (txtKeyword.Text == "")
            GetProducts();
        else
            GetProducts(txtKeyword.Text);
    }

    protected void ddlCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (ddlCategories.SelectedValue)
        {
            case "1":
                lblunit.Text = "Kilogram";
                break;
            case "2":
                lblunit.Text = "Liters";
                break;
            case "3":
                lblunit.Text = "Pieces";
                break;
            default:
                lblunit.Text = "";
                break;
        }
    }
}