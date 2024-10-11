using Base.Scripts.Cards;
using pure_unity_methods;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Snap.Scripts
{
    public class SnapCardTracker : Singleton<SnapCardTracker>
    {
        [SerializeField] private SnapCardDisplay displayOne;
        [SerializeField] private SnapCardDisplay displayTwo;
        private Card activeCard;
        private Card newCard;
        private bool activeDisplay;
    
        public void DealStateEntered()
        {
            if (!SnapDealer.Instance.CardsRemainingInDeck())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                return;
            }
            activeCard = newCard;
            newCard = SnapDealer.Instance.TakeRandomCardFromDeck();
            SetCardSlots(activeCard, newCard);
            activeDisplay = !activeDisplay;
        }

        private void SetCardSlots(Card cardOne, Card cardTwo)
        {
            if (activeDisplay)
            {
                displayOne.SetNewCard(cardOne);
                displayTwo.SetNewCard(cardTwo);
            }
            else
            {
                displayOne.SetNewCard(cardTwo);
                displayTwo.SetNewCard(cardOne);
            }

        }

        public bool CardsMatch()
        {
            return activeCard.GetRank() == newCard.GetRank();
        }
    }
}
