using System;

namespace api
{
    public class RecipeData
    {
        public Recipe recipe { get; set; }
    }
    
    public class Recipe
    {
        public string uri { get; set; }

        public string label { get; set; }

        public string image { get; set; }

        public string[] cautions { get; set; }

        public Ingredient[] ingredients { get; set; }

        public double calories { get; set; }

        public string[] cuisineType { get; set; }

        public string[] mealType { get; set; }

        public string[] dishType { get; set; }

        public string url { get; set; }
    }

    public class Ingredient
    {
        public string text { get; set; }

        public string image { get; set; }
    }
}
