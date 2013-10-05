using System;
using System.Linq;

namespace StudentsSort
{
    class Student
    {
        private string firstName = string.Empty;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private string lastName = string.Empty;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private int age = 0;
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
    }
}
