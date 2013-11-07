using System.ComponentModel.DataAnnotations;

namespace Exam.Web.Models
{
    public class SubmitCommentModel
    {
        [Required]
        public string Comment { get; set; }

        [Required]
        public int TicketId { get; set; }
    }
}