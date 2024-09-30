using System;
using Base.Scripts.StateManagement;

namespace Poker.Scripts.StateManager
{
    public class Evaluation : State
    {
        public override void OnStateEnter(Action callBack)
        {
            callBack();
        }
    }
}
