using System.Collections.Generic;

namespace WebServicesTesting.Model
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public virtual ICollection<Student> Students { get; set; }

        public School()
        {
            this.Students = new HashSet<Student>();
        }
    }
}
