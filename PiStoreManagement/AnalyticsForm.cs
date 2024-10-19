using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BUS;

namespace PiStoreManagement
{
    public partial class AnalyticsForm : Form
    {
        private BillBUS billBUS = new BillBUS();
        private List<string> incomeData = new List<string>();
        private string query = "SELECT CAST(BillDate AS DATE) AS Text, SUM(TotalPrice) AS TotalIncome FROM Bill WHERE BillDate >= DATEADD(DAY, -7, GETDATE()) GROUP BY CAST(BillDate AS DATE) ORDER BY Text";

        public AnalyticsForm()
        {
            InitializeComponent();
            addComboBoxItems();
            cbSelection.SelectedIndex = 0;
            loadIncomeDataChart(query, cbSelection.Text);
        }

        private void addComboBoxItems()
        {
            cbSelection.Items.Add("Last 7 days");
            cbSelection.Items.Add("Day");
            cbSelection.Items.Add("Week");
            cbSelection.Items.Add("Month");
            cbSelection.Items.Add("Year");
        }

        private void loadIncomeDataChart(string query, string period)
        {
            incomeData = billBUS.getBillListByTimePeriod(query);

            lineChart.Series.Clear();
            barChart.Series.Clear();
            pieChart.Series.Clear();

            Series lineSeries = new Series("Income");
            lineSeries.ChartType = SeriesChartType.Spline;
            lineSeries.BorderWidth = 3;
            lineChart.Series.Add(lineSeries);

            Series barSerires = new Series("Income");
            barSerires.ChartType = SeriesChartType.Column;
            barSerires.Color = Color.ForestGreen;
            barChart.Series.Add(barSerires);

            Series pieSeries = new Series("Income");
            pieSeries.ChartType = SeriesChartType.Pie;
            pieChart.Series.Add(pieSeries);

            string xAxisTitle = "";

            foreach (string data in incomeData)
            {
                string[] dataPart = data.Split('-');

                if (dataPart.Length == 2)
                {
                    string textPart = dataPart[0];
                    double totalIncome = double.Parse(dataPart[1]);

                    // date value
                    if (period == "Last 7 days" || period == "Day")
                    {
                        if (DateTime.TryParse(textPart, out DateTime dateValue))
                        {
                            lineSeries.Points.AddXY(dateValue.ToString("MM/dd/yyyy"), totalIncome);
                            barSerires.Points.AddXY(dateValue.ToString("MM/dd/yyyy"), totalIncome);
                            pieSeries.Points.AddXY(dateValue.ToString("MM/dd/yyyy"), totalIncome);

                            xAxisTitle = "Date";
                        }
                    }
                    
                    // week
                    if (period == "Week")
                    {
                        int dayOfWeek;
                        if (int.TryParse(textPart, out dayOfWeek))
                        {
                            string dayName = Enum.GetName(typeof(DayOfWeek), dayOfWeek - 1);
                            lineSeries.Points.AddXY(dayName, totalIncome);
                            barSerires.Points.AddXY(dayName, totalIncome);
                            pieSeries.Points.AddXY(dayName, totalIncome);
                            xAxisTitle = "Day of the week";
                        }
                    }

                    // month
                    if (period == "Month")
                    {
                        int dayOfMonth;
                        if (int.TryParse(textPart, out dayOfMonth))
                        {
                            lineSeries.Points.AddXY($"Day {dayOfMonth}", totalIncome);
                            barSerires.Points.AddXY($"Day {dayOfMonth}", totalIncome);
                            pieSeries.Points.AddXY($"Day {dayOfMonth}", totalIncome);
                            xAxisTitle = "Day of the Month";
                        }
                    }

                    // year
                    if (period == "Year")
                    {
                        int monthOfYear;
                        if (int.TryParse(textPart, out monthOfYear))
                        {
                            string monthName = new DateTime(1, monthOfYear, 1).ToString("MMMM");
                            lineSeries.Points.AddXY(monthName, totalIncome);
                            barSerires.Points.AddXY(monthName, totalIncome);
                            pieSeries.Points.AddXY(monthName, totalIncome);
                            xAxisTitle = "Month of the Year";
                        }
                    }

                    lineChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                    lineChart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                }
            }

            //lineChart.ChartAreas[0].AxisY.Interval = (lineChart.ChartAreas[0].AxisY.Maximum - lineChart.ChartAreas[0].AxisY.Minimum) / 10; 

            lineChart.ChartAreas[0].AxisX.Title = xAxisTitle;
            lineChart.ChartAreas[0].AxisY.Title = "Income";
            lineChart.Titles.Clear();
            lineChart.Titles.Add($"{period} Income Report");

            // realign the x-axis of bar chart
            barChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            barChart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            // set pie chart label style
            pieSeries.Label = "#VALX: #VALY"; 
            pieChart.Titles.Clear();
            pieChart.Titles.Add($"{period} Income Distribution");
        }

        private void cbSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedPeriod = cbSelection.SelectedItem.ToString();
            
            switch (selectedPeriod)
            {
                case "Last 7 days":
                    query = "SELECT CAST(BillDate AS DATE) AS Text, SUM(TotalPrice) AS TotalIncome FROM Bill WHERE BillDate >= DATEADD(DAY, -7, GETDATE()) GROUP BY CAST(BillDate AS DATE) ORDER BY Text";
                    break;
                case "Day":
                    query = "SELECT CAST(BillDate AS DATE) AS Text, SUM(TotalPrice) AS TotalIncome FROM Bill WHERE CAST(BillDate AS DATE) = CAST(GETDATE() AS DATE) GROUP BY CAST(BillDate AS DATE)";
                    break;
                case "Week":
                    query = "SELECT DATEPART(WEEKDAY, BillDate) AS Text, SUM(TotalPrice) AS TotalIncome FROM Bill WHERE DATEDIFF(WEEK, 0, BillDate) = DATEDIFF(WEEK, 0, GETDATE()) GROUP BY DATEPART(WEEKDAY, BillDate) ORDER BY Text;";
                    break;
                case "Month":
                    query = "SELECT DAY(BillDate) AS Text, SUM(TotalPrice) AS TotalIncome FROM Bill WHERE MONTH(BillDate) = MONTH(GETDATE()) AND YEAR(BillDate) = YEAR(GETDATE()) GROUP BY DAY(BillDate) ORDER BY Text;";
                    break;
                case "Year":
                    query = "SELECT MONTH(BillDate) AS Text, SUM(TotalPrice) AS TotalIncome FROM Bill WHERE YEAR(BillDate) = YEAR(GETDATE()) GROUP BY MONTH(BillDate) ORDER BY Text;";
                    break;
            }

            loadIncomeDataChart(query, cbSelection.Text);
        }
    }
}
