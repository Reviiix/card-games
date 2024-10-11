using System;
using Base.Scripts.StateManagement;

namespace Snap.Scripts.StateManagement
{
    public class Deal : State
    {
        public override void OnStateEnter(Action callBack)
        {
            SnapCardTracker.Instance.DealStateEntered();
            callBack();
        }
    }
}
