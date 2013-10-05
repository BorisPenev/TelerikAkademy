using System;
using CodeFirst.Data;
using CodeFirst.Model;

namespace CodeFirst.WebApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var db = new CodeFirstDbContext())
            {
                var course = new Course
                {
                    Name = "Algorihms",
                    Description = "gadno",
                    Materials = "za sega njama"
                };
                
                //var material = "za sega njama";
                //course.Materials.Add(material);
                db.Courses.Add(course);

                var student = new Student
                {
                    Name = "Pesho",
                    Number = 10000
                };
                
                student.Cources.Add(course);
                db.Students.Add(student);

                try
                {
                    db.SaveChanges();
                }
                catch (Exception exception)
                {
                    Response.Write(exception.InnerException.InnerException.Message);
                }
                
            }
        }
    }
}