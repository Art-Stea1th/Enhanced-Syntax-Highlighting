using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.CodeAnalysis.Text;

namespace ASD.ESH.Helpers {

    internal static class Extensions {

        public static async Task<IEnumerable<ClassifiedSpan>> GetClassifiedSpansAsync(
            this Document document, TextSpan textSpan, CancellationToken cancellationToken = default(CancellationToken))
            => await Classifier.GetClassifiedSpansAsync(document, textSpan, cancellationToken);
    }
}