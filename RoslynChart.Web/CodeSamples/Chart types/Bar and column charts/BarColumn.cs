using RoslynChart.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.DataVisualization.Charting;

namespace RoslynChart.SampleCode.Chart_types.Bar_and_column_charts
{
    public class BarColumn : IChartExample
    {
        public Chart GetChart()
        {
// Bar
Chart chart = new Chart();
chart.Width = 500;
chart.Height = 500;
chart.Palette = ChartColorPalette.Berry;
chart.BackColor = ColorTranslator.FromHtml("#F3DFC1");
chart.BorderlineDashStyle = ChartDashStyle.Solid; 
chart.BackGradientStyle = GradientStyle.HorizontalCenter;
chart.BorderlineWidth = 2;
chart.BorderlineColor = Color.FromArgb(181, 64, 1);

chart.Titles.Add(new Title
{
    ShadowColor = Color.FromArgb(32, 0, 0, 0),
    Font = new Font("Trebuchet MS", 14.25f, FontStyle.Bold),
    ShadowOffset = 3,
    Text="Column Chart", 
    Name = "Title1", 
    ForeColor = Color.FromArgb(26, 59, 105)
});

chart.Legends.Add(new Legend
{
    TitleFont = new Font("Microsoft Sans Serif", 8f, FontStyle.Bold),
    BackColor = Color.Transparent,
    Font = new Font("Trebuchet MS", 8.25f, FontStyle.Bold),
    IsTextAutoFit = false,
    Enabled = false,
    Name="Default"
});

chart.BorderSkin.SkinStyle = BorderSkinStyle.FrameThin6;

chart.Series.Add(new Series
{
    XValueType = ChartValueType.DateTime,
    Name = "Series1",
    BorderColor = Color.FromArgb(180, 26, 59, 105)
});

chart.Series[0].Points.AddXY(36890, 32);
chart.Series[0].Points.AddXY(36891, 56);
chart.Series[0].Points.AddXY(36892, 35);
chart.Series[0].Points.AddXY(36893, 12);
chart.Series[0].Points.AddXY(36894, 35);
chart.Series[0].Points.AddXY(36895, 6);
chart.Series[0].Points.AddXY(36896, 23);

chart.Series.Add(new Series
{
    XValueType = ChartValueType.DateTime,
    Name = "Series2",
    BorderColor = Color.FromArgb(180, 26, 59, 105)
});

chart.Series[0].Points.AddXY(36890, 56);
chart.Series[0].Points.AddXY(36891, 2);
chart.Series[0].Points.AddXY(36892, 58);
chart.Series[0].Points.AddXY(36893, 59);
chart.Series[0].Points.AddXY(36894, 52);
chart.Series[0].Points.AddXY(36895, 63);
chart.Series[0].Points.AddXY(36896, 43);

chart.ChartAreas.Add(new ChartArea());

chart.ChartAreas[0].Area3DStyle.Rotation = 10;
chart.ChartAreas[0].Area3DStyle.Perspective = 10;
chart.ChartAreas[0].Area3DStyle.Inclination = 15;
chart.ChartAreas[0].Area3DStyle.IsRightAngleAxes = true;
chart.ChartAreas[0].Area3DStyle.WallWidth = 0;
chart.ChartAreas[0].Area3DStyle.IsClustered = false;

chart.ChartAreas[0].AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
chart.ChartAreas[0].AxisY.LabelAutoFitMaxFontSize = 8;
chart.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Trebuchet MS", 8.25f, FontStyle.Bold);
chart.ChartAreas[0].AxisY.LabelStyle.Format = "C0";
chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);

chart.ChartAreas[0].AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
chart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Trebuchet MS", 8.25f, FontStyle.Bold);
chart.ChartAreas[0].AxisX.LabelStyle.IsEndLabelVisible = false;
chart.ChartAreas[0].AxisX.LabelStyle.Format = "MM-dd";
chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);

// Set series chart type
chart.Series[0].ChartType = SeriesChartType.Column;
chart.Series[1].ChartType = SeriesChartType.Column;

// Set series point width
chart.Series[0]["PointWidth"] = "0.6";

// Show data points labels
chart.Series[0].IsValueShownAsLabel = true;

// Set data points label style
chart.Series[0]["BarLabelStyle"] = "Center";

// Show as 3D
chart.ChartAreas[0].Area3DStyle.Enable3D = true;

// Draw as 3D Cylinder
chart.Series[0]["DrawingStyle"] = "Cylinder";

return chart;
        }
    }
}