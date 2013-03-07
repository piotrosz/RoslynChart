using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
using Roslyn.Services;
using Roslyn.Services.CSharp;
using System.IO;

namespace Transformation
{
    class Program
    {
        static void Main(string[] args)
        {
            Compilation test = CreateTestCompilation();

            foreach (SyntaxTree sourceTree in test.SyntaxTrees)
            {
                SemanticModel model = test.GetSemanticModel(sourceTree);

                TypeInferenceRewriter rewriter = new TypeInferenceRewriter(model);

                SyntaxNode newSource = rewriter.Visit(sourceTree.GetRoot());

                if (newSource != sourceTree.GetRoot())
                {
                    File.WriteAllText(sourceTree.FilePath, newSource.GetText().ToString());
                }
            }
        }

        private static Compilation CreateTestCompilation()
        {
            SyntaxTree programTree = SyntaxTree.ParseFile(@"..\..\Program.cs");
            SyntaxTree rewriterTree = SyntaxTree.ParseFile(@"..\..\TypeInferenceRewriter.cs");

            List<SyntaxTree> sourceTrees = new List<SyntaxTree>();
            sourceTrees.Add(programTree);
            sourceTrees.Add(rewriterTree);

            MetadataReference mscorlib = MetadataReference.CreateAssemblyReference("mscorlib");
            MetadataReference roslynCompilers = MetadataReference.CreateAssemblyReference("Roslyn.Compilers");
            MetadataReference csCompiler = MetadataReference.CreateAssemblyReference("Roslyn.Compilers.CSharp");

            List<MetadataReference> references = new List<MetadataReference>();
            references.Add(mscorlib);
            references.Add(roslynCompilers);
            references.Add(csCompiler);

            CompilationOptions compilationOptions = new CompilationOptions(OutputKind.ConsoleApplication);

            return Compilation.Create("TransformationCS", compilationOptions, sourceTrees, references);
        }
    }
}
