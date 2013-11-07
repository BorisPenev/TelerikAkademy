using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BloggingSystem.Data;

namespace BloggingSystem.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        protected const int SessionKeyLength = 50;

        protected T PerformOperationAndHandleExceptions<T>(Func<T> operation)
        {
            try
            {
                return operation();
            }
            catch (Exception ex)
            {
                var errResponse = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                throw new HttpResponseException(errResponse);
            }
        }

        protected T PerformOperationAndHandleExceptionsWithSessionKey<T>(string sessionKey, BlogContext context, Func<T> operation)
        {
            try
            {
                ValidateSessionKey(sessionKey);
                ValidateSession<T>(sessionKey, context);

                return operation();
            }
            catch (Exception ex)
            {
                var errResponse = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                throw new HttpResponseException(errResponse);
            }
        }

        private static void ValidateSession<T>(string sessionKey, BlogContext context)
        {
            if (context.Users.FirstOrDefault(usr => usr.SessionKey == sessionKey) == null)
            {
                throw new InvalidOperationException("Invalid Session");
            }
        }

        protected static void ValidateSessionKey(string sessionKey)
        {
            if (sessionKey == null || sessionKey.Length != SessionKeyLength)
            {
                throw new InvalidOperationException("Invalid Session");
            }
        }
    }
}
