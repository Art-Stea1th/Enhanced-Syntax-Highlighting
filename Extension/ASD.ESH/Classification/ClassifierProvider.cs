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

        [Import]
        private IClassificationTypeRegistryService registryService; // set via MEF
        [Import]
        private IClassificationFormatMapService   formatMapService; // set via MEF

#pragma warning restore CS0649

        private bool initialized = false;

        public IClassifier GetClassifier(ITextBuffer textBuffer) {

            if (!initialized) {
                Initialize(textBuffer);
            }
            return Container.Resolve<IClassifier>();
        }

        private void Initialize(ITextBuffer textBuffer) {
            try {
                Container.Register(registryService);
                Container.Register(formatMapService);
                Container.Register<ClassificationTypes>();
                Container.Register(textBuffer);
                Container.Register<IClassifier, Classifier>();
                initialized = true;
            }
            catch { initialized = false; }
        }
    }
}