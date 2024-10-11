using System;
using Base.Scripts.StateManagement;
using Snap.Scripts.Score;
using UnityEngine;

namespace Snap.Scripts.StateManagement
{
    public class Snap : State
    {
        [SerializeField] private SnapWinScreen winScreen;

        public override void OnStateEnter(Action callBack)
        {
            SnapScoreManager.Instance.IncreaseScore();
            winScreen.Activate(GetWaitTime(), callBack);
        }
        
        private static int GetWaitTime()
        {
            return ((SnapStateManager)SequentialStateManager.Instance).waitTime;
        }
    }
}
