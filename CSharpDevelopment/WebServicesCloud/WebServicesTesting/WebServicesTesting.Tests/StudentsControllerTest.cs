using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Transactions;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebServicesTesting.WebApi.Models;

namespace WebServicesTesting.Tests
{
    [TestClass()]
    public class StudentsControllerTest
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
            var studentModels = new List<StudentModel>();

            var httpServer = new InMemoryHttpServer("http://localhost:4728/");
            var response = httpServer.CreateGetRequest("api/students");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);
        }

        [TestMethod()]
        public void PostTest()
        {
            var student = new StudentModel()
            {
                FirstName = "Mimi",
                LastName = "Chervenopeyka",
                Age = 19,
                Grade = "12a"
            };

            var httpServer = new InMemoryHttpServer("http://localhost:4728/");
            var response = httpServer.CreatePostRequest("api/students", student);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsNotNull(response.Content);

            var contentString = response.Content.ReadAsStringAsync().Result;
            var model = JsonConvert.DeserializeObject<StudentModel>(contentString);

            Assert.IsTrue(model.Id > 0);
        }
    }
}
