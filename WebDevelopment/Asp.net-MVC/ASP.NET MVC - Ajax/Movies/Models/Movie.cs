using System;
using System.Linq;

namespace Movies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Studio { get; set; }
        public string StudioAddress { get; set; }
        public int Year { get; set; }
        public virtual Actor LeadMaleActor { get; set; }
        public virtual Actor LeadFemaleActor { get; set; }
    }
}