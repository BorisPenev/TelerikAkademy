using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CodeJewelModels
{
    public class CodeJewel
    {
        public CodeJewel()
        {
            this.Votes = new HashSet<Vote>();
        }

        public int Id { get; set; }
        public Category Category { get; set; }
        [Required]
        public string AuthorMail { get; set; }
         [Required]
        public string SourceCode { get; set; }

        public ICollection<Vote> Votes { get; set; }

    }
}
