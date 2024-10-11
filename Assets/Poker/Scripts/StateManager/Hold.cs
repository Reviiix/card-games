using System;
using Base.Scripts.StateManagement;
using Poker.Scripts.Buttons;

namespace Poker.Scripts.StateManager
{
    public class Hold : State
    {
        public override void OnStateEnter(Action callBack)
        {
            PokerButtonManager.Instance.EnableAllButtons();
            PokerButtonManager.Instance.Play.SetDrawText();
            callBack();
        }
    }
}