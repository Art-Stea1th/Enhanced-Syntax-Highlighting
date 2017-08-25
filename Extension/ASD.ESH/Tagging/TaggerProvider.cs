using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace ASD.ESH.Tagging {

    using Common;
    using Helpers;

    [Export(typeof(ITaggerProvider))]
    [ContentType(Constants.ContentType)]
    [TagType(typeof(IClassificationTag))]
    internal sealed class TaggerProvider : ITaggerProvider {

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
            => buffer.Properties.GetOrCreateSingletonProperty(nameof(Tagger),
                () => new Tagger() as ITagger<T>);

    }
}