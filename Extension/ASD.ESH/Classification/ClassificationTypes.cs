using System.Collections.Generic;
using System.Windows.Media;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Text.Classification;

namespace ASD.ESH.Classification {

    using Helpers;

    internal sealed class ClassificationTypes {

        private const string prefix = nameof(ASD) + "." + nameof(ESH);
        private Dictionary<SymbolKind, IClassificationType> Types { get; }

        public IClassificationType this[SymbolKind kind]
            => Types.ContainsKey(kind) ? Types[kind] : null;

        public ClassificationTypes() {

            var typeRegistryService = Container.Resolve<IClassificationTypeRegistryService>();
            var formatMapService = Container.Resolve<IClassificationFormatMapService>();

            var classification = typeRegistryService.GetClassificationType(type: "identifier");
            var @base = new List<IClassificationType> { classification };

            Types = new Dictionary<SymbolKind, IClassificationType> {
                { SymbolKind.Field, typeRegistryService.GetOrCreateClassificationType($"{prefix}.Field", @base) },
                { SymbolKind.Method, typeRegistryService.GetOrCreateClassificationType($"{prefix}.Method", @base) },
                { SymbolKind.Namespace, typeRegistryService.GetOrCreateClassificationType($"{prefix}.Namespace", @base) },
                { SymbolKind.Parameter, typeRegistryService.GetOrCreateClassificationType($"{prefix}.Parameter", @base) },
                { SymbolKind.Property, typeRegistryService.GetOrCreateClassificationType($"{prefix}.Property", @base) }
            };


            var codeFormatMap = formatMapService.GetClassificationFormatMap(category: "text");

            //var fieldProperties = codeFormatMap.GetExplicitTextProperties(this[SymbolKind.Field]).SetForeground(Colors.Silver);
            var methodProperties = codeFormatMap.GetExplicitTextProperties(this[SymbolKind.Method]).SetForeground(Colors.PaleGoldenrod);
            //var namespaceProperties = codeFormatMap.GetExplicitTextProperties(this[SymbolKind.NamedType]).SetForeground(Colors.Silver);
            var parameterProperties = codeFormatMap.GetExplicitTextProperties(this[SymbolKind.Parameter]).SetForeground(Colors.Gray);
            var propertyProperties = codeFormatMap.GetExplicitTextProperties(this[SymbolKind.Property]).SetForeground(Colors.LightSkyBlue);

            //codeFormatMap.AddExplicitTextProperties(this[SymbolKind.Field], fieldProperties, classification);
            codeFormatMap.AddExplicitTextProperties(this[SymbolKind.Method], methodProperties, classification);
            //codeFormatMap.AddExplicitTextProperties(this[SymbolKind.Namespace], namespaceProperties, classification);
            codeFormatMap.AddExplicitTextProperties(this[SymbolKind.Parameter], parameterProperties, classification);
            codeFormatMap.AddExplicitTextProperties(this[SymbolKind.Property], propertyProperties, classification);
        }
    }
}