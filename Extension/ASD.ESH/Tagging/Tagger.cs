using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

namespace ASD.ESH.Tagging {

    using Common;
    using Helpers;

    internal sealed class Tagger : ITagger<IClassificationTag> {

        private Types Types => Singleton<Types>.Instance;

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public IEnumerable<ITagSpan<IClassificationTag>> GetTags(NormalizedSnapshotSpanCollection spans) {
            return Enumerable.Empty<ITagSpan<IClassificationTag>>();
        }
    }
}