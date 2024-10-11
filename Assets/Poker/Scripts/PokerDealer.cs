using System;
using Base.Scripts;
using Base.Scripts.Cards;
using Base.Scripts.StateManagement;
using Poker.Scripts.StateManager;
using pure_unity_methods;
using UnityEngine;

namespace Poker.Scripts
{
    public class PokerDealer : Singleton<PokerDealer>, IDealer
    {
        [SerializeField] private DeckOfCards deck;
        [SerializeField] private Hand[] hands;

        public void Initialise()
        {
            deck.Initialise();
        }

        public void DealCards(Action callBack)
        {
            if (!((PokerStateManager)SequentialStateManager.Instance).IsDealState()) throw new Exception("Attempting to deal cards outside of the deal state.");
            
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
            if (!((PokerStateManager)SequentialStateManager.Instance).IsDrawState()) throw new Exception("Attempting to draw cards outside of the deal state.");

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

        public Card[] TakeCardsFromDeck(int amountOfCards)
        {
            var cards = new Card[amountOfCards];
            for (var i = 0; i < amountOfCards; i++)
            {
                cards[i] = TakeRandomCardFromDeck();
            }
            return cards;
        }

        public int GetAmountOfCardsInHand(int handIndex)
        {
            return hands[handIndex].AmountOfCards;
        }

        public void ReturnCardToDeck(Card card)
        {
            deck.ReturnCardToDeck(card);
        }

        public Card TakeRandomCardFromDeck()
        {
            return deck.TakeRandomCard();
        }
        
        public Card TakeSpecificCardFromDeck(int index)
        {
            return deck.TakeSpecificCard(index);
        }

        public Sprite GetCardBack()
        {
            return deck.cardBack;
        }

        public int GetAmountOfCardsInDeck()
        {
            return deck.AmountOfCards;
        }
    }
}
