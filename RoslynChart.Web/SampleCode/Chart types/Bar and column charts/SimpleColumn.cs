using ChartScript.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.DataVisualization.Charting;

namespace ChartScript.SampleCode.Chart_types.Bar_and_column_charts
{
    public class SimpleColumn : IChartExample
    {
        public Chart GetChart()
        {
// [Simple column]
var values = new double[] { 9, 30, 5 };
var labels = new string[] { "A", "B", "C" };

Chart chart = new Chart();
chart.ChartAreas.Add(new ChartArea());
chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
chart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
chart.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
chart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 18f, FontStyle.Bold);

chart.ChartAreas[0].AxisX.LineWidth = 0;
chart.ChartAreas[0].AxisY.Enabled = AxisEnabled.False;

chart.Height = 500;
chart.Width = 500;

chart.Series.Add(new Series());
chart.Series[0].ChartType = SeriesChartType.Column;
chart.Series[0].Color = Color.FromArgb(0, 105, 209);
chart.Series[0].IsValueShownAsLabel = true;
chart.Series[0].Font = new Font("Arial", 34f, FontStyle.Bold);

for (int i = 0; i < values.Length; i++)
{
    chart.Series[0].Points.AddXY(labels[i], values[i]);
}

return chart;
// End
        }
    }
}