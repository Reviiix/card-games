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
            var jacksOrBetters = cards.Count(card => card.ReturnRank() >= Rank.Jack);

            return jacksOrBetters >= 2;
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
