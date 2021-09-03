namespace api.Models
{
    public class RecipesSearchAPI
    {
        public int from { get; set; }

        public int to { get; set; }

        public int count { get; set; }

        public Rec[] hits { get; set; }
    }

    public class Rec
    {
        public Recipe recipe { get; set; }
    }
}