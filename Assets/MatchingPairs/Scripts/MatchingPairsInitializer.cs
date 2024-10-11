
using MatchingPairs.Scripts.GridSystem;
using MatchingPairs.Scripts.ScoreSystem;
using MatchingPairs.Scripts.StateManagement;
using pure_unity_methods;

namespace MatchingPairs.Scripts
{
    /// <summary>
    /// Using a project initializer can help reduce race conditions by allowing for more granular control of the load sequence.
    /// </summary>
    public class MatchingPairsInitializer : Singleton<MatchingPairsInitializer>, IInitializer
    {
        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            MatchingPairsStateManager.Instance.Initialise();
            MatchingPairsDealer.Instance.Initialise();
            MatchingPairsScoreManager.Instance.Initialise();
            StartCoroutine(GridManager.Instance.Initialise(() =>
            {
                //dependent on GridManager
                MatchingPairsAudioManager.Instance.Initialise();
                Evaluator.Instance.Initialise();
            }));
        }
    }
}
