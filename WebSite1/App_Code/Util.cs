using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.IO;

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

    //for encryption
    public static string EnryptString(string strEncrypted)
    {
	    try
	    {
		    string EncryptionKey = ConfihurationManager.AppSettings["encQRKey"].ToString();
		    byte[] clearBytes = Encoding.Unicode.GetBytes(strEncrypted);
            using (Aes qrencryptor = Aes.Create())
            {
                Rfc2898DeriveBytes qrdb = new Rfc2898DeriveBytes(EncrytionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                qrencryptor.Key = qrdb.GetBytes(32);
                qrencryptor.IV = qrdb.GetBytes(16);
                using (MemoryStream qrms = new MemoryStream())
                {
                    using (CryptoStream qrcs = new CryptoStream(qrms, qrencryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        qrcs.Write(clearBytes, 0, clearBytes.Length);
                        qrcs.Close();
                    }
                    strEncrypted = Convert.ToBase64String(qrms.ToArray());
                }
            }
            return paramval;
	    }
        catch (Exception)
        {
            return "NotExistingPage.aspx";
        }
    }

    //for decryption
    public static string DecryptString(string strDecrypted)
    {
        try
        {
            string EncryptionKey = ConfihurationManager.AppSettings["encQRKey"].ToString();
            qrencrypt = qrencrypt.Replace(" ", "+");
            byte[] qrencryptBytes = Encoding.Unicode.GetBytes(strDecrypted);
            using (Aes qrencryptor = Aes.Create())
            {
                Rfc2898DeriveBytes qrdb = new Rfc2898DeriveBytes(EncrytionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                qrencryptor.Key = qrdb.GetBytes(32);
                qrencryptor.IV = qrdb.GetBytes(16);
                using (MemoryStream qrms = new MemoryStream())
                {
                    using (CryptoStream qrcs = new CryptoStream(qrms, qrencryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        qrcs.Write(clearBytes, 0, clearBytes.Length);
                        qrcs.Close();
                    }
                    strEncrypted = Convert.ToBase64String(qrms.ToArray());
                }
            }
            return paramval;
        }
        catch (Exception)
        {
            return "NotExistingPage.aspx";
        }
    }

    //public static string DecodeFrom64(string encodedData)
    //{
    //    System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
    //    System.Text.Decoder utf8Decode = encoder.GetDecoder();
    //    byte[] todecode_byte = Convert.FromBase64String(encodedData);
    //    int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
    //    char[] decoded_char = new char[charCount];
    //    utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
    //    string result = new String(decoded_char);
    //    return result;
    //}

    //for sending activation email (hindi pa tapos)
    public static void SendActivationEmail(string recipient, string name, 
        string usertype, string body)
    {
        MailMessage mm = new MailMessage();
        mm.From = new MailAddress("lifelineambulancerescue@gmail.com");
        mm.To.Add(recipient);
        mm.Subject = "Account Activation";
        mm.Body = body;
        mm.IsBodyHtml = true;

        SmtpClient client = new SmtpClient();
        client.EnableSsl = true;
        client.UseDefaultCredentials = true;
        NetworkCredential cred = new NetworkCredential("lifelineambulancerescue@gmail.com", "swantonbomb");
        client.Host = "smtp.gmail.com";
        client.Port = 587;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Credentials = cred;
        client.Send(mm);
    }

    //public static int CountData(string table)
    //{
    //    SqlConnection con = new SqlConnection(GetConnection());
    //    con.Open();
    //    SqlCommand cmd = new SqlCommand();
    //    cmd.Connection = con;
    //    cmd.CommandText = "SELECT COUNT (*) FROM " + table;
    //    int count = (int)cmd.ExecuteScalar();
    //    con.Close();
    //    con.Dispose();
    //    return count;
    //}

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
                WHERE OrderNo IS NULL AND UserID=@UserID
                AND ProductID=@ProductID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                //cmd.Parameters.AddWithValue("@OrderNo", 0);
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
                    Amount = Amount + @Amount WHERE OrderNo IS NULL AND
                    UserID=@UserID AND ProductID=@ProductID";
            }
            else
            {
                query = @"INSERT INTO OrderDetails (UserID,
                    ProductID, Quantity, Amount) VALUES (@UserID,
                    @ProductID, @Quantity, @Amount)";
            }

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                string UserID = HttpContext.Current.Session["UserID"].ToString();
                //cmd.Parameters.AddWithValue("@OrderNo", null);
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

    public static void Log(string UserID, string activity)
    {
        using (SqlConnection con = new SqlConnection(GetConnection()))
        {
            con.Open();
            string query = @"INSERT INTO Logs (UserID, LogTime, Activity)
                                VALUES (@UserID, @LogTime, @Activity)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@LogTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@Activity", CreateSHAHash(activity));

                cmd.ExecuteNonQuery();
            }
        }
    }
}