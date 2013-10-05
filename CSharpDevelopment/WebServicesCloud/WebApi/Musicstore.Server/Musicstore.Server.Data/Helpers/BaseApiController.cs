using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using Musicstore.Server.Data.Interfaces;
using Musicstore.Server.Models.Interfaces;

namespace Musicstore.Server.Data.Helpers
{
    public class BaseApiController<T> : ApiController where T : class, IIdentifier
    {
        protected IRepository DataStore { get; set; }
        protected string[] Includes { get; set; }

        //public BaseApiController()
        //{
        //    //TODO: USE DEPENDENCY INJECTION FOR DECOUPLING
        //    this.DataStore = new EfRepository();
        //}

        public BaseApiController(IRepository albumRepository)
        {
            DataStore = albumRepository;
        }

        // GET api/<controller>
        public virtual IEnumerable<T> Get()
        {
            return DataStore.All<T>(Includes);
        }

        // GET api/<controller>/5
        public virtual T Get(int id)
        {
            return DataStore.Find<T>(t => t.Id == id, Includes);
        }

        // POST api/<controller>
        public virtual void Post([FromBody]T value)
        {
            try
            {
                DataStore.Create<T>(value);
            }
            catch (OptimisticConcurrencyException ex)
            {
                throw ex;
            }
        }

        // PUT api/<controller>
        public virtual void Put([FromBody]T value)
        {
            DataStore.Update<T>(value);
        }

        // DELETE api/<controller>/5
        public virtual void Delete(int id)
        {
            DataStore.Delete<T>(t => t.Id == id);
        }

        public virtual void Delete([FromBody]T value)
        {
            Delete(value.Id);
        }

        protected IEnumerable GetModelErrors()
        {
            return this.ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }
    }
}
