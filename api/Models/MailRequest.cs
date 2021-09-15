namespace api.Models
{
    public class MailRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string FullName { get; set; }
    }
}