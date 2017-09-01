using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace ASD.ESH.Classification {

    internal sealed class Classifier : IClassifier {

        private ClassifierDocument document;

#pragma warning disable CS0067
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
#pragma warning restore CS0067

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span) {

            if (document == null || document.SnapshotSpan.Snapshot != span.Snapshot) {

                document = ClassifierDocument.Resolve(span);

                if (document == null) {
                    return new List<ClassificationSpan>();
                }
            }

            var spans = document.GetClassificationSpans(span);

            if (spans == null) {
                return new List<ClassificationSpan>();
            }

            return spans.ToList();
        }
    }
}