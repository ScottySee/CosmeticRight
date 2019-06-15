using FusionCharts.Charts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Globalization;
using System.Drawing;

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

        //Color[] barColors = new Color[9]
        //{
        //      Color.Purple,
        //      Color.SteelBlue,
        //      Color.Aqua,
        //      Color.Yellow,
        //      Color.Navy,
        //      Color.Green,
        //      Color.Blue,
        //      Color.Red,
        //      Color.AliceBlue
        //};

        ////Assingning the color to bars
        //foreach (ChartSeriesItem item in Chart2.Series[0].Items)
        //{
        //    item.Appearance.FillStyle.MainColor = barColors[item.Index];
        //}

        if (!IsPostBack)
        {
            GetWebsiteCount();
            GetOrderCount();
            GetUserCount();
            GetSales();
            GetChartDataSales();
            GetChartDataOrders();
        }
    }

    void GetWebsiteCount()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT count(*) as Count FROM WebsiteVisit";

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
            string query = @"SELECT count(*) as Count FROM Orders WHERE Status='Done'";

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
            string query = @"SELECT count(*) as Count FROM Users";

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

            string query = @"SELECT SUM(Amount) AS Total FROM OrderDetails od JOIN Orders o ON od.OrderNo = o.OrderNo WHERE o.Status='Done'";

            //string query = @"Select DISTINCT o.OrderNo, (SELECT u.Lastname + ' ' + u.Firstname AS Customer FROM Users u WHERE od.UserID=u.UserID) as Username, (SELECT SUM(Amount) FROM OrderDetails WHERE OrderNo = o.OrderNo) AS Total,
            //        o.DateOrdered From Orders o, OrderDetails od WHERE o.Status='Done'";

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

    private void GetChartDataSales()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT SUM(total) Total, DateOrdered  FROM (SELECT (SELECT Amount FROM OrderDetails WHERE OrderNo = o.OrderNo) Total, DATEPART                    (Month, DateOrdered) DateOrdered FROM Orders o WHERE o.Status='Done') SalesPerMonth GROUP BY DateOrdered";

            //string query = @"Select SUM(Amount) AS Total FROM OrderDetails od JOIN Orders o ON od.OrderNo = o.OrderNo WHERE o.Status = 'Done' GROUP BY DATEPART(MONTH, DateOrdered)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    Series series = Chart1.Series["Series1"];
                    while (data.Read())
                    {
                        series.Points.AddXY(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(data["DateOrdered"].ToString())), data["Total"]);

                        //Convert.ToDateTime(data["DateStart"]).ToString("MM/dd/yyyy");
                    }
                    foreach (Series charts in Chart1.Series)
                    {
                        int i = 0;
                        foreach (DataPoint point in charts.Points)
                        {
                            point.Label = string.Format("{0:0}", point.YValues[0]);
                            if (i % 3 == 0)
                            {
                                Color colour = ColorTranslator.FromHtml("#CD5C5C");
                                point.Color = colour;
                            }
                            else if (i % 3 == 1)
                            {
                                Color colour = ColorTranslator.FromHtml("#bfbfbf");
                                point.Color = colour;
                            }
                            else
                            {
                                Color colour = ColorTranslator.FromHtml("#5ccd80");
                                point.Color = colour;
                            }

                            i++;
                        }
                    }
                }
            }
        }
    }

    private void GetChartDataOrders()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string query = @"SELECT Count(OrderNo) OrderCount, DateOrdered FROM (SELECT OrderNo, DATEPART(Month, DateOrdered) DateOrdered
                                 FROM (SELECT * FROM Orders o where o.Status = 'Done') Table1) Table2 GROUP BY DateOrdered";

            //string query = @"Select SUM(Amount) AS Total FROM OrderDetails od JOIN Orders o ON od.OrderNo = o.OrderNo WHERE o.Status = 'Done' GROUP BY DATEPART(MONTH, DateOrdered)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    Series series = Chart2.Series["Series1"];
                    while (data.Read())
                    {
                        series.Points.AddXY(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(data["DateOrdered"].ToString())), data["OrderCount"]);

                        //Convert.ToDateTime(data["DateStart"]).ToString("MM/dd/yyyy");
                    }
                    foreach (Series charts in Chart2.Series)
                    {
                        int i = 0;
                        foreach (DataPoint point in charts.Points)
                        {
                            point.Label = string.Format("{0:0}", point.YValues[0]);
                            if (i % 3 == 0)
                            {
                                Color colour = ColorTranslator.FromHtml("#CD5C5C");
                                point.Color = colour;
                            }
                            else if (i % 3 == 1)
                            {
                                Color colour = ColorTranslator.FromHtml("#bfbfbf");
                                point.Color = colour;
                            }
                            else
                            {
                                Color colour = ColorTranslator.FromHtml("#5ccd80");
                                point.Color = colour;
                            }

                            i++;
                        }
                    }
                }
            }
        }
    }


    protected void Chart2_Customize(object sender, EventArgs e)
    {
        try
        {
            Chart1.BackColor = System.Drawing.Color.LightGray;


        }
        catch (Exception)
        { }
    }

    protected void Chart1_Load(object sender, EventArgs e)
    {
        try
        {

            Series series1 = new Series("Spline");

            series1.ChartType = SeriesChartType.Column;

            series1.Points[0].Color = System.Drawing.Color.Red;
            series1.Points[1].Color = System.Drawing.Color.Green;
            series1.Points[2].Color = System.Drawing.Color.Blue;
            series1.Color = System.Drawing.Color.Green;

            Chart1.Series.Add(series1);

        }
        catch (Exception)
        { }
    }
}