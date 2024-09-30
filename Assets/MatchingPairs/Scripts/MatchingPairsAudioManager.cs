using pure_unity_methods;
using UnityEngine;

namespace MatchingPairs.Scripts
{
    
    public class MatchingPairsAudioManager : AudioManager
    {
        [SerializeField] private AudioClip success;
        [SerializeField] private AudioClip failure;
        [SerializeField] private AudioClip win;

        public void PlaySuccess()
        {
            PlayClip(success);
        }
    
        public void PlayFailure()
        {
            PlayClip(failure);
        }
    
        public void PlayGameWon()
        {
            PlayClip(win);
        }
    }
}
