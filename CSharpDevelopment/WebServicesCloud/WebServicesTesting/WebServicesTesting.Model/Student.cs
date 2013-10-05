using System.Collections.Generic;

namespace WebServicesTesting.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Grade { get; set; }
        public virtual ICollection<Mark> Marks { get; set; }
        public int? SchoolId { get; set; }
        public virtual School School { get; set; }

        public Student()
        {
            this.Marks = new HashSet<Mark>();
        }
    }
}
