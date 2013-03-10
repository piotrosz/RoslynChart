using Roslyn.Scripting;
using Roslyn.Scripting.CSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.DataVisualization.Charting;

namespace RoslynChart.Core
{
    public class ChartScriptEngine
    {
        private ScriptEngine engine;
        private Session session;

        public ChartScriptEngine()
        {
            engine = new ScriptEngine();

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
        }

        public Chart CreateChart(string code)
        {
            code = "Chart CreateChart() { Chart chart = new Chart(); " + code + " return chart; }";
            session = engine.CreateSession();
            session.Execute(code);
            return (Chart) session.Execute("CreateChart()");
        }
    }
}