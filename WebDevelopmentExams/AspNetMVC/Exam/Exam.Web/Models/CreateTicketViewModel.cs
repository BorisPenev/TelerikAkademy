using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Exam.Models;

namespace Exam.Web.Models
{
    public class CreateTicketViewModel
    {
        [Required]
        public int SelectedCategory { get; set; }

        public List<SelectListItem> CategoryItems { get; set; }

        [Required]
        [ShouldNotContainBug(ErrorMessage = "The word 'bug' should not used in the title!")]
        public string Title { get; set; }

        [Required]
        public PriorityType SelectedPriority { get; set; }

        public List<SelectListItem> PriorityItems { get; set; }

        public string ScreenshotUrl { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}