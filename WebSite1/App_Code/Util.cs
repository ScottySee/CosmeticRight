using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for Util
/// </summary>
public class Util
{
    public Util()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string GetConnection()
    {
        return ConfigurationManager.ConnectionStrings["MyCon"].ConnectionString;
    }

    public static string CreateSHAHash(string Phrase)
    {
        SHA512Managed HashTool = new SHA512Managed();
        Byte[] PhraseAsByte = System.Text.Encoding.UTF8.GetBytes(string.Concat(Phrase));
        Byte[] EncryptedBytes = HashTool.ComputeHash(PhraseAsByte);
        HashTool.Clear();
        return Convert.ToBase64String(EncryptedBytes);
    }

    public static int CountData(string table)
    {
        SqlConnection con = new SqlConnection(GetConnection());
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT COUNT (*) FROM " + table;
        int count = (int)cmd.ExecuteScalar();
        con.Close();
        con.Dispose();
        return count;
    }

    public static double GetPrice(string ID)
    {
        using (SqlConnection con = new SqlConnection(GetConnection()))
        {
            con.Open();
            string query = @"SELECT Price FROM Products WHERE ProductID=@ProductID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@ProductID", ID);
                return Convert.ToDouble((decimal)cmd.ExecuteScalar());
            }
        }
    }

    public static bool IsExisting(string ID)
    {
        using (SqlConnection con = new SqlConnection(GetConnection()))
        {
            con.Open();
            string query = @"SELECT ProductID FROM OrderDetails
                WHERE OrderNo=@OrderNo AND UserID=@UserID
                AND ProductID=@ProductID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo", 0);
                cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToString());
                // HttpContext.Current.Session["userid"].ToString()
                cmd.Parameters.AddWithValue("@ProductID", ID);
                return cmd.ExecuteScalar() == null ? false : true;
            }
        }
    }

    public static void AddToCart(string ID, string quantity)
    {
        using (SqlConnection con = new SqlConnection(GetConnection()))
        {
            con.Open();
            string query = "";
            bool existingProduct = IsExisting(ID);

            if (existingProduct)
            {
                query = @"UPDATE OrderDetails SET Quantity = Quantity + @Quantity,
                    Amount = Amount + @Amount WHERE OrderNo=@OrderNo AND
                    UserID=@UserID AND ProductID=@ProductID";
            }
            else
            {
                query = @"INSERT INTO OrderDetails VALUES (@OrderNo, @UserID,
                    @ProductID, @Quantity, @Amount)";
            }

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                string UserID = HttpContext.Current.Session["UserID"].ToString();
                cmd.Parameters.AddWithValue("@OrderNo", 0);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                // HttpContext.Current.Session["userid"].ToString()
                cmd.Parameters.AddWithValue("@ProductID", ID);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@Amount",
                    int.Parse(quantity) * GetPrice(ID));
                cmd.ExecuteNonQuery();

            }
        }
    }
}