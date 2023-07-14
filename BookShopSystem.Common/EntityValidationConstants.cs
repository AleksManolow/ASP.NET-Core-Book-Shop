namespace BookShopSystem.Common
{
    public static class EntityValidationConstants
    {
        public static class Genre
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public static class Book
        {
            public const int TitleMinLength = 10;
            public const int TitleMaxLength = 50;

            public const int AuthorMinLength = 10;
            public const int AuthorMaxLength = 50;

            public const int DescriptionMinLength = 50;
            public const int DescriptionMaxLength = 500;

            public const int ImageUrnMaxLength = 2048;

            public const string PriceMinLength = "0";
            public const string PriceMaxLength = "2000";
        }
    }
}
