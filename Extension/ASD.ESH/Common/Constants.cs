namespace ASD.ESH.Common {

    internal static class Constants {

        private const string company = nameof(ASD);
        private const string product = nameof(ESH);

        private static class DName {
            public const string Prefix = "User Tag";
            public const string Separator = " - ";
        }
        private static class TName {
            public const string Prefix = company + Separator + product;
            public const string Separator = ".";
        }


        public const string ContentType = "CSharp";

        internal static class Parameter {
            public const string FormatName = nameof(Parameter);
            public const string DisplayName = DName.Prefix + DName.Separator + FormatName;
            public const string TypeName = TName.Prefix + TName.Separator + FormatName;
        }

        internal static class Property {
            public const string FormatName = nameof(Property);
            public const string DisplayName = DName.Prefix + DName.Separator + FormatName;
            public const string TypeName = TName.Prefix + TName.Separator + FormatName;
        }
    }
}