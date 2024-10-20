using System;
using System.Collections.Generic;
using System.Drawing;
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

            Series lineSeries = new Series("Income") { ChartType = SeriesChartType.Spline };
            Series barSerires = new Series("Income") { ChartType = SeriesChartType.Column };
            Series pieSeries = new Series("Income") { ChartType = SeriesChartType.Pie };

            lineSeries.BorderWidth = 3;
            barSerires.Color = Color.ForestGreen;

            lineChart.Series.Add(lineSeries);
            barChart.Series.Add(barSerires);
            pieChart.Series.Add(pieSeries);

            string xAxisTitle = "";
            decimal maxIncome = 0;

            foreach (string data in incomeData)
            {
                string[] dataPart = data.Split('-');

                if (dataPart.Length == 2)
                {
                    string textPart = dataPart[0];
                    double totalIncome = double.Parse(dataPart[1]);

                    maxIncome = Math.Max(maxIncome, (decimal)totalIncome);

                    // last 7 days
                    if (period == "Last 7 days")
                    {
                        if (DateTime.TryParse(textPart, out DateTime dateValue))
                        {
                            lineSeries.Points.AddXY(dateValue.ToString("MM/dd"), totalIncome);
                            barSerires.Points.AddXY(dateValue.ToString("MM/dd"), totalIncome);
                            pieSeries.Points.AddXY(dateValue.ToString("MM/dd"), totalIncome);

                            xAxisTitle = "Date";
                        }
                    }
                    
                    // day
                    if (period == "Day")
                    {
                        if (DateTime.TryParse(textPart, out DateTime dateValue))
                        {
                            lineSeries.Points.AddXY(dateValue.ToString("hh:mm"), totalIncome);
                            barSerires.Points.AddXY(dateValue.ToString("hh:mm"), totalIncome);
                            pieSeries.Points.AddXY(dateValue.ToString("hh:mm"), totalIncome);

                            xAxisTitle = "Time";
                        }
                    }

                    // week
                    if (period == "Week")
                    {
                        int dayOfWeek = 1;
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

            // ceiling the total income of y-axis
            lineChart.ChartAreas[0].AxisY.Maximum = Math.Ceiling((double)maxIncome * 1.1);
            barChart.ChartAreas[0].AxisY.Maximum = Math.Ceiling((double)maxIncome * 1.1);
            pieChart.ChartAreas[0].AxisY.Maximum = Math.Ceiling((double)maxIncome * 1.1); 

            lineChart.ChartAreas[0].AxisX.Title = xAxisTitle;
            barChart.ChartAreas[0].AxisX.Title = xAxisTitle;

            lineChart.ChartAreas[0].AxisY.Title = "Income";

            pieChart.Titles.Clear();
            lineChart.Titles.Clear();
            barChart.Titles.Clear();

            lineChart.Titles.Add($"{period} Income Report");
            barChart.Titles.Add($"{period} Income Report");

            // realign the x-axis of bar chart
            lineChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            lineChart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
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
                    query = "SELECT CAST(BillDate AS DATETIME) AS Text, TotalPrice AS TotalIncome FROM Bill WHERE CAST(BillDate AS DATE) = CAST(GETDATE() AS DATE);";
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
