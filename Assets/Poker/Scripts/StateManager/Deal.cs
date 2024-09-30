using System;
using Base.Scripts.StateManagement;
using Poker.Scripts.Buttons;

namespace Poker.Scripts.StateManager
{
    public class Deal : State
    {
        public override void OnStateEnter(Action callBack)
        {
            ButtonManager.Instance.EnableAllButtons(false);
            Dealer.Instance.DealCards(callBack);
        }
    }
}