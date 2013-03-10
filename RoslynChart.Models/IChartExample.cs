using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.DataVisualization.Charting;

namespace RoslynChart.Models
{
    public interface IChartExample
    {
        Chart GetChart();
    }
}