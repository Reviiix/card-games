using System;
using System.Linq;
using Base.Scripts;
using Base.Scripts.Cards;
using pure_unity_methods;
using UnityEngine;

namespace MatchingPairs.Scripts
{
    public class MatchingPairsDealer : Singleton<MatchingPairsDealer>, IDealer
    {
        [SerializeField] private DeckOfCards deck;
        
        public void Initialise()
        {
            deck.Initialise();
        }
        
        /// <summary>
        /// Create two matching arrays of cards and combine them together.
        /// This ensures there are always the correct amount of matches
        /// </summary>
        public Card[] TakeCardsFromDeck(int amountOfCards)
        {
            var half = amountOfCards / 2;
            var firstHalf = new Card[half];
            var secondHalf = new Card[half];
            for (var i = 0; i < half; i++)
            {
                firstHalf[i] = TakeRandomCardFromDeck();
            }
            firstHalf.CopyTo(secondHalf, 0);
            var cards = firstHalf.Concat(secondHalf).ToArray();
            return CollectionUtilities.ShuffleArray(cards);
        }
        
        public Sprite GetCardBack()
        {
            return deck.cardBack;
        }

        public int GetAmountOfCardsInDeck()
        {
            return deck.AmountOfCards;
        }

        public Card TakeRandomCardFromDeck()
        {
            return deck.TakeRandomCard();
        }

        public Card TakeSpecificCardFromDeck(int index)
        {
            return deck.TakeSpecificCard(index);       
        }
        
        public void ReturnCardToDeck(Card card)
        {
            deck.ReturnCardToDeck(card);
        }
        
        public void DealCards(Action callBack)
        {
            throw new NotImplementedException();
        }

        public void DrawCards(Action callBack)
        {
            throw new NotImplementedException();
        }
    }
}
