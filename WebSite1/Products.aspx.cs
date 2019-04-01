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
            GetCategories();
        }
        //FOR VIEWING
        GetProducts();
        GetCategories();
    }

    void GetProducts()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        //@"Server=MSI\MSSQLSERVER2;Database=Test;Integrated Security=true";
        {
            con.Open();
            string query = @"SELECT * FROM Products WHERE Status != 'Archived'";

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

    // eto yung para sa pagconnect sa database para sa category
    void GetCategories()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT CatID, Category FROM Category";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    ddlCategories.DataSource = data;
                    ddlCategories.BackColor = System.Drawing.Color.Black;
                    ddlCategories.DataTextField = "Category";
                    ddlCategories.DataValueField = "CatID";
                    ddlCategories.DataBind();
                    ddlCategories.Items.Insert(0, new ListItem("Select a category...", ""));
                }
            }
        }
    }

    protected void AddProduct(object sender, EventArgs e)
    {
        string fileName = Path.GetFileName(fileUpload1.FileName);
        fileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/Products/") + fileName);
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"INSERT INTO Products VALUES (@Name, @CatID, @Code, @Description, @Image, @Price, @Available, @Criticallevel, @Maximum, @DateManufactured, @DateExpired, @Status, @DateAdded, @DateModified)";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Name", Server.HtmlEncode(txtProductName.Text.Trim()));
                cmd.Parameters.AddWithValue("@CatID", ddlCategories.SelectedValue);
                cmd.Parameters.AddWithValue("@Code", Server.HtmlEncode(txtCode.Text.Trim()));
                cmd.Parameters.AddWithValue("@Description", Server.HtmlEncode(txtDescription.Text.Trim()));
                cmd.Parameters.AddWithValue("@Image", fileUpload1.FileName);
                cmd.Parameters.AddWithValue("@Price", Server.HtmlEncode(txtPrice.Text.Trim()));
                cmd.Parameters.AddWithValue("@Available", Server.HtmlEncode(Available.Text.Trim()));
                cmd.Parameters.AddWithValue("@Criticallevel", Server.HtmlEncode(txtCritical.Text.Trim()));
                cmd.Parameters.AddWithValue("@Maximum", Server.HtmlEncode(txtMax.Text.Trim()));
                cmd.Parameters.AddWithValue("@DateManufactured", Server.HtmlEncode(datestart.Text));
                cmd.Parameters.AddWithValue("@DateExpired", Server.HtmlEncode(dateend.Text));
                cmd.Parameters.AddWithValue("@Status", Server.HtmlEncode("Active"));
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                cmd.ExecuteNonQuery();

                //start of Auditlog 
                Util.Log(Session["UserID"].ToString(), "The warehouse admin has added a product");
                //end of auditlog

                message.InnerText = "Product Successfully Added.";

                //lahat ng textbox
                txtProductName.Text = null;
                txtCode.Text = null;
                txtDescription.Text = null;
                txtPrice.Text = null;
                Available.Text = null;
                txtCritical.Text = null;
                txtMax.Text = null;
                datestart.Text = null;
                dateend.Text = null;
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
                            datestart.Text = Convert.ToDateTime(data["DateManufactured"]).ToString("MM/dd/yyyy");
                            dateend.Text = Convert.ToDateTime(data["DateExpired"]).ToString("MM/dd/yyyy");
                        }
                    }
                    else
                    {
                        Response.Redirect("Announcement.aspx");
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
                Description=@Description, Price=@Price, Image=@Image,
                Criticallevel=@Criticallevel, Maximum=@Maximum, Available=@Available, DateManufactured=@DateManufactured,
                DateExpired=@DateExpired, DateModified =@DateModified WHERE ProductID=@ProductID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Name", Server.HtmlEncode(txtProductName.Text.Trim()));
                cmd.Parameters.AddWithValue("@CatID", ddlCategories.SelectedValue);
                cmd.Parameters.AddWithValue("@Code", Server.HtmlEncode(txtCode.Text.Trim()));
                cmd.Parameters.AddWithValue("@Description", Server.HtmlEncode(txtDescription.Text.Trim()));

                if (fileUpload1.HasFile)
                {
                    string fileExt = Path.GetFileName(fileUpload1.FileName);
                    cmd.Parameters.AddWithValue("@Image", fileExt);
                    //fileUpload1.SaveAs("~/Images/Announcement/" + fileExt);
                    fileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/Products/") + fileExt);
                }
                else
                {
                    string fileExt = Path.GetFileName(fileUpload1.FileName);

                    cmd.Parameters.AddWithValue("@Image", Session["image"].ToString());
                    Session.Remove("image");
                }
                cmd.Parameters.AddWithValue("@Price", Server.HtmlEncode(txtPrice.Text.Trim()));
                cmd.Parameters.AddWithValue("@Available", Server.HtmlEncode(Available.Text.Trim()));
                cmd.Parameters.AddWithValue("@Criticallevel", Server.HtmlEncode(txtCritical.Text.Trim()));
                cmd.Parameters.AddWithValue("@Maximum", Server.HtmlEncode(txtMax.Text.Trim()));
                cmd.Parameters.AddWithValue("@DateManufactured", Server.HtmlEncode(datestart.Text.Trim()));
                cmd.Parameters.AddWithValue("@DateExpired", Server.HtmlEncode(dateend.Text.Trim()));
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                cmd.Parameters.AddWithValue("@ProductID", Session["ProductID"].ToString());
                cmd.ExecuteNonQuery();

                //start of Auditlog 
                Util.Log(Session["UserID"].ToString(), "The office admin has updated a product");
                //end of auditlog

                message.InnerText = "Product Successfully Updated.";

                txtProductName.Text = null;
                txtCode.Text = null;
                txtDescription.Text = null;
                txtPrice.Text = null;
                Available.Text = null;
                txtCritical.Text = null;
                txtMax.Text = null;
                datestart.Text = null;
                dateend.Text = null;
            }
        }
    }

    void DeleteRecord(int ID)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"UPDATE Products SET Status = 'Archived' WHERE ProductID=@ProductID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@ProductID", ID);
                cmd.ExecuteNonQuery();

                //start of Auditlog 
                Util.Log(Session["UserID"].ToString(), "The Warehouse Admin has archived a product");
                //end of auditlog

                Response.Redirect("Products.aspx");
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Products.aspx");
    }

    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    dpProducts.SetPageProperties(0, dpProducts.MaximumRows, false);

    //    if (txtKeyword.Text == "")
    //        GetProducts();
    //    else
    //        GetProducts(txtKeyword.Text);
    //}

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