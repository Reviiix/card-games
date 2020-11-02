using UnityEngine;
using Random = UnityEngine.Random;

namespace Poker
{
    public class DeckOfCards : MonoBehaviour
    {
        private const bool AllowDuplicateCards = false;
        [SerializeField] private Card[] cards;
        public int AmountOfCards => cards.Length;
        public Sprite cardBack;
        

        public Card TakeRandomCard()
        {
            while (true)
            {
                var card = cards[Random.Range(0, cards.Length)];

                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (AllowDuplicateCards) return card;
                
                if (card.inPlay)
                {
                    continue;
                }

                card.RemoveFromPlay();
                return card;
            }
        }

        public Card TakeSpecificCard(int index)
        {
            Debugging.DisplayDebugMessage("Card forcibly taken from pack, duplicate cards are now a possibility.");
            return cards[index];
        }
    }

}
