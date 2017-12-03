using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace ASD.ESH.Classification {

    internal static partial class TypesRegistry {

        private sealed class Definitions {

            private const string priority = PredefinedClassificationTypeNames.Identifier;

#pragma warning disable CS0649                        

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + nameof(UserTagName.Enum))]
            internal static ClassificationTypeDefinition EnumType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + nameof(UserTagName.Event))]
            internal static ClassificationTypeDefinition EventType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + nameof(UserTagName.Field))]
            internal static ClassificationTypeDefinition FieldType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + nameof(UserTagName.FieldConstant))]
            internal static ClassificationTypeDefinition FieldConstantType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + nameof(UserTagName.Method))]
            internal static ClassificationTypeDefinition MethodType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + nameof(UserTagName.MethodExtension))]
            internal static ClassificationTypeDefinition MethodExtensionType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + nameof(UserTagName.MethodStatic))]
            internal static ClassificationTypeDefinition MethodStaticType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + nameof(UserTagName.Namespace))]
            internal static ClassificationTypeDefinition NamespaceType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + nameof(UserTagName.Parameter))]
            internal static ClassificationTypeDefinition ParameterType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + nameof(UserTagName.Property))]
            internal static ClassificationTypeDefinition PropertyType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + nameof(UserTagName.Local))]
            internal static ClassificationTypeDefinition LocalType;

#pragma warning restore CS0649            

            [Export(typeof(EditorFormatDefinition)), Name(pF + nameof(UserTagName.Enum)), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + nameof(UserTagName.Enum)), Order(After = priority)]
            private sealed class EnumFormatDefinition : FormatDefinition {
                public EnumFormatDefinition()
                    : base($"{UserTagName.Enum} Entries") { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + nameof(UserTagName.Event)), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + nameof(UserTagName.Event)), Order(After = priority)]
            private sealed class EventFormatDefinition : FormatDefinition {
                public EventFormatDefinition()
                    : base($"{UserTagName.Event}s", "#9CDCFE") { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + nameof(UserTagName.Field)), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + nameof(UserTagName.Field)), Order(After = priority)]
            private sealed class FieldFormatDefinition : FormatDefinition {
                public FieldFormatDefinition()
                    : base($"{UserTagName.Field}s", "#9CDCFE") { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + nameof(UserTagName.FieldConstant)), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + nameof(UserTagName.FieldConstant)), Order(After = priority)]
            private sealed class FieldConstantFormatDefinition : FormatDefinition {
                public FieldConstantFormatDefinition()
                    : base($"{UserTagName.Field}s (Constant)", "#9CDCFE") { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + nameof(UserTagName.Method)), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + nameof(UserTagName.Method)), Order(After = priority)]
            private sealed class MethodFormatDefinition : FormatDefinition {
                public MethodFormatDefinition()
                    : base($"{UserTagName.Method}s", "#DCDCAA") { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + nameof(UserTagName.MethodExtension)), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + nameof(UserTagName.MethodExtension)), Order(After = priority)]
            private sealed class MethodExtensionFormatDefinition : FormatDefinition {
                public MethodExtensionFormatDefinition()
                    : base($"{UserTagName.Method}s (Extension)", "#DCDCAA") { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + nameof(UserTagName.MethodStatic)), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + nameof(UserTagName.MethodStatic)), Order(After = priority)]
            private sealed class MethodStaticFormatDefinition : FormatDefinition {
                public MethodStaticFormatDefinition()
                    : base($"{UserTagName.Method}s (Static)", "#DCDCAA") { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + nameof(UserTagName.Namespace)), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + nameof(UserTagName.Namespace)), Order(After = priority)]
            private sealed class NamespaceFormatDefinition : FormatDefinition {
                public NamespaceFormatDefinition()
                    : base($"{UserTagName.Namespace}s") { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + nameof(UserTagName.Parameter)), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + nameof(UserTagName.Parameter)), Order(After = priority)]
            private sealed class ParameterFormatDefinition : FormatDefinition {
                public ParameterFormatDefinition()
                    : base($"{UserTagName.Parameter}s", "#808080") { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + nameof(UserTagName.Property)), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + nameof(UserTagName.Property)), Order(After = priority)]
            private sealed class PropertyFormatDefinition : FormatDefinition {
                public PropertyFormatDefinition()
                    : base(/*$"{UserTag.Property}",*/$"Properties", "#9CDCFE") { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + nameof(UserTagName.Local)), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + nameof(UserTagName.Local)), Order(After = priority)]
            private sealed class LocalFormatDefinition : FormatDefinition {
                public LocalFormatDefinition()
                    : base($"{UserTagName.Local} Variables") { }
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