using RoslynChart.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.DataVisualization.Charting;

namespace RoslynChart.SampleCode
{
    public class LineChart : IChartExample
    {
        public Chart GetChart()
        {
// Line chart
Chart chart = new Chart();
chart.Palette = ChartColorPalette.EarthTones;

chart.Width = 700;
chart.Height = 500;
chart.BackColor = Color.Transparent;
chart.ChartAreas.Add(new ChartArea());
chart.ChartAreas[0].Position.Width = 75;
chart.ChartAreas[0].Position.Height = 100;

chart.ChartAreas[0].Axes[0].LineWidth = 0;
chart.ChartAreas[0].Axes[0].MajorTickMark.Size = 2;
chart.ChartAreas[0].Axes[0].LabelStyle.ForeColor = Color.White;
chart.ChartAreas[0].Axes[0].LabelStyle.Font = new Font("Arial", 11);

chart.ChartAreas[0].BackColor = Color.Transparent;
chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
chart.ChartAreas[0].AxisY.IsLabelAutoFit = false;

chart.Legends.Add(new Legend());
chart.Legends[0].BackColor = Color.Transparent;
chart.Legends[0].ForeColor = Color.White;
chart.Legends[0].Font = new Font("Arial", 11);

chart.Series.Add(new Series());
Random random = new Random();

for(int x = 0; x < 100; x++)
{
    chart.Series[0].Points.AddXY(x, random.Next(1000));
}

for (int s = 0; s < chart.Series.Count; s++)
{
    Series series = chart.Series[s];

    series.ChartType = SeriesChartType.Line;
    series.IsValueShownAsLabel = true;
    series.Font = new Font("Arial", 11, FontStyle.Bold);

    series.MarkerColor = series.Color;
    series.MarkerSize = 10;
    series.MarkerStyle = (MarkerStyle)s;

    series.BorderWidth = 3;
    series.BorderDashStyle = ChartDashStyle.Solid;
}

chart.Legends[0].Docking = Docking.Bottom;
chart.Legends[0].Alignment = StringAlignment.Center;
chart.Legends[0].LegendStyle = LegendStyle.Table;

return chart;
        }
    }
}