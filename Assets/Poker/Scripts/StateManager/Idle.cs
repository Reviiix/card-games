using System;
using Base.Scripts.StateManagement;
using Poker.Scripts.Buttons;

namespace Poker.Scripts.StateManager
{
    public class Idle : State
    {
        public override void OnStateEnter(Action callBack)
        {
            ButtonManager.Instance.EnableAllButtons();
            ButtonManager.Instance.Play.SetDealText();
            callBack();
        }
    }
}
