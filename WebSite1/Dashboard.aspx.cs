using FusionCharts.Charts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        ////store label-value pair
        //var dataValuePair = new List<KeyValuePair<string, double>>();

        //dataValuePair.Add(new KeyValuePair<string, double>("Venezuela", 290));
        //dataValuePair.Add(new KeyValuePair<string, double>("Saudi", 260));
        //dataValuePair.Add(new KeyValuePair<string, double>("Canada", 180));
        //dataValuePair.Add(new KeyValuePair<string, double>("Iran", 140));
        //dataValuePair.Add(new KeyValuePair<string, double>("Russia", 115));
        //dataValuePair.Add(new KeyValuePair<string, double>("UAE", 100));
        //dataValuePair.Add(new KeyValuePair<string, double>("US", 30));
        //dataValuePair.Add(new KeyValuePair<string, double>("China", 30));

        //StringBuilder jsonData = new StringBuilder();
        //StringBuilder data = new StringBuilder();
        //// store chart config name-config value pair

        //Dictionary<string, string> chartConfig = new Dictionary<string, string>();
        //chartConfig.Add("caption", "Countries With Most Oil Reserves [2017-18]");
        //chartConfig.Add("subCaption", "In MMbbl = One Million barrels");
        //chartConfig.Add("xAxisName", "Country");
        //chartConfig.Add("yAxisName", "Reserves (MMbbl)");
        //chartConfig.Add("numberSuffix", "k");
        //chartConfig.Add("theme", "fusion");

        //// json data to use as chart data source
        //jsonData.Append("{'chart':{");
        //foreach (var config in chartConfig)
        //{
        //    jsonData.AppendFormat("'{0}':'{1}',", config.Key, config.Value);
        //}
        //jsonData.Replace(",", "},", jsonData.Length - 1, 1);

        //// build  data object from label-value pair
        //data.Append("'data':[");

        //foreach (KeyValuePair<string, double> pair in dataValuePair)
        //{
        //    data.AppendFormat("{{'label':'{0}','value':'{1}'}},", pair.Key, pair.Value);
        //}
        //data.Replace(",", "]", data.Length - 1, 1);

        //jsonData.Append(data.ToString());
        //jsonData.Append("}");
        ////Create chart instance
        //// charttype, chartID, width, height, data format, data


        //Chart MyFirstChart = new Chart("column2d", "first_chart", "800", "550", "json", jsonData.ToString());
        //// render chart
        //Literal1.Text = MyFirstChart.Render();
        if (!IsPostBack)
        {
            GetWebsiteCount();
            GetOrderCount();
            GetUserCount();
            GetSales();
        }
    }

    void GetWebsiteCount()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"select count(*) as Count from WebsiteVisit";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            count.Text = data["Count"].ToString();
                        }
                    }
                }
            }
        }
    }

    void GetOrderCount()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"select count(*) as Count from Orders";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            count1.Text = data["Count"].ToString();
                        }
                    }
                }
            }
        }
    }

    void GetUserCount()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"select count(*) as Count from Users";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            count2.Text = data["Count"].ToString();
                        }
                    }
                }
            }
        }
    }

    void GetSales()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            //string query = @"SELECT SUM(Amount) AS Total FROM OrderDetails";

            string query = @"Select (SELECT SUM(Amount) FROM OrderDetails WHERE OrderNo = o.OrderNo) AS Total From Orders o Where o.Status='Done'";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            sales.Text = data["Total"].ToString();
                        }
                    }
                }
            }
        }
    }
}