﻿using System;
using System.Linq;

namespace AcademyPopcorn
{
    public class Gift: MovingObject
    {
        public new const string CollisionGroupString = "gift";

        public Gift(MatrixCoords topLeft, MatrixCoords speed)
            : base(topLeft, new char[,] { { 'g' } }, speed)
        {
        }

        public override bool CanCollideWith(string otherCollisionGroupString)
        {
            return otherCollisionGroupString == "racket";
        }

        public override string GetCollisionGroupString()
        {
            return Gift.CollisionGroupString;
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
