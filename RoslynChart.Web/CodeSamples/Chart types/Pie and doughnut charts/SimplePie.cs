using RoslynChart.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.DataVisualization.Charting;

namespace RoslynChart.SampleCode.Chart_types.Pie_and_doughnut_charts
{
    public class SimplePie : IChartExample
    {
        public Chart GetChart()
        {
// [Simple pie]
Chart chart = new Chart();

var yValues = new double[] { 0.3, 0.2, 0.5 };

chart.ChartAreas.Add(new ChartArea());
chart.Palette = ChartColorPalette.None;
chart.PaletteCustomColors = new Color[] 
{ 
	Color.FromArgb(111, 174, 21), 
	Color.FromArgb(255, 39, 40),
	Color.FromArgb(132, 135, 130)
};

chart.Height = 500;
chart.Width = 500;

chart.Series.Add(new Series());
chart.Series[0].ChartType = SeriesChartType.Pie;
chart.Series[0].IsValueShownAsLabel = true;
chart.Series[0].LabelFormat = "{P0}";
chart.Series[0].Font = new Font("Arial", 34f, FontStyle.Bold);

foreach (double y in yValues)
{
    chart.Series[0].Points.AddY(y);
}

return chart;
// End
        }
    }
}