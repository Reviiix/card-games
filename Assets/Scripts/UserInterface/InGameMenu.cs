using System;
using Poker;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    [Serializable]
    public class InGameUserInterface : UserInterface, IUserInterface
    {
        public TMP_Text timeText;
        public TMP_Text scoreText;

        [SerializeField]
        private Button dealButton;
        [SerializeField]
        private Button drawButton;

        private void Initialise()
        {
            AddButtonEvents();
        }

        //Game Manager not in the init scene wait before using for it.
        private void AddButtonEvents()
        {
            //dealButton.onClick.AddListener(GameManager.Instance.dealer.Deal);
            
            //drawButton.onClick.AddListener(GameManager.Instance.dealer.Draw);
        }

        public void Enable(bool state = true)
        {
            display.enabled = state;
        }
    }
}
