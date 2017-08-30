using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace ASD.ESH.Classification {

    using Helpers;

    internal sealed class Classifier : IClassifier {

        private ClassifierDocument document;
        private ITextBuffer buffer;

#pragma warning disable CS0067
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
#pragma warning restore CS0067

        public Classifier() {
            buffer = Container.Resolve<ITextBuffer>();
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span) {

            if (document == null || document.Snapshot != span.Snapshot) {

                document = ClassifierDocument.Resolve(buffer, span.Snapshot);

                if (document == null) {
                    return new List<ClassificationSpan>();
                }
            }
            var task = Classify(span);
            task.Wait();
            return task.Result;
        }

        private async Task<List<ClassificationSpan>> Classify(SnapshotSpan span) {

            var spans = await document.GetClassificationSpans(span);
            var result = new List<ClassificationSpan>();

            foreach (var cSpan in spans) {

                if (cSpan == null) { continue; }
                result.Add(cSpan);
            }
            return result;
        }
    }
}