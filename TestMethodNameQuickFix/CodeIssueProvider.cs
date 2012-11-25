using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Roslyn.Compilers;
using Roslyn.Compilers.Common;
using Roslyn.Compilers.CSharp;
using Roslyn.Services;
using Roslyn.Services.Editor;

namespace TestMethodNameQuickFix
{
    [ExportCodeIssueProvider("TestMethodNameQuickFix", LanguageNames.CSharp)]
    public class CodeIssueProvider : ICodeIssueProvider
    {
        public IEnumerable<CodeIssue> GetIssues(IDocument document, CommonSyntaxNode node, CancellationToken cancellationToken)
        {
            var methodDeclaration = (MethodDeclarationSyntax)node;

            if (methodDeclaration.AttributeLists.Any(a => a.Attributes.Any(at => at.Name.ToString() == "Fact")) &&
                !methodDeclaration.Identifier.ToString().IsUnderscoreCase()) // Not already underscore case
            {
                return new[] { 
                    new CodeIssue(CodeIssueKind.Warning, methodDeclaration.Identifier.Span, 
                    "Convert to underscore_case",
                    new CodeAction(document, methodDeclaration)) 
                };
            }

            return null;
        }

        public IEnumerable<Type> SyntaxNodeTypes
        {
            get
            {
                yield return typeof(MethodDeclarationSyntax);
            }
        }

        #region Unimplemented ICodeIssueProvider members

        public IEnumerable<CodeIssue> GetIssues(IDocument document, CommonSyntaxToken token, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> SyntaxTokenKinds
        {
            get { return null; }
        }

        #endregion
    }
}
