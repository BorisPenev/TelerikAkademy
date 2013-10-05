using System.Collections.Generic;
using System.Net;
using System.Transactions;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebServicesTesting.WebApi.Models;

namespace WebServicesTesting.Tests
{
    [TestClass()]
    public class SchoolControllerTest
    {
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

        [TestMethod()]
        public void GetTest()
        {
            var schoolModels = new List<SchoolModel>();

            var httpServer = new InMemoryHttpServer("http://localhost:4728/");
            var response = httpServer.CreateGetRequest("api/school");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            PostTest();

            var httpServer = new InMemoryHttpServer("http://localhost:4728/");
            var response = httpServer.CreateGetRequest("api/school");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);

            var contentString = response.Content.ReadAsStringAsync().Result;
            var model = JsonConvert.DeserializeObject<SchoolModel>(contentString);

            Assert.IsTrue(model.Id == 1);
        }

        [TestMethod()]
        public void PostTest()
        {
            SchoolModel school = new SchoolModel()
            {
                Location = "bg",
                Name = "lelq vachka"
            };

            var httpServer = new InMemoryHttpServer("http://localhost:4728/");
            var response = httpServer.CreatePostRequest("api/school", school);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsNotNull(response.Content);

            var contentString = response.Content.ReadAsStringAsync().Result;
            var model = JsonConvert.DeserializeObject<SchoolModel>(contentString);

            Assert.IsTrue(model.Id > 0);
        }
    }
}
