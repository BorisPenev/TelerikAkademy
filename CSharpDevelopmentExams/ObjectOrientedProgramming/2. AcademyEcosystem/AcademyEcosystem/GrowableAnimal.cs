using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyEcosystem
{
    class GrowableAnimal: Animal, ICarnivore
    {
        public bool IsEated = false;
        public int IncreaseFactor = 0;

        public GrowableAnimal(string name, Point location, int size) : base(name, location, size)
        {
        }

        public virtual int TryEatAnimal(Animal animal)
        {
            return 0;
        }

        public override void Update(int timeElapsed)
        {
            if (IsEated)
            {
                this.Size += this.IncreaseFactor;
                IsEated = false;
            }
            base.Update(timeElapsed);
        }
    }
}
