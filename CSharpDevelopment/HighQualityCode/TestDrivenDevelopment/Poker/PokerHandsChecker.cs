using System;
using System.Linq;

namespace Poker
{
    public class PokerHandsChecker : IPokerHandsChecker
    {
        public bool IsValidHand(IHand hand)
        {
            if (hand == null || hand.Cards == null || hand.Cards.Count != 5)
            {
                return false;
            }

            for (int i = 0; i < hand.Cards.Count - 1; i++)
            {
                for (int j = i + 1; j < hand.Cards.Count; j++)
                {
                    if (hand.Cards[i].Face.Equals(hand.Cards[j].Face) && hand.Cards[i].Suit.Equals(hand.Cards[j].Suit))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool IsStraightFlush(IHand hand)
        {
            bool result = true;
            CardSuit cs = hand.Cards[0].Suit;

            if (hand.Cards.Any(c => c.Suit != cs))
            {
                return false;
            }

            var tempCards = hand.Cards.OrderBy(c => (int)c.Face).ToList();
            for (int i = 0; i < tempCards.Count; i++)
            {
                if (i + 1 < tempCards.Count)
                {
                    if (((int)tempCards[i].Face + 1) != (int)tempCards[i + 1].Face)
                    {
                        if (i + 1 == 4 && tempCards[4].Face == CardFace.Ace)
                        {
                            continue;
                        }
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        public bool IsFourOfAKind(IHand hand)
        {
            var result = true;
            var group = hand.Cards.GroupBy(c => c.Face);
            if (group.Count() != 2 || group.Count(r => r.Count() != 4) > 1)
            {
                result = false;
            }
            return result;
        }

        public bool IsFullHouse(IHand hand)
        {
            var result = true;
            var group = hand.Cards.GroupBy(c => c.Face);
            if (group.Count() != 2 || group.Count(r => r.Count() == 1) > 0)
            {
                result = false;
            }
            return result;
        }

        public bool IsFlush(IHand hand)
        {
            if (hand.Cards.GroupBy(c => c.Suit).Count() > 1)
            {
                return false;
            }

            var tempCards = hand.Cards.OrderBy(c => (int)c.Face).ToList();
            for (int i = 0; i < tempCards.Count; i++)
            {
                if (i + 1 < tempCards.Count)
                {
                    if (((int)tempCards[i].Face + 1) == (int)tempCards[i + 1].Face)
                    {
                        if (i + 1 == 4)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return true;
        }

        public bool IsStraight(IHand hand)
        {
            bool result = true;

            if (hand.Cards.GroupBy(c => c.Suit).Count() <= 1)
            {
                return false;
            }

            var tempCards = hand.Cards.OrderBy(c => (int)c.Face).ToList();
            for (int i = 0; i < tempCards.Count; i++)
            {
                if (i + 1 < tempCards.Count)
                {
                    if (((int)tempCards[i].Face + 1) != (int)tempCards[i + 1].Face)
                    {
                        if (i + 1 == 4 && tempCards[4].Face == CardFace.Ace)
                        {
                            continue;
                        }
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        public bool IsThreeOfAKind(IHand hand)
        {
            var result = true;
            var group = hand.Cards.GroupBy(c => c.Face);
            if (group.Count() != 3 || group.Count(r => r.Count() == 3) != 1)
            {
                result = false;
            }
            return result;
        }

        public bool IsTwoPair(IHand hand)
        {
            var result = true;
            var group = hand.Cards.GroupBy(c => c.Face);
            if (group.Count() != 3 || group.Count(r => r.Count() == 3) > 0)
            {
                result = false;
            }
            return result;
        }

        public bool IsOnePair(IHand hand)
        {
            var result = hand.Cards.GroupBy(c => c.Face);
            if (result.Count() == 4)
            {
                return true;
            }
            return false;
        }

        public bool IsHighCard(IHand hand)
        {
            return (!IsStraightFlush(hand) &&
                    !IsFourOfAKind(hand) &&
                    !IsFullHouse(hand) &&
                    !IsFlush(hand) &&
                    !IsStraight(hand) &&
                    !IsThreeOfAKind(hand) &&
                    !IsTwoPair(hand) &&
                    !IsOnePair(hand));
        }

        public int CompareHands(IHand firstHand, IHand secondHand)
        {
            //7* from homework not will be implemented due out of time.
            //todo:should return -1, 0 or 1
            throw new NotImplementedException();
        }
    }
}
