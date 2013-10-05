using System;
using System.Linq;

namespace CodeJewelModels
{
   public class Vote
    {
        public int Id { get; set; }

        public int VoteValue { get; set; }
        public string AuthorEmail { get; set; }
    }
}
