using System;

public class Evaluation : State
{
    public override void OnStateEnter(Action callBack)
    {
        callBack();
    }
}
