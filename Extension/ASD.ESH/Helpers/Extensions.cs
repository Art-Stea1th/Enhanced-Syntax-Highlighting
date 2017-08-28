using System.Collections.Generic;
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
    }
}