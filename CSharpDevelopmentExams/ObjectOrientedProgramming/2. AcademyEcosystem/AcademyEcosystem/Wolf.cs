using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcademyEcosystem
{
    class Wolf: Animal, ICarnivore
    {
        public Wolf(string name, Point location) 
            : base(name, location, 4)
        {
        }

        public int TryEatAnimal(Animal animal)
        {
            if (animal != null && (animal.Size <= this.Size || animal.State == AnimalState.Sleeping))
                return animal.GetMeatFromKillQuantity();
            return 0;
        }
    }
}
