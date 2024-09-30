using MatchingPairs.Cards;
using MatchingPairs.GridSystem;
using MatchingPairs.pure_unity_methods;
using MatchingPairs.ScoreSystem;
using MatchingPairs.StateManagement;

namespace MatchingPairs
{
    /// <summary>
    /// Using a project initializer can help reduce race conditions by allowing for more granular control of the load sequence.
    /// </summary>
    public class MaatchingPairsInitializer : Singleton<MaatchingPairsInitializer>
    {
        private void Start()
        {
            MatchingPairsStateManager.Instance.Initialise();
            MMatchingPairsMenu.Instance.Initialise();
            MatchingPairsDeckOfCards.Instance.Initialise();
            MatchingPairsScore.Instance.Initialise();
            StartCoroutine(MatchingPairsGridManager.Instance.Initialise(() =>
            {
                //dependent on GridManager
                MatchingPairsAudio.Instance.Initialise();
                MatchingPairsEvaluator.Instance.Initialise();
            }));
        }
    }
}
