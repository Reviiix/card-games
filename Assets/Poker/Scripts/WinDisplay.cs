using TMPro;
using UnityEngine;

namespace Poker.Scripts
{
    public class WinDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text amountDisplay;
        [SerializeField] private TMP_Text evaluationDisplay;

        public void ShowWin(Evaluation evalToShow)
        {
            evaluationDisplay.text = evalToShow.ToString();
            amountDisplay.text = (int)evalToShow + "";
        }
    }
}
