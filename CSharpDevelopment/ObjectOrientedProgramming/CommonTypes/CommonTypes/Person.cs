using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    public class Person
    {
        private string name;
        private int? age;

        public Person(string name, int? age)
        {
            this.name = name;
            this.age = age;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name: " + name);
            if(age.HasValue)
                sb.AppendLine("Age: " + age.Value);
            else
                sb.AppendLine("Age: unspecified");
            return sb.ToString();
        }
    }
}
