using System;

public class Hold : State
{
    public override void OnStateEnter(Action callBack)
    {
        ButtonManager.Instance.EnableAllButtons();
        ButtonManager.Instance.Play.SetDrawText();
        callBack();
    }
}