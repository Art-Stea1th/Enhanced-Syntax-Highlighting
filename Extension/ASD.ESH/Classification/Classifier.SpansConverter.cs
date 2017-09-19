using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace ASD.ESH.Classification {

    internal sealed partial class Classifier {

        internal sealed class SpansConverter {

            private SemanticModel model;
            private SyntaxNode root;

            private ITextSnapshot snapshot;

            public SpansConverter(SemanticModel model, SyntaxNode root, ITextSnapshot snapshot) {
                this.model = model; this.root = root; this.snapshot = snapshot;
            }

            public IEnumerable<ClassificationSpan> ConvertAll(IEnumerable<ClassifiedSpan> spans) {

                var filteredByType = spans.Where(s => s.ClassificationType == ClassificationTypeNames.Identifier);

                foreach (var span in filteredByType) {
                    var converted = Convert(span);
                    if (converted != null) {
                        yield return converted;
                    }
                }
            }

            private ClassificationSpan Convert(ClassifiedSpan span) {

                var symbol = GetSymbol(span.TextSpan);
                if (symbol == null) { return null; }

                var type = TypesRegistry.ResolveType(symbol.Kind);
                if (type == null) { return null; }

                return CreateSpan(span.TextSpan, type);
            }

            private ISymbol GetSymbol(TextSpan textSpan) {

                var expressionSyntaxNode = GetExpression(root.FindNode(textSpan));

                return model.GetSymbolInfo(expressionSyntaxNode).Symbol
                    ?? model.GetDeclaredSymbol(expressionSyntaxNode);
            }

            private SyntaxNode GetExpression(SyntaxNode node) {

                switch (node) {
                    case ArgumentSyntax s:
                        return s.Expression;
                    case AttributeArgumentSyntax s:
                        return s.Expression;
                    default:
                        return node;
                }
            }

            private ClassificationSpan CreateSpan(TextSpan span, IClassificationType type)
                => new ClassificationSpan(new SnapshotSpan(snapshot, new Span(span.Start, span.Length)), type);
        }
    }
}