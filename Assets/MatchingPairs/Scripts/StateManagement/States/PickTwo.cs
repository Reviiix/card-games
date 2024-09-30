using System;
using Base.Scripts.StateManagement;
using UnityEngine;

namespace MatchingPairs.Scripts.StateManagement.States
{
    public class PickTwo : State
    {
        public override void OnStateEnter(Action callBack)
        {
            Debug.Log($"Entering {nameof(PickOne)} state.");
            callBack();
        }
    }
}