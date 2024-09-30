using System;
using Base.Scripts.pure_unity_methods;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Base.Scripts.Cards
{
    public class DeckOfCards : Singleton<DeckOfCards>
    {
        [SerializeField] private bool allowDuplicateCards;
        [SerializeField] private Card[] cards;
        public int AmountOfCards => cards.Length;
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

        public void ReturnCardToDeck(Card card)
        {
            ReturnCardToDeck(card.DeckIndex);
        }
        
        public void ReturnCardToDeck(int card)
        {
            if (!cards[card].InPlay && !allowDuplicateCards) throw new Exception($"Cheat attempt or critical error. Attempting to return duplicate card to deck and deck type does not support this. (card index: {card})");
            cards[card].MarkInActive();
        }

        public Card TakeSpecificCard(int index)
        {
            Debugging.DisplayDebugMessage("Card forcibly taken from deck, duplicate cards are now a possibility.");
            return cards[index];
        }
    }

}
