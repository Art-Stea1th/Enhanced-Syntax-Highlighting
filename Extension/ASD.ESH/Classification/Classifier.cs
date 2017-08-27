using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace ASD.ESH.Classification {

    internal sealed class Classifier : IClassifier {

        private ClassifierDocument document;
        private ClassificationSpanProvider spanProvider;
        private ITextBuffer buffer;

#pragma warning disable CS0067
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
#pragma warning restore CS0067

        public Classifier(ITextBuffer buffer) {
            this.buffer = buffer;
            spanProvider = new ClassificationSpanProvider();
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span) {

            if (document == null || document.Snapshot != span.Snapshot) {

                var task = ClassifierDocument.Resolve(buffer, span.Snapshot);

                try { task.Wait(); }
                catch (Exception) { return new List<ClassificationSpan>(); }

                if ((document = task.Result) == null) {
                    return new List<ClassificationSpan>();
                }
            }
            return Classify(span).ToList();
        }

        private IEnumerable<ClassificationSpan> Classify(SnapshotSpan span) {

            foreach (var textSpan in document.GetTextSpans(span)) {

                var symbol = document.GetSymbol(textSpan);
                if (symbol == null) { continue; }

                var classificationSpan = spanProvider.GetSpan(symbol.Kind, textSpan, span.Snapshot);
                if (classificationSpan == null) { continue; }

                yield return classificationSpan;
            }
        }
    }
}