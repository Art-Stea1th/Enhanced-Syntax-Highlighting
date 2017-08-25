using Microsoft.VisualStudio.Text.Classification;

namespace ASD.ESH.Common {

    internal sealed class Types {

        internal readonly IClassificationType ParameterType;
        internal readonly IClassificationType PropertyType;

        public Types() {

            //var compositionService = Package.GetGlobalService(typeof(SComponentModel)) as IComponentModel;
            //compositionService.DefaultCompositionService.SatisfyImportsOnce(this);

            //ParameterType = TypeRegistryService.GetClassificationType(Constants.Parameter.TypeName);
            //PropertyType = TypeRegistryService.GetClassificationType(Constants.Property.TypeName);
        }
    }
}