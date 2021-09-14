namespace api.Abstractions
{
    // The only reason I'm coding keys names this way instead of using the Enum type is because C# does not allow string as a type for the properties of the enum
    public static class CacheKeys
    {
        public static readonly string Recipes = "recipes";
        public static readonly string Drinks = "drinks";
        public static readonly string Diet = "diet";
    }
}