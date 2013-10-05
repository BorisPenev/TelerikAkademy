using System.Collections.Generic;

namespace CodeFirst.Model
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Materials { get; set; }

        private ICollection<Student> students = new HashSet<Student>();
        public virtual ICollection<Student> Student
        {
            get { return this.students; }
            set { this.students = value; }
        }

        private ICollection<Homework> homeworks = new HashSet<Homework>();
        public virtual ICollection<Homework> Homeworks
        {
            get { return this.homeworks; }
            set { this.homeworks = value; }
        }
    }
}
