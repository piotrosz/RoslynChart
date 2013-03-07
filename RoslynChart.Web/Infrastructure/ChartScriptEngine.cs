using Roslyn.Scripting;
using Roslyn.Scripting.CSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.DataVisualization.Charting;

namespace ChartScript.Infrastructure
{
    // ScriptEngine wrapper
    public class ChartScriptEngine
    {
        private ScriptEngine engine;

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

        public Session CreateSession()
        {
            return engine.CreateSession();
        }
    }
}