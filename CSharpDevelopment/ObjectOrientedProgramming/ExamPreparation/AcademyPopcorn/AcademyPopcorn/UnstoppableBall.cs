using System;
using System.Linq;

namespace AcademyPopcorn
{
    class UnstoppableBall : Ball
    {
        public new const string CollisionGroupString = "ball";

        public UnstoppableBall(MatrixCoords topLeft, MatrixCoords speed)
            : base(topLeft, speed)
        {
        }

        public override bool CanCollideWith(string otherCollisionGroupString)
        {
            return otherCollisionGroupString == "unpassableblock" || otherCollisionGroupString == "indestructibleblock";
        }

        public override string GetCollisionGroupString()
        {
            return UnstoppableBall.CollisionGroupString;
        }

        public override void RespondToCollision(CollisionData collisionData)
        {
            if (collisionData.hitObjectsCollisionGroupStrings[0] != "block")
            {
                if (collisionData.CollisionForceDirection.Row * this.Speed.Row < 0)
                {
                    this.Speed.Row *= -1;
                }
                if (collisionData.CollisionForceDirection.Col * this.Speed.Col < 0)
                {
                    this.Speed.Col *= -1;
                }
            }
            
        }
    }
}
