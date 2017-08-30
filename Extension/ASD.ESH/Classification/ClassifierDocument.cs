using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

using RoslynClassifier = Microsoft.CodeAnalysis.Classification.Classifier;

namespace ASD.ESH.Classification {

    using Helpers;

    internal sealed class ClassifierDocument {

        private Document document;
        private Workspace workspace;

        public SemanticModel SemanticModel { get; }
        public SyntaxNode SyntaxRoot { get; }
        public ITextSnapshot Snapshot { get; }


        private ClassificationTypes types;

        private ClassifierDocument(Document document, Workspace workspace, SemanticModel model, SyntaxNode root, ITextSnapshot snapshot) {
            this.document = document; this.workspace = workspace; SemanticModel = model; SyntaxRoot = root; Snapshot = snapshot; types = Container.Resolve<ClassificationTypes>();
        }

        public static ClassifierDocument Resolve(ITextBuffer buffer, ITextSnapshot snapshot) {
            var task = ResolveImpl(buffer, snapshot);
            try {
                task.Wait();
                return task.Result;
            }
            catch {
                return null;
            }            
        }

        private static async Task<ClassifierDocument> ResolveImpl(ITextBuffer buffer, ITextSnapshot snapshot) {

            var workspace = buffer.GetWorkspace();
            var document = snapshot.GetOpenDocumentInCurrentContextWithChanges();

            if (workspace == null || document == null) { return null; }

            var semanticModel = await document.GetSemanticModelAsync().ConfigureAwait(false);
            var syntaxRoot = await document.GetSyntaxRootAsync().ConfigureAwait(false);

            return new ClassifierDocument(document, workspace, semanticModel, syntaxRoot, snapshot);
        }

        public async Task<IEnumerable<ClassificationSpan>> GetClassificationSpans(SnapshotSpan span) {

            var classifiedSpans = await RoslynClassifier
                .GetClassifiedSpansAsync(document, TextSpan.FromBounds(span.Start, span.End));

            bool Equals(string a, string b) => string.Equals(a, b, StringComparison.InvariantCultureIgnoreCase);

            return classifiedSpans.Where(cs => Equals(cs.ClassificationType, "identifier")).Select(cs => GetSpan(cs.TextSpan));
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
            return CreateSpan(Snapshot, span, types[symbol.Kind]);
        }

        private ISymbol GetSymbol(TextSpan textSpan) {

            var expressionSyntaxNode = GetExpression(SyntaxRoot.FindNode(textSpan));

            return SemanticModel.GetSymbolInfo(expressionSyntaxNode).Symbol
                ?? SemanticModel.GetDeclaredSymbol(expressionSyntaxNode);
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