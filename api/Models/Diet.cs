namespace api.Models
{
    public class DietsContent
    {
        public Diet[] diets { get; set; }
    }

    public class Diet
    {
        public string name { get; set; }

        public string description { get; set; }

        public string url { get; set; }

        public string image { get; set; }

        public string diet { get; set; }
    }
}