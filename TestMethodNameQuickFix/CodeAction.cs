using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Roslyn.Compilers;
using Roslyn.Compilers.Common;
using Roslyn.Compilers.CSharp;
using Roslyn.Services;
using Roslyn.Services.Editor;

namespace TestMethodNameQuickFix
{
    public class CodeAction : ICodeAction
    {
        private IDocument document;
        private MethodDeclarationSyntax methodDeclaration;

        public CodeAction(IDocument document, MethodDeclarationSyntax methodDeclaration)
        {
            this.document = document;
            this.methodDeclaration = methodDeclaration;
        }

        public string Description
        {
            get { return "Change CamelCase to underscore_case"; }
        }

        public CodeActionEdit GetEdit(CancellationToken cancellationToken)
        {
            var newMethodDeclaration = methodDeclaration.WithIdentifier(Syntax.Identifier(methodDeclaration.Identifier.ValueText.ToUnderscoreCase()));

            var oldRoot = (SyntaxNode)document.GetSyntaxRoot(cancellationToken);
            var newRoot = oldRoot.ReplaceNode(methodDeclaration, newMethodDeclaration);

            return new CodeActionEdit(document.UpdateSyntaxRoot(newRoot));
        }
    }
}
