using System.Collections.Generic;
using System.Text;

namespace InheritanceAndPolymorphism
{
    public class LocalCourse: Course
    {
        private string lab = string.Empty;
        public string Lab
        {
            get { return lab; }
            set { lab = value; }
        }

        public LocalCourse(string name)
            : base(name)
        {
        }

        public LocalCourse(string name, string teacherName)
            : base(name, teacherName)
        {
        }

        public LocalCourse(string name, string teacherName, IList<string> students)
            : base(name, teacherName, students)
        {
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.Append("LocalCourse { ");
            result.Append(base.ToString());
            if (this.Lab != null)
            {
                result.AppendFormat("; Lab = {0}", this.Lab);
            }
            result.Append(" }");
            
            return result.ToString();
        }
    }
}
