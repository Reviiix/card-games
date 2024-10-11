using Base.Scripts.StateManagement;
using pure_unity_methods;
using Snap.Scripts.StateManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Snap.Scripts
{
    [RequireComponent(typeof(Button))]
    public class SnapButton : Singleton<SnapButton>
    {
        private SnapStateManager stateManagerCache;
        private SnapCardTracker snapCardTrackerCache;

        public void Initialise()
        {
            stateManagerCache = (SnapStateManager)SequentialStateManager.Instance;
            GetComponent<Button>().onClick.AddListener(OnButtonPress);
            snapCardTrackerCache = SnapCardTracker.Instance;
        }

        private void OnButtonPress()
        {
            if (!stateManagerCache.IsWaitState()) return;

            if (snapCardTrackerCache.CardsMatch())
            {
                stateManagerCache.SetSnapState();
            }
        }
    }
}
