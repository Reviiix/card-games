using UnityEngine;

namespace Poker
{
    public class Dealer : MonoBehaviour
    {
        [SerializeField] private Hand[] hands;
        private static DeckOfCards Deck => GameManager.Instance.deck;

        [ContextMenu("Deal")]
        public void Deal()
        {
            if (CheatMenu.CheatCardsAvailable)
            {
                
            }
            foreach (var hand in hands)
            {
                hand.Deal(TakeCardsFromPack(hand.AmountOfCards));
            }
        }
        
        public void Draw()
        {
            foreach (var hand in hands)
            {
                hand.Deal(TakeCardsFromPack(hand.AmountOfCards));
            }
        }

        public int AmountOfCardsInHand(int index)
        {
            return hands[index].AmountOfCards;
        }
        
        private static Card[] TakeCardsFromPack(int amountOfCards)
        {
            var cards = new Card[amountOfCards];
            for (var i = 0; i < amountOfCards; i++)
            {
                cards[i] = Deck.TakeRandomCard();
            }
            return cards;
        }
    }
}
