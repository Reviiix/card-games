using Snap.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Base.Scripts.Cards
{
    public abstract class CardDisplay : MonoBehaviour
    {
        public Card Card { get; protected set; }
        [SerializeField] protected Image cardImage;
        
        public void SetNewCard(Card card)
        {
            SetCard(card);
            SetSprite(GetSprite(card));
        }

        private void SetCard(Card card)
        {
            Card = card;
        }
        
        protected void SetSprite(Sprite newSprite)
        {
            cardImage.sprite = newSprite;
        }

        protected virtual Sprite GetSprite(Card card)
        {
            return card.GetCardSprite();
        }
    }
}
