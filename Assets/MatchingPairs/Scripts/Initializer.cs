using Base.Scripts.Cards;
using Base.Scripts.pure_unity_methods;
using MatchingPairs.Scripts.GridSystem;
using MatchingPairs.Scripts.ScoreSystem;
using MatchingPairs.Scripts.StateManagement;
using Poker;

namespace MatchingPairs.Scripts
{
    /// <summary>
    /// Using a project initializer can help reduce race conditions by allowing for more granular control of the load sequence.
    /// </summary>
    public class Initializer : Singleton<Initializer>
    {
        private void Start()
        {
            MatchingPairsStateManager.Instance.Initialise();
            DeckOfCards.Instance.Initialise();
            Score.Instance.Initialise();
            StartCoroutine(MatchingPairsGridManager.Instance.Initialise(() =>
            {
                //dependent on GridManager
                MatchingPairsAudioManager.Instance.Initialise();
                Evaluator.Instance.Initialise();
            }));
        }
    }
}
