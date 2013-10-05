using System.Collections.Generic;
using Musicstore.Server.Models.Interfaces;

namespace Musicstore.Server.Models
{
    public class Album : IIdentifier
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Producer { get; set; }
        public virtual ICollection<Artist> Artists { get; set; }
        public virtual ICollection<Song> Songs { get; set; }

        protected Album(){}

        public Album(string title)
        {
            Title = title;
            Artists = new HashSet<Artist>();
            Songs = new HashSet<Song>();
        }
    }
}
