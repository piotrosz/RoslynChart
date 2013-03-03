using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.DataVisualization.Charting;

namespace ChartScript.SampleCode.Chart_types.Bar_and_column_charts
{
    public class BarColumn
    {
        public Chart GetChart()
        {
            // [Bar, column]
            Chart chart = new Chart();



            //<asp:CHART id="Chart1" runat="server" Palette="BrightPastel" BackColor="#F3DFC1" Width="412px" Height="296px" BorderlineDashStyle="Solid" BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="181, 64, 1">
            //                <titles>
            //                    <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3" Text="Column Chart" Name="Title1" ForeColor="26, 59, 105"></asp:Title>
            //                </titles>
            //                <legends>
            //                    <asp:Legend TitleFont="Microsoft Sans Serif, 8pt, style=Bold" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Enabled="False" Name="Default"></asp:Legend>
            //                </legends>
            //                <borderskin SkinStyle="Emboss"></borderskin>
            //                <series>
            //                    <asp:Series XValueType="DateTime" Name="Series1" BorderColor="180, 26, 59, 105">
            //                        <points>
            //                            <asp:DataPoint XValue="36890" YValues="32" />
            //                            <asp:DataPoint XValue="36891" YValues="56" />
            //                            <asp:DataPoint XValue="36892" YValues="35" />
            //                            <asp:DataPoint XValue="36893" YValues="12" />
            //                            <asp:DataPoint XValue="36894" YValues="35" />
            //                            <asp:DataPoint XValue="36895" YValues="6" />
            //                            <asp:DataPoint XValue="36896" YValues="23" />
            //                        </points>
            //                    </asp:Series>
            //                    <asp:Series XValueType="DateTime" Name="Series2" BorderColor="180, 26, 59, 105">
            //                        <points>
            //                            <asp:DataPoint XValue="36890" YValues="67" />
            //                            <asp:DataPoint XValue="36891" YValues="24" />
            //                            <asp:DataPoint XValue="36892" YValues="12" />
            //                            <asp:DataPoint XValue="36893" YValues="8" />
            //                            <asp:DataPoint XValue="36894" YValues="46" />
            //                            <asp:DataPoint XValue="36895" YValues="14" />
            //                            <asp:DataPoint XValue="36896" YValues="76" />
            //                        </points>
            //                    </asp:Series>
            //                </series>
            //                <chartareas>
            //                    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom">
            //                        <area3dstyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
            //                        <axisy LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8">
            //                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" Format="C0" />
            //                            <MajorGrid LineColor="64, 64, 64, 64" />
            //                        </axisy>
            //                        <axisx LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8">
            //                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" IsEndLabelVisible="False" Format="MM-dd" />
            //                            <MajorGrid LineColor="64, 64, 64, 64" />
            //                        </axisx>
            //                    </asp:ChartArea>
            //                </chartareas>
            //            </asp:CHART>

            // Set series chart type
            chart.Series["Default"].ChartType = SeriesChartType.Bar;

            // Set series point width
            chart.Series["Default"]["PointWidth"] = "0.6";

            // Show data points labels
            chart.Series["Default"].IsValueShownAsLabel = true;

            // Set data points label style
            chart.Series["Default"]["BarLabelStyle"] = "Center";

            // Show as 3D
            chart.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

            // Draw as 3D Cylinder
            chart.Series["Default"]["DrawingStyle"] = "Cylinder";


            return chart;
            // End
        }
    }
}