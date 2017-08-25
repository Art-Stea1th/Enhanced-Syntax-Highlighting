using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace ASD.ESH.Classification {

    using Common;
    using Helpers;

    internal sealed class Classifier : IClassifier {

        private Types Types => Singleton<Types>.Instance;

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span) {
            return new List<ClassificationSpan>() {
                new ClassificationSpan(new SnapshotSpan(span.Snapshot, new Span(span.Start, span.Length)), Types.ParameterType)
            };
        }
    }
}