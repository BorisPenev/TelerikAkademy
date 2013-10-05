using System;
using System.Collections.Generic;
using System.Text;

namespace InheritanceAndPolymorphism
{
    public abstract class Course
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name cannot be null or empty.");
                }
                name = value;
            }
        }

        private string teacherName = string.Empty;
        public string TeacherName
        {
            get { return teacherName; }
            set { teacherName = value; }
        }

        private IList<string> students = new List<string>();
        public IList<string> Students
        {
            get { return students; }
            set { students = value; }
        }

        public Course(string name)
        {
            this.Name = name;
            this.TeacherName = string.Empty;
            this.Students = new List<string>();
        }

        public Course(string name, string teacherName)
            : this(name)
        {
            this.TeacherName = teacherName;
        }

        public Course(string name, string teacherName, IList<string> students)
            : this(name, teacherName)
        {
            this.Students = students;
        }

        public string GetStudentsAsString()
        {
            if (this.Students == null || this.Students.Count == 0)
            {
                return "{ }";
            }

            return "{ " + string.Join(", ", this.Students) + " }";
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendFormat("Name = {0}", this.Name);
            if (!string.IsNullOrEmpty(this.TeacherName))
            {
                result.AppendFormat("; Teacher = {0}", this.TeacherName);
            }
            if (this.Students.Count > 0)
            {
                result.AppendFormat("; Students = {0}", this.GetStudentsAsString());
            }

            return result.ToString();
        }
    }
}
