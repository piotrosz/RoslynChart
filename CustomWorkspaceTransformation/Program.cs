using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
using Roslyn.Compilers.VisualBasic;
using Roslyn.Services;
using Roslyn.Services.CSharp;
using Roslyn.Compilers.Common;

namespace CustomWorkspaceTransformation
{
    class Program
    {
        static void Main(string[] args)
        {
            var workspace = Workspace.LoadSolution(@"..\..\..\code\SampleCode1\SampleCode1.sln");

            var originalSolution = workspace.CurrentSolution;
            ISolution newSolution = originalSolution;

            foreach (var project in originalSolution.Projects)
            {
                foreach (var documentId in project.DocumentIds)
                {
                    var document = newSolution.GetDocument(documentId);

                    // Transform the syntax tree of the document and get the root of the new tree
                    CommonSyntaxNode newRoot = TransformSyntaxRoot(document);

                    newSolution = newSolution.UpdateDocument(document.Id, newRoot);
                }
            }

            workspace.ApplyChanges(originalSolution, newSolution);
        }

        private static CommonSyntaxNode TransformSyntaxRoot(IDocument document)
        {
            var originalRoot = document.GetSyntaxRoot();
            switch (document.LanguageServices.Language)
            {
                case LanguageNames.CSharp:
                    return TransformRootCSharp((Roslyn.Compilers.CSharp.SyntaxNode)originalRoot);
                case LanguageNames.VisualBasic:
                    return TransformRootVisualBasic((Roslyn.Compilers.VisualBasic.SyntaxNode)originalRoot);
            }

            return originalRoot;
        }

        private static CommonSyntaxNode TransformRootCSharp(Roslyn.Compilers.CSharp.SyntaxNode originalRoot)
        {
            var comments = originalRoot.DescendantTrivia().Where(t => t.Kind == Roslyn.Compilers.CSharp.SyntaxKind.SingleLineCommentTrivia);
            var newRoot = originalRoot.ReplaceTrivia(comments, (t1, t2) => Roslyn.Compilers.CSharp.SyntaxTriviaList.Empty);

            return newRoot;
        }

        private static CommonSyntaxNode TransformRootVisualBasic(Roslyn.Compilers.VisualBasic.SyntaxNode originalRoot)
        {
            var comments = originalRoot.DescendantTrivia().Where(t => t.Kind == Roslyn.Compilers.VisualBasic.SyntaxKind.CommentTrivia);
            var newRoot = originalRoot.ReplaceTrivia(comments, (t1, t2) => Roslyn.Compilers.VisualBasic.SyntaxTriviaList.Empty);

            return newRoot;
        }
    }
}
