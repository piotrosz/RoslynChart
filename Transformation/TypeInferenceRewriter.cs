using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
using Roslyn.Services;
using Roslyn.Services.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transformation
{
    public class TypeInferenceRewriter : SyntaxRewriter
    {
        private readonly SemanticModel semanticModel;

        public TypeInferenceRewriter(SemanticModel semanticModel)
        {
            this.semanticModel = semanticModel;
        }

        public override SyntaxNode VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            // e.g. int x, y = 0;
            if (node.Declaration.Variables.Count > 1)
            {
                return node;
            }

            // e.g. int x;
            if (node.Declaration.Variables[0].Initializer == null)
            {
                return node;
            }

            VariableDeclaratorSyntax declarator = node.Declaration.Variables.First(); 
            TypeSyntax variableTypeName = node.Declaration.Type;
            TypeSymbol variableType = (TypeSymbol)semanticModel.GetSymbolInfo(variableTypeName).Symbol;

            TypeInfo initializerInfo = semanticModel.GetTypeInfo(declarator.Initializer.Value);

            // only when type is the same, (e.g. no base class casting)
            if (variableType == initializerInfo.Type)
            {
                TypeSyntax varTypeName = Syntax.IdentifierName("var")
                                     .WithLeadingTrivia(variableTypeName.GetLeadingTrivia())
                                     .WithTrailingTrivia(variableTypeName.GetTrailingTrivia());

                return node.ReplaceNode<LocalDeclarationStatementSyntax, TypeSyntax>(variableTypeName, varTypeName);
            }
            else
            {
                return node;
            }

        }
    }
}
