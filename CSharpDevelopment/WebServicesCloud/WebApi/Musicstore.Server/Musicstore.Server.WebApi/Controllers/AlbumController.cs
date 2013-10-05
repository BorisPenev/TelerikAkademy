using Musicstore.Server.Data.Helpers;
using Musicstore.Server.Data.Interfaces;
using Musicstore.Server.Data.Services;
using Musicstore.Server.Models;

namespace Musicstore.Server.WebApi.Controllers
{
    //[AutofacControllerConfiguration]
    public class AlbumController : BaseApiController<Album>
    {
        private readonly AlbumService _albumService;

        public AlbumController(IRepository repository)
            : base(repository)
        {
            Includes = new[] { "Artists", "Songs" };
            _albumService = new AlbumService(repository);
        }

        public override void Post(Album value)
        {
            _albumService.PostService(value);
            base.Post(value);
        }

        public override void Put(Album value)
        {
            _albumService.PutService(value);
            base.Put(value);
        }

        //private readonly IRepository<Album> _albumRepository;

        //public AlbumController(IRepository<Album> albumRepository)
        //{
        //    _albumRepository = albumRepository;
        //}

        //// GET api/Album
        //public IQueryable<Album> GetAlbums()
        //{
        //    return _albumRepository.GetAll;
        //}

        //// GET api/Album/5
        //public Album GetAlbum(int id)
        //{
        //    Album album = _albumRepository.GetAll.Include(x => x.Artists).Include(x => x.Songs).FirstOrDefault(x => x.Id == id);// db.Albums.Include("Artists").FirstOrDefault(a => a.Id == id);
        //    if (album == null)
        //    {
        //        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
        //    }

        //    return album;
        //}

        //// PUT api/Album/5
        //public HttpResponseMessage PutAlbum(int id, Album album)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }

        //    if (id != album.Id)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }

        //    //CheckArtistsInAlbums(album);
        //    //db.Entry(album).State = EntityState.Modified;

        //    try
        //    {
        //        album.Artists.ApplyCurrentValues();
        //        var entity = _albumRepository.Update(album);
        //        return Request.CreateResponse(HttpStatusCode.OK, entity);
        //        //db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
        //    }
        //}

        //// POST api/Album
        //public HttpResponseMessage PostAlbum(Album album)
        //{
            
        //    if (ModelState.IsValid)
        //    {
        //        var entity = _albumRepository.Add(album);

        //        //CheckArtistsInAlbums(album);

        //        //db.Albums.Add(album);
        //        //db.SaveChanges();

        //        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, entity);
        //        return response;
        //    }
        //    else
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }
        //}

        //// DELETE api/Album/5
        //public HttpResponseMessage DeleteAlbum(int id)
        //{
            
        //    //Album album = db.Albums.Find(id);
        //    //if (album == null)
        //    //{
        //    //    return Request.CreateResponse(HttpStatusCode.NotFound);
        //    //}

        //    //db.Albums.Remove(album);

        //    try
        //    {
        //        _albumRepository.Delete(id);
        //        //db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}


    }
}