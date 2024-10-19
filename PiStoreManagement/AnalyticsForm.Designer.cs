namespace PiStoreManagement
{
    partial class AnalyticsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lineChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pieChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cbSelection = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.barChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.lineChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barChart)).BeginInit();
            this.SuspendLayout();
            // 
            // lineChart
            // 
            chartArea4.Name = "ChartArea1";
            this.lineChart.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.lineChart.Legends.Add(legend4);
            this.lineChart.Location = new System.Drawing.Point(224, 109);
            this.lineChart.Name = "lineChart";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.lineChart.Series.Add(series4);
            this.lineChart.Size = new System.Drawing.Size(1288, 357);
            this.lineChart.TabIndex = 0;
            this.lineChart.Text = "chart1";
            // 
            // pieChart
            // 
            chartArea5.Name = "ChartArea1";
            this.pieChart.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.pieChart.Legends.Add(legend5);
            this.pieChart.Location = new System.Drawing.Point(953, 494);
            this.pieChart.Name = "pieChart";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.pieChart.Series.Add(series5);
            this.pieChart.Size = new System.Drawing.Size(559, 357);
            this.pieChart.TabIndex = 2;
            this.pieChart.Text = "chart1";
            // 
            // cbSelection
            // 
            this.cbSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSelection.FormattingEnabled = true;
            this.cbSelection.Location = new System.Drawing.Point(1312, 52);
            this.cbSelection.Name = "cbSelection";
            this.cbSelection.Size = new System.Drawing.Size(200, 33);
            this.cbSelection.TabIndex = 75;
            this.cbSelection.SelectedIndexChanged += new System.EventHandler(this.cbSelection_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Bright", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label2.Location = new System.Drawing.Point(220, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 20);
            this.label2.TabIndex = 76;
            this.label2.Text = "Income Analytics";
            // 
            // barChart
            // 
            chartArea6.Name = "ChartArea1";
            this.barChart.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.barChart.Legends.Add(legend6);
            this.barChart.Location = new System.Drawing.Point(224, 494);
            this.barChart.Name = "barChart";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.barChart.Series.Add(series6);
            this.barChart.Size = new System.Drawing.Size(703, 357);
            this.barChart.TabIndex = 77;
            this.barChart.Text = "chart2";
            // 
            // AnalyticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1645, 931);
            this.Controls.Add(this.barChart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbSelection);
            this.Controls.Add(this.pieChart);
            this.Controls.Add(this.lineChart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AnalyticsForm";
            this.Text = "AnalyticsForm";
            ((System.ComponentModel.ISupportInitialize)(this.lineChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart lineChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart pieChart;
        private System.Windows.Forms.ComboBox cbSelection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataVisualization.Charting.Chart barChart;
    }
}