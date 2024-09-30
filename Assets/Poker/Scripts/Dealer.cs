using System;
using Base.Scripts;
using Base.Scripts.Cards;
using Base.Scripts.pure_unity_methods;
using Base.Scripts.StateManagement;
using UnityEngine;

namespace Poker.Scripts
{
    public class Dealer : Singleton<Dealer>
    {
        [SerializeField] private DeckOfCards deck;
        [SerializeField] private Hand[] hands;
        
        public void DealCards(Action callBack)
        {
            if (!SequentialStateManager.Instance.IsDealState()) throw new Exception("Attempting to deal cards outside of the deal state.");
            
            ResetHands(() =>
            {
                if (CheatMenu.CheatCardsAvailable)
                {
                    return;
                }
                foreach (var hand in hands)
                {
                    hand.Deal(TakeCardsFromDeck(hand.AmountOfCards), callBack);
                }
            }, true);
        }
        
        public void DrawCards(Action callBack)
        {
            if (!SequentialStateManager.Instance.IsDrawState()) throw new Exception("Attempting to draw cards outside of the deal state.");

            ResetHands(() =>
            {
                if (CheatMenu.CheatCardsAvailable)
                {
                    return;
                }
                foreach (var hand in hands)
                {
                    hand.Deal(TakeCardsFromDeck(hand.AmountOfCards), callBack);
                }
            });
        }

        public void ResetHands(Action callBack, bool ignoreHeld = false)
        {
            foreach (var hand in hands)
            {
                hand.ResetCards(callBack, ignoreHeld);
            }
        }

        private Card[] TakeCardsFromDeck(int amountOfCards)
        {
            var cards = new Card[amountOfCards];
            for (var i = 0; i < amountOfCards; i++)
            {
                cards[i] = TakeRandomCardFromDeck();
            }
            return cards;
        }

        public void ReturnCardToDeck(Card card)
        {
            deck.ReturnCardToDeck(card);
        }

        private Card TakeRandomCardFromDeck()
        {
            return deck.TakeRandomCard();
        }
        
        public Card TakeSpecificCardFromDeck(int index)
        {
            return deck.TakeSpecificCard(index);
        }

        public int GetAmountOfCardsInDeck()
        {
            return deck.AmountOfCards;
        }
        
        public int GetAmountOfCardsInHand(int index)
        {
            return hands[index].AmountOfCards;
        }
        
        public Sprite GetCardBack()
        {
            return deck.cardBack;
        }
    }
}
