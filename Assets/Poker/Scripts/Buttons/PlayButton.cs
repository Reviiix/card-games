using Base.Scripts;
using Base.Scripts.Buttons;
using Base.Scripts.StateManagement;
using TMPro;
using UnityEngine;

namespace Poker.Scripts.Buttons
{
    public class PlayGameButton :  GameButton
    {
        private const string DealString = "DEAL";
        private const string DrawString = "DRAW";
        [SerializeField] private TMP_Text text; 
    
        public override void OnClick()
        {
            SequentialStateManager.Instance.ProgressState();
        }
    
        public void SetDealText()
        {
            text.text = DealString;
        }
    
        public void SetDrawText()
        {
            text.text = DrawString;
        }
    }
}
