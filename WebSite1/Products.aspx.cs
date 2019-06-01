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
                    //GetCategories();
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
            string query = @"SELECT CatID, Category FROM Categories WHERE Status!='Archived'";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    ddlCategories.DataSource = data;
                    ddlCategories.BackColor = System.Drawing.Color.Black;
                    ddlCategories.DataTextField = "Category";
                    ddlCategories.DataValueField = "CatID";
                    ddlCategories.SelectedValue = null;
                    ddlCategories.DataBind();
                    ddlCategories.Items.Insert(0, new ListItem("Select a category...", ""));
                }
            }
        }
    }

    public bool CodeIsExisting(string code)
    {
        bool existing = true;
        SqlConnection con = new SqlConnection(Util.GetConnection());
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = @"SELECT Code FROM Products WHERE Code=@Code";
        cmd.Parameters.AddWithValue("@Code", code);
        existing = cmd.ExecuteScalar() == null ? false : true;
        con.Close();
        con.Dispose();
        return existing;
    }

    private bool CompareArray(byte[] a1, byte[] a2)
    {
        if (a1.Length != a2.Length)
            return false;
        for (int i = 0; i < a1.Length; i++)
        {
            if (a1[i] != a2[i])
                return false;
        }
        return true;
    }

    protected void AddProduct(object sender, EventArgs e)
    {
        Dictionary<string, byte[]> imageHeader = new Dictionary<string, byte[]>();
        imageHeader.Add("JPG", new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 });
        imageHeader.Add("JPEG", new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 });
        imageHeader.Add("PNG", new byte[] { 0x89, 0x50, 0x4E, 0x47 });

        byte[] header;
        string fileExt;
        fileExt = fileUpload1.FileName.Substring(fileUpload1.FileName.LastIndexOf('.') + 1).ToUpper();

        string fileName = Path.GetFileName(fileUpload1.FileName);
        fileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/Products/") + fileName);

        byte[] tmp = imageHeader[fileExt];
        header = new byte[tmp.Length];
        fileUpload1.FileContent.Read(header, 0, header.Length);

        bool CodeExisting = CodeIsExisting(txtCode.Text);

        if (CompareArray(tmp, header))
        {
            if (!CodeExisting)
            {
                if (txtProductName.Text.Trim().Length > 0)
                {
                    using (SqlConnection con = new SqlConnection(Util.GetConnection()))
                    {
                        con.Open();
                        string query = @"INSERT INTO Products VALUES (@Product, @CatID, @Code, @Description, @Image, @Price, @Criticallevel, @Maximum, @UserID, @Status, @DateAdded, @DateModified)";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@Product", Server.HtmlEncode(txtProductName.Text.Trim()));
                            cmd.Parameters.AddWithValue("@CatID", ddlCategories.SelectedValue);
                            cmd.Parameters.AddWithValue("@Code", Server.HtmlEncode(txtCode.Text.Trim()));
                            cmd.Parameters.AddWithValue("@Description", Server.HtmlEncode(txtDescription.Text.Trim()));
                            cmd.Parameters.AddWithValue("@Image", fileUpload1.FileName);
                            cmd.Parameters.AddWithValue("@Price", Server.HtmlEncode(txtPrice.Text.Trim()));
                            cmd.Parameters.AddWithValue("@Criticallevel", Server.HtmlEncode(txtCritical.Text.Trim()));
                            cmd.Parameters.AddWithValue("@Maximum", Server.HtmlEncode(txtMax.Text.Trim()));
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                            cmd.Parameters.AddWithValue("@Status", Server.HtmlEncode("Active"));
                            cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                            cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                            cmd.ExecuteNonQuery();

                            //start of Auditlog 
                            Util.Log(Session["UserID"].ToString(), "The warehouse admin has added a product");
                            //end of auditlog

                            message.InnerText = "Product Successfully Added.";

                            //lahat ng textbox
                            txtProductName.Text = "";
                            txtCode.Text = "";
                            txtDescription.Text = "";
                            txtPrice.Text = "";
                            txtCritical.Text = "";
                            txtMax.Text = "";
                        }
                    }
                }
                else
                {
                    message.InnerText = "Product Name cannot be empty.";
                }
            }
            else
            {
                message.InnerText = "Code have already existed.";
            }
        }
        else
        {
            message.InnerText = "Image has invalid format.";
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
                            txtProductName.Text = data["Product"].ToString();
                            ddlCategories.SelectedValue = data["CatID"].ToString();
                            txtCode.Text = data["Code"].ToString();
                            Session["image"] = data["Image"].ToString();
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
        Dictionary<string, byte[]> imageHeader = new Dictionary<string, byte[]>();
        imageHeader.Add("JPG", new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 });
        imageHeader.Add("JPEG", new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 });
        imageHeader.Add("PNG", new byte[] { 0x89, 0x50, 0x4E, 0x47 });

        byte[] header;
        string fileExt;
        fileExt = fileUpload1.FileName.Substring(fileUpload1.FileName.LastIndexOf('.') + 1).ToUpper();
        byte[] tmp = imageHeader[fileExt];
        header = new byte[tmp.Length];
        fileUpload1.FileContent.Read(header, 0, header.Length);

        bool CodeExisting = CodeIsExisting(txtCode.Text);

        if (CompareArray(tmp, header))
        {
            if (!CodeExisting)
            {
                if (txtProductName.Text.Trim().Length > 0)
                {
                    using (SqlConnection con = new SqlConnection(Util.GetConnection()))
                    {
                        con.Open();
                        string query = @"UPDATE Products SET Product=@Product, CatID=@CatID, Code=@Code,
                Description=@Description, Price=@Price, Image=@Image,
                Criticallevel=@Criticallevel, Maximum=@Maximum, DateModified=@DateModified
                WHERE ProductID=@ProductID";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@Product", Server.HtmlEncode(txtProductName.Text.Trim()));
                            cmd.Parameters.AddWithValue("@CatID", ddlCategories.SelectedValue);
                            cmd.Parameters.AddWithValue("@Code", Server.HtmlEncode(txtCode.Text.Trim()));
                            cmd.Parameters.AddWithValue("@Description", Server.HtmlEncode(txtDescription.Text.Trim()));

                            if (fileUpload1.HasFile)
                            {
                                string filename = Path.GetFileName(fileUpload1.FileName);
                                cmd.Parameters.AddWithValue("@Image", filename);
                                //fileUpload1.SaveAs("~/Images/Announcement/" + fileExt);
                                fileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/Products/") + filename);
                            }
                            else
                            {
                                string filename = Path.GetFileName(fileUpload1.FileName);

                                cmd.Parameters.AddWithValue("@Image", Session["image"].ToString());
                                Session.Remove("image");
                            }
                            cmd.Parameters.AddWithValue("@Price", Server.HtmlEncode(txtPrice.Text.Trim()));
                            cmd.Parameters.AddWithValue("@Criticallevel", Server.HtmlEncode(txtCritical.Text.Trim()));
                            cmd.Parameters.AddWithValue("@Maximum", Server.HtmlEncode(txtMax.Text.Trim()));
                            cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                            cmd.Parameters.AddWithValue("@ProductID", Session["ProductID"].ToString());
                            cmd.ExecuteNonQuery();

                            //start of Auditlog 
                            Util.Log(Session["UserID"].ToString(), "The office admin has updated a product");
                            //end of auditlog

                            message.InnerText = "Product Successfully Updated.";

                            txtProductName.Text = "";
                            txtCode.Text = "";
                            txtDescription.Text = "";
                            txtPrice.Text = "";
                            txtCritical.Text = "";
                            txtMax.Text = "";
                        }
                    }
                }
                else
                {
                    message.InnerText = "Product Name cannot be empty";
                }
            }
            else
            {
                message.InnerText = "Code have already existed.";
            }
        }
        else
        {
            message.InnerText = "Image has invalid format.";
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

    //protected void ddlCategories_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    switch (ddlCategories.SelectedValue)
    //    {
    //        case "1":
    //            lblunit.Text = "Kilogram";
    //            break;
    //        case "2":
    //            lblunit.Text = "Liters";
    //            break;
    //        case "3":
    //            lblunit.Text = "Pieces";
    //            break;
    //        default:
    //            lblunit.Text = "";
    //            break;
    //    }
    //}
}