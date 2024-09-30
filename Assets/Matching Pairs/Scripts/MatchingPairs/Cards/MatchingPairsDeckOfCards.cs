using MatchingPairs.pure_unity_methods;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MatchingPairs.Cards
{
    public class MatchingPairsDeckOfCards : Singleton<MatchingPairsDeckOfCards>
    {
        [SerializeField] private bool allowDuplicateCards;
        [SerializeField] private Card[] cards;
        public Sprite cardBack;

        public void Initialise()
        {
            for (var i = 0; i < cards.Length; i++)
            {
                cards[i].Initialise(i);
            }
        }

        public Card TakeRandomCard()
        {
            while (true)
            {
                var cardIndex = Random.Range(0, cards.Length);
                if (allowDuplicateCards) return cards[cardIndex];
                if (cards[cardIndex].InPlay) continue;
                cards[cardIndex].MarkActive();
                return cards[cardIndex];
            }
        }
    }
}
