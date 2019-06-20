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
            string query = @"SELECT SUM(Amount) AS Total FROM OrderDetails od JOIN Orders o ON od.OrderNo = o.OrderNo WHERE o.Status='Done'";

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
            string query = @"SELECT SUM(total) Total, DateOrdered  FROM (SELECT (SELECT Amount FROM OrderDetails WHERE OrderNo = o.OrderNo) Total,
                                DATEPART(Month, DateOrdered) DateOrdered FROM Orders o WHERE o.Status='Done') SalesPerMonth GROUP BY DateOrdered";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    Series series = Chart1.Series["Series1"];
                    while (data.Read())
                    {
                        series.Points.AddXY(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(data["DateOrdered"].ToString())), data["Total"]);
                    }
                    foreach (Series charts in Chart1.Series)
                    {
                        int i = 0;
                        foreach (DataPoint point in charts.Points)
                        {
                            point.Label = string.Format("{0:0}", point.YValues[0]);
                            if (i % 4 == 0)
                            {
                                Color colour = ColorTranslator.FromHtml("#FF0000");
                                point.Color = colour;
                            }
                            else if (i % 4 == 1)
                            {
                                Color colour = ColorTranslator.FromHtml("#2f00ff");
                                point.Color = colour;
                            }
                            else if (i % 4 == 2)
                            {
                                Color colour = ColorTranslator.FromHtml("#00f9ff");
                                point.Color = colour;
                            }
                            else
                            {
                                Color colour = ColorTranslator.FromHtml("#e3ff00");
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
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    Series series = Chart2.Series["Series1"];
                    while (data.Read())
                    {
                        series.Points.AddXY(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(data["DateOrdered"].ToString())), data["OrderCount"]);
                    }
                    foreach (Series charts in Chart2.Series)
                    {
                        int i = 0;
                        foreach (DataPoint point in charts.Points)
                        {
                            point.Label = string.Format("{0:0}", point.YValues[0]);
                            if (i % 4 == 0)
                            {
                                Color colour = ColorTranslator.FromHtml("#FF0000");
                                point.Color = colour;
                            }
                            else if (i % 4 == 1)
                            {
                                Color colour = ColorTranslator.FromHtml("#2f00ff");
                                point.Color = colour;
                            }
                            else if (i % 4 == 2)
                            {
                                Color colour = ColorTranslator.FromHtml("#00f9ff");
                                point.Color = colour;
                            }
                            else
                            {
                                Color colour = ColorTranslator.FromHtml("#e3ff00");
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