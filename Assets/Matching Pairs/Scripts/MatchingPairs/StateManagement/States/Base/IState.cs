using System;

namespace MatchingPairs.StateManagement.States.Base
{
    public interface IState
    {
        public void OnStateEnter(Action callBack);
    }
}
