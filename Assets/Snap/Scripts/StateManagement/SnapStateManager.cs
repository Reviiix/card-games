using Base.Scripts.StateManagement;
using pure_unity_methods;
using UnityEngine;

namespace Snap.Scripts.StateManagement
{
    public class SnapStateManager : SequentialStateManager
    {
        [SerializeField] private State snapState;
        [Range(1,10)] public int waitTime = 1;

        public void SetSnapState()
        {
            ActiveState = snapState;
            ActiveState.OnStateEnter(()=>
            {
                if (ActiveState.progressImmediately)
                {
                    StartCoroutine(Wait.WaitThenCallBack(waitTime, ProgressState));
                }
            });
        }

        public bool IsWaitState()
        {
            return ActiveState is CheckForSnap;
        }
        
        public bool IsSnapState()
        {
            return ActiveState is Snap;
        }
    }
}
