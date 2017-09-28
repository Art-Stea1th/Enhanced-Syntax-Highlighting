using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace ASD.ESH.Classification {

    internal static partial class TypesRegistry {

        private sealed class Definitions {

            private const string priority = Priority.High;

#pragma warning disable CS0649

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + nameof(SymbolKind.Field))]
            internal static ClassificationTypeDefinition FieldType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + nameof(SymbolKind.Method))]
            internal static ClassificationTypeDefinition MethodType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + nameof(SymbolKind.Namespace))]
            internal static ClassificationTypeDefinition NamespaceType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + nameof(SymbolKind.Parameter))]
            internal static ClassificationTypeDefinition ParameterType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + nameof(SymbolKind.Property))]
            internal static ClassificationTypeDefinition PropertyType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + nameof(SymbolKind.Local))]
            internal static ClassificationTypeDefinition LocalType;

#pragma warning restore CS0649

            [Export(typeof(EditorFormatDefinition)), Name(pF + nameof(SymbolKind.Field)), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + nameof(SymbolKind.Field)), Order(Before = priority)]
            private sealed class FieldFormatDefinition : FormatDefinition {
                public FieldFormatDefinition()
                    : base($"{SymbolKind.Field}", "#9CDCFE") { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + nameof(SymbolKind.Method)), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + nameof(SymbolKind.Method)), Order(Before = priority)]
            private sealed class MethodFormatDefinition : FormatDefinition {
                public MethodFormatDefinition()
                    : base($"{SymbolKind.Method}", "#DCDCAA") { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + nameof(SymbolKind.Namespace)), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + nameof(SymbolKind.Namespace)), Order(Before = priority)]
            private sealed class NamespaceFormatDefinition : FormatDefinition {
                public NamespaceFormatDefinition()
                    : base($"{SymbolKind.Namespace}") { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + nameof(SymbolKind.Parameter)), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + nameof(SymbolKind.Parameter)), Order(Before = priority)]
            private sealed class ParameterFormatDefinition : FormatDefinition {
                public ParameterFormatDefinition()
                    : base($"{SymbolKind.Parameter}", "#808080") { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + nameof(SymbolKind.Property)), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + nameof(SymbolKind.Property)), Order(Before = priority)]
            private sealed class PropertyFormatDefinition : FormatDefinition {
                public PropertyFormatDefinition()
                    : base($"{SymbolKind.Property}", "#9CDCFE") { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + nameof(SymbolKind.Local)), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + nameof(SymbolKind.Local)), Order(Before = priority)]
            private sealed class LocalFormatDefinition : FormatDefinition {
                public LocalFormatDefinition()
                    : base($"{SymbolKind.Local} Variable") { }
            }

            private abstract class FormatDefinition : ClassificationFormatDefinition {

                public FormatDefinition(string displayName, string defaultForegroundColor) : this(displayName) {
                    ForegroundColor = (Color)ColorConverter
                        .ConvertFromString(defaultForegroundColor);
                }

                public FormatDefinition(string displayName) {
                    DisplayName = $"User Tags - {displayName}";
                }
            }
        }
    }
}