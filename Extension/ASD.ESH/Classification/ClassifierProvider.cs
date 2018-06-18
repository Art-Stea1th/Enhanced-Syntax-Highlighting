// Copyright (c) "ESH-Repository" source code contributors. All Rights Reserved.
// Licensed under the Microsoft Public License (MS-PL).
// See LICENSE.md in the "ESH-Repository" root for license information.
// "ESH-Repository" root address: https://github.com/Art-Stea1th/Enhanced-Syntax-Highlighting

using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace ASD.ESH.Classification {

    [Export(typeof(IClassifierProvider))]
    [ContentType("CSharp"), ContentType("Basic")]
    internal sealed class ClassifierProvider : IClassifierProvider {

#pragma warning disable CS0649, IDE0044, RCS1169
        [Import] private IClassificationTypeRegistryService registryService; // set via MEF
#pragma warning restore CS0649, IDE0044, RCS1169

        IClassifier IClassifierProvider.GetClassifier(ITextBuffer textBuffer) {

            return textBuffer.Properties.GetOrCreateSingletonProperty(
                creator: () => {
                    TypesRegistry.InitializeIfNeeded(registryService);
                    return new Classifier();
                });
        }
    }
}