using pure_unity_methods.Effects;
using TMPro;
using UnityEngine;

namespace MatchingPairs.Scripts.ScoreSystem
{
    [RequireComponent(typeof(TMP_Text))]
    public class RollupScoreDisplay : MonoBehaviour
    {
        private bool active;
        private TMP_Text text;
        private int previousScore;
        private Coroutine rollupRoutine;
        [SerializeField] private int rollUpTime;
        [SerializeField] private string prefix;
    
        private void OnEnable()
        {
            Score.OnScoreUpdate += OnScoreUpdate;
            text = GetComponent<TMP_Text>();
        }
    
        private void OnDisable()
        {
            Score.OnScoreUpdate -= OnScoreUpdate;
        }

        private void OnScoreUpdate(ScoreInformation scoreInformation)
        {
            var score = scoreInformation.Score;
            if (!ShouldTriggerRollup(previousScore, score))
            {
                active = false;
                return;
            }
            if (ShouldStopRollup())
            {
                StopCoroutine(rollupRoutine);
            }
            active = true;
            RollUp(score);
        }
    
        private void RollUp(int newScore)
        {
            rollupRoutine = StartCoroutine(NumberRollup.Rollup(text, previousScore, newScore, prefix, string.Empty, rollUpTime, () =>
            {
                previousScore = newScore;
                active = false;
            }));
        }

        private static bool ShouldTriggerRollup(int oldScore, int newScore)
        {
            return newScore != 0 && newScore != oldScore;
        }
    
        private bool ShouldStopRollup()
        {
            return active && rollupRoutine != null;
        }

    }
}
