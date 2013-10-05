using System;
using System.Linq;

namespace Zoo
{
    class Cat : Animal
    {
        public Cat(int age, string name, SexType sex)
            : base(age, name, sex)
        {
        }

        public override string Sound()
        {
            return "myau";
        } 
    }
}
