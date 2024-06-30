namespace Stock.Common
{
    public static class EntityValidationConstants
    {
        public static class StockValidation
        {
            public const string DecimalColumnType = "decimal(18,2)";

            public const int SymbolMinLength = 1;
            public const int SymbolMaxLength = 100;

            public const int CompanyNameMinLength = 3;
            public const int CompanyNameMaxLength = 100;

            public const int IndustryMinLength = 2;
            public const int IndustryMaxLength = 100;
        }

        public static class CommentValidation
        {
            public const int TitleMinLength = 3;
            public const int TitleMaxLength = 50;

            public const int ContentMinLength = 5;
            public const int ContentMaxLength = 250;
        }
    }
}
