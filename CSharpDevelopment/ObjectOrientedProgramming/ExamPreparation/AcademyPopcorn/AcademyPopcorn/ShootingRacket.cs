using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyPopcorn
{
    public class ShootingRacket: Racket
    {
        public int Width { get; protected set; }
        bool isShootingEnable = false;
        public bool isShoot = false;

        public ShootingRacket(MatrixCoords topLeft, int width)
            : base(topLeft, width)
        {
        }

        public override bool CanCollideWith(string otherCollisionGroupString)
        {
            return base.CanCollideWith(otherCollisionGroupString) || otherCollisionGroupString == "gift";
        }

        public override void RespondToCollision(CollisionData collisionData)
        {
            base.RespondToCollision(collisionData);
            if (collisionData.hitObjectsCollisionGroupStrings[0] == "gift")
                isShootingEnable = true;
        }

        public override IEnumerable<GameObject> ProduceObjects()
        {
            List<Bullet> bullets = new List<Bullet>();
            if (isShootingEnable && isShoot)
            {
                bullets.Add(new Bullet(new MatrixCoords(topLeft.Row, topLeft.Col + 2), new MatrixCoords(-1, 0)));
                isShoot = false;
            }
            return bullets;
        }

        public void Fire()
        {
            this.isShoot = true;
        }
    }
}
