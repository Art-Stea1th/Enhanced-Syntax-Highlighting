using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace ASD.ESH.Classification {

    using Helpers;

    internal sealed partial class Classifier : IClassifier {

#pragma warning disable CS0067
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
#pragma warning restore CS0067

        private Document document;
        private ITextSnapshot snapshot;

        private IList<ClassificationSpan> lastSpans;

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span) {

            var snapshot = span.Snapshot;

            var document = snapshot.GetOpenDocumentInCurrentContextWithChanges();
            if (document == null) { return EmptyList<ClassificationSpan>(); }

            if (this.document?.Id != document.Id || this.snapshot != snapshot) {

                this.document = document; this.snapshot = snapshot;

                Task.Factory.StartNew(async () => lastSpans = await GetSpansAsync(document, snapshot));
            }
            return lastSpans ?? EmptyList<ClassificationSpan>();
        }

        private async Task<IList<ClassificationSpan>> GetSpansAsync(Document document, ITextSnapshot snapshot) {

            var model = await document.GetSemanticModelAsync();
            var root = await document.GetSyntaxRootAsync();
            var spans = await document.GetClassifiedSpansAsync(new TextSpan(0, snapshot.Length));

            var resultSpans = new List<ClassificationSpan>(spans.Count());
            var converter = new SpansConverter(model, root, snapshot);

            Parallel.ForEach(converter.ConvertAll(spans), (span) => {
                resultSpans.Add(span);
            });
            return resultSpans;
        }

        private static IList<T> EmptyList<T>() => Enumerable.Empty<T>().ToList();
    }
}