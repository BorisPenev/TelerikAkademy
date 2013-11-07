using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

         [Required]
        public PriorityType Priority { get; set; }

        public string ScreenshotURL { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public Ticket()
        {
            Comments = new HashSet<Comment>();
        }
    }

    public enum PriorityType
    {
        Medium,
        Low,
        High
    }
}
