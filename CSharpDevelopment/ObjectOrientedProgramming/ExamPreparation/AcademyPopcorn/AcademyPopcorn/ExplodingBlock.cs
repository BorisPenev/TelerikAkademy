using System;
using System.Collections.Generic;
using System.Linq;

namespace AcademyPopcorn
{
    public class ExplodingBlock : Block
    {
        public const char Symbol = 'E';
        public new const string CollisionGroupString = "explodingblock";
        private bool isHit = false;

        public ExplodingBlock(MatrixCoords upperLeft)
            : base(upperLeft)
        {
            this.body[0, 0] = ExplodingBlock.Symbol;
        }

        public override string GetCollisionGroupString()
        {
            return ExplodingBlock.CollisionGroupString;
        }

        public override void RespondToCollision(CollisionData collisionData)
        {
            this.IsDestroyed = true;
            this.isHit = true;
            ProduceObjects();
        }

        public override IEnumerable<GameObject> ProduceObjects()
        {
            List<ExplodingPart> explodingParts = new List<ExplodingPart>();
            if (IsDestroyed)
            {
                explodingParts.Add(new ExplodingPart(topLeft, new MatrixCoords(-1, 0)));
                explodingParts.Add(new ExplodingPart(topLeft, new MatrixCoords(1, 0)));
                explodingParts.Add(new ExplodingPart(topLeft, new MatrixCoords(0, 1)));
                explodingParts.Add(new ExplodingPart(topLeft, new MatrixCoords(0, -1)));
                explodingParts.Add(new ExplodingPart(topLeft, new MatrixCoords(1, -1)));
                explodingParts.Add(new ExplodingPart(topLeft, new MatrixCoords(-1, 1)));
                explodingParts.Add(new ExplodingPart(topLeft, new MatrixCoords(-1, -1)));
                explodingParts.Add(new ExplodingPart(topLeft, new MatrixCoords(1, 1)));
            }
            return explodingParts;
        }
    }
}
