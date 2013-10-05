using System;
using System.Linq;

namespace Zoo
{
    class Dog: Animal
    {
        public Dog(int age, string name, SexType sex)
            : base(age, name, sex)
        {
        }

        public override string Sound()
        {
            return "bow-wow";
        }
    }
}
