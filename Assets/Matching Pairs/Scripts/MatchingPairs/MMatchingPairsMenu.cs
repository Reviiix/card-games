using MatchingPairs.pure_unity_methods;
using MatchingPairs.StateManagement;
using UnityEngine;
using UnityEngine.UI;

namespace MatchingPairs
{
    public class MMatchingPairsMenu : Singleton<MMatchingPairsMenu>
    {
        [SerializeField] private Canvas display;
        [SerializeField] private Button menuButton;
        [SerializeField] private Button playButton;
        [SerializeField] private Button restartButton;

        public void Initialise()
        {
            menuButton.onClick.AddListener(OpenMenu);
            playButton.onClick.AddListener(PlayPressed);
            restartButton.onClick.AddListener(RestartPressed);
        }

        public void OpenMenu()
        {
            var isMenuState = MatchingPairsStateManager.Instance.IsMenuState();
            MatchingPairsStateManager.Instance.SetMenuState(!isMenuState, () =>
            {
                display.enabled = !isMenuState;
            });
        }

        private void PlayPressed()
        {
            if (MatchingPairsEvaluator.Instance.IsWon())
            {
                MatchingPairsStateManager.Instance.RestartGame();
            }
            else
            {
                OpenMenu();
            }
        }

        private static void RestartPressed()
        {
            MatchingPairsStateManager.Instance.RestartGame();
        }
    }
}
