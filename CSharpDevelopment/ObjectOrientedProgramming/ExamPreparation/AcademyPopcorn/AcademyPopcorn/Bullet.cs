using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyPopcorn
{
    public class Bullet : MovingObject
    {
        public new const string CollisionGroupString = "bullet";

        public Bullet(MatrixCoords topLeft, MatrixCoords speed)
            : base(topLeft, new char[,] { { '^' } }, speed)
        {
        }

        public override bool CanCollideWith(string otherCollisionGroupString)
        {
            return otherCollisionGroupString == "block";
        }

        public override string GetCollisionGroupString()
        {
            return Bullet.CollisionGroupString;
        }

        public override void RespondToCollision(CollisionData collisionData)
        {
            this.IsDestroyed = true;
        }

        protected override void UpdatePosition()
        {
            this.TopLeft += this.Speed;
        }
    }
}
