using System;
using System.Collections;
using Base.Scripts.StateManagement;
using UnityEngine;

namespace Snap.Scripts.StateManagement
{
    public class CheckForSnap : State
    {
        private Coroutine stateRoutine;
        
        public override void OnStateEnter(Action callBack)
        {
            stateRoutine = StartCoroutine(CheckForSnapState(callBack));
        }

        private IEnumerator CheckForSnapState(Action callBack)
        {
            var timePassed = 0f;
            const float increment = 0.1f;
            var waitTime = GetWaitTime();
            while (timePassed < waitTime)
            {
                yield return new WaitForSeconds(increment);
                if (IsSnapState())
                {
                    StopCoroutine(stateRoutine);
                    yield break;
                }

                timePassed += increment;
            }

            if (!IsSnapState())
            {
                callBack();
            }
        }

        private static bool IsSnapState()
        {
            return ((SnapStateManager)SequentialStateManager.Instance).IsSnapState();
        }
        
        private static  int GetWaitTime()
        {
            return ((SnapStateManager)SequentialStateManager.Instance).waitTime;
        }
    }
}
