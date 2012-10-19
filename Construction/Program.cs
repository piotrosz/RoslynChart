using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
using Roslyn.Services;
using Roslyn.Services.CSharp;
using System.IO;

namespace Construction
{
    class Program
    {
        static void Main(string[] args)
        {
            NameSyntax name = Syntax.IdentifierName("System");
            name = Syntax.QualifiedName(name, Syntax.IdentifierName("Collections"));
            name = Syntax.QualifiedName(name, Syntax.IdentifierName("Generic"));

            SyntaxTree tree = SyntaxTree.ParseFile(@"..\..\..\code\test.cs");

            var root = tree.GetRoot();

            var oldUsing = root.Usings[1];
            var newUsing = oldUsing.WithName(name);

            root = root.ReplaceNode(oldUsing, newUsing);

            //root.GetText();
        }
    }
}
