using System;
using Base.Scripts;
using Base.Scripts.Cards;
using pure_unity_methods;
using UnityEngine;

namespace Snap.Scripts
{
    public class SnapDealer : Singleton<SnapDealer>, IDealer
    {
        [SerializeField] private DeckOfCards deck;
        public static Action<int> OnCardRemovedFromDeck;

        public void Initialise()
        {
            deck.Initialise();
            OnCardRemovedFromDeck?.Invoke(deck.GetAmountOfCardsInPlay());
        }

        public Card TakeRandomCardFromDeck()
        {
            OnCardRemovedFromDeck?.Invoke(deck.GetAmountOfCardsInPlay());
            return deck.TakeRandomCard();
        }

        public int GetAmountOfCardsInDeck()
        {
            return deck.AmountOfCards;
        }

        public bool CardsRemainingInDeck()
        {
            return deck.GetAmountOfCardsInPlay() < GetAmountOfCardsInDeck();
        }
        
        public Sprite GetCardBack()
        {
            return deck.cardBack;
        }
        
        public Card TakeSpecificCardFromDeck(int index)
        {
            throw new NotImplementedException();
        }

        public void ReturnCardToDeck(Card card)
        {
            throw new NotImplementedException();
        }

        public void DealCards(Action callBack)
        {
            throw new NotImplementedException();
        }

        public Card[] TakeCardsFromDeck(int amountOfCards)
        {
            throw new NotImplementedException();
        }
    }
}
