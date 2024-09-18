using System.Collections.Generic;
using System.Linq;

namespace Poker
{
    public static class Evaluator
    {
        public static Evaluation Evaluate(IEnumerable<Card> cards)
        {
            if (IsJacksOrBetter(cards))
            {
                return Evaluation.JacksOrBetter;
            }

            return Evaluation.Nothing;
        }

        private static bool IsJacksOrBetter(IEnumerable<Card> cards)
        {
            var possible  = cards.Where(card => card.GetRank() >= Rank.Jack || card.GetRank() == Rank.Ace).ToArray();
            if (possible.Length != 2) return false;
            return possible[0].GetRank() == possible[1].GetRank();
        }
    }

    public enum Evaluation
    {
        JacksOrBetter = 1,
        TwoPair = 2,
        ThreeOfAKind = 2,
        FourOfAKind = 100,
        FullHouse = 5,
        Flush = 10,
        Straight = 15,
        StraightFlush = 50,
        RoyalFlush = 250,
        
        Nothing = 0,
    }
    
}
