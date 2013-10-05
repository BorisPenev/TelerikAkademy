using Musicstore.Server.Data.Helpers;
using Musicstore.Server.Data.Interfaces;
using Musicstore.Server.Models;

namespace Musicstore.Server.WebApi.Controllers
{
    public class SongController : BaseApiController<Song>
    {
        public SongController(IRepository repository)
            : base(repository)
        {
            Includes = new[] { "Artist", "Albums" };
        }

        //private readonly IRepository<Song> _songRepository;

        //public SongController(IRepository<Song> songRepository)
        //{
        //    _songRepository = songRepository;
        //}

        //// GET api/Song
        //public IQueryable<Song> GetSongs()
        //{
        //    var songs = _songRepository.GetAll.Include("Artist").Include("Albums");
        //    return songs;
        //}

        //// GET api/Song/5
        //public Song GetSong(int id)
        //{
        //    Song song = _songRepository.GetById(id);// db.Songs.Find(id);
        //    if (song == null)
        //    {
        //        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
        //    }

        //    return song;
        //}

        //// PUT api/Song/5
        //public HttpResponseMessage PutSong(int id, Song song)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }

        //    if (id != song.Id)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }

        //    //db.Entry(song).State = EntityState.Modified;

        //    try
        //    {
        //        var entity = _songRepository.Update(song);
        //        return Request.CreateResponse(HttpStatusCode.OK, entity);
        //        //db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
        //    }
        //}

        //// POST api/Song
        //public HttpResponseMessage PostSong(Song song)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var entity = _songRepository.Add(song);

        //        //db.Songs.Add(song);
        //        //db.SaveChanges();

        //        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, entity);
        //        return response;
        //    }
        //    else
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }
        //}

        //// DELETE api/Song/5
        //public HttpResponseMessage DeleteSong(int id)
        //{
        //    //Song song = db.Songs.Find(id);
        //    //if (song == null)
        //    //{
        //    //    return Request.CreateResponse(HttpStatusCode.NotFound);
        //    //}

        //    //db.Songs.Remove(song);

        //    try
        //    {
        //        _songRepository.Delete(id);
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
    }
}