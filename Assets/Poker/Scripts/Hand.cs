using System;
using System.Collections;
using System.Collections.Generic;
using Base.Scripts.Cards;
using UnityEngine;

namespace Poker.Scripts
{
    public class Hand : MonoBehaviour
    {
        private static readonly WaitForSeconds TimeBetweenDealingCards = new WaitForSeconds(0.3f);
        private Evaluation evaluation;
        [SerializeField] private WinDisplay winDisplay;
        [SerializeField] private CardSlot[] cardsSlots;
        public int AmountOfCards => cardsSlots.Length;

        public void Deal(Card[] cards, Action callBack)
        {
            StartCoroutine(DealRoutine(cards, callBack));
        }

        private IEnumerator DealRoutine(IReadOnlyList<Card> cards, Action callBack)
        {
            evaluation = Evaluator.Evaluate(cards);
            
            for (var i = 0; i < cards.Count; i++)
            {
                if (cardsSlots[i].Held) continue;
                cardsSlots[i].SetNewCard(cards[i]);
                yield return TimeBetweenDealingCards;
            }

            winDisplay.ShowWin(evaluation);
            callBack();
        }
        
        public void ResetCards(Action callBack, bool ignoreHeld = false)
        {
            StartCoroutine(ResetCardsRoutine(callBack, ignoreHeld));
        }
        
        private IEnumerator ResetCardsRoutine(Action callBack, bool ignoreHeld = false)
        {
            yield return TimeBetweenDealingCards;
            foreach (var card in cardsSlots)
            {
                card.ResetCard(ignoreHeld);
            }
            yield return TimeBetweenDealingCards;
            callBack();
        }
    }

}
