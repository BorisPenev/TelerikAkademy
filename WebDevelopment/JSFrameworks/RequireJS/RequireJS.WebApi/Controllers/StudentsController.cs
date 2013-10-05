using System.Collections.Generic;
using System.Web.Http;
using RequireJS.WebApi.Models;

namespace RequireJS.WebApi.Controllers
{
    public class StudentsController : ApiController
    {
        public IEnumerable<Student> GetStudents()
        {
            var students = new List<Student>();
            students.Add(new Student()
            {
                FirstName = "Doncho",
                LastName = "Minkov",
                Grade = "Telerik Academy",
                Age = 32,
                Marks = new List<Mark>()
                {
                    {
                        new Mark()
                        {
                            Subject = "Math",
                            Score = 4
                        }
                    },
                    {
                        new Mark()
                        {
                            Subject = "JavaScript",
                            Score = 6
                        }
                    }
                }
            });
            students.Add(new Student()
            {
                FirstName = "Nikolay",
                LastName = "Kostov",
                Grade = "Telerik Academy",
                Age = 32,
                Marks = new List<Mark>()
                {
                    {
                        new Mark()
                        {
                            Subject = "MVC",
                            Score = 6
                        }
                    },
                    {
                        new Mark()
                        {
                            Subject = "JavaScript",
                            Score = 5
                        }
                    }
                }
            });
            students.Add(new Student()
            {
                FirstName = "Ivaylo",
                LastName = "Kendov",
                Grade = "Telerik Academy",
                Age = 32,
                Marks = new List<Mark>()
                {
                    {
                        new Mark()
                        {
                            Subject = "OOP",
                            Score = 4
                        }
                    },
                    {
                        new Mark()
                        {
                            Subject = "C#",
                            Score = 6
                        }
                    }
                }
            });
            students.Add(new Student()
            {
                FirstName = "Svetlin",
                LastName = "Nakov",
                Grade = "Telerik Academy",
                Age = 32,
                Marks = new List<Mark>()
                {
                    {
                        new Mark()
                        {
                            Subject = "Unit Testing",
                            Score = 5
                        }
                    },
                    {
                        new Mark()
                        {
                            Subject = "WPF",
                            Score = 6
                        }
                    }
                }
            });
            students.Add(new Student()
            {
                FirstName = "Asya",
                LastName = "Georgieva",
                Grade = "Telerik Academy",
                Age = 32,
                Marks = new List<Mark>()
                {
                    {
                        new Mark()
                        {
                            Subject = "Automation Testing",
                            Score = 6
                        }
                    },
                    {
                        new Mark()
                        {
                            Subject = "Manual Testing",
                            Score = 4
                        }
                    }
                }
            });
            students.Add(new Student()
            {
                FirstName = "Georgi",
                LastName = "Georgiev",
                Grade = "Telerik Academy",
                Age = 32
            });
            return students;
        }
    }
}
