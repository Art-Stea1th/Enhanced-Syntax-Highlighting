using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace ASD.ESH.Classification {

    using Helpers;

    internal sealed class ClassificationSpanProvider {

        private ClassificationTypes Types => Singleton<ClassificationTypes>.Instance;

        public ClassificationSpan GetSpan(SymbolKind kind, TextSpan span, ITextSnapshot snapshot) {
            switch (kind) {
                case SymbolKind.Parameter: return CreateSpan(snapshot, span, Types.ParameterType);
                case SymbolKind.Property: return CreateSpan(snapshot, span, Types.PropertyType);
                case SymbolKind.Method: return CreateSpan(snapshot, span, Types.MethodType);
                default: return null;
            }
        }

        private ClassificationSpan CreateSpan(ITextSnapshot snapshot, TextSpan span, IClassificationType type)
            => new ClassificationSpan(new SnapshotSpan(snapshot, new Span(span.Start, span.Length)), type);
    }
}