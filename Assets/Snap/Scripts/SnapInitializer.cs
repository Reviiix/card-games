using Base.Scripts.StateManagement;
using pure_unity_methods;
using Snap.Scripts.Score;
using Snap.Scripts.StateManagement;

namespace Snap.Scripts
{
    /// <summary>
    /// Using a project initializer can help reduce race conditions by allowing for more granular control of the load sequence.
    /// </summary>
    public class SnapInitializer : Singleton<SnapInitializer>, IInitializer
    {
        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            SnapDealer.Instance.Initialise();
            SnapScoreManager.Instance.Initialise();
            SnapButton.Instance.Initialise();
            ((SnapStateManager)SequentialStateManager.Instance).Initialise();
        }
    }
}
