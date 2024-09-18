using System;

public class Idle : State
{
    public override void OnStateEnter(Action callBack)
    {
        ButtonManager.Instance.EnableAllButtons();
        ButtonManager.Instance.Play.SetDealText();
        callBack();
    }
}
