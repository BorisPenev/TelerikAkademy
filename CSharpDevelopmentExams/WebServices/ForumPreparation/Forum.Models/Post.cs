using System;
using System.Collections.Generic;
using System.Globalization;

namespace Forum.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Thread> Threads { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public Post()
        {
            this.Threads = new HashSet<Thread>();
            this.Votes = new HashSet<Vote>();
            this.Comments = new HashSet<Comment>();
        }
    }
}
