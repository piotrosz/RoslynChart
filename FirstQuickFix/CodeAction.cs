using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
using Roslyn.Services;
using Roslyn.Services.Editor;
using System.Threading;

namespace FirstQuickFix
{
    public class CodeAction : ICodeAction
    {
        private IDocument document;
        private LocalDeclarationStatementSyntax localDeclaration;

        public CodeAction(IDocument document, LocalDeclarationStatementSyntax localDeclaration)
        {
            this.document = document;
            this.localDeclaration = localDeclaration;
        }
        public string Description
        {
            get { return "Make constant"; }
        }

        public CodeActionEdit GetEdit(CancellationToken cancellationToken)
        {
            // Remove the leading trivia from the local declaration.
            var firstToken = localDeclaration.GetFirstToken();
            var leadingTrivia = firstToken.LeadingTrivia;
            var trimmedLocal = localDeclaration.ReplaceToken(firstToken, firstToken.WithLeadingTrivia(SyntaxTriviaList.Empty));

            // Create a const token with the original leading trivia.
            var constToken = Syntax.Token(leadingTrivia, SyntaxKind.ConstKeyword);

            // Insert the const token into the modifiers list, creating a new modifiers list.
            var newModifiers = trimmedLocal.Modifiers.Insert(0, constToken);

            // Produce the new local declaration.
            var newLocal = trimmedLocal.WithModifiers(newModifiers);

            // Add an annotation to format the new local declaration.
            var formattedLocal = CodeAnnotations.Formatting.AddAnnotationTo(newLocal);

            // Replace the old local declaration with the new local declaration.
            var oldRoot = document.GetSyntaxRoot(cancellationToken);
            var newRoot = oldRoot.ReplaceNode(localDeclaration, formattedLocal);

            // Create and return a new CodeActionEdit for the transformed tree.
            return new CodeActionEdit(document.UpdateSyntaxRoot(newRoot));
        }
    }
}
