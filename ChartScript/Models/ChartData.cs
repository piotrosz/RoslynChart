using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.DataVisualization.Charting;

namespace ChartScript.Models
{
    public class ChartData
    {
        public Chart BarChart
        {
            get { return BuildBarChart(); }
        }

        private Chart BuildBarChart()
        {
            return BindChartData();
        }

        private Chart BindChartData()
        {
            Chart chart = new Chart();
            chart.Width = 150;
            chart.Height = 150;

            chart.ChartAreas.Add(new ChartArea());
            chart.Series.Add(new Series());

            chart.Series[0].ChartType = SeriesChartType.Column;
            chart.Series[0].BackGradientStyle = GradientStyle.HorizontalCenter;
            chart.Series[0].BackSecondaryColor = System.Drawing.Color.Black;

            var xValues = new int[] { 1, 2, 3, 4 };
            var yValues = new double[] { 0.3, 0.3, 0.1, 0.7 };

            for (int i = 0; i < xValues.Length; i++)
            {
                int x = xValues[i];
                double y = yValues[i];
                int ptIdx = chart.Series[0].Points.AddXY(x, y);
                DataPoint pt = chart.Series[0].Points[ptIdx];
            }

            return chart;

        }
    }

}