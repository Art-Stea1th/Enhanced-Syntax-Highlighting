// Copyright (c) "ESH-Repository" source code contributors. All Rights Reserved.
// Licensed under the Microsoft Public License (MS-PL).
// See LICENSE.md in the "ESH-Repository" root for license information.
// "ESH-Repository" root address: https://github.com/Art-Stea1th/Enhanced-Syntax-Highlighting

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.VisualStudio.Text.Classification;

namespace ASD.ESH.Classification {

    internal static partial class TypesRegistry {

        private const string pT = "ASD-ESH.";         // prefix   type-name
        private const string pF = pT + "Definition."; // prefix format-name

        private static readonly object token = new object();
        private static IClassificationTypeRegistryService registryService;

        public static void InitializeIfNeeded(IClassificationTypeRegistryService service) {

            if (service == null) {
                throw new System.ArgumentNullException(nameof(service));
            }
            if (registryService == null) {
                lock (token) {
                    if (registryService == null) {
                        registryService = service;
                    }
                }
            }
        }

        public static IClassificationType ResolveType(ISymbol symbol) {

            var userTagName = default(string);

            var modifier = symbol is INamespaceSymbol || symbol is ILocalSymbol
                ? DeclarationModifiers.None
                : DeclarationModifiers.From(symbol);

            switch (symbol.Kind) {

                case SymbolKind.Event:
                    userTagName
                        = UserTagName.Event; break;

                case SymbolKind.Field:
                    userTagName
                        = symbol.ContainingType.TypeKind == TypeKind.Enum
                        ? UserTagName.FieldEnum
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
                        = UserTagName.LocalVariable; break;
            }

            return userTagName != null
                ? registryService.GetClassificationType(pT + userTagName)
                : null;
        }

        private static class UserTagName {

            public const string Event = nameof(SymbolKind.Event);
            public const string Field = nameof(SymbolKind.Field);
            public const string FieldConstant = nameof(SymbolKind.Field) + "Constant";
            public const string FieldEnum = nameof(TypeKind.Enum);
            public const string LocalVariable = nameof(SymbolKind.Local);
            public const string Method = nameof(SymbolKind.Method);
            public const string MethodExtension = nameof(SymbolKind.Method) + "Extension";
            public const string MethodStatic = nameof(SymbolKind.Method) + "Static";
            public const string Namespace = nameof(SymbolKind.Namespace);
            public const string Parameter = nameof(SymbolKind.Parameter);
            public const string Property = nameof(SymbolKind.Property);
        }
    }
}