// Copyright (c) "ESH-Repository" source code contributors. All Rights Reserved.
// Licensed under the Microsoft Public License (MS-PL).
// See LICENSE.md in the "ESH-Repository" root for license information.
// "ESH-Repository" root address: https://github.com/Art-Stea1th/Enhanced-Syntax-Highlighting

using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace ASD.ESH.Classification {

    internal static partial class TypesRegistry {

        private sealed class Definitions {

            private static class DefaultColor {
                public const string Blue = "#9CDCFE";
                public const string Yellow = "#DCDCAA";
                public const string DarkGray = "#808080";
            }

            private const string priority = PredefinedClassificationTypeNames.Identifier;

#pragma warning disable CS0649            

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + UserTagName.Event)]
            internal static ClassificationTypeDefinition EventType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + UserTagName.Field)]
            internal static ClassificationTypeDefinition FieldType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + UserTagName.FieldConstant)]
            internal static ClassificationTypeDefinition FieldConstantType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + UserTagName.FieldEnum)]
            internal static ClassificationTypeDefinition FieldEnumType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + UserTagName.LocalVariable)]
            internal static ClassificationTypeDefinition LocalVariableType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + UserTagName.Method)]
            internal static ClassificationTypeDefinition MethodType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + UserTagName.MethodExtension)]
            internal static ClassificationTypeDefinition MethodExtensionType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + UserTagName.MethodStatic)]
            internal static ClassificationTypeDefinition MethodStaticType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + UserTagName.Namespace)]
            internal static ClassificationTypeDefinition NamespaceType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + UserTagName.Parameter)]
            internal static ClassificationTypeDefinition ParameterType;

            [Export(typeof(ClassificationTypeDefinition)), Name(pT + UserTagName.Property)]
            internal static ClassificationTypeDefinition PropertyType;

#pragma warning restore CS0649

            [Export(typeof(EditorFormatDefinition)), Name(pF + UserTagName.Event), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + UserTagName.Event), Order(After = priority)]
            private sealed class EventFormatDefinition : FormatDefinition {
                public EventFormatDefinition()
                    : base($"{UserTagName.Event}s", DefaultColor.Blue) { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + UserTagName.Field), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + UserTagName.Field), Order(After = priority)]
            private sealed class FieldFormatDefinition : FormatDefinition {
                public FieldFormatDefinition()
                    : base($"{UserTagName.Field}s", DefaultColor.Blue) { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + UserTagName.FieldConstant), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + UserTagName.FieldConstant), Order(After = priority)]
            private sealed class FieldConstantFormatDefinition : FormatDefinition {
                public FieldConstantFormatDefinition()
                    : base($"{UserTagName.Field}s (Constant)", DefaultColor.Blue) { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + UserTagName.FieldEnum), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + UserTagName.FieldEnum), Order(After = priority)]
            private sealed class FieldEnumFormatDefinition : FormatDefinition {
                public FieldEnumFormatDefinition()
                    : base($"{UserTagName.Field}s (Inside Enums)") { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + UserTagName.LocalVariable), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + UserTagName.LocalVariable), Order(After = priority)]
            private sealed class LocalVariableFormatDefinition : FormatDefinition {
                public LocalVariableFormatDefinition()
                    : base($"{UserTagName.LocalVariable} Variables") { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + UserTagName.Method), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + UserTagName.Method), Order(After = priority)]
            private sealed class MethodFormatDefinition : FormatDefinition {
                public MethodFormatDefinition()
                    : base($"{UserTagName.Method}s", DefaultColor.Yellow) { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + UserTagName.MethodExtension), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + UserTagName.MethodExtension), Order(After = priority)]
            private sealed class MethodExtensionFormatDefinition : FormatDefinition {
                public MethodExtensionFormatDefinition()
                    : base($"{UserTagName.Method}s (Extension)", DefaultColor.Yellow) { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + UserTagName.MethodStatic), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + UserTagName.MethodStatic), Order(After = priority)]
            private sealed class MethodStaticFormatDefinition : FormatDefinition {
                public MethodStaticFormatDefinition()
                    : base($"{UserTagName.Method}s (Static)", DefaultColor.Yellow) { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + UserTagName.Namespace), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + UserTagName.Namespace), Order(After = priority)]
            private sealed class NamespaceFormatDefinition : FormatDefinition {
                public NamespaceFormatDefinition()
                    : base($"{UserTagName.Namespace}s") { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + UserTagName.Parameter), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + UserTagName.Parameter), Order(After = priority)]
            private sealed class ParameterFormatDefinition : FormatDefinition {
                public ParameterFormatDefinition()
                    : base($"{UserTagName.Parameter}s", DefaultColor.DarkGray) { }
            }

            [Export(typeof(EditorFormatDefinition)), Name(pF + UserTagName.Property), UserVisible(true)]
            [ClassificationType(ClassificationTypeNames = pT + UserTagName.Property), Order(After = priority)]
            private sealed class PropertyFormatDefinition : FormatDefinition {
                public PropertyFormatDefinition()
                    : base(/*$"{UserTagName.Property}",*/"Properties", DefaultColor.Blue) { }
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