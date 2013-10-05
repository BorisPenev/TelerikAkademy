using System;
using System.Linq;

namespace Zoo
{
    class Kitten: Cat
    {
        public Kitten(int age, string name) :
            base(age, name, SexType.Female)
        {
        }
    }
}
