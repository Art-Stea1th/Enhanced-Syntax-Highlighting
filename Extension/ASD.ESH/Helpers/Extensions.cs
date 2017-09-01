using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace ASD.ESH.Helpers {

    internal static class Extensions {

        public static IClassificationType GetOrCreateClassificationType(
            this IClassificationTypeRegistryService service,
            string typeName, IEnumerable<IClassificationType> baseTypes) {
            return
                service.GetClassificationType(typeName) ??
                service.CreateClassificationType(typeName, baseTypes);
        }

        public static IEnumerable<ClassifiedSpan> GetClassifiedSpans(this Document document, TextSpan textSpan)
            => Classifier.GetClassifiedSpansAsync(document, textSpan).Synchronize();

        public static SemanticModel GetSemanticModel(this Document document)
            => document.GetSemanticModelAsync().Synchronize();

        public static SyntaxNode GetSyntaxRoot(this Document document)
            => document.GetSyntaxRootAsync().Synchronize();

        private static TResult Synchronize<TResult>(this Task<TResult> task) where TResult : class {
            task.ConfigureAwait(false); try { task.Wait(); } catch { return null; } return task.Result;
        }
    }
}