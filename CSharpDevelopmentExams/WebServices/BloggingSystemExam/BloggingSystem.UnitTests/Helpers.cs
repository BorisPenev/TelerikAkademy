using BloggingSystem.WebApi.Models;
using Newtonsoft.Json;

namespace BloggingSystem.UnitTests
{
    static class Helpers
    {
        public static LoggedUserModel RegisterTestValidUser(InMemoryHttpServer httpServer, UserModel testUser)
        {
            var response = httpServer.Post("api/users/register", testUser);
            var contentString = response.Content.ReadAsStringAsync().Result;
            var userModel = JsonConvert.DeserializeObject<LoggedUserModel>(contentString);
            return userModel;
        }
    }
}
