using System;
using System.Collections.Generic;

namespace BloggingSystem.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime PostDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public Post()
        {
            this.Tags = new HashSet<Tag>();
            this.Comments = new HashSet<Comment>();
        }
    }
}
