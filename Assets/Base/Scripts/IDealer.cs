using System;
using Base.Scripts.Cards;
using UnityEngine;

namespace Base.Scripts
{
    public interface IDealer
    {
        public void Initialise();
        public void DealCards(Action callBack);
        public Card[] TakeCardsFromDeck(int amountOfCards);
        public Card TakeRandomCardFromDeck();
        public Card TakeSpecificCardFromDeck(int index);
        public void ReturnCardToDeck(Card card);
        public Sprite GetCardBack();
    
        public int GetAmountOfCardsInDeck();

    }
}
