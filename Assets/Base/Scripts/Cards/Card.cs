using System;
using UnityEngine;

namespace Base.Scripts.Cards
{
    [Serializable]
    public struct Card
    {
        public int DeckIndex { get; private set; }
        [SerializeField] private Suit suit;
        [SerializeField] private Rank rank;
        [SerializeField] private Sprite cardSprite;
        public bool InPlay { get; private set; }

        public void Initialise(int index)
        {
            DeckIndex = index;
            InPlay = false;
        }

        public Suit GetSuit()
        {
            return suit;
        }
        
        public Rank GetRank()
        {
            return rank;
        }
        
        public Sprite GetCardSprite()
        {
            return cardSprite;
        }
        

        public void MarkActive()
        {
            InPlay = true;
        }
        
        public void MarkInActive()
        {
            InPlay = false;
        }
        
    }
    
    public enum Suit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades,
    }

    public enum Rank
    {
        Joker = 0,
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
    }
}