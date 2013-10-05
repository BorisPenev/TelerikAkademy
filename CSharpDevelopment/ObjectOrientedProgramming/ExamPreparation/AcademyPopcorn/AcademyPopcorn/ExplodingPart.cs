using System;
using System.Linq;

namespace AcademyPopcorn
{
    public class ExplodingPart: MovingObject
    {
        public ExplodingPart(MatrixCoords topLeft, MatrixCoords speed)
            : base(topLeft, new char[,] { { 'e' } }, speed)
        {
        }

        public override void Update()
        {
            base.Update();
            this.IsDestroyed = true;
        }

        public override void RespondToCollision(CollisionData collisionData)
        {
            this.IsDestroyed = true;
        }

        public override bool CanCollideWith(string otherCollisionGroupString)
        {
            return otherCollisionGroupString == "block";
        }

        public override string GetCollisionGroupString()
        {
            return Ball.CollisionGroupString;
        }
    }
}
