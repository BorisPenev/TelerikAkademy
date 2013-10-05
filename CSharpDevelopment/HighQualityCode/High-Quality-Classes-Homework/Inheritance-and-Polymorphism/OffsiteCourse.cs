using System.Collections.Generic;
using System.Text;

namespace InheritanceAndPolymorphism
{
    public class OffsiteCourse: Course
    {
        private string town = string.Empty;
        public string Town
        {
            get { return town; }
            set { town = value; }
        }

        public OffsiteCourse(string name)
            : base(name)
        {
        }

        public OffsiteCourse(string name, string teacherName)
            : base(name, teacherName)
        {
        }

        public OffsiteCourse(string name, string teacherName, IList<string> students)
            : base(name, teacherName, students)
        {
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.Append("OffsiteCourse { ");
            result.Append(base.ToString());
            if (this.Town != null)
            {
                result.AppendFormat("; Town = {0}", this.Town);
            }
            result.Append(" }");
            
            return result.ToString();
        }
    }
}
