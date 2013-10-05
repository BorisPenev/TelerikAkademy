using System.Linq;
using System.Net;
using System.Net.Http;
using WebServicesTesting.Data;
using WebServicesTesting.Model;
using WebServicesTesting.WebApi.Models;

namespace WebServicesTesting.WebApi.Controllers
{
    public class StudentsController : BaseApiController
    {
        public IQueryable<StudentModel> Get()
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(() =>
            {
                var db = new SchoolContext();
                var result = db.Students.Select(s => new StudentModel()
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Age = s.Age,
                    Grade = s.Grade
                });
                return result;
            });
            return responseMsg;
        }

        public StudentModel GetById(int id)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(() =>
            {
                var result = new StudentModel();
                var db = new SchoolContext();
                var s = db.Students.Find(id);
                if (s != null)
                {
                    result.Id = s.Id;
                    result.FirstName = s.FirstName;
                    result.LastName = s.LastName;
                    result.Age = s.Age;
                    result.Grade = s.Grade;
                }
                return result;
            });
            return responseMsg;
        }

        public IQueryable<StudentModel> GetById(string subject, double value)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(() =>
            {
                var db = new SchoolContext();
                var result = db.Students
                    .Where(s => s.Marks.Any(m => m.Subject == subject && Equals(m.Value, value)))
                    .Select(s => new StudentModel()
                    {
                        Id = s.Id,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Age = s.Age,
                        Grade = s.Grade
                    });
                return result;
            });
            return responseMsg;
        }

        public HttpResponseMessage Post(StudentModel student)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(() =>
            {
                var newStudent = new Student()
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Age = student.Age,
                    Grade = student.Grade,
                    School = null,
                    SchoolId = null
                };
                var db = new SchoolContext();
                db.Students.Add(newStudent);
                db.SaveChanges();

                student.Id = newStudent.Id;
                var response = this.Request.CreateResponse(HttpStatusCode.Created, student);
                return response;
            });
            return responseMsg;
        }
    }
}
