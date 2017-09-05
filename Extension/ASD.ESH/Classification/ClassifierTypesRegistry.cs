using System.Collections.Generic;
using System.Windows.Media;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Text.Classification;

namespace ASD.ESH.Classification {

    internal sealed class ClassifierTypesRegistry {

        private const string prefix = nameof(ASD) + "." + nameof(ESH);
        private Dictionary<SymbolKind, IClassificationType> Types { get; }

        public IClassificationType this[SymbolKind kind]
            => Types.ContainsKey(kind) ? Types[kind] : null;

        public ClassifierTypesRegistry(IClassificationTypeRegistryService registryService, IClassificationFormatMapService formatMapService) {

            var classification = registryService.GetClassificationType(type: "identifier");
            var @base = new List<IClassificationType> { classification };

            Types = new Dictionary<SymbolKind, IClassificationType> {
                { SymbolKind.Field, GetOrCreateClassificationType(registryService, $"{prefix}.Field", @base) },
                { SymbolKind.Method, GetOrCreateClassificationType(registryService, $"{prefix}.Method", @base) },
                { SymbolKind.Namespace, GetOrCreateClassificationType(registryService, $"{prefix}.Namespace", @base) },
                { SymbolKind.Parameter, GetOrCreateClassificationType(registryService, $"{prefix}.Parameter", @base) },
                { SymbolKind.Property, GetOrCreateClassificationType(registryService, $"{prefix}.Property", @base) }
            };


            var codeFormatMap = formatMapService.GetClassificationFormatMap(category: "text");

            var fieldProperties = codeFormatMap.GetExplicitTextProperties(this[SymbolKind.Field]).SetForeground(Colors.LightSkyBlue);
            var methodProperties = codeFormatMap.GetExplicitTextProperties(this[SymbolKind.Method]).SetForeground(Colors.PaleGoldenrod);
            //var namespaceProperties = codeFormatMap.GetExplicitTextProperties(this[SymbolKind.NamedType]).SetForeground(Colors.Silver);
            var parameterProperties = codeFormatMap.GetExplicitTextProperties(this[SymbolKind.Parameter]).SetForeground(Colors.Gray);
            var propertyProperties = codeFormatMap.GetExplicitTextProperties(this[SymbolKind.Property]).SetForeground(Colors.LightSkyBlue);

            codeFormatMap.AddExplicitTextProperties(this[SymbolKind.Field], fieldProperties, classification);
            codeFormatMap.AddExplicitTextProperties(this[SymbolKind.Method], methodProperties);
            //codeFormatMap.AddExplicitTextProperties(this[SymbolKind.Namespace], namespaceProperties, classification);
            codeFormatMap.AddExplicitTextProperties(this[SymbolKind.Parameter], parameterProperties);
            codeFormatMap.AddExplicitTextProperties(this[SymbolKind.Property], propertyProperties);
        }

        private static IClassificationType GetOrCreateClassificationType(
            IClassificationTypeRegistryService service, string typeName, IEnumerable<IClassificationType> baseTypes) {
            return
                service.GetClassificationType(typeName) ??
                service.CreateClassificationType(typeName, baseTypes);
        }
    }
}