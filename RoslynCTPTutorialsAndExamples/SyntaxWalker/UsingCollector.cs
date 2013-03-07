using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;

namespace SyntaxWalker
{
    class UsingCollector : SyntaxVisitor
    {
        public readonly List<UsingDirectiveSyntax> Usings = new List<UsingDirectiveSyntax>();

        public override void Visit(SyntaxNode node)
        {
            base.Visit(node);
        }

        public override void VisitUsingDirective(UsingDirectiveSyntax node)
        {
            //if (node.Name.ToString() != "System" && !node.Name.ToString().StartsWith("System."))
            //{
                this.Usings.Add(node);
            //}
        }

        public override void VisitUsingStatement(UsingStatementSyntax node)
        {
            base.VisitUsingStatement(node);
        }
    }
}