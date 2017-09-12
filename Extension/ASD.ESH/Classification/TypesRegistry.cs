using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Text.Classification;

namespace ASD.ESH.Classification {

    internal static partial class TypesRegistry {

        private const string pT = "ASD-ESH.";         // prefix   type-name
        private const string pF = pT + "Definition."; // prefix format-name

        private static IClassificationTypeRegistryService registryService;
        public static bool Initialized => registryService != null;

        public static void Initialize(IClassificationTypeRegistryService service)
            => registryService = service;

        public static IClassificationType ResolveType(SymbolKind kind)
            => registryService.GetClassificationType($"{pT}{kind}");
    }
}