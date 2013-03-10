using RoslynChart.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.DataVisualization.Charting;

namespace RoslynChart.SampleCode.Chart_types.Bar_and_column_charts
{
    public class Stacked : IChartExample
    {
        public Chart GetChart()
        {
// [Stacked]
Chart chart = new Chart();
chart.Height = 296;
chart.Width = 412;
chart.BackColor = ColorTranslator.FromHtml("#D3DFF0");
chart.Palette = ChartColorPalette.EarthTones;
chart.BorderlineDashStyle = ChartDashStyle.DashDotDot;
chart.BackGradientStyle = GradientStyle.DiagonalRight;
chart.BorderlineWidth = 2;
chart.BorderlineColor = Color.FromArgb(26, 59, 105);

chart.Legends.Add(new Legend { 
    TitleFont = new Font("Microsoft Sans Serif", 8f, FontStyle.Bold),
    BackColor = Color.Transparent,
    Font = new Font("Trebuchet MS", 8.25f, FontStyle.Bold),
    IsTextAutoFit = false,
    Enabled = false,
    Name = "Default"
});

chart.BorderSkin.SkinStyle = BorderSkinStyle.FrameThin4;

chart.Series.Add(new Series
{
    Name = "Series1",
    ChartType = SeriesChartType.StackedArea100,
    BorderColor = Color.FromArgb(180, 26, 59, 105),
    Color = Color.FromArgb(220, 65, 140, 240)
});

chart.Series.Add(new Series
{
    Name = "Series2", 
    ChartType = SeriesChartType.StackedArea100,
    BorderColor = Color.FromArgb(180, 26, 59, 105),
    Color = Color.FromArgb(220, 252, 180, 65)
});

chart.Series.Add(new Series
{
    Name = "Series3",
    ChartType = SeriesChartType.StackedArea100,
    BorderColor = Color.FromArgb(180, 26, 59, 105),
    Color = Color.Fuchsia
});

chart.Series.Add(new Series
{
    Name = "Series4",
    ChartType = SeriesChartType.StackedArea100,
    BorderColor = Color.FromArgb(180, 26, 59, 105),
    Color = Color.FromArgb(220, 5, 100, 146)
});

chart.ChartAreas.Add(new ChartArea
{
    Name = "ChartArea1",
    BorderColor = Color.FromArgb(64, 64, 64, 64),
    BorderDashStyle = ChartDashStyle.Solid,
    BackSecondaryColor = Color.Transparent,
    BackColor = Color.FromArgb(64, 165, 191, 228),
    ShadowColor = Color.Transparent,
    BackGradientStyle = GradientStyle.LeftRight,
});

chart.ChartAreas[0].Area3DStyle.Rotation = 40;
chart.ChartAreas[0].Area3DStyle.Inclination = 15;
chart.ChartAreas[0].Area3DStyle.WallWidth = 2;

chart.ChartAreas[0].Position.X = 3;
chart.ChartAreas[0].Position.Y = 3;
chart.ChartAreas[0].Position.Height = 92;
chart.ChartAreas[0].Position.Width = 92;

chart.ChartAreas[0].AxisX.LineColor = Color.Turquoise;
chart.ChartAreas[0].AxisX.LabelAutoFitMaxFontSize = 8;

chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Red;

chart.ChartAreas[0].AxisY.LineColor = Color.YellowGreen;
chart.ChartAreas[0].AxisY.LabelAutoFitMaxFontSize = 8;

// Populate series data
Random random = new Random();
for (int pointIndex = 0; pointIndex < 10; pointIndex++)
{
    chart.Series["Series1"].Points.AddY(random.Next(45, 95));
    chart.Series["Series2"].Points.AddY(random.Next(0, 40));
    chart.Series["Series3"].Points.AddY(random.Next(2, 95));
    chart.Series["Series4"].Points.AddY(random.Next(100, 200));
}

// Set chart type
chart.Series["Series1"].ChartType = SeriesChartType.StackedArea100;

// Show point labels
chart.Series["Series1"].IsValueShownAsLabel = true;

// Disable X axis margin
chart.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = false;

// Enable 3D
chart.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

// Set the first two series to be grouped into Group1
chart.Series[0]["StackedGroupName"] = "Group1";
chart.Series[1]["StackedGroupName"] = "Group1";

// Set the last two series to be grouped into Group2
chart.Series[2]["StackedGroupName"] = "Group2";
chart.Series[3]["StackedGroupName"] = "Group2";

return chart;
// End
        }
    }
}