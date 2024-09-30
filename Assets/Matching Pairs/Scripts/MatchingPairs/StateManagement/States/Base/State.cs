using System;
using UnityEngine;

namespace MatchingPairs.StateManagement.States.Base
{
    [Serializable]
    public abstract class State : MonoBehaviour, IState
    {
        public bool progressImmediately;
    
        public virtual void OnStateEnter(Action callBack)
        {
            throw new NotImplementedException();
        }
    }
}
