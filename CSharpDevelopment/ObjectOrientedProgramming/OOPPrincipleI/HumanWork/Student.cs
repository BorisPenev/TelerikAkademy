using System;
using System.Linq;

namespace HumanWork
{
    class Student: Human
    {
        public int Grade { get; set; }

        public Student(string firstName, string lastName, int grade)
            : base(firstName, lastName)
        {
            this.Grade = grade;
        }

        public override string ToString()
        {
            return base.ToString() + " " + this.Grade;
        }
    }
}
