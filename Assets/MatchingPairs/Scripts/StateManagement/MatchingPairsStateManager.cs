using Base.Scripts;
using Base.Scripts.StateManagement;
using MatchingPairs.Scripts.StateManagement.States;
using UnityEngine.SceneManagement;
using Evaluation = Poker.Scripts.StateManager.Evaluation;

namespace MatchingPairs.Scripts.StateManagement
{
    public class MatchingPairsStateManager : SequentialStateManager
    {
        public void GameWon()
        {
            RestartGame();
        }

        private static void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public bool IsPickOne()
        {
            return ActiveState is PickOne;
        }
    
        public bool IsPickTwo()
        {
            return ActiveState is PickTwo;
        }
    
        public bool IsEvaluation()
        {
            return ActiveState is Evaluation;
        }
    }
}
