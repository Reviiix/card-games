using System;
using MatchingPairs.pure_unity_methods;
using MatchingPairs.StateManagement;
using MatchingPairs.StateManagement.States;

namespace MatchingPairs.ScoreSystem
{
    public class MatchingPairsScore : Singleton<MatchingPairsScore>
    {
        public static Action<ScoreInformation> OnScoreUpdate;
        private ScoreInformation score = new();
        private int comboTracker;

        public void Initialise()
        {
            score = new ScoreInformation();
            OnScoreUpdate?.Invoke(score);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            MatchingPairs.StateManagement.States.Evaluation.OnEvaluationComplete += OnEvaluationComplete;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            MatchingPairs.StateManagement.States.Evaluation.OnEvaluationComplete -= OnEvaluationComplete;
        }

        private void OnEvaluationComplete(bool match)
        {
            AddTurns(1);
            AddMatches(match ? 1 : 0);
            CheckCombo(match);

            if (MatchingPairsEvaluator.Instance.IsWon())
            {
                MatchingPairsStateManager.Instance.GameWon();
            }
        }

        private void CheckCombo(bool match)
        {
            if (match)
            {
                comboTracker++;
                if (IsValidCombo(comboTracker))
                {
                    AddCombo(1);
                }
            }
            else
            {
                comboTracker = 0;
            }
        }

        private void AddCombo(int combo)
        {
            score.Combo += combo;
            OnScoreUpdate?.Invoke(score);
        }

        private void AddMatches(int matches)
        {
            score.Matches += matches;
            OnScoreUpdate?.Invoke(score);
        }
    
        private void AddTurns(int turns)
        {
            score.Turns += turns;
            OnScoreUpdate?.Invoke(score);
        }
        private static  bool IsValidCombo(int combo)
        {
            return combo >= 2;
        }
    }

    public class ScoreInformation
    {
        public int Combo;
        public int Matches;
        public int Turns;
        public int Score => Matches * 10 * (Combo+1);
    }
}