using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web.Http;
using BloggingSystem.Models;
using BloggingSystem.WebApi.Controllers;
using BloggingSystem.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace BloggingSystem.UnitTests
{
    [TestClass]
    public class UnitTestPostsController
    {
        static TransactionScope tran;
        private InMemoryHttpServer httpServer;

        [TestInitialize]
        public void TestInit()
        {
            var type = typeof(PostsController);
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

        #region Create post

        [TestMethod]
        public void CreatePost_WithValidData_ShouldSaveToDatabase()
        {
            var testUser = new UserModel()
            {
                Username = "VALIDUSER",
                DisplayName = "VALIDNICK",
                AuthCode = new string('b', 40)
            };
            var userModel = Helpers.RegisterTestValidUser(httpServer, testUser);

            var testPost = new PostModel()
            {
                Title = "NEW POST",
                Tags = new List<string>()
                {
                    "post"
                },
                Text = "this is just a test post"
            };

            var response = httpServer.Post("api/posts?sessionKey=" + userModel.SessionKey, testPost);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsNotNull(response.Content);

            var contentString = response.Content.ReadAsStringAsync().Result;
            var postModel = JsonConvert.DeserializeObject<ResponsePostModel>(contentString);

            Assert.AreEqual(postModel.Title, testPost.Title);
            Assert.IsTrue(postModel.Id > 0);
        }

        [TestMethod]
        public void CreatePost_WithNullData_ShouldSaveToDatabase()
        {
            var testUser = new UserModel()
            {
                Username = "VALIDUSER",
                DisplayName = "VALIDNICK",
                AuthCode = new string('b', 40)
            };
            var userModel = Helpers.RegisterTestValidUser(httpServer, testUser);

            var testPost = new PostModel();

            var response = httpServer.Post("api/posts?sessionKey=" + userModel.SessionKey, testPost);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void CreatePost_WithInValidSessionKey_ShouldSaveToDatabase()
        {
            var testPost = new PostModel()
            {
                Title = "NEW POST",
                Tags = new List<string>()
                {
                    "post"
                },
                Text = "this is just a test post"
            };

            var response = httpServer.Post("api/posts?sessionKey=fgfkfkffkk", testPost);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion

        #region SearchByTags

        [TestMethod]
        public void GetPostByTags_WithValidData_ShouldSaveToDatabase()
        {
            var testUser = new UserModel()
            {
                Username = "VALIDUSER",
                DisplayName = "VALIDNICK",
                AuthCode = new string('b', 40)
            };
            var userModel = Helpers.RegisterTestValidUser(httpServer, testUser);

            var testPost = new PostModel()
            {
                Title = "NEW POST created",
                Tags = new List<string>()
                {
                    "post",
                    "web",
                    "api",
                    "root",
                },
                Text = "this is just a test post"
            };

            var createResponse = httpServer.Post("api/posts?sessionKey=" + userModel.SessionKey, testPost);

            var response = httpServer.Get("api/posts?tags=root&sessionKey=" + userModel.SessionKey);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);

            var contentString = response.Content.ReadAsStringAsync().Result;
            var postModel = JsonConvert.DeserializeObject<List<PostModel>>(contentString);

            Assert.IsTrue(postModel.Count == 1);
            Assert.AreEqual(postModel.First().Title, testPost.Title);
            Assert.AreEqual(postModel.First().Text, testPost.Text);
        }

        [TestMethod]
        public void GetPostByTags_WithTagNotExist_ShouldSaveToDatabase()
        {
            var testUser = new UserModel()
            {
                Username = "VALIDUSER",
                DisplayName = "VALIDNICK",
                AuthCode = new string('b', 40)
            };
            var userModel = Helpers.RegisterTestValidUser(httpServer, testUser);

            var testPost = new PostModel()
            {
                Title = "NEW POST created",
                Tags = new List<string>()
                {
                    "post",
                    "web",
                    "api",
                    "root",
                },
                Text = "this is just a test post"
            };

            var createResponse = httpServer.Post("api/posts?sessionKey=" + userModel.SessionKey, testPost);

            var response = httpServer.Get("api/posts?tags=star,wars&sessionKey=" + userModel.SessionKey);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);

            var contentString = response.Content.ReadAsStringAsync().Result;
            var postModel = JsonConvert.DeserializeObject<List<PostModel>>(contentString);

            Assert.IsTrue(postModel.Count == 0);
        }

        [TestMethod]
        public void GetPostByTags_WithNullData_ShouldSaveToDatabase()
        {
            var testUser = new UserModel()
            {
                Username = "VALIDUSER",
                DisplayName = "VALIDNICK",
                AuthCode = new string('b', 40)
            };
            var userModel = Helpers.RegisterTestValidUser(httpServer, testUser);

            var testPost = new PostModel()
            {
                Title = "NEW POST created",
                Tags = new List<string>()
                {
                    "post",
                    "web",
                    "api",
                    "root",
                },
                Text = "this is just a test post"
            };

            var createResponse = httpServer.Post("api/posts?sessionKey=" + userModel.SessionKey, testPost);

            var response = httpServer.Get("api/posts?tags=&sessionKey=" + userModel.SessionKey);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);

            var contentString = response.Content.ReadAsStringAsync().Result;
            var postModel = JsonConvert.DeserializeObject<List<PostModel>>(contentString);

            Assert.IsTrue(postModel.Count == 0);
        }

        #endregion

        #region CreateComment

        [TestMethod]
        public void CreateCommentar_WithValidData_ShouldSaveToDatabase()
        {
            var testUser = new UserModel()
            {
                Username = "VALIDUSER",
                DisplayName = "VALIDNICK",
                AuthCode = new string('b', 40)
            };
            var userModel = Helpers.RegisterTestValidUser(httpServer, testUser);

            var testPost = new PostModel()
            {
                Title = "NEW POST created",
                Tags = new List<string>()
                {
                    "post",
                    "web",
                    "api",
                    "root",
                },
                Text = "this is just a test post"
            };

            var postResponse = httpServer.Post("api/posts?sessionKey=" + userModel.SessionKey, testPost);
            var postContentString = postResponse.Content.ReadAsStringAsync().Result;
            var postModel = JsonConvert.DeserializeObject<PostModel>(postContentString);

            var testComment = new CommentModel()
            {
                Text = "Abe kefi me toq post"
            };

            var commentResponse = httpServer.Put("api/posts/" + postModel.Id + "/comment?sessionKey=" + userModel.SessionKey, testComment);
            Assert.AreEqual(HttpStatusCode.OK, commentResponse.StatusCode);
        }

        [TestMethod]
        public void CreateCommentar_WithNullData_ShouldSaveToDatabase()
        {
            var testUser = new UserModel()
            {
                Username = "VALIDUSER",
                DisplayName = "VALIDNICK",
                AuthCode = new string('b', 40)
            };
            var userModel = Helpers.RegisterTestValidUser(httpServer, testUser);

            var testPost = new PostModel()
            {
                Title = "NEW POST created",
                Tags = new List<string>()
                {
                    "post",
                    "web",
                    "api",
                    "root",
                },
                Text = "this is just a test post"
            };

            var postResponse = httpServer.Post("api/posts?sessionKey=" + userModel.SessionKey, testPost);
            var postContentString = postResponse.Content.ReadAsStringAsync().Result;
            var postModel = JsonConvert.DeserializeObject<PostModel>(postContentString);

            var commentResponse = httpServer.Put("api/posts/" + postModel.Id + "/comment?sessionKey=" + userModel.SessionKey, null);
            Assert.AreEqual(HttpStatusCode.BadRequest, commentResponse.StatusCode);
        }

        [TestMethod]
        public void CreateCommentar_WithWrongPostIdData_ShouldSaveToDatabase()
        {
            var testUser = new UserModel()
            {
                Username = "VALIDUSER",
                DisplayName = "VALIDNICK",
                AuthCode = new string('b', 40)
            };
            var userModel = Helpers.RegisterTestValidUser(httpServer, testUser);

            var testComment = new CommentModel()
            {
                Text = "Abe kefi me toq post"
            };

            var commentResponse = httpServer.Put("api/posts/9999/comment?sessionKey=" + userModel.SessionKey, testComment);
            Assert.AreEqual(HttpStatusCode.BadRequest, commentResponse.StatusCode);
        }

        [TestMethod]
        public void CreateCommentar_WithInvalideSessionKeyData_ShouldSaveToDatabase()
        {
            var testUser = new UserModel()
            {
                Username = "VALIDUSER",
                DisplayName = "VALIDNICK",
                AuthCode = new string('b', 40)
            };
            var userModel = Helpers.RegisterTestValidUser(httpServer, testUser);

            var testPost = new PostModel()
            {
                Title = "NEW POST created",
                Tags = new List<string>()
                {
                    "post",
                    "web",
                    "api",
                    "root",
                },
                Text = "this is just a test post"
            };

            var postResponse = httpServer.Post("api/posts?sessionKey=32543tfg", testPost);
            var postContentString = postResponse.Content.ReadAsStringAsync().Result;
            var postModel = JsonConvert.DeserializeObject<PostModel>(postContentString);

            var testComment = new CommentModel()
            {
                Text = "Abe kefi me toq post"
            };

            var commentResponse = httpServer.Put("api/posts/" + postModel.Id + "/comment?sessionKey=dsadasda", testComment);
            Assert.AreEqual(HttpStatusCode.BadRequest, commentResponse.StatusCode);
        }

        #endregion

    }
}
