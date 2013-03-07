using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
using Roslyn.Services;
using Roslyn.Services.CSharp;

namespace Semantics
{
    class Program
    {
        static void Main(string[] args)
        {
            MetadataReference xunitReference = MetadataReference.CreateAssemblyReference("xunit.dll");
            
        }

        static void test1()
        {
            SyntaxTree tree = SyntaxTree.ParseFile(@"..\..\..\code\test.cs");

            var compilation = Compilation.Create("HelloWorld")
                             .AddReferences(MetadataReference.CreateAssemblyReference("mscorlib"))
                             .AddSyntaxTrees(tree);

            var model = compilation.GetSemanticModel(tree);

            var nameInfo = model.GetSymbolInfo(tree.GetRoot().Usings[0].Name);

            var systemSymbol = (NamespaceSymbol)nameInfo.Symbol;

            foreach (var ns in systemSymbol.GetNamespaceMembers())
            {
                Console.WriteLine(ns.Name);
            }

            //

            var helloWorldString = tree.GetRoot().DescendantNodes()
                                       .OfType<LiteralExpressionSyntax>()
                                       .First();

            var literalInfo = model.GetTypeInfo(helloWorldString);

            var stringTypeSymbol = (NamedTypeSymbol)literalInfo.Type;

            Console.Clear();
            foreach (var name in (from method in stringTypeSymbol.GetMembers().OfType<MethodSymbol>()
                                  where method.ReturnType == stringTypeSymbol &&
                                        method.DeclaredAccessibility == Accessibility.Public
                                  select method.Name).Distinct())
            {
                Console.WriteLine(name);
            }
        }
    }
}
