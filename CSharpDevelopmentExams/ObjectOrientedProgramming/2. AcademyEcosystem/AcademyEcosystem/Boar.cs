using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcademyEcosystem
{
    class Boar : GrowableAnimal, IHerbivore
    {
        public Boar(string name, Point location) : base(name, location, 4)
        {
            this.IncreaseFactor = 1;
        }

        public int EatPlant(Plant plant)
        {
            if (plant != null)
            {
                this.IsEated = true;
                return plant.GetEatenQuantity(2);
            }
            return 0;
        }

        public override int TryEatAnimal(Animal animal)
        {
            if (animal != null && animal.Size <= this.Size)
                return animal.GetMeatFromKillQuantity();
            return base.TryEatAnimal(animal);
        }
    }
}
