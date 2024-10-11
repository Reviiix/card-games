using TMPro;
using UnityEngine;

namespace Snap.Scripts
{
    public class CardsRemainingDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text display;
        [SerializeField] private string prefix;
        private int deckCount;
        
        private void OnEnable()
        {
            SnapDealer.OnCardRemovedFromDeck += OnCountChange;
        }
        
        private void OnDisable()
        {
            SnapDealer.OnCardRemovedFromDeck -= OnCountChange;
        }

        private void OnCountChange(int cardsInPlay)
        {
            display.text = $"{prefix} \n {cardsInPlay}/{SnapDealer.Instance.GetAmountOfCardsInDeck()}";
        }
    }
}