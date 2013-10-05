using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcademyEcosystem
{
    class Zombie: Animal
    {
        public Zombie(string name, Point location) : base(name, location, 1)
        {
        }

        public override int GetMeatFromKillQuantity()
        {
            return 10;
        }
    }
}
