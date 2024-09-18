using System;

public class Win : State
{
    public override void OnStateEnter(Action callBack)
    {
        callBack();
    }
}
