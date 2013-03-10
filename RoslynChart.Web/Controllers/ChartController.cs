using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using System.Drawing;
using Roslyn.Scripting.CSharp;
using RoslynChart.Models;
using RoslynChart.Core;

namespace RoslynChart.Controllers
{
    public class ChartController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gets code samples from disk.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetCodeSamples()
        {
            string rootDir = HttpContext.Request.MapPath("~/CodeSamples");
            var importer = new CodeSamplesImporter(rootDir);

            var model = importer.Import();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(string code)
        {
            var engine = new ChartScriptEngine();
            var result = new CreateChartResult();
            string guid = Guid.NewGuid().ToString();

            try
            {
                var chart = engine.CreateChart(code);
                Session[guid] = ReturnChart(chart);
            }
            catch (Exception ex)
            {
                return Json(new CreateChartResult{
                    Message = ex.Message
                });
            }

            return Json(new CreateChartResult { Guid = guid, Message = "Success" });
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
