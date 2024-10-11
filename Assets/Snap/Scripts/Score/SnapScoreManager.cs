using System;
using pure_unity_methods;
using UnityEngine;

namespace Snap.Scripts.Score
{
    public class SnapScoreManager : Singleton<SnapScoreManager>
    {
        private int Score;
        [Range(0,100)]
        [SerializeField] private int scoreIncrement = 50;
        public static Action<int> OnScoreChange;

        public void Initialise()
        {
            OnScoreChange?.Invoke(0);
        }

        #if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                IncreaseScore();
            }
        }
        #endif

        public void IncreaseScore()
        {
            Score += scoreIncrement;
            OnScoreChange(Score);
        }
    }
}
