using System;
using System.Collections.Generic;
using System.Linq;

namespace School
{
    class Student : People
    {
        private Guid classNumber = Guid.NewGuid();
        public Guid ClassNumber
        {
            get { return classNumber; }
        }

        private List<Teacher> teachers = new List<Teacher>();
        public List<Teacher> Teachers
        {
            get { return teachers; }
            set { teachers = value; }
        }
    }
}
