using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace ASD.ESH.Classification {

    [Export(typeof(IClassifierProvider))]
    [ContentType("CSharp")]
    internal sealed class ClassifierProvider : IClassifierProvider {
#pragma warning disable CS0649

        [Import]
        internal IClassificationTypeRegistryService TypeRegistryService; // set via MEF
        [Import]
        internal IClassificationFormatMapService FormatMapService; // set via MEF

#pragma warning restore CS0649
        public IClassifier GetClassifier(ITextBuffer buffer) {

            buffer.Properties.GetOrCreateSingletonProperty(nameof(ClassificationTypes),
                () => new ClassificationTypes(TypeRegistryService, FormatMapService));

            return buffer.Properties.GetOrCreateSingletonProperty(nameof(Classifier),
                () => new Classifier(buffer) as IClassifier);
        }
    }
}