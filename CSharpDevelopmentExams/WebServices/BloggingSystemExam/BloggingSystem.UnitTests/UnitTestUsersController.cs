using System;
using System.Collections.Generic;
using System.Net;
using System.Transactions;
using System.Web.Http;
using BloggingSystem.WebApi.Controllers;
using BloggingSystem.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace BloggingSystem.UnitTests
{
    [TestClass]
    public class UnitTestUsersController
    {
        static TransactionScope tran;
        private InMemoryHttpServer httpServer;

        [TestInitialize]
        public void TestInit()
        {
            var type = typeof (UsersController);
            tran = new TransactionScope();

            var routes = new List<Route>
            {
                new Route(
                    "PutCommentsApi",
                    "api/posts/{postId}/comment",
                    new
                    {
                        controller = "posts",
                        action = "PutComment"
                    }),
                new Route(
                    "UsersApi",
                    "api/users/{action}",
                    new
                    {
                        controller = "users"
                    }),

                new Route(
                    "DefaultApi",
                    "api/{controller}/{id}",
                    new {id = RouteParameter.Optional}),
            };
            this.httpServer = new InMemoryHttpServer("http://localhost/", routes);
        }

        [TestCleanup]
        public void TearDown()
        {
            tran.Dispose();
        }

        #region Test Register

        [TestMethod]
        public void Register_WhenUserModelValid_ShouldSaveToDatabase()
        {
            var testUser = new UserModel()
            {
                Username = "VALIDUSER",
                DisplayName = "VALIDNICK",
                AuthCode = new string('b', 40)
            };
            var model = Helpers.RegisterTestValidUser(httpServer, testUser);
            Assert.AreEqual(testUser.DisplayName, model.DisplayName);
            Assert.IsNotNull(model.SessionKey);
        }

        [TestMethod]
        public void Register_WhenUserNameIsNOTValid_ShouldThrowException()
        {
            var testUser = new UserModel()
            {
                Username = "VALI#DUSER",
                DisplayName = "VALIDNICK",
                AuthCode = new string('b', 40)
            };
            var response = httpServer.Post("api/users/register", testUser);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void Register_WhenDisplayNameIsNOTValid_ShouldThrowException()
        {
            var testUser = new UserModel()
            {
                Username = "VALIDUSER",
                DisplayName = "VALID#NICK",
                AuthCode = new string('b', 40)
            };
            var response = httpServer.Post("api/users/register", testUser);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void Register_WhenAuthCodeIsNOTValid_ShouldThrowException()
        {
            var testUser = new UserModel()
            {
                Username = "VALIDUSER",
                DisplayName = "VALIDNICK",
                AuthCode = new string('b', 41)
            };
            var response = httpServer.Post("api/users/register", testUser);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void Register_WhenAuthCodeIsNOTValid2_ShouldThrowException()
        {
            var testUser = new UserModel()
            {
                Username = "VALIDUSER",
                DisplayName = "VALIDNICK",
                AuthCode = new string('b', 39)
            };
            var response = httpServer.Post("api/users/register", testUser);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion

        #region Test Logout

        [TestMethod]
        public void Logout_WhenSessionKeyIsValid_ShouldSaveToDatabase()
        {
            var testUser = new UserModel()
            {
                Username = "VALIDUSER",
                DisplayName = "VALIDNICK",
                AuthCode = new string('b', 40)
            };
            var model = Helpers.RegisterTestValidUser(httpServer, testUser);

            var response = httpServer.Put("api/users/logout?sessionKey=" + model.SessionKey, testUser);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Logout_WhenSessionKeyIsNotValid_ShouldSaveToDatabase()
        {
            var response = httpServer.Put("api/users/logout?sessionKey=ddadadawadwfa", null);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion
    }
}
