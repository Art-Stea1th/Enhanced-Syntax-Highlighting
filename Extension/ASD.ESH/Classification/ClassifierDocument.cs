using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Text;

using RoslynClassifier = Microsoft.CodeAnalysis.Classification.Classifier;

namespace ASD.ESH.Classification {

    internal sealed class ClassifierDocument {

        public Workspace Workspace { get; }
        public SemanticModel SemanticModel { get; }
        public SyntaxNode SyntaxRoot { get; }
        public ITextSnapshot Snapshot { get; }

        private ClassifierDocument(Workspace workspace, SemanticModel model, SyntaxNode root, ITextSnapshot snapshot) {
            Workspace = workspace; SemanticModel = model; SyntaxRoot = root; Snapshot = snapshot;
        }

        public static async Task<ClassifierDocument> Resolve(ITextBuffer buffer, ITextSnapshot snapshot) {

            var workspace = buffer.GetWorkspace();
            var document = snapshot.GetOpenDocumentInCurrentContextWithChanges();

            if (document == null) { return null; }

            var semanticModel = await document.GetSemanticModelAsync().ConfigureAwait(false);
            var syntaxRoot = await document.GetSyntaxRootAsync().ConfigureAwait(false);

            return new ClassifierDocument(workspace, semanticModel, syntaxRoot, snapshot);
        }

        public IEnumerable<TextSpan> GetTextSpans(SnapshotSpan span) {

            var classifiedSpans = RoslynClassifier
                .GetClassifiedSpans(SemanticModel, TextSpan.FromBounds(span.Start, span.End), Workspace);

            bool Equals(string a, string b) => string.Equals(a, b, StringComparison.InvariantCultureIgnoreCase);

            return classifiedSpans.Where(cs => Equals(cs.ClassificationType, "identifier")).Select(cs => cs.TextSpan);
        }

        public ISymbol GetSymbol(TextSpan textSpan) {
            var node = SyntaxRoot.FindNode(textSpan);
            return SemanticModel.GetSymbolInfo(node).Symbol
                ?? SemanticModel.GetDeclaredSymbol(node);
        }
    }
}