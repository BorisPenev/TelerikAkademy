using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using BloggingSystem.Data;
using BloggingSystem.Models;
using BloggingSystem.WebApi.Models;

namespace BloggingSystem.WebApi.Controllers
{
    public class UsersController : BaseApiController
    {
        private const int MinUsernameLength = 6;
        private const int MaxUsernameLength = 30;
        private const string ValidUsernameCharacters =
            "qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM1234567890_.";
        private const string ValidNicknameCharacters =
            "qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM1234567890_. -";

        private const string SessionKeyChars =
            "qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM";
        private static readonly Random rand = new Random();

        private const int Sha1Length = 40;

        //POST api/users/register
        [ActionName("register")]
        public HttpResponseMessage PostRegisterUser(UserModel model)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(() =>
            {
                var context = new BlogContext();
                using (context)
                {
                    this.ValidateUsername(model.Username);
                    this.ValidateNickname(model.DisplayName);
                    this.ValidateAuthCode(model.AuthCode);
                    var usernameToLower = model.Username.ToLower();
                    var nicknameToLower = model.DisplayName.ToLower();
                    var user = context.Users.FirstOrDefault(
                        usr => usr.Username == usernameToLower
                               || usr.DisplayName.ToLower() == nicknameToLower);

                    if (user != null)
                    {
                        throw new InvalidOperationException("Users exists");
                    }

                    user = new User()
                    {
                        Username = usernameToLower,
                        DisplayName = model.DisplayName,
                        AuthCode = model.AuthCode
                    };

                    context.Users.Add(user);
                    context.SaveChanges();

                    user.SessionKey = this.GenerateSessionKey(user.Id);
                    context.SaveChanges();

                    var loggedModel = new LoggedUserModel()
                    {
                        DisplayName = user.DisplayName,
                        SessionKey = user.SessionKey
                    };

                    var response =
                        this.Request.CreateResponse(HttpStatusCode.Created,
                            loggedModel);
                    return response;
                }
            });

            return responseMsg;
        }

        //POST api/users/login
        [ActionName("login")]
        public HttpResponseMessage PostLoginUser(UserModel model)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(() =>
            {
                var context = new BlogContext();
                using (context)
                {
                    this.ValidateUsername(model.Username);
                    this.ValidateAuthCode(model.AuthCode);
                    var usernameToLower = model.Username.ToLower();
                    var user = context.Users.FirstOrDefault(
                        usr => usr.Username == usernameToLower
                        && usr.AuthCode == model.AuthCode);

                    if (user == null)
                    {
                        throw new InvalidOperationException("Invalid username or password");
                    }

                    if (user.SessionKey == null)
                    {
                        user.SessionKey = this.GenerateSessionKey(user.Id);
                        context.SaveChanges();
                    }

                    var loggedModel = new LoggedUserModel()
                    {
                        DisplayName = user.DisplayName,
                        SessionKey = user.SessionKey
                    };

                    var response =
                        this.Request.CreateResponse(HttpStatusCode.Created,
                                        loggedModel);
                    return response;
                }
            });

            return responseMsg;
        }

        //PUT api/users/logout
        [ActionName("logout")]
        public HttpResponseMessage PutLogoutUser(string sessionKey)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(() =>
            {
                ValidateSessionKey(sessionKey);

                var context = new BlogContext();
                using (context)
                {
                    var user = context.Users.FirstOrDefault(u => u.SessionKey == sessionKey);
                    if (user != null)
                    {
                        user.SessionKey = null;
                        context.SaveChanges();
                    }
                }

                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                return response;
            });
            return responseMsg;
        }

        private void ValidateUsername(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException("Username cannot be null");
            }

            if (username.Length < MinUsernameLength)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("Username must be at least {0} characters long",
                        MinUsernameLength));
            }

            if (username.Length > MaxUsernameLength)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("Username must be less than {0} characters long",
                        MaxUsernameLength));
            }

            if (username.Any(ch => !ValidUsernameCharacters.Contains(ch)))
            {
                throw new ArgumentOutOfRangeException(
                    "Username must contain only Latin letters, digits .,_");
            }
        }

        private void ValidateNickname(string nickname)
        {
            if (nickname == null)
            {
                throw new ArgumentNullException("nickname cannot be null");
            }

            if (nickname.Length < MinUsernameLength)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("nickname must be at least {0} characters long",
                        MinUsernameLength));
            }

            if (nickname.Length > MaxUsernameLength)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("nickname must be less than {0} characters long",
                        MaxUsernameLength));
            }

            if (nickname.Any(ch => !ValidNicknameCharacters.Contains(ch)))
            {
                throw new ArgumentOutOfRangeException(
                    "nickname must contain only Latin letters, digits .,_");
            }
        }

        private void ValidateAuthCode(string authCode)
        {
            if (authCode == null || authCode.Length != Sha1Length)
            {
                throw new ArgumentOutOfRangeException("Password should be encrypted");
            }
        }

        private string GenerateSessionKey(int userId)
        {
            var skeyBuilder = new StringBuilder(SessionKeyLength);
            skeyBuilder.Append(userId);
            while (skeyBuilder.Length < SessionKeyLength)
            {
                var index = rand.Next(SessionKeyChars.Length);
                skeyBuilder.Append(SessionKeyChars[index]);
            }
            return skeyBuilder.ToString();
        }
    }
}
