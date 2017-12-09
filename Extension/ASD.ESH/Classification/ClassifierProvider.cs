using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace ASD.ESH.Classification {

    [Export(typeof(IClassifierProvider))]
    [ContentType("CSharp")]
    internal sealed class ClassifierProvider : IClassifierProvider {

#pragma warning disable CS0649
        [Import] private IClassificationTypeRegistryService registryService; // set via MEF
#pragma warning restore CS0649

        IClassifier IClassifierProvider.GetClassifier( ITextBuffer textBuffer ) {
            return textBuffer.Properties.GetOrCreateSingletonProperty(
                creator: () => {
                    TypesRegistry.InitializeIfNeeded( registryService );
                    return new Classifier();
                }
            );
        }
    }
}