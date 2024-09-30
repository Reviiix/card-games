using System;
using Base.Scripts.StateManagement;

namespace MatchingPairs.Scripts.StateManagement.States
{
    public class Evaluation : State
    {
        public static Action<bool> OnEvaluationComplete;
    
        public override void OnStateEnter(Action callBack)
        {
            //Debug.Log($"Entering {nameof(Evaluation)} state."); //Debug tool.
            OnEvaluationComplete?.Invoke(Evaluator.Instance.IsMatch());
            callBack();
        }
    }
}
