using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using ChartScript.Models;
using System.IO;
using System.Drawing;
using Roslyn.Scripting.CSharp;

namespace ChartScript.Controllers
{
    public class ChartController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public FileContentResult DrawChart()
        {
            var viewModel = new ChartData();
            Chart chart = viewModel.BarChart;
            return ReturnChart(chart);
        }

        [HttpPost]
        public FileContentResult DrawChart(string code)
        {
            var engine = new ScriptEngine();
            
            new[]  
                {    
                    typeof(System.ComponentModel.Component).Assembly,
                    typeof (int).Assembly,
                    typeof (HttpContext).Assembly,
                    typeof (Color).Assembly,
                    typeof (Chart).Assembly,  
                    typeof (IEnumerable<>).Assembly,  
                    typeof (IQueryable).Assembly
                }.ToList().ForEach(asm => engine.AddReference(asm));

            new[]  
                {  
                   "System", 
                   "System.Web",
                   "System.Drawing",
                   "System.Linq",   
                   "System.Collections",  
                   "System.Collections.Generic",
                   "System.Web.UI.DataVisualization.Charting"
                }.ToList().ForEach(ns => engine.ImportNamespace(ns));

            var session = engine.CreateSession();

            session.Execute(code);
            var resultingChart = (Chart) session.Execute("CreateChart()");

            return ReturnChart(resultingChart);
        }

        private FileContentResult ReturnChart(Chart chart)
        {
            MemoryStream ms = new MemoryStream();
            chart.SaveImage(ms, ChartImageFormat.Png);
            ms.Position = 0;
            return File(ms.GetBuffer(), "image/png");
        }

    }
}
