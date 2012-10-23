using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Roslyn.Compilers;
using Roslyn.Compilers.Common;
using Roslyn.Compilers.CSharp;
using Roslyn.Services;
using Roslyn.Services.Editor;

namespace FirstQuickFix
{
    [ExportCodeIssueProvider("FirstQuickFix", LanguageNames.CSharp)]
    class CodeIssueProvider : ICodeIssueProvider
    {
        public IEnumerable<CodeIssue> GetIssues(IDocument document, CommonSyntaxNode node, CancellationToken cancellationToken)
        {
            #region Old code
            //var tokens = from nodeOrToken in node.ChildNodesAndTokens()
            //             where nodeOrToken.IsToken
            //             select nodeOrToken.AsToken();

            //foreach (var token in tokens)
            //{
            //    var tokenText = token.ToString();

            //    if (tokenText.Contains('a'))
            //    {
            //        var issueDescription = string.Format("'{0}' contains the letter 'a'", tokenText);
            //        yield return new CodeIssue(CodeIssueKind.Warning, token.Span, issueDescription);
            //    }
            //}
            #endregion

            var localDeclaration = (LocalDeclarationStatementSyntax)node;


            if (localDeclaration.Modifiers.Any(SyntaxKind.ConstKeyword)) // already is const
            {
                return null;
            }

            var semanticModel = document.GetSemanticModel(cancellationToken);

            // Ensure that all variables in the local declaration have initializers that
            // are assigned with constant values.
            foreach (var variable in localDeclaration.Declaration.Variables)
            {
                var initializer = variable.Initializer;
                if (initializer == null) // no initializer -> cannot make const
                {
                    return null;
                }

                var constantValue = semanticModel.GetConstantValue(initializer.Value);
                if (!constantValue.HasValue)
                {
                    return null;
                }
            }

            // Perform data flow analysis on the local declaration.
            var dataFlowAnalysis = semanticModel.AnalyzeDataFlow(localDeclaration);

            // Retrieve the local symbol for each variable in the local declaration
            // and ensure that it is not written outside of the data flow analysis region.
            foreach (var variable in localDeclaration.Declaration.Variables)
            {
                var variableSymbol = semanticModel.GetDeclaredSymbol(variable);
                if (dataFlowAnalysis.WrittenOutside.Contains(variableSymbol))
                {
                    return null;
                }
            }

            return new[]
            {
                new CodeIssue(CodeIssueKind.Warning, localDeclaration.Span,
                    "Can be made constant", new CodeAction(document, localDeclaration))
            };
        }

        public IEnumerable<Type> SyntaxNodeTypes
        {
            get
            {
                yield return typeof(LocalDeclarationStatementSyntax); //typeof(SyntaxNode);
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
