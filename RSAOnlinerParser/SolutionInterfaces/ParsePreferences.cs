namespace Preferences
{
    public static class ParsePreferences
    {
        public static string DriverUrl { get; set; } = "http://onliner.by";
        public static int ParseRequestTimeout { get; set; } = 5000;

        public static string[] XPaths { get; set; } =
        {
            @".//li[@class='b-main-navigation__item'][1]",
            @".//li[@class='catalog-navigation-classifier__item '][3]",
            @"//*[@id='container']/div/div[2]/div/div/div[1]/div[3]/div/div[3]/div[1]/div/div[6]",
            @"//*[@id='container']/div/div[2]/div/div/div[1]/div[3]/div/div[3]/div[1]/div/div[6]/div[2]/div/a[1]",
            @"//*[@id='schema-order']/a",
            @"//*[@id='schema-order']/div[2]/div/div[5]"
        };

        public static string PaginationPagesXPath { get; set; } = @".//a[@class='schema-pagination__pages-link']";
        public static string NameXPath { get; set; } = @".//div[@class='schema-product__title']/a/span";
        public static string PriceXPath { get; set; } = @".//div[@class='schema-product__price']/a/span";
        public static string LinkXPath { get; set; } = @".//div[@class='schema-product__title']/a";
        public static string PaginationElementXPath { get; set; } = @"//*[@id='schema-pagination']/div[1]";
        public static string ElementsXPath { get; set; } = @".//div[@class='schema-product__group']";
    }
}