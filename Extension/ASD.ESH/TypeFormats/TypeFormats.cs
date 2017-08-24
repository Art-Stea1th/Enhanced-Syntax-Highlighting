using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

// Description: https://msdn.microsoft.com/en-us/library/dd885244.aspx

namespace ASD.ESH.TypeFormats {

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = typeFullName)]
    [Name(typeFullName)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class Parameter : ClassificationFormatDefinition {

        private const string typeFullName = Constants.RegistryPrefix + "." + nameof(Parameter);

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(typeFullName)]
        private static ClassificationTypeDefinition parameterType = null;

        public Parameter() {
            DisplayName = Constants.DisplayNamePrefix + " - " + nameof(Parameter);
            ForegroundColor = Colors.Gray;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = typeFullName)]
    [Name(typeFullName)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class Property : ClassificationFormatDefinition {

        private const string typeFullName = Constants.RegistryPrefix + "." + nameof(Property);

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(typeFullName)]
        private static ClassificationTypeDefinition propertyType = null;

        public Property() {
            DisplayName = Constants.DisplayNamePrefix + " - " + nameof(Property);
            ForegroundColor = Colors.LightBlue;
        }
    }

    internal static class Constants {

        public const string RegistryPrefix = Company + "." + Product;
        public const string DisplayNamePrefix = "User Tag";

        private const string Company = nameof(ASD);
        private const string Product = nameof(ESH);
    }
}