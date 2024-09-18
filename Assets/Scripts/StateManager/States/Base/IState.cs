using System;

public interface IState
{
    public void OnStateEnter(Action callBack);
}
