using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProductInventory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetProduct();
            GetProductInventory();
        }
    }

    void GetProduct()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT * FROM Products WHERE Status!='Archived'";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    ddlProduct.DataSource = data;
                    ddlProduct.BackColor = System.Drawing.Color.Black;
                    ddlProduct.DataTextField = "Product";
                    ddlProduct.DataValueField = "ProductID";
                    ddlProduct.DataBind();
                    ddlProduct.Items.Insert(0, new ListItem("Select a product...", ""));
                }
            }
        }
    }

    void GetProductInventory()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        //@"Server=MSI\MSSQLSERVER2;Database=Test;Integrated Security=true";
        {
            con.Open();
            string query = @"SELECT pi.InventoryID, p.Product AS Product, pi.UserID, pi.Quantity, pi.DateManufactured, pi.DateExpired,                      pi.DateAdded, pi.DateModified FROM ProductInventory pi
                              INNER JOIN products p on p.ProductID=pi.ProductID";

            //SELECT COUNT(DISTINCT Country) FROM Customers;

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "ProductInventory");
                    lvInventory.DataSource = ds;
                    lvInventory.DataBind();
                }
            }
        }
    }

    public bool IsExisting(string ID)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT ProductID FROM ProductInventory WHERE ProductID=@ProductID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@ProductID", ddlProduct.SelectedValue);
                return cmd.ExecuteScalar() == null ? false : true;
            }
        }
    }

    protected void AddInventory(object sender, EventArgs e)
    {
        #region Step #1: Add Inventory Record in ProductInventory table
        //bool ItemExisting = IsExisting(ddlProduct.SelectedValue);

        //if (!ItemExisting)
        //{
            using (SqlConnection con = new SqlConnection(Util.GetConnection()))
            {
                con.Open();
                string query = @"INSERT INTO ProductInventory VALUES (@ProductID, @UserID, @Quantity, @DateManufactured, @DateExpired, @DateAdded, @DateModified)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", ddlProduct.SelectedValue);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                    cmd.Parameters.AddWithValue("@Quantity", Server.HtmlEncode(txtavailable.Text.Trim()));
                    cmd.Parameters.AddWithValue("@DateManufactured", Server.HtmlEncode(datestart.Text.Trim()));
                    cmd.Parameters.AddWithValue("@DateExpired", Server.HtmlEncode(dateend.Text.Trim()));
                    cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                    cmd.ExecuteNonQuery();

                    //start of Auditlog 
                    Util.Log(Session["UserID"].ToString(), "The warhouse admin has added an inventory");
                    //end of auditlog

                    message1.InnerText = "Inventory Successfully Added.";

                    //lahat ng textbox
                    ddlProduct.Text = "";
                    txtavailable.Text = "";
                    datestart.Text = "";
                    dateend.Text = "";

                    Response.Redirect("ProductInventory.aspx");
                }
            }
        //}
        //else
        //{
        //    using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        //    {
        //        con.Open();
        //        string query = @"UPDATE ProductInventory SET Quantity = Quantity + @Quantity, DateManufactured=@DateManufactured,   DateExpired=@DateExpired, DateModified=@DateModified WHERE UserID=@UserID AND ProductID=@ProductID";

        //        using (SqlCommand cmd = new SqlCommand(query, con))
        //        {
        //            cmd.Parameters.AddWithValue("@ProductID", ddlProduct.SelectedValue);
        //            cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
        //            cmd.Parameters.AddWithValue("@Quantity", Server.HtmlEncode(txtavailable.Text.Trim()));
        //            cmd.Parameters.AddWithValue("@DateManufactured", Server.HtmlEncode(datestart.Text.Trim()));
        //            cmd.Parameters.AddWithValue("@DateExpired", Server.HtmlEncode(dateend.Text.Trim()));
        //            cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
        //            cmd.ExecuteNonQuery();

        //            //start of Auditlog 
        //            Util.Log(Session["UserID"].ToString(), "The warhouse admin has updated the inventory");
        //            //end of auditlog

        //            message1.InnerText = "Inventory Successfully Updated.";

        //            //lahat ng textbox
        //            ddlProduct.Text = "";
        //            txtavailable.Text = "";
        //            datestart.Text = "";
        //            dateend.Text = "";

        //            Response.Redirect("ProductInventory.aspx");
        //        }
        //    }
        //}
        #endregion
    }

    //protected void MinusInventory(object sender, EventArgs e)
    //{
    //    using (SqlConnection con = new SqlConnection(Util.GetConnection()))
    //    {
    //        con.Open();
    //        string query = @"UPDATE ProductInventory SET Quantity = Quantity - @Quantity, DateModified=@DateModified WHERE UserID=@UserID AND Product=@Product";

    //        using (SqlCommand cmd = new SqlCommand(query, con))
    //        {
    //            cmd.Parameters.AddWithValue("@Product", ddlProduct.SelectedValue);
    //            cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
    //            cmd.Parameters.AddWithValue("@Quantity", Server.HtmlEncode(txtavailable.Text.Trim()));
    //            cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
    //            cmd.ExecuteNonQuery();

    //            //start of Auditlog 
    //            Util.Log(Session["UserID"].ToString(), "The warhouse admin has updated the inventory");
    //            //end of auditlog

    //            message1.InnerText = "Inventory Successfully Updated.";

    //            //lahat ng textbox
    //            ddlProduct.Text = "";
    //            txtavailable.Text = "";
    //            datestart.Text = "";
    //            dateend.Text = "";

    //            Response.Redirect("ProductInventory.aspx");
    //        }
    //    }
    //}
}