using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Text.Classification;

namespace ASD.ESH.Classification {

    internal static partial class TypesRegistry {

        private const string pT = "ASD-ESH.";         // prefix   type-name
        private const string pF = pT + "Definition."; // prefix format-name

        private static readonly object token = new object();
        private static IClassificationTypeRegistryService registryService = null;

        public static void InitializeIfNeeded( IClassificationTypeRegistryService service ) {
            if ( service == null )
                throw new System.ArgumentNullException( nameof(service) );

            if ( registryService == null ) {
                lock ( token ) {
                    if ( registryService == null )
                        registryService = service;
                }
            }
        }

        public static IClassificationType ResolveType(SymbolKind kind)
            => registryService.GetClassificationType($"{pT}{kind}");
    }
}