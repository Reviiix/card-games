using System;
using System.Text;
using TMPro;
using UnityEngine;

namespace MatchingPairs.ScoreSystem
{
    public class ScoreDisplay : MonoBehaviour
    {
        public DisplayItem combo;
        public DisplayItem matches;
        public DisplayItem turns;

        private void OnEnable()
        {
            MatchingPairsScore.OnScoreUpdate += OnScoreUpdate;
        }

        private void OnDisable()
        {
            MatchingPairsScore.OnScoreUpdate -= OnScoreUpdate;
        }

        private void OnScoreUpdate(ScoreInformation score)
        {
            combo.OnScoreUpdate(score.Combo);
            matches.OnScoreUpdate(score.Matches);
            turns.OnScoreUpdate(score.Turns);
        }

        [Serializable]
        public class DisplayItem
        {
            [SerializeField] private TMP_Text value;
            [SerializeField] private string prefix;
            private readonly StringBuilder stringBuilder = new ();
        
            public void OnScoreUpdate(int score)
            {
                value.text = stringBuilder.Append(prefix).Append(score.ToString()).ToString();
                stringBuilder.Clear();
            }
        }
    }
}
