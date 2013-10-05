using System.Collections.Generic;

namespace CodeFirst.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }

        private ICollection<Course> cources = new HashSet<Course>();
        public virtual ICollection<Course> Cources
        {
            get { return this.cources; }
            set { this.cources = value; }
        }

        private ICollection<Homework> homeworks = new HashSet<Homework>();
        public virtual ICollection<Homework> Homeworks
        {
            get { return this.homeworks; }
            set { this.homeworks = value; }
        }
    }
}
