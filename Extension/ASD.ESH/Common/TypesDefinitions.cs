using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace ASD.ESH.Common {

    internal static class TypesDefinitions {

#pragma warning disable CS0169

        [Export(typeof(ClassificationTypeDefinition)), Name(Constants.Parameter.TypeName)]
        private static ClassificationTypeDefinition parameterType;

        [Export(typeof(ClassificationTypeDefinition)), Name(Constants.Property.TypeName)]
        private static ClassificationTypeDefinition propertyType;

        [Export(typeof(ClassificationTypeDefinition)), Name(Constants.Method.TypeName)]
        private static ClassificationTypeDefinition methodType;

#pragma warning restore CS0169
    }
}