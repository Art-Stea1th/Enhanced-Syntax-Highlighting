using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace ASD.ESH.Classification {

    internal sealed partial class Classifier {

        private sealed class Cache {

            private readonly int maxUnitsCount = 32;
            private readonly ConcurrentDictionary<DocumentId, Unit> units;

            public Cache(int maxUnitsCount = 32) {
                this.maxUnitsCount = maxUnitsCount;
                units = new ConcurrentDictionary<DocumentId, Unit>();
            }

            public void AddOrUpdate(DocumentId documentId, ITextSnapshot snapshot, IList<ClassificationSpan> spans) {
                ReduceCache();
                units.AddOrUpdate(documentId,
                    (id) => new Unit(snapshot, spans), (id, oldUnit) => new Unit(snapshot, spans));
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

            private sealed class Unit : ICacheUnit {

                public ITextSnapshot Snapshot { get; }
                public IList<ClassificationSpan> Spans { get; }

                public DateTime CreationTime { get; }

                public Unit(ITextSnapshot snapshot, IList<ClassificationSpan> spans) {
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