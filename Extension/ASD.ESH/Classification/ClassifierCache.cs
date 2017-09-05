using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace ASD.ESH.Classification {

    internal sealed partial class Classifier {

        internal sealed class ClassifierCache {

            private readonly int maxUnitsCount;
            private readonly ConcurrentDictionary<DocumentId, CacheUnit> units;

            public ClassifierCache(int maxUnitsCount = 32) {
                this.maxUnitsCount = maxUnitsCount;
                units = new ConcurrentDictionary<DocumentId, CacheUnit>();
            }

            public void AddOrUpdate(DocumentId documentId, ITextSnapshot snapshot, IList<ClassificationSpan> spans) {
                ReduceCache();
                units.AddOrUpdate(documentId,
                    (id) => new CacheUnit(snapshot, spans), (id, oldUnit) => new CacheUnit(snapshot, spans));
            }

            public ICacheUnit GetOrNull(DocumentId id) => units.SingleOrDefault(u => u.Key == id).Value;

            private void ReduceCache() {

                if (units.Count > maxUnitsCount) {

                    var orderedByTime = units.OrderBy(u => u.Value.CreationTime);
                    var oldest = orderedByTime.First();

                    while (units.Count > maxUnitsCount) {
                        units.TryRemove(oldest.Key, out var value);
                    }
                }
            }

            private sealed class CacheUnit : ICacheUnit {

                public ITextSnapshot Snapshot { get; }
                public IList<ClassificationSpan> Spans { get; }

                public DateTime CreationTime { get; }

                public CacheUnit(ITextSnapshot snapshot, IList<ClassificationSpan> spans) {
                    Snapshot = snapshot; Spans = spans; CreationTime = DateTime.Now;
                }
            }
        }

        internal interface ICacheUnit {
            ITextSnapshot Snapshot { get; }
            IList<ClassificationSpan> Spans { get; }
        }
    }        
}