using System;
using Base.Scripts.StateManagement;

namespace Poker.Scripts.StateManager
{
    public class Win : State
    {
        public override void OnStateEnter(Action callBack)
        {
            callBack();
        }
    }
}
