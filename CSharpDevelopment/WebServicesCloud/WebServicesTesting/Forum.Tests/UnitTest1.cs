using System;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using Newtonsoft.Json;
using WebServicesTesting.Model;
using WebServicesTesting.WebApi.Models;

namespace Forum.Tests
{
    [TestClass]
    public class ThreadsControllerIntegrationTests
    {
        [TestMethod]
        public void GetAll_WhenDataInDatabase_ShouldReturnData()
        {
            var test = new testModel();
            var httpServer = new InMemoryHttpServer("http://localhost/");
            var response = httpServer.CreateGetRequest("api/school");
        }

        static TransactionScope tran;

        [TestInitialize]
        public void TestInit()
        {
            tran = new TransactionScope();
        }

        [TestCleanup]
        public void TearDown()
        {
            tran.Dispose();
        }



        [TestMethod]
        public void Register_WhenUserModelValid_ShouldSaveToDatabase()
        {
            var testSchool = new School()
            {
                Name = "test school",
                Location = "bg"
            };
            var httpServer = new InMemoryHttpServer("http://localhost/");
            var response = httpServer.CreatePostRequest("api/school/posts", testSchool);

            //Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            //Assert.IsNotNull(response.Content);

            //var contentString = response.Content.ReadAsStringAsync().Result;
            //var model = JsonConvert.DeserializeObject<LoggedUserModel>(contentString);
            //Assert.AreEqual(testUser.Nickname, model.Nickname);
            //Assert.IsNotNull(model.SessionKey);
        }
    }
}
