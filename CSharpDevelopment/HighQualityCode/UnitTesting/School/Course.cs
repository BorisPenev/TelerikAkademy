using System;
using System.Collections.Generic;
using System.Linq;

namespace School
{
    public class Course
    {
        private const int MAX_STUDENTS = 30;

        public readonly List<int> StudentIds = new List<int>();

        private string name = string.Empty;
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public Course(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name cannot be null or empty");
            }
            this.name = name;
        }

        internal void Join(Student student)
        {
            if (this.StudentIds.Count >= MAX_STUDENTS)
            {
                throw new ArgumentOutOfRangeException("Cannot add student. Max students reached for this course!");
            }

            this.StudentIds.Add(student.Id);
        }

        internal void Leave(Student student)
        {
            if (this.StudentIds.All(s => s != student.Id))
            {
                throw  new KeyNotFoundException("This student do not exist for this course");
            }

            this.StudentIds.Remove(student.Id);
        }
    }
}
