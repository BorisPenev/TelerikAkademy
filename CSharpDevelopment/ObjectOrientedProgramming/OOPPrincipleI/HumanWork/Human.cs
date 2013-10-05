using System;
using System.Linq;

namespace HumanWork
{
    abstract class Human
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

        public Human(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}
