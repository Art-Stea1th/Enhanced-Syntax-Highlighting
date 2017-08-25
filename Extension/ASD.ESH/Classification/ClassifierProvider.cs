using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace ASD.ESH.Classification {

    using Common;
    using Helpers;

    [Export(typeof(IClassifierProvider))]
    [ContentType(Constants.ContentType)]
    internal sealed class ClassifierProvider : IClassifierProvider {
#pragma warning disable CS0649
        [Import]
        internal IClassificationTypeRegistryService TypeRegistryService; // Set via MEF
#pragma warning restore CS0649
        public IClassifier GetClassifier(ITextBuffer buffer)
            => buffer.Properties.GetOrCreateSingletonProperty(nameof(Classifier),
                () => new Classifier() as IClassifier);

    }
}