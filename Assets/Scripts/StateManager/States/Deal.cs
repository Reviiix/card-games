using System;
using Poker;

public class Deal : State
{
    public override void OnStateEnter(Action callBack)
    {
        ButtonManager.Instance.EnableAllButtons(false);
        Dealer.Instance.DealCards(callBack);
    }
}
