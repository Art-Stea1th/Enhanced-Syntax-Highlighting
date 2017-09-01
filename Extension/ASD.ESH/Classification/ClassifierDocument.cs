using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace ASD.ESH.Classification {

    using Helpers;

    internal sealed class ClassifierDocument {

        private Document document;
        private SemanticModel semanticModel;
        private SyntaxNode syntaxRoot;

        public SnapshotSpan SnapshotSpan { get; }

        private ClassificationRegistry types;

        private ClassifierDocument(
            Document document, SemanticModel semanticModel, SyntaxNode syntaxRoot, SnapshotSpan snapshotSpan) {
            this.document = document;
            this.semanticModel = semanticModel;
            this.syntaxRoot = syntaxRoot;
            SnapshotSpan = snapshotSpan;
            types = Container.Resolve<ClassificationRegistry>();
        }

        public static ClassifierDocument Resolve(SnapshotSpan snapshotSpan) {

            var document = snapshotSpan.Snapshot.GetOpenDocumentInCurrentContextWithChanges();
            if (document == null) { return null; }

            var semanticModel = document.GetSemanticModel();
            var syntaxRoot = document.GetSyntaxRoot();

            return new ClassifierDocument(document, semanticModel, syntaxRoot, snapshotSpan);
        }

        public IEnumerable<ClassificationSpan> GetClassificationSpans(SnapshotSpan span) {

            var classifiedSpans = document.GetClassifiedSpans(TextSpan.FromBounds(span.Start, span.End));

            bool Equals(string a, string b) => string.Equals(a, b, StringComparison.InvariantCultureIgnoreCase);

            return classifiedSpans.Where(cs => Equals(cs.ClassificationType, "identifier")).Select(cs => GetSpan(cs.TextSpan));
        }

        private ClassificationSpan GetSpan(TextSpan span) {

            var symbol = GetSymbol(span);
            if (symbol == null) {
                return null;
            }
            var type = types[symbol.Kind];
            if (type == null) {
                return null;
            }
            return CreateSpan(SnapshotSpan.Snapshot, span, types[symbol.Kind]);
        }

        private ISymbol GetSymbol(TextSpan textSpan) {

            var expressionSyntaxNode = GetExpression(syntaxRoot.FindNode(textSpan));

            return semanticModel.GetSymbolInfo(expressionSyntaxNode).Symbol
                ?? semanticModel.GetDeclaredSymbol(expressionSyntaxNode);
        }

        private SyntaxNode GetExpression(SyntaxNode node) {
            switch (node) {
                case ArgumentSyntax argument:
                    return argument.Expression;
                case AttributeArgumentSyntax attributeArgument:
                    return attributeArgument.Expression;
                default:
                    return node;
            }
        }

        private ClassificationSpan CreateSpan(ITextSnapshot snapshot, TextSpan span, IClassificationType type)
            => new ClassificationSpan(new SnapshotSpan(snapshot, new Span(span.Start, span.Length)), type);
    }
}