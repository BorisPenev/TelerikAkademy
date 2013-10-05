using System;
using System.Collections.Generic;
using Musicstore.Server.Models.Interfaces;

namespace Musicstore.Server.Models
{
    public class Artist : IIdentifier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<Song> Songs { get; set; }

        protected Artist(){}

        public Artist(string name)
        {
            this.Name = name;
            this.DateOfBirth = DateTime.Now;
            Albums = new HashSet<Album>();
            Songs = new HashSet<Song>();
        }
    }
}
