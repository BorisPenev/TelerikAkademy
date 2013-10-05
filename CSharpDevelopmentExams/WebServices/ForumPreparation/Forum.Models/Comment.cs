using System;

namespace Forum.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int Content { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
        public DateTime CommentDate { get; set; }
    }
}