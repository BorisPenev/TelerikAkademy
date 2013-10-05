using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CodeJewelApi.Controllers
{
    public class BaseController : ApiController
    {
        protected HttpResponseMessage PerformOperation<T>(Func<T> action)
        {
            try
            {
                var result = action();
                return Request.CreateResponse(HttpStatusCode.OK,result);
            }
            
            catch (Exception ex)
            {
                var response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                return response;
            }
        }

        protected HttpResponseMessage PerformOperation(Action action)
        {
            try
            {
                action();
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            catch (Exception ex)
            {
                var response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                return response;
            }
        }
    }
}
