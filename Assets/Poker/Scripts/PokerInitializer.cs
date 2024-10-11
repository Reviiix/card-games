using Base.Scripts.StateManagement;
using pure_unity_methods;

namespace Poker.Scripts
{
    /// <summary>
    /// Using a project initializer can help reduce race conditions by allowing for more granular control of the load sequence.
    /// </summary>
    public class PokerInitializer : Singleton<PokerInitializer>, IInitializer
    {
        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            SequentialStateManager.Instance.Initialise();
            PokerDealer.Instance.Initialise();
        }
    }
}