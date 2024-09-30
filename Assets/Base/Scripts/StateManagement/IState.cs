using System;

namespace Base.Scripts.StateManagement
{
    public interface IState
    {
        public void OnStateEnter(Action callBack);
    }
}
