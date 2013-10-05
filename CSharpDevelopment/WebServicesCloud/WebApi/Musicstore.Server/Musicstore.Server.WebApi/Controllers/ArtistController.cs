using Musicstore.Server.Data.Helpers;
using Musicstore.Server.Data.Interfaces;
using Musicstore.Server.Data.Services;
using Musicstore.Server.Models;

namespace Musicstore.Server.WebApi.Controllers
{
    public class ArtistController : BaseApiController<Artist>
    {
        private readonly ArtistService _artistService;

        public ArtistController(IRepository repository)
            : base(repository)
        {
            Includes = new[] {"Albums", "Songs"};
            _artistService = new ArtistService(repository);
        }

        public override void Post(Artist value)
        {
            _artistService.PostService(value);
            base.Post(value);
        }

        public override void Put(Artist value)
        {
            _artistService.PutService(value);
            base.Put(value);
        }

        //private readonly IRepository<Artist> _artistRepository;
        //private readonly IRepository<Album> _albumRepository;

        //public ArtistController(IRepository<Artist> artistRepository, IRepository<Album> albumRepository)
        //{
        //    _artistRepository = artistRepository;
        //    _albumRepository = albumRepository;
        //}

        //// GET api/Artist
        //public IQueryable<Artist> GetArtists()
        //{
        //    return _artistRepository.GetAll;
        //}

        //// GET api/Artist/5
        //public Artist GetArtist(int id)
        //{
        //    Artist artist = _artistRepository.GetAll.Include(x=>x.Albums).Include(x=>x.Songs).FirstOrDefault(x => x.Id == id);//.GetById(id);// db.Artists.Include("Albums").FirstOrDefault(a => a.Id == id);
        //    if (artist == null)
        //    {
        //        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
        //    }

        //    return artist;
        //}

        //// PUT api/Artist/5
        //public HttpResponseMessage PutArtist(int id, Artist artist)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }

        //    if (id != artist.Id)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }

        //    //CheckAlbumsInArtist(artist);
        //    //db.Entry(artist).State = EntityState.Modified;

        //    try
        //    {
        //        var entity = _artistRepository.Update(artist);
        //        return Request.CreateResponse(HttpStatusCode.OK, entity);
        //        //db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK, artist);
        //}

        //// POST api/Artist
        //public HttpResponseMessage PostArtist(Artist artist)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var entity = _artistRepository.Add(artist);

        //        //CheckAlbumsInArtist(artist);

        //        //db.Artists.Add(artist);
        //        //db.SaveChanges();

        //        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, artist);
        //        return response;
        //    }
        //    else
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }
        //}

        //// DELETE api/Artist/5
        //public HttpResponseMessage DeleteArtist(int id)
        //{
        //    //Artist artist = db.Artists.Find(id);
        //    //if (artist == null)
        //    //{
        //    //    return Request.CreateResponse(HttpStatusCode.NotFound);
        //    //}

        //    //db.Artists.Remove(artist);

        //    try
        //    {
        //        _artistRepository.Delete(id);
        //        //db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}

        ////protected override void Dispose(bool disposing)
        ////{
        ////    db.Dispose();
        ////    base.Dispose(disposing);
        ////}

        //private void CheckArtistsInAlbums(Album album)
        //{
        //    var artists = new List<Artist>();
        //    album.Artists.ForEach(artist =>
        //    {
        //        var a = db.Artists.FirstOrDefault(x => x.Id == artist.Id);
        //        if (a == null)
        //        {
        //            a = new Artist();
        //            a.DateOfBirth = DateTime.Now;
        //        }
        //        artists.Add(a);
        //    });
        //    album.Artists = artists;
        //}
    }
}