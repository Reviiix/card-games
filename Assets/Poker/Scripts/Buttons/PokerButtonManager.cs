using Base.Scripts.Buttons;
using pure_unity_methods;
using UnityEngine;

namespace Poker.Scripts.Buttons
{
    public class PokerButtonManager : Singleton<PokerButtonManager>
    {
        public PlayGameButton Play { get; private set; }
        [SerializeField] private GameButton[] buttons;

        public void Awake()
        {
            AssignReferencesFromList();
        }

        private void AssignReferencesFromList()
        {
            foreach (var button in buttons)
            {
                if (button is PlayGameButton gameButton)
                {
                    Play = gameButton;
                }
            }
        }

        public void EnableAllButtons(bool state = true)
        {
            foreach (var button in buttons)
            {
                button.Enable(state);
            }
        }
    }
}
