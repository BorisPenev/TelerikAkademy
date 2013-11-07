using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exam.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public Category()
        {
            Tickets = new HashSet<Ticket>();
        }
    }
}
