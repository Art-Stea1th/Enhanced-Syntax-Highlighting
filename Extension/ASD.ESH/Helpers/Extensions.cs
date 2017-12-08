// Copyright (c) Stanislav Kuzmich.  All Rights Reserved.
// Licensed under the Microsoft Public License (MS-PL).
// See License.txt in the project for license information.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.CodeAnalysis.Text;

namespace ASD.ESH.Helpers {

    internal static class Extensions {

        public static async Task<IEnumerable<ClassifiedSpan>> GetClassifiedSpansAsync(
            this Document document, TextSpan textSpan, CancellationToken cancellationToken = default(CancellationToken))
            => await Classifier.GetClassifiedSpansAsync(document, textSpan, cancellationToken);
    }
}