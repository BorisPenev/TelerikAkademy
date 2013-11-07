using System.Collections.Generic;

namespace BloggingSystem.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Post> Posts { get; set; }

        public Tag()
        {
            this.Posts = new HashSet<Post>();
        }
    }
}
