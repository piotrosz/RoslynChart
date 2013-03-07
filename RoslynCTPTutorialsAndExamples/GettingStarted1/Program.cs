using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Roslyn.Scripting;
using Roslyn.Compilers.CSharp;
using Roslyn.Compilers;

namespace GettingStarted1
{
    class Program
    {
        static void Main()
        {
            SyntaxTree syntaxTree = SyntaxTree.ParseFile(@"..\..\..\code\test.cs");
            CompilationUnitSyntax root = syntaxTree.GetRoot();

            var methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>()
                .Where(m => m.AttributeLists.Any(a => a.Attributes.Any(at => at.Name.ToString() == "Fact")));

            CompilationUnitSyntax newRoot;

            foreach (var methodDeclaration in methods)
            {
                var newMethodDeclaration = methodDeclaration.WithIdentifier(Syntax.Identifier("method2"));
                newRoot = root.ReplaceNode(methodDeclaration, newMethodDeclaration);
            }
        }

        static void test1()
        {
            SyntaxTree syntaxTree = SyntaxTree.ParseFile(@"..\..\..\code\test.cs");
            CompilationUnitSyntax root = syntaxTree.GetRoot();

            var firstMember = root.Members[0];
            var namespaceDeclarations = (NamespaceDeclarationSyntax)firstMember;
            var classDeclaration = (ClassDeclarationSyntax)namespaceDeclarations.Members[0];
            var mainDeclaration = (MethodDeclarationSyntax)classDeclaration.Members[0];

            TypeSyntax returnType = mainDeclaration.ReturnType;
            BlockSyntax body = mainDeclaration.Body;
            ParameterListSyntax parameters = mainDeclaration.ParameterList;

            var argsParameter = parameters.Parameters[0];

            // -------------------------------------------------------------------------------

            var firstParameters = from methodDeclaration in root.DescendantNodes()
                                                    .OfType<MethodDeclarationSyntax>()
                                  where methodDeclaration.Identifier.ValueText == "Main"
                                  select methodDeclaration.ParameterList.Parameters.First();

            var argsParameter2 = firstParameters.Single();
        }
    }
}
