using System;
using System.Linq;

namespace AcademyPopcorn
{
    class UnpassableBlock : IndestructibleBlock
    {
        public new const string CollisionGroupString = "unpassableblock";
        public const char Symbol = '=';

        public UnpassableBlock(MatrixCoords topLeft)
            : base(topLeft)
        {
            this.body[0, 0] = UnpassableBlock.Symbol;
        }

        public override bool CanCollideWith(string otherCollisionGroupString)
        {
            return otherCollisionGroupString == "unpassableblock";
        }

        public override string GetCollisionGroupString()
        {
            return UnpassableBlock.CollisionGroupString;
        }
    }
}
