using Microsoft.VisualStudio.Text.Classification;

namespace ASD.ESH.Common {

    internal sealed class Types {

        private IClassificationTypeRegistryService service;

        public IClassificationType ParameterType { get; private set; }
        public IClassificationType PropertyType { get; private set; }

        public void OneTimeInitialize(IClassificationTypeRegistryService service) {
            if (this.service == null && service != null) {
                FillTypes(this.service = service);
            }
        }
        private void FillTypes(IClassificationTypeRegistryService service) {
            ParameterType = service.GetClassificationType(Constants.Parameter.TypeName);
            PropertyType = service.GetClassificationType(Constants.Property.TypeName);
        }
    }
}