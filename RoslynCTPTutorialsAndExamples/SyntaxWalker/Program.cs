using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;

namespace SyntaxWalker
{
    class Program
    {
        static void Main(string[] args)
        {
            SyntaxTree tree = SyntaxTree.ParseText(
@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
 
namespace TopLevel
{
    using Microsoft;
    using System.ComponentModel;
 
    namespace Child1
    {
        using Microsoft.Win32;
        using System.Runtime.InteropServices;
 
        class Foo { }
    }
 
    namespace Child2
    {
        using System.CodeDom;
        using Microsoft.CSharp;
 
        class Bar { }
    }
}");
            var collector = new UsingCollector();

            foreach (var node in tree.GetRoot().DescendantNodesAndSelf())
            {
                collector.Visit(node);
            }

            foreach (var directive in collector.Usings)
            {
                Console.WriteLine(directive.Name);
            }
        }
    }
}
