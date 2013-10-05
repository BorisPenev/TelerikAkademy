using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CinemaReserve.RestAPI.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        protected static Random rand = new Random();

        protected T ExecuteOperationHandleExceptions<T>(Func<T> action)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                var resp = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                throw new HttpResponseException(resp);
            }
        }
    }
}