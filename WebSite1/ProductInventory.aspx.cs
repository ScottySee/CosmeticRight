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
    int ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //DELETE ID CHECKING START
            if (Request.QueryString["DeleteID"] != null)
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
            else
            {
                GetProduct();
                GetProductInventory();
                GetInventory();
            }
            //DELETE ID CHECKING END
            GetProduct();
            GetProductInventory();
            GetInventory();

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
        {
            con.Open();
            string query = @"SELECT pi.InventoryID, p.Product AS Product, pi.UserID, pi.Quantity, pi.DateManufactured, pi.DateExpired,
                              pi.Status, pi.DateAdded, pi.DateModified FROM ProductInventory pi
                              INNER JOIN products p on p.ProductID=pi.ProductID WHERE pi.Status != 'Archived'";
            //where pi.DateExpired > GETDATE()
            //SELECT COUNT(DISTINCT Country) FROM Customers;

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "ProductInventory");
                    lvProductInventory.DataSource = ds;
                    lvProductInventory.DataBind();
                }
            }
        }
    }

    void GetInventory()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT DISTINCT i.ID, p.Product As Product, i.Quantity FROM Inventory i 
                                INNER JOIN ProductInventory pi on pi.ProductID=i.ProductID
                                INNER JOIN products p on p.ProductID=i.ProductID WHERE pi.DateExpired > GETDATE()";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Inventory");
                    lvInventory.DataSource = ds;
                    lvInventory.DataBind();
                }
            }
        }
    }

    public bool ProductIsExisting(string product)
    {
        bool existing = true;
        SqlConnection con = new SqlConnection(Util.GetConnection());
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = @"SELECT ProductID FROM Inventory WHERE ProductID=@ProductID";
        cmd.Parameters.AddWithValue("@ProductID", product);
        existing = cmd.ExecuteScalar() == null ? false : true;
        con.Close();
        con.Dispose();
        return existing;
    }

    protected void AddInventory(object sender, EventArgs e)
    {
        #region Step #1: Add Inventory Record in ProductInventory table
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"INSERT INTO ProductInventory VALUES (@ProductID, @UserID, @Quantity, @DateManufactured, @DateExpired, @Status, @DateAdded, @DateModified)";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@ProductID", ddlProduct.SelectedValue);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                cmd.Parameters.AddWithValue("@Quantity", Server.HtmlEncode(txtavailable.Text.Trim()));
                cmd.Parameters.AddWithValue("@DateManufactured", Server.HtmlEncode(datestart.Text.Trim()));
                cmd.Parameters.AddWithValue("@DateExpired", Server.HtmlEncode(dateend.Text.Trim()));
                cmd.Parameters.AddWithValue("@Status", Server.HtmlEncode("Active"));
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                cmd.ExecuteNonQuery();

                //start of Auditlog 
                Util.Log(Session["UserID"].ToString(), "The warhouse admin has added an inventory");
                //end of auditlog

                message1.InnerText = "Inventory Successfully Added.";

                //lahat ng textbox

            }
        }
        #endregion

        #region Step #2: Add Inventory Record in Inventory table

        bool ProductExisting = ProductIsExisting(ddlProduct.SelectedValue);

        if (!ProductExisting)
        {
            using (SqlConnection con = new SqlConnection(Util.GetConnection()))
            {
                con.Open();
                string query = @"INSERT INTO Inventory VALUES (@ProductID, @Quantity)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", ddlProduct.SelectedValue);
                    cmd.Parameters.AddWithValue("@Quantity", Server.HtmlEncode(txtavailable.Text.Trim()));
                    cmd.ExecuteNonQuery();
                    Response.Redirect("ProductInventory.aspx");

                    ddlProduct.Text = "";
                    txtavailable.Text = "";
                    datestart.Text = "";
                    dateend.Text = "";
                }
            }
        }
        else
        {
            using (SqlConnection con = new SqlConnection(Util.GetConnection()))
            {
                con.Open();
                string query = @"UPDATE Inventory SET Quantity = Quantity + @Quantity WHERE ProductID = @ProductID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", ddlProduct.SelectedValue);
                    cmd.Parameters.AddWithValue("@Quantity", Server.HtmlEncode(txtavailable.Text.Trim()));
                    cmd.ExecuteNonQuery();
                    Response.Redirect("ProductInventory.aspx");

                    ddlProduct.Text = "";
                    txtavailable.Text = "";
                    datestart.Text = "";
                    dateend.Text = "";
                }
            }
        }
        #endregion
    }

    void DeleteRecord(int ID)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"UPDATE ProductInventory SET Status = 'Archived' WHERE InventoryID=@InventoryID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@InventoryID", ID);
                cmd.ExecuteNonQuery();

                //start of Auditlog 
                Util.Log(Session["UserID"].ToString(), "The Warehouse Admin has archived an inventory");
                //end of auditlog

                Response.Redirect("ProductInventory.aspx");
            }
        }
    }

    //protected void UpdateStatus_Click(object sender, EventArgs e)
    //{
    //    using (SqlConnection con = new SqlConnection(Util.GetConnection()))
    //    {
    //        con.Open();
    //        string query = @"UPDATE ProductInventory SET Status='Expired' WHERE DateExpired > GETDATE()";

    //        using (SqlCommand cmd = new SqlCommand(query, con))
    //        {
    //            cmd.Parameters.AddWithValue("@InventoryID", Session["InventoryID"].ToString());
    //            cmd.ExecuteNonQuery();

    //            Response.Redirect("ProductInventory.aspx");
    //        }
    //    }
    //}
}