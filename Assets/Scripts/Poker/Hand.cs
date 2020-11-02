using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Poker
{
    public class Hand : MonoBehaviour
    {
        private static readonly WaitForSeconds TimeBetweenDealingCards = new WaitForSeconds(0.3f);
        private Evaluation _evaluation;
        [SerializeField] private WinDisplay winDisplay;
        [SerializeField] private CardInHand[] cardsInHand;
        public int AmountOfCards => cardsInHand.Length;

        public void Deal(Card[] cards)
        {
            StartCoroutine(DealRoutine(cards));
        }

        private IEnumerator DealRoutine(IReadOnlyList<Card> cards)
        {
            _evaluation = Evaluator.Evaluate(cards);
            
            for (var i = 0; i < cards.Count; i++)
            {
                cardsInHand[i].SetNew(cards[i]);
                yield return TimeBetweenDealingCards;
            }

            winDisplay.ShowWin(_evaluation);
        }
    }
    
    [Serializable]
    public struct CardInHand
    {
        public Image cardImage;
        public Card CardValue { get; private set; }

        public void SetNew(Card newCard)
        {
            SetNewValue(newCard);
            SetImage(newCard.ReturnCardSprite());
        }

        private void SetNewValue(Card newCardValue) => CardValue = newCardValue;
        
        private void SetImage(Sprite newSprite) => cardImage.sprite = newSprite;
    }

}
