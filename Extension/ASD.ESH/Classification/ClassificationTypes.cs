using System;
using Microsoft.VisualStudio.Text.Classification;

namespace ASD.ESH.Classification {

    using Common;

    internal sealed class ClassificationTypes {

        public IClassificationType ParameterType { get; private set; }
        public IClassificationType PropertyType { get; private set; }
        public IClassificationType MethodType { get; private set; }

        public bool Initialized => ParameterType != null && PropertyType != null;

        public void OneTimeInitialize(IClassificationTypeRegistryService service) {

            if (!Initialized && service != null) {

                ParameterType = GetClassificationType(service, Constants.Parameter.TypeName);
                PropertyType = GetClassificationType(service, Constants.Property.TypeName);
                MethodType = GetClassificationType(service, Constants.Method.TypeName);
            }
        }

        private IClassificationType GetClassificationType(IClassificationTypeRegistryService service, string typeName)
            => service.GetClassificationType(typeName)
            ?? throw new ArgumentNullException(typeName, $"'{typeName}' not found in the Registry.");
    }
}