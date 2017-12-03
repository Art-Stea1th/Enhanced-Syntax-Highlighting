using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.VisualStudio.Text.Classification;

namespace ASD.ESH.Classification {

    internal static partial class TypesRegistry {

        private const string pT = "ASD-ESH.";         // prefix   type-name
        private const string pF = pT + "Definition."; // prefix format-name

        private static IClassificationTypeRegistryService registryService;
        public static bool Initialized => registryService != null;

        public static void Initialize(IClassificationTypeRegistryService service)
            => registryService = service;

        public static IClassificationType ResolveType(ISymbol symbol) {

            var userTagName = default(string);
            var modifier = DeclarationModifiers.From(symbol);

            switch (symbol.Kind) {

                case SymbolKind.Event:
                    userTagName
                        = UserTagName.Event; break;

                case SymbolKind.Field:
                    userTagName
                        = symbol.ContainingType.TypeKind == TypeKind.Enum
                        ? UserTagName.Enum
                        : modifier.IsConst
                        ? UserTagName.FieldConstant
                        : UserTagName.Field; break;

                case SymbolKind.Method:
                    userTagName
                        = (symbol as IMethodSymbol).IsExtensionMethod
                        ? UserTagName.MethodExtension
                        : modifier.IsStatic
                        ? UserTagName.MethodStatic
                        : UserTagName.Method; break;

                case SymbolKind.Namespace:
                    userTagName
                        = UserTagName.Namespace; break;

                case SymbolKind.Parameter:
                    userTagName
                        = UserTagName.Parameter; break;

                case SymbolKind.Property:
                    userTagName
                        = UserTagName.Property; break;

                case SymbolKind.Local:
                    userTagName
                        = UserTagName.Local; break;
            }

            return userTagName != null
                ? registryService.GetClassificationType($"{pT}{userTagName}")
                : null;
        }

        private static class UserTagName {

            public const string Enum = nameof(TypeKind.Enum);
            public const string Event = nameof(SymbolKind.Event);
            public const string Field = nameof(SymbolKind.Field);
            public const string FieldConstant = nameof(SymbolKind.Field) + "Constant";
            public const string Method = nameof(SymbolKind.Method);
            public const string MethodExtension = nameof(SymbolKind.Method) + "Extension";
            public const string MethodStatic = nameof(SymbolKind.Method) + "Static";
            public const string Namespace = nameof(SymbolKind.Namespace);
            public const string Parameter = nameof(SymbolKind.Parameter);
            public const string Property = nameof(SymbolKind.Property);
            public const string Local = nameof(SymbolKind.Local);
        }
    }
}