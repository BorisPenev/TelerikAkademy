using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyPopcorn
{
    public class ShootEngine: Engine
    {
        public ShootEngine(IRenderer renderer, IUserInterface userInterface, int sleeping)
            : base(renderer, userInterface, sleeping)
        {
        }

        public void ShootPlayerRacket()
        {
            if (this.playerRacket is ShootingRacket)
            {
                (this.playerRacket as ShootingRacket).Fire();
            }
        }
    }
}
