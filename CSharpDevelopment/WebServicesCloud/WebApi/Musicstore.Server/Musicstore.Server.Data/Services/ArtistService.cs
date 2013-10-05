using System;
using System.Collections.Generic;
using Musicstore.Server.Data.Interfaces;
using Musicstore.Server.Models;
using WebGrease.Css.Extensions;

namespace Musicstore.Server.Data.Services
{
    public class ArtistService
    {
        private readonly IRepository _repository;
        //private readonly IRepository<Artist> _artistRepository;
        //private readonly IRepository<Album> _albumRepository;
        //private readonly IRepository<Song> _songRepository;

        public ArtistService(IRepository repository)
                             //IRepository<Artist> artistRepository,
                             //IRepository<Album> albumRepository,
                             //IRepository<Song> songRepository)
        {
            _repository = repository;
            //_albumRepository = albumRepository;
            //_artistRepository = artistRepository;
            //_songRepository = songRepository;
        }

        public T PostService<T>(T value)
        {
            CheckArtistsInAlbums(value as Album);
            return value;
        }

        public T PutService<T>(T value)
        {
            CheckArtistsInAlbums(value as Album);
            return value;
        }

        private void CheckArtistsInAlbums(Album album)
        {
            var artists = new List<Artist>();
            album.Artists.ForEach(artist =>
            {
                var a = _repository.Find<Artist>(x => x.Id == artist.Id);
                if (a == null)
                {
                    a = new Artist(artist.Name);
                    a.DateOfBirth = DateTime.Now;
                }
                artists.Add(a);
            });
            album.Artists = artists;
        }
    }
}
