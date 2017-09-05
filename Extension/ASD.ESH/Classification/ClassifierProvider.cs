using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace ASD.ESH.Classification {

    using Helpers;

    [Export(typeof(IClassifierProvider))]
    [ContentType("CSharp")]
    internal sealed class ClassifierProvider : IClassifierProvider {

#pragma warning disable CS0649
        [Import] private IClassificationTypeRegistryService registryService; // set via MEF
        [Import] private IClassificationFormatMapService   formatMapService; // set via MEF
#pragma warning restore CS0649

        private bool initialized = false;

        IClassifier IClassifierProvider.GetClassifier(ITextBuffer textBuffer) {

            if (!initialized) { Initialize(); }

            return new Classifier();
        }

        private void Initialize() {
            Container.Register(new ClassifierTypesRegistry(registryService, formatMapService));
        }
    }
}