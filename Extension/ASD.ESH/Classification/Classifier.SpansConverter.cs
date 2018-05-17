// Copyright (c) "ESH-Repository" source code contributors. All Rights Reserved.
// Licensed under the Microsoft Public License (MS-PL).
// See LICENSE.md in the "ESH-Repository" root for license information.
// "ESH-Repository" root address: https://github.com/Art-Stea1th/Enhanced-Syntax-Highlighting

using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

using CS = Microsoft.CodeAnalysis.CSharp;
using VB = Microsoft.CodeAnalysis.VisualBasic;

namespace ASD.ESH.Classification {
    internal sealed partial class Classifier {
        internal sealed class SpansConverter {
            private readonly SemanticModel model;
            private readonly SyntaxNode root;
            private readonly ITextSnapshot snapshot;

            public SpansConverter(SemanticModel model, SyntaxNode root, ITextSnapshot snapshot) {
                this.model = model; this.root = root; this.snapshot = snapshot;
            }

            public IEnumerable<ClassificationSpan> ConvertAll(IEnumerable<ClassifiedSpan> spans) {
                return spans
                    .Where(s => s.ClassificationType == ClassificationTypeNames.Identifier
                        || (s.ClassificationType.EndsWith("name") && !s.ClassificationType.StartsWith("xml")))
                    .Select(Convert)
                    .Where(cs => cs != null);
            }

            private ClassificationSpan Convert(ClassifiedSpan span) {
                var symbol = GetSymbol(span.TextSpan);
                if (symbol == null) { return null; }

                var type = TypesRegistry.ResolveType(symbol);
                if (type == null) { return null; }

                return CreateSpan(span.TextSpan, type);
            }

            private ISymbol GetSymbol(TextSpan textSpan) {
                var expressionSyntaxNode = GetExpression(root.FindNode(textSpan));

                return model.GetSymbolInfo(expressionSyntaxNode).Symbol
                    ?? model.GetDeclaredSymbol(expressionSyntaxNode);
            }

            private SyntaxNode GetExpression(SyntaxNode node) {
                switch (node) {
                    case CS.Syntax.ArgumentSyntax s:
                        return s.Expression;
                    case CS.Syntax.AttributeArgumentSyntax s:
                        return s.Expression;
                    case VB.Syntax.SimpleArgumentSyntax s:
                        return s.Expression;
                    default:
                        return node;
                }
            }

            private ClassificationSpan CreateSpan(TextSpan span, IClassificationType type)
                => new ClassificationSpan(new SnapshotSpan(snapshot, new Span(span.Start, span.Length)), type);
        }
    }
}