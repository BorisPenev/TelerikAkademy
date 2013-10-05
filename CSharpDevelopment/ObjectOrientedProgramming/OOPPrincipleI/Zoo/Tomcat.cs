using System;
using System.Linq;

namespace Zoo
{
    class Tomcat: Cat
    {
        public Tomcat(int age, string name):
            base(age, name, SexType.Male)
        {
        }
    }
}
