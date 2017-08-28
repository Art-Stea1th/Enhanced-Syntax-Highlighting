using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace ASD.ESH.Classification {

    internal sealed class ClassificationSpanProvider {

        private ClassificationTypes types;
        private ClassifierDocument document;

        public ClassificationSpanProvider(ClassifierDocument document, ClassificationTypes types) {
            this.document = document; this.types = types;
        }

        public ClassificationSpan GetSpan(TextSpan span) {

            var symbol = GetSymbol(span);
            if (symbol == null) {
                return null;
            }
            var type = types[symbol.Kind];
            if (type == null) {
                return null;
            }
            return CreateSpan(document.Snapshot, span, types[symbol.Kind]);
        }

        private ISymbol GetSymbol(TextSpan textSpan) {

            var expressionSyntaxNode = GetExpression(document.SyntaxRoot.FindNode(textSpan));

            return document.SemanticModel.GetSymbolInfo(expressionSyntaxNode).Symbol
                ?? document.SemanticModel.GetDeclaredSymbol(expressionSyntaxNode);
        }

        private SyntaxNode GetExpression(SyntaxNode node) {

            return node.Kind() == SyntaxKind.Argument
                ? (node as ArgumentSyntax).Expression
                : node.Kind() == SyntaxKind.AttributeArgument
                ? (node as AttributeArgumentSyntax).Expression
                : node;
        }

        private ClassificationSpan CreateSpan(ITextSnapshot snapshot, TextSpan span, IClassificationType type)
            => new ClassificationSpan(new SnapshotSpan(snapshot, new Span(span.Start, span.Length)), type);        
    }
}