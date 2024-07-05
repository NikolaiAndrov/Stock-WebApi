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

            public const string PurchaseMinValue = "0.01";
            public const string PurchaseMaxValue = "50000000";

            public const string LastDivMinValue = "0.01";
            public const string LastDivMaxValue = "100";

            public const long MarketCapMinValue = 1;
            public const long MarketCapMaxValue = 50000000;

            public const int PageMinValue = 1;
            public const int PageMaxValue = int.MaxValue;

            public const int ItemsPerPageMinValue = 1;
            public const int ItemsPerPageMaxValue = 100;
        }

        public static class CommentValidation
        {
            public const int TitleMinLength = 3;
            public const int TitleMaxLength = 50;

            public const int ContentMinLength = 5;
            public const int ContentMaxLength = 250;
        }

        public static class UserValidation
        {
            public const int UserNameMinLength = 3;
            public const int UserNameMaxLength = 25;

            public const int PasswordMinLength = 5;
            public const int PasswordMaxLength = 25;
        }
    }
}
