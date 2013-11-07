using System;

namespace BloggingSystem.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime PostDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
