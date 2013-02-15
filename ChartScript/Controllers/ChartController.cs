using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using System.Drawing;
using Roslyn.Scripting.CSharp;
using ChartScript.Infrastructure;

namespace ChartScript.Controllers
{
    public class ChartController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateChart(string code)
        {
            var engine = new ChartScriptEngine();

            var session = engine.CreateSession();

            string guid = Guid.NewGuid().ToString();
            try
            {
                session.Execute(code);
                var resultingChart = (Chart)session.Execute("CreateChart()");

                Session[guid] = ReturnChart(resultingChart);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

            return Json(guid);
        }

        [HttpGet]
        public FileContentResult ReturnChart(string guid)
        {
            return (FileContentResult)Session[guid];
        }

        private FileContentResult ReturnChart(Chart chart)
        {
            var stream = new MemoryStream();
            chart.SaveImage(stream, ChartImageFormat.Png);
            stream.Position = 0;
            return File(stream.GetBuffer(), "image/png");
        }

    }
}
