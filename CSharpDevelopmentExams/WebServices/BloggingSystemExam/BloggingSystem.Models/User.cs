namespace BloggingSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } // unique
        public string DisplayName { get; set; }
        public string AuthCode { get; set; }
        public string SessionKey { get; set; }
    }
}
