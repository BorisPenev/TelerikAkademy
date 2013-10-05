using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcademyEcosystem
{
    class Lion : GrowableAnimal
    {
        public Lion(string name, Point location)
            : base(name, location, 6)
        {
            this.IncreaseFactor = 1;
        }

        public override int TryEatAnimal(Animal animal)
        {
            if (animal != null && animal.Size <= this.Size * 2)
            {
                this.IsEated = true;
                return animal.GetMeatFromKillQuantity();
            }
            return base.TryEatAnimal(animal);
        }
    }
}
