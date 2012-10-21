using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
using Roslyn.Services;
using Roslyn.Services.CSharp;
using Roslyn.Scripting.CSharp;

namespace ScriptingIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                var engine = new ScriptEngine();

                var session = engine.CreateSession();

                session.Execute(@"var a = 42;");
                session.Execute(@"System.Console.WriteLine(a);");
            }

            {
                // Interacting with host application
                var hostObject = new HostObject();
                var engine = new ScriptEngine();

                //Let us use engine's Addreference for adding the required assemblies  
                new[]  
                {  
                        typeof (Console).Assembly,  
                        typeof (HostObject).Assembly,  
                        typeof (IEnumerable<>).Assembly,  
                        typeof (IQueryable).Assembly
                }.ToList().ForEach(asm => engine.AddReference(asm));

                new[]  
                {  
                   "System", "System.Linq",   
                   "System.Collections",  
                   "System.Collections.Generic"  
                }.ToList().ForEach(ns => engine.ImportNamespace(ns));

                var session = engine.CreateSession(hostObject);

                session.Execute(@"Value = 156;");
                session.Execute(@"System.Console.WriteLine(Value);");
            }
        }
    }

    public class HostObject
    {
        public int Value { get; set; }
    }
}
