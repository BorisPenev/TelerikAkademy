using System;
using System.Linq;

namespace AcademyPopcorn
{
    public class TrailObject : GameObject 
    {
        private int lifetime = 0;
        public new const string CollisionGroupString = "trail";

        public override void Update()
        {
            if (this.lifetime == 0)
            {
                IsDestroyed = true;
            }
            else
            {
                this.lifetime--;
            }
        }

        public TrailObject(MatrixCoords topLeft, int lifetime)
            : base(topLeft, new char[,] { { '*' } })
        {
            this.lifetime = lifetime;
        }

        public override string GetCollisionGroupString()
        {
            return TrailObject.CollisionGroupString;
        }
    }
}
