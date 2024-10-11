using Base.Scripts;
using Base.Scripts.Cards;
using Base.Scripts.StateManagement;
using Poker.Scripts.StateManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Poker.Scripts
{
    public class PokerCardDisplay : CardDisplay
    {
        public bool Held { get; private set; }
        [SerializeField] private Button holdButton;
        [SerializeField] private TMP_Text heldDisplay;

        public void Awake()
        {
            holdButton.onClick.AddListener(Hold);
        }

        private void Hold()
        {
            if (!((PokerStateManager)SequentialStateManager.Instance).IsHoldState()) return;
            
            Held = !Held;
            EnableHeldDisplay(Held);
        }

        private void EnableHeldDisplay(bool state = true)
        {
            heldDisplay.enabled = state;
        }
        
        public void ResetCard(bool ignoreHeld = false)
        {
            if (Card.IsJoker()) return; //Joker likely means card is not set yet
        
            if (Held && !ignoreHeld) return;
            PokerDealer.Instance.ReturnCardToDeck(Card);
            Card = new Card();
            Held = false;
            EnableHeldDisplay(Held);
            SetSprite(PokerDealer.Instance.GetCardBack());
        }
        
        protected override Sprite GetSprite(Card card)
        {
            return card.IsJoker() ? PokerDealer.Instance.GetCardBack() : card.GetCardSprite();
        }
    }
}