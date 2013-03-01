using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.DataVisualization.Charting;

namespace ChartScript.SampleCode
{
    class LineCurvesSample
    {
        private Chart GetChart()
        {
// [Line curves]
Chart chart = new Chart();
chart.Palette = ChartColorPalette.BrightPastel;
chart.BackColor = ColorTranslator.FromHtml("#F3DFC1");
chart.ImageType = ChartImageType.Png;
chart.Width = 412;
chart.Height = 296;
chart.BorderlineDashStyle = ChartDashStyle.Solid;
chart.BackGradientStyle = GradientStyle.TopBottom;
chart.BorderlineWidth = 2;
chart.BorderlineColor = Color.FromArgb(181, 64, 1);

chart.Legends.Add(new Legend
{
    Enabled = true,
    IsTextAutoFit = false,
    Name = "Default",
    BackColor = Color.Transparent,
    Font = new Font("Trebuchet MS", 8.25f, FontStyle.Bold)
});

chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;

chart.Series.Add(new Series
{
    MarkerSize = 8,
    BorderWidth = 3,
    XValueType = ChartValueType.Double,
    Name = "Series1",
    ChartType = SeriesChartType.Line,
    MarkerStyle = MarkerStyle.Circle,
    ShadowColor = Color.Black,
    BorderColor = Color.FromArgb(180, 26, 59, 105),
    Color = Color.FromArgb(220, 65, 140, 240),
    ShadowOffset = 2,
    YValueType = ChartValueType.Double
});

chart.Series.Add(new Series
{
    MarkerSize = 9,
    BorderWidth = 3, 
    XValueType = ChartValueType.Double,
    Name = "Series2",
    ChartType = SeriesChartType.Line,
    MarkerStyle = MarkerStyle.Diamond,
    ShadowColor = Color.Gray,
    BorderColor = Color.FromArgb(180, 26, 59, 105),
    Color = Color.FromArgb(220, 224, 64, 10),
    ShadowOffset = 2,
    YValueType = ChartValueType.Double
});

chart.ChartAreas.Add(new ChartArea
{
    Name = "ChartArea1",
    BorderColor = Color.FromArgb(64, 64, 64, 64),
    BorderDashStyle = ChartDashStyle.Solid,
    BackSecondaryColor = Color.White,
    BackColor = Color.OldLace,
    ShadowColor = Color.Transparent,
    BackGradientStyle = GradientStyle.TopBottom
});

chart.ChartAreas[0].Area3DStyle.Rotation = 25;
chart.ChartAreas[0].Area3DStyle.Perspective = 9;
chart.ChartAreas[0].Area3DStyle.LightStyle = LightStyle.Realistic;
chart.ChartAreas[0].Area3DStyle.Inclination = 40;
chart.ChartAreas[0].Area3DStyle.IsRightAngleAxes = false;
chart.ChartAreas[0].Area3DStyle.WallWidth = 3;
chart.ChartAreas[0].Area3DStyle.IsClustered = false;

chart.ChartAreas[0].AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
chart.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Trebuchet MS", 8.25f, FontStyle.Bold);
chart.ChartAreas[0].AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);

chart.ChartAreas[0].AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
chart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Trebuchet MS", 8.25f, FontStyle.Bold);
chart.ChartAreas[0].AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);

// Populate series with random data
Random random = new Random();
for (int pointIndex = 0; pointIndex < 10; pointIndex++)
{
    chart.Series["Series1"].Points.AddY(random.Next(45, 95));
    chart.Series["Series2"].Points.AddY(random.Next(5, 75));
}

// Set series chart type
chart.Series["Series1"].ChartType = SeriesChartType.Line;
chart.Series["Series2"].ChartType = SeriesChartType.Spline;

// Set point labels
chart.Series["Series1"].IsValueShownAsLabel = true;
chart.Series["Series2"].IsValueShownAsLabel = true;

// Enable X axis margin
chart.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;

// Enable 3D, and show data point marker lines
chart.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
chart.Series["Series1"]["ShowMarkerLines"] = "True";
chart.Series["Series2"]["ShowMarkerLines"] = "True";

return chart;
// End
        }
    }
}