using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebServicesTesting.Data;
using WebServicesTesting.Model;
using WebServicesTesting.WebApi.Models;

namespace WebServicesTesting.WebApi.Controllers
{
    public class SchoolController : BaseApiController
    {
        public IQueryable<SchoolModel> Get()
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(() =>
            {
                var db = new SchoolContext();
                var result = db.Schools.Select(s => new SchoolModel()
                {
                    Id = s.Id,
                    Location = s.Location,
                    Name = s.Name
                });
                return result;
            });
            return responseMsg;
        }

        public SchoolModel GetById(int id)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(() =>
            {
                var result = new SchoolModel();
                var db = new SchoolContext();
                var school = db.Schools.Find(id);
                if (school != null)
                {
                    result.Id = school.Id;
                    result.Location = school.Location;
                    result.Name = school.Name;
                }
                return result;
            });
            return responseMsg;
        }

        [HttpPost]
        [ActionName("posts")]
        public HttpResponseMessage Post(SchoolModel school)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(() =>
            {
                var newcSchool = new School()
                {
                    Location = school.Location,
                    Name = school.Name
                };
                var db = new SchoolContext();
                db.Schools.Add(newcSchool);
                db.SaveChanges();

                school.Id = newcSchool.Id;
                var response = this.Request.CreateResponse(HttpStatusCode.Created, school);
                return response;
            });
            return responseMsg;
        }
    }
}
