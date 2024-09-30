using System;
using MatchingPairs.StateManagement.States.Base;
using UnityEngine;

namespace MatchingPairs.StateManagement.States
{
    public class PickOne : State
    {
        public override void OnStateEnter(Action callBack)
        {
            Debug.Log($"Entering {nameof(PickOne)} state.");
            callBack();
        }
    }
}
