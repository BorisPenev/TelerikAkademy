using System.Collections.Generic;
using Musicstore.Server.Models.Interfaces;

namespace Musicstore.Server.Models
{
    //[DataContract(IsReference = true)]
    public class Song : IIdentifier
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual ICollection<Album> Albums { get; set; }

        protected Song(){}

        public Song(string title)
        {
            Title = title;
            Albums = new HashSet<Album>();
        }
    }
}
