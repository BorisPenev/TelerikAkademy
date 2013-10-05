using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Nickname { get; set; }

        [Required]
        public string AuthCode { get; set; } //length = 40

        public string SessionKey { get; set; }

        public virtual ICollection<Thread> Threads { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public User()
        {
            this.Threads = new HashSet<Thread>();
            this.Posts = new HashSet<Post>();
            this.Votes = new HashSet<Vote>();
            this.Comments = new HashSet<Comment>();
        }

    }
}
