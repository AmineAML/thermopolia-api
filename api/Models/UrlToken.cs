namespace api.Models
{
    public class UrlToken
    {
        public int ID { get; set; }
        public string RandomGeneratedString { get; set; }
        // ID reference of the subscriber
        public int Subscriber { get; set; }
    }
}