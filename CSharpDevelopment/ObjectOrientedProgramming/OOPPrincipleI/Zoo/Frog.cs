using System;
using System.Linq;

namespace Zoo
{
    class Frog: Animal
    {
        public Frog(int age, string name, SexType sex)
            : base(age, name, sex)
        {
        }

        public override string Sound()
        {
            return "kvaaaaaaaaaaak";
        } 
    }
}
