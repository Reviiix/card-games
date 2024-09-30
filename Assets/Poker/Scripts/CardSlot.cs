using Base.Scripts;
using Base.Scripts.Cards;
using Base.Scripts.StateManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Poker.Scripts
{
    public class CardSlot : MonoBehaviour
    {
        public Card Card { get; private set; }
        public bool Held { get; private set; }
        
        [SerializeField] private Image cardImage;
        [SerializeField] private Button holdButton;
        [SerializeField] private TMP_Text heldDisplay;

        public void Awake()
        {
            holdButton.onClick.AddListener(Hold);
        }

        public void SetNewCard(Card card)
        {
            SetCard(card);
            SetSprite(card.GetCardSprite());
        }

        private void Hold()
        {
            if (!SequentialStateManager.Instance.IsHoldState()) return;
            
            Held = !Held;
            EnableHeldDisplay(Held);
        }

        private void SetCard(Card card)
        {
            Card = card;
        }
        
        private void SetSprite(Sprite newSprite)
        {
            cardImage.sprite = newSprite;
        }

        private void EnableHeldDisplay(bool state = true)
        {
            heldDisplay.enabled = state;
        }
        
        public void ResetCard(bool ignoreHeld = false)
        {
            if (Card.GetRank() == Rank.Joker) return; //Joker likely means card is not set yet
        
            if (Held && !ignoreHeld) return;
            Dealer.Instance.ReturnCardToDeck(Card);
            Card = new Card();
            Held = false;
            EnableHeldDisplay(Held);
            SetSprite(Dealer.Instance.GetCardBack());
        }
    }
}