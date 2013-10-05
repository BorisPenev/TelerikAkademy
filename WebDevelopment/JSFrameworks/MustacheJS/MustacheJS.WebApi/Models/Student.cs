using System.Collections.Generic;

namespace MustacheJS.WebApi.Models
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Grade { get; set; }
        public int Age { get; set; }
        public List<Mark> Marks { get; set; }  
    }
}