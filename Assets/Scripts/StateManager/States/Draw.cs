using System;
using Poker;

public class Draw : State
{
    public override void OnStateEnter(Action callBack)
    {
        ButtonManager.Instance.EnableAllButtons(false);
        Dealer.Instance.DrawCards(callBack);
    }
}
