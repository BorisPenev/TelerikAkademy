using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Documents;
using CinemaReserve.ResponseModels;

namespace CinemaReserve.WpfClient.Data
{
    public static class LoadData
    {
        private const string BaseServicesUrl = "http://localhost:50971/api/";

        public static void GetAllCinemas(Action<object> action)
        {
            var bw = new BackgroundWorker();
            bw.DoWork += ((se, ea) =>
            {
                try
                {
                    var courseByIdResponse =
                    HttpRequester.Get<List<CinemaModel>>(BaseServicesUrl + "cinemas");
                    ea.Result = courseByIdResponse;
                }
                catch (Exception ex)
                {
                    ea.Result = "Database Error! Please try again.";
                }
                
            });
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += ((se, ea) =>
            {
                if (action != null)
                {
                    action(ea.Result);
                }
            });
        }

        public static void GetAllMovies(Action<object> action)
        {
            var bw = new BackgroundWorker();
            bw.DoWork += ((se, ea) =>
            {
                try
                {
                    var moveisResponse = HttpRequester.Get<List<MovieModel>>(BaseServicesUrl + "movies");
                    ea.Result = moveisResponse;
                }
                catch (Exception ex)
                {
                    ea.Result = "Database Error! Please try again.";
                }

            });
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += ((se, ea) =>
            {
                if (action != null)
                {
                    action(ea.Result);
                }
            });
        }

        public static void GetMoviesByCinemaId(int cinemaId, Action<object> action)
        {
            var bw = new BackgroundWorker();
            bw.DoWork += ((se, ea) =>
            {
                try
                {
                    var moveisResponse = HttpRequester.Get<List<MovieModel>>(BaseServicesUrl + "cinemas/" + cinemaId);
                    ea.Result = moveisResponse;
                }
                catch (Exception ex)
                {
                    ea.Result = "Database Error! Please try again.";
                }

            });
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += ((se, ea) =>
            {
                if (action != null)
                {
                    action(ea.Result);
                }
            });
        }

        public static void GetMoviesById(int id, Action<object> action)
        {
            var bw = new BackgroundWorker();
            bw.DoWork += ((se, ea) =>
            {
                try
                {
                    var moveisResponse = HttpRequester.Get<MovieDetailsModel>(BaseServicesUrl + "movies/" + id);
                    ea.Result = moveisResponse;
                }
                catch (Exception ex)
                {
                    ea.Result = "Database Error! Please try again.";
                }

            });
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += ((se, ea) =>
            {
                if (action != null)
                {
                    action(ea.Result);
                }
            });
        }

        public static void GetProjection(int cinemaId,int movieId, Action<object> action)
        {
            var bw = new BackgroundWorker();
            bw.DoWork += ((se, ea) =>
            {
                try
                {
                    var moveisResponse = HttpRequester.Get<List<ProjectionModel>>(BaseServicesUrl + "cinemas/" + cinemaId + "/projections/" + movieId);
                    ea.Result = moveisResponse;
                }
                catch (Exception ex)
                {
                    ea.Result = "Database Error! Please try again.";
                }

            });
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += ((se, ea) =>
            {
                if (action != null)
                {
                    action(ea.Result);
                }
            });
        }

        public static void GetProjectionById(int projectionId, Action<object> action)
        {
            var bw = new BackgroundWorker();
            bw.DoWork += ((se, ea) =>
            {
                try
                {
                    var moveisResponse = HttpRequester.Get<ProjectionDetailsModel>(BaseServicesUrl + "projections/" + projectionId);
                    ea.Result = moveisResponse;
                }
                catch (Exception ex)
                {
                    ea.Result = "Database Error! Please try again.";
                }

            });
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += ((se, ea) =>
            {
                if (action != null)
                {
                    action(ea.Result);
                }
            });
        }

        public static void Reservation(int projectionId, CreateReservationModel model, Action<object> action)
        {
            var bw = new BackgroundWorker();
            bw.DoWork += ((se, ea) =>
            {
                try
                {
                    var moveisResponse = HttpRequester.Post<ReservationModel>(BaseServicesUrl + "projections/" + projectionId, model);
                    ea.Result = moveisResponse;
                }
                catch (Exception ex)
                {
                    ea.Result = "Database Error! Please try again.";
                }

            });
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += ((se, ea) =>
            {
                if (action != null)
                {
                    action(ea.Result);
                }
            });
        }

        public static void CancelReservation(int projectionId, RejectReservationModel model, Action<object> action)
        {
            var bw = new BackgroundWorker();
            bw.DoWork += ((se, ea) =>
            {
                try
                {
                    var moveisResponse = HttpRequester.Put(BaseServicesUrl + "projections/" + projectionId, model);
                    ea.Result = moveisResponse;
                }
                catch (Exception ex)
                {
                    ea.Result = "Database Error! Please try again.";
                }

            });
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += ((se, ea) =>
            {
                if (action != null)
                {
                    action(ea.Result);
                }
            });
        }

        public static void Search(string qeryString, Action<object> action)
        {
            var bw = new BackgroundWorker();
            bw.DoWork += ((se, ea) =>
            {
                try
                {
                    var searchResponse = HttpRequester.Get<List<MovieModel>>(BaseServicesUrl + "movies?keyword=" + qeryString);
                    ea.Result = searchResponse;
                }
                catch (Exception ex)
                {
                    ea.Result = "Database Error! Please try again.";
                }
            });
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += ((se, ea) =>
            {
                if (action != null)
                {
                    action(ea.Result);
                }
            });
        }

        //public static void LoadAllCourses(Action<IEnumerable<GlanceCourseModel>> callBack)
        //{
        //    BackgroundWorker backgroundWorker = new BackgroundWorker();
        //    backgroundWorker.DoWork += ((se, ea) =>
        //    {
        //        var loginResponse = HttpRequester.Get<IEnumerable<GlanceCourseModel>>(BaseServicesUrl + "Courses");
        //        ea.Result = loginResponse;
        //    });

        //    backgroundWorker.RunWorkerCompleted += ((se, ea) =>
        //    {
        //        var result = ea.Result as IEnumerable<GlanceCourseModel>;
        //        if (callBack != null)
        //        {
        //            callBack(result);
        //        }
        //    });

        //    backgroundWorker.RunWorkerAsync();
        //}

        //public static void AddCourse(GlanceCourseModel courseModel, string sessionKey, Action<GlanceCourseModel> callBack)
        //{
        //    BackgroundWorker backgroundWorker = new BackgroundWorker();
        //    backgroundWorker.DoWork += ((se, ea) =>
        //    {
        //        var url = BaseServicesUrl + "admin/course?sessionKey=" + sessionKey;
        //        var model = HttpRequester.Post<GlanceCourseModel>(url, courseModel);
        //        ea.Result = model;
        //    });

        //    backgroundWorker.RunWorkerCompleted += ((se, ea) =>
        //    {
        //        if (callBack != null)
        //        {
        //            callBack(ea.Result as GlanceCourseModel);
        //        }
        //    });

        //    backgroundWorker.RunWorkerAsync();
        //}

        //public static void DeactivateCourse(int courseId, string sessionKey, Action callBack)
        //{
        //    BackgroundWorker backgroundWorker = new BackgroundWorker();
        //    backgroundWorker.DoWork += ((se, ea) =>
        //    {
        //        var url = string.Format("{0}admin/deletecourse/{1}?sessionKey={2}", BaseServicesUrl, courseId, sessionKey);
        //        HttpRequester.Delete(url);
        //    });

        //    backgroundWorker.RunWorkerCompleted += ((se, ea) =>
        //    {
        //        if (callBack != null)
        //        {
        //            callBack();
        //        }
        //    });

        //    backgroundWorker.RunWorkerAsync();
        //}

        //public static void UpdateCourse(GlanceCourseModel courseModel, string sessionKey, Action callBack)
        //{
        //    BackgroundWorker backgroundWorker = new BackgroundWorker();
        //    backgroundWorker.DoWork += ((se, ea) =>
        //    {
        //        var url = string.Format("{0}admin/updatecourse/{1}?sessionKey={2}", BaseServicesUrl, courseModel.CourseId, sessionKey);
        //        HttpRequester.Put(url, courseModel);
        //    });

        //    backgroundWorker.RunWorkerCompleted += ((se, ea) =>
        //    {
        //        if (callBack != null)
        //        {
        //            callBack();
        //        }
        //    });

        //    backgroundWorker.RunWorkerAsync();
        //}



        //public static void CreateLectures(AddLectureModel newLecture, string sessionKey, int id, Action callback)
        //{
        //    BackgroundWorker bw = new BackgroundWorker();
        //    bw.DoWork += ((se, ea) =>
        //    {
        //        HttpRequester.Post(BaseServicesUrl + "admin/addlecture/" + id + "?sessionKey=" + sessionKey, newLecture);
        //    });
        //    bw.RunWorkerAsync();
        //    bw.RunWorkerCompleted += ((se, ea) =>
        //    {
        //        if (callback != null)
        //        {
        //            callback();
        //        }
        //    });
        //}


        //public static void Edit(string username, string nickname, UserType userType,
        //    string website, Gender gender, string hometown, DateTime? birthday,
        //    string email, string occupation, string aboutme, int studentNumber, DateTime registerDate,
        //    DateTime lastVisit, IEnumerable<CourseUserModel> courses, Action<LoginResponseModel> callback)
        //{
        //    //Username, Nickname, UserType, Website, Gender,Hometown,
        //    //        Birthday, Email, Occupation, Aboutme, StudentNumber, RegistrationDate,
        //    //        LastVisit, Courses


        //    AppCache.Config.IsBusyPool.Add("Editing, please wait");
        //    BackgroundWorker bw = new BackgroundWorker();
        //    bw.DoWork += ((se, ea) =>
        //    {
        //        //Validation!!!!!
        //        //validate username
        //        //validate email
        //        //validate authentication code
        //        //use validation from WebAPI
        //        var userFullModel = new UserFullModel()
        //        {
        //            Username = username,
        //            Nickname = nickname,
        //            UserType = userType,
        //            WebSite=website,
        //            Gender=gender,
        //            Hometown=hometown,
        //            Birthday=birthday,
        //            Email=email,
        //            Occupation=occupation,
        //            AboutMe=aboutme,
        //            StudentNumber=studentNumber,
        //            RegistrationDate=registerDate,
        //            LastVisit=lastVisit,
        //            Courses=courses,
        //        };
        //        var loginResponse = HttpRequester.Get<UserFullModel>(BaseServicesUrl + "users/register", userFullModel);
        //        ea.Result = loginResponse;
        //    });
        //    bw.RunWorkerAsync();
        //    bw.RunWorkerCompleted += ((se, ea) =>
        //    {
        //        AppCache.Config.IsBusyPool.TryRemove("Registering, please wait");
        //        var result = ea.Result as LoginResponseModel;
        //        if (callback != null)
        //        {
        //            callback(result);
        //        }
        //    });
        //}

        //public static void PrintProfile(string username, string accessToken, Action<UserFullModel> callback)
        //{
        //    AppCache.Config.IsBusyPool.Add("Loading user profile, please wait");
        //    var bw = new BackgroundWorker();
        //    bw.DoWork += ((se, ea) =>
        //    {
        //        var searchResponse = HttpRequester.Get<UserFullModel>(BaseServicesUrl + "/users/GetUser/" + username + "/?sessionKey=" + accessToken);
        //        // // api/users/get/{id}
        //        ea.Result = searchResponse;
        //    });
        //    bw.RunWorkerAsync();
        //    bw.RunWorkerCompleted += ((se, ea) =>
        //    {
        //        AppCache.Config.IsBusyPool.TryRemove("Loading user profile, please wait");
        //        var result = ea.Result as UserFullModel;
        //        if (callback != null)
        //        {
        //            callback(result);
        //        }
        //    });
        //}

        
    }
}
