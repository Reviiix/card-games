using pure_unity_methods.Effects;
using TMPro;
using UnityEngine;

namespace Snap.Scripts.Score
{
    public class ScoreDisplay : MonoBehaviour
    {
        private int scoreCache;
        [SerializeField] private int rollUpTime;
        [SerializeField] private TMP_Text display;
        [SerializeField] private string prefix;
        
        private void OnEnable()
        {
            SnapScoreManager.OnScoreChange += OnScoreChange;
        }
        
        private void OnDisable()
        {
            SnapScoreManager.OnScoreChange -= OnScoreChange;
        }

        private void OnScoreChange(int score)
        {
            StartCoroutine(NumberRollup.Rollup(display, scoreCache, score, prefix, string.Empty, rollUpTime));
        }
    }
}
