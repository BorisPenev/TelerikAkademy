using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyPopcorn
{
    public class GiftBlock: Block
    {
        public const char Symbol = 'G';
        public new const string CollisionGroupString = "giftblock";

        public GiftBlock(MatrixCoords upperLeft)
            : base(upperLeft)
        {
            this.body[0, 0] = GiftBlock.Symbol;
        }

        public override string GetCollisionGroupString()
        {
            return GiftBlock.CollisionGroupString;
        }

        public override void RespondToCollision(CollisionData collisionData)
        {
            this.IsDestroyed = true;
            ProduceObjects();
        }

        public override IEnumerable<GameObject> ProduceObjects()
        {
            List<Gift> gifts = new List<Gift>();
            if (IsDestroyed)
            {
                gifts.Add(new Gift(topLeft, new MatrixCoords(1, 0)));
            }
            return gifts;
        }
    }
}
