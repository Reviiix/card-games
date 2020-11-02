using System;
using UnityEngine;

namespace Poker
{
    [Serializable]
    public struct Card
    {
        [SerializeField] private Suit suit;
        [SerializeField] private Rank rank;
        [SerializeField] private Sprite cardSprite;
        public bool inPlay { get; private set; }

        public Suit ReturnSuit()
        {
            return suit;
        }
        
        public Rank ReturnRank()
        {
            return rank;
        }
        
        public Sprite ReturnCardSprite()
        {
            return cardSprite;
        }

        public void RemoveFromPlay()
        {
            inPlay = false;
        }
        
        public void ReturnToPack()
        {
            inPlay = false;
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
        
        Ace = 14,
    
        Joker,
    }
}