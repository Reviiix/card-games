using System;
using MatchingPairs.StateManagement.States.Base;
using UnityEngine;

namespace MatchingPairs.StateManagement.States
{
    public class Evaluation : State
    {
        public static Action<bool> OnEvaluationComplete;
    
        public override void OnStateEnter(Action callBack)
        {
            Debug.Log($"Entering {nameof(Evaluation)} state.");
            OnEvaluationComplete?.Invoke(MatchingPairsEvaluator.Instance.IsMatch());
            callBack();
        }
    }
}
