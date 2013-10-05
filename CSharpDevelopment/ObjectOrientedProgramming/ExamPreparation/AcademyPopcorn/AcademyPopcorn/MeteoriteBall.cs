using System;
using System.Linq;

namespace AcademyPopcorn
{
    class MeteoriteBall: Ball
    {
        public new const string CollisionGroupString = "meteoriteball";

        public MeteoriteBall(MatrixCoords topLeft, MatrixCoords speed)
            : base(topLeft, speed)
        {
        }

        public override string GetCollisionGroupString()
        {
            return MeteoriteBall.CollisionGroupString;
        }
    }
}
