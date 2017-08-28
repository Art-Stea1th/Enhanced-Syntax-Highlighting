using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace ASD.ESH.Classification {

    internal sealed class Classifier : IClassifier {

        private ClassifierDocument document;
        private ClassificationSpanProvider provider;
        private ITextBuffer buffer;

#pragma warning disable CS0067
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
#pragma warning restore CS0067

        public Classifier(ITextBuffer buffer) {
            this.buffer = buffer;
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span) {

            if (document == null || document.Snapshot != span.Snapshot) {

                document = ClassifierDocument.Resolve(buffer, span.Snapshot);

                if (document == null) {
                    return new List<ClassificationSpan>();
                }
                provider = new ClassificationSpanProvider(document,
                    buffer.Properties.GetProperty(nameof(ClassificationTypes)) as ClassificationTypes);
            }
            return Classify(span).ToList();
        }

        private IEnumerable<ClassificationSpan> Classify(SnapshotSpan span) {

            foreach (var textSpan in document.GetTextSpans(span)) {

                var classificationSpan = provider.GetSpan(textSpan);
                if (classificationSpan == null) { continue; }
                yield return classificationSpan;
            }
        }
    }
}